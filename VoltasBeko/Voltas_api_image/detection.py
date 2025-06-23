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
from datetime import datetime
# Suppress DeprecationWarnings from pkg_resources
warnings.filterwarnings("ignore", category=DeprecationWarning, module="pkg_resources")
warnings.filterwarnings("ignore", category=DeprecationWarning)
import logging

def mylogger(text):
    try:
        with open("backlogger.txt","a") as logfile:
            logfile.write(text + "\n")
    except:
        pass

names=['Capillary_Tube','Condensor_Tube','Drier_Tube','Lock_Ring']
names_dict = {'Capillary_Tube': "class_0", 'Condensor_Tube': "class_1", 'Drier_Tube': "class_2", 'Lock_Ring': "class_3"}
# Load the YOLOv8 model
modelseg = YOLO('Segbest.pt')#seg5best.pt'Segbest.pt'
#temp detection
#frame=cv2.imread(r'temp.bmp')
#tempimg = cv2.cvtColor(frame, cv2.COLOR_RGB2BGR)
#tempimg = cv2.resize(tempimg, (640,640))
#temp=modelseg.predict(tempimg,conf=0.2,iou=.5)
print("seg model loaded")

# Online Python compiler (interpreter) to run Python online.
# Write Python 3 code in this online editor and run it.
print("Try programiz.pro")

def find_overlap_percentage(rect1, rect2):
    x1_min, y1_min, x1_max, y1_max = rect1
    x2_min, y2_min, x2_max, y2_max = rect2

    # Calculate the intersection rectangle
    x_overlap = max(0, min(x1_max, x2_max) - max(x1_min, x2_min))
    y_overlap = max(0, min(y1_max, y2_max) - max(y1_min, y2_min))
    intersection_area = x_overlap * y_overlap

    # Calculate the areas of both rectangles
    area_rect1 = (x1_max - x1_min) * (y1_max - y1_min)
    area_rect2 = (x2_max - x2_min) * (y2_max - y2_min)

    # Calculate the overlap percentage
    overlap_percentage = (intersection_area / min(area_rect1, area_rect2)) * 100

    return overlap_percentage

#check for "Inner rectangle is inside Outer Rectangle
def is_inside(inner_rect, outer_rect):
    inner_x1, inner_y1, inner_x2, inner_y2 = inner_rect
    outer_x1, outer_y1, outer_x2, outer_y2 = outer_rect

    return (
        inner_x1 >= outer_x1 and inner_x2 <= outer_x2 and
        inner_y1 >= outer_y1 and inner_y2 <= outer_y2
    )

def ValidateResults(DetectionsParm,classCorr,frame):
    print("DetectionsParm : ",DetectionsParm)
    dectcorr=list(classCorr.keys())#get all detection corr
    print("detection corr : ",dectcorr)
    final_res=[]
    for item in DetectionsParm:
        region = tuple(map(int, item["region"].split(", ")))
        x1, y1, x2, y2 = region
        region=(x1, y1, x1+x2, y1+y2)
        frame=cv2.rectangle(frame, (region[0], region[1]), (region[2], region[3]),(255,0,0), 4)
        thr=item["overlapPercent"]
        for corr in dectcorr:
            overlap=find_overlap_percentage(region, corr)
            if overlap>=thr or is_inside(corr, region):
                cls=classCorr[corr]#detected class
                status=item[cls] #check status 
                if status:
                    res='result_' + cls
                    frame=cv2.rectangle(frame, (corr[0], corr[1]), (corr[2], corr[3]),(0,255,0), 4)
                    print("res",res)
                    item[res]=True #update status
        final_res.append(item)
    return final_res,frame

