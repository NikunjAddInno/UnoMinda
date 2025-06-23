#pragma once
#include <opencv2/opencv.hpp>
#include <vector>


using namespace cv;
using namespace std;

#define ai_color_red cv::Scalar(0,0,255);
#define ai_color_gree cv::Scalar(0,255,0);
#define ai_color_blue cv::Scalar(255,0,0);
#define ai_color_orange cv::Scalar(0,100,100);
#define ai_color_white cv::Scalar(255,255,255);
#define ai_color_black cv::Scalar(0,0,0);


float ai_distancePoint(Point p, Point q)
{
	Point diff = p - q;
	return cv::sqrt(diff.x * diff.x + diff.y * diff.y);
}

Point2f ai_getMidPoint(Point2f p1, Point2f p2)
{
	return Point2f((p1.x + p2.x) / 2.0, (p1.y + p2.y) / 2.0);
}
void ai_getColor_and_grayImages(Mat imageIn, Mat& mgray, Mat& mcolor)
{
	if (imageIn.channels() > 1)
	{
		mcolor = imageIn.clone();
		cvtColor(imageIn, mgray, COLOR_BGR2GRAY);
	}
	else
	{
		mgray = imageIn.clone();
		cvtColor(imageIn, mcolor, COLOR_GRAY2BGR);
	}
}
void ai_imageDetails(Mat image, string title)
{
	if (image.size().width > 0)
	{
		cout << "____Details::" << title << "_____" << endl;
		cout << "Size::" << image.size();
		cout << "   Channels::" << image.channels() << endl;
	}
}

bool compareByXcoord(vector<Point>& a, vector<Point>& b)
{
	RotatedRect ra = minAreaRect(a);
	RotatedRect rb = minAreaRect(b);
	return ra.center.x < rb.center.x;
}
vector <vector<Point>>  ai_sortConts_byXcoord(vector <vector<Point>> contIn)
{
	std::sort(contIn.begin(), contIn.end(), compareByXcoord);
	return contIn;
}
bool compareByYcoord(vector<Point>& a, vector<Point>& b)
{
	RotatedRect ra = minAreaRect(a);
	RotatedRect rb = minAreaRect(b);
	return ra.center.y < rb.center.y;
}

vector <vector<Point>>  ai_sortConts_byYcoord(vector <vector<Point>> contIn)
{
	std::sort(contIn.begin(), contIn.end(), compareByYcoord);
	return contIn;
}

bool compareContByArea(vector<Point>& a, vector<Point>& b)
{

	return contourArea(a) > contourArea(b); //greater first
}
vector <vector<Point>>  ai_sortContByarea(vector <vector<Point>>& contIn)
{
	std::sort(contIn.begin(), contIn.end(), compareContByArea);
	return contIn;
}

void ai_Morph(const cv::Mat& src, cv::Mat& dst, int operation, int kernel_type, int size, int iterations = 1)
{
	cv::Point anchor = cv::Point(size, size);
	cv::Mat element = getStructuringElement(kernel_type, cv::Size(2 * size + 1, 2 * size + 1), anchor);
	morphologyEx(src, dst, operation, element, anchor, iterations);
}
void ai_resShow(string name, Mat img, float scale = 1)
{
	Mat res;
	//scale = 0.2;
	resize(img, res, Size(), scale, scale);
	imshow(name, res);
	waitKey(5);
}

