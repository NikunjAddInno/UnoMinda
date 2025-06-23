// This is the main DLL file.

#include "stdafx.h"

#include "algorithmLib.h"
#include <vector>
#include <math.h>
#include "AI_OCVLib.h"
#include "tempMatchFast.cpp"
#include <math.h>

#include "transformations.h";
//#include "contourComparison.h"

#include "seaprateColours.h"
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
const int Cat_TipLocation_Th = 9;
const int Cat_AngleTol = 10;
//for length
const int DiaDiff_ndl_Cat = 11;
const int TD_cat_egdeThreshold = 12;
const int needle_offset = 13;
const int Bevel_rotation = 14;





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

//mobile
float mmppMobile = 0.0752941;   //85pix= 6.25mm mmpp//680pix= 51.2mm  0.07529411764705882352941176470588mmpp

//test fn for match_templ_instances
string baseFolder = "mobileFrame";
string imageBaseFolder = "D:/CV/mobile_GMC_elentech/";
//String testimage =  "Image_20240628200059375.bmp";
//String testimage =  "Image_20240628195149133.bmp";
string testimage = "Image_20240628200358649.bmp";
//String testimage = "Image_20240628200506297.bmp";// "Image_20240628200059375.bmp";
//colours
enum sheetColour { blue, red, black, gray };


Scalar blueLow = Scalar(96, 69, 25);
Scalar blueHigh = Scalar(112, 229, 255);

Scalar redLow = Scalar(3, 143, 32);
Scalar redHigh = Scalar(11, 223, 253);

Scalar blackLow = Scalar(0, 0, 10);
Scalar blackHigh = Scalar(150, 43, 19);

Scalar grayLow = Scalar(14, 18, 157);
Scalar grayHigh = Scalar(21, 39, 212);

struct colourRange
{
	string name = "";
	Scalar low = Scalar(0, 0, 0);
	Scalar high = Scalar(180, 255, 255);
};
vector<colourRange> vec_colourRangeHSV = { colourRange{"blue",blueLow,blueHigh},  colourRange{"red",redLow,redHigh} ,colourRange{"black",blackLow,blackHigh},colourRange{"gray",grayLow,grayHigh} };

char window_name[30] = "HSV Segemtation";
Mat src;
vector<Scalar> blue_pts = vector < Scalar>();
struct matching_cp
{
	string templatePath = "";
	Mat templateImage;
	int matchModel_InstNo = -1;
	Rect templateROI;
	Point setup_location = Point(0, 0);
	Rect searchROI;
	int matchTolerance_percent = 60;
	float rotationTol_deg = 3;
	Point2f shiftTol = Point2f(0.5, 0.5);
	sheetColour sheet_colour = sheetColour::black;

	bool result = false;

	void addData(string path, Rect templateROIa, Rect searchROIa, int matchPercThresh, float rotation_thresh, Point2f shiftTola, sheetColour objColour)
	{
		templatePath = path;
		templateROI = templateROIa;
		searchROI = searchROIa;
		matchTolerance_percent = matchPercThresh;
		rotationTol_deg = rotation_thresh;
		shiftTol = shiftTola;
		sheet_colour = objColour;

	}
	int load_fixture()
	{
		templateImage = imread(imageBaseFolder + templatePath, 0);
		Mat templSheet;// = imread(pathT2);
		setup_location = ai_get_rectCenter(templateROI);
		matchModel_InstNo = get_MatchModelInstance();
		int resp = -2;
		resp = learn_MatchModelInstance(matchModel_InstNo, templateImage);
		if (resp == 1)
		{
			cout << "leaarned template " << templatePath << " sucecssfully . instno :: " << matchModel_InstNo << endl;
		}
		else
		{

			cout << "Failed to leaarn template " << templatePath << endl;
		}
		return resp;
	}
	int loadTemplate()
	{
		templateImage = imread(imageBaseFolder + templatePath);
		Mat templSheet;// = imread(pathT2);
		setup_location = ai_get_rectCenter(templateROI);
		createHSVmask(templateImage, vec_colourRangeHSV[sheet_colour].low, vec_colourRangeHSV[sheet_colour].high, templSheet);
		matchModel_InstNo = get_MatchModelInstance();
		int resp = -2;
		resp = learn_MatchModelInstance(matchModel_InstNo, templSheet);
		if (resp == 1)
		{
			cout << "leaarned template " << templatePath << " sucecssfully . instno :: " << matchModel_InstNo << endl;
		}
		else
		{

			cout << "Failed to leaarn template " << templatePath << endl;
		}
		return resp;
	}

};
vector <matching_cp> vec_fixture = vector<matching_cp>();
vector <matching_cp> vec_matchingTools = vector<matching_cp>();

