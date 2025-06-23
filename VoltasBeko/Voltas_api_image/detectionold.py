import torch
import numpy as np
import cv2
from flask import Flask, jsonify,request, send_file
import json
import base64
from PIL import Image
import io
import time
import torchvision
from collections import Counter
import glob
import os
from ultralytics import YOLO
import traceback
import cv2
import numpy as np
import matplotlib.pyplot as plt
import math
import warnings
import signal

# Suppress DeprecationWarnings from pkg_resources
warnings.filterwarnings("ignore", category=DeprecationWarning, module="pkg_resources")
warnings.filterwarnings("ignore", category=DeprecationWarning)



def mylogger(text):
    try:
        with open("backlogger.txt","a") as logfile:
            logfile.write(text + "\n")
    except:
        pass

names=['Capillary_Tube','Condensor_Tube','Drier_Tube','Lock_Ring']
# Load the YOLOv8 model
modelseg = YOLO('Segbest.pt')#seg5best.pt'Segbest.pt'
#temp detection
#frame=cv2.imread(r'temp.bmp')
#tempimg = cv2.cvtColor(frame, cv2.COLOR_RGB2BGR)
#tempimg = cv2.resize(tempimg, (640,640))
#temp=modelseg.predict(tempimg,conf=0.2,iou=.5)
print("seg model loaded")

def ValidateResults(DetectionsParm,classCorr):
    #for parm in DetectionsParm:
    for item in DetectionsParm:
        region = tuple(map(int, item["region"].split(", ")))
        if region in classCorr:
            classes_to_update = classCorr[region]
            for class_key in classes_to_update:
                item[class_key] = True
    ResultParm=DetectionsParm
    return ResultParm

def dectdifect(frame,DetectionsParm):
    #model.predict('bus.jpg', save=True, imgsz=320, conf=0.5,iou=.7,device=device)
    results = modelseg.predict(frame,conf=0.4,iou=.5)
    # Extract bounding boxes, classes, names, and confidences
    boxes = results[0].boxes.xyxy.tolist()#[x_min, y_min, x_max, y_max]
    classes = results[0].boxes.cls.tolist()
    names = results[0].names
    masks = results[0].masks
    #confidences = results[0].boxes.conf.tolist()
    #class_name= [names[int(class_id)] for class_id in classes]
    p=0
    plotcorr=[]
    cntcorr=[]
    class_name=[]
    dectclassCorr={}
    for box, cls in zip(boxes, classes):#, confidences): conf
        x_min, y_min, x_max, y_max = map(int, box)
        class_label = names[int(cls)]
        #polygon = masks[p].xy[0]
        #p+=1
        # Find the minimum area rectangle
        #rect = cv2.minAreaRect(polygon) #(center(x, y), (width, height), angle of rotation)
        #cntcorr.append([int(rect[0][0]),int(rect[0][1])])
        #class_name.append(class_label)
        # Draw bounding box
        dectclassCorr[[x_min, y_min,x_max, y_max]]=class_label
        frame=cv2.rectangle(frame, (x_min, y_min), (x_max, y_max), (0, 0, 255), 2)
        # Display class name and area
        #label = f"{class_label}: {polyarea:.2f}"
        frame=cv2.putText(frame, class_label, (x_min, y_min - 10), cv2.FONT_HERSHEY_SIMPLEX, 0.5, (255, 42, 4), 2)
        #plotcorr.append([x_min, y_min,x_max, y_max])
    ResParm=ValidateResults(DetectionsParm,dectclassCorr)
    return ResParm#,class_name,cntcorr,plotcorr

def opencv_image_to_base64(image):
    # Encode the OpenCV image as a JPEG image in memory
    _, buffer = cv2.imencode('.jpg', image)
    
    # Convert the image buffer to a base64 string
    base64_string = base64.b64encode(buffer).decode('utf-8')
    
    return base64_string

def stringToImage(base64_string):
    imgdata = base64.b64decode(base64_string)
    return Image.open(io.BytesIO(imgdata))


app = Flask(__name__)

@app.route("/ServerCheck")
def ServerCheck():
    print("In ServerCheck")
    return {"Server": "OK"}

def shutdown_server():
    
    os.kill(os.getpid(), signal.SIGINT)

@app.route('/shutdown', methods=['GET'])
def shutdown():
    os.kill(os.getpid(), signal.SIGINT)
    return jsonify({"message": "Server is shutting down..."})


@app.route('/back_Detection_old', methods=['POST'])
def detection():
    if not request.data:
        return jsonify({'error': 'Empty request'}), 400
    data = json.loads(request.data)
    image_bytes=data.get("image")
    #if image_bytes is not None:
    imgFromcs = stringToImage(image_bytes)
    numpyImage = np.array(imgFromcs)

    print("Image converted to numpy")
    openCvImage = cv2.cvtColor(numpyImage, cv2.COLOR_RGB2BGR)
    try:
        res_frame = dectdifect(openCvImage)
        res_frame = cv2.resize(res_frame,(640,640))
        cv2.imwrite("bact_dect.jpg",res_frame)
        res_frame=opencv_image_to_base64(res_frame)
        response_data={"image":res_frame}
        return jsonify(response_data), 200
    except Exception as e:
        print(traceback.print_exc())
        return jsonify({'error': traceback.print_exc()}), 500
    

@app.route('/back_Detection', methods=['POST'])
def process_data():
    data = request.json
    if data is None:
        return jsonify({"error": "No JSON data received"}), 400
    image_bytes=data.get("image")
    DetectionsParm=data.get("objDetections") #list of dict
    #if image_bytes is not None:
    imgFromcs = stringToImage(image_bytes)
    numpyImage = np.array(imgFromcs)

    print("Image converted to numpy")
    openCvImage = cv2.cvtColor(numpyImage, cv2.COLOR_RGB2BGR)
    try:
        res_parm = dectdifect(openCvImage,DetectionsParm)
        #res_frame = cv2.resize(res_frame,(640,640))
        #cv2.imwrite("bact_dect.jpg",res_frame)
        #res_frame=opencv_image_to_base64(res_frame)
        data["objDetections"]=res_parm
        #response_data={"image":res_frame}
        return jsonify(data), 200
    except Exception as e:
        print(traceback.print_exc())
        return jsonify({'error': traceback.print_exc()}), 500
if __name__ == '__main__':
    app.run(debug=True, port = 5004)

"""
#check for inner rect
def is_inside(inner_rect, outer_rect):
    inner_x1, inner_y1, inner_x2, inner_y2 = inner_rect
    outer_x1, outer_y1, outer_x2, outer_y2 = outer_rect

    return (
        inner_x1 >= outer_x1 and inner_x2 <= outer_x2 and
        inner_y1 >= outer_y1 and inner_y2 <= outer_y2
    )

def find_outer_rectangle(inner_rect, outer_rectangles):
    for outer_rect in outer_rectangles:
        if is_inside(inner_rect, outer_rect):
            print(f"Inner rectangle is inside Outer Rectangle")
            return outer_rect

    return None

#result = find_outer_rectangle(inner_rectangle, outer_rectangles)


def calculate_polygon_area(polygon):
    ppm=0.15789#mmpp
    n = len(polygon)
    area = 0
    for i in range(n):
        x1, y1 = polygon[i]
        x2, y2 = polygon[(i + 1) % n]
        area += (x1 * y2 - x2 * y1)

    area = abs(area) / 2
    area = (area * (ppm**2))#.round(2)
    return round(area, 2)#int(area)
"""