def dectdifect(frame,DetectionsParm):
    #model.predict('bus.jpg', save=True, imgsz=320, conf=0.5,iou=.7,device=device)
    results = modelseg.predict(frame,conf=0.3,iou=.5)
    # Extract bounding boxes, classes, names, and confidences
    boxes = results[0].boxes.xyxy.tolist()#[x_min, y_min, x_max, y_max]
    classes = results[0].boxes.cls.tolist()
    names = results[0].names
    #masks = results[0].masks
    #confidences = results[0].boxes.conf.tolist()
    #class_name= [names[int(class_id)] for class_id in classes]
    dectclassCorr={}
    label_color = {
        'Capillary_Tube': (55, 50, 255),
        'Condensor_Tube': (0, 55, 255),
        'Drier_Tube': (100, 0, 255),
        'Lock_Ring': (0,100,255),
        }
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
       
        dectclassCorr[(x_min, y_min,x_max, y_max)]=class_label
        bgr = label_color.get(class_label, (0, 0, 0))
        #frame=cv2.rectangle(frame, (x_min, y_min), (x_max, y_max),bgr, 2)
        # Display class name and area
        #label = f"{class_label}: {polyarea:.2f}"
        frame=cv2.putText(frame, class_label, (x_min, y_min - 10), cv2.FONT_HERSHEY_SIMPLEX, 0.5, (200, 42, 4), 2)
        #plotcorr.append([x_min, y_min,x_max, y_max])
    if len(dectclassCorr)>0:
        dectclassCorr = {key: names_dict.get(class_name, class_name) for key, class_name in dectclassCorr.items()} #modifying classes
        ResParm,frame=ValidateResults(DetectionsParm,dectclassCorr,frame)
    else:
        ResParm=DetectionsParm
    return ResParm,  frame#,class_name,cntcorr,plotcorr

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
app.logger.setLevel(logging.ERROR)

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
    
def load_data():
    try:
        with open(r'E:\Software\VoltasBeko_Copy_kk\VoltasBeko\Voltas_api_image\csharp.json', 'r') as f:
            data = json.load(f)
        return data
    except FileNotFoundError:
        return None
folder_path = r"E:\Software\VoltasBeko_Copy_kk\VoltasBeko\Voltas_api_image\process_image"    
@app.route('/back_Detection', methods=['POST'])
def process_data():
    data = request.json
    print("len of request data : ",len(data))
    if data is None:
        return jsonify({"error": "No JSON data received"}), 400
    
    with open('DefectCriteria.json', 'w') as f:
        json.dump(data, f)
    data=load_data()
   ##print("loaded data : ",data)
    image_bytes=data.get("image")
    DetectionsParm=data.get("objDetections") #list of dict
    #print("DetectionsParm : ",DetectionsParm)
    #if image_bytes is not None:
    imgFromcs = stringToImage(image_bytes)
    numpyImage = np.array(imgFromcs)

    print("Image converted to numpy")
    openCvImage = cv2.cvtColor(numpyImage, cv2.COLOR_RGB2BGR)
    try:
        res_parm,res_frame = dectdifect(openCvImage,DetectionsParm)
        res_frame = cv2.resize(res_frame,(640,640))
        current_datetime = datetime.now().strftime("%Y-%m-%d_%H-%M-%S")
        image_name = f"back_dect_{current_datetime}.jpg"
        image_path = os.path.join(folder_path, image_name)
        cv2.imwrite(image_path, res_frame)
        res_frame=opencv_image_to_base64(res_frame)
        for det, res in zip(DetectionsParm, res_parm):
            # Check status of each class and result_class, update finalResult if they are the same
            if all(det[f'class_{i}'] == res[f'result_class_{i}'] for i in range(4)):
                res['finalResult'] = True
        data["image"]=res_frame
        data["objDetections"]=res_parm
        #print("response_data= ",data)
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
    
def find_intersection_percentage(rect1, rect2):
    x1, y1, w1, h1 = rect1
    x2, y2, w2, h2 = rect2
    
    # Calculate coordinates of intersection rectangle
    x_intersect = max(x1, x2)
    y_intersect = max(y1, y2)
    w_intersect = min(x1 + w1, x2 + w2) - x_intersect
    h_intersect = min(y1 + h1, y2 + h2) - y_intersect
    
    # Calculate area of intersection rectangle
    intersection_area = max(0, w_intersect) * max(0, h_intersect)
    
    # Calculate area of each individual rectangle
    area_rect1 = w1 * h1
    area_rect2 = w2 * h2
    
    # Calculate intersection percentage
    if area_rect1 == 0 or area_rect2 == 0:
        return 0
    else:
        intersection_percentage = (intersection_area / min(area_rect1, area_rect2)) * 100
        return intersection_percentage
        
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
"""