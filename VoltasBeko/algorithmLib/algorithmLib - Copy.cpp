// This is the main DLL file.

#include "stdafx.h"

#include "EdgesSubPix.h"
#include "algorithmLib.h"
#include <vector>
#include <math.h>
#include "commonOCVfn.h"
using namespace System;
using namespace cv;
using namespace std;
//using namespace toolDef;
//2413,1028
Mat image00;
Mat imageGrey;
Mat imgROI;
Mat draw;

Mat z;
Mat defV;
Mat defH;
vector<Point2f> centerDist;
vector<Point2f> usersPoints;
Point2f P1;
Point2f P2;
string path;

Point tl{160,128};
Point tr{ 3848,248};
Point bl{74,2768};
Point br{3751,2888};
//fn defs
Mat BitmapToMat(System::Drawing::Bitmap^ bitmap);

//--------json load
enum toolTypeCpp { Arc, Width, Circle, Thread, Angle };
string arr_enumNames[] = { "Arc", "Width", "Circle", "Thread", "Angle" };
toolDef::_modelData obj_modelData = toolDef::_modelData();
System::String^ __clrcall algorithmLib::Class1::load_template(System::String^ json_model_data)
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
		obj_modelData = json::parse(json_model_data_std_string);
	} 
	catch (std::exception & ex)
	{
		std::cout << "	 ex.what(); " << ex.what() << std::endl;
	}

	int toolCount =  obj_modelData.get_list_tool_cpp().size();
	cout << " tool Count :::" << toolCount << endl;
	vector<toolDef::ListToolCpp> appliedTools = obj_modelData.get_list_tool_cpp();
	for (int i = 0; i < appliedTools.size(); i++)
	{
		//obj_modelData.get_list_tool_cpp()[i].get_tool_name;
		cout << "----------------------------------------------------------------" << endl;
		cout <<"tool name:"<< obj_modelData.get_list_tool_cpp()[i].get_tool_name() << endl;
		vector<toolDef::Point> tool_inputPoints= appliedTools[i].get_detection_points();
		toolTypeCpp t_type =  (toolTypeCpp)appliedTools[i].get_type();
		cout << "tool type::" << to_string(t_type) << endl;;
		cout << "points in tool " << tool_inputPoints.size()<< endl;
		
		switch (t_type)
		{//Arc, Width, Circle, Thread, Angle 
			cout << "tool Type:" ;
			case  Arc:
			{
				cout << "Arc" << endl;
				break;
			}
			case  Width:
			{
				cout << "Width" << endl;
				break;
			}
			case  Circle:
			{
				cout << "Circle" << endl;
				break;
			}
			case  Thread:
			{
				cout << "Thread" << endl;
				break;
			}
			case  Angle:
			{
				cout << "Angle" << endl;
				break;
			}
			default :
			{
				cout << "invalid tool tyoe in c++" << endl;
			}

		}


		for (int k = 0; k < tool_inputPoints.size(); k++)
		{
		toolDef::Point p = tool_inputPoints[k];
			cout << "Point :" << k << "  value x:" << p.get_x() <<"   tool y:"<< p.get_y() << endl;
		}
		cout << "----------------------------------------------------------------" << endl;

	}

	// _modelData:: from_json

//	std::cout << json_model_data_std_string << std::endl;
	return "";
}
System::String^ __clrcall algorithmLib::Class1::measureSample(System::Drawing::Bitmap^ bitmap0)
{
	Mat imageIn = BitmapToMat(bitmap0);

	//----rotate image ----
	Point center1 = Point(imageIn.cols / 2, imageIn.rows / 2);
	
	Mat rot_mat = getRotationMatrix2D(center1, 45, 1.0);
	warpAffine(imageIn, imageIn, rot_mat, imageIn.size());
	//
	Mat inGray;
	Mat inColor;
	Mat draw;
	if (imageIn.channels() > 2) // input is color
	{
		inColor = imageIn.clone();
		draw = imageIn.clone();
		cvtColor(imageIn, inGray, COLOR_BGR2GRAY);
	}
	else //input is gray
	{
		inGray = imageIn.clone();
		cvtColor(imageIn, inColor, COLOR_GRAY2BGR);
		draw = inColor.clone();
	}

	int toolCount = obj_modelData.get_list_tool_cpp().size();
	cout << " tool Count :::" << toolCount << endl;
	vector<toolDef::ListToolCpp> appliedTools = obj_modelData.get_list_tool_cpp();
	for (int i = 0; i < appliedTools.size(); i++)
	{
		//obj_modelData.get_list_tool_cpp()[i].get_tool_name;
		cout << "----------------------------------------------------------------" << endl;
		cout << "tool name:" << obj_modelData.get_list_tool_cpp()[i].get_tool_name() << endl;
		vector<toolDef::Point> tool_inputPoints = appliedTools[i].get_detection_points();
		toolTypeCpp t_type = (toolTypeCpp)appliedTools[i].get_type();
		cout << "tool type::" << to_string(t_type) << endl;;
		cout << "points in tool " << tool_inputPoints.size() << endl;

		vector <Point> toolPoints;
		for (int k = 0; k < tool_inputPoints.size(); k++)
		{
			toolDef::Point p = tool_inputPoints[k];
			cout << "Point :" << k << "  value x:" << p.get_x() << "   tool y:" << p.get_y() << endl;
			Point pt = Point(p.get_x(), p.get_y());
			toolPoints.push_back(pt);

			pt=transformPoint_shift_rot(pt, rot_mat);
			circle(draw, pt, 6, Scalar(0, 0, 255), -1);
			printValueImage(draw, arr_enumNames[t_type], k, pt, color_red, 2, 2);
		}
		switch (t_type)
		{//Arc, Width, Circle, Thread, Angle 
			cout << "tool Type:";
		case  Arc:
		{
			transformPoint_shift_rot(toolPoints, rot_mat);

			cout << "Arc" << endl;
			float radii = 1;
			Point cntr = Point(0, 0);

			fitCircle(toolPoints[0], toolPoints[1], toolPoints[2], &radii, &cntr);

			circle(draw, cntr, radii,color_blue, 4);
			break;
		}
		case  Width:
		{
			transformPoint_shift_rot(toolPoints, rot_mat);

			line(draw, toolPoints[0], toolPoints[1], color_blue,4);
			Point lineCntr = (toolPoints[0] + toolPoints[1]) / 2;
			line(draw, lineCntr, toolPoints[2], color_red,4);
			cout << "Width" << endl;
			break;
		}
		case  Circle:
		{
			transformPoint_shift_rot(toolPoints, rot_mat);

			cout << "Circle" << endl;
			circle(draw, toolPoints[0],distancePoint(toolPoints[1],toolPoints[0]), color_blue, 2);
			circle(draw, toolPoints[0], distancePoint(toolPoints[2], toolPoints[0]) , color_blue, 2);
			circle(draw, toolPoints[0], 4, color_red,-1);
			break;
		}
		case  Thread:
		{
			cout << "Thread" << endl;
			int x = min(toolPoints[0].x, toolPoints[1].x);
			int y = min(toolPoints[0].y, toolPoints[1].y);
			int w = abs(toolPoints[1].x - toolPoints[0].x);
			int h =abs(toolPoints[1].y - toolPoints[0].y);

			Rect r = Rect (x, y, w, h);
			Point ptStart =Point(0, 0);
			Point ptEnd = Point(0, 0);
			int width = 10;
			if ((toolPoints[0].x - toolPoints[1].y) < 0) //horizontal direction arrow
			{
				ptStart = Point(toolPoints[0].x, (toolPoints[0].y + toolPoints[1].y) / 2);
				ptEnd =  Point(toolPoints[1].x, (toolPoints[0].y + toolPoints[1].y) / 2);
				width = abs(toolPoints[0].y - toolPoints[1].y);
			}
			else  //vertical direction
			{
				ptStart =  Point((toolPoints[0].x + toolPoints[1].x) / 2, toolPoints[0].y);
				ptEnd = Point((toolPoints[0].x + toolPoints[1].x) / 2, toolPoints[1].y);
				width = abs(toolPoints[0].x - toolPoints[1].x);
			}
			transformPoint_shift_rot(toolPoints, rot_mat);
			ptStart= transformPoint_shift_rot(ptStart, rot_mat);
			ptEnd = transformPoint_shift_rot(ptEnd, rot_mat);

			arrowedLine(draw, ptStart, ptEnd, color_green, 4, 8, 0, 0.1);

			//-----get vertices
			
			Point2f returnVecl1[2];
			ptToVector(ptEnd, ptStart, returnVecl1);
			Point tre = getPointAtDist(returnVecl1, width/2, 2);
			Point bre = getPointAtDist(returnVecl1, width / 2, 3);

			ptToVector(ptStart, ptEnd, returnVecl1);
			Point ble = getPointAtDist(returnVecl1, width / 2, 2);
			Point tle = getPointAtDist(returnVecl1, width / 2, 3);
			circle(draw, tre, 10, Scalar(0, 0, 255), -1);
			circle(draw, bre, 10, Scalar(0, 180, 125), -1);

			circle(draw, tle, 10, Scalar(0,255, 0), -1);
			circle(draw, ble, 10, Scalar(120, 125,0), -1);
			//---
			//rectangle(draw, Rect(toolPoints[0], toolPoints[1]), color_blue, 4);
			break;
		}
		case  Angle:
		{
			transformPoint_shift_rot(toolPoints, rot_mat);
			cout << "Angle" << endl;
			line(draw, toolPoints[0], toolPoints[1], color_blue, 4);
			line(draw, toolPoints[2], toolPoints[3], color_blue, 4);
			break;
		}
		default:
		{
			cout << "invalid tool tyoe in c++" << endl;
		}

		}

		cout << "----------------------------------------------------------------" << endl;
	
	}
	resShow("tools", draw,0.3);

	return "";
}
//---------------file read write
string path_s = "";


void pushUsersPoint(vector<Point2f>& usersPoints, Point2f a, Point2f b, Point2f c, Point2f d) {
	usersPoints.push_back(a);
	usersPoints.push_back(b);
	usersPoints.push_back(c);
	usersPoints.push_back(d);
}

void  SmlContourCenter_writeXML(vector<Point2f> data)
{

	FileStorage fs(path_s + "SmlContourCenter.xml", FileStorage::WRITE);

	fs << "Center" << "[";
	for (int i = 0; i < data.size(); ++i)
	{

		fs << "{";
		Point2f  tmp;
		tmp = data.at(i);

		fs << "x" << tmp.x;
		fs << "y" << tmp.y;


		fs << "}";
	}
	fs << "]";
	fs.release();
}
int  SmlContourCenter_readXML(vector<Point2f>& data)
{
	FileStorage fs(path_s + "SmlContourCenter.xml", FileStorage::READ);
	data.clear();
	if (!fs.isOpened())
		return 1;

	FileNode fn = fs["Center"];
	int id = 0;

	for (FileNodeIterator it = fn.begin(); it != fn.end(); it++, id++)
	{
		FileNode item = *it;

		Point2f temp;
		item["x"] >> temp.x;
		item["y"] >> temp.y;

		data.push_back(temp);
		//  cout<<"read contours point :"<<temp<<endl;
	}
	fs.release();
}

void  SmlContourSize_writeXML(vector<cv::Size2f> data)
{

	FileStorage fs(path_s + "SmlContourSize.xml", FileStorage::WRITE);

	fs << "Center" << "[";
	for (int i = 0; i < data.size(); ++i)
	{

		fs << "{";
		cv::Size2f tmp;
		tmp = data.at(i);

		fs << "x" << tmp.height;
		fs << "y" << tmp.width;


		fs << "}";
	}
	fs << "]";
	fs.release();
}
int  SmlContourSize_readXML(vector<cv::Size2f>& data)
{
	FileStorage fs(path_s + "SmlContourSize.xml", FileStorage::READ);
	data.clear();
	if (!fs.isOpened())
		return 1;

	FileNode fn = fs["Center"];
	int id = 0;

	for (FileNodeIterator it = fn.begin(); it != fn.end(); it++, id++)
	{
		FileNode item = *it;

		cv::Size2f temp;
		item["x"] >> temp.height;
		item["y"] >> temp.width;

		data.push_back(temp);
		//  cout<<"read contours point :"<<temp<<endl;
	}
	fs.release();
}

//----------------------------------


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
//other funcs

float getAngle(Point2f l1s, Point2f l1e, Point2f l2s, Point2f l2e)
{
	float ang1 = atan2(l1s.y - l1e.y, l1s.x - l1e.x);
	float ang2 = atan2(l2s.y - l2e.y, l2s.x - l2e.x);
	float ang = (ang2 - ang1) * 180 / (3.14);
	if (ang < 0)
	{
		ang += 360;
	}
	if (ang > 180) {
		ang = 360 - ang;
	}
	return ang;
}
Point2f getClosestPoint(vector<Point2f> contours, Point2f point, int maxId = -1) {
	Point2f closePoint;
	int dist{};
	for (auto i = contours.begin(); i !=  contours.end(); i++) {
		if (i == contours.begin()) {
			closePoint = *i;
			dist = distancePoint(point, *i);
		}
		float distance{};
		distance = distancePoint(point, *i);
		if (distance < dist) {
			closePoint = *i;
			dist = distancePoint(point, *i);
		}
	}
		

	return closePoint;
}

bool does_exist(const vector<Point2f> & v, Point2f item) {

	/*for (int i = 0; i < v.size(); i++) {
		for (int j = 0; j < v[i].size(); j++) {
			if (abs(item.x - v[i][j].x) < 3 && abs(item.y - v[i][j].y) < 3) {
				return true;
			}
		}
	}*/
	for (auto row = v.begin(); row != v.end(); ++row) {
			if (abs(item.x - row->x) < 3 && abs(item.y - row->y) < 3) {
				return true;
			}
	}


	return false;
}

string coeffFile = "D:/Internship/out_camera_data.xml";
Mat correctLensDist(Mat imageIn, string coeffFile)
{
	string filename = coeffFile;// "C:/Add_innovation/out_camera_data.xml";
	// cout << endl << "Reading: " << endl;
	FileStorage fs;
	fs.open(filename, FileStorage::READ);
	/*    if (!fs.isOpened())
		{
			cerr << "Failed to open " << filename << endl;

		}*/

	Mat cameraMatrix, distCoeffs;
	int image_width, image_height;
	fs["camera_matrix"] >> cameraMatrix;                                      // Read cv::Mat
	fs["distortion_coefficients"] >> distCoeffs;
	fs["image_width"] >> image_width;
	fs["image_height"] >> image_height;
	//Size ImageSize;
	//(image_width,image_height);
	//ImageSize.width = image_width;
	//ImageSize.height = image_height;

	Mat  src, map1, map2;
	initUndistortRectifyMap(cameraMatrix, distCoeffs, Mat(),
		getOptimalNewCameraMatrix(cameraMatrix, distCoeffs, Size(image_width, image_height), 1, Size(image_width, image_height), 0),
		Size(image_width, image_height), CV_16SC2, map1, map2);
	// view = imread("C:/Users/thinkPad/Desktop/chart_images/i10.bmp", IMREAD_GRAYSCALE);

	//   	namedWindow("ORIGINAL IMAGE");
	//	imshow("ORIGINAL IMAGE", view);
	remap(imageIn, src, map1, map2, INTER_LINEAR);
	//namedWindow("Undistorted Image");
   // imshow("Undistorted Image", src);
	//waitKey();
	//cout<<"start persp "<<endl;
	//imgPers = src.clone();
	return src;
}


