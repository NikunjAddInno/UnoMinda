#pragma once
#pragma once
#include <opencv2/imgcodecs.hpp>
#include <opencv2/core/core.hpp>
#include <opencv2/highgui/highgui.hpp>
#include <opencv2/imgproc/imgproc.hpp>
#include <opencv2/opencv.hpp>
#include <iostream>
#include <stdio.h>
#include <string.h>
#include <vector>
#include <fstream>
#include <iterator>
#include <time.h>
//#include <Math.h>
#define pdd pair<double, double>
using namespace std;
using namespace cv;

Scalar color_blue = Scalar(255, 0, 0);
Scalar color_green = Scalar(0, 255, 0);
Scalar color_red = Scalar(0, 0, 255);
Scalar color_white = Scalar(255, 255, 255);
Scalar color_black = Scalar(0, 0, 0);
Point transformPoint_rot(Point pt, Mat rot_mat)
{
	cv::Point result;
	//Point pt = pt0 + shiftV;
	result.x = rot_mat.at<double>(0, 0) * pt.x + rot_mat.at<double>(0, 1) * pt.y + rot_mat.at<double>(0, 2);
	result.y = rot_mat.at<double>(1, 0) * pt.x + rot_mat.at<double>(1, 1) * pt.y + rot_mat.at<double>(1, 2);
	//result = result + shiftV;
	return result;
}

Point transformPoint_shift_rot(Point pt0, Mat rot_mat, Point shiftV)
{
	cv::Point result;
	Point pt = pt0 + shiftV;
	result.x = rot_mat.at<double>(0, 0) * pt.x + rot_mat.at<double>(0, 1) * pt.y + rot_mat.at<double>(0, 2);
	result.y = rot_mat.at<double>(1, 0) * pt.x + rot_mat.at<double>(1, 1) * pt.y + rot_mat.at<double>(1, 2);
	//result = result + shiftV;
	return result;
}
Point2f transformPoint_shift_rot(Point2f pt0, Mat rot_mat, Point2f shiftV)
{
	cv::Point2f result;
	Point2f pt = pt0 + shiftV;
	result.x = rot_mat.at<double>(0, 0) * pt.x + rot_mat.at<double>(0, 1) * pt.y + rot_mat.at<double>(0, 2);
	result.y = rot_mat.at<double>(1, 0) * pt.x + rot_mat.at<double>(1, 1) * pt.y + rot_mat.at<double>(1, 2);
	//result = result + shiftV;
	return result;
}
Point transformPoint_shift_rot_Point(Point pt, Mat rot_mat, Point shiftV)
{
		pt = pt + shiftV;
		cv::Point result;
		result.x = rot_mat.at<double>(0, 0) * pt.x + rot_mat.at<double>(0, 1) * pt.y + rot_mat.at<double>(0, 2);
		result.y = rot_mat.at<double>(1, 0) * pt.x + rot_mat.at<double>(1, 1) * pt.y + rot_mat.at<double>(1, 2);
		//result = result + shiftV;
		return result;
}
Point2f transformPoint_shift_rot_PointFloat(Point2f pt, Mat rot_mat, Point2f shiftV)
{
	pt = pt + shiftV;
	cv::Point2f result;
	result.x = rot_mat.at<double>(0, 0) * pt.x + rot_mat.at<double>(0, 1) * pt.y + rot_mat.at<double>(0, 2);
	result.y = rot_mat.at<double>(1, 0) * pt.x + rot_mat.at<double>(1, 1) * pt.y + rot_mat.at<double>(1, 2);
	//result = result + shiftV;
	return result;
}
Point transformPoint_shift_rot(vector <Point>& vec_pt, Mat rot_mat, Point shiftV)
{
	for (int i = 0; i < vec_pt.size(); i++)
	{
		Point pt = vec_pt[i];
		pt = pt + shiftV;
		cv::Point result;
		result.x = rot_mat.at<double>(0, 0) * pt.x + rot_mat.at<double>(0, 1) * pt.y + rot_mat.at<double>(0, 2);
		result.y = rot_mat.at<double>(1, 0) * pt.x + rot_mat.at<double>(1, 1) * pt.y + rot_mat.at<double>(1, 2);
		//result = result + shiftV;
		vec_pt[i] = result;
	}
	return Point(0, 0);
}
vector<Point> transformPtVector_shift_rot(vector <Point>& vec_pt, Mat rot_mat, Point shiftV)
{
	vector<Point> returnVec(vec_pt.size());
	for (int i = 0; i < vec_pt.size(); i++)
	{
		Point pt = vec_pt[i];
		pt = pt + shiftV;
		cv::Point result;
		result.x = rot_mat.at<double>(0, 0) * pt.x + rot_mat.at<double>(0, 1) * pt.y + rot_mat.at<double>(0, 2);
		result.y = rot_mat.at<double>(1, 0) * pt.x + rot_mat.at<double>(1, 1) * pt.y + rot_mat.at<double>(1, 2);
		//result = result + shiftV;
		returnVec[i] = result;
	}
	return returnVec;
}

Mat ai_cropRotatedRectFromImage(vector<Point> rrPts, Rect destRect, Mat image)
{
	vector<Point> rectPts = { Point(0,0),Point(destRect.width,0), Point(destRect.width,destRect.height),Point(0, destRect.height) };
	Mat h = findHomography(rrPts, rectPts, RANSAC);
	Mat cropImg = Mat::zeros(destRect.size(), image.type());
	// Use homography to warp image
	warpPerspective(image, cropImg, h, cropImg.size());
	return cropImg;
}