//
static void onMouse(int event, int x, int y, int f, void*) {
	Mat image = src.clone();
	if (event != EVENT_LBUTTONDOWN)
	{
		return;
	}
	Vec3b rgb = image.at<Vec3b>(y, x);
	int B = rgb.val[0];
	int G = rgb.val[1];
	int R = rgb.val[2];

	Mat HSV;
	Mat RGB = image(Rect(x, y, 1, 1));
	cvtColor(RGB, HSV, COLOR_BGR2HSV);

	Vec3b hsv = HSV.at<Vec3b>(0, 0);
	int H = hsv.val[0];
	int S = hsv.val[1];
	int V = hsv.val[2];
	blue_pts.push_back(Scalar(hsv.val[0], hsv.val[1], hsv.val[2]));
	char name[30];
	printf(name, "B=%d", B);
	putText(image, name, Point(150, 40), FONT_HERSHEY_SIMPLEX, .7, Scalar(0, 255, 0), 2, 8, false);

	printf(name, "G=%d", G);
	putText(image, name, Point(150, 80), FONT_HERSHEY_SIMPLEX, .7, Scalar(0, 255, 0), 2, 8, false);

	printf(name, "R=%d", R);
	putText(image, name, Point(150, 120), FONT_HERSHEY_SIMPLEX, .7, Scalar(0, 255, 0), 2, 8, false);

	printf(name, "H=%d", H);
	putText(image, name, Point(25, 40), FONT_HERSHEY_SIMPLEX, .7, Scalar(0, 255, 0), 2, 8, false);

	printf(name, "S=%d", S);
	putText(image, name, Point(25, 80), FONT_HERSHEY_SIMPLEX, .7, Scalar(0, 255, 0), 2, 8, false);

	printf(name, "V=%d", V);
	putText(image, name, Point(25, 120), FONT_HERSHEY_SIMPLEX, .7, Scalar(0, 255, 0), 2, 8, false);

	printf(name, "X=%d", x);
	putText(image, name, Point(25, 300), FONT_HERSHEY_SIMPLEX, .7, Scalar(0, 0, 255), 2, 8, false);

	printf(name, "Y=%d", y);
	putText(image, name, Point(25, 340), FONT_HERSHEY_SIMPLEX, .7, Scalar(0, 0, 255), 2, 8, false);

	//imwrite("hsv.jpg",image);
	imshow(window_name, image);
	if (blue_pts.size() > 1)
	{
		Scalar valMin;
		Scalar valMax;

		createHSVLimits(blue_pts, true, valMin, valMax);

		cout << "valMin::" << valMin << endl;
		cout << "valMax::" << valMax << endl;
		Mat masked;
		createHSVmask(src, valMin, valMax, masked);
		ai_resShow("masked", masked, 0.4);
	}

}

Mat getBlueSheets(Mat imageInRGB)
{
	Scalar valMin;
	Scalar valMax;
	//	vector<Scalar> blue_pts = { Scalar(80,255,10),Scalar(107,184,250), Scalar(115,80,45),Scalar(98,136,30) };
	vector<Scalar> blue_pts = { Scalar(80,255,10),Scalar(107,184,250), Scalar(115,80,45),Scalar(98,136,30) };

	createHSVLimits(blue_pts, true, valMin, valMax);

	cout << "valMin::" << valMin << endl;
	cout << "valMax::" << valMax << endl;
	Mat masked;
	createHSVmask(imageInRGB, valMin, valMax, masked);
	ai_resShow("masked", masked, 0.4);
	return masked;
}
Point fixedFeature1 = Point(578, 1404);
Point fixedFeature2 = Point(849, 1404);
vector <Point> polygon = vector<Point>();

vector <Rect> rect_gapMeasure = vector<Rect>();
vector <Rect> gray_vectors = vector<Rect>();

void transformAndDrawPolygon(Mat& image, float rotAngle, Point center, Point shift)
{
	Mat inv_rotMat = cv::getRotationMatrix2D(center, rotAngle, 1.0);
	//line(image, transformPoint_rot(fixedFeature1, inv_rotMat), transformPoint_rot(fixedFeature2, inv_rotMat), Scalar(0, 200, 0), 2);
	vector<Point> vecTrx = transformPtVector_shift_rot(polygon, inv_rotMat, shift);
	polylines(image, vecTrx, true, Scalar(0, 165, 255), 2);
	//line(image, , transformPoint_shift_rot(fixedFeature2, inv_rotMat, shift), Scalar(0, 200, 0), 2);
}

vector<Point> transformAndDraw_Rect(Mat& image, Rect rectIn, float rotAngle, Point center, Point shift)
{
	vector<Point> rectPts = { Point(rectIn.x,rectIn.y),Point(rectIn.x + rectIn.width,rectIn.y), Point(rectIn.x + rectIn.width,rectIn.y + rectIn.height),Point(rectIn.x,rectIn.y + rectIn.height) };
	Mat inv_rotMat = cv::getRotationMatrix2D(center, rotAngle, 1.0);
	vector<Point> vecTrx = transformPtVector_shift_rot(rectPts, inv_rotMat, shift);
	polylines(image, vecTrx, true, Scalar(0, 165, 255), 2);

	return vecTrx;
	//line(image, transformPoint_rot(fixedFeature1, inv_rotMat), transformPoint_rot(fixedFeature2, inv_rotMat), Scalar(0, 200, 0), 2);
	//line(image, transformPoint_shift_rot(fixedFeature1, inv_rotMat, shift), transformPoint_shift_rot(fixedFeature2, inv_rotMat, shift), Scalar(0, 200, 0), 2);
}

void transformAndDraw(Mat& image, float rotAngle, Point center, Point shift)
{
	Mat inv_rotMat = cv::getRotationMatrix2D(center, rotAngle, 1.0);
	//line(image, transformPoint_rot(fixedFeature1, inv_rotMat), transformPoint_rot(fixedFeature2, inv_rotMat), Scalar(0, 200, 0), 2);
	line(image, transformPoint_shift_rot(fixedFeature1, inv_rotMat, shift), transformPoint_shift_rot(fixedFeature2, inv_rotMat, shift), Scalar(0, 200, 0), 2);
}

bool checkChildPartThresh(Mat imageGray, int thresh, cv::ThresholdTypes cvThreshType, int pixelPercentage)
{
	threshold(imageGray, imageGray, thresh, 255, cvThreshType);// THRESH_BINARY);
	double pixCnt = countNonZero(imageGray);
	ai_resShow("pix thresh", imageGray, 4);
	return ((pixCnt * 100 / (imageGray.cols * imageGray.rows)) > pixelPercentage);
}

bool checkChildPartGaps(Mat imageGray, int thresh, cv::ThresholdTypes cvThreshType, string regionName, vector<float>& vec_gaps, vector<float> vec_gapLimits)
{
	//Mat thresh;
	vec_gaps = { (float)imageGray.cols,(float)imageGray.cols,(float)imageGray.rows,(float)imageGray.rows };
	threshold(imageGray, imageGray, thresh, 255, cvThreshType);// THRESH_BINARY);
	int pixForAvg = 4;
	int half = pixForAvg / 2;
	if (imageGray.cols < pixForAvg + 2 || imageGray.rows < pixForAvg + 2)
	{
		return false; //invalid region
	}
	Mat midRows = imageGray(Rect(0, (imageGray.rows / 2) - half, imageGray.cols, pixForAvg));
	Mat midCols = imageGray(Rect((imageGray.cols / 2) - half, 0, pixForAvg, imageGray.rows));

	Mat row_mean, col_mean;
	reduce(midRows, row_mean, 0, REDUCE_AVG); //cols =width of img//rows 1
	reduce(midCols, col_mean, 1, REDUCE_AVG);//cols =1, rows = width of image
	//reduce(midCols, col_mean, 0, REDUCE_AVG);
	/*cout << "imageSize::" << endl;
	cout<<imageGray << endl;
	cout << "avgRows::" << endl;
	cout<<row_mean << endl;
	cout << "avgCols::" << endl;
	cout<< col_mean << endl;*/
	bool startFlag = false;
	int gapValL2R = 0;
	int gapValR2L = 0;
	int gapValT2D = 0;
	int gapValD2T = 0;

	int breakVal = 0;
	for (int x = 0; x < row_mean.cols - 1; x++)
	{
		//cout << row_mean.at<uchar>(0, x) << endl;
		if ((row_mean.at<uchar>(0, x) == 255 && !startFlag) || (row_mean.at<uchar>(0, x) == 255 && row_mean.at<uchar>(0, x + 1) == 255))
		{
			startFlag = true;
			gapValL2R++;
		}
		else
		{
			if (startFlag)
			{
				//gapValL2R++;

				break;
			}
		}
		breakVal = x;
	}
	//cout << "gapSize X L::" << gapValL2R << endl;
	startFlag = false;
	for (int x = row_mean.cols - 1; x > breakVal; x--)
	{
		//cout << row_mean.at<uchar>(0, x) << endl;
		if ((row_mean.at<uchar>(0, x) > 0 && !startFlag) || (row_mean.at<uchar>(0, x) == 255 && row_mean.at<uchar>(0, x - 1) == 255))
		{
			startFlag = true;
			gapValR2L++;
		}
		else
		{
			if (startFlag)
			{
				//gapValR2L++;
				break;
			}
		}
	}
	//cout << "gapSize X R::" << gapValR2L << endl;

	breakVal = 0;
	startFlag = false;
	for (int x = 0; x < col_mean.rows - 1; x++)
	{
		//cout << col_mean.at<uchar>(x, 0) << endl;
		if ((col_mean.at<uchar>(x, 0) > 0 && !startFlag) || (col_mean.at<uchar>(x, 0) == 255 && col_mean.at<uchar>(x + 1, 0) == 255))
		{
			startFlag = true;
			gapValT2D++;
		}
		else
		{
			if (startFlag)
			{
				//gapValT2D++;

				break;
			}
		}
		breakVal = x;
	}
	//cout << "gapSize Y T::" << gapValT2D << endl;
	startFlag = false;
	for (int x = col_mean.rows - 1; x > breakVal; x--)
	{
		//cout << col_mean.at<uchar>(x, 0) << endl;
		if ((col_mean.at<uchar>(x, 0) > 0 && !startFlag) || (col_mean.at<uchar>(x, 0) == 255 && col_mean.at<uchar>(x - 1, 0) == 255))
		{
			startFlag = true;
			gapValD2T++;
		}
		else
		{
			if (startFlag)
			{
				//gapValD2T++;
				break;
			}
		}
	}
	//cout << "gapSize Y D::" << gapValD2T << endl;
	cout << "gaps::" << regionName << format(" L=%d R=%d T=%d B=%d", gapValL2R, gapValR2L, gapValT2D, gapValD2T) << endl;
	vec_gaps[0] = (float)gapValL2R * mmppMobile;
	vec_gaps[1] = (float)gapValR2L * mmppMobile;
	vec_gaps[2] = (float)gapValT2D * mmppMobile;
	vec_gaps[3] = (float)gapValD2T * mmppMobile;

	return (vec_gaps[0] <= vec_gapLimits[0] && vec_gaps[1] <= vec_gapLimits[1] && vec_gaps[2] <= vec_gapLimits[2] && vec_gaps[3] <= vec_gapLimits[3]);

}


