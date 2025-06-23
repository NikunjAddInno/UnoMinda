import cv2
import numpy as np
import matplotlib.pyplot as plt
import math
import datetime
import os
def mylogger(text):
    try:
        with open("logger.txt","a") as logfile:
            logfile.write(text + "\n")
    except:
        pass

def hsv_color_tracker_old(img, lower_hsv, upper_hsv):
    # Read the image
    #img = cv2.imread(image_path)
    hsv_img = cv2.cvtColor(img, cv2.COLOR_BGR2HSV)
    
    #Define the lower and upper thresholds for the specific color
    lower_color = np.array(lower_hsv)
    upper_color = np.array(upper_hsv)
    #Create a mask
    mask = cv2.inRange(hsv_img, lower_color, upper_color)
    # Apply the mask
    tracked_img = cv2.bitwise_and(img, img, mask=mask)
    
    # Convert BGR to RGB for plotting
    img_rgb = cv2.cvtColor(tracked_img, cv2.COLOR_BGR2RGB)
    """
    # Plot the tracked color
    plt.imshow(img_rgb)
    plt.axis('off')
    plt.show()
    """

    # Return the plotted image as ROI
    return img_rgb

def hsv_color_tracker(img):# lower_hsv, upper_hsv
    # Read the image
    #img = cv2.imread(image_path)
    hsv_img = cv2.cvtColor(img, cv2.COLOR_BGR2HSV)
    # Enhance contrast using histogram equalization
    hsv_img[..., 2] = cv2.equalizeHist(hsv_img[..., 2])
    
    #Define the lower and upper thresholds for the specific color
    lower_colorL0 = np.array([0, 30, 30])
    upper_colorL0 = np.array([15, 255, 255])
    #Create a mask
    maskL0 = cv2.inRange(hsv_img, lower_colorL0, upper_colorL0)

    lower_colorU0 = np.array([170, 30, 30])
    upper_colorU0 = np.array([180, 255, 255])
    #Create a mask
    maskU0 = cv2.inRange(hsv_img, lower_colorU0, upper_colorU0)
    mask0=maskL0 | maskU0

    ########
    # Define the range for red color in HSV
    lower_redL1 = np.array([0, 30, 50])
    upper_redL1 = np.array([20, 255, 255])#10
    maskL1 = cv2.inRange(hsv_img, lower_redL1, upper_redL1)
    
    lower_redU1 = np.array([160, 30, 50])
    upper_redU1 = np.array([180, 255, 255])
    maskU1 = cv2.inRange(hsv_img, lower_redU1, upper_redU1)
    mask1=maskL1 | maskU1
    
    #########
    # Define the range for red color in HSV
    lower_redL2 = np.array([0, 50, 50])
    upper_redL2 = np.array([15, 255, 255])#10
    maskL2 = cv2.inRange(hsv_img, lower_redL2, upper_redL2)
    
    lower_redU2 = np.array([160, 50, 50])
    upper_redU2 = np.array([180, 255, 255])
    maskU2 = cv2.inRange(hsv_img, lower_redU2, upper_redU2)
    mask2=maskL2 | maskU2

    ########
    # Define the range for red color in HSV
    lower_redL3 = np.array([0, 120, 70])
    upper_redL3 = np.array([15, 255, 255])#10
    maskL3 = cv2.inRange(hsv_img, lower_redL3, upper_redL3)
    
    lower_redU3 = np.array([160, 120, 70])
    upper_redU3 = np.array([180, 255, 255])
    maskU3 = cv2.inRange(hsv_img, lower_redU3, upper_redU3)
    mask3=maskL3 | maskU3
    
    # Combine the masks
    mask = mask0 | mask1 | mask2 | mask3
    # Apply the mask
    tracked_img = cv2.bitwise_and(img, img, mask=mask)
    
    # Convert BGR to RGB for plotting
    img_rgb = cv2.cvtColor(tracked_img, cv2.COLOR_BGR2RGB)
    """
    # Plot the tracked color
    plt.imshow(img_rgb)
    plt.axis('off')
    plt.show()
    """

    # Return the plotted image as ROI
    return img_rgb

def find_corner_coordinates(roi_image):
    # Get the dimensions of the ROI image
    height, width, _ = roi_image.shape 
    # Calculate coordinates
    bottom_left = (0, height)
    top_right = (width, 0)
    bottom_right = (width, height)

    return bottom_left, top_right, bottom_right

def draw_angle_lines(roi_image, midpoint):
    # Get ROI dimensions
    height, width, _ = roi_image.shape
    
    # Calculate angles in radians
    #angles = [5, 35, 70, 98, 127, 175]
    angles = [3, 40, 75, 100, 130, 177]
    end_points = []
    for angle_deg in angles:
        # Convert angle to radians
        angle_rad = math.radians(angle_deg)
        
        # Calculate endpoint coordinates
        dx = int(width * math.cos(angle_rad))
        dy = int(height * math.sin(angle_rad))
        endpoint = (midpoint[0] + dx, midpoint[1] - dy)
        
        # Draw line from midpoint to endpoint
        #cv2.line(roi_image, midpoint, endpoint, (0, 255, 0), thickness=2)
        
        # Store endpoint coordinates
        end_points.append(endpoint)
    
    return roi_image, end_points
#adjust based on use case(high>5000 if red fridge)
# Function to check for RGB pixel values in a given triangle
def check_rgb_in_triangle(triangle_image):
    r_values = []
    # Loop through each pixel in the triangle image
    pixel_thr=0
    count_red=cv2.countNonZero(cv2.cvtColor(triangle_image, cv2.COLOR_BGR2GRAY))
    print("red_count : ",count_red)
    #total_poxel=triangle_image.shape[0]*triangle_image.shape[1]
    """
    for row in triangle_image:
        for pixel in row:
            # Check if pixel value is within the desired range
            if 160 <= pixel[0] <= 250:#adjust according
                #print("pixel value of tringle : ",pixel[0])
                r_values.append(pixel[0])
    # If any pixel with desired value is found, return True
    print("len(r_values) : ",len(r_values))
    """
    mylogger(str(count_red))
    if count_red > 5000: #adjust based on use case(high>5000 if red fridge)
        return True
    else:
        return False
folder_path = r'E:\Software\VoltasBeko_Copy_kk\VoltasBeko\Voltas_api_image\RatingImage'
def ratings(image):
    #image = cv2.cvtColor(image, cv2.COLOR_RGB2BGR)
    current_time = datetime.datetime.now().strftime("%Y-%m-%d_%H-%M-%S")

    # Define the file name
    file_name = f"image_{current_time}.jpg"

    # Define the complete path
    file_path = os.path.join(folder_path, file_name)
    cv2.imwrite(file_path, image)
    star=0
    # Example usage
    #image_path = r'G:\voltas\module\test.bmp'
    # Define the lower and upper HSV thresholds for the specific color (Hue, Saturation, Value)
    roi_image = hsv_color_tracker(image)#

    # Find corner coordinates
    bottom_left, _, bottom_right = find_corner_coordinates(roi_image)

    # Draw a blue line between bottom left and bottom right coordinates
    #cv2.line(roi_image, bottom_left, bottom_right, (0, 0, 255), thickness=4)

    # Calculate midpoint
    midpoint = ((bottom_left[0] + bottom_right[0]) // 2, (bottom_left[1] + bottom_right[1]) // 2)

    # Mark the midpoint with a green circle
    #cv2.circle(roi_image, midpoint, radius=5, color=(0, 255, 0), thickness=-1)

    # Draw lines at specified angles from the midpoint to the border of ROI
    roi_image_with_angles, end_points = draw_angle_lines(roi_image, midpoint)

    # Draw green lines to join the endpoints
    lines_to_draw = [(0, 1), (1, 2), (2, 3), (3, 4), (4, 5)]  # Line indices to join endpoints
    for line in lines_to_draw:
        start_point = end_points[line[0]]
        end_point = end_points[line[1]]
        #cv2.line(roi_image_with_angles, start_point, end_point, (0, 255, 0), thickness=2)

    # Crop triangles and plot them
    triangle_images = []
    triangle_names = ['1', '2', '3', '4', '5']
    for idx, line in enumerate(lines_to_draw):
        start_point = end_points[line[0]]
        end_point = end_points[line[1]]
        x_vals = [start_point[0], midpoint[0], end_point[0]]
        y_vals = [start_point[1], midpoint[1], end_point[1]]
        triangle_mask = np.zeros_like(roi_image_with_angles)
        triangle_mask = cv2.fillPoly(triangle_mask, [np.array(list(zip(x_vals, y_vals)))], (255, 255, 255))
        triangle_image = cv2.bitwise_and(roi_image_with_angles, triangle_mask)
        triangle_images.append(triangle_image)
    
    # Plot cropped triangles
    #fig, axes = plt.subplots(1, len(triangle_images), figsize=(15, 5))
    for idx, (triangle_image, triangle_name) in enumerate(zip(triangle_images, triangle_names)):
        #axes[idx].imshow(triangle_image)
        #axes[idx].axis('off')
        #axes[idx].set_title(f'Triangle {triangle_name}')
        # Check for RGB values of r = 200 to 220 in each triangle
        cv2.imwrite(f"{idx}_triangle_image.jpg",triangle_image)
        if check_rgb_in_triangle(triangle_image):
            
            star+=1
            print(f"In triangle {triangle_name}, RGB values of r = 200 to 220 found.")

    #plt.show()
    return star


### Function to annotate the image
##def annotate_image(image, text, position=(50, 50), font_scale=1, color=(255, 0, 0), thickness=2):
##    annotated_image = image.copy()
##    cv2.putText(annotated_image, text, position, cv2.FONT_HERSHEY_SIMPLEX, font_scale, color, thickness)
##    return annotated_image
##
### Folder containing images
##folder_path = r'G:\voltas\RatingImage'
##
### Loop through each image in the folder
##for filename in os.listdir(folder_path):
##    if filename.endswith('.jpg') or filename.endswith('.bmp'):
##        # Read the image
##        image_path = os.path.join(folder_path, filename)
##        #print("image_path : ",image_path)
##        image = cv2.imread(image_path)
##
##        # Process the image and get the value
##        value = ratings(image)
##
##        # Annotate the image
##        annotated_image = annotate_image(image, str(value))
##
##        # Convert BGR image (OpenCV format) to RGB (matplotlib format)
##        annotated_image_rgb = cv2.cvtColor(annotated_image, cv2.COLOR_BGR2RGB)
##        folder_pt=r"G:\voltas\result"
##        res_path = os.path.join(folder_pt, filename)
##        cv2.imwrite(res_path,annotated_image_rgb)
##        # Display the image
##        #plt.imshow(annotated_image_rgb)
##        #plt.title(filename)
##        #plt.axis('off')
##        #plt.show()
