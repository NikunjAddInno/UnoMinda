// This is the main DLL file.

#include "stdafx.h"

#include "algorithmLib.h"
#include <vector>
#include <math.h>
#include "AI_OCVLib.h"
using namespace System;
using namespace cv;
using namespace std;
//using namespace toolDef;
//2413,1028

string path;


//fn defs
Mat BitmapToMat(System::Drawing::Bitmap^ bitmap);

//--------json load
float mmPPcathater = 0.0075471698113208;
enum toolTypeCpp { Arc, Width, Circle, Thread, Angle,Locate ,Match, Inner_Hex, Outer_Hex};
string arr_enumNames[] = { "Arc", "Width", "Circle", "Thread", "Angle","Locate","Match", "Inner_Hex", "Outer_Hex"};

struct rectData
{
	Rect r;
	int threshold;

};

Mat img_template;
Mat img_templateCtop;

toolCpp_cat::modelData_Cat inst_catModel[2];// = toolCpp_cat::modelData_Cat();
vector <rectData> vec_cam1Rects;
vector <rectData> vec_cam2Rects;
vector<toolCpp_cat::ListInspection> list_inspections0;
vector<toolCpp_cat::ListInspection> list_inspections1;
//c1 thresh
const int Cathater_Base_Dia = 0;
const int Bevel_Width = 1;
const int Bevel_length = 2;
const int Tip_Angle = 3;
const int Tip_Blunt_Limit = 4;
const int Tip_to_Cathater_distance = 5;
const int Cathater_Burr_Tolerance = 6;
const int Bevel_Threshold = 7;
const int Burr_Threshold = 8;





static const char* string_to_char_array(System::String^ string)
{
	const char* str = (const char*)(Marshal::StringToHGlobalAnsi(string)).ToPointer();
	return str;
}
std::vector<toolCpp_cat::ListInsRoi > vec_insROI;
System::String^ __clrcall algorithmLib::Class1::load_template_cath(System::String^ json_model_data, int camIdx)
{
	cout << "printing model data from c++++" << endl;
	using System::Runtime::InteropServices::Marshal;
	System::IntPtr pointer_for_model_name = Marshal::StringToHGlobalAnsi(json_model_data);
	char* charPointer_json = reinterpret_cast<char*> (pointer_for_model_name.ToPointer());
	std::string json_model_data_std_string(charPointer_json, json_model_data->Length);
	//toolDef:: _modelData md = toolDef:: _modelData();
	//obj_modelData = json::parse(json_model_data_std_string);

	try
	{
		inst_catModel[camIdx] = json::parse(json_model_data_std_string);
	}
	catch (std::exception & ex)
	{
		std::cout << "	 ex.what(); " << ex.what() << std::endl;
	}
	vec_insROI = inst_catModel[camIdx].get_list_ins_roi();
	if (camIdx == 0)
	{
	list_inspections0 = inst_catModel[camIdx].get_list_inspections();
	}
	if (camIdx == 1)
	{
	list_inspections1 = inst_catModel[camIdx].get_list_inspections();
	}
	//for (int k = 0; k < vec_insROI.size(); k++)
	//{
	//	//vec_insROI[k].get
	//}
	cout << "c+++++++++++ inspectionParCnt :" << inst_catModel[camIdx].get_list_inspections().size() << endl;
	cout << "c+++++++++++ cameraIdx :" << inst_catModel[camIdx].get_camera_idx() << endl;
	
	return "";
}
int algorithmLib::Class1::printCamIdx(int camIdx)
{
	cout << "c+++++++++++ cameraIdx from fn:" << inst_catModel[camIdx].get_camera_idx() << endl;
	//rects cam1 
	if (camIdx == 0)
	{
		for (int i = 0; i < vec_cam1Rects.size(); i++)
		{
			cout << "rect ::" << vec_cam1Rects[i].r << "   thresh::" << vec_cam1Rects[i].threshold << endl;
		}
		//vector<toolCpp_cat::ListInspection> list_inspections0 = inst_catModel[camIdx].get_list_inspections();
		for (int k= 0; k < list_inspections0.size(); k++)
		{
			cout<< list_inspections0[k].get_mutable_par_name() << "  value::"<< list_inspections0[k].get_mutable_std_value()<< endl;
		}
	}
	else if (camIdx == 1)
	{
		for (int i = 0; i < vec_cam2Rects.size(); i++)
		{
			cout << "rect ::" << vec_cam2Rects[i].r << "   thresh::" << vec_cam2Rects[i].threshold << endl;
		}
		//vector<toolCpp_cat::ListInspection> list_inspections1 = inst_catModel[camIdx].get_list_inspections();
		for (int k = 0; k < list_inspections1.size(); k++)
		{
			cout << list_inspections1[k].get_mutable_par_name() << "  value::" << list_inspections1[k].get_mutable_std_value() << endl;
		}
	}
	return inst_catModel[camIdx].get_camera_idx();
}
int algorithmLib::Class1::loadInspectionRects(int camIdx, int rect_idx, int x, int y, int width, int height, int threshold)
{
	rectData rd;
	if (camIdx == 0)
	{
		if (rect_idx == 0)
		{
			vec_cam1Rects.clear();
		}
		rd.r = Rect(x, y, width, height);
		rd.threshold = threshold;
		vec_cam1Rects.push_back(rd);
	}
	if (camIdx == 1)
	{
		if (rect_idx == 0)
		{
			vec_cam2Rects.clear();
		}
		rd.r = Rect(x, y, width, height);
		rd.threshold = threshold;
		vec_cam2Rects.push_back(rd);
	}
	return rect_idx;
}

string path_s = "";




Mat BitmapToMat(System::Drawing::Bitmap^ bitmap)
{

	System::Drawing::Rectangle blank = System::Drawing::Rectangle(0, 0, bitmap->Width, bitmap->Height);
	System::Drawing::Imaging::BitmapData^ bmpdata = bitmap->LockBits(blank, System::Drawing::Imaging::ImageLockMode::ReadWrite, bitmap->PixelFormat);
	if (bitmap->PixelFormat == System::Drawing::Imaging::PixelFormat::Format8bppIndexed)
	{
		//tmp = cvCreateImage(cvSize(bitmap->Width, bitmap->Height), IPL_DEPTH_8U, 1);
		//tmp->imageData = (char*)bmData->Scan0.ToPointer();
		cv::Mat thisimage(cv::Size(bitmap->Width, bitmap->Height), CV_8UC1, bmpdata->Scan0.ToPointer(), cv::Mat::AUTO_STEP);
		bitmap->UnlockBits(bmpdata);
		return thisimage;
	}

	else if (bitmap->PixelFormat == System::Drawing::Imaging::PixelFormat::Format24bppRgb)
	{

		cv::Mat thisimage(cv::Size(bitmap->Width, bitmap->Height), CV_8UC3, bmpdata->Scan0.ToPointer(), cv::Mat::AUTO_STEP);
		bitmap->UnlockBits(bmpdata);
		return thisimage;

	}

	Mat returnMat = (Mat::zeros(640, 480, CV_8UC1));
	//   bitmap->UnlockBits(bmData);
	return returnMat;
	//   return cvarrToMat(tmp);
}

void lineFromPoints(Point2f P, Point2f Q, double* a, double* b, double* c)
{
	*a = Q.y - P.y;
	*b = P.x - Q.x;
	*c = -1 * (*a * (P.x) + *b * (P.y));

	if (b < 0) {
		//cout << "The line passing through points P and Q "
		//	"is: "
		//	<< a << "x - " << b << "y = " << c << endl;
		*b = -1 * (*b);
	}
	//else {
		//cout << "The line passing through points P and Q "
		//	"is: "
		//	<< a << "x + " << b << "y = " << c << endl;
	//}
}
float getAngle(Point2f l1s, Point2f l1e, Point2f l2s, Point2f l2e)
{
	float ang1 = atan2(l1s.y - l1e.y, l1s.x - l1e.x);
	float ang2 = atan2(l2s.y - l2e.y, l2s.x - l2e.x);
	float ang = (ang2 - ang1) * 180 / (3.14);
	if (ang < 0)
	{
		ang += 360;
	}
	//if (ang > 180) {
	//	ang = 360 - ang;
	//}
	return ang;
}