vector < vector<Point>> ai_findContsAll(Mat image, int retrivalMethod, int thresholdType = 0, int thresh_val = 70)
{
	Mat gray;
	if (image.channels() > 1)
	{
		cvtColor(image, gray, COLOR_BGR2GRAY);

	}
	else
	{
		gray = image.clone();
	}
	vector< vector<Point> > contours, all_contours;
	vector<Vec4i> hierarchy;
	threshold(gray, gray, thresh_val, 255, thresholdType);
	findContours(gray, all_contours, hierarchy, retrivalMethod, CHAIN_APPROX_SIMPLE);

	return all_contours;
}
int ai_findContsAll_wo_thresh_GKD(Mat imageGray, vector<vector<Point>>& contours, int retrivalMethod)
{

	vector<Vec4i> hierarchy;
	//threshold(gray, gray, thresh_val, 255, thresholdType);
	findContours(imageGray.clone(), contours, hierarchy, retrivalMethod, CHAIN_APPROX_NONE);

	return contours.size();
}
vector < vector<Point>> ai_findContsAll_wo_thresh(Mat image, int retrivalMethod)
{
	Mat gray;
	if (image.channels() > 1)
	{
		cvtColor(image, gray, COLOR_BGR2GRAY);

	}
	else
	{
		gray = image.clone();
	}
	vector< vector<Point> > contours, all_contours;
	vector<Vec4i> hierarchy;
	//threshold(gray, gray, thresh_val, 255, thresholdType);
	findContours(gray, all_contours, hierarchy, retrivalMethod, CHAIN_APPROX_NONE);

	return all_contours;
}

//returns color image with contours drawn
Mat ai_drawContours(vector<vector<Point>>& contours, Mat drawImage, Scalar color = Scalar(0, 255, 0), Point offset = Point(0, 0))
{
	//Mat colorImage;
	//if (drawImage.channels() > 1)
	//{
	//	colorImage = drawImage.clone();
	//}
	//else
	//{
	//	cvtColor(drawImage, colorImage, COLOR_GRAY2BGR);
	//}
	//vector<Vec4i> hierarchy;
	drawContours(drawImage, contours, -1, color, 1);// , 8, hierarchy, 100000000, offset);
	//Mat m;
	//drawContours(drawImage, contours, -1, Scalar(0, 255, 0), 2,8, m,100000000,offset);
	return drawImage;
}

vector < vector<Point>> ai_filterConts_bySize(vector<vector<Point>>& contours, float minDim_l, float minDim_h, float maxDim_l, float maxDim_h, float minArea = 1)
{
	vector < vector<Point>> filteredConts;
	RotatedRect rr;
	double contMaxD, contMinD;
	for (int i = 0; i < contours.size(); ++i)
	{
		if (contourArea(contours[i]) > minArea)
		{
			rr = minAreaRect(contours[i]);

			contMaxD = max(rr.size.width, rr.size.height);      //width
			contMinD = min(rr.size.width, rr.size.height);
			//cout << "cont MaxDIm: " << contMaxD << endl;
			//cout << "cont minDim: " << contMinD << endl;
			if (contMaxD > maxDim_l&& contMaxD <= maxDim_h && contMinD > minDim_l&& contMinD <= minDim_h)
			{
				filteredConts.push_back(contours[i]);
			}
		}
	}
	return filteredConts;
}

int ai_getMaxAreaContourId(vector <vector<cv::Point>>& contours) {
	double maxArea = 0;
	int maxAreaContourId = -1;
	for (int j = 0; j < contours.size(); j++) {
		double newArea = cv::contourArea(contours.at(j));
		if (newArea > maxArea) {
			maxArea = newArea;
			maxAreaContourId = j;
		}
	}
	return maxAreaContourId;
}

Mat ai_getCircularmask(Size maskSize, Point center, int maskId, int maskOd)
{
	Mat mask = Mat::zeros(maskSize, CV_8UC1);
	circle(mask, center, (float)maskOd / 2.0, 255, -1);
	circle(mask, center, (float)maskId / 2.0, 0, -1);
	return mask;
}

