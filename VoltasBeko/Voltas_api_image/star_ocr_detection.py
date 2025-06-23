import cv2
import numpy as np
import matplotlib.pyplot as plt
import math
#import easyocr
from flask import Flask, request, jsonify
from PIL import Image
import io
from ratings_dection import ratings
from flask import Flask, jsonify,request, send_file
import json
import base64
import time
from collections import Counter
import traceback
import os
from paddleocr import PaddleOCR
import warnings
import signal
import logging
# Suppress DeprecationWarnings from pkg_resources
warnings.filterwarnings("ignore", category=DeprecationWarning, module="pkg_resources")
warnings.filterwarnings("ignore", category=DeprecationWarning)



def mylogger(text):
    try:
        with open("logger.txt","a") as logfile:
            logfile.write(text + "\n")
    except:
        pass

def find_dict_value_old(dicttext):
    iter_values = iter(dicttext.values())
    print("iter_values : ",iter_values)

    volume=None
    energy=None
    for value in iter_values:
        print("value : ",value)
        if "liters" in value:
            #return value
            volume=value
        elif "ANNUAL" in value or "ENERGY" in value or "CONSUMPIION" in value:
            #return next(iter_values)
            energy=next(iter_values)
        else:
            continue
    return energy,volume

def find_dict_value(dicttext):
    iter_values = iter(dicttext.values())
    print("iter_values : ",iter_values)

    volume=''
    energy=''
    previous_value = None
    for value in iter_values:
        value = value.lower()
        print("value : ", value)
        if "liters" in value:
            volume = value
        elif "annual" in value or "energy" in value or "consumption" in value:
            energy = next(iter_values)
        elif "units" in value or "year" in value:
            if energy=='':
                energy=previous_value
        else:
            previous_value = value
            continue
    if volume=='' and len(dicttext) != 0:
        volume = list(dicttext.values())[-1]
    #mylogger(energy)
    return energy,volume
"""
reader = easyocr.Reader(['en'])
def perform_ocr(image):
    result_ocr_one = reader.readtext(image)
##    for detection in result_ocr_one:
##        readtext = detection[1]
##        print("readtext : ",readtext)
    readtext_dict = {detection[0][1][1]: detection[1] for detection in result_ocr_one}
    #for y_coord, readtext in readtext_dict.items():
    #    print("y_coord:", y_coord, "readtext:", readtext)
    print("readtext_dict : ",readtext_dict)
    energy,volume=find_dict_value(readtext_dict)
    return energy,volume
"""

# Initialize PaddleOCR
ocr = PaddleOCR(lang='en',show_log = False)
def PaddleOCR_perform_ocr(image):
    # Perform OCR on the provided image
    cv2.imwrite("ocr_image.jpg",image)
    result = ocr.ocr("ocr_image.jpg", cls=False)#image
    
    # Extract text and coordinates and structure them into a dictionary
    readtext_dict = {}
    if result is not None:
        for res in result:
            if res is not None:  # Check if res is not None
                for line in res:
                    text = line[1][0]
                    y_coord = line[0][1][1]  #y-coordinate
                    readtext_dict[y_coord] = text
    # Return the dictionary containing detected text
    energy,volume=find_dict_value(readtext_dict)
    energy=''.join(filter(str.isdigit,energy))
    volume=''.join(filter(str.isdigit,volume))
    #mylogger(str(readtext_dict))
    return energy,volume

def result(image):
    height, width = image.shape[:2]
    midpoint = height // 2
    upper_half = image[0:midpoint, :]
    lower_half = image[midpoint:height, :]

    #image for ratings
    h, w = upper_half.shape[:2]
    mid = h // 2
    rating_img=upper_half[0:mid+10, :]
    starRating=ratings(rating_img)#get star

    #get image for ocr
    lower_height = lower_half.shape[0]
    part_height = lower_height // 4
    # Display each part of the image
    #energy=lower_half[:part_height, :]
    ocr_image=image[midpoint-20:midpoint+(3*part_height), :]
    #cv2.imwrite("ocr_image.jpg",ocr_image)
    
    #uncomment if you want to see image
##    fig, axes = plt.subplots(1, 2, figsize=(10, 5))
##    axes[0].imshow(cv2.cvtColor(energy, cv2.COLOR_BGR2RGB))
##    axes[0].set_title('energy')
##
##    axes[1].imshow(cv2.cvtColor(volume, cv2.COLOR_BGR2RGB))
##    axes[1].set_title('volume')
##    plt.show()
    #cv2.imwrite("ocr_image.jpg",ocr_image)
    energytext,volumetext=PaddleOCR_perform_ocr(ocr_image)#perform_ocr(ocr_image) #  

    return energytext, volumetext, starRating
    
def stringToImage(base64_string):
    imgdata = base64.b64decode(base64_string)
    return Image.open(io.BytesIO(imgdata))

def opencv_image_to_base64(image):
    # Encode the OpenCV image as a JPEG image in memory
    _, buffer = cv2.imencode('.jpg', image)
    
    # Convert the image buffer to a base64 string
    base64_string = base64.b64encode(buffer).decode('utf-8')

app = Flask(__name__)
app.logger.setLevel(logging.ERROR)

def shutdown_server():
    
    os.kill(os.getpid(), signal.SIGINT)

@app.route('/shutdown', methods=['GET'])
def shutdown():
    os.kill(os.getpid(), signal.SIGINT)
    return jsonify({"message": "Server is shutting down..."})

@app.route("/ServerCheck")
def ServerCheck():
    print("In ServerCheck")
    return {"Server": "OK"}


@app.route('/analyze_image', methods=['POST'])
def analyze_image():
    data = json.loads(request.data)
    image_bytes=data.get("image")
    imgFromcs = stringToImage(image_bytes)
    numpyImage = np.array(imgFromcs)

    print("Image converted to numpy")
    openCvImage = cv2.cvtColor(numpyImage, cv2.COLOR_RGB2BGR)
    #cv2.imwrite("imagefromapi.jpg",openCvImage)
    try:
        energy_consumption, volume, star_rating = result(openCvImage)
        response_data = {
            'EnergyConsumption': energy_consumption,
            'Volume': volume,
            'StarRating': star_rating
        }
        return jsonify(response_data), 200
    except Exception as e:
        error_message = traceback.format_exc()
        print(error_message)
        mylogger(error_message)
        return jsonify({'error': error_message}), 500

if __name__ == '__main__':
    app.run(debug=True, port = 5001)

