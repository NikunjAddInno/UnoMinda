import cv2
import numpy as np
import sys
import base64
import uvicorn
from fastapi import FastAPI, File, UploadFile
from fastapi.responses import JSONResponse
from pydantic import BaseModel
import matplotlib.pyplot as plt
import math
import easyocr
#from flask import Flask, request, jsonify
from PIL import Image
import io
from ratings_dection import ratings
import traceback
import json
import base64
import time
import os
import signal

from collections import Counter
#from star_ocr_detection import process_image
class ImageRequest(BaseModel):
    image: str

class DetectionTask:
    def __init__(self, model_path, class_file):
        self.net = self.load_model(model_path)
        self.classes = self.load_classes(class_file)

    def load_model(self, model_path):
        return cv2.dnn.readNetFromONNX(model_path)

    def load_classes(self, class_file):
        with open(class_file, 'r') as file:
            return [line.strip() for line in file.readlines()]

    def perform_detection(self, frame, confidence_threshold=0.5, nms_threshold=0.5):
        raise NotImplementedError("perform_detection method must be implemented in subclass.")

    def draw_boxes(self, frame, detections):
        raise NotImplementedError("draw_boxes method must be implemented in subclass.")

class CropDetectionTask(DetectionTask):
    def perform_detection(self, frame, confidence_threshold=0.5, nms_threshold=0.5):
        if len(frame.shape) == 2:
            height, width = frame.shape
            channels = 1
            frame = cv2.cvtColor(frame, cv2.COLOR_GRAY2RGB)
        else:
            height, width, channels = frame.shape

        blob = cv2.dnn.blobFromImage(frame, scalefactor=1/255, size=(640, 640), mean=[0, 0, 0], swapRB=True, crop=False)
        self.net.setInput(blob)
        detections = self.net.forward()[0]

        class_ids = []
        confidences = []
        boxes = []

        x_scale, y_scale = width / 640, height / 640

        for row in detections:
            confidence = row[4]
            if confidence > confidence_threshold:
                classes_score = row[5:]
                class_id = np.argmax(classes_score)
                if classes_score[class_id] > confidence_threshold:
                    class_ids.append(class_id)
                    confidences.append(confidence)
                    cx, cy, w, h = row[:4]
                    x1 = int((cx - w/2) * x_scale)
                    y1 = int((cy - h/2) * y_scale)
                    width = int(w * x_scale)
                    height = int(h * y_scale)
                    box = np.array([x1, y1, width, height])
                    boxes.append(box)

        indices = cv2.dnn.NMSBoxes(boxes, confidences, confidence_threshold, nms_threshold)
        return [(boxes[i], self.classes[class_ids[i]], confidences[i]) for i in indices]
class TargetDetectionTask(CropDetectionTask):
    pass
app = FastAPI()

server_process_id = os.getpid()

@app.get("/shutdown")
def shutdown():
    """
    Shutdown the FastAPI server.
    """
    try:
        # Send a SIGINT signal to the server process
        os.kill(server_process_id, signal.SIGINT)
        message = "Server shutting down..."
    except ProcessLookupError:
        message = "Server process not found."

    return JSONResponse(content={"message": message})

@app.get("/ServerCheck")
async def server_check():
    return{"Server": "Ok"}

@app.post('/predict')
async def predictions(request: ImageRequest):
    # Decode base64 image
    image_data = base64.b64decode(request.image)
    nparr = np.frombuffer(image_data, np.uint8)
    frame = cv2.imdecode(nparr, cv2.IMREAD_COLOR)


    if frame is None:
        print("Failed to load the image. Please provide a valid image path.")
        return {"success": False}

    print("Image loaded successfully.")

    crop_task = CropDetectionTask('best_crop.onnx', 'crop.names')
    target_task = TargetDetectionTask('best.onnx', 'voltas.names')

    # Perform crop detection
    crop_detections = crop_task.perform_detection(frame)

    detection_labels = []
    crop_detections_labels=[]# List to store detected labels as strings

    for box, label, confidence in crop_detections:
        crop_detections_labels.append(f"{label}: {confidence:.2f}")
        x1, y1, w, h = box
        roi = frame[y1:y1+h, x1:x1+w]
        target_detections = target_task.perform_detection(roi)
        for target_box, target_label, target_confidence in target_detections:
            detection_labels.append(f"{target_label}: {target_confidence:.2f}")  # Append detected labels
            target_box_adjusted = (target_box[0] + x1, target_box[1] + y1, target_box[2], target_box[3])
            frame = cv2.rectangle(frame, (target_box_adjusted[0], target_box_adjusted[1]),
                                  (target_box_adjusted[0] + target_box_adjusted[2], target_box_adjusted[1] + target_box_adjusted[3]),
                                  (0, 255, 0), 1)
            text = f"{target_label}: {target_confidence:.2f}"
            if target_label=="With_Screw":
                frame = cv2.putText(frame, text, (target_box_adjusted[0], target_box_adjusted[1] - 2),
                                    cv2.FONT_HERSHEY_SIMPLEX, 0.8, (0, 255, 0), 2, cv2.LINE_AA)
            else:
                frame = cv2.putText(frame, text, (target_box_adjusted[0], target_box_adjusted[1] - 2),
                                    cv2.FONT_HERSHEY_SIMPLEX, 0.8, (0, 0, 255), 2, cv2.LINE_AA)

    # Encode image to base64
    _, img_encoded = cv2.imencode('.png', frame)
    img_base64 = base64.b64encode(img_encoded).decode('utf-8')

    return {"success": True, "image": img_base64,"crop_detections_labels": crop_detections_labels,"detection_labels": detection_labels}


"""
class ImageRequest(BaseModel):
    image: str

@app.post('/star_detection')
async def star_predictions(request: ImageRequest):
    try:
        img_from_cs = string_to_image(image)
        numpy_image = np.array(img_from_cs)
        openCvImage = cv2.cvtColor(numpyImage, cv2.COLOR_RGB2BGR)
        energy_consumption, volume, star_rating = process_image(img_np)
        response_data = {
            'energy_consumption': energy_consumption,
            'volume': volume,
            'star_rating': star_rating
        }
        print("response_data : ",response_data)
        return response_data
    except Exception as e:
        print(traceback.print_exc())
        return {'error': str(e)}
"""

if __name__ == "__main__":
    uvicorn.run(app, host="127.0.0.1", port=5008)