Mat ai_getSobel(Mat inMat, int derivative, int bin_threshold)
{
	Mat sobelOp;
	Mat grad_x, grad_y;
	Mat abs_grad_x, abs_grad_y;
	int ddepth = CV_16S;
	int ksize = 3;
	/// Gradient X
	Sobel(inMat, grad_x, ddepth, derivative, 0, ksize);

	Sobel(inMat, grad_y, ddepth, 0, derivative, ksize);

	convertScaleAbs(grad_x, abs_grad_x);
	convertScaleAbs(grad_y, abs_grad_y);
	//![convert]

	//![blend]
	/// Total Gradient (approximate)
	addWeighted(abs_grad_x, 0.5, abs_grad_y, 0.5, 0, sobelOp);
	//![blend]
	imshow("sobel OPP", sobelOp);
	//![display]
	cv::threshold(sobelOp, sobelOp, bin_threshold, 255, THRESH_BINARY_INV);
	//imshow("sobel OPP", sobelOp);
	//waitKey(0);
	return sobelOp;
}
Mat ai_getSobel_raw(Mat inMat, int derivative)
{
	Mat sobelOp;
	Mat grad_x, grad_y;
	Mat abs_grad_x, abs_grad_y;
	int ddepth = CV_16S;
	int ksize = 3;
	/// Gradient X
	Sobel(inMat, grad_x, ddepth, derivative, 0, ksize);

	Sobel(inMat, grad_y, ddepth, 0, derivative, ksize);

	convertScaleAbs(grad_x, abs_grad_x);
	convertScaleAbs(grad_y, abs_grad_y);
	//![convert]

	//![blend]
	/// Total Gradient (approximate)
	addWeighted(abs_grad_x, 0.5, abs_grad_y, 0.5, 0, sobelOp);
	//![blend]
	//imshow("sobel OPP", sobelOp);
	//![display]
	//cv::threshold(sobelOp, sobelOp, bin_threshold, 255, THRESH_BINARY_INV);
	//imshow("sobel OPP", sobelOp);
	//waitKey(0);
	return sobelOp;
}

Mat ai_thresholdHSV_circularH(Mat imageIn, int  low_H, int high_H, int low_S, int high_S, int low_V, int high_V)
{
	Mat frame_HSV;
	Mat frame_threshold;
	cvtColor(imageIn, frame_HSV, COLOR_BGR2HSV);
	// Detect the object based on HSV Range Values
	Mat th1;
	Mat th2;
	// 0-12    140-180
	//s  0 120
	//v 21  91
	//cout << "HSV L " << low_H << "  " << low_S << "  " << low_V << endl;
	//cout << "HSV H " << high_H << "  " << high_S << "  " << high_V << endl;

	if (low_H < high_H)
	{
		inRange(frame_HSV, Scalar(low_H, low_S, low_V), Scalar(high_H, high_S, high_V), th1);
		//inRange(frame_HSV, Scalar(high_H, low_S, low_V), Scalar(255, high_S, high_V), th2);
		th2 = th1.clone();
	}
	else
	{
		inRange(frame_HSV, Scalar(0, low_S, low_V), Scalar(high_H, high_S, high_V), th1);
		inRange(frame_HSV, Scalar(low_H, low_S, low_V), Scalar(255, high_S, high_V), th2);
	}
	//ai_resShow("maskedTH1", th1, 0.4);
	//ai_resShow("maskedTH2", th2, 0.4);
	bitwise_or(th1, th2, frame_threshold);
	return frame_threshold;
}

Point ai_get_rectCenter(Rect r)
{
	return Point(r.x + (r.width / 2), r.y + (r.height / 2));
}
Point2f ai_get_rectCenterFloat(Rect r)
{
	return Point2f((float)r.x + ((float)r.width / 2.0), (float)r.y + ((float)r.height / 2.0));
}
Rect ai_getRect_from_cent_size(Point2f center, Size sz)
{
	return  Rect(center.x - sz.width / 2, center.y - sz.height / 2, sz.width, sz.height);
}
//validates if rectangle is inside the image to avoid crop exceptions
Rect ai_validate_andModify_RectBounds(Rect inputRect, int imageRows, int immageCols)
{
	if (inputRect.x < 0)
		inputRect.x = 0;
	if (inputRect.y < 0)
		inputRect.y = 0;
	if (inputRect.height <= 0)
		inputRect.height = 10;
	if ((inputRect.y + inputRect.height) > imageRows)
	{
		inputRect.height = imageRows - inputRect.y - 2;
	}
	if ((inputRect.width + inputRect.x) > immageCols)
	{
		inputRect.width = (immageCols - inputRect.x - 1);
	}
	return inputRect;
}