void getLine(double x1, double y1, double x2, double y2, double& a, double& b, double& c)
{
	// (x- p1X) / (p2X - p1X) = (y - p1Y) / (p2Y - p1Y) 
	a = y1 - y2; // Note: this was incorrectly "y2 - y1" in the original answer
	b = x2 - x1;
	c = x1 * y2 - x2 * y1;
}
double dist(double pct1X, double pct1Y, double pct2X, double pct2Y, double pct3X, double pct3Y)
{
	double a, b, c;
	getLine(pct2X, pct2Y, pct3X, pct3Y, a, b, c);
	return abs(a * pct1X + b * pct1Y + c) / sqrt(a * a + b * b);
}

Rect validateRectBounds(Rect tipRect, int rows, int cols)
{
	if (tipRect.x < 0)
		tipRect.x = 0;
	if (tipRect.y < 0)
		tipRect.y = 0;
	if (tipRect.height <= 0)
		tipRect.height = 10;
	if ((tipRect.y + tipRect.height) > rows)
	{
		tipRect.height = rows - tipRect.y - 2;
	}
	if ((tipRect.width + tipRect.x) > cols)
	{
		tipRect.width = (cols - tipRect.x - 1);
	}
	return tipRect;
}

//other funcs
Mat detectEdge(Mat image, string pinName, int edgeTh, int edgeDir, Point* p1, Point* p2, int pose, int* result, float* angleRet, float strength = 0.6, float angleStd = -1, float angleTol = 3, int PtDeviation = 2)
{
	if (angleStd == -1 && pose == 0) // set std vals if std is not passed
	{
		angleStd = 90;
	}
	if (angleStd == -1 && pose == 1)
	{
		angleStd = 0;
	}

	Mat colorImg;
	cvtColor(image, colorImg, COLOR_GRAY2BGR);
	//imshow(pinName, image);
	vector<Point> edgePts;
	float avgY = 0;
	float avgLeft = 0;
	float sizeL = 0;
	*p1 = Point(0, 0);
	*p2 = Point(0, 0);
	//int th =30;
	//vector <Point> edgePts;
	int length = 0;
	float angle = 45;
	if (pose == 0) //detect vertical edge
	{
		length = image.rows;
		//cout << "case 1 " << endl;
		for (int i = 0; i < image.rows; i++)
		{
			for (int j = 5; j < image.cols - 5; j++)
				//for (int j = image.cols - 5; j > 5; j--)
			{
				float sumL = 0;
				float  sumU = 0;
				int pixVal = 0;
				for (int p = j - 5; p < j + 5; p++)
				{
					pixVal = image.at<uchar>(i, p);
					if (p < j)
						sumL += pixVal;
					else
						sumU += pixVal;
				}
				sumL = sumL / 5.0;
				sumU = sumU / 5.0;
				int diff = 0;
				if (edgeDir == 0)
					diff = sumU - sumL;
				else if (edgeDir == 1)
					diff = sumL - sumU;
				else
					diff = abs(sumL - sumU);

				if (diff > edgeTh)
				{
					//circle(colorImg, Point(j, i), 1, Scalar(0, 255, 255), -1);
					edgePts.push_back(Point(j, i));
					break;
				}
			}
		}
	}
	else //to detect hor edge 
	{
		length = image.cols;
		//cout << "case 2" << endl;
		for (int i = 0; i < image.cols; i++)
		{
			for (int j = 5; j < image.rows - 5; j++)
				//for (int j = image.cols - 5; j > 5; j--)
			{
				float sumL = 0;
				float  sumU = 0;
				int pixVal = 0;
				for (int p = j - 5; p < j + 5; p++)
				{
					pixVal = image.at<uchar>(p, i);
					if (p < j)
						sumL += pixVal;
					else
						sumU += pixVal;
				}
				sumL = sumL / 5.0;
				sumU = sumU / 5.0;
				int diff = 0;
				if (edgeDir == 0)
					diff = sumU - sumL;
				else if (edgeDir == 1)
					diff = sumL - sumU;
				else
					diff = abs(sumL - sumU);
				//	circle(colorImg, Point(i, j), 1, Scalar(255,0,0), -1);
				if (diff > edgeTh)
				{
					//circle(colorImg, Point(i, j), 1, Scalar(0, 255, 255), -1);
					edgePts.push_back(Point(i, j));
					break;
				}
			}
			//	resShow("int", colorImg, 1);
				//waitKey();

		}
	}

	int validCnt = 0;
	if (edgePts.size() > 2)
	{
		cv::Vec4f line_para;
		cv::fitLine(edgePts, line_para, cv::DIST_L2, 0, 1e-2, 1e-2);
		cv::Point point0;
		point0.x = line_para[2];
		point0.y = line_para[3];

		double k = line_para[1] / line_para[0];

		//Calculate end of line(y = k(x - x0) + y0)
		cv::Point point1, point2;
		if (pose == 1)
		{
			point1.x = 0;
			point1.y = k * (0 - point0.x) + point0.y;
			point2.x = image.cols;
			point2.y = k * (image.cols - point0.x) + point0.y;
		}
		else
		{
			point1.y = 1;
			point1.x = point0.x + ((float)(1 - point0.y) / k);
			point2.y = image.rows;
			point2.x = point0.x + ((float)((image.rows - point0.y)) / k);
		}
		for (int k = 0; k < edgePts.size(); k++)
		{
			double d = dist(edgePts[k].x, edgePts[k].y, point1.x, point1.y, point2.x, point2.y);
			if (abs(d) < PtDeviation)
			{
				circle(colorImg, edgePts[k], 1, Scalar(0, 255, 0), -1);
				validCnt++;
			}
			else
				circle(colorImg, edgePts[k], 1, Scalar(0, 0, 255), -1);
		}

		//circle(colorImg, point1, 3, Scalar(0, 0, 255), -1);
		//circle(colorImg, point2, 3, Scalar(0, 0, 255), -1);
		//cout << pinName << "  point 1:" << point1 << "  point 2:" << point2 << endl;
		//cv::line(colorImg, point1, point2, cv::Scalar(0, 255, 0), 2, 8, 0);
		*p1 = point1;
		*p2 = point2;
		Point dp1, dp2;
		dp1 = *p1;
		dp2 = *p2;
		dp1.y = image.rows - dp1.y;
		dp2.y = image.rows - dp2.y;
		//if(point1.x< point2.x )
		//angle = getAngle(Point2f(0, 1000), Point2f(0, 0), *p1, *p2);
		//else
		//angle = getAngle(Point2f(0, 1000), Point2f(0, 0), *p2, *p1);

		//if (point1.x < point2.x)
		//	angle = getAngle(Point2f(0, 100), Point2f(0, 0), dp1, dp2);
		//else
		//	angle = getAngle(Point2f(0, 100), Point2f(0, 0), dp2, dp1);
		if (point1.x < point2.x)
			angle = getAngle(Point2f(0, 0), Point2f(100, 0), dp1, dp2);
		else
			angle = getAngle(Point2f(0, 0), Point2f(100, 0), dp2, dp1);
		if (angle > 180)
			angle = abs(angle - 180);
		*angleRet = angle;
		//cout << pinName + " angle :" << angle << endl;
	}
	else
	{
		cout << pinName << " no edge" << endl;
	}
	bool angleRes = (abs(angleStd - angle) < angleTol);
	if (angleRes && (validCnt > (strength * length)))
	{
		*result = 1;
	}
	else
	{
		if (!angleRes)
			cout << "angle Defect :: found angle::" << angle << "  std::" << angleStd << endl;
		if (!(validCnt > (strength * length)))
			cout << "strength defect:: current strength::" << validCnt << "  std::" << strength * length << endl;
		*result = 0;
	}
	//imshow(pinName, colorImg);

	return colorImg;
}
Mat detectEdgeOppDir(Mat image, string pinName, int edgeTh, int edgeDir, Point* p1, Point* p2, int pose, int* result, float* angleRet, float strength = 0.6, float angleStd = -1, float angleTol = 3, int PtDeviation = 2)
{
	if (angleStd == -1 && pose == 0) // set std vals if std is not passed
	{
		angleStd = 90;
	}
	if (angleStd == -1 && pose == 1)
	{
		angleStd = 0;
	}

	Mat colorImg;
	cvtColor(image, colorImg, COLOR_GRAY2BGR);
	//imshow(pinName, image);
	vector<Point> edgePts;
	float avgY = 0;
	float avgLeft = 0;
	float sizeL = 0;
	*p1 = Point(0, 0);
	*p2 = Point(0, 0);
	//int th =30;
	//vector <Point> edgePts;
	int length = 0;
	float angle = 45;
	if (pose == 0) //detect vertical edge
	{
		length = image.rows;
		//cout << "case 1 " << endl;
		for (int i = 0; i < image.rows; i++)
		{
			//for (int j = 5; j < image.cols - 5; j++)
			for (int j = image.cols - 5; j > 5; j--)
			{
				float sumL = 0;
				float  sumU = 0;
				int pixVal = 0;
				for (int p = j - 5; p < j + 5; p++)
				{
					pixVal = image.at<uchar>(i, p);
					if (p < j)
						sumL += pixVal;
					else
						sumU += pixVal;
				}
				sumL = sumL / 5.0;
				sumU = sumU / 5.0;
				int diff = 0;
				if (edgeDir == 0)
					diff = sumU - sumL;
				else if (edgeDir == 1)
					diff = sumL - sumU;
				else
					diff = abs(sumL - sumU);

				if (diff > edgeTh)
				{
					//circle(colorImg, Point(j, i), 1, Scalar(0, 255, 255), -1);
					edgePts.push_back(Point(j, i));
					break;
				}
			}
		}
	}
	else //to detect hor edge 
	{
		length = image.cols;
		//cout << "case 2" << endl;
		for (int i = 0; i < image.cols; i++)
		{
			//for (int j = 5; j < image.rows - 5; j++)
			for (int j = image.rows - 5; j > 5; j--)
			{
				float sumL = 0;
				float  sumU = 0;
				int pixVal = 0;
				for (int p = j - 5; p < j + 5; p++)
				{
					pixVal = image.at<uchar>(p, i);
					if (p < j)
						sumL += pixVal;
					else
						sumU += pixVal;
				}
				sumL = sumL / 5.0;
				sumU = sumU / 5.0;
				int diff = 0;
				if (edgeDir == 0)
					diff = sumU - sumL;
				else if (edgeDir == 1)
					diff = sumL - sumU;
				else
					diff = abs(sumL - sumU);
				//circle(colorImg, Point(i, j), 1, Scalar(255,0,0), -1);

				//cout << "   diff val::" << diff << endl;
				if (diff > edgeTh)
				{
					//circle(colorImg, Point(i, j), 1, Scalar(0, 255, 255), -1);
					edgePts.push_back(Point(i, j));
					break;
				}
				//resShow("int", colorImg, 1);
				//waitKey();
			}


		}
	}

	int validCnt = 0;
	if (edgePts.size() > 2)
	{
		cv::Vec4f line_para;
		cv::fitLine(edgePts, line_para, cv::DIST_L2, 0, 1e-2, 1e-2);
		cv::Point point0;
		point0.x = line_para[2];
		point0.y = line_para[3];

		double k = line_para[1] / line_para[0];

		//Calculate end of line(y = k(x - x0) + y0)
		cv::Point point1, point2;
		if (pose == 1)
		{
			point1.x = 0;
			point1.y = k * (0 - point0.x) + point0.y;
			point2.x = image.cols;
			point2.y = k * (image.cols - point0.x) + point0.y;
		}
		else
		{
			point1.y = 1;
			point1.x = point0.x + ((float)(1 - point0.y) / k);
			point2.y = image.rows;
			point2.x = point0.x + ((float)((image.rows - point0.y)) / k);
		}
		for (int k = 0; k < edgePts.size(); k++)
		{
			double d = dist(edgePts[k].x, edgePts[k].y, point1.x, point1.y, point2.x, point2.y);
			if (abs(d) < PtDeviation)
			{
				//circle(colorImg, edgePts[k], 1, Scalar(0, 255, 0), -1);
				validCnt++;
			}
			//else
				//circle(colorImg, edgePts[k], 1, Scalar(0, 0, 255), -1);
		}

		//circle(colorImg, point1, 3, Scalar(0, 0, 255), -1);
		//circle(colorImg, point2, 3, Scalar(0, 0, 255), -1);
		//cout << pinName << "  point 1:" << point1 << "  point 2:" << point2 << endl;
		//cv::line(colorImg, point1, point2, cv::Scalar(0, 255, 0), 2, 8, 0);
		*p1 = point1;
		*p2 = point2;
		Point dp1, dp2;
		dp1 = *p1;
		dp2 = *p2;
		dp1.y = image.rows - dp1.y;
		dp2.y = image.rows - dp2.y;
		//if(point1.x< point2.x )
		//angle = getAngle(Point2f(0, 1000), Point2f(0, 0), *p1, *p2);
		//else
		//angle = getAngle(Point2f(0, 1000), Point2f(0, 0), *p2, *p1);

		//if (pose == 0) //new
		//{
		//	if (point1.y < point2.y)
		//		angle = getAngle(Point2f(0, 100), Point2f(0, 0), dp1, dp2);
		//	else
		//		angle = getAngle(Point2f(0, 100), Point2f(0, 0), dp2, dp1);

		//	*angleRet = angle;
		//}
		//else
		//{
	/*		if (point1.x < point2.x)
				angle = getAngle(Point2f(0, 100), Point2f(0, 0), dp1, dp2);
			else
				angle = getAngle(Point2f(0, 100), Point2f(0, 0), dp2, dp1);*/
		if (point1.x < point2.x)
			angle = getAngle(Point2f(0, 0), Point2f(100, 0), dp1, dp2);
		else
			angle = getAngle(Point2f(0, 0), Point2f(100, 0), dp2, dp1);
		if (angle > 180)
			angle = abs(angle - 180);
		*angleRet = angle;
		//}
	//	cout << pinName + " angle :" << angle << endl;
	}
	else
	{
		cout << pinName << " no edge" << endl;
	}
	bool angleRes = (abs(angleStd - angle) < angleTol);
	if (angleRes && (validCnt > (strength * length)))
	{
		*result = 1;
	}
	else
	{
		if (!angleRes)
			cout << "angle Defect :: found angle::" << angle << "  std::" << angleStd << endl;
		if (!(validCnt > (strength * length)))
			cout << "strength defect:: current strength::" << validCnt << "  std::" << strength * length << endl;
		*result = 0;
	}
	//imshow(pinName, colorImg);

	return colorImg;
}
Mat detectEdgeOppDirFace(Mat image, string pinName, int edgeTh, int edgeDir, Point* p1, Point* p2, int pose, int* result, float* angleRet, float strength = 0.6, float angleStd = -1, float angleTol = 3, int PtDeviation = 2)
{
	if (angleStd == -1 && pose == 0) // set std vals if std is not passed
	{
		angleStd = 90;
	}
	if (angleStd == -1 && pose == 1)
	{
		angleStd = 0;
	}

	Mat colorImg;
	cvtColor(image, colorImg, COLOR_GRAY2BGR);
	//imshow(pinName, image);
	vector<Point> edgePts;
	float avgY = 0;
	float avgLeft = 0;
	float sizeL = 0;
	*p1 = Point(0, 0);
	*p2 = Point(0, 0);
	//int th =30;
	//vector <Point> edgePts;
	int length = 0;
	float angle = 45;
	if (pose == 0) //detect vertical edge
	{
		length = image.rows;
		//cout << "case 1 " << endl;
		for (int i = 0; i < image.rows; i++)
		{
			//for (int j = 5; j < image.cols - 5; j++)
			for (int j = image.cols - 5; j > 5; j--)
			{
				float sumL = 0;
				float  sumU = 0;
				int pixVal = 0;
				for (int p = j - 5; p < j + 5; p++)
				{
					pixVal = image.at<uchar>(i, p);
					if (p < j)
						sumL += pixVal;
					else
						sumU += pixVal;
				}
				sumL = sumL / 5.0;
				sumU = sumU / 5.0;
				int diff = 0;
				if (edgeDir == 0)
					diff = sumU - sumL;
				else if (edgeDir == 1)
					diff = sumL - sumU;
				else
					diff = abs(sumL - sumU);

				if (diff > edgeTh)
				{
					circle(colorImg, Point(j, i), 1, Scalar(0, 255, 255), -1);
					edgePts.push_back(Point(j, i));
					break;
				}
			}
		}
	}
	else //to detect hor edge 
	{
		length = image.cols;
		//cout << "case 2" << endl;
		for (int i = 0; i < image.cols; i++)
		{
			//for (int j = 5; j < image.rows - 5; j++)
			for (int j = image.rows - 5; j > 5; j--)
			{
				float sumL = 0;
				float  sumU = 0;
				int pixVal = 0;
				for (int p = j - 5; p < j + 5; p++)
				{
					pixVal = image.at<uchar>(p, i);
					if (p < j)
						sumL += pixVal;
					else
						sumU += pixVal;
				}
				sumL = sumL / 5.0;
				sumU = sumU / 5.0;
				int diff = 0;
				if (edgeDir == 0)
					diff = sumU - sumL;
				else if (edgeDir == 1)
					diff = sumL - sumU;
				else
					diff = abs(sumL - sumU);
				//circle(colorImg, Point(i, j), 1, Scalar(255,0,0), -1);

				//cout << "   diff val::" << diff << endl;
				if (diff > edgeTh)
				{
					//circle(colorImg, Point(i, j), 1, Scalar(0, 255, 255), -1);
					edgePts.push_back(Point(i, j));
					break;
				}
				//resShow("int", colorImg, 1);
				//waitKey();
			}


		}
	}

	int validCnt = 0;
	if (edgePts.size() > 2)
	{
		cv::Vec4f line_para;
		cv::fitLine(edgePts, line_para, cv::DIST_L2, 0, 1e-2, 1e-2);
		cv::Point point0;
		point0.x = line_para[2];
		point0.y = line_para[3];

		double k = line_para[1] / line_para[0];

		//Calculate end of line(y = k(x - x0) + y0)
		cv::Point point1, point2;
		if (pose == 1)
		{
			point1.x = 0;
			point1.y = k * (0 - point0.x) + point0.y;
			point2.x = image.cols;
			point2.y = k * (image.cols - point0.x) + point0.y;
		}
		else
		{
			point1.y = 1;
			point1.x = point0.x + ((float)(1 - point0.y) / k);
			point2.y = image.rows;
			point2.x = point0.x + ((float)((image.rows - point0.y)) / k);
		}
		for (int k = 0; k < edgePts.size(); k++)
		{
			double d = dist(edgePts[k].x, edgePts[k].y, point1.x, point1.y, point2.x, point2.y);
			if (abs(d) < PtDeviation)
			{
				circle(colorImg, edgePts[k], 1, Scalar(0, 255, 0), -1);
				validCnt++;
			}
			else
				circle(colorImg, edgePts[k], 1, Scalar(0, 0, 255), -1);
		}

		circle(colorImg, point1, 3, Scalar(0, 0, 255), -1);
		circle(colorImg, point2, 3, Scalar(0, 0, 255), -1);
		cout << pinName << "  point 1:" << point1 << "  point 2:" << point2 << endl;
		//cv::line(colorImg, point1, point2, cv::Scalar(0, 255, 0), 2, 8, 0);
		*p1 = point1;
		*p2 = point2;
		Point dp1, dp2;
		dp1 = *p1;
		dp2 = *p2;
		dp1.y = image.rows - dp1.y;
		dp2.y = image.rows - dp2.y;
		//if(point1.x< point2.x )
		//angle = getAngle(Point2f(0, 1000), Point2f(0, 0), *p1, *p2);
		//else
		//angle = getAngle(Point2f(0, 1000), Point2f(0, 0), *p2, *p1);

		//if (pose == 0) //new
		//{
		//	if (point1.y < point2.y)
		//		angle = getAngle(Point2f(0, 100), Point2f(0, 0), dp1, dp2);
		//	else
		//		angle = getAngle(Point2f(0, 100), Point2f(0, 0), dp2, dp1);

		//	*angleRet = angle;
		//}
		//else
		//{
		//if (point1.x < point2.x)
		//	angle = getAngle(Point2f(0, 100), Point2f(0, 0), dp1, dp2);
		//else
		//	angle = getAngle(Point2f(0, 100), Point2f(0, 0), dp2, dp1);
		if (point1.x < point2.x)
			angle = getAngle(Point2f(0, 0), Point2f(100, 0), dp1, dp2);
		else
			angle = getAngle(Point2f(0, 0), Point2f(100, 0), dp2, dp1);
		if (angle > 180)
			angle = abs(angle - 180);
		*angleRet = angle;
		//}
		cout << pinName + " angle :" << angle << endl;
	}
	else
	{
		cout << pinName << " no edge" << endl;
	}
	bool angleRes;
	if (abs(angle - 180) < angleTol || abs(angle - 0) < angleTol)
	{
		angleRes = true;
	}
	else
	{
		angleRes = false;
	}

	if (angleRes && (validCnt > (strength * length)))
	{
		*result = 1;
	}
	else
	{
		if (!angleRes)
			cout << "angle Defect :: found angle::" << angle << "  std::" << angleStd << endl;
		if (!(validCnt > (strength * length)))
			cout << "strength defect:: current strength::" << validCnt << "  std::" << strength * length << endl;
		*result = 0;
	}
	imshow(pinName, colorImg);

	return colorImg;
}


	Mat getMaskingPolygon(Mat imageIn, Rect TipRect, Point tip, Rect edgeRect, vector<Point> leftEdge, vector<Point> rightEdge, int height_aboveTip, int width_LnR, int burrTol, int* defectCntRet)
	{
		tip = Point(TipRect.x + tip.x, TipRect.y + tip.y);
		leftEdge[0] = Point(edgeRect.x + leftEdge[0].x, edgeRect.y + leftEdge[0].y);
		leftEdge[1] = Point(edgeRect.x + leftEdge[1].x, edgeRect.y + leftEdge[1].y);
		rightEdge[0] = Point(edgeRect.x + rightEdge[0].x, edgeRect.y + rightEdge[0].y);
		rightEdge[1] = Point(edgeRect.x + rightEdge[1].x, edgeRect.y + rightEdge[1].y);

		int extremeL = min(leftEdge[0].x, leftEdge[1].x);
		int extremeR = max(rightEdge[0].x, rightEdge[1].x);
		Point ptTL = Point(extremeL - width_LnR, tip.y - height_aboveTip);
		Point ptBL = Point(extremeL - width_LnR, leftEdge[1].y);

		Point ptTR = Point(extremeR + width_LnR, tip.y - height_aboveTip);
		Point ptBR = Point(extremeR + width_LnR, rightEdge[1].y);

		//tip boundary
		Point Lcorner = Point(leftEdge[0].x, tip.y);
		Point Rcorner = Point(rightEdge[0].x, tip.y);

		vector< vector <Point>> polygon(1);
		//Mat maskRect = Mat::zeros(Size(abs(ptTL.x - ptTR.x), abs(ptTL.y - ptBL.y)), CV_8UC1);
		Mat maskRect = Mat::zeros(imageIn.size(), CV_8UC1);
		maskRect = 255 - maskRect;
		polygon[0].push_back(leftEdge[1]);
		polygon[0].push_back(leftEdge[0]);
		polygon[0].push_back(Lcorner);
		polygon[0].push_back(Rcorner);
		polygon[0].push_back(rightEdge[0]);
		polygon[0].push_back(rightEdge[1]);
		Rect tipRoi = Rect(ptTL, ptBR);
		tipRoi = validateRectBounds(tipRoi, imageIn.rows, imageIn.cols);
		fillPoly(maskRect, polygon, cv::Scalar(0));
		drawContours(maskRect, polygon, 0, Scalar(0), 4);
		rectangle(maskRect, tipRoi, Scalar(0), 2);
		//imshow("tipMask", maskRect);
		//cout << "imagein channels::" << imageIn.channels() << endl;
		Mat burrCheck = Mat::zeros(tipRoi.size(), CV_8UC1);
		burrCheck = 255 - burrCheck;
		imageIn(tipRoi).copyTo(burrCheck, maskRect(tipRoi));
		Mat burrTh;
		threshold(burrCheck, burrTh, 100, 255, THRESH_BINARY_INV);
		//------find defects
		vector<vector<cv::Point> > contoursDef;
		vector<Vec4i> hierarchyDef;
		Point2f verticesDef[4];
		RotatedRect r1Def;
		int DefCntProf = 0;

		//
		Mat defectDraw;
		cvtColor(burrCheck, defectDraw, COLOR_GRAY2BGR);
		//imshow("defect th ", burrTh);
		findContours(burrTh, contoursDef, hierarchyDef, RETR_CCOMP, CHAIN_APPROX_NONE);
		int defectCnt = 0;
		for (int ck = 0; ck < contoursDef.size(); ck++)
		{
			RotatedRect rr = minAreaRect(contoursDef[ck]);
			if (rr.size.width > burrTol || rr.size.height > burrTol)
			{
				drawContours(defectDraw, contoursDef, ck, Scalar(0, 0, 255), -1);
				defectCnt++;
			}
		}
		*defectCntRet = defectCnt;
		//cout << "Defect count burrs ::" << defectCnt << endl;
		//---
		//imshow("tipMask", burrCheck);
		//imshow("burrDefects", defectDraw);
		//waitKey(20);
		return maskRect;
	}
	bool checkWithinTol(int camIdx, int parameter, float measuredVal)
	{
		if (camIdx == 0 && parameter< list_inspections0.size())
		{
			float limL = (list_inspections0[parameter].get_std_value() - list_inspections0[parameter].get_tolerance_l());
			float limH = (list_inspections0[parameter].get_std_value() + list_inspections0[parameter].get_tolerance_h());

			if (measuredVal <= limH && measuredVal >= limL)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else if (camIdx == 1 && parameter < list_inspections1.size())
		{
			float limL = (list_inspections1[parameter].get_std_value() - list_inspections1[parameter].get_tolerance_l());
			float limH = (list_inspections1[parameter].get_std_value() + list_inspections1[parameter].get_tolerance_h());

			if (measuredVal <= limH && measuredVal >= limL)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		
	}
	bool debugModeEn = false;
	////////////////////////cathater processing fns
	////tip to cathater distance
	struct meshwidthData //width cam
	{
		float start_pos_mm = 0;
		float end_pos_mm = 0;
		float width_mm = 0;
		float start_pos_pix = 0;
		float end_pos_pix = 0;
		float width_pix = 0;

		Mat drawEdges(Mat imageColor)
		{
			if (start_pos_pix + 10 < imageColor.cols)
				start_pos_pix += 10;
			if (end_pos_pix + 10 < imageColor.cols)
				end_pos_pix += 10;
			line(imageColor, Point(start_pos_pix, imageColor.rows - 1), Point(start_pos_pix, 0), Scalar(0, 255, 0), 2);
			line(imageColor, Point(end_pos_pix, imageColor.rows - 1), Point(end_pos_pix, 0), Scalar(16, 185, 245), 2);
			return imageColor;
		}
	};
	struct widthCameraParams //width cam
	{

		int filerSize = 35;// 35;//number of taps in linear filter
		int noiseFilter = 4;//mean differences below this are ignored
		int edgeWidth = 3;//min number of points required to establish up/down trend
		int edgeMagnitude = 350; //strength of up/down trend
		float mmpp_wCam = 0.10964; // mm per pixel for width cam
	};
	widthCameraParams inst_WidthCamPar;
	meshwidthData detect_Width_and_edges(Mat img)
	{
		meshwidthData inst_meshWidthData;

		vector <int> vec_intensity(img.cols, 299);
		//imshow("Source", img);

		//cout << "imageChannels::" << img.channels() << endl;
		long int avgRedChannel = 0, avgGreenChannel = 0, avgBlueChannel = 0;

		//test
		int imageHeight = inst_WidthCamPar.filerSize * 255 / 12;
		//Mat graphGreenChannel = Mat::zeros(Size(img.cols, imageHeight), CV_8UC3);

		//Mat graphBlueChannel = Mat::zeros(Size(img.cols, 300), CV_8UC1);

		float yPos = 0;
		for (int i = 0; i < img.cols; i++)
		{
			for (int j = 0; j < img.rows; j++)
			{
				avgBlueChannel += (int)img.at<uchar>(Point(i, j));
				//cout << "pixVal ::" << (int)img.at<uchar>(Point(i, j))<< endl;

			}
			if (avgBlueChannel == 0)
				yPos = 299;
			else
				yPos = 299 - (int)(avgBlueChannel / img.rows);
			//cout << "i::" << i << "  y::" << yPos << endl;
			//graphBlueChannel.at<uchar>(Point(i, yPos)) = 255;

			avgBlueChannel = 0;
			vec_intensity[i] = yPos;
		}
		int trend_scope = inst_WidthCamPar.filerSize;//num of pixels used to predict trend
		int upCnt = 0;
		int dnCnt = 0;
		int diff = 0;
		vector<int> vec_trend(vec_intensity.size(), 0);
		bool firstDnTrend = false;
		int firstEdgeLoc = 0;
		int LastEdgeLoc = img.cols;

		int trend = -1;
		int trendCnt = 0;
		for (int k = 0; k < vec_intensity.size() - trend_scope; k++)
		{
			for (int l = 1; l < trend_scope; l++)
			{
				diff = vec_intensity[k + l] - vec_intensity[k];
				if (diff < -1 * inst_WidthCamPar.noiseFilter)
					dnCnt += diff;
				else if (diff > inst_WidthCamPar.noiseFilter)
					upCnt += diff;
			}
			//cout << "up::" << upCnt << "   dnCnt::" << dnCnt << endl;
			if (upCnt >= (-1 * dnCnt))
			{//test
				//cout << "row::" << k << "col::" << upCnt << endl;
				//graphGreenChannel.at<Vec3b>(imageHeight -1- ( upCnt),k)[1] = 255;
				//line(graphGreenChannel, Point(k, imageHeight-1), Point(k, imageHeight-1 - upCnt), Scalar(0, 255,0));
				if (upCnt > inst_WidthCamPar.edgeMagnitude)
				{
					if (trend == 0)
					{
						trendCnt++;
						if (!firstDnTrend && trendCnt > inst_WidthCamPar.edgeWidth)
						{
							firstEdgeLoc = k;
							firstDnTrend = true;

						}
					}
					else
					{
						trend = 0;
						trendCnt = 0;
					}
				}
			}
			else
			{//test
				//cout << "down row::" << k << "col::" << dnCnt << endl;
			//	graphGreenChannel.at<Vec3b>(imageHeight -1-(-1*(dnCnt)),k)[2] = 255;
			//	line(graphGreenChannel, Point(k, imageHeight-1), Point(k, imageHeight-1 - (-1 * (dnCnt))), Scalar(0, 0, 255));
				if ((-1 * dnCnt) > inst_WidthCamPar.edgeMagnitude)
				{
					if (trend == 1)
					{
						trendCnt++;
						if (firstDnTrend && trendCnt > inst_WidthCamPar.edgeWidth)
						{
							LastEdgeLoc = k;
							//cout << "breaking at ::" << k << "  dnCnt ::" << dnCnt << endl;
							break;
						}
					}
					else
					{
						trend = 1;
						trendCnt = 0;
					}
				}
			}


			upCnt = 0;
			dnCnt = 0;
			diff = 0;

		}
		//test
	/*		cout << "first edge::" << firstEdgeLoc << "    last edge::" << LastEdgeLoc << endl;
			line(graphGreenChannel, Point(firstEdgeLoc, imageHeight - 1), Point(firstEdgeLoc, 0), Scalar(0, 255, 255),4);
			line(graphGreenChannel, Point(LastEdgeLoc, imageHeight - 1), Point(LastEdgeLoc, 0), Scalar(16, 185, 245),4);*/
			//imwrite("plot.bmp", graphBlueChannel);
			//imwrite("plotTrend.bmp", graphGreenChannel);

			//imshow("BlueChannelGraph", graphBlueChannel);
		inst_meshWidthData.start_pos_pix = firstEdgeLoc;
		inst_meshWidthData.end_pos_pix = LastEdgeLoc;
		//inst_meshWidthData.width_pix = abs(LastEdgeLoc - firstEdgeLoc);
		//inst_meshWidthData.start_pos_mm = inst_meshWidthData.start_pos_pix * inst_WidthCamPar.mmpp_wCam;
		//inst_meshWidthData.end_pos_mm = inst_meshWidthData.end_pos_pix * inst_WidthCamPar.mmpp_wCam;
		//inst_meshWidthData.width_mm = inst_meshWidthData.width_pix * inst_WidthCamPar.mmpp_wCam;

		//	cout << "mesh width data w::" << inst_meshWidthData.width_mm << endl;
		//	cout << "mesh width data s::" << inst_meshWidthData.start_pos_mm << endl;

		//inst_meshWidthData.drawEdges(img);

		//ai_resShow("plotTrend", graphGreenChannel, 1);
		//test
	//	ai_resShow("plot", graphBlueChannel, 0.3);
	//	ai_resShow("img", img, 0.3);
	//	waitKey(0);
		return inst_meshWidthData;
	}
	/////
	int edgeDirBit = 0;//0 --black to white, 1 white to black ,2 both
	Mat detectTip(Mat imageGray, rectData tipIn, rectData midIn, rectData baseIn, int camIdx, vector <toolCpp_cat::ListInspection> vec_inspectionParams, bool& finalResult)
	{	//inputs

		//rects
		Rect e1 = Rect(250, 890, 540, 109);//mid-cathater burr
		Rect e2 = Rect(250, 1131, 550, 109);//base

		e2 = validateRectBounds(baseIn.r, imageGray.rows, imageGray.cols);
		e1 = validateRectBounds(midIn.r, imageGray.rows, imageGray.cols);
		//eTip = validateRectBounds(tipIn.r, imageGray.rows, imageGray.cols);
		//vars
		int tipFaceThresh_bin = (int)vec_inspectionParams[Bevel_Threshold].get_std_value();// 100;//threshold for tip face
		//Size tipSize = Size((int)vec_inspectionParams[Bevel_Width].get_std_value(), (int)vec_inspectionParams[Bevel_length].get_std_value());// Size(70, 220);//dim l, dim h 
		//Size tipTol = Size(10, 10);
		//Size tip_ok_rangeL = Size(tipSize.width - tipTol.width, tipSize.height - tipTol.height);
		//Size tip_ok_rangeH = Size(tipSize.width + tipTol.width, tipSize.height + tipTol.height);
		//Size2f tip_Angle_Limit = Size2f(47.5, 52.5);
		//Size ndl_to_cath_dist_Range = Size(280, 385);
		float tipBluntLimit = (int)vec_inspectionParams[Tip_Blunt_Limit].get_std_value();// 8;
		float cathaterBurrLim = (int)vec_inspectionParams[Cathater_Burr_Tolerance].get_std_value();// 2;
		int burrThreshold_bin = (int)vec_inspectionParams[Burr_Threshold].get_std_value();// 60;
		//checkWithinTol()

		//optional 
		//Size cathaterBaseDiaLim = Size(40, 200);


		//resuts
		bool tip_present = false;
		bool tip_size_ok = false;
		bool tip_angle_ok = false;
		bool tip_blunt = true;
		bool ndl_to_cath_dist_ok = false;
		bool cathaterTip_ok = false;
		//
		finalResult = false;


		Mat imageColor;
		cvtColor(imageGray, imageColor, COLOR_GRAY2BGR);
		// detect base of cathater
		Scalar color_res;
		int resEdge[] = { 0,0,0,0,0,0,0 };
		float angleRet[] = { 0,0,0,0,0,0,0 };
		vector <Point> BaseL_Lines = { Point(0,0),Point(0,0) };
		vector <Point> BaseR_Lines = { Point(0,0),Point(0,0) };
		//Mat pinImgR = 
		detectEdge(imageGray(e2), "Base L", baseIn.threshold, edgeDirBit, &BaseL_Lines[0], &BaseL_Lines[1], 0, &resEdge[0], &angleRet[0], 0.9, 90, 5, 5);//baseL
		if (debugModeEn)
		{
			//printValueImage(imageColor, "p0", BaseL_Lines[0].y, Point(e2.x + BaseL_Lines[0].x, e2.y + BaseL_Lines[0].y), Scalar(0, 255, 0), 2);
			//printValueImage(imageColor, "p1", BaseL_Lines[1].y, Point(e2.x + BaseL_Lines[1].x, e2.y + BaseL_Lines[1].y), Scalar(0, 255, 0), 2);
			color_res = Scalar(0, (resEdge[0]) * 255, (1 - resEdge[0]) * 255);
			cv::line(imageColor(e2), BaseL_Lines[0], BaseL_Lines[1], color_res, 2, 8, 0);
			rectangle(imageColor, e2, color_res, 1);
		}
		//	printValueImage(imageShow, "angle:", angleRet[0],Point(e2.x,e2.y)+ p1Pts[1],Scalar(200,0,0),2);
		//Mat pinImgR2 = 
		detectEdgeOppDir(imageGray(e2), "Base R", baseIn.threshold, 1 - edgeDirBit, &BaseR_Lines[0], &BaseR_Lines[1], 0, &resEdge[2], &angleRet[2], 0.9, 90, 5, 5);//BaseR
		if (debugModeEn)
		{
			//printValueImage(imageColor, "p0", BaseR_Lines[0].y, Point(e2.x + BaseR_Lines[0].x, e2.y + BaseR_Lines[0].y), Scalar(0, 255, 0), 2);
			//printValueImage(imageColor, "p1", BaseR_Lines[1].y, Point(e2.x + BaseR_Lines[1].x, e2.y + BaseR_Lines[1].y), Scalar(0, 255, 0), 2);
			color_res = Scalar(0, (resEdge[2]) * 255, (1 - resEdge[2]) * 255);
			cv::line(imageColor(e2), BaseR_Lines[0], BaseR_Lines[1], color_res, 2, 8, 0);
			rectangle(imageColor, e2, color_res, 1);
		}

		Point midBase0 = Point(e2.x + (BaseL_Lines[0].x + BaseR_Lines[0].x) / 2, e2.y + BaseR_Lines[0].y);
		Point midBase1 = Point(e2.x + (BaseL_Lines[1].x + BaseR_Lines[1].x) / 2, e2.y + BaseR_Lines[1].y);
		cv::line(imageColor, midBase0, midBase1, color_res, 2, 8, 0);
		float baseDia = abs(BaseL_Lines[0].x - BaseR_Lines[0].x)*mmPPcathater;
		if (!checkWithinTol(camIdx,Cathater_Base_Dia, baseDia))//(baseDia < cathaterBaseDiaLim.width) || (baseDia > cathaterBaseDiaLim.height))
		{
			finalResult = false;
			drawTextWithBackGround(imageColor, cv::format("Sample not found %3.2f",baseDia), Point(50, 50), Scalar(255, 255, 255), Scalar(0, 0, 255), 2, 2);
			drawTextWithBackGround(imageColor, cv::format("NG"), Point(600, 60), Scalar(255, 255, 255), Scalar(0, 0, 255), 4, 2);
			return imageColor;
		}

		///find tip presence
		//Mat imageCont = imageGray.clone();
		vector<vector<Point>> conts = ai_findContsAll(imageGray, THRESH_BINARY, tipFaceThresh_bin);
		for (int k = 0; k < conts.size();k++)
		{
			Rect br = boundingRect(conts[k]);
			cout << br.width * mmPPcathater << "    height::" << br.height * mmPPcathater << endl;
		}
		vector<vector<Point>> contsFiltered = ai_filterConts_bySize(conts, 30, 100, 120, 400, 200);//actual 71,223  //tip size pixel
		contsFiltered = ai_sortConts_byYcoord(contsFiltered);
		int maxCont_id = ai_getMaxAreaContourId(contsFiltered);
		if ((contsFiltered.size() > 0) && maxCont_id != -1)
		{
			//RotatedRect rr=minAreaRect(contsFiltered[maxCont_id]);
			//cout <<"tipSz::"<< rr.size << endl;
			//drawContours(imageColor, contsFiltered, 0, Scalar(0, 255, 0), -1);
			tip_present = true;
		}
		else
		{
			finalResult = false;
			drawTextWithBackGround(imageColor, "Tip not found", Point(50, 50), Scalar(255, 255, 255), Scalar(0, 0, 255), 2, 2);
			drawTextWithBackGround(imageColor, cv::format("NG"), Point(600, 60), Scalar(255, 255, 255), Scalar(0, 0, 255), 4, 2);
			return imageColor;
		}

		if (tip_present)
		{//check tip width, length angle and top shape
			//length width
			Rect tipRect = boundingRect(contsFiltered[maxCont_id]);
			//cout << "tipSz BR::w::" <<tipRect.width<<"  h:"<< tipRect.height << endl;
			//rectangle(imageColor, tipRect, Scalar(255, 0, 0), 1);
			if(checkWithinTol(camIdx,Bevel_Width, tipRect.width * mmPPcathater) && checkWithinTol(camIdx, Bevel_length, tipRect.height * mmPPcathater))//((tip_ok_rangeL.width < tipRect.width) && (tipRect.width < tip_ok_rangeH.width) && (tip_ok_rangeL.height < tipRect.height) && (tipRect.height < tip_ok_rangeH.height))
			{
				rectangle(imageColor, tipRect, Scalar(0, 250, 0), 1);
				tip_size_ok = true;
			}
			else
			{
				rectangle(imageColor, tipRect, Scalar(0, 0, 255), 1);
				tip_size_ok = false;
			}
			//angle
			//tip 

			Rect eTip = tipRect;
			eTip.height = eTip.height / 4;
			vector <Point> ptEndPts = { Point(0,0),Point(0,0) };
			//Mat pinTip0 = 
			detectEdge(imageGray(eTip), "tipA2L", tipIn.threshold, edgeDirBit, &ptEndPts[0], &ptEndPts[1], 0, &resEdge[5], &angleRet[5], 0.8, 65, 5, 4);
			color_res = Scalar(0, 180,130);

			if (debugModeEn)
			{
				color_res = Scalar(0, (resEdge[5]) * 255, (1 - resEdge[5]) * 255);

				cv::line(imageColor(eTip), ptEndPts[0], ptEndPts[1], color_res, 2, 8, 0);
				//rectangle(imageColor, eTip, color_res, 1);
				//------modify and extend the lines 
				double a, b, c = 0;
				Point p1 = Point(eTip.x, eTip.y) + ptEndPts[0];
				Point p2 = Point(eTip.x, eTip.y) + ptEndPts[1];

				lineFromPoints(p1, p2, &a, &b, &c);
				Point2f ptExtend = Point2f(p1.x + 40, 0);
				ptExtend.y = (-1 * c - (double)a * (ptExtend.x)) / b;
				cv::line(imageColor, p1, ptExtend, color_res, 2, 8, 0);
			}


			vector <Point> ptEndPts2 = { Point(0,0),Point(0,0) };
			//Mat pinTip1 = 
			detectEdgeOppDir(imageGray(eTip), "TipA2R", tipIn.threshold, 1 - edgeDirBit, &ptEndPts2[0], &ptEndPts2[1], 0, &resEdge[6], &angleRet[6], 0.8, 115, 5, 4);

			if (debugModeEn)
			{
				color_res = Scalar(0, (resEdge[6]) * 255, (1 - resEdge[6]) * 255);
				cv::line(imageColor(eTip), ptEndPts2[0], ptEndPts2[1], color_res, 2, 8, 0);
				//rectangle(imageColor, eTip, color_res, 1);
				//------modify and extend the lines 
				double a, b, c = 0;
				Point p1 = Point(eTip.x, eTip.y) + ptEndPts2[0];
				Point p2 = Point(eTip.x, eTip.y) + ptEndPts2[1];

				lineFromPoints(p1, p2, &a, &b, &c);
				Point2f ptExtend = Point2f(p1.x - 40, 0);
				ptExtend.y = (-1 * c - (double)a * (ptExtend.x)) / b;

				cv::line(imageColor, p1, ptExtend, color_res, 2, 8, 0);
			}
			//bluntness check
			cv::line(imageColor(eTip), ptEndPts[0], ptEndPts2[0], color_res, 1, 8, 0);
			float blunt_sz = ai_distancePoint(ptEndPts[0], ptEndPts2[0])*mmPPcathater;
			//cout << "blunt size::" << blunt_sz << endl;
			if ((blunt_sz - 0.20) > tipBluntLimit)
			{
				tip_blunt = true;
				drawTextWithBackGround(imageColor, cv::format("Tip Blunt :%3.1f", blunt_sz), Point(50, 90), Scalar(255, 255, 255), Scalar(0, 0, 255), 2, 2);
			}
			else
			{
				tip_blunt = false;
			}
			//print tip angle
			float tip_angle_val = abs(angleRet[5] - angleRet[6]);
		/*	cout << "a5::" << angleRet[5] << "   a6::" << angleRet[6] << endl;
			cout << "a diff::" << tip_angle_val << endl;*/
			ai_printValueImage(imageColor, "Tip /_:", tip_angle_val, Point(eTip.x + 100, eTip.y - 80), Scalar(0, 255, 0), 2, 1);
			if (checkWithinTol(camIdx,Tip_Angle, tip_angle_val))//(tip_Angle_Limit.width < tip_angle_val) && (tip_angle_val < tip_Angle_Limit.height))
			{
				tip_angle_ok = true;
			}
			else
			{
				drawTextWithBackGround(imageColor, cv::format("Tip Angle NG: %3.1f", tip_angle_val), Point(50, 130), Scalar(255, 255, 255), Scalar(0, 0, 255), 2, 2);
			}



			Rect distDetectionArea = Rect(e2.x + BaseL_Lines[0].x, tipRect.y + tipRect.height / 2, 150, abs((tipRect.y + tipRect.height / 2) - e2.y + BaseL_Lines[0].y));
			Mat distImage = imageGray(distDetectionArea);
			Mat rot90;
			transpose(distImage, rot90);
			//ai_resShow("cropAr", distImage, 1);
	//		ai_resShow("cropAr90", rot90, 1);
			GaussianBlur(rot90, rot90, Size(5, 5), 0, 0);
			rot90.setTo(0, rot90 < 40);

			//ai_resShow("cropArTh", rot90, 1);

			meshwidthData wd = detect_Width_and_edges(rot90);
			int cathaterTipY = (wd.end_pos_pix + distDetectionArea.y) + 20;
			//vertical
			//arrowedLine()
			cv::arrowedLine(imageColor, Point(distDetectionArea.x-20, tipRect.y), Point(distDetectionArea.x-20, cathaterTipY), Scalar(0, 165, 255), 2, 8, 0);
			cv::arrowedLine(imageColor, Point(distDetectionArea.x - 20, cathaterTipY), Point(distDetectionArea.x-20, tipRect.y), Scalar(0,165,255), 2, 8, 0);
		
			//top
			cv::line(imageColor, Point(0, cathaterTipY), Point(imageColor.cols - 1, cathaterTipY), Scalar(0, 128,139), 2, 8, 0);
			//bottom
			cv::line(imageColor, Point(0, tipRect.y), Point(imageColor.cols - 1, tipRect.y), Scalar(0, 128,139), 2, 8, 0);

			float ndlTocatTipDist = abs(tipRect.y - cathaterTipY)*mmPPcathater;
			if (checkWithinTol(camIdx,Tip_to_Cathater_distance, ndlTocatTipDist))//(ndl_to_cath_dist_Range.width < ndlTocatTipDist) && (ndlTocatTipDist < ndl_to_cath_dist_Range.height))
			{
				ndl_to_cath_dist_ok = true;
			}
			else
			{
				drawTextWithBackGround(imageColor, cv::format("Tip Dist:Ndl->Cat NG:%3.1f", ndlTocatTipDist), Point(50, 180), Scalar(255, 255, 255), Scalar(0, 0, 255), 2, 2);
				ndl_to_cath_dist_ok = false;
			}
			ai_printValueImage(imageColor, "d:", ndlTocatTipDist, Point(eTip.x - 200, distDetectionArea.y), Scalar(0, 255, 0), 2);
			//mid cathater burr
			//modify mid rect e1 to start from cathater tip
			e1.y = cathaterTipY + 70;
			e1 = validateRectBounds(e1, imageGray.rows, imageGray.cols);
			vector <Point> pMidTopPts = { Point(0,0),Point(0,0) };
			//Mat pinImgL = 
			detectEdge(imageGray(e1), "Mid L", midIn.threshold, edgeDirBit, &pMidTopPts[0], &pMidTopPts[1], 0, &resEdge[1], &angleRet[1], 0.7, angleRet[0] - 7, 5, 4);//midL
			//vector<Point> burrDefMaskPolygon = vector<Point>();
			vector< vector <Point>> burrDefMaskPolygon(1);
			if (debugModeEn)
			{
				color_res = Scalar(0, (resEdge[1]) * 255, (1 - resEdge[1]) * 255);
				cv::line(imageColor(e1), pMidTopPts[0], pMidTopPts[1], color_res, 2, 8, 0);
				rectangle(imageColor, e1, color_res, 1);
				ai_printValueImage(imageColor, "A1:", abs(angleRet[0] - angleRet[1]), Point(e1.x, e1.y + pMidTopPts[0].y), Scalar(0, 255, 0), 2);
			}
			//check if midL is ok
			if (resEdge[1])
			{
				double a, b, c = 0;
				Point p1 = Point(e1.x, e1.y) + pMidTopPts[0];
				Point p2 = Point(e1.x, e1.y) + pMidTopPts[1];

				/*	lineFromPoints(p1, p2, &a, &b, &c);
					Point2f ptExtend = Point2f(p1.x + 40, 0);
					ptExtend.y = (-1 * c - (double)a * (ptExtend.x)) / b;
					cv::line(imageColor, p1, ptExtend, color_res, 2, 8, 0);		*/
				lineFromPoints(p1, p2, &a, &b, &c);
				//Point2f ptExtend = Point2f(0, p1.y-250);
				Point2f ptExtend = Point2f(0, tipRect.y + tipRect.height + 20);
				ptExtend.x = -1 * (((ptExtend.y * b) + c) / (double)a);

				cv::line(imageColor, p1, ptExtend, color_res, 2, 8, 0);
				burrDefMaskPolygon[0].push_back(p1);
				burrDefMaskPolygon[0].push_back(ptExtend);

			}

			//waitKey(0);

			vector <Point> pMidLowerPts = { Point(0,0),Point(0,0) };
			//Mat pinImgL2 = 
			detectEdgeOppDir(imageGray(e1), "Mid R", midIn.threshold, 1 - edgeDirBit, &pMidLowerPts[0], &pMidLowerPts[1], 0, &resEdge[3], &angleRet[3], 0.7, angleRet[2] + 7, 5, 4);//MidR
			if (debugModeEn)
			{
				color_res = Scalar(0, (resEdge[3]) * 255, (1 - resEdge[3]) * 255);
				cv::line(imageColor(e1), pMidLowerPts[0], pMidLowerPts[1], color_res, 2, 8, 0);
				rectangle(imageColor, e1, color_res, 1);
				ai_printValueImage(imageColor, "A1:", abs(angleRet[0] - angleRet[3]), Point(e1.x + e1.width, e1.y + pMidLowerPts[0].y + 20), Scalar(0, 255, 0), 2);
			}
			//------modify and extend the lines
			///check if mid R is OK
			if (resEdge[3])
			{
				double a, b, c = 0;
				Point p1 = Point(e1.x, e1.y) + pMidLowerPts[0];
				Point p2 = Point(e1.x, e1.y) + pMidLowerPts[1];

				lineFromPoints(p1, p2, &a, &b, &c);
				/*	Point2f ptExtend = Point2f(p1.x - 40, 0);
					ptExtend.y = (-1 * c - (double)a * (ptExtend.x)) / b;*/
					//Point2f ptExtend = Point2f(0, p1.y - 250);
				Point2f ptExtend = Point2f(0, tipRect.y + tipRect.height + 20);
				ptExtend.x = -1 * (((ptExtend.y * b) + c) / (double)a);
				cv::line(imageColor, p1, ptExtend, color_res, 2, 8, 0);
				burrDefMaskPolygon[0].push_back(ptExtend);
				burrDefMaskPolygon[0].push_back(p1);
			}

			if (resEdge[1] && resEdge[3] && burrDefMaskPolygon[0].size() == 4)
			{
				Rect cropRect = boundingRect(burrDefMaskPolygon[0]);
				cropRect.x = cropRect.x - 80;
				cropRect.width = cropRect.width + 160;
				cropRect = validateRectBounds(cropRect, imageGray.rows, imageGray.cols);
				//for(int k=0;k< burrDefMaskPolygon.size()
				//fillPoly(imageGray, burrDefMaskPolygon[0], cv::Scalar(0));
				drawContours(imageGray, burrDefMaskPolygon, 0, Scalar(0), -1);

				Mat cropBurrTest = imageGray(cropRect).clone();
				vector<vector<Point>> vec_defects = ai_findContsAll(cropBurrTest, THRESH_BINARY, burrThreshold_bin);
				if (vec_defects.size() > 0)
				{
					vec_defects = ai_filterConts_bySize(vec_defects, cathaterBurrLim, 10000, cathaterBurrLim, 10000, cathaterBurrLim * cathaterBurrLim);
					if (vec_defects.size() > 0)
					{
						cathaterTip_ok = false;
						ai_drawContours_w_offset(vec_defects, imageColor, Scalar(0, 0, 255), Point(cropRect.x, cropRect.y));
						drawTextWithBackGround(imageColor, cv::format("Cathater Tip NG. Cnt:%d", vec_defects.size()), Point(50, 230), Scalar(255, 255, 255), Scalar(0, 0, 255), 2, 2);
					}
					else
					{
						cathaterTip_ok = true;
					}

				}
				else
				{
					cathaterTip_ok = true;
				}

				//imshow("cropPar", cropBurrTest);

			}
			else
			{
				cathaterTip_ok = false;
				drawTextWithBackGround(imageColor, cv::format("Cathater Tip NG"), Point(50, 230), Scalar(255, 255, 255), Scalar(0, 0, 255), 2, 2);

			}
			Point midMid0 = Point(e1.x + (pMidTopPts[0].x + pMidLowerPts[0].x) / 2, e1.y + pMidLowerPts[0].y);
			Point midMid1 = Point(e1.x + (pMidTopPts[1].x + pMidLowerPts[1].x) / 2, e1.y + pMidLowerPts[1].y);


			cv::line(imageColor, midMid0, midMid1, color_res, 2, 8, 0);
			//mid cathater burr end

		}

		finalResult = tip_present && tip_angle_ok && tip_size_ok && (!tip_blunt) && ndl_to_cath_dist_ok && cathaterTip_ok;
		if (finalResult)
		{
			drawTextWithBackGround(imageColor, cv::format("OK"), Point(600, 60), Scalar(255, 255, 255), Scalar(0, 255, 0), 4, 2);
		}
		else
		{
			drawTextWithBackGround(imageColor, cv::format("NG"), Point(600, 60), Scalar(255, 255, 255), Scalar(0, 0, 255), 4, 2);
		}

		//ai_drawContours(contsFiltered, imageColor, Scalar(0, 0, 255));
	//	ai_resShow("conts"+to_string(camIdx), imageColor, 0.8);
		//waitKey(50);
		return imageColor;
	}

	int algorithmLib::Class1::dummyProcC1(System::Drawing::Bitmap^ bitmap0, int camNo)
	{
		Mat imageIn0 = BitmapToMat(bitmap0);
		debugModeEn = debugModeProp;
		Mat result = imageIn0;
	try{
		if (vec_cam1Rects.size() ==3)
		{
		/*	for (int id = 0; id < vec_cam1Rects.size(); id++)
			{
				Rect roi = vec_cam1Rects[id].r;
				rectangle(result, roi, Scalar(255, 0, 0), 2);
			}*/
			Mat gray;
			cvtColor(imageIn0, gray, COLOR_BGR2GRAY);
			bool res = false;
			result= detectTip(gray, vec_cam1Rects[2], vec_cam1Rects[1], vec_cam1Rects[0],camNo, list_inspections0 ,res);
			//resize(draw, result, result.size());
			cout << "result C1::" << res << endl;
			resultC1Prop = res;
			if (res)
			{
				putText(result, "OK", Point(880, 50), FONT_HERSHEY_COMPLEX, 2, Scalar(0, 255, 0), 2);
			}
			else
			{
				putText(result, "NG", Point(880, 50), FONT_HERSHEY_COMPLEX, 2, Scalar(0, 0, 255), 2);
			}
		}
	}
	catch (exception exx)
	{
		putText(result, "Exception", Point(480, 50), FONT_HERSHEY_SCRIPT_COMPLEX, 2, Scalar(0, 255, 0), 2);
		resultC1Prop = false;
		resize(result, imageIn0, imageIn0.size());
		return 0;
	}
		putText(result, to_string(camNo), Point(50, 50), FONT_HERSHEY_SCRIPT_COMPLEX, 2, Scalar(0, 255, 0), 2);
		resize(result, imageIn0, imageIn0.size());
		return 1;
	}

	int algorithmLib::Class1::dummyProcC2(System::Drawing::Bitmap^ bitmap0, int camNo)
	{
		Mat imageIn0 = BitmapToMat(bitmap0);
		debugModeEn = debugModeProp;
		Mat result = imageIn0;
		try
		{
			if (vec_cam2Rects.size() == 3)
			{
				//for (int id = 0; id < vec_cam2Rects.size(); id++)
				//{
				//	Rect roi = vec_cam2Rects[id].r;
				//	rectangle(result, roi, Scalar(255, 0, 0), 2);
				//}
				Mat gray;
				cvtColor(imageIn0, gray, COLOR_BGR2GRAY);
				bool res = false;
				result = detectTip(gray, vec_cam2Rects[2], vec_cam2Rects[1], vec_cam2Rects[0],camNo, list_inspections1,res);
				//resize(draw, result, result.size());
				resultC2Prop = res;
				cout << "result C2::" << res << endl;
				if (res)
				{
					putText(result, "OK", Point(880, 50), FONT_HERSHEY_COMPLEX, 2, Scalar(0, 255, 0), 2);
				}
				else
				{
					putText(result, "NG", Point(880, 50), FONT_HERSHEY_COMPLEX, 2, Scalar(0,0, 255), 2);
				}
			}
		}
		catch (exception exx)
		{
			putText(result,"Exception", Point(480, 50), FONT_HERSHEY_SCRIPT_COMPLEX, 2, Scalar(0, 255, 0), 2);
			resultC2Prop = false;
			resize(result, imageIn0, imageIn0.size());
			return 1;
		}
		putText(result, to_string(camNo), Point(50, 50), FONT_HERSHEY_SCRIPT_COMPLEX, 2, Scalar(0, 255, 0), 2);
		resize(result, imageIn0, imageIn0.size());
		return 1;
	}