int testTemplMatchFast(Mat& imageColour)
{
	polygon = { Point(344,824),Point(515,824) ,Point(591,863) ,Point(628,941) ,Point(628,1085),Point(516,1085),Point(344,960) };
	//string path = "F:/AISVids/Asahi Backup 8April23/SubPC1/Images/4/08_04_2023/14_12_36.bmp"; //PC1

	//string path = imageBaseFolder+testimage;//PC2
	//string path = imageBaseFolder + "front/" + to_string(imageNum) + ".bmp";//PC2

	//string path = "F:/AISVids/Asahi Backup 8April23/SubPC3/08_04_2023/14_11_07.bmp";//PC3
	 //src= imread(path);
	 //resize(src, src, Size(), 0.5, 0.5);
	//imshow(window_name, src);
	//setMouseCallback(window_name, onMouse, 0);
	//waitKey();
	//getBlueSheets(src);

	Mat imageIn;
	//Mat imageColour = imread(path);
	cvtColor(imageColour, imageIn, COLOR_BGR2GRAY);
	Mat imgDraw = imageColour;// .clone();
	//Mat imgDraw;
	//cvtColor(imageIn, imgDraw, COLOR_GRAY2BGR);
	//string pathT1 = imageBaseFolder+"images/templates/t1.bmp";//PC2
	//string pathT2 = imageBaseFolder+"images/templates/t2.bmp";//PC2

	//Mat templ1 = imread(pathT1, 0);
	//Mat templ2 = imread(pathT2, 0);


	//threshold(imageIn, imageIn, 20, 255, THRESH_BINARY);
	//threshold(templ1, templ1, 20, 255, THRESH_BINARY);
	//threshold(templ2, templ2, 20, 255, THRESH_BINARY);
	//int instNoT1 = get_MatchModelInstance();
	//int instNoT2 = get_MatchModelInstance();

	//string pathTSheet = imageBaseFolder + "images/templates/tSheet.bmp";//PC2
	//Mat templSheetRGB = imread(pathTSheet);
	//Mat templSheet;// = imread(pathT2);
	//
	//createHSVmask(templSheetRGB, blueLow, blueHigh, templSheet);
	//int instNoTSheet = get_MatchModelInstance();
	//Rect roi1 = Rect(615, 1270, 262, 218);
	//Rect roi2 = Rect(1890, 1050, 270, 270);
	//Rect roi1 = Rect(615, 1070, 462, 418);
	//Rect roi2 = Rect(1890, 850, 470, 470);
	//rectangle(imgDraw, roi1, Scalar(255, 0, 0), 2);
	//rectangle(imgDraw, roi2, Scalar(255, 0, 0), 2);

	//rectangle(imgDraw, roiSheet, Scalar(255, 0, 0), 2);


	//cout << "template channels::" << templ1.channels() << endl;
	//int resp = -2;
	//resp = learn_MatchModelInstance(instNoT1, templ1);
	//cout << "response to learnTempl T1 = " << resp << endl;
	//resp = -2;
	//resp = learn_MatchModelInstance(instNoT2, templ2);
	//cout << "response to learnTempl T2 = " << resp << endl;

	//resp = -2;
	//resp = learn_MatchModelInstance(instNoTSheet, templSheet);
	//cout << "response to learnTempl T2 = " << resp << endl;
	m_bDebugMode = false;

	matching_cp fixture1 = vec_fixture[0];
	matching_cp fixture2 = vec_fixture[1];

	int matchResp = -2;

	Point matchLocT1 = Point(0, 0);
	Point matchLocT2 = Point(0, 0);
	Point matchLocTSheet = Point(0, 0);
	vector<s_SingleTargetMatch> matchResultsT1 = findMatch_MatchModelInstance(fixture1.matchModel_InstNo, imageIn(fixture1.searchROI), imgDraw(fixture1.searchROI), &matchResp);
	if (matchResultsT1.size() > 0)
	{
		matchLocT1 = Point(fixture1.searchROI.x, fixture1.searchROI.y) + Point(matchResultsT1[0].ptCenter.x, matchResultsT1[0].ptCenter.y);
		float matchAngle = matchResultsT1[0].dMatchedAngle;
		cout << "  location ::" << matchLocT1 << endl;
		cout << "angle::" << matchAngle << endl;
	}
	matchResp = -2;
	vector<s_SingleTargetMatch> matchResultsT2 = findMatch_MatchModelInstance(fixture2.matchModel_InstNo, imageIn(fixture2.searchROI), imgDraw(fixture2.searchROI), &matchResp);
	if (matchResultsT2.size() > 0)
	{
		matchLocT2 = Point(fixture2.searchROI.x, fixture2.searchROI.y) + Point(matchResultsT2[0].ptCenter.x, matchResultsT2[0].ptCenter.y);
		float matchAngle = matchResultsT2[0].dMatchedAngle;
		cout << "  location ::" << matchLocT2 << endl;
		cout << "angle::" << matchAngle << endl;
	}
	if (!matchResultsT1.size() > 0 || !matchResultsT2.size() > 0)
	{
		drawTextWithBackGround(imgDraw, "NG. Part not located", Point(100, 100), Scalar::all(255), Scalar(0, 0, 200), 2, 2);
		return 0;
	}
	line(imgDraw, matchLocT1, matchLocT2, Scalar(0, 0, 255), 2, 4);

	float angle = ai_getLineAngle(matchLocT1, matchLocT2);
	cout << "angle of line::" << angle << endl;
	angle = angle - 351.06;//initial angle at setup
	cout << "angle current::" << angle << endl;
	Point center = (matchLocT2 + matchLocT1) / 2;
	Point shift = center - Point(1492, 1208);

	cout << "________________________center::" << center << endl;
	cout << "shift::" << shift << endl;
	circle(imgDraw, center, 4, Scalar(0, 255, 0), -1);

	//----------child parts
	for (int k = 0; k < vec_matchingTools.size(); k++)
	{
		matching_cp cp = vec_matchingTools[k];
		Point offset = Point(1492, 1208) - vec_matchingTools[k].setup_location;// Point(760, 718);
		matchResp = -2;
		Mat imgRoiSheet;
		createHSVmask(imageColour(vec_matchingTools[k].searchROI), vec_colourRangeHSV[vec_matchingTools[k].sheet_colour].low, vec_colourRangeHSV[vec_matchingTools[k].sheet_colour].high, imgRoiSheet);
		ai_resShow(std::to_string(k), imgRoiSheet, 1);
		vector<s_SingleTargetMatch> matchResultsTSheet = findMatch_MatchModelInstance(vec_matchingTools[k].matchModel_InstNo, imgRoiSheet, imgDraw(vec_matchingTools[k].searchROI), &matchResp);
		if (matchResultsTSheet.size() > 0)
		{
			matchLocTSheet = Point(vec_matchingTools[k].searchROI.x, vec_matchingTools[k].searchROI.y) + Point(matchResultsTSheet[0].ptCenter.x, matchResultsTSheet[0].ptCenter.y);
			float matchAngle = matchResultsTSheet[0].dMatchedAngle;
			float angleWRTfixture = matchAngle - angle;
			cout << "  location sheet::" << matchLocTSheet << endl;
			cout << "angle sheet::" << matchAngle << endl;
			Point shift = offset - (center - matchLocTSheet);
			Point2f shiftmm = Point2f(shift.x * mmppMobile, shift.y * mmppMobile);
			cout << "sheet shift =" << shift << endl;
			drawTextWithBackGround(imgDraw, cv::format("A:%3.2f", angleWRTfixture), ai_get_rectCenter(vec_matchingTools[k].searchROI), Scalar::all(255), Scalar(200, 0, 0), 2, 2);
			//drawTextWithBackGround(imgDraw, cv::format("Sh x:%3.1f,y:%3.1f", shift.x,shift.y), ai_get_rectCenter(roiSheet)+Point(0,90), Scalar::all(255), Scalar(200, 0, 0), 4, 2);
			if (abs(shiftmm.x) <= cp.shiftTol.x && abs(shiftmm.y) <= cp.shiftTol.y && abs(angleWRTfixture) <= cp.rotationTol_deg)
			{
				drawTextWithBackGround(imgDraw, cv::format("Sh x:%3.1f,y:%3.1f", shiftmm.x, shiftmm.y), ai_get_rectCenter(vec_matchingTools[k].searchROI) + Point(0, 90), Scalar::all(255), Scalar(0, 200, 0), 2, 2);
				vec_matchingTools[k].result = true;
			}
			else
			{
				drawTextWithBackGround(imgDraw, cv::format("S x:%3.1f,y:%3.1f", shiftmm.x, shiftmm.y), ai_get_rectCenter(vec_matchingTools[k].searchROI) + Point(0, 90), Scalar::all(255), Scalar(0, 0, 200), 2, 2);
				vec_matchingTools[k].result = false;
			}
		}
		else
		{
			drawTextWithBackGround(imgDraw, "Miss", ai_get_rectCenter(vec_matchingTools[k].searchROI), Scalar::all(255), Scalar(0, 0, 200), 4, 2);
			vec_matchingTools[k].result = false;
		}
		transformAndDraw(imgDraw, -1 * angle, center, shift);
		//	transformAndDrawPolygon(imgDraw, -1 * angle, center, shift);
	}
	//Expected location sheet
	//Point offset = Point(1492, 1208) -Point(760, 718);
	//matchResp = -2;
	//Mat imgRoiSheet;
	//createHSVmask(imageColour(roiSheet), blueLow, blueHigh, imgRoiSheet);
	//ai_resShow("roiTsheetMask", imgRoiSheet, 1);
	//vector<s_SingleTargetMatch> matchResultsTSheet = findMatch_MatchModelInstance(instNoTSheet, imgRoiSheet, imgDraw(roiSheet), &matchResp);
	//if (matchResultsTSheet.size() > 0)
	//{
	//	matchLocTSheet = Point(roiSheet.x, roiSheet.y) + Point(matchResultsTSheet[0].ptCenter.x, matchResultsTSheet[0].ptCenter.y);
	//	float matchAngle = matchResultsTSheet[0].dMatchedAngle;
	//	cout << "  location sheet::" << matchLocTSheet << endl;
	//	cout << "angle sheet::" << matchAngle << endl;
	//	Point shift = offset-( center - matchLocTSheet);
	//	cout << "sheet shift =" << shift << endl;
	//	drawTextWithBackGround(imgDraw, cv::format("A:%3.2f", matchAngle + angle), ai_get_rectCenter(roiSheet), Scalar::all(255), Scalar(200, 0, 0), 2, 2);
	//	//drawTextWithBackGround(imgDraw, cv::format("Sh x:%3.1f,y:%3.1f", shift.x,shift.y), ai_get_rectCenter(roiSheet)+Point(0,90), Scalar::all(255), Scalar(200, 0, 0), 4, 2);
	//	drawTextWithBackGround(imgDraw, cv::format("Shift x:%d,y:%d", shift.x,shift.y), ai_get_rectCenter(roiSheet)+Point(0,90), Scalar::all(255), Scalar(200, 0, 0), 2, 2);
	//}
	//else
	//{
	//	drawTextWithBackGround(imgDraw, "Sheet Missing", ai_get_rectCenter(roiSheet), Scalar::all(255), Scalar(0, 0, 200), 4, 2);
	//}
	//cout << "matches found Sheet:: " << matchResultsTSheet.size() << endl;
	//cout << "response to match instance Sheet=" << matchResp << endl;
	////	//child parts

	//transformAndDraw(imgDraw, -1*angle, center,shift);
	//transformAndDrawPolygon(imgDraw, -1 * angle, center, shift);

	for (int k = 0; k < rect_gapMeasure.size(); k++)
	{
		vector<Point>  rotPts = transformAndDraw_Rect(imgDraw, rect_gapMeasure[k], -1 * angle, center, shift);
		//vector<Point> rectPts = { Point(0,0),Point(rect_gapMeasure[k].width,0), Point(rect_gapMeasure[k].width,rect_gapMeasure[k].height),Point(0, rect_gapMeasure[k].height) };
		//Mat h = findHomography(rotPts, rectPts, RANSAC);
		//Mat cropImg = Mat::zeros(rect_gapMeasure[k].size(), imgDraw.type());
		//// Use homography to warp image
		//warpPerspective(imgDraw, cropImg, h, cropImg.size());
		Mat cropImg = ai_cropRotatedRectFromImage(rotPts, rect_gapMeasure[k], imageIn);

		//		Mat warp_mat = getAffineTransform(rotPts, rectPts);

				//warpAffine(imgDraw, cropImg, warp_mat, rect_gapMeasure[k].size());
		vector<float> vec_GapsMeasured;
		vector<float> vec_gapLimits = { 0.8,0.8,0.8,0.8 };
		Mat presenceClone = cropImg.clone();
		bool result = checkChildPartGaps(cropImg, 230, THRESH_BINARY, to_string(k), vec_GapsMeasured, vec_gapLimits);
		bool resultPresence = checkChildPartThresh(presenceClone, 230, THRESH_BINARY_INV, 70);
		result = resultPresence;
		cout << "measured gaps vec::::" << format(" L=%3.2f R=%3.2f T=%3.2f B=%3.2f", vec_GapsMeasured[0], vec_GapsMeasured[1], vec_GapsMeasured[2], vec_GapsMeasured[3]) << endl;
		if (result)
			circle(imgDraw, rotPts[0], 8, Scalar(0, 255, 0), -1);
		else
			circle(imgDraw, rotPts[0], 8, Scalar(0, 0, 255), -1);
		imshow("CropWarp", cropImg);
		//	waitKey();
	}
	for (int k = 0; k < gray_vectors.size(); k++)
	{
		vector<Point>  rotPts = transformAndDraw_Rect(imgDraw, gray_vectors[k], -1 * angle, center, shift);
		//vector<Point> rectPts = { Point(0,0),Point(rect_gapMeasure[k].width,0), Point(rect_gapMeasure[k].width,rect_gapMeasure[k].height),Point(0, rect_gapMeasure[k].height) };
		//Mat h = findHomography(rotPts, rectPts, RANSAC);
		//Mat cropImg = Mat::zeros(rect_gapMeasure[k].size(), imgDraw.type());
		//// Use homography to warp image
		//warpPerspective(imgDraw, cropImg, h, cropImg.size());
		Mat cropImg = ai_cropRotatedRectFromImage(rotPts, gray_vectors[k], imageIn);

		//		Mat warp_mat = getAffineTransform(rotPts, rectPts);

				//warpAffine(imgDraw, cropImg, warp_mat, rect_gapMeasure[k].size());



		bool result = checkChildPartThresh(cropImg, 230, THRESH_BINARY_INV, 70);


		if (result)
			circle(imgDraw, rotPts[0], 8, Scalar(0, 255, 0), -1);
		else
			circle(imgDraw, rotPts[0], 8, Scalar(0, 0, 255), -1);
		imshow("CropWarp", cropImg);
		//	waitKey();
	}


	drawTextWithBackGround(imgDraw, cv::format("A:%3.2f", angle), center + Point(0, -90), Scalar::all(255), Scalar(200, 0, 0), 4, 2);
	ai_resShow("detections", imgDraw, 0.4);
	//imwrite("baseFolder/templateMatch_detections.bmp", imgDraw);

	waitKey();
	return 1;
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

int algorithmLib::Class1::loadTemplateDataMobile(System::String^ modelPath)
{
	string basePath = string_to_char_array(modelPath);
	cout << "string from c++" << basePath << endl;
	matching_cp fixture1;
	fixture1.addData("images/templates/t1.bmp", Rect(0, 0, 100, 100), Rect(615, 1070, 462, 418), 60, 4, Point2f(1.1, 1.1), sheetColour::black);
	vec_fixture.push_back(fixture1);

	matching_cp fixture2;
	fixture2.addData("images/templates/t2.bmp", Rect(0, 0, 100, 100), Rect(1890, 850, 470, 470), 60, 4, Point2f(1.1, 1.1), sheetColour::black);
	vec_fixture.push_back(fixture2);

	for (int k = 0; k < vec_fixture.size(); k++)
	{
		vec_fixture[k].load_fixture();
	}
	//init child part templates
	matching_cp cp_sheetBlue;
	cp_sheetBlue.addData("images/templates/sheet1.bmp", Rect(443, 619, 343, 199), Rect(343, 519, 543, 499), 60, 4, Point2f(1.1, 1.1), sheetColour::blue);
	vec_matchingTools.push_back(cp_sheetBlue);

	matching_cp cp_sheetRed;
	cp_sheetRed.addData("images/templates/redSheet2.bmp", Rect(2060, 892, 186, 185), Rect(2060 - 100, 892 - 100, 186 + 150, 185 + 150), 60, 4, Point2f(1.1, 1.1), sheetColour::red);
	vec_matchingTools.push_back(cp_sheetRed);

	matching_cp cp_sheetCam;
	cp_sheetCam.addData("images/templates/blackCam1.bmp", Rect(321, 804, 337, 165), Rect(321 - 100, 804 - 100, 337 + 200, 165 + 200), 60, 4, Point2f(1.1, 1.1), sheetColour::black);
	vec_matchingTools.push_back(cp_sheetCam);

	matching_cp cp_sheetMid;
	cp_sheetMid.addData("images/templates/blueSheet2.bmp", Rect(680, 854, 329, 457), Rect(680 - 100, 854 - 100, 329 + 200, 457 + 200), 60, 4, Point2f(1.1, 1.1), sheetColour::blue);
	vec_matchingTools.push_back(cp_sheetMid);

	matching_cp cp_sheetBottom;
	cp_sheetBottom.addData("images/templates/blueSheet3.bmp", Rect(2136, 904, 290, 478), Rect(2136 - 100, 904 - 100, 290 + 200, 478 + 200), 60, 4, Point2f(1.1, 1.1), sheetColour::blue);
	vec_matchingTools.push_back(cp_sheetBottom);

	matching_cp cp_sheetBott1;
	cp_sheetBott1.addData("images/templates/blueSheet4.bmp", Rect(2154, 570, 261, 128), Rect(2154 - 100, 570 - 100, 261 + 200, 128 + 200), 60, 4, Point2f(1.1, 1.1), sheetColour::blue);
	vec_matchingTools.push_back(cp_sheetBott1);
	//---load cp templates
	for (int k = 0; k < vec_matchingTools.size(); k++)
	{
		vec_matchingTools[k].loadTemplate();
	}
	//initiate gap vec temporarily


	//init rects fro shitft and presence measurements
	rect_gapMeasure = { Rect(474,1124,70,41),Rect(575,1400,70,40), Rect(786,1400,70,40),Rect(2230,1344,70,41), Rect(2143,1229,70,41),Rect(400,632,40,70) };
	gray_vectors = { ai_getRect_from_cent_size(ai_get_rectCenter(rect_gapMeasure[0]),Size(30,20)), ai_getRect_from_cent_size(ai_get_rectCenter(rect_gapMeasure[1]),Size(30,20)), ai_getRect_from_cent_size(ai_get_rectCenter(rect_gapMeasure[2]),Size(30,20)) , ai_getRect_from_cent_size(ai_get_rectCenter(rect_gapMeasure[3]),Size(30,20)), ai_getRect_from_cent_size(ai_get_rectCenter(rect_gapMeasure[4]),Size(20,30)) };
	m_dScore = 0.4;
	m_dToleranceAngle = 2;
	return 1;
}
	bool debugModeEn = false;

	int algorithmLib::Class1::dummyProcC1(System::Drawing::Bitmap^ bitmap0, int camNo,int insMode) //insMode 0: lengthcheck 1:full check
	{
		Mat imageIn0 = BitmapToMat(bitmap0);
		debugModeEn = debugModeProp;
		Mat result = imageIn0;
		//inst_WidthCamParC1.edgeMagnitude = (int)list_inspections0[Cat_TipLocation_Th].get_std_value();
			try{
		/*	for (int id = 0; id < vec_cam1Rects.size(); id++)
			{
				Rect roi = vec_cam1Rects[id].r;
				rectangle(result, roi, Scalar(255, 0, 0), 2);
			}*/
			Mat gray;
			cvtColor(imageIn0, gray, COLOR_BGR2GRAY);
			testTemplMatchFast(result);
				bool res = false;
				//result = detectTip(gray, vec_cam1Rects[2], vec_cam1Rects[1], vec_cam1Rects[0], camNo, list_inspections0, res);
			//	cout << "result C1::" << res << endl;
				resultC1Prop = res;


				if (res)
				{
					drawTextWithBackGround(result, cv::format("OK"), Point(860, 60), Scalar(255, 255, 255), Scalar(0, 255, 0), 4, 4);
				}
				else
				{
					drawTextWithBackGround(result, cv::format("NG"), Point(860, 60), Scalar(255, 255, 255), Scalar(0, 0, 200), 4, 4);
				}
			
			//resize(draw, result, result.size());

		
	}
	catch (exception exx)
	{
		putText(result, "Exception", Point(480, 50), FONT_HERSHEY_SCRIPT_COMPLEX, 2, Scalar(0, 255, 0), 2);
		L_ResultC1Prop = false;
		resultC1Prop = false;
		cv::resize(result, imageIn0, imageIn0.size());
		return 0;
	}
		putText(result, to_string(camNo) + "__" + to_string(insMode), Point(50, 50), FONT_HERSHEY_SCRIPT_COMPLEX, 2, Scalar(0, 255, 0), 2);
		cv::resize(result, imageIn0, imageIn0.size());
		return 1;
	}

	int algorithmLib::Class1::dummyProcC2(System::Drawing::Bitmap^ bitmap0, int camNo, int insMode) //insMode 0: lengthcheck 1:full check
	{
		Mat imageIn0 = BitmapToMat(bitmap0);
		debugModeEn = debugModeProp;
		Mat result = imageIn0;
		//inst_WidthCamParC2.edgeMagnitude = (int)list_inspections1[Cat_TipLocation_Th].get_std_value();
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

				if (insMode == 0)
				{
					bool res = false;
					float returnLength = 0;
					bool bevelSampleAbsent = false;
					//result = checkTD(gray, vec_cam2Rects[2], vec_cam2Rects[1], vec_cam2Rects[0], camNo, list_inspections1, res, returnLength, bevelSampleAbsent);
					L_ResultC2Prop = res;
					LengthC2Prop = returnLength;
					bevel_Sample_absentC2Prop = bevelSampleAbsent;
					//cout << "length c2::" << LengthC2Prop << endl;
					if (res)
					{
						drawTextWithBackGround(result, cv::format("OK_TD"), Point(800, 60), Scalar(255, 255, 255), Scalar(0, 255, 0), 2, 2);
					}
					/*else
					{
					drawTextWithBackGround(result, cv::format("NG_TD"), Point(800, 60), Scalar(255, 255, 255), Scalar(0, 0, 200),2, 2);
					}*/
				}
				else
				{
					bool res = false;
				//	result = detectTip(gray, vec_cam2Rects[2], vec_cam2Rects[1], vec_cam2Rects[0], camNo, list_inspections1, res);
					//resize(draw, result, result.size());
					resultC2Prop = res;
					if (res)
					{
						drawTextWithBackGround(result, cv::format("OK"), Point(860, 60), Scalar(255, 255, 255), Scalar(0, 255, 0), 4, 4);
					}
					else
					{
						drawTextWithBackGround(result, cv::format("NG"), Point(860, 60), Scalar(255, 255, 255), Scalar(0, 0, 200), 4, 4);
					}
				}
				//cout << "result C2::" << res << endl;
			
			}
		}
		catch (exception exx)
		{
			putText(result,"Exception", Point(480, 50), FONT_HERSHEY_SCRIPT_COMPLEX, 2, Scalar(0, 255, 0), 2);
			L_ResultC2Prop = false;
			resultC2Prop = false;
			cv::resize(result, imageIn0, imageIn0.size());
			return 1;
		}
		putText(result, to_string(camNo)+"__"+ to_string(insMode), Point(50, 50), FONT_HERSHEY_SCRIPT_COMPLEX, 2, Scalar(0, 255, 0), 2);
		cv::resize(result, imageIn0, imageIn0.size());
		return 1;
	}