bool ai_validate_rectArea_in_Image(Rect inputRect, Size imageSz)
{

	if ((inputRect.x < 0) || (inputRect.y < 0) || (inputRect.height <= 0) || (inputRect.width <= 0) || ((inputRect.y + inputRect.height) > imageSz.height) || ((inputRect.width + inputRect.x) > imageSz.width))
		return false;
	else
		return true;

}
bool ai_check_pt_lies_inRect(Point pt, Rect r)
{
	if (pt.x > r.x&& pt.x<(r.x + r.width) && pt.y>r.y&& pt.y < (r.y + r.height))
	{
		return true;
	}
	return false;

}
void drawTextWithBackGround(Mat image, std::string text, Point location, Scalar fontColor = Scalar(255, 255, 255), Scalar backGrColor = Scalar(0, 0, 0), int fontSize = 1, int thickness = 4)
{
	Rect drawRect = Rect(location.x - 5, location.y - (fontSize * 15), text.length() * fontSize * 11, (fontSize * 20));
	rectangle(image, drawRect, backGrColor, -1);

	putText(image, text, location, FONT_HERSHEY_PLAIN, fontSize, fontColor, thickness);


}
void drawText(Mat image, std::string text, Point location, Scalar fontColor = Scalar(255, 255, 255), int fontSize = 1, int thickness = 4)
{
	Rect drawRect = Rect(location.x - 5, location.y - (fontSize * 15), text.length() * fontSize * 11, (fontSize * 20));
	//rectangle(image, drawRect, backGrColor, -1);
	putText(image, text, location, FONT_HERSHEY_PLAIN, fontSize, fontColor, thickness);
}


Point2f getCenterMoments(vector<Point2f> pts)
{
	Moments mu = moments(pts);
	return Point2f(static_cast<float>(mu.m10 / (mu.m00 + 1e-5)), static_cast<float>(mu.m01 / (mu.m00 + 1e-5)));

}

//get point at dist arithmatic
int ai_ptToVector(Point2i p, Point2i q, Point2f(&returnVec)[2])
{
	//returnVec[2]----[0] element contains direction [1] contains point
	Point2f diff = p - q;
	float magV = sqrt(diff.x * diff.x + diff.y * diff.y);
	Mat img(640, 480, CV_8UC3, Scalar(0, 0, 0));
	Point2f U = Point2f(diff.x / magV, diff.y / magV);

	returnVec[0] = Point2f(U.x, U.y);
	returnVec[1] = Point2f(p.x, p.y);
	return 1;
}
Point2f  ai_getPointAtDist(Point2f returnVec[2], int distance, int position) {
	//returnVec[2]----[0] element contains direction [1] contains point
	Point2f returnPoint;
	Point2f swappedVec;
	swappedVec.x = returnVec[0].y;
	swappedVec.y = returnVec[0].x;
	if (position == 1) //opposite direction
	{
		returnPoint = returnVec[1] - distance * returnVec[0];
	}
	else if (position == 2) //90 CW invert Y
	{
		returnPoint = returnVec[1] + distance * Point2f(swappedVec.x, -1 * swappedVec.y);
	}
	else if (position == 3) // 90  CCW invert x
	{
		returnPoint = returnVec[1] + distance * Point2f(-1 * swappedVec.x, swappedVec.y);
	}
	else //same direction
	{
		returnPoint = returnVec[1] + distance * returnVec[0];
	}
	return returnPoint;
}

//dir: 0;same, 1 : opp , 2: 90CW 3: 270 CW
Point ai_getPointAtDistAndPosition(Point2f pt_st, Point2f pt_end, int direction, int distancePix)
{
	Point2f returnVecl1[2];
	ai_ptToVector(pt_st, pt_end, returnVecl1);
	return ai_getPointAtDist(returnVecl1, distancePix, direction);

}

float ai_getLineAngle(Point p1, Point p2)
{
	// Compute the direction in radians and convert to degrees
//	float directionRAD = std::atan2(p2.y-p1.y, p2.x-p1.x);
	float directionDEG = std::atan2(p2.y - p1.y, p2.x - p1.x) * (180.0 / CV_PI);

	// If the direction is negative, make it positive
	if (directionDEG < 0)
		directionDEG += 360;
	return directionDEG;
}


