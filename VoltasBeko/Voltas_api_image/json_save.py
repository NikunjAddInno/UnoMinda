##import requests
##import base64
##import json
##import io
##from PIL import Image
##import cv2
##import numpy as np
##import matplotlib.pyplot as plt
##
##with open('test_img.bmp', 'rb') as image_file:
##    encoded_string = base64.b64encode(image_file.read()).decode('utf-8')
##
###API endpoint
##url = 'http://127.0.0.1:5000/predict_cover'
##payload = {
##    'image': encoded_string,
##    'thr': 100
##}
##
##with open('payload.json', 'w') as json_file:
##    json.dump(payload, json_file)
##
##response = requests.post(url, json=payload)
##response_data = json.loads(response.text)
##defect_image_base64 = response_data['defImage']
##defect_list = response_data['serialized_Defects']
##
##
##def base64ToImage(base64_string):
##    imgdata = base64.b64decode(base64_string)
##    return Image.open(io.BytesIO(imgdata))
##
##defect_image = base64ToImage(defect_image_base64)
##defect_image_np = np.array(defect_image)
##plt.imshow(cv2.cvtColor(defect_image_np, cv2.COLOR_BGR2RGB))
##plt.show()
##print("Defect List:", defect_list)
##
### Shutdown server
##
##shutdown_url = 'http://127.0.0.1:5000/shutdown' 
##shutdown_response = requests.post(shutdown_url)
##print(shutdown_response.text)


import requests
import base64
import json
import io
from PIL import Image
import cv2
import numpy as np
import matplotlib.pyplot as plt

with open('img1.bmp', 'rb') as image_file:
    encoded_string = base64.b64encode(image_file.read()).decode('utf-8')

# API endpoint
url = 'http://127.0.0.1:5004/back_Detection'
shutdown_url = 'http://127.0.0.1:5000/shutdown'  # New shutdown endpoint
payload = {
    'image': encoded_string,    
}
#'thr': 100
with open('payload.json', 'w') as json_file:
    json.dump(payload, json_file)

try:
    response = requests.post(url, json=payload)
    response.raise_for_status()  # Raise an error for bad responses (4xx or 5xx)
    
    response_data = json.loads(response.text)
    defect_image_base64 = response_data['image']
    #defect_list = response_data['serialized_Defects']

    def base64ToImage(base64_string):
        imgdata = base64.b64decode(base64_string)
        return Image.open(io.BytesIO(imgdata))

    defect_image = base64ToImage(defect_image_base64)
    defect_image_np = np.array(defect_image)
    plt.imshow(cv2.cvtColor(defect_image_np, cv2.COLOR_BGR2RGB))
    plt.show()
    #print("Defect List:", defect_list)

    # Shutdown server
    shutdown_response = requests.post(shutdown_url)
    shutdown_response.raise_for_status()  # Raise an error for bad responses (4xx or 5xx)
    print(shutdown_response.text)

except requests.exceptions.RequestException as e:
    #print(f"Error in request: {e}")
    print("execpt")
    pass
    