int ringWidth=35;

	//--------------------------------------------sheetmetaldemo----------------------------
	Mat res;
	Mat templ;


	Mat getRotation15AugRotateTempl2img(Mat roi, Mat temp, Mat grayImgToBeRotated, Mat grayImgToBeRotated2, float* matchValue, cv::Point* matchCoord, float* angleRet) //roi,temp ----image and template in any form i.e. edge,inrange etc, grayImagetoberotated--original image which will be rotated and returned
	{
		Mat roi2;
		int rotationAngles = 10;
		resize(roi, roi2, cv::Size(roi.cols / 6, roi.rows / 6));
		Mat temp_roi2;
		resize(temp, temp_roi2, cv::Size(temp.cols / 6, temp.rows / 6));

		// imshow("template",temp_roi2);
		//cv::Point2f center1(roi2.cols / 2.0, roi2.rows / 2.0);
		cv::Point2f center1(temp_roi2.cols / 2.0, temp_roi2.rows / 2.0);
		cv::Size a1 = cv::Size(temp_roi2.cols, temp_roi2.rows);
		vector<double> Minvalues(360);
		Mat roi_rot;
		int result_cols = roi2.cols - temp_roi2.cols + 1;
		int result_rows = roi2.rows - temp_roi2.rows + 1;
		Mat dstImage;
		dstImage.create(result_rows, result_cols, CV_32FC1);
		int match_method = 0; //0-4
		double matchTemp;
		double minVal = 1000000000000;
		double angleBest = 0;
		double minValBest = 1000000000000;
		double maxVal;  cv::Point minLoc; cv::Point maxLoc;
		double angleInc = 0;
		float indexer;
		for (int i = -1 * 4 * rotationAngles; i < 4 * rotationAngles; i++)
		{
			indexer = i;
			if (i == 0)
				angleInc = 0;
			else
			{
				angleInc = indexer / 4;
			}
			/*if (i>0)
			{int rem =i%2;
			if (rem ==1)
			angleInc=(i/2)+0.5;
			else
			angleInc=i/2;
			}
			else
			{    int temp=-1*i;
			int rem = temp%2;
			if (rem ==1)
			angleInc=(i/2)-0.5;
			else
			angleInc=i/2;
			}*/
			//		 cout<<angleInc<<endl;
			Mat rot_mat = getRotationMatrix2D(center1, double(angleInc), 1.0);
			//-----------------
			//		Mat rot_mat = getRotationMatrix2D(center1, double(angleInc), 1.0);
			cv::Rect bbox = cv::RotatedRect(cv::Point2f(), temp_roi2.size(), double(angleInc)).boundingRect();
			// adjust transformation matrix
			rot_mat.at<double>(0, 2) += bbox.width / 2.0 - temp_roi2.cols / 2.0;
			rot_mat.at<double>(1, 2) += bbox.height / 2.0 - temp_roi2.rows / 2.0;
			// warpAffine(edgeImage2, edgeImage2_rot, rot_mat, a, 1);
				//warpAffine(roi2, roi_rot, rot_mat, a1, 1);
			Mat templRot;
			warpAffine(temp_roi2, templRot, rot_mat, bbox.size());
			////draw mask----------------
			//Mat mask = Mat::zeros(templRot.size(), CV_8UC1);
			//cv::RotatedRect rotatedRectangle(cv::Point2f(templRot.cols / 2, templRot.rows / 2), temp_roi2.size(), double(180 - angleInc));

			//// We take the edges that OpenCV calculated for us
			//cv::Point2f vertices2f[4];
			//rotatedRectangle.points(vertices2f);

			//// Convert them so we can use them in a fillConvexPoly
			//cv::Point vertices[4];
			//for (int i = 0; i < 4; ++i) {
			//	vertices[i] = vertices2f[i];
			//}

			//// Now we can fill the rotated rectangle with our specified color
			//cv::fillConvexPoly(mask,
			//	vertices,
			//	4,
			//	Scalar(255));
			////----------draw mask end
			// //----------------------
			// warpAffine(edgeImage2, edgeImage2_rot, rot_mat, a, 1);
			//warpAffine(roi2, roi_rot, rot_mat, a1, 1);
			////Mat templRot;
			////warpAffine(temp_roi2, templRot, rot_mat, a1, 1);

			cv::Point matchLoc;
			cout << "angleInc ::" << angleInc << endl;
			cout << "template type ::" << templRot.type() << "  templ size::" << templRot.size() << endl;
			cout << "roi2 type ::" << roi2.type() << "  roi2 size::" << roi2.size() << endl;

			matchTemplate(roi2, templRot, dstImage, match_method);// , mask);
			minMaxLoc(dstImage, &minVal, &maxVal, &minLoc, &maxLoc, Mat());
			//Minvalues[i] = minVal;
			//rectangle(roi2, minLoc, cv::Point(minLoc.x + temp_roi2.cols, minLoc.y + temp_roi2.rows), Scalar(0, 0, 0), 2, 8, 0);
		//	imshow("rotImg",templRot);
		//	imshow("img", roi2);
		//	waitKey(10);
			if (minVal < minValBest)
			{
				minValBest = minVal;

				angleBest = angleInc;
			}
			//if (i==0)
			//	{matchTemp=minVal;
			//cout << "min_temp_value: "<< minVal <<endl;}
			//else
			//{if (minVal<matchTemp)
			//{
			//	matchTemp=minVal;
			//cout << "min_temp_value: "<< minVal <<"i="<<i<<endl;
			//	imshow("roi_rot",roi_rot);
			//}
			//}

			// waitKey();
			// waitKey(0);
			//cout << "min_temp_value: "<< minVal2 << "\t" << i<<endl;
		}

		double min_temp_value = minValBest;
		double angle = -1 * angleBest;
		*angleRet = angleBest;
		//	 cout<<"Angle"<<angleBest<<endl;
		//	 cout<<"Value"<<minValBest<<endl;
		//for (int i = 0; i < rotationAngles ; i++ )
		//{
		// if(Minvalues[i]  < min_temp_value)
		// {
		//	 min_temp_value = Minvalues[i] ;
		//	 angle = i;
		// }
		//}
		//  cout << "angle: " << angle <<endl;
		// cout << "min_temp_value: "<< min_temp_value <<endl;
		cv::Point2f center2(roi.cols / 2.0, roi.rows / 2.0);
		cv::Size a2 = cv::Size(roi.cols, roi.rows);
		cv::Size a3 = cv::Size(grayImgToBeRotated.cols, grayImgToBeRotated.rows);
		Mat rot_mat = getRotationMatrix2D(center2, angle, 1.0);

		warpAffine(roi, roi_rot, rot_mat, a2, 1);
		Mat returnImage;
		warpAffine(grayImgToBeRotated, returnImage, rot_mat, a3, 1);
		warpAffine(grayImgToBeRotated2, grayImgToBeRotated2, rot_mat, a3, 1);
		matchTemplate(roi_rot, temp, dstImage, match_method);
		minMaxLoc(dstImage, &minVal, &maxVal, &minLoc, &maxLoc, Mat());
		*matchCoord = minLoc;
		*matchValue = minVal;
		return  returnImage;
	}


	int out = 0;
	Mat templateImg;
	Mat templateImgR;


	int loadTemplate(string strings, string stringT2)
	{
		//std::string path = string_to_char_array(string);
		templateImg = imread(strings);
		//std::string patht2 = string_to_char_array(stringT2);
		templateImgR = imread(stringT2);

		//loads 24 bit template image
		//imshow("templateUpper", templateImg);
		//imshow("templateLower", templateImgR);
		//waitKey(10);
		return 1;
	}
	//----all common fns



	Mat  undistortPers(Mat ldcImg, double widthMM, double heightMM, int code)
	{
		auto t = std::time(nullptr);  //add*
		auto tm = *std::localtime(&t);

		std::ostringstream oss;
		oss << std::put_time(&tm, "%d-%m-%Y %H-%M-%S");
		auto  starT = oss.str();  //add*
		//startTime = starT;
		Point2f srcTri[4];
		Point2f dstTri[4];

		cout << "Point TL " << tl << endl;
		cout << "Point TR " << tr << endl;
		cout << "Point BL " << bl << endl;
		cout << "Point BR " << br << endl;

		Mat rot_mat(2, 3, CV_32FC1);
		Mat warp_mat(2, 3, CV_32FC1);
		Mat src, warp_dst, warp_rotate_dst;

		/// Load the image
		src = ldcImg.clone();

		/// Set the dst image the same type and size as src
		warp_dst = Mat::zeros(src.rows, src.cols, src.type());
		/*  Point tl= Point(480,119);
		   Point tr= Point(1496,123);
		   Point bl= Point(464,2142);
		   Point br= Point(1477,2155);*/
		   //  Point tl= Point(144,534);
		   //Point tr= Point(1866,544);
		   //Point bl= Point(136,1912);
		   //Point br= Point(1860,1917);
		   //	  Point tl= Point(294,735);
		   //Point tr= Point(1676,738);
		   //Point bl= Point(280,2116);
		   //Point br= Point(1677,2117);

		//  		  Point tl= Point(470,188);   //31-7-19
		   //Point tr= Point(2173,118); 
		   //Point bl= Point(487,819);
		   //Point br= Point(2194,761);

		  /// Set your 3 points to calculate the  Affine Transform
		   //int width=distancePoint(tl,tr);
		   //int height=distancePoint(tl,bl);
		 //int height=2023;
		 //int width =height*(416.5/831);

		cout << "height::" << heightMM;
		cout << "width::" << widthMM;
		int height = distancePoint(tl, bl);
		//	cout<<"mmPerPix  :: "<<600.0/height<<endl; 
		int width = height * (widthMM / heightMM); //calculate width in pixels from aspect ration of actual height and width width(pix)=height(pix)*(width (mm)/height (mm))

		cout << "height::" << height;
		cout << "width::" << width;
		srcTri[0] = tl;


		srcTri[1] = tr;
		Point trTemp = Point(tl.x + width, tl.y); //a point in same row as tl at a distance of width from tl
		Point2f returnVecl1[2];
		ptToVector(tl, trTemp, returnVecl1);
		Point thirdPoint00l1 = getPointAtDist(returnVecl1, -1 * height, 3);
		returnVecl1[1] = trTemp;
		Point thirdPoint01l1 = getPointAtDist(returnVecl1, -1 * height, 3);
		//circle(src,tl,20,Scalar(255,0,0),3);
		//circle(src,trTemp,20,Scalar(255,0,0),3);
		//circle(src,thirdPoint00l1 ,20,Scalar(0,255,0),3);
		//circle(src,thirdPoint01l1 ,20,Scalar(0,0,255),3);
		//imshow("pts",src);
		// imwrite("C:/Users/thinkPad/Desktop/chart_images/imgWarpsrc.bmp",src);
	   //srcTri[2] = thirdPoint01l1;
	   //  srcTri[3] = thirdPoint00l1;
		srcTri[2] = br;
		srcTri[3] = bl;
		dstTri[0] = tl;
		dstTri[1] = trTemp;
		/*  dstTri[2] = br;
			 dstTri[3] = bl;*/
		dstTri[2] = thirdPoint01l1;
		dstTri[3] = thirdPoint00l1;

		warp_mat = getPerspectiveTransform(srcTri, dstTri);

		warpPerspective(src, warp_dst, warp_mat, warp_dst.size());
		if (code == 2)
		{
			circle(warp_dst, tl, 20, Scalar(0, 140, 255), 3);
			circle(warp_dst, trTemp, 20, Scalar(255, 0, 0), 3);
			circle(warp_dst, thirdPoint00l1, 20, Scalar(0, 255, 0), 3);
			circle(warp_dst, thirdPoint01l1, 20, Scalar(0, 0, 255), 3);
		}
		//imshow("warpOut",warp_dst);
			// imwrite("D:/CV/15Jan/imgPersp.bmp",warp_dst);
	//	resShow("wrap_dst", warp_dst,0.3);
		return warp_dst;

	}
	
	Mat checkUniformWithEdge(Mat imIn, Point p1, Point p2, int widthIn)// , Mat otherImage)// Mat* returnRot)
	{
		Mat cropImg;
		//   Mat drawImg;
		Mat dst;
		float distanceP = distancePoint(p1, p2);

		//get initial points right---  get vertices of rect
		Point2f returnVecl1[2];
		ptToVector(p1, p2, returnVecl1);
		Point tre = p2;// getPointAtDist(returnVecl1, 1 * widthIn, 2);
		//circle(imIn, tre, 8, Scalar(255,0,0), -1);
		//imshow("tre", imIn);
		Point ble = getPointAtDist(returnVecl1, 1 * widthIn, 2);
		//circle(imIn, ble, 8, Scalar(0,255, 0), -1);
		//imshow("tre", imIn);

		Point2f returnVecl2[2];
		ptToVector(p2, p1, returnVecl2);
		Point tle = p1;// getPointAtDist(returnVecl2, 1 * widthIn, 2);
		//circle(imIn, tle, 8, Scalar(0, 0,255), -1);
		//imshow("tre", imIn);
		Point bre = getPointAtDist(returnVecl2, 1 * widthIn, 3);
		//circle(imIn, bre, 8, Scalar(0, 255, 255), -1);
		//resShow("tre", imIn, 0.4);
		//Point2f vertices[4] = { bre,tle,ble,tre };
		Point2f vertices[4] = { tle,ble,tre,bre };
		//Point2f pointsArr[4] = { Point(0,0),Point(distanceP - 1,0),Point(distanceP - 1,widthIn * 2 - 1),Point(0,widthIn * 2 - 1) };
		Point2f pointsArr[4] = { Point(0,0),Point(0,widthIn - 1),Point(distanceP - 1,0),Point(distanceP - 1,widthIn - 1) };
		Mat transMat = getPerspectiveTransform(vertices, pointsArr);
		//	warpPerspective(imIn, dst, transMat, Size(widthIn * 2, distanceP));
		warpPerspective(imIn, dst, transMat, Size(distanceP, widthIn));
		//	warpPerspective(otherImage, *returnRot, transMat, Size(distanceP, widthIn * 2));

		resShow("crop", dst, 0.4);
		//waitKey();
		return dst;
	}
	//save std
	float mmpp = 0.1255077192982456;   //0.  0.119253723812  1235148514851485 0.1244638403990025
	float sizeTol = 0.5; // 0.2
	int extraCnt = 0;
	int missCnt = 0;
	Mat imageC;
	double distance_Pt2Line(Point line_start, Point line_end, Point point)
	{
		double normalLength = hypot(line_end.x - line_start.x, line_end.y - line_start.y);
		//double normalLength = distancePoint(line_start,line_end);
		double distance = (double)((point.x - line_start.x) * (line_end.y - line_start.y) - (point.y - line_start.y) * (line_end.x - line_start.x)) / normalLength;
		//	direction = distance / abs(distance);
		return distance;
		//abs(distance) to get absolute distance
	}
	double distance_Pt2Line(Point line_start, Point line_end, Point point, int& direction)
	{
		double normalLength = hypot(line_end.x - line_start.x, line_end.y - line_start.y);
		//double normalLength = distancePoint(line_start,line_end);
		double distance = (double)((point.x - line_start.x) * (line_end.y - line_start.y) - (point.y - line_start.y) * (line_end.x - line_start.x)) / normalLength;
		direction = distance / abs(distance);
		return distance;
		//abs(distance) to get absolute distance
	}
	Point2f intersection(Point2f o1, Point2f p1, Point2f o2, Point2f p2)
	{
		Point2f x = o2 - o1;
		Point2f d1 = p1 - o1;
		Point2f d2 = p2 - o2;
		Point2f r;

		float cross = d1.x * d2.y - d1.y * d2.x;
		if (std::abs(cross) < /*EPS*/1e-8)
			return false;

		double t1 = (x.x * d2.y - x.y * d2.x) / cross;
		r = o1 + d1 * t1;
		return r;
	}
	vector<Point2f> get_bisector(Point2f p1, Point2f p2, Mat image)
	{
		Point2f mid; double slope, b;
		vector<Point2f>perp;
		mid.x = (p1.x + p2.x) / 2;
		mid.y = (p1.y + p2.y) / 2;
		slope = (double)(p2.y - p1.y) / (p2.x - p1.x);
		slope = -1 * (1 / slope);
		b = mid.y - (slope * mid.x);
		double x_int = (-1 * b) / slope;
		double y_int = b;

		if (x_int < 0) {
			double x = (image.rows - b) / slope;
			//circle(image, Point(x, image.cols), 5, CL_WHITE, 1);
			perp.push_back(Point2f(x, image.rows));
		}
		else {
			//circle(image, Point(x_int, 0), 5, CL_WHITE, 1);
			perp.push_back(Point2f(x_int, 0));
		}

		if (y_int < 0) {
			double y = (image.cols * slope) + b;
			//circle(image, Point(image.rows, y), 5, CL_WHITE, 1);
			perp.push_back(Point2f(image.cols, y));
		}
		else {
			//circle(image, Point(0, y_int), 5, CL_WHITE, 1);
			perp.push_back(Point2f(0, y_int));
		}

		return perp;
	}
	vector<Point2f> getRoi(vector<Point2f> vecPoints, Point2f user) {
		vector<Point2f> temp;
		for (int i = 0; i < vecPoints.size(); i++) {
			float dist = distancePoint(user, vecPoints[i]);
			if (dist < 12) {
				temp.push_back(vecPoints[i]);
			}
		}
		return temp;
	}
	Point2f getFarthestPoint(const vector<Point2f>& vecPoints, Point2f user, RotatedRect brect, vector<Point2f>& tempPoints) {
		cout << "User x " << brect.boundingRect2f().tl().x << endl;
		cout << "User x " << brect.boundingRect2f().tl().y << endl;
		Point2f closePoint = getClosestPoint(vecPoints, Point2f(brect.boundingRect2f().tl().x + user.x, brect.boundingRect2f().tl().y + user.y));
		//vector<Point2f> tempPoints;
		tempPoints = getRoi(vecPoints, closePoint);
		/*for (int i = 0; i < tempPoints.size(); i++) {
			circle(imageC, tempPoints[i], 2, Scalar(0, 150, 255), -1);
		}*/
		Point2f far{};
		float distance_1{};
		for (int i = 0; i < tempPoints.size(); i++) {
			float dist = distancePoint(closePoint, tempPoints[i]);
			if (dist > distance_1) {
				far = tempPoints[i];
				distance_1 = dist;
			}
		}

		return far;
		
	}
	void getNextPoints(Point2f start,const vector<Point2f>& tempPoints, vector<Point2f> &nextPoints) {
		for (int i = 0; i < tempPoints.size(); i++) {
			float dist = distancePoint(start, tempPoints[i]);
			if (dist != 0 && dist > 5) {
				nextPoints.push_back(tempPoints[i]);
			}
		}

		


	}
	
	Point2f intersection_1(Point2f A, Point2f B, Point2f C, Point2f D) {
		// Line AB represented as a1x + b1y = c1
		double a = B.y - A.y;
		double b = A.x - B.x;
		double c = a * (A.x) + b * (A.y);
		// Line CD represented as a2x + b2y = c2
		double a1 = D.y - C.y;
		double b1 = C.x - D.x;
		double c1 = a1 * (C.x) + b1 * (C.y);
		double det = a * b1 - a1 * b;
		if (det == 0) {
			
			
			return Point2f(FLT_MAX, FLT_MAX);
		}
		else {
			double x = (b1 * c - b * c1) / det;
			double y = (a * c1 - a1 * c) / det;
		
			return Point2f(x, y);
		}
	}
	Point2f getCornerPoint(const vector<Point2f>& vecPoints,Point2f user, RotatedRect brect) {
		vector<Point2f> tempPoints;
		vector<Point2f> nextPoints;
		//Point2f pointOnCont = getClosestPoint(vecPoints, user);
		Point2f start = getFarthestPoint(vecPoints, user, brect, tempPoints);
		getNextPoints(start, tempPoints, nextPoints);

		/*for (int i = 0; i < nextPoints.size(); i++) {
			circle(imageC, nextPoints[i],3, Scalar(0, 150, 255), -1);
		}*/
		Point2f lineEnd{};
		float dist{};

		for (int i = 0; i < nextPoints.size(); i++) {
			float distance = distancePoint(start, nextPoints[i]);
			if (i == 0) {
				dist = distance;
			}
			if (distance <= dist) {
				dist = distance;
				lineEnd = nextPoints[i];
			}
		}
		cout << "Start Point " << start << endl;
		cout << "Last Point " << lineEnd << endl;
		vector<Point2f> sortedPoint;
		vector<int> isCopied(nextPoints.size(),0);
		
		
		Point2f first = start;
		for(int j = 0 ;j < nextPoints.size();j++){
			int index{-1};
			Point2f n{};
			float minD{};
			bool copied{ false };
		    for (int i = 0; i < nextPoints.size(); i++) {
				if (isCopied.at(i) == 1 || first == nextPoints[i])
					continue;
		    	float distance = distancePoint(first, nextPoints[i]);
		    	if (!copied) {
					minD = distance;
					copied = true;
		    	}
		    	if (distance <= minD) {
					minD = distance;
		    		n = nextPoints[i];
					index = i;
		    	}
		    }
			cout << "index" << index << endl;
			if (index != -1) {
			   isCopied.at(index) = 1;
			   sortedPoint.push_back(n);
			   first = n;
			}

		}

			
	
		/*for (int i = 0; i < isCopied.size(); i++) {
			cout << isCopied.at(i) << endl;
		}

		for (int i = 0; i < sortedPoint.size(); i++) {
			cout << "Sorted Points " + to_string(i) << sortedPoint[i] << endl;
			Mat draw = imageC.clone();

			circle(draw, start, 3, Scalar(0, 255, 0), -1);
			circle(draw, lineEnd, 3, Scalar(255, 0, 0), -1);

			circle(draw, sortedPoint[i], 1, Scalar(0, 100, 255), -1);
			imshow("draw", draw(brect.boundingRect2f()));
			waitKey(0);
		}*/
		Point2f cornerPoint{};
		cout << "Next Points size " << nextPoints.size() << endl;
		
		for (int i = 0; i < sortedPoint.size(); i++) {
			int direction{};
			double distance = (distance_Pt2Line(start,lineEnd, sortedPoint[i], direction));

			float angle = getAngle(start, lineEnd, start, sortedPoint[i]);

			if (angle > 7) {
				cornerPoint = sortedPoint[i];
				break;
			}
			cout << "angle " << angle << endl;
			
		}
		
		circle(imageC, start, 1, Scalar(0, 0, 255), -1);
		circle(imageC, cornerPoint, 1, Scalar(0, 255, 0), -1);
		cout << "Corner Point " << cornerPoint << endl;

		imshow("corner", imageC(brect.boundingRect2f()));
		return cornerPoint;
		
	}
	int findEdgePoints(Mat image, Point2f p1, Point2f p2, Point2f& p1a, Point2f& p2a)
	{
		Mat imgProc = image.clone();
		//line1
		Point2f returnVecl1[2];

		Point2f thirdPoint00l1;
		Point2f thirdPoint01l1;

		ptToVector(p1, p2, returnVecl1);

		int directionl1 = 0;
		double distanceP = 10;
		thirdPoint00l1 = getPointAtDist(returnVecl1, -1 * 15, 3);
		returnVecl1[1] = p2;
		thirdPoint01l1 = getPointAtDist(returnVecl1, -1 * 15, 3);

		Point2f returnVecEd00[2];
		Point2f pt00temp, pt01temp;
		pt00temp = Point(0, 0);
		pt01temp = Point(0, 0);
		ptToVector(thirdPoint00l1, thirdPoint01l1, returnVecEd00);
		int e1V0, e2V0;
		Point2f ptline10, ptline11, ptline20, ptline21 = Point(0, 0);
		e1V0 = 0;
		e2V0 = 255;
		for (int j = 1; j < (30); j++)
		{

			Point2f ptTemp;
			ptTemp = getPointAtDist(returnVecEd00, j, 3);
			if (e1V0 > 60 && imgProc.at<uchar>(ptTemp) < 60)
			{
				ptline10 = ptTemp;
			}
			if (e2V0 < 60 && imgProc.at<uchar>(ptTemp)>60)
			{
				ptline11 = ptTemp;
			}

			e1V0 = imgProc.at<uchar>(ptTemp);
			e2V0 = e1V0;
		}
		if (ptline10.x == 0 & ptline10.y == 0)
		{
			pt00temp = ptline11;
		}
		else
		{
			pt00temp = ptline10;
		}
		ptToVector(thirdPoint01l1, thirdPoint00l1, returnVecEd00);;
		e1V0 = 0;
		e2V0 = 255;
		for (int j = 1; j < (30); j++)
		{

			Point2f ptTemp;
			ptTemp = getPointAtDist(returnVecEd00, -1 * j, 3);
			if (e1V0 > 60 && imgProc.at<uchar>(ptTemp) < 60)
			{
				ptline20 = ptTemp;
			}
			if (e2V0 < 60 && imgProc.at<uchar>(ptTemp)>60)
			{
				ptline21 = ptTemp;
			}

			e1V0 = imgProc.at<uchar>(ptTemp);
			e2V0 = e1V0;

		}
		if (ptline20.x == 0 & ptline20.y == 0)
		{
			pt01temp = ptline21;
		}
		else
		{
			pt01temp = ptline20;
		}
		p1a = pt00temp;
		p2a = pt01temp;
		/*		circle(imgProc, pt00temp, 3,  Scalar(0,0,255),2,8, 0);
			circle(imgProc, pt01temp, 3,  Scalar(0,0,255),2,8, 0);
			return imgProc;*/
		return 1;
	}
	Mat findArc_radii_test(Mat image, Mat drawImage, Point2f p1, Point2f p2, Point2f p3, float& radii)
	{
		Mat temp = image;
		Mat res, edge, locs;
		cv::Canny(temp, edge, 0, 255);



		//cvtColor(edge,edge,CV_GRAY2BGR);

	//imshow("Output Window", edge);

		vector<Point2f> points(3);
		points[0] = p1;
		points[1] = p2;
		points[2] = p3;

		//cout << "Points: " << points[0] << endl;
		vector<Point2f> cpts;

		int size = 10;
		for (int i = 0; i < points.size(); i++) {
			Mat roi = Mat::zeros(edge.rows, edge.cols, CV_8U);
			cv::Mat maskedImage;
			cv::Mat mask(edge.size(), edge.type());
			mask.setTo(cv::Scalar(0, 0, 0));
			cv::rectangle(mask, Point(points[i].x - size / 2, points[i].y - size / 2), Point(points[i].x + size / 2, points[i].y + size / 2), Scalar(255, 255, 255), -1, 8);
			edge.copyTo(maskedImage, mask);

			vector<Point> locs;
			findNonZero(maskedImage, locs);
			if (locs.size() == 0)
			{
				radii = 0;
				return drawImage;
			}
			//cout << "Locs: " << locs[0] << endl;
			cpts.push_back(locs[0]);
			//imshow("ROI", maskedImage);
			//waitKey(0);
		}
		Point2f pt00temp, pt01temp;
		Point2f pt10temp, pt11temp;
		findEdgePoints(temp, p1, p2, pt00temp, pt01temp);
		findEdgePoints(temp, p2, p3, pt10temp, pt11temp);

		vector<Point2f> perp1, perp2;
		perp1 = get_bisector(pt00temp, pt01temp, edge);
		perp2 = get_bisector(pt10temp, pt11temp, edge);

		Point r;
		r = intersection(perp1[0], perp1[1], perp2[0], perp2[1]);
		cvtColor(edge, edge, COLOR_GRAY2BGR);
		double rad = distancePoint(r, cpts[1]);
		circle(drawImage, pt00temp, 3, Scalar(255, 0, 0), 3);
		circle(drawImage, pt01temp, 3, Scalar(255, 0, 0), 3);
		circle(drawImage, pt11temp, 3, Scalar(255, 0, 0), 3);
		cv::line(drawImage, perp1[0], perp1[1], Scalar(0, 255, 0), 1);
		cv::line(drawImage, perp2[0], perp2[1], Scalar(0, 255, 0), 1);
		//line(edge, cpts[0], cpts[1],  Scalar(0,255,0), 1, 8);
		//line(edge, cpts[1], cpts[2],  Scalar(0,255,0), 1, 8);
		cv::circle(drawImage, r, rad, Scalar(255, 0, 0), 3);
		cv::circle(drawImage, r, 3, Scalar(0, 205, 255), 3);
		cout << "Center: " << r << endl;
		cout << "Radius: " << rad << endl;
		radii = rad;
		char T[100];
		sprintf_s(T, "rcenter(%3.2d ,%3.2d) radius=%3.2f  ", r.x, r.y, rad);
		//putText(drawImage, T, p1, FONT_HERSHEY_COMPLEX_SMALL, 5, CV_RGB(0, 255, 0), 2, 4, false);

		//imshow("gray", edge);
		//waitKey(0);
		return drawImage;
	}
	

	Mat fnE2E(Mat image, Point2f p1, Point2f p2, Point2f p3, float& measuredVal, int flag)
	{
		Point2f returnVec[2];
		Point2f thirdPoint00;
		Point2f thirdPoint01;
		Point2f thirdPoint10;
		Point2f thirdPoint11;
		Mat imgProc = image.clone();
		//if (flag==1)
		cvtColor(image, image, COLOR_GRAY2BGR);
		//cvtColor(image,image,CV_GRAY2BGR);
		ptToVector(p1, p2, returnVec);
		// thirdPoint = getPointAtDist(returnVec,50,0);
		//line(image, Point(p1x, p1y), thirdPoint, Scalar(0,0,0), 3, CV_AA);
		// thirdPoint = getPointAtDist(returnVec,50,1);
		//line(image, Point(p1x, p1y), thirdPoint, Scalar(0,255,255), 3, CV_AA);
		// thirdPoint = getPointAtDist(returnVec,50,2);
		//line(image, Point(p1x, p1y), thirdPoint, Scalar(255,0,0), 3, CV_AA);
		// thirdPoint = getPointAtDist(returnVec,50,3);
		//line(image, Point(p1x, p1y), thirdPoint, Scalar(0,0,255), 3, CV_AA);

		//line(image, Point(p1x, p1y), Point(p2x, p2y), Scalar(255,0,0), 0, CV_AA);
		int direction = 0;
		double distanceP = distance_Pt2Line(p1, p2, p3, direction);
		measuredVal = abs(distanceP);
		cout << "distance " << distanceP * mmpp << endl;
		thirdPoint00 = getPointAtDist(returnVec, -1 * direction * 20, 3);
		thirdPoint10 = getPointAtDist(returnVec, distanceP + (direction * 20), 3);
		returnVec[1] = p2;
		thirdPoint01 = getPointAtDist(returnVec, -1 * direction * 20, 3);
		thirdPoint11 = getPointAtDist(returnVec, distanceP + (direction * 20), 3);


		//line(image, thirdPoint00, thirdPoint01, Scalar(0,0,255), 3, CV_AA);
		//line(image,thirdPoint00, thirdPoint10, Scalar(0,255,0), 3, CV_AA);
		//line(image, thirdPoint10, thirdPoint11, Scalar(0,0,255), 3, CV_AA);
		//line(image, thirdPoint01, thirdPoint11, Scalar(0,255,0), 3, CV_AA);
		//
					//--------------added for correct width ---redetecting edge points based on user inputs-----------------------------------------
		Point2f returnVecEd00[2];
		Point2f pt00temp, pt01temp;
		pt00temp = Point(0, 0);
		pt01temp = Point(0, 0);
		ptToVector(thirdPoint00, thirdPoint01, returnVecEd00);;
		int e1V0 = 0;
		for (int j = 1; j < (abs(distanceP) + 20); j++)
		{

			Point2f ptTemp;
			ptTemp = getPointAtDist(returnVecEd00, (direction * j), 3);
			if (e1V0 > 60 && imgProc.at<uchar>(ptTemp) < 60)
			{
				pt00temp = ptTemp;
			}

			e1V0 = imgProc.at<uchar>(ptTemp);
		}

		ptToVector(thirdPoint01, thirdPoint00, returnVecEd00);;
		e1V0 = 0;
		for (int j = 1; j < (abs(distanceP) + 20); j++)
		{

			Point2f ptTemp;
			ptTemp = getPointAtDist(returnVecEd00, -1 * (direction * j), 3);
			if (e1V0 > 60 && imgProc.at<uchar>(ptTemp) < 60)
			{
				pt01temp = ptTemp;
			}

			e1V0 = imgProc.at<uchar>(ptTemp);

		}
		circle(image, pt00temp, 3, Scalar(0, 0, 255), 2, 8, 0);
		circle(image, pt01temp, 3, Scalar(0, 0, 255), 2, 8, 0);
		thirdPoint00 = pt00temp;
		thirdPoint01 = pt01temp;
		ptToVector(pt00temp, pt01temp, returnVec);
		distanceP = distance_Pt2Line(pt00temp, pt01temp, p3, direction);
		thirdPoint00 = getPointAtDist(returnVec, -1 * direction * 10, 3);
		thirdPoint10 = getPointAtDist(returnVec, distanceP + (direction * 10), 3);
		returnVec[1] = pt01temp;
		thirdPoint01 = getPointAtDist(returnVec, -1 * direction * 10, 3);
		thirdPoint11 = getPointAtDist(returnVec, distanceP + (direction * 10), 3);
		// ----------------------------------------------------------

		Point2f diff;
		diff.x = abs(thirdPoint00.x - thirdPoint01.x);
		diff.y = abs(thirdPoint00.y - thirdPoint01.y);
		double  numPoints;
		float averageWidth;
		int  sumAvg = 0;
		int linecount = 0;
		int width[200];

		for (int k = 0; k < 200; k++)//init array
		{
			width[k] = 0;
		}


		Point2f returnVecEd[2];
		ptToVector(thirdPoint00, thirdPoint01, returnVecEd);

		if (diff.x > diff.y)
		{
			numPoints = diff.x;
		}
		else
		{
			numPoints = diff.y;
		}
		for (int i = 1; i < numPoints; i = i + 6)
		{
			Point2f ptStart = getPointAtDist(returnVecEd, 6, 1);
			returnVecEd[1] = ptStart;
			// int width=0;
			Point2f ptE1, ptE2 = Point(0, 0);

			int e1V, e2V;
			e1V = 255;
			e2V = 0;
			//  ptE2=getPointAtDist(returnVecEd,distanceP ,3);
			for (int j = 1; j < (abs(distanceP) + 20); j++)
			{

				Point2f ptTemp;
				ptTemp = getPointAtDist(returnVecEd, (direction * j), 3);
				if (imgProc.at<uchar>(ptTemp) > 60)
				{
					width[linecount] += 1;
				}
				if (e1V < 60 && imgProc.at<uchar>(ptTemp)>60)
				{
					ptE1 = ptTemp;
				}
				if (e2V > 60 && imgProc.at<uchar>(ptTemp) < 60)
				{
					ptE2 = ptTemp;
				}

				e1V = imgProc.at<uchar>(ptTemp);
				e2V = e1V;

				/*	if (j==(abs(distanceP)+19)) {
						line(image, ptE1, ptE2, Scalar(0,255,0), 1, CV_AA);
						}*/
			}
			linecount = linecount + 1;
			circle(image, ptE1, 1, Scalar(255, 0, 0), 2, 8, 0);
			circle(image, ptE2, 1, Scalar(0, 0, 255), 2, 8, 0);
			line(image, ptE1, ptE2, Scalar(0, 255, 0), 1, 8);
		}

		for (int k = 0; k < linecount; k++)
		{
			sumAvg = sumAvg + width[k];
			averageWidth = sumAvg / linecount;
			measuredVal = averageWidth;
		}

		// 	char T[100];
		//sprintf_s(T,"Sum=%3.2d  lineCnt=%3.2d avg=%3.2f ",sumAvg,linecount,averageWidth );
		//putText(image, T, Point(20, 120), CV_FONT_HERSHEY_COMPLEX_SMALL, 1, CV_RGB(155, 0, 0), 2, 4, false);
		//namedWindow( "circle_and_lines", WINDOW_AUTOSIZE );
		//imshow("circle_and_lines",image);
		//waitKey(0);
		return image;
	}
	void Morph(const cv::Mat& src, cv::Mat& dst, int operation, int kernel_type, int size)
	{
		cv::Point anchor = cv::Point(size, size);
		cv::Mat element = getStructuringElement(kernel_type, cv::Size(2 * size + 1, 2 * size + 1), anchor);
		morphologyEx(src, dst, operation, element, anchor);
	}	
	int profiledefect = 0;
	Mat findProfileDifferences(Mat image1, Mat template1)
	{
		Mat imgDiff;
		Mat thT;
		Mat thimg;
		threshold(image1, thimg, 30, 255, THRESH_BINARY_INV);//30
		threshold(template1, thT, 30, 255, THRESH_BINARY_INV); // 30
		cv::absdiff(thT, thimg, imgDiff);
	//	resShow("imgDiff", thimg, 0.3);
		Mat draw;
		cvtColor(image1, draw, COLOR_GRAY2BGR);
		//cv::resize(imgDiff, t, cv::Size(), 0.4, 0.4);
			//imshow("imgDiff",t);	
		//	imshow("Biggest Contour",imgDraw);
		//	imshow("Biggest Contour witout hierarchy",imgPart );
		//imwrite(path_s + "Biggest Contour.jpg", imgDraw);


		//	waitKey();

		//*********************************************************blob detection***************************************************

		Mat imgDiffCopy = imgDiff.clone();
		/*cv::resize(imgDiffCopy, t, cv::Size(), 0.4, 0.4);
		imshow("imgDiffCopy", t);*/
		Morph(imgDiffCopy, imgDiffCopy, MORPH_ERODE, MORPH_ELLIPSE, 3);
		Morph(imgDiffCopy, imgDiffCopy, MORPH_ERODE, MORPH_ELLIPSE, 3);
	//	resShow("imgDiffCopy ", imgDiffCopy, 0.3);

		//cv::resize(imgDiffCopy, t, cv::Size(), 0.4, 0.4);
		//imshow("imgDiffCopy  morph", t);
		//waitKey(5);
			//-3waitKey();
		//cv::SimpleBlobDetector detector;

		vector<vector<cv::Point> > contoursDef;
		vector<Vec4i> hierarchyDef;
		Point2f verticesDef[4];
		RotatedRect r1Def;
		int DefCntProf = 0;

		//
		findContours(imgDiffCopy, contoursDef, hierarchyDef, RETR_CCOMP, CHAIN_APPROX_NONE);

		for (int ck = 0; ck < contoursDef.size(); ck++)
		{
			r1Def = minAreaRect(contoursDef[ck]);
			double w = max(r1Def.size.width, r1Def.size.height);
			double h = min(r1Def.size.width, r1Def.size.height);

			if ((w / h > 20) && h < 10)
			{
				DefCntProf = DefCntProf;
			}
			else if (contourArea(contoursDef[ck]) > 1400)
			{
				drawContours(draw, contoursDef, ck, Scalar(0, 0, 255), -1);
				DefCntProf = DefCntProf + 1;
			}

		}
		profiledefect = DefCntProf;
		cout << "profileDefect ::" << profiledefect << endl;
	//	resShow("profileDiff", draw, 1);
		return draw;
	}
	Mat getConts(Mat image0)
	{
		Rect r_roi = Rect(10, (0.18 * image0.rows), image0.cols - 20, image0.rows - (0.36 * image0.rows));
		Mat image;
		image = image0(r_roi);
		Mat gray;
		Mat draw;


		if (image.channels() > 1)
		{
			cvtColor(image, gray, COLOR_BGR2GRAY);
			draw = image.clone();
		}
		else
		{
			cvtColor(image, draw, COLOR_GRAY2BGR);
			gray = image.clone();
		}
		//-----------------find subpix conts___________
		int low = 20;//40;
		int high = 80; //100
		double alpha = 1.0;
		int mode = 1;
		vector<Contour> contours;
		vector<Contour> uniqueContours;

		vector<Vec4i> hierarchy;
		int64 t0 = getCPUTickCount();
		EdgesSubPix(gray, alpha, low, high, contours, hierarchy, mode);
		int64 t1 = getCPUTickCount();
		//cout << "execution time is " << (t1 - t0) / (double)getTickFrequency() << " seconds" << endl;
		//cout << "contours size:" << contours.size() << endl;
		/*vector < vector < cv::Point2f > > contours2;
		for (int i=0;i<contours[0].points.size();i++)
		{
		contours2[0].push_back(cv::Point2f(contours[0].points[i].x,contours[0].points[i].y));
		}
		cout<<"cont point size()"<<contours2[0].size()<<endl;
		drawContours(imageC,contours2 ,0 , Scalar(0, 255, 0), 5, 8);
		waitKey();*/
		vector<vector<cv::Point2f> > conts;

		vector<RotatedRect > rr_finalConts;

		vector<RotatedRect > finalContsWoBig;


		Mat rr;
		for (int i = 0; i < contours.size(); i++)
		{
			RotatedRect  brect = minAreaRect(contours[i].points);

			cout << "center:" << i << ":" << brect.center << endl;
			//cout<<"height_"<<i<<"=="<<brect.size.height*0.25<<endl;
				//cout<<"width_"<<i<<"=="<<brect.size.width*0.25<<endl;
			float heightR = max(brect.size.height, brect.size.width);
			float widthR = min(brect.size.height, brect.size.width);
			if (i == 0)
			{
				rr_finalConts.push_back(brect);
				uniqueContours.push_back(contours[i]);

				//conts.push_back(contours[i].points);
				/*circle(imageC,brect.center,5,Scalar(255,255,0),4);

					putText(imageC,cv::format("%3.2f",0.25*max(brect.size.height,brect.size.width)),brect.center,CV_FONT_HERSHEY_COMPLEX,1,Scalar(0,0,255));
					putText(imageC,cv::format("%3.2f",0.25*min(brect.size.height,brect.size.width)),Point(brect.center.x,brect.center.y+60),CV_FONT_HERSHEY_COMPLEX,1,Scalar(0,0,255));*/
			}
			//resize(imageC,rr,Size(),0.35,0.35);
	//	imshow("rr",rr);
	//	waitKey();
			bool unique = true;
			for (int j = 0; j < rr_finalConts.size(); j++)
			{
				if ((abs(rr_finalConts[j].center.x - brect.center.x) < 5) && (abs(rr_finalConts[j].center.y - brect.center.y) < 5))
				{
					float height = max(rr_finalConts[j].size.height, rr_finalConts[j].size.width);
					float width = min(rr_finalConts[j].size.height, rr_finalConts[j].size.width);
					float hdiff = abs(height - heightR);
					float wdiff = abs(width - widthR);
					if (hdiff < 8 && wdiff < 8)
					{
						if (height > heightR && width > widthR)
						{
							rr_finalConts.at(j) = brect;

						}
						unique = false;
						break;
					}




				}


			}

			if (unique == true)
			{
				rr_finalConts.push_back(brect);
				uniqueContours.push_back(contours[i]);
			}

		}

		cout << "finalConts size:" << rr_finalConts.size() << endl;
		cout << "UniqueConts size:" << uniqueContours.size() << endl;
		int bigIdx = -1;
		float maxar = 0;
		vector <Point2f> vecStd_centers;
		vector <Size2f> vecStd_rects;

		for (int i = 0; i < rr_finalConts.size(); i++)
		{
			//rectangle(draw, finalConts[i].boundingRect(), Scalar(200, 0, 0), 8);
			Point2f verts[4];
			rr_finalConts[i].points(verts);
			double contWidth, contHeight;
			Point SaveContour_Center = rr_finalConts[i].center;
			float widthC = max(rr_finalConts[i].size.height, rr_finalConts[i].size.width);   //detected current
			float heightC = min(rr_finalConts[i].size.height, rr_finalConts[i].size.width);


			circle(draw, SaveContour_Center * 4, 6, Scalar(0, 255, 0), 3);

			float ar = (rr_finalConts[i].size.height * rr_finalConts[i].size.width);
			if ((rr_finalConts[i].size.height * rr_finalConts[i].size.width) > maxar)
			{
				bigIdx = i;
				maxar = (rr_finalConts[i].size.height * rr_finalConts[i].size.width);
			}
			Rect r = rr_finalConts[i].boundingRect();
			if (ar > 100 && r.height < (0.95 * gray.rows) && r.width < (0.95 * image.cols))
			{
				for (int j = 0; j < uniqueContours[i].points.size(); j++)
				{
					line(draw, uniqueContours[i].points[j], uniqueContours[i].points[(j + 1) % uniqueContours[i].points.size()], Scalar(0, 230, 100), 1);
				}
				cout << "contSize::" << heightC << "  width" << widthC << endl;
				putText(draw, cv::format("%4.2f", (heightC * mmpp)), cv::Point(SaveContour_Center.x + 10, SaveContour_Center.y + 20), FONT_HERSHEY_SIMPLEX, 1, Scalar(0, 180, 0), 1);
				putText(draw, cv::format("%4.2f", (widthC * mmpp)), cv::Point(SaveContour_Center.x + 10, SaveContour_Center.y + 60), FONT_HERSHEY_SIMPLEX, 1, Scalar(0, 180, 0), 1);
				vecStd_centers.push_back(rr_finalConts[i].center);
				vecStd_rects.push_back(Size(widthC, heightC));


			}

			//for (int i = 0; i < 4; i++)
			//{
			//	line(draw, verts[i], verts[(i + 1) % 4], Scalar(200, 0, 0), 2);
			//}
		}
		SmlContourCenter_writeXML(vecStd_centers);
		SmlContourSize_writeXML(vecStd_rects);
		cout << "conts final saved::" << (vecStd_centers.size()) << endl;
		putText(draw, " total shapes:" + to_string(vecStd_centers.size()), cv::Point(200, 200), FONT_HERSHEY_SIMPLEX, 1, Scalar(0, 0, 180), 1);
		//test read ---
		vector <Point2f> vecRead_centers;
		vector <Size2f> vecRead_rects;
		SmlContourCenter_readXML(vecRead_centers);
		SmlContourSize_readXML(vecRead_rects);
		cout << "readCont size centers::" << vecRead_centers.size() << endl;
		cout << "readCont size rects::" << vecRead_rects.size() << endl;


		//
		if (bigIdx != -1)
		{
			cout << "biggest contour found :: index ::" << bigIdx << endl;
			for (int j = 0; j < uniqueContours[bigIdx].points.size(); j++)
			{
				line(draw, uniqueContours[bigIdx].points[j], uniqueContours[bigIdx].points[(j + 1) % uniqueContours[bigIdx].points.size()], Scalar(200, 0, 0), 4);
			}

			vector <Point2f> lf;
			vector<Point2f> rh;
			Point2f verts[4];
			rr_finalConts[bigIdx].points(verts);
			for (int i = 0; i < 4; i++)
			{
				//line(draw, verts[i], verts[(i + 1) % 4], Scalar(200, 0, 0), 2);

				if (verts[i].x < rr_finalConts[bigIdx].center.x)
					lf.push_back(verts[i]);
				else
					rh.push_back(verts[i]);
			}
			Point2f verts_o[4];
			if (lf.size() == 2 && rh.size() == 2)
			{
				if (lf[0].y < lf[1].y)
				{
					verts_o[0] = lf[0];
					verts_o[2] = lf[1];
				}
				else
				{
					verts_o[0] = lf[1];
					verts_o[2] = lf[0];
				}

				if (rh[0].y < rh[1].y)
				{
					verts_o[1] = rh[0];
					verts_o[3] = rh[1];
				}
				else
				{
					verts_o[1] = rh[1];
					verts_o[3] = rh[0];
				}
			}
			//circle(draw, verts_o[0], 6, Scalar(0, 0, 255), -1);
			//circle(draw, verts_o[1], 6, Scalar(0, 255,0), -1);
			//circle(draw, verts_o[2], 6, Scalar(255,0, 0), -1);
			//circle(draw, verts_o[3], 6, Scalar(0, 255, 200), -1);
			Mat straightTemplate = checkUniformWithEdge(gray, verts_o[0], verts_o[1], (int)distancePoint(verts_o[1], verts_o[3]));
			resShow("template", straightTemplate);
            imwrite("template.bmp", straightTemplate);
			Point2f center;
			RotatedRect brect = minAreaRect(uniqueContours[bigIdx].points);
			center = brect.center;

			for (int i = 0; i < uniqueContours.size(); i++) {
				vector<Point2f> distance;
				Point2f point;
				RotatedRect rect = minAreaRect(uniqueContours[i].points);
				point.x = center.x - rect.center.x;
				point.y = center.y - rect.center.y;
				centerDist.push_back(point);
			}

		}
		else
		{
			cout << "biggest cont not found" << endl;
		}

		//---------------------------------------------

		imwrite("foundConts.bmp", draw);

	//	resShow("imageROI", draw, 0.5);
		return draw;
	}
	void drawContours_1(Mat image, vector<Contour> xyz, int id = -1) {
		cout << "Enterend Drawing contours" << endl;
		Mat draw = image.clone();
		if (id == -1) {
			for (size_t x = 0; x < xyz.size(); x++) {
				
				RotatedRect brect = minAreaRect(xyz[x].points);
				int width = max(brect.size.height, brect.size.width);
				int height = min(brect.size.height, brect.size.width);
				for (size_t y = 0; y < xyz[x].points.size(); y+= 3) {
					circle(draw, xyz[x].points[y], 3, Scalar(0, 255, 255), -1);
				}
				cout << "width " << width << endl;
				cout << "height " << height << endl;
			}
		}
		else {
			for (size_t i = 0; i < xyz[id].points.size(); i+= 3) {
				circle(draw, xyz[id].points[i], 3, Scalar(0, 255, 255), -1);
			}
		}
	//	resShow("contours", draw, 0.3);

	}
	int maxContourId(vector<Contour> contours) {
		int id = -1;
		float maxAr{};
		for (size_t i = 0; i < contours.size(); i++) {
			float Area = contourArea(contours[i].points);
			if (Area > maxAr) {
				maxAr = Area;
				id = i;
			}

		}
		return id;
	}
	//testing

	void displayAngle(Mat& imageC, vector<Point2f>& closestPoint, RotatedRect brect, float defaultAngle, float angleTol = 0.5) {
		

		float angle_1 = getAngle(closestPoint[0], closestPoint[1], closestPoint[2], closestPoint[3]);
		cout << "angle_1 " << angle_1 << endl;
		 
		Point2f center = brect.center;
		if (abs(angle_1 - defaultAngle) < angleTol) {

		     if (closestPoint[2].y < center.y) {
		           putText(imageC, cv::format("A=%4.2f", angle_1), cv::Point(closestPoint[2].x + 20, closestPoint[2].y - 50), FONT_HERSHEY_SIMPLEX, 2, Scalar(0, 255, 0), 3);
		     }else
		     	   putText(imageC, cv::format("A=%4.2f", angle_1), cv::Point(closestPoint[2].x + 20, closestPoint[2].y + 120), FONT_HERSHEY_SIMPLEX, 2, Scalar(0, 255, 0), 3);
		}
		else {
			if (closestPoint[2].y < center.y) {
				putText(imageC, cv::format("A=%4.2f", angle_1), cv::Point(closestPoint[2].x + 20, closestPoint[2].y - 50), FONT_HERSHEY_SIMPLEX, 2, Scalar(0, 0, 255), 3);
			}
			else
				putText(imageC, cv::format("A=%4.2f", angle_1), cv::Point(closestPoint[2].x + 20, closestPoint[2].y + 120), FONT_HERSHEY_SIMPLEX, 2, Scalar(0, 0, 255), 3);
		}

	
	}

	void displayCornerPoints(Mat& imageS, Point2f cornerPoint_1, Point2f cornerPoint_2) {
		float dist = distancePoint(cornerPoint_1, cornerPoint_2);
		cout << "Dist " << dist * mmpp << endl;
		putText(imageS, cv::format("D:%4.2", (dist * mmpp)), cornerPoint_1, FONT_HERSHEY_COMPLEX, 2, Scalar(255, 0, 0), 2);
		circle(imageS, cornerPoint_1, 10, Scalar(255, 0, 0), -1);
		circle(imageS, cornerPoint_2, 10, Scalar(255, 0, 0), -1);
		resShow("imageS", imageS);
	}
	
	//void displayDistance(Mat& imageS, RotatedRect center, vector<Point2f>& closestPoint) {
	//	float dist = distance_Pt2Line(closestPoint[0], closestPoint[1], closestPoint[2]);
	//	line(imageS, closestPoint[2], Point(closestPoint[2].x, closestPoint[2].y + dist), Scalar(0, 0, 255), 3);
	//	cout << "Dist " << dist << endl;
	//	putText(imageS, cv::format("D=%4.2f", abs(dist * mmpp)), cv::Point(closestPoint[2].x + 20, closestPoint[2].y + 50), FONT_HERSHEY_SIMPLEX, 2, Scalar(0, 180, 0), 3);
	//}
	void displayDistance(Mat& imageS, Point2f cornerPoint1, Point2f cornerPoint2) {

		float dist = distancePoint(cornerPoint1, cornerPoint2);
	//	line(draw, closestPoint[2], Point(closestPoint[2].x, closestPoint[2].y + dist), Scalar(0, 0, 255), 3);
		cout << "Dist " << dist << endl;
		putText(imageS, cv::format("D=%4.2f", (dist * mmpp)), cv::Point(cornerPoint1.x + 50, cornerPoint1.y + 150), FONT_HERSHEY_SIMPLEX, 2, Scalar(255, 0, 0), 3);
		circle(imageS, cornerPoint1, 4, Scalar(255, 0, 0), -1);
		circle(imageS, cornerPoint2, 4, Scalar(255, 0, 0), -1);
	}
	void func(Mat &imageS,vector<Point2f>& usersPoints, vector<Point2f> &vecPoints, RotatedRect brect,float defaultAngle, Point2f &cornerPoint, int flag = 0) {
		
		vector<Point2f> closestPoint;
		for (size_t i = 0; i < usersPoints.size(); i++) {
			Point2f temp = Point2f(brect.boundingRect2f().tl().x + usersPoints[i].x, brect.boundingRect2f().tl().y + usersPoints[i].y);
			Point2f temp_1 = getClosestPoint(vecPoints, temp);
			circle(imageS, temp_1, 10, Scalar(255, 0, 0), -1);
			closestPoint.push_back(temp_1);
		}
		
		   if (flag == 1) {
		   
		   	displayAngle(imageS, closestPoint, brect, defaultAngle);
		   	cornerPoint = intersection_1(closestPoint[0], closestPoint[1], closestPoint[2], closestPoint[3]);
		    
		   }
		   else if (flag == 2) {
			   float dist_1 = abs(distance_Pt2Line(closestPoint[0], closestPoint[1], closestPoint[2]));
			   cout << "distance " << dist_1 << endl;
			   putText(imageS, cv::format("D=%4.2f", (dist_1 * mmpp)), cv::Point(closestPoint[0].x + 50, closestPoint[0].y - 50), FONT_HERSHEY_SIMPLEX, 2, Scalar(0, 0, 255), 3);
			   line(imageS, closestPoint[2], Point(closestPoint[2].x, closestPoint[2].y - dist_1), Scalar(0, 0, 0), 3);

		   }
		  
		

		    	
		
		usersPoints.clear();
	}


		





	Mat getContsMatch(Mat image0)
	{
		Rect r_roi = Rect(10, (0.18 * image0.rows), image0.cols - 20, image0.rows - (0.36 * image0.rows));
		Mat image, imageBig;
		imageBig = image0(r_roi);
		image = Mat::zeros(Size(1.6 * imageBig.cols, 1.6 * imageBig.rows), CV_8UC1);
		image = 255 - image;
		cvtColor(image, image, COLOR_GRAY2BGR);
		Rect c = Rect(0.3 * imageBig.cols, 0.3 * imageBig.rows, imageBig.cols, imageBig.rows);
		imageBig.copyTo(image(c));
		Mat gray;
		Mat draw;

		if (image.channels() > 1)
		{
			cvtColor(image, gray, COLOR_BGR2GRAY);
			draw = image.clone();
		}
		else
		{
			cvtColor(image, draw, COLOR_GRAY2BGR);
			gray = image.clone();
		}
		Mat templateStd = imread("template.bmp", 0);
		//resShow("templateRead", templateStd);
		float minV;
		cv::Point minLoc;
		float angle;
		Mat thT, thImg;
		threshold(templateStd, thT, 30, 255, THRESH_BINARY_INV);// 30
		//resShow("thT", thT, 0.3);
		threshold(gray, thImg, 30, 255, THRESH_BINARY_INV); // 30
		//resShow("thImg", thImg, 0.3);
		
		


		
		Mat rotatedImg = getRotation15AugRotateTempl2img(thImg, thT, gray, draw, &minV, &minLoc, &angle);
		
		Rect rCrop = Rect(minLoc, templateStd.size());
		Mat cropped = rotatedImg(rCrop);
		Mat profileDiffImg=findProfileDifferences(cropped, templateStd);
		 imageC = rotatedImg.clone();
		cvtColor(imageC, imageC, COLOR_GRAY2BGR);
		//resShow("imageCropped", cropped);
		//-----------------find subpix conts___________
		int low = 30;//40;
		int high = 90; //100
		double alpha = 1.0;
		int mode = 1;
		
		vector<Contour> contours;
		vector<Contour> uniqueContours;

		vector<Vec4i> hierarchy;
		int64 t0 = getCPUTickCount();
		imwrite("rotated.bmp", rotatedImg);
		EdgesSubPix(rotatedImg, alpha, low, high, contours, hierarchy, mode);
		profileDiffImg.copyTo(draw(rCrop));
		int64 t1 = getCPUTickCount();

		//cout << "execution time is " << (t1 - t0) / (double)getTickFrequency() << " seconds" << endl;
		//cout << "contours size:" << contours.size() << endl;
		/*vector < vector < cv::Point2f > > contours2;
		for (int i=0;i<contours[0].points.size();i++)
		{
		contours2[0].push_back(cv::Point2f(contours[0].points[i].x,contours[0].points[i].y));
		}
		cout<<"cont point size()"<<contours2[0].size()<<endl;
		drawContours(imageC,contours2 ,0 , Scalar(0, 255, 0), 5, 8);
		waitKey();*/
		

		vector<vector<cv::Point2f> > conts;

		vector<RotatedRect > rr_finalConts;

		vector<RotatedRect > finalContsWoBig;
		for (size_t i = 0; i < contours.size(); i++) {
			RotatedRect brect = minAreaRect(contours[i].points);
			float dist = distancePoint(Point(rotatedImg.cols / 2, rotatedImg.rows / 2), brect.center);
			if (dist > 1000) {
				contours.erase(contours.begin() + i);
				i--;
			}
			/*float dist = distancePoint(Point(rotatedImg.cols / 2, rotatedImg.rows / 2), brect.center);
			if (dist > 1600) {
				contours.erase(contours.begin() + i);
				i--;
			}*/
		}

		Mat rr;
		for (int i = 0; i < contours.size(); i++)
		{
			RotatedRect  brect = minAreaRect(contours[i].points);

			cout << "center:" << i << ":" << brect.center << endl;
			//cout<<"height_"<<i<<"=="<<brect.size.height*0.25<<endl;
				//cout<<"width_"<<i<<"=="<<brect.size.width*0.25<<endl;
			float heightR = max(brect.size.height, brect.size.width);
			float widthR = min(brect.size.height, brect.size.width);
			if (i == 0)
			{
				rr_finalConts.push_back(brect);
				uniqueContours.push_back(contours[i]);

				//conts.push_back(contours[i].points);
				/*circle(imageC,brect.center,5,Scalar(255,255,0),4);

					putText(imageC,cv::format("%3.2f",0.25*max(brect.size.height,brect.size.width)),brect.center,CV_FONT_HERSHEY_COMPLEX,1,Scalar(0,0,255));
					putText(imageC,cv::format("%3.2f",0.25*min(brect.size.height,brect.size.width)),Point(brect.center.x,brect.center.y+60),CV_FONT_HERSHEY_COMPLEX,1,Scalar(0,0,255));*/
			}
			//resize(imageC,rr,Size(),0.35,0.35);
	//	imshow("rr",rr);
	//	waitKey();
			bool unique = true;
			for (int j = 0; j < rr_finalConts.size(); j++)
			{
				if ((abs(rr_finalConts[j].center.x - brect.center.x) < 5) && (abs(rr_finalConts[j].center.y - brect.center.y) < 5))
				{
					float height = max(rr_finalConts[j].size.height, rr_finalConts[j].size.width);
					float width = min(rr_finalConts[j].size.height, rr_finalConts[j].size.width);
					float hdiff = abs(height - heightR);
					float wdiff = abs(width - widthR);
					if (hdiff < 8 && wdiff < 8)
					{
						if (height > heightR && width > widthR)
						{
							rr_finalConts.at(j) = brect;

						}
						unique = false;
						break;
					}




				}


			}

			if (unique == true)
			{
				rr_finalConts.push_back(brect);
				uniqueContours.push_back(contours[i]);
			}

		}
		int maxId = maxContourId(uniqueContours);

		cout << "Unique Contours size " << uniqueContours.size() << endl;
		vector<Point2f> vecPoints;
		for (auto row = uniqueContours.begin(); row != uniqueContours.end(); row++) {
			for (auto col = row->points.begin(); col != row->points.end(); col++) {
				vecPoints.push_back(*col);
			}
		}
		  
			
			//drawContours_1(imageC, contours, maxId);
		RotatedRect largRect = minAreaRect(uniqueContours[maxId].points);
		
		
		float radii{};
		//cout << "angle " << getAngle(closestPoint[0], closestPoint[1], closestPoint[2], closestPoint[3]) << endl;
		//findArc_radii_test(rotatedImg, draw, closestPoint[0], closestPoint[1], closestPoint[2], radii);
		cout << "radii " << radii * mmpp << endl;
		
		usersPoints.clear();
		
		usersPoints.push_back(Point2f(32,31));
		usersPoints.push_back(Point2f(157, 157));
		usersPoints.push_back(Point2f(70, 1));
		usersPoints.push_back(Point2f(150,1));
		Point2f corner_1{};
		func(draw, usersPoints, vecPoints, largRect, 45,corner_1,1);
		circle(draw, corner_1, 10, Scalar(255, 0, 0), -1);
		
		usersPoints.push_back(Point2f(32, 31));
		usersPoints.push_back(Point2f(298, 297));
		usersPoints.push_back(Point2f(422, 397));
		usersPoints.push_back(Point2f(499, 397));
		Point2f corner_2{};
		func(draw, usersPoints, vecPoints, largRect, 45, corner_2, 1);
		circle(draw, corner_2, 10, Scalar(255, 0, 0), -1);
		usersPoints.push_back(Point2f(422, 397));
		usersPoints.push_back(Point2f(499, 397));
		usersPoints.push_back(Point2f(1337, 333));
		usersPoints.push_back(Point2f(1400, 274));
		Point2f corner_7{};
		func(draw, usersPoints, vecPoints, largRect, 45, corner_7, 1);
		circle(draw, corner_7, 10, Scalar(255, 0, 0), -1);

		cout << "corner 2 " << corner_2 << endl;
		float dist = distancePoint(corner_2, corner_7);

		cout << "Dist " << dist << endl;
		putText(draw, cv::format("D=%4.2f", (dist* mmpp)), cv::Point(corner_7.x - 50, corner_7.y + 150), FONT_HERSHEY_SIMPLEX, 2, Scalar(255, 0, 0), 3);
		cout << "corner_3 " << corner_7 << endl;
		
		usersPoints.push_back(Point2f(1337, 333));
		usersPoints.push_back(Point2f(1400, 274));
		usersPoints.push_back(Point2f(1463, 266));
		usersPoints.push_back(Point2f(1514, 319));
		Point2f corner_3{};
		func(draw, usersPoints, vecPoints, largRect, 90, corner_3, 1);
		
		circle(draw, corner_3, 10, Scalar(255, 0, 0), -1);

		usersPoints.push_back(Point2f(2293, 396));
		usersPoints.push_back(Point2f(2364, 396));
		usersPoints.push_back(Point2f(2416, 367));
		usersPoints.push_back(Point2f(2471, 313));
		Point2f corner_4{};
		func(draw, usersPoints, vecPoints, largRect, 45, corner_4, 1);
		cout << "corner_4 " << corner_4 << endl;
		circle(draw, corner_4, 10, Scalar(255, 0, 0), -1);

		usersPoints.push_back(Point2f(2416, 367));
		usersPoints.push_back(Point2f(2471, 313));
		usersPoints.push_back(Point2f(2620, 0));
		usersPoints.push_back(Point2f(2755, 0));
		Point2f corner_5{};
		func(draw, usersPoints, vecPoints, largRect, 45, corner_5, 1);
		cout << "corner_5 " << corner_5 << endl;
		circle(draw, corner_5, 10, Scalar(255, 0, 0), -1);
		dist = distancePoint(corner_4, corner_5);
	
		cout << "Dist " << dist << endl;
		putText(draw, cv::format("D=%4.2f", (dist* mmpp)), cv::Point(corner_5.x + 50, corner_5.y + 150), FONT_HERSHEY_SIMPLEX, 2, Scalar(255, 0, 0), 3);
		usersPoints.push_back(Point2f(345, 3));
		usersPoints.push_back(Point2f(632, 3));
		usersPoints.push_back(Point2f(547, 396));
		func(draw, usersPoints, vecPoints, largRect, 45, corner_5, 2);
		/*usersPoints.push_back(Point2f(1434, 239));
		usersPoints.push_back(Point2f(1515, 318));
		usersPoints.push_back(Point2f(2293, 396));
		usersPoints.push_back(Point2f(2364, 396));
		Point2f corner_6{};
		func(draw, usersPoints, vecPoints, largRect, 45, corner_6, 1);
				
		usersPoints.push_back(Point2f(2293, 396));
		usersPoints.push_back(Point2f(2364, 396));
		usersPoints.push_back(Point2f(2416, 367));
		usersPoints.push_back(Point2f(2471, 313));
		Point2f corner_7{};
		func(draw, usersPoints, vecPoints, largRect, 45, corner_7, 1);*/
		resShow("draw", draw);

	
		cout << "finalConts size:" << rr_finalConts.size() << endl;
		cout << "UniqueConts size:" << uniqueContours.size() << endl;
		float width = max(largRect.size.width, largRect.size.height);
		//test read ---
		vector <Point2f> SmlContCenter; //read
		vector <Size2f> SmlContSize;

		vector <Point2f> SaveContour_Center;//current
		vector <Size2f> currCSize;
		SmlContourCenter_readXML(SmlContCenter);
		SmlContourSize_readXML(SmlContSize);
		cout << "readCont size centers::" << SmlContCenter.size() << endl;
		cout << "readCont size rects::" << SmlContSize.size() << endl;
		float maxar = 0;
		int bigIdx = -1;

		for (int i = 0; i < rr_finalConts.size(); i++)
		{

			float ar = (rr_finalConts[i].size.height * rr_finalConts[i].size.width);
			if ((rr_finalConts[i].size.height * rr_finalConts[i].size.width) > maxar)
			{
				bigIdx = i;
				maxar = (rr_finalConts[i].size.height * rr_finalConts[i].size.width);
			}
			Rect r = rr_finalConts[i].boundingRect();
			if (ar > 100 && r.height < (0.95 * gray.rows) && r.width < (0.95 * image.cols))
			{
				float widthC = max(rr_finalConts[i].size.height, rr_finalConts[i].size.width);   //detected current
				float heightC = min(rr_finalConts[i].size.height, rr_finalConts[i].size.width);
				SaveContour_Center.push_back(rr_finalConts[i].center);
				currCSize.push_back(Size2f(widthC, heightC));


			}


			//rectangle(draw, finalConts[i].boundingRect(), Scalar(200, 0, 0), 8);
			//Point2f verts[4];
			//rr_finalConts[i].points(verts);
			//double contWidth, contHeight;
			//Point SaveContour_Center = rr_finalConts[i].center;
			//float widthC = max(rr_finalConts[i].size.height, rr_finalConts[i].size.width);   //detected current
			//float heightC = min(rr_finalConts[i].size.height, rr_finalConts[i].size.width);

			//putText(draw, cv::format("%4.2f", (heightC)), cv::Point(SaveContour_Center.x + 10, SaveContour_Center.y + 20), FONT_HERSHEY_SIMPLEX, 1, Scalar(0, 180, 0), 1);
			//putText(draw, cv::format("%4.2f", (widthC)), cv::Point(SaveContour_Center.x + 10, SaveContour_Center.y + 60), FONT_HERSHEY_SIMPLEX, 1, Scalar(0, 180, 0), 1);
			//circle(draw, SaveContour_Center * 4, 6, Scalar(0, 255, 0), 3);
			//for (int j = 0; j < uniqueContours[i].points.size(); j++)
			//{
			//	line(draw, uniqueContours[i].points[j], uniqueContours[i].points[(j + 1) % uniqueContours[i].points.size()], Scalar(0, 230, 100), 1);
			//}

			//for (int i = 0; i < 4; i++)
			//{
			//	line(draw, verts[i], verts[(i + 1) % 4], Scalar(200, 0, 0), 2);
			//}
		}
		vector <int> flagsCurr(rr_finalConts.size(),0);
		vector <int> flagsStd(SmlContSize.size(),0);
		vector<int> pointFlag(centerDist.size(), 0);
		if (bigIdx != -1)
		{
			//vector<Point> biggest;
			//Point2f vts[4];
            		

			for (int i = 0; i < rr_finalConts.size(); i++) //saved conts
			{

				float ar = (rr_finalConts[i].size.height * rr_finalConts[i].size.width);
				Rect r = rr_finalConts[i].boundingRect();
				int resp = pointPolygonTest(uniqueContours[bigIdx].points, rr_finalConts[i].center, false);

					if (resp <= 0)
					{
						flagsCurr[i] = 1;
						continue;
					}

				if (ar > 100 && r.height < (0.95 * gray.rows) && r.width < (0.95 * image.cols))
				
				{
					float widthC = 0;
					float heightC = 0;
					widthC = max(rr_finalConts[i].size.height, rr_finalConts[i].size.width) * mmpp;   //detected current
					heightC = min(rr_finalConts[i].size.height, rr_finalConts[i].size.width) * mmpp;
					Point SaveContour_Center = rr_finalConts[i].center;
					bool found = false;
					bool presentInside{ false };
					RotatedRect brect = minAreaRect(uniqueContours[bigIdx].points);
					Point2f largeContourCenter = brect.center;
					for (int j = 0; j < SmlContSize.size(); j++) //detected conts
					{
						cout << "height * mmpp " << heightC << endl;
						cout << "width * mmpp " << widthC << endl;
						if ((abs(widthC - (SmlContSize[j].width) * mmpp) < sizeTol) && (abs(heightC - (SmlContSize[j].height) * mmpp) < sizeTol) && flagsStd[j]!=1)
						{
							flagsCurr[i] = 1;
							found = true;
							flagsStd[j] = 1;
						}
						for (int i = 0; i < uniqueContours.size(); i++) {
							RotatedRect rect = minAreaRect(uniqueContours[bigIdx].points);
							Point2f point;
							bool present{ false };
							point.x = largeContourCenter.x - rect.center.x;
							point.y = largeContourCenter.y - rect.center.y;
							present = does_exist(centerDist, point);
							presentInside = present;
						}
						if (presentInside && found) {




							for (int k = 0; k < uniqueContours[i].points.size(); k++)
							{
								line(draw, uniqueContours[i].points[k], uniqueContours[i].points[(k + 1) % uniqueContours[i].points.size()], Scalar(0, 230, 0), 3);
						
							}
							if (abs(heightC - widthC) < 0.1)
							{
								putText(draw, cv::format("D=%4.2f", (heightC)), cv::Point(SaveContour_Center.x + 20, SaveContour_Center.y - 20), FONT_HERSHEY_SIMPLEX, 2, Scalar(0, 180, 0), 3);
								if (rr_finalConts[i].boundingRect().tl().x < (draw.cols / 2)) {
									Point2f center = rr_finalConts[i].center;

									circle(draw, Point(center.x, corner_2.y), 10, Scalar(255, 0, 0), -1);
									
									putText(draw, cv::format("D=%4.2f", (abs(corner_2.x - center.x) * mmpp)), cv::Point(corner_2.x + 50, corner_2.y + 250), FONT_HERSHEY_SIMPLEX, 2, Scalar(255, 0, 0), 3);
								}
								if (rr_finalConts[i].boundingRect().tl().x > (draw.cols/ 2)) {
									
									Point2f center = rr_finalConts[i].center;
									usersPoints.clear();
									vector<Point2f> closestPoint_1;
									usersPoints.push_back(Point2f(1923,397));
									usersPoints.push_back(Point2f(2177,395));
									for (int i = 0; i < usersPoints.size(); i++) {
										Point2f temp = Point2f(largRect.boundingRect2f().tl().x + usersPoints[i].x, largRect.boundingRect2f().tl().y + usersPoints[i].y);
										Point2f temp_1 = getClosestPoint(vecPoints, temp);
										circle(draw, temp_1, 10, Scalar(0, 0, 255), -1);
										closestPoint_1.push_back(temp_1);
									}
									closestPoint_1.push_back(Point2f(center));
									circle(draw, center, 10, Scalar(0, 0, 255), -1);
									dist = distance_Pt2Line(closestPoint_1[0], closestPoint_1[1], closestPoint_1[2]);
									line(draw, closestPoint_1[2], Point(closestPoint_1[2].x, closestPoint_1[2].y + dist), Scalar(0, 0, 255), 3);

									putText(draw, cv::format("D=%4.2f", abs(dist* mmpp)), cv::Point(closestPoint_1[0].x + 50, closestPoint_1[0].y + 50), FONT_HERSHEY_SIMPLEX, 2, Scalar(0, 0, 255), 3);

								}
							}
							else
							{

								
								if (heightC > 100 && widthC > 200) {
									putText(draw, cv::format("H=%4.2f", (heightC)), cv::Point(r.x - 80, SaveContour_Center.y - 200), FONT_HERSHEY_SIMPLEX, 2, Scalar(0, 180, 0), 3);
									putText(draw, cv::format("W=%4.2f", (widthC)), cv::Point(r.x - 80, SaveContour_Center.y -140), FONT_HERSHEY_SIMPLEX, 2, Scalar(0, 180, 0), 3);
								}
								else {
									putText(draw, cv::format("H=%4.2f", (heightC)), cv::Point(SaveContour_Center.x + 20, SaveContour_Center.y + 20), FONT_HERSHEY_SIMPLEX, 2, Scalar(0, 180, 0), 3);
									putText(draw, cv::format("W=%4.2f", (widthC)), cv::Point(SaveContour_Center.x + 20, SaveContour_Center.y + 80), FONT_HERSHEY_SIMPLEX, 2, Scalar(0, 180, 0), 3);
								}
							}
							break;
						}
							
						cout << "checking centers" << endl;

					}

					if (found == false)
					{
						flagsCurr[i] = 0;
						for (int k = 0; k < uniqueContours[i].points.size(); k++)
						{
							line(draw, uniqueContours[i].points[k], uniqueContours[i].points[(k + 1) % uniqueContours[i].points.size()], Scalar(0, 0, 250), 3);
							
						}
						if (abs(heightC - widthC) < 0.1)
						{
							putText(draw, cv::format("D=%4.2f", (heightC)), cv::Point(SaveContour_Center.x + 20, SaveContour_Center.y - 20), FONT_HERSHEY_SIMPLEX, 2, Scalar(0, 0,255), 3);
						}
						else
						{
							if (heightC > 100 && widthC > 200) {
								putText(draw, cv::format("H=%4.2f", (heightC)), cv::Point(r.x - 80, SaveContour_Center.y - 200), FONT_HERSHEY_SIMPLEX, 2, Scalar(0, 0, 255), 3);
								putText(draw, cv::format("W=%4.2f", (widthC)), cv::Point(r.x - 80, SaveContour_Center.y - 140), FONT_HERSHEY_SIMPLEX, 2, Scalar(0, 0, 255), 3);
							}
							else {
								putText(draw, cv::format("H=%4.2f", (heightC)), cv::Point(SaveContour_Center.x + 20, SaveContour_Center.y + 20), FONT_HERSHEY_SIMPLEX, 2, Scalar(0, 0, 255), 3);
								putText(draw, cv::format("W=%4.2f", (widthC)), cv::Point(SaveContour_Center.x + 20, SaveContour_Center.y + 80), FONT_HERSHEY_SIMPLEX, 2, Scalar(0, 0, 255), 3);
							}
						}
					}

				}
				//	circle(Final_Result_Mat, SmlContCenter.at(i), 6, Scalar(0, 255, 255), 3);
			}
			
			
		}
		else
		{
			putText(draw, "Biggest Cont missing", Point(100, 200), FONT_HERSHEY_SIMPLEX, 1, Scalar(0, 140, 255), 1);//-0.87
			//resShow("imageROI", draw, 0.3);
			usersPoints.clear();
			
			return draw;
		}
		extraCnt = 0;
		missCnt = 0;
		for (int k = 0; k < flagsCurr.size(); k++)
		{
			if (flagsCurr[k] != 1)
				extraCnt++;

		}
		
		//flagsStd
		for (int k = 0; k < flagsStd.size(); k++)
		{
			if (flagsStd[k] != 1)
				missCnt++;


		}

		if (missCnt > 0)
			putText(draw, "Missing Count:" + to_string(missCnt), Point(100, 400), FONT_HERSHEY_SIMPLEX, 4, Scalar(0, 140, 255), 2);//-0.87
		if (extraCnt > 0)
			putText(draw, "Extra Count:" + to_string(extraCnt), Point(100, 600), FONT_HERSHEY_SIMPLEX, 4, Scalar(0, 140, 255), 2);//-0.87
			//---------------------------------------------

		imwrite("foundConts.bmp", draw);

	//	resShow("imageROI", draw, 0.3);
		usersPoints.clear();
		
		return draw;
	}

	//------------------------------
	int algorithmLib::Class1::testImage(System::Drawing::Bitmap^ bitmap0)
	{
		Mat imageIn0 = BitmapToMat(bitmap0);

		Mat imageIn = correctLensDist(imageIn0, coeffFile);

		imwrite("imageIn.bmp", imageIn);
		Mat result_2 = undistortPers(imageIn, 4610, 3310, 1);
		
		Mat result = getContsMatch(result_2);
		//resShow("Result", result);
		resize(result, imageIn0, imageIn0.size());
		if (missCnt == 0 && extraCnt == 0 && profiledefect==0)
			return 1;
		else
			return 0;
	}
	int algorithmLib::Class1::saveTemplate(int maskEn, System::Drawing::Bitmap^ bitmap0)
	{
		Mat imageIn0 = BitmapToMat(bitmap0);
		Mat imageIn=correctLensDist(imageIn0, coeffFile);
		Mat result_2 = undistortPers(imageIn, 4610, 3310, 2);
		Mat result  = getConts(result_2);
		resize(result, imageIn0, imageIn0.size());
		return 1;
	
	}
