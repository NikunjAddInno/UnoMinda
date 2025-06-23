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
#include "markInspect_applied.h"
#include "cls_resultFrontCam.h"
using namespace System;
using namespace cv;
using namespace std;
using namespace markInspection_tools;
//using namespace toolDef;
//2413,1028

string path;
enum sheetColour { blue, red, black, gray };
static const char* string_to_char_array(System::String^ string)
{
	const char* str = (const char*)(Marshal::StringToHGlobalAnsi(string)).ToPointer();
	return str;
}
float mmppMobile = 0.021650;// 0.0416260162;// 0.03959783;// 0.0752941;   //85pix= 6.25mm mmpp//680pix= 51.2mm  0.07529411764705882352941176470588mmpp

//fn defs
Mat BitmapToMat(System::Drawing::Bitmap^ bitmap);

//--------json load
float mmPPcathater = 0.021650;
enum toolTypeCpp { Arc, Width, Circle, Thread, Angle, Locate, Match, Inner_Hex, Outer_Hex };
string arr_enumNames[] = { "Arc", "Width", "Circle", "Thread", "Angle","Locate","Match", "Inner_Hex", "Outer_Hex" };

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



Scalar blueLow = Scalar(89, 58, 25);
Scalar blueHigh = Scalar(112, 255, 255);

Scalar redLow = Scalar(0, 154, 32);
Scalar redHigh = Scalar(11, 255, 255);

Scalar blackLow = Scalar(0, 0, 10);
Scalar blackHigh = Scalar(113, 133., 25);

Scalar grayLow = Scalar(14, 18, 157);
Scalar grayHigh = Scalar(21, 39, 212);


struct colourRange
{
	string name = "";
	Scalar low = Scalar(0, 0, 0);
	Scalar high = Scalar(180, 255, 255);
	int id=1;
};
vector<colourRange> vec_colourRangeHSV = vector < colourRange>();
//vector<colourRange> vec_colourRangeHSV = { colourRange{"blue",blueLow,blueHigh},  colourRange{"red",redLow,redHigh} ,colourRange{"black",blackLow,blackHigh},colourRange{"gray",grayLow,grayHigh} };
int algorithmLib::Class1::loadColours(int index, int id, System::String^ name, int h_low, int s_low, int v_low, int h_high, int s_high, int v_high)
{
	if (index == 0)
	{
		vec_colourRangeHSV.clear();
	}
	colourRange r;
	r.id = id;
	r.name = string_to_char_array(name);
	r.low = Scalar(h_low, s_low, v_low);
	r.high = Scalar(h_high, s_high, v_high);
	vec_colourRangeHSV.push_back(r);
	cout << "*******************************************loading colour " << id << "  total colours::" << vec_colourRangeHSV.size() << "  hue::" << r.low[0] << endl;
	return 1;
}

colourRange getColourFromId(int id, bool& found)// Scalar& low, Scalar& high)
{
	//low = Scalar(0, 0, 0);
	//high = Scalar(180, 255, 255);
	colourRange c;
	found = false;
	for (int k = 0; k < vec_colourRangeHSV.size(); k++)
	{
		if (vec_colourRangeHSV[k].id == id)
		{
			c = vec_colourRangeHSV[k];
			found = true;
			break;
		}
	}
	return c;

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
		for (int k = 0; k < list_inspections0.size(); k++)
		{
			cout << list_inspections0[k].get_mutable_par_name() << "  value::" << list_inspections0[k].get_mutable_std_value() << endl;
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

MarkInsTools obj_markIns_Tools = MarkInsTools();
////toolsRegion



struct GrayPresence
{
	int tool_id = 0;

	int fixType = 1;
	int fixToolRef = -1;
	int fixMode = 0;

	string name = "";
	Rect roi = Rect(0, 0, 10, 10);
	int threshold;
	cv::ThresholdTypes thresholdType;
	int matchPercent = 50;
	int thresholdMode = 0;
	//mode 0
	int colourId = 1;
	//mode 1
	colourRange colourRangeVals;
	void addData(int id, string name_a, Rect Roia, int thresholda, int threshTypea, int matchPercenta, int threshmodea, int colourIDa, int f_type, int f_mode, int f_refNo)
	{
		tool_id = id;
		fixType = f_type;
		fixMode = f_mode;
		fixToolRef = f_refNo;

		name = name_a;
		matchPercent = matchPercenta;
		roi = Roia;
		threshold = thresholda;
		thresholdType = (cv::ThresholdTypes)threshTypea;
		thresholdMode = threshmodea;
		colourId = colourIDa;

		bool colourFound = false;
		colourRangeVals = getColourFromId(colourId, colourFound);
		if (!colourFound)
		{
			cout << "ERROR:: selected colour not found in cpp" << endl;
		}
	}

	bool result = false;

};
struct boundaryGap
{
	string name = "";
	vector<float> gapLimits = vector<float>(4, 0);
	Rect roi = Rect(0, 0, 10, 10);
	int threshold;
	cv::ThresholdTypes thresholdType;
	bool result = false;
	Rect roiOuter = Rect(0, 0, 15, 15);
	int thresholdOuter;
	cv::ThresholdTypes thresholdTypeOuter;
	int matchPercentOuter = 50;
	bool enabledMeasure = true;
	bool enabledOuter = true;
	void addData(string name_a,bool enMeasurea,bool enOutera,float L, float R, float T, float B, Rect Roia, int thresholda, int threshTypea, Rect roiOutera,int thresholdOutera,int matchPCoutera)
	{
		enabledMeasure = enMeasurea;
		name = name_a;
		gapLimits = { L,R,T,B };
		roi = Roia;
		threshold = thresholda;
		thresholdType = (cv::ThresholdTypes)threshTypea;

		enabledOuter = enOutera;
		roiOuter = roiOutera;
		thresholdOuter = thresholdOutera;
		matchPercentOuter = matchPCoutera;
		thresholdTypeOuter = cv::ThresholdTypes::THRESH_BINARY_INV;
	}


};

struct fixture
{

	string templatePathT1 = "";
	string templatePathT2 = "";
	float scoreT1 = 50;
	float scoreT2 = 50;
	Rect roiT1 = Rect(0, 0, 10, 10);
	Rect roiT2 = Rect(0, 0, 20, 20);
	Rect searchArT1 = Rect(0, 0, 20, 20);
	Rect searchArT2 = Rect(0, 0, 20, 20);
	float dist_t1t2 = 0;
	float rotationLimit = 5;

	//calculte in cpp  at setup
	Point2f setupCenter = Point(0, 0);
	float setupAngle = 0;
	int matchModelInstT1 = -1;
	int matchModelInstT2 = -1;
	Mat imgT1;
	Mat imgT2;
	//result
	bool result = false;

	void addData(string templatePathT1a, string templatePathT2a, float scoreT1a, float scoreT2a, Rect roiT1a, Rect roiT2a, Rect searchArT1a, Rect searchArT2a, float dist_t1t2a, float rotationLimita)
	{
		templatePathT1 = templatePathT1a;
		templatePathT2 = templatePathT2a;
		scoreT1 = scoreT1a;
		scoreT2 = scoreT2a;
		roiT1 = roiT1a;
		roiT2 = roiT2a;
		searchArT1 = searchArT1a;
		searchArT2 = searchArT2a;
		dist_t1t2 = dist_t1t2a;
		rotationLimit = rotationLimita;
		//calculate and set
		Point centT1 = ai_get_rectCenter(roiT1);
		Point centT2 = ai_get_rectCenter(roiT2);
		//setupCenter = Point(1492, 1208);// (centT1 + centT2) / 2;
		setupCenter = (centT1 + centT2) / 2;
		//setupAngle = 351.06;// ai_getLineAngle(centT1, centT2);
		setupAngle = ai_getLineAngle(centT1, centT2);
			if (setupAngle > 180)
			{
		setupAngle = setupAngle-360;
			}
		cout << "  ^^^^^^^^^^^^^ setup angle ::" << setupAngle << endl;
		cout << "  ^^^^^^^^^^^^^ setup center ::" << setupCenter << endl;

	}
	int load_fixture()
	{
		imgT1 = imread(templatePathT1, 0);
		imgT2 = imread(templatePathT2, 0);
		Mat templSheet;// = imread(pathT2);

		matchModelInstT1 = get_MatchModelInstance();
		int resp = -2;
		resp = learn_MatchModelInstance(matchModelInstT1, imgT1);
		if (resp == 1)
		{
			cout << "leaarned fixture template " << templatePathT1 << " sucecssfully . instno :: " << matchModelInstT1 << endl;
		}
		else
		{

			cout << "Failed to leaarn fixture template " << templatePathT1 << endl;
		}

		matchModelInstT2 = get_MatchModelInstance();
		resp = -2;
		resp = learn_MatchModelInstance(matchModelInstT2, imgT2);
		if (resp == 1)
		{
			cout << "leaarned fixture template " << templatePathT2 << " sucecssfully . instno :: " << matchModelInstT2 << endl;
		}
		else
		{

			cout << "Failed to leaarn fixture template " << templatePathT2 << endl;
		}
		return resp;
	}

	int locatePartFixture(Mat imageIn, Mat& imgDraw, float& angle, Point2f& center, Point2f& shift)
	{
		m_dScore = 0.5;
		int matchResp = -2;

		Point2f matchLocT1 = Point2f(0, 0);
		Point2f matchLocT2 = Point2f(0, 0);
		//Point matchLocTSheet = Point(0, 0);
		vector<s_SingleTargetMatch> matchResultsT1 = findMatch_MatchModelInstance(matchModelInstT1, imageIn(searchArT1), imgDraw(searchArT1), &matchResp);
		if (matchResultsT1.size() > 0)
		{
			matchLocT1 = Point2f(searchArT1.x, searchArT1.y) + Point2f(matchResultsT1[0].ptCenter.x, matchResultsT1[0].ptCenter.y);
			float matchAngle = matchResultsT1[0].dMatchedAngle; 
			cout << "  location ::" << matchLocT1 << endl;
			cout << "angle::" << matchAngle << endl;
		}
		matchResp = -2;
		vector<s_SingleTargetMatch> matchResultsT2 = findMatch_MatchModelInstance(matchModelInstT2, imageIn(searchArT2), imgDraw(searchArT2), &matchResp);
		if (matchResultsT2.size() > 0)
		{
			matchLocT2 = Point2f(searchArT2.x, searchArT2.y) + Point2f(matchResultsT2[0].ptCenter.x, matchResultsT2[0].ptCenter.y);
			float matchAngle = matchResultsT2[0].dMatchedAngle;
			cout << "  location ::" << matchLocT2 << endl;
			cout << "angle::" << matchAngle << endl;
		}
		if (!matchResultsT1.size() > 0 || !matchResultsT2.size() > 0)
		{
			drawTextWithBackGround(imgDraw, "Part not located", Point(100, 100), Scalar::all(255), Scalar(0, 0, 200), 4, 2);
			m_dScore = 0.6;
			return 0;
		}
		line(imgDraw, matchLocT1, matchLocT2, Scalar(0, 0, 255), 3, 4);
		cout << "****setup angle ::" << setupAngle << endl;
		cout << "****setup center ::" << setupCenter << endl;
		float angle_t = ai_getLineAngle(matchLocT1, matchLocT2);
		if (angle_t > 180)
		{
			angle_t =  angle_t-360;
		}

		cout << "angle of line::" << angle_t << endl;
		angle_t = angle_t - setupAngle;// 351.06;//initial angle at setup
		cout << "angle current::" << angle_t << endl;
		center = (matchLocT2 + matchLocT1) / 2.0;
		shift = center - setupCenter;// Point(1492, 1208);
		circle(imgDraw, center, 10, Scalar(0, 0, 255), -1);
		//drawTextWithBackGround(imgDraw, cv::format("setup angle::%3.2f curr ang::%3.2f Shift x:%3.2f y%3.2f",setupAngle,angle_t,shift.x*mmppMobile,shift.y*mmppMobile), Point(400, 300), Scalar::all(255), Scalar(0, 0, 200), 3, 2);
		angle = angle_t;
		m_dScore = 0.6;
		return 1;
	}

};
struct fix_locatedData
{
	bool parsed = false;
	bool located = false;
	bool tool_result = false;
	Point locatedCenter = Point(0, 0);
	float locatedAngle = 0;
	Point locationShift = Point(0, 0);

	void resetData()
	{
		parsed = false;
		located = false;
		locatedCenter = Point(0, 0);
		locatedAngle = 0;
		locationShift = Point(0, 0);
		tool_result = false;
	};

};
struct matching_cp
{
	int tool_id = 0;

	int fixType = 1;
	int fixToolRef = -1;
	int fixMode = 0;

	string name = "";
	string templatePath = "";
	Mat templateImage;
	int matchModel_InstNo = -1;
	Rect templateROI;
	Point setup_location = Point(0, 0);
	Rect searchROI;
	int matchTolerance_percent = 60;
	float rotationTol_deg = 3;
	Point2f shiftTol = Point2f(0.5, 0.5);
	Point2f shiftTol_neg = Point2f(0.5, 0.5);
	sheetColour sheet_colour = sheetColour::black;

	int templateThresholdMode = 0;
	//mode 0
	int colourid = 1;
	//mode 1
	int thresholdVal = 150;
	int threshType = 0;
	//mode 2 load gray image
	colourRange colourRangeVals;
	bool result = false;

	fix_locatedData locatedData;
	void addData(int id, string name_a, string path, Rect templateROIa, Rect searchROIa, int matchPercThresh, float rotation_thresh, Point2f shiftTola, Point2f shiftTola_neg, int templThreshMode, int m0_colourida, int m1_thresholdValue, int m1_threshType, int f_type, int f_mode, int f_refNo)
	{
		tool_id = id;
		fixType = f_type;
		fixMode = f_mode;
		fixToolRef = f_refNo;
		name = name_a;
		templatePath = path;
		templateROI = templateROIa;
		searchROI = searchROIa;
		matchTolerance_percent = matchPercThresh;
		rotationTol_deg = rotation_thresh;
		shiftTol = shiftTola;
		shiftTol_neg = shiftTola_neg;
		//sheet_colour = templThreshMode;
		templateThresholdMode = templThreshMode;
		colourid = m0_colourida;
		thresholdVal = m1_thresholdValue;
		threshType = m1_threshType;

		bool colourFound = false;
		colourRangeVals = getColourFromId(colourid, colourFound);
		if (!colourFound)
		{
			cout << "ERROR:: selected colour not found in cpp" << endl;
		}
	}
	int load_fixture()
	{
		templateImage = imread(templatePath, 0);
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
		cout << "LOADING CROI template:::::::::::::::::::::::::::mode::" << templateThresholdMode << endl;
		templateImage = imread(templatePath);
		Mat templSheet;// = imread(pathT2);
		setup_location = ai_get_rectCenter(templateROI);
		//createHSVmask(templateImage, vec_colourRangeHSV[sheet_colour].low, vec_colourRangeHSV[sheet_colour].high, templSheet);
		if (templateThresholdMode == 1)
		{
			//	cout << "condition HSV" << endl;
			createHSVmask(templateImage, colourRangeVals.low, colourRangeVals.high, templSheet);
			//	ai_resShow("mask HSV", templSheet,1);

		}
		else if (templateThresholdMode == 2)
		{
			//	cout << "condition Binary" << endl;
			Mat gray;
			cvtColor(templateImage, gray, COLOR_BGR2GRAY);
			threshold(gray, templSheet, thresholdVal, 255, threshType);
			//	ai_resShow("mask thresh template binary", templSheet, 1);
			//	waitKey(0);
		}
		else
		{
			//	cout << "condition Gray" << endl;
			cvtColor(templateImage, templSheet, COLOR_BGR2GRAY);
			//	ai_resShow("mask gray", templSheet, 1);
		}
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

struct markIns_refData
{
	int refId = 0;
	int instNoGlassTempl = -1;
	int instROItemplate = -1;
	float roiRotAngle = 0;
	int roi_flipMode = 3;//no rotation
	int subPcNo = 1;
	//vector <cls_checkPrintMatch_helper::modelTemplVars> vec_regTGPM = vector <cls_checkPrintMatch_helper::modelTemplVars>();
	vector<int> roiTools_templ_InstNo = vector<int>();

	MarkInsTools markIns_Tools = MarkInsTools();
	vector<LstTool> vec_roiTools = vector<LstTool>();
	vector<LstTool> vec_qrTools = vector<LstTool>();
	vector<LstGroupPrintCheckTool> vec_groupPrintChk = vector<LstGroupPrintCheckTool>();
	vector<LstDatecodeTool> vec_dateCode = vector<LstDatecodeTool>();
	vector<LstTool> vec_weekCode = vector<LstTool>();
	vector<LstTool> vec_mask = vector<LstTool>();

	vector<LstFixtureTool> vec_fixtureCS = vector<LstFixtureTool>();
	vector<LstCroiTool> vec_CROI_CS = vector<LstCroiTool>();
	vector<LstBoundaryGapTool> vec_BoundaryGapCS = vector<LstBoundaryGapTool>();
	vector<LstTool> vec_GrayPresenceCS = vector<LstTool>();

	fixture mainFixtureC1;
	vector <matching_cp> vec_CROITools = vector<matching_cp>();
	vector<boundaryGap> vec_boundaryGap = vector<boundaryGap>();
	vector< GrayPresence> vec_grayPresence = vector< GrayPresence>();
};
vector<markIns_refData> vec_markIns_refInstances = vector<markIns_refData>();
int algorithmLib::Class1::Clear_MarkInspectioData()
{
	vec_markIns_refInstances.clear();
	clearAll_MatchModelInstance();

	return vec_markIns_refInstances.size();
};
System::String^ __clrcall algorithmLib::Class1::Load_MarkInspectioData(System::String^ json_model_data)
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
		obj_markIns_Tools = nlohmann::json::parse(json_model_data_std_string);
	}
	catch (std::exception & ex)
	{
		std::cout << "	 ex.what(); " << ex.what() << std::endl;
	}
	cout << "tools applied c++::" << obj_markIns_Tools.get_applied_tool_cnt() << endl;
	if (obj_markIns_Tools.get_applied_tool_cnt() > 0)
	{

		//add mark insp ref data instance to vector
		markIns_refData inst_refData;


		//cout << "imagePath ROI ::" << obj_markIns_Tools.get_roi_file_name() << endl;
		//cout <<"Rect full roi::"<< markIns_Tools.get_full_roi().getCVrect()<< endl;
		//FullRoi f = obj_markIns_Tools.get_full_roi();
		//Rect r = f.getCVrect();
		//cout << "ROI rect::" << r << endl;
		//cout << "total roi tools::" << obj_markIns_Tools.get_lst_roi_tools().size() << endl;

		//cout << "total Mask tools::" << obj_markIns_Tools.get_lst_mask_tools().size() << endl;
		cout << "total Fixture tools::" << obj_markIns_Tools.get_lst_fixture_tools().size() << endl;
		cout << "total gray tools::" << obj_markIns_Tools.get_lst_gray_presence_tools().size() << endl;
		cout << "total CROI tools::" << obj_markIns_Tools.get_lst_croi_tool().size() << endl;
		cout << "total boundaryGap tools::" << obj_markIns_Tools.get_lst_boundary_gap_tools().size() << endl;

		inst_refData.vec_fixtureCS = obj_markIns_Tools.get_lst_fixture_tools();
		inst_refData.vec_BoundaryGapCS = obj_markIns_Tools.get_lst_boundary_gap_tools();
		inst_refData.vec_CROI_CS = obj_markIns_Tools.get_lst_croi_tool();
		inst_refData.vec_GrayPresenceCS = obj_markIns_Tools.get_lst_gray_presence_tools();



		inst_refData.vec_roiTools = obj_markIns_Tools.get_lst_roi_tools();
		inst_refData.vec_qrTools = obj_markIns_Tools.get_lst_qr_tools();
		inst_refData.vec_groupPrintChk = obj_markIns_Tools.get_lst_group_print_check_tools();
		inst_refData.vec_dateCode = obj_markIns_Tools.get_lst_datecode_tools();
		inst_refData.vec_weekCode = obj_markIns_Tools.get_lst_week_code_tools();
		inst_refData.vec_mask = obj_markIns_Tools.get_lst_mask_tools();
		//load template full glass
		inst_refData.roiRotAngle = obj_markIns_Tools.get_roi_rotation();
		inst_refData.roi_flipMode = obj_markIns_Tools.get_roi_flip_mode();
		cout << "flip mode from json ::" << inst_refData.roi_flipMode << endl;
		inst_refData.refId = obj_markIns_Tools.get_mark_config_id();
		inst_refData.subPcNo = obj_markIns_Tools.get_sub_p_cno();

		if (inst_refData.vec_fixtureCS.size() > 0)
		{
			LstFixtureTool f = inst_refData.vec_fixtureCS[0];

			FullRoi roi1F = f.get_rect_roi_t1();
			Rect roi1 = roi1F.getCVrect();
			FullRoi roi2F = f.get_rect_roi_t2();
			Rect roi2 = roi2F.getCVrect();
			FullRoi s1F = f.get_rect_searc_region_t1();
			Rect searchRegion1 = s1F.getCVrect();
			FullRoi s2F = f.get_rect_searc_region_t2();
			Rect searchRegion2 = s2F.getCVrect();
			float distT = f.get_distance_bw_t1_t2();
			float rot_tol = f.get_rotattion_limit_deg();
			inst_refData.mainFixtureC1.addData(f.get_template_1___name(), f.get_template_2___name(), f.get_match_score_thresh_t1(), f.get_match_score_thresh_t2(), roi1, roi2, searchRegion1, searchRegion2, distT, rot_tol);
			inst_refData.mainFixtureC1.load_fixture();
		}

		for (int m = 0; m < inst_refData.vec_CROI_CS.size(); m++)
		{
			LstCroiTool f = inst_refData.vec_CROI_CS[m];
			//ppppppppppppppppppp
			string toolname = f.get_name();
			string templatePath = f.get_template_name();
			FullRoi roi1F = f.get_rect_roi();
			Rect roi1 = roi1F.getCVrect();

			FullRoi s1F = f.get_search_region();
			Rect searchRegion1 = s1F.getCVrect();

			float matchP = f.get_mathc_score_thresh();

			float rotTh = f.get_rotation_limit();

			CenterLoc pt = f.get_shift_tolerance();
			Point2f shiftTol = pt.getCVpoint();
			CenterLoc pt_n = f.get_shift_tolerance_neg();
			Point2f shiftTol_neg = pt_n.getCVpoint();
			int colourID = f.get_colour_id();
			int templateMode = f.get_mode();

			//fixture ref 
			int id = f.get_id();
			int fixType = f.get_fixtureType();
			int fixMode = f.get_fixture_mode();
			int fixToolRef = f.get_fixtureToolReference();
			//
			matching_cp cp_sheet;

			int m1_threshVal = f.get_threshold();
			int m1_threshType = f.get_threshold_type();

			cp_sheet.addData(id, toolname, templatePath, roi1, searchRegion1, matchP, rotTh, shiftTol, shiftTol_neg, templateMode, colourID, m1_threshVal, m1_threshType, fixType, fixMode, fixToolRef);///no significance of sheet colour enum argument
			inst_refData.vec_CROITools.push_back(cp_sheet);


		}
		for (int k = 0; k < inst_refData.vec_CROITools.size(); k++)
		{
			cout << inst_refData.vec_CROITools[k].templatePath << endl;
			inst_refData.vec_CROITools[k].loadTemplate();
		}

		for (int m = 0; m < inst_refData.vec_BoundaryGapCS.size(); m++)
		{
			LstBoundaryGapTool f = inst_refData.vec_BoundaryGapCS[m];
			string toolname = f.get_name();
			FullRoi s1F = f.get_rect_roi();
			Rect roi = s1F.getCVrect();
			boundaryGap bg;

			FullRoi s1Outer = f.get_rect_roi_outer();
			Rect roiOuter = s1Outer.getCVrect();
			int threshOuter = f.get_threshold_outer();
			int fillPCOuter = f.get_fill_percent_outer();


			bool enMeasure = f.get_enabled_measure();
			bool enOuter = f.get_enabled_outer();

			bg.addData(toolname, enMeasure, enOuter, f.get_gap_left(), f.get_gap_right(), f.get_gap_top(), f.get_gap_bottom(), roi, f.get_threshold(), f.get_threshold_type(), roiOuter, threshOuter, fillPCOuter);
			inst_refData.vec_boundaryGap.push_back(bg);
		}

		for (int m = 0; m < inst_refData.vec_GrayPresenceCS.size(); m++)
		{
			LstTool f = inst_refData.vec_GrayPresenceCS[m];
			string toolname = f.get_name();
			FullRoi s1F = f.get_rect_roi();
			Rect roi = s1F.getCVrect();
			GrayPresence gp;
			float thresh = *f.get_threshold();
			float threshType = *f.get_threshold_type();
			float matchPerc = *f.get_match_percent();

			int threshMode = f.get_mode();
			int colour_ID = f.get_colour_id();

			//fixture ref 
			int id = f.get_id();
			int fixType = f.get_fixtureType();
			int fixMode = f.get_fixture_mode();
			int fixToolRef = f.get_fixtureToolReference();
			//
			cout << "%%%%%%%%%%%%%%%%% thresh type and match perc " << threshType << "  mp  " << matchPerc << endl;
			gp.addData(id, toolname, roi, thresh, threshType, matchPerc, threshMode, colour_ID, fixType, fixMode, fixToolRef);
			inst_refData.vec_grayPresence.push_back(gp);
		}

		//string glassTemplateFile = obj_markIns_Tools.get_glass_template_file_name();
		//if (glassTemplateFile != "")
		//{
		//	Mat glassTempl = imread(glassTemplateFile, 0);
		//	ai_imageDetails(glassTempl, "glass Template read");
		//	//inst_refData.instNoGlassTempl = loadModelTemplate(glassTempl, scaling_for_fastMatch);
		//}

		//load template mark roi
		//string markROITemplateFile = obj_markIns_Tools.get_roi_file_name();
		//if (markROITemplateFile != "")
		//{
		//	Mat roiTempl = imread(markROITemplateFile, 0);
		//	//inst_refData.instROItemplate = loadModelTemplate(roiTempl, scaling_for_fastMatch);
		//	//inst_refData.instROItemplate = loadModelTemplate(roiTempl, 1);
		//	//ai_resShow("template")
		//}


		//for (int m = 0; m < inst_refData.vec_roiTools.size(); m++)
		//{
		//	int templInstNo = -1;
		//	if (inst_refData.vec_roiTools[m].get_template_name())
		//	{
		//		string templateFile = *inst_refData.vec_roiTools[m].get_template_name();
		//		Mat templ = imread(templateFile, 0);
		//		//templInstNo = loadModelTemplate(templ, scaling_for_fastMatch);
		//	}

		//	inst_refData.roiTools_templ_InstNo.push_back(templInstNo);
		//}

		for (int k = 0; k < vec_markIns_refInstances.size(); k++)
		{
			if (vec_markIns_refInstances[k].refId == inst_refData.refId)
			{//eraise instance with same id , then insert
				vec_markIns_refInstances.erase(vec_markIns_refInstances.begin() + k);
			}
		}
		vec_markIns_refInstances.push_back(inst_refData);

		cout << "ref ID was::" << inst_refData.refId << endl;
		//for (int k = 0; k < vec_roiTools.size(); k++)
		//{
		//	LstTool t = vec_roiTools[k];

		//	cout << "id::" << t.get_id() << "   name ::" <<t.get_name() << endl;
		//	cout << t.get_rotation_limit() << endl;
		//	float rotLim = *t.get_rotation_limit();
		//	cout << "int conv Val :" << rotLim << endl;
		//
		//}
		//PerformOcrOnImage();
		//readOCRaspose()
	}
	cout<<"mark instance size*************number of camera models ::"<<vec_markIns_refInstances.size() << endl;

//	//std::cout << json_model_data_std_string << std::endl;
	return "";
}

////toolsRegion


//mobile



//test fn for match_templ_instances
string baseFolder = "mobileFrame";
string imageBaseFolder = "D:/CV/mobile_GMC_elentech/";
//String testimage =  "Image_20240628200059375.bmp";
//String testimage =  "Image_20240628195149133.bmp";
string testimage = "Image_20240628200358649.bmp";
//String testimage = "Image_20240628200506297.bmp";// "Image_20240628200059375.bmp";
//colours



//Scalar blueLow = Scalar(96, 69, 25);
//Scalar blueHigh = Scalar(112, 229, 255);
//
//Scalar redLow = Scalar(3, 143, 32);
//Scalar redHigh = Scalar(11, 223, 253);
//
//Scalar blackLow = Scalar(0, 0, 10);
//Scalar blackHigh = Scalar(150, 43, 19);
//
//Scalar grayLow = Scalar(14, 18, 157);
//Scalar grayHigh = Scalar(21, 39, 212);


char window_name[30] = "HSV Segemtation";
Mat src;
vector<Scalar> blue_pts = vector < Scalar>();
//struct matching_cp
//{
//	string templatePath = "";
//	Mat templateImage;
//	int matchModel_InstNo = -1;
//	Rect templateROI;
//	Point setup_location = Point(0, 0);
//	Rect searchROI;
//	int matchTolerance_percent = 60;
//	float rotationTol_deg = 3;
//	Point2f shiftTol = Point2f(0.5, 0.5);
//	sheetColour sheet_colour = sheetColour::black;
//
//	bool result = false;
//
//	void addData(string path, Rect templateROIa, Rect searchROIa, int matchPercThresh, float rotation_thresh, Point2f shiftTola, sheetColour objColour)
//	{
//		templatePath = path;
//		templateROI = templateROIa;
//		searchROI = searchROIa;
//		matchTolerance_percent = matchPercThresh;
//		rotationTol_deg = rotation_thresh;
//		shiftTol = shiftTola;
//		sheet_colour = objColour;
//
//	}
//	int load_fixture()
//	{
//		cout << "Template path$$$$$$$$$$$$$$$$$$$$$$$$$$ ::" << imageBaseFolder + templatePath << endl;
//		templateImage = imread(imageBaseFolder + templatePath, 0);
//		Mat templSheet;// = imread(pathT2);
//		setup_location = ai_get_rectCenter(templateROI);
//		matchModel_InstNo = get_MatchModelInstance();
//		int resp = -2;
//		resp = learn_MatchModelInstance(matchModel_InstNo, templateImage);
//		if (resp == 1)
//		{
//			cout << "leaarned template " << templatePath << " sucecssfully . instno :: " << matchModel_InstNo << endl;
//		}
//		else
//		{
//
//			cout << "Failed to leaarn template " << templatePath << endl;
//		}
//		return resp;
//	}
//	int loadTemplate()
//	{
//		cout << "Template path$$$$$$$$$$$$$$$$$$$$$$$$$$ ::" << imageBaseFolder + templatePath << endl;
//		templateImage = imread(imageBaseFolder + templatePath);
//		Mat templSheet;// = imread(pathT2);
//		setup_location = ai_get_rectCenter(templateROI);
//		createHSVmask(templateImage, vec_colourRangeHSV[sheet_colour].low, vec_colourRangeHSV[sheet_colour].high, templSheet);
//		matchModel_InstNo = get_MatchModelInstance();
//		int resp = -2;
//		resp = learn_MatchModelInstance(matchModel_InstNo, templSheet);
//		if (resp == 1)
//		{
//			cout << "leaarned template " << templatePath << " sucecssfully . instno :: " << matchModel_InstNo << endl;
//		}
//		else
//		{
//
//			cout << "Failed to leaarn template " << templatePath << endl;
//		}
//		return resp;
//	}
//
//};
//vector <matching_cp> vec_fixture = vector<matching_cp>();
//vector <matching_cp> vec_matchingTools = vector<matching_cp>();

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

Rect transformRect_centerOnly(Rect rectIn, float rotAngle, Point center, Point shift)
{
	Point centSearchROI = ai_get_rectCenter(rectIn);
	Mat inv_rotMat = cv::getRotationMatrix2D(center, rotAngle, 1.0);
	Point shiftedCent = transformPoint_shift_rot_Point(centSearchROI,inv_rotMat,shift);
	return ai_getRect_from_cent_size(shiftedCent, rectIn.size());
}
Rect transformRect_centerOnlyFloat(Rect rectIn, float rotAngle, Point2f center, Point2f shift)
{
	Point2f centSearchROI = ai_get_rectCenter(rectIn);
	Mat inv_rotMat = cv::getRotationMatrix2D(center, rotAngle, 1.0);
	Point2f shiftedCent = transformPoint_shift_rot_Point(centSearchROI, inv_rotMat, shift);
	return ai_getRect_from_cent_size(shiftedCent, rectIn.size());
}

Point2f transformRect_Point(Rect rectIn, float rotAngle, Point2f center, Point2f shift)
{
	Point2f centROI = ai_get_rectCenterFloat(rectIn);
	Mat inv_rotMat = cv::getRotationMatrix2D(center, rotAngle, 1.0);
	return transformPoint_shift_rot_PointFloat(centROI, inv_rotMat, shift);
}

vector<Point> transformAndDraw_Rect(Mat& image, Rect rectIn, float rotAngle, Point center, Point shift)
{
	vector<Point> rectPts = { Point(rectIn.x,rectIn.y),Point(rectIn.x + rectIn.width,rectIn.y), Point(rectIn.x + rectIn.width,rectIn.y + rectIn.height),Point(rectIn.x,rectIn.y + rectIn.height) };
	Mat inv_rotMat = cv::getRotationMatrix2D(center, rotAngle, 1.0);
	vector<Point> vecTrx = transformPtVector_shift_rot(rectPts, inv_rotMat, shift);
	//polylines(image, vecTrx, true, Scalar(0, 165, 255), 1);

	return vecTrx;
	//line(image, transformPoint_rot(fixedFeature1, inv_rotMat), transformPoint_rot(fixedFeature2, inv_rotMat), Scalar(0, 200, 0), 2);
	//line(image, transformPoint_shift_rot(fixedFeature1, inv_rotMat, shift), transformPoint_shift_rot(fixedFeature2, inv_rotMat, shift), Scalar(0, 200, 0), 2);
}
vector<Point> transformAndDraw_RotRect(Mat& image, vector<Point> rectPts, float rotAngle, Point center, Point shift)
{
	//vector<Point> rectPts = { Point(rectIn.x,rectIn.y),Point(rectIn.x + rectIn.width,rectIn.y), Point(rectIn.x + rectIn.width,rectIn.y + rectIn.height),Point(rectIn.x,rectIn.y + rectIn.height) };
	Mat inv_rotMat = cv::getRotationMatrix2D(center, rotAngle, 1.0);
	vector<Point> vecTrx = transformPtVector_shift_rot(rectPts, inv_rotMat, shift);
	//polylines(image, vecTrx, true, Scalar(0, 165, 255), 1);

	return vecTrx;
	//line(image, transformPoint_rot(fixedFeature1, inv_rotMat), transformPoint_rot(fixedFeature2, inv_rotMat), Scalar(0, 200, 0), 2);
	//line(image, transformPoint_shift_rot(fixedFeature1, inv_rotMat, shift), transformPoint_shift_rot(fixedFeature2, inv_rotMat, shift), Scalar(0, 200, 0), 2);
}

Point2f transformBackPointF(Point2f inPt, float rotAngle, Point2f center, Point2f shift)
{
	Mat rot_mat = cv::getRotationMatrix2D(center, rotAngle, 1.0);
	cv::Point2f result;
	result.x = rot_mat.at<double>(0, 0) * inPt.x + rot_mat.at<double>(0, 1) * inPt.y + rot_mat.at<double>(0, 2);
	result.y = rot_mat.at<double>(1, 0) * inPt.x + rot_mat.at<double>(1, 1) * inPt.y + rot_mat.at<double>(1, 2);
	 return result + shift;


	//return transformPoint_shift_rot(inPt, inv_rotMat, shift);
}

void transformAndDraw(Mat& image, float rotAngle, Point center, Point shift)
{
	Mat inv_rotMat = cv::getRotationMatrix2D(center, rotAngle, 1.0);
	//line(image, transformPoint_rot(fixedFeature1, inv_rotMat), transformPoint_rot(fixedFeature2, inv_rotMat), Scalar(0, 200, 0), 2);
	line(image, transformPoint_shift_rot(fixedFeature1, inv_rotMat, shift), transformPoint_shift_rot(fixedFeature2, inv_rotMat, shift), Scalar(0, 200, 0), 2);
}

bool checkChildPartThresh(Mat imageGray, int thresh, cv::ThresholdTypes cvThreshType, int pixelPercentage, int& measuredPercent)
{
	threshold(imageGray, imageGray, thresh, 255, cvThreshType);// THRESH_BINARY);
	double pixCnt = countNonZero(imageGray);
	//ai_resShow("pix thresh", imageGray, 4);
	measuredPercent = pixCnt * 100 / (imageGray.cols * imageGray.rows);
	return ((pixCnt * 100 / (imageGray.cols * imageGray.rows)) > pixelPercentage);
}

vector<bool> checkChildPartGaps(Mat imageGray, int thresh, cv::ThresholdTypes cvThreshType, string regionName, vector<float>& vec_gaps, vector<float> vec_gapLimits, bool& resultAll_return)
{
	vector<bool> returnVecResult = vector<bool>(4, false);
	resultAll_return = false;
	//Mat thresh;
	vec_gaps = { (float)imageGray.cols,(float)imageGray.cols,(float)imageGray.rows,(float)imageGray.rows };
	threshold(imageGray, imageGray, thresh, 255, cvThreshType);// THRESH_BINARY);
	int pixForAvg = 8;
	int half = pixForAvg / 2;
	if (imageGray.cols < pixForAvg + 2 || imageGray.rows < pixForAvg + 2)
	{
		return returnVecResult; //invalid region
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
	int grayThresh = 127;
	//for (int x = 0; x < row_mean.cols - 1; x++)
	for (int x = 0; x < row_mean.cols / 2; x++)
	{
		//cout << row_mean.at<uchar>(0, x) << endl;
		if ((row_mean.at<uchar>(0, x) > grayThresh && !startFlag) || (row_mean.at<uchar>(0, x) > grayThresh && row_mean.at<uchar>(0, x + 1) > grayThresh))
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
	//for (int x = row_mean.cols - 1; x > breakVal; x--)
	for (int x = row_mean.cols - 1; x > row_mean.cols / 2; x--)
	{
		//cout << row_mean.at<uchar>(0, x) << endl;
		if ((row_mean.at<uchar>(0, x) > grayThresh && !startFlag) || (row_mean.at<uchar>(0, x) > grayThresh && row_mean.at<uchar>(0, x - 1) > grayThresh))
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
	//for (int x = 0; x < col_mean.rows - 1; x++)
	for (int x = 0; x < col_mean.rows / 2; x++)
	{
		//cout << col_mean.at<uchar>(x, 0) << endl;
		if ((col_mean.at<uchar>(x, 0) > grayThresh && !startFlag) || (col_mean.at<uchar>(x, 0) > grayThresh && col_mean.at<uchar>(x + 1, 0) > grayThresh))
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
	//for (int x = col_mean.rows - 1; x > breakVal; x--)
	for (int x = col_mean.rows - 1; x > col_mean.rows / 2; x--)
	{
		//cout << col_mean.at<uchar>(x, 0) << endl;
		if ((col_mean.at<uchar>(x, 0) > grayThresh && !startFlag) || (col_mean.at<uchar>(x, 0) > grayThresh  && col_mean.at<uchar>(x - 1, 0) > grayThresh))
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

	returnVecResult[0] = vec_gaps[0] <= vec_gapLimits[0];
	returnVecResult[1] = vec_gaps[1] <= vec_gapLimits[1];
	returnVecResult[2] = vec_gaps[2] <= vec_gapLimits[2];
	returnVecResult[3] = vec_gaps[3] <= vec_gapLimits[3];


	resultAll_return = (returnVecResult[0] && returnVecResult[1] && returnVecResult[2] && returnVecResult[3]);
	return returnVecResult;
}

vector<bool>  checkOutOfBoxThresh(Mat imageGray, Rect roiOuter, Rect roiInner, int thresh, cv::ThresholdTypes cvThreshType, int pixelPercentage, bool& finalResult)
{
	vector<bool> returnVecResult = vector<bool>(4, false);
	vector<double> pixCnt = vector<double>(4, 0);
	Rect left = Rect(0, 0, abs(roiOuter.x - roiInner.x), roiOuter.height);// abs(roiOuter.y - roiInner.y));
	Rect right = Rect(abs(roiOuter.x - roiInner.x) + roiInner.width, 0, abs(roiOuter.x + roiOuter.width - (roiInner.x + roiInner.width)), roiOuter.height);
	Rect top = Rect(0, 0, roiOuter.width, abs(roiOuter.y - roiInner.y));
	Rect bottom = Rect(0, abs(roiOuter.y - roiInner.y) + roiInner.height, roiOuter.width, abs(roiOuter.y + roiOuter.height - (roiInner.y + roiInner.height)));
	//cout << "Left::" << left << endl;
	//cout << "Rt::" << right << endl;
	//cout << "Top::" << top << endl;
	//cout << "Bt::" << bottom << endl;
	threshold(imageGray, imageGray, thresh, 255, cvThreshType);// THRESH_BINARY);

//	double pixCnt = countNonZero(imageGray);
	pixCnt[0] = countNonZero(imageGray(left));
	pixCnt[1] = countNonZero(imageGray(top));
	pixCnt[2] = countNonZero(imageGray(right));
	pixCnt[3] = countNonZero(imageGray(bottom));

	returnVecResult[0] = ((pixCnt[0] * 100 / (left.area())) > pixelPercentage);
	returnVecResult[1] = ((pixCnt[1] * 100 / (top.area())) > pixelPercentage);
	returnVecResult[2] = ((pixCnt[2] * 100 / (right.area())) > pixelPercentage);
	returnVecResult[3] = ((pixCnt[3] * 100 / (bottom.area())) > pixelPercentage);

	/*if ((pixCnt[0] * 100 / (left.area()))> pixelPercentage)
	{
		returnVecResult[0] = true;
	}
	else
	{
		returnVecResult[0] = false;
	}

	if ((pixCnt[1] * 100 / (right.area())) >= pixelPercentage)
	{
		returnVecResult[1] = true;
	}
	else
	{
		returnVecResult[1] = false;
	}
	if ((pixCnt[2] * 100 / (top.area())) >= pixelPercentage)
	{
		returnVecResult[2] = true;
	}
	else
	{
		returnVecResult[2] = false;
	}
	if ((pixCnt[3] * 100 / (bottom.area())) >= pixelPercentage)
	{
		returnVecResult[3] = true;
	}else
	{
		returnVecResult[3] = false;
	}*/
	//ai_resShow("pix thresh", imageGray, 4);
	//Mat draw;
	//cvtColor(imageGray, draw, COLOR_GRAY2BGR);
	//rectangle(draw, left, Scalar(0, 255*returnVecResult[0],(1- returnVecResult[0])*255), 2);
	//cout << "val " << returnVecResult[0] << "   pc:" << (pixCnt[0] * 100 / (left.area())) << "  area ::" << left.area() <<"cnt::"<< pixCnt[0] << endl;
	//ai_resShow("rects", draw, 4);
	//waitKey();
	//rectangle(draw, right, Scalar(0, 255 * returnVecResult[1], (1 - returnVecResult[1]) * 255), 2);
	//cout << "val " << returnVecResult[1] << "   pc:" << (pixCnt[1] * 100 / (right.area())) << "  area ::" << right.area() << "cnt::" << pixCnt[1] << endl;
	//ai_resShow("rects", draw, 4);
	//waitKey();
	//rectangle(draw, top, Scalar(0, 255 * returnVecResult[2], (1 - returnVecResult[2]) * 255), 2);
	//cout << "val " << returnVecResult[2] << "   pc:" << (pixCnt[2] * 100 / (top.area())) << "  area ::" << top.area() << "cnt::" << pixCnt[2] << endl;
	//ai_resShow("rects", draw, 4);
	//waitKey();
	//rectangle(draw, bottom, Scalar(0, 255 * returnVecResult[3], (1 - returnVecResult[3]) * 255), 2);
	//cout << "val " << returnVecResult[3] << "   pc:" << (pixCnt[3] * 100 / (bottom.area())) << "  area ::" << bottom.area() << "cnt::" << pixCnt[3] << endl;
	//ai_resShow("rects", draw, 4);
	//waitKey();
	finalResult = returnVecResult[0] && returnVecResult[1] && returnVecResult[2] && returnVecResult[3];
	return returnVecResult;// ((pixCnt * 100 / (imageGray.cols * imageGray.rows)) > pixelPercentage);
}

//int testTemplMatchFast(Mat& imageColour, bool& resultRet)
//{
//	resultRet = true;
//	polygon = { Point(344,824),Point(515,824) ,Point(591,863) ,Point(628,941) ,Point(628,1085),Point(516,1085),Point(344,960) };
//
//
//	Mat imageIn;
//	//Mat imageColour = imread(path);
//	cvtColor(imageColour, imageIn, COLOR_BGR2GRAY);
//	Mat imgDraw = imageColour;// .clone();
//
//	m_bDebugMode = false;
//
//	matching_cp fixture1 = vec_fixture[0];
//	matching_cp fixture2 = vec_fixture[1];
//
//	int matchResp = -2;
//
//	Point matchLocT1 = Point(0, 0);
//	Point matchLocT2 = Point(0, 0);
//	Point matchLocTSheet = Point(0, 0);
//	vector<s_SingleTargetMatch> matchResultsT1 = findMatch_MatchModelInstance(fixture1.matchModel_InstNo, imageIn(fixture1.searchROI), imgDraw(fixture1.searchROI), &matchResp);
//	if (matchResultsT1.size() > 0)
//	{
//		matchLocT1 = Point(fixture1.searchROI.x, fixture1.searchROI.y) + Point(matchResultsT1[0].ptCenter.x, matchResultsT1[0].ptCenter.y);
//		float matchAngle = matchResultsT1[0].dMatchedAngle;
//		cout << "  location ::" << matchLocT1 << endl;
//		cout << "angle::" << matchAngle << endl;
//	}
//	matchResp = -2;
//	vector<s_SingleTargetMatch> matchResultsT2 = findMatch_MatchModelInstance(fixture2.matchModel_InstNo, imageIn(fixture2.searchROI), imgDraw(fixture2.searchROI), &matchResp);
//	if (matchResultsT2.size() > 0)
//	{
//		matchLocT2 = Point(fixture2.searchROI.x, fixture2.searchROI.y) + Point(matchResultsT2[0].ptCenter.x, matchResultsT2[0].ptCenter.y);
//		float matchAngle = matchResultsT2[0].dMatchedAngle;
//		cout << "  location ::" << matchLocT2 << endl;
//		cout << "angle::" << matchAngle << endl;
//	}
//	if (!matchResultsT1.size() > 0 || !matchResultsT2.size() > 0)
//	{
//		drawTextWithBackGround(imgDraw, "NG. Part not located", Point(100, 100), Scalar::all(255), Scalar(0, 0, 200), 2, 2);
//		resultRet = false;
//		return 0;
//	}
//	line(imgDraw, matchLocT1, matchLocT2, Scalar(0, 0, 255), 2, 4);
//
//	float angle = ai_getLineAngle(matchLocT1, matchLocT2);
//	cout << "angle of line::" << angle << endl;
//	angle = angle - 351.06;//initial angle at setup
//	cout << "angle current::" << angle << endl;
//	Point center = (matchLocT2 + matchLocT1) / 2;
//	Point shift = center - Point(1492, 1208);
//
//	cout << "________________________center::" << center << endl;
//	cout << "shift::" << shift << endl;
//	circle(imgDraw, center, 4, Scalar(0, 255, 0), -1);
//
//	//----------child parts
//	for (int k = 0; k < vec_matchingTools.size(); k++)
//	{
//		matching_cp cp = vec_matchingTools[k];
//		Point offset = Point(1492, 1208) - vec_matchingTools[k].setup_location;// Point(760, 718);
//		matchResp = -2;
//		Mat imgRoiSheet;
//		//createHSVmask(templateImage, colourRangeVals.low, colourRangeVals.high, templSheet);
//		//createHSVmask(imageColour(vec_matchingTools[k].searchROI), vec_colourRangeHSV[vec_matchingTools[k].sheet_colour].low, vec_colourRangeHSV[vec_matchingTools[k].sheet_colour].high, imgRoiSheet);
//		createHSVmask(imageColour(vec_matchingTools[k].searchROI),cp.colourRangeVals.low,cp.colourRangeVals.high,imgRoiSheet);
//		//ai_resShow(std::to_string(k), imgRoiSheet, 1);
//		vector<s_SingleTargetMatch> matchResultsTSheet = findMatch_MatchModelInstance(vec_matchingTools[k].matchModel_InstNo, imgRoiSheet, imgDraw(vec_matchingTools[k].searchROI), &matchResp);
//		if (matchResultsTSheet.size() > 0)
//		{
//			matchLocTSheet = Point(vec_matchingTools[k].searchROI.x, vec_matchingTools[k].searchROI.y) + Point(matchResultsTSheet[0].ptCenter.x, matchResultsTSheet[0].ptCenter.y);
//			float matchAngle = matchResultsTSheet[0].dMatchedAngle;
//			float angleWRTfixture = matchAngle - angle;
//			cout << "  location sheet::" << matchLocTSheet << endl;
//			cout << "angle sheet::" << matchAngle << endl;
//			Point shift = offset - (center - matchLocTSheet);
//			Point2f shiftmm = Point2f(shift.x * mmppMobile, shift.y * mmppMobile);
//			cout << "sheet shift =" << shift << endl;
//			drawTextWithBackGround(imgDraw, cv::format("A:%3.2f", angleWRTfixture), ai_get_rectCenter(vec_matchingTools[k].searchROI), Scalar::all(255), Scalar(200, 0, 0), 2, 2);
//			//drawTextWithBackGround(imgDraw, cv::format("Sh x:%3.1f,y:%3.1f", shift.x,shift.y), ai_get_rectCenter(roiSheet)+Point(0,90), Scalar::all(255), Scalar(200, 0, 0), 4, 2);
//			if (abs(shiftmm.x) <= cp.shiftTol.x && abs(shiftmm.y) <= cp.shiftTol.y && abs(angleWRTfixture) <= cp.rotationTol_deg)
//			{
//				drawTextWithBackGround(imgDraw, cv::format("Sh x:%3.1f,y:%3.1f", shiftmm.x, shiftmm.y), ai_get_rectCenter(vec_matchingTools[k].searchROI) + Point(0, 90), Scalar::all(255), Scalar(0, 200, 0), 2, 2);
//				vec_matchingTools[k].result = true;
//			}
//			else
//			{
//				drawTextWithBackGround(imgDraw, cv::format("S x:%3.1f,y:%3.1f", shiftmm.x, shiftmm.y), ai_get_rectCenter(vec_matchingTools[k].searchROI) + Point(0, 90), Scalar::all(255), Scalar(0, 0, 200), 2, 2);
//				vec_matchingTools[k].result = false;
//				cout << "match****************************tool NG::" << cp.templatePath << "index::" << k << endl;
//				resultRet = false;
//			}
//		}
//		else
//		{
//			drawTextWithBackGround(imgDraw, "Miss", ai_get_rectCenter(vec_matchingTools[k].searchROI), Scalar::all(255), Scalar(0, 0, 200), 4, 2);
//			vec_matchingTools[k].result = false;
//			cout << "match****************************tool NG::" << cp.templatePath << "index::" << k << endl;
//			resultRet = false;
//		}
//		transformAndDraw(imgDraw, -1 * angle, center, shift);
//		//	transformAndDrawPolygon(imgDraw, -1 * angle, center, shift);
//	}
//	//Expected location sheet
//	//Point offset = Point(1492, 1208) -Point(760, 718);
//	//matchResp = -2;
//	//Mat imgRoiSheet;
//	//createHSVmask(imageColour(roiSheet), blueLow, blueHigh, imgRoiSheet);
//	//ai_resShow("roiTsheetMask", imgRoiSheet, 1);
//	//vector<s_SingleTargetMatch> matchResultsTSheet = findMatch_MatchModelInstance(instNoTSheet, imgRoiSheet, imgDraw(roiSheet), &matchResp);
//	//if (matchResultsTSheet.size() > 0)
//	//{
//	//	matchLocTSheet = Point(roiSheet.x, roiSheet.y) + Point(matchResultsTSheet[0].ptCenter.x, matchResultsTSheet[0].ptCenter.y);
//	//	float matchAngle = matchResultsTSheet[0].dMatchedAngle;
//	//	cout << "  location sheet::" << matchLocTSheet << endl;
//	//	cout << "angle sheet::" << matchAngle << endl;
//	//	Point shift = offset-( center - matchLocTSheet);
//	//	cout << "sheet shift =" << shift << endl;
//	//	drawTextWithBackGround(imgDraw, cv::format("A:%3.2f", matchAngle + angle), ai_get_rectCenter(roiSheet), Scalar::all(255), Scalar(200, 0, 0), 2, 2);
//	//	//drawTextWithBackGround(imgDraw, cv::format("Sh x:%3.1f,y:%3.1f", shift.x,shift.y), ai_get_rectCenter(roiSheet)+Point(0,90), Scalar::all(255), Scalar(200, 0, 0), 4, 2);
//	//	drawTextWithBackGround(imgDraw, cv::format("Shift x:%d,y:%d", shift.x,shift.y), ai_get_rectCenter(roiSheet)+Point(0,90), Scalar::all(255), Scalar(200, 0, 0), 2, 2);
//	//}
//	//else
//	//{
//	//	drawTextWithBackGround(imgDraw, "Sheet Missing", ai_get_rectCenter(roiSheet), Scalar::all(255), Scalar(0, 0, 200), 4, 2);
//	//}
//	//cout << "matches found Sheet:: " << matchResultsTSheet.size() << endl;
//	//cout << "response to match instance Sheet=" << matchResp << endl;
//	////	//child parts
//
//	//transformAndDraw(imgDraw, -1*angle, center,shift);
//	//transformAndDrawPolygon(imgDraw, -1 * angle, center, shift);
//
//	for (int k = 0; k < rect_gapMeasure.size(); k++)
//	{
//		vector<Point>  rotPts = transformAndDraw_Rect(imgDraw, rect_gapMeasure[k], -1 * angle, center, shift);
//		//vector<Point> rectPts = { Point(0,0),Point(rect_gapMeasure[k].width,0), Point(rect_gapMeasure[k].width,rect_gapMeasure[k].height),Point(0, rect_gapMeasure[k].height) };
//		//Mat h = findHomography(rotPts, rectPts, RANSAC);
//		//Mat cropImg = Mat::zeros(rect_gapMeasure[k].size(), imgDraw.type());
//		//// Use homography to warp image
//		//warpPerspective(imgDraw, cropImg, h, cropImg.size());
//		Mat cropImg = ai_cropRotatedRectFromImage(rotPts, rect_gapMeasure[k], imageIn);
//
//		//		Mat warp_mat = getAffineTransform(rotPts, rectPts);
//
//				//warpAffine(imgDraw, cropImg, warp_mat, rect_gapMeasure[k].size());
//		vector<float> vec_GapsMeasured;
//		vector<float> vec_gapLimits = { 0.8,0.8,0.8,0.8 };
//		Mat presenceClone = cropImg.clone();
//		bool result = checkChildPartGaps(cropImg, 230, THRESH_BINARY, to_string(k), vec_GapsMeasured, vec_gapLimits);
//		bool resultPresence = checkChildPartThresh(presenceClone, 230, THRESH_BINARY_INV, 70);
//		result = resultPresence;
//		cout << "measured gaps vec::::" << format(" L=%3.2f R=%3.2f T=%3.2f B=%3.2f", vec_GapsMeasured[0], vec_GapsMeasured[1], vec_GapsMeasured[2], vec_GapsMeasured[3]) << endl;
//		if (result)
//		{
//			circle(imgDraw, rotPts[0], 8, Scalar(0, 255, 0), -1);
//		}
//		else
//		{
//			circle(imgDraw, rotPts[0], 8, Scalar(0, 0, 255), -1);
//			resultRet = false;
//		}
//		//imshow("CropWarp", cropImg);
//		//	waitKey();
//	}
//	for (int k = 0; k < gray_vectors.size(); k++)
//	{
//		vector<Point>  rotPts = transformAndDraw_Rect(imgDraw, gray_vectors[k], -1 * angle, center, shift);
//		//vector<Point> rectPts = { Point(0,0),Point(rect_gapMeasure[k].width,0), Point(rect_gapMeasure[k].width,rect_gapMeasure[k].height),Point(0, rect_gapMeasure[k].height) };
//		//Mat h = findHomography(rotPts, rectPts, RANSAC);
//		//Mat cropImg = Mat::zeros(rect_gapMeasure[k].size(), imgDraw.type());
//		//// Use homography to warp image
//		//warpPerspective(imgDraw, cropImg, h, cropImg.size());
//		Mat cropImg = ai_cropRotatedRectFromImage(rotPts, gray_vectors[k], imageIn);
//
//		//		Mat warp_mat = getAffineTransform(rotPts, rectPts);
//
//				//warpAffine(imgDraw, cropImg, warp_mat, rect_gapMeasure[k].size());
//
//
//
//		bool result = checkChildPartThresh(cropImg, 230, THRESH_BINARY_INV, 70);
//
//
//		if (result)
//		{
//			circle(imgDraw, rotPts[0], 8, Scalar(0, 255, 0), -1);
//		}
//		else
//		{
//			circle(imgDraw, rotPts[0], 8, Scalar(0, 0, 255), -1);
//			resultRet = false;
//		}
//		//imshow("CropWarp", cropImg);
//		//	waitKey();
//	}
//
//
//	drawTextWithBackGround(imgDraw, cv::format("A:%3.2f", angle), center + Point(0, -90), Scalar::all(255), Scalar(200, 0, 0), 4, 2);
//	//ai_resShow("detections", imgDraw, 0.4);
//	//imwrite("baseFolder/templateMatch_detections.bmp", imgDraw);
//
//	//waitKey();
//	return 1;
//}
//
//colour range
struct setColourCPP_CS
{
	Mat image;
	Mat mat_hsv;
	colourRange rangeTemp;
	vector<Scalar> colourVals = vector<Scalar>();
	Mat mask;
};
setColourCPP_CS colourProcessHSV;
//reset 1== load image, 2= undo point 0= normal operation
//int algorithmLib::Class1::calculateColourRange(int reset, System::Drawing::Bitmap^ in, System::Drawing::Bitmap^ out, int x, int y, int^ hL, int^ sL, int^ vL)
int algorithmLib::Class1::calculateColourRange(int reset, System::Drawing::Bitmap^ in, System::Drawing::Bitmap^ out, int x, int y, int colourSpread, int& hL, int& sL, int& vL, int& hH, int& sH, int& vH)
{
	if (reset == 1)
	{
		colourProcessHSV.image = BitmapToMat(in).clone();
		cvtColor(colourProcessHSV.image, colourProcessHSV.mat_hsv, COLOR_BGR2HSV);
		colourProcessHSV.colourVals.clear();
		return 1;
	}
	else
	{
		if (colourProcessHSV.colourVals.size() > 0 && reset == 2)
		{
			colourProcessHSV.colourVals.pop_back();
			//erase(colourProcessHSV.colourVals.size()-1);
		}

		if (colourProcessHSV.mat_hsv.size().width > 0)
		{
			Mat returnImg = BitmapToMat(out);
			if (reset == 0)
			{
				Vec3b hsv = colourProcessHSV.mat_hsv.at<Vec3b>(y, x);
				int H = hsv.val[0];
				int S = hsv.val[1];
				int V = hsv.val[2];

				colourProcessHSV.colourVals.push_back(Scalar(hsv.val[0], hsv.val[1], hsv.val[2]));
				cout << format("cpp colours (%d,%d) h=%d s=%d v=%d", x, y, H, S, V) << endl;
			}
			Scalar valMin;
			Scalar valMax;

			if (colourProcessHSV.colourVals.size() > 1)
			{
				createHSVLimits(colourProcessHSV.colourVals, true, valMin, valMax);
				valMin = Scalar(valMin[0] - colourSpread, valMin[1] - colourSpread, valMin[2] - colourSpread);
				valMax = Scalar(valMax[0] + colourSpread, valMax[1] + colourSpread, valMax[2] + colourSpread);
				hL = valMin[0];
				sL = valMin[1];
				vL = valMin[2];

				hH = valMax[0];
				sH = valMax[1];
				vH = valMax[2];

				createHSVmask(colourProcessHSV.image, valMin, valMax, colourProcessHSV.mask);
				Mat returnRGB;
				cvtColor(colourProcessHSV.mask, returnRGB, COLOR_GRAY2BGR);
				resize(returnRGB, returnImg, returnImg.size());
			}
			return 1;
		}
		else
		{
			return 0;
		}
	}

}
int algorithmLib::Class1::maskHSVpreview(System::Drawing::Bitmap^ in, System::Drawing::Bitmap^ out, int hL, int sL, int vL, int hH, int sH, int vH)
{
	if (colourProcessHSV.image.size().width > 0)
	{
		Mat returnImg = BitmapToMat(out);
		Scalar valMin = Scalar(hL, sL, vL);
		Scalar valMax = Scalar(hH, sH, vH);
		createHSVmask(colourProcessHSV.image, valMin, valMax, colourProcessHSV.mask);
		Mat returnRGB;
		cvtColor(colourProcessHSV.mask, returnRGB, COLOR_GRAY2BGR);
		resize(returnRGB, returnImg, returnImg.size());

		return 1;
	}
	return 0;
}
int algorithmLib::Class1::thresholdImagePreview(int threshVal, System::Drawing::Bitmap^ in, System::Drawing::Bitmap^ out, int threshType)
{
	//Mat inImg = BitmapToMat(in).clone();
	if (colourProcessHSV.image.size().width > 0)
	{

		Mat returnImg = BitmapToMat(out);
		Mat mat_thresh;
		cvtColor(colourProcessHSV.image, mat_thresh, COLOR_BGR2GRAY);
		//	threshold(colourProcessHSV.image, mat_thresh, threshVal, 255, threshType);

		threshold(mat_thresh, mat_thresh, threshVal, 255, threshType);
		//cout << "mat thresh size::" << mat_thresh.size() << "    channels::" << mat_thresh.channels() << endl;
		Mat returnRGB;
		cvtColor(mat_thresh, returnRGB, COLOR_GRAY2BGR);
		resize(returnRGB, returnImg, returnImg.size());
		return 1;
	}
	return 0;
}
//colour range
//main fn
resultFrontCam::ListDefectDetail setResultTojson(Rect roiRect, string toolName, bool result)
{
	resultFrontCam::ListDefectDetail tempResult;
	resultFrontCam::Roi roi;
	//roi.setCVrect(Rect(10, 10, imageColour.cols - 20, imageColour.rows - 20));
	roi.setCVrect(roiRect);
	tempResult.set_cv_rect(roi);
	tempResult.set_result(result);
	tempResult.set_defect_name(toolName);
	return tempResult;
}
int testTemplMatchFast(Mat& imageColour, bool& resultRet,int camCode,bool testMode , vector <resultFrontCam::ListDefectDetail>& vec_defectTools)// vector<string> &vec_defectTools)
{
	int ngToolCnt = 0;
	cout << "vec_markIns_refInstances size ::" << vec_markIns_refInstances.size() << "   current index::" <<camCode << endl;
	markIns_refData d = vec_markIns_refInstances[camCode];
	resultRet = true;
	//polygon = { Point(344,824),Point(515,824) ,Point(591,863) ,Point(628,941) ,Point(628,1085),Point(516,1085),Point(344,960) };
	/*resultFrontCam::ListDefectDetail tempResult;
	resultFrontCam::Roi roi;
	roi.setCVrect(Rect(10, 10, imageColour.cols - 20, imageColour.rows - 20));
	tempResult.set_cv_rect(roi);
	tempResult.set_result(false);
	tempResult.set_defect_name("");*/
	
	Mat imageIn;
	//Mat imageColour = imread(path);
	cvtColor(imageColour, imageIn, COLOR_BGR2GRAY);
	Mat imageRGB = imageColour.clone();
	Mat imgDraw = imageColour;// .clone();
	
	m_bDebugMode = false;



	Point2f shiftF = Point2f(0, 0);
	Point2f centerF = Point2f(0, 0);
	float angleF = 0;


	 int FixtureResp =d.mainFixtureC1.locatePartFixture(imageIn, imgDraw, angleF, centerF, shiftF);
	 if (FixtureResp == 0)
	 {
		 resultRet = true;//false

		// vec_defectTools.push_back("Unable to locate ROI");
		 vec_defectTools.push_back(setResultTojson( Rect(10, 10, imageColour.cols - 20, imageColour.rows - 20),"Unable to locate ROI", true)); // code changed false => true
		//cout << "data pushed in defect vector:: ROI defect::" << vec_defectTools[vec_defectTools.size() - 1] << endl;
	 }
	
	/*cout << "angle of line::" << angleF << endl;
	cout << "________________________center::" << centerF << endl;
	cout << "center::" << centerF << endl;
	cout << "shift::" << shiftF << endl;*/
	Point2f shiftFixture = Point2f(shiftF.x, shiftF.y);
	Point center = Point(centerF.x, centerF.y);
	float angle = angleF;
	//----------child parts
	int matchResp;
	Point2f matchLocTSheet = Point2f(0, 0);
	for (int k = 0; k < d.vec_CROITools.size(); k++)
	{
		cout << "))))))))))))))))))))))))))) processing CROI)))))))))--------------------                    " << k << endl;
		matching_cp cp = d.vec_CROITools[k];
		d.vec_CROITools[k].locatedData.resetData();
		fix_locatedData locatedData_Curr;
		locatedData_Curr.parsed = true;
		locatedData_Curr.locatedCenter = d.vec_CROITools[k].setup_location;
		//Point offset = Point(1492, 1208) - d.vec_CROITools[k].setup_location;// Point(760, 718);
		Point offset = Point(d.mainFixtureC1.setupCenter.x, d.mainFixtureC1.setupCenter.y) - d.vec_CROITools[k].setup_location;// Point(760, 718);
		matchResp = -2;
		Mat imgRoiSheet;
		//createHSVmask(templateImage, colourRangeVals.low, colourRangeVals.high, templSheet);
		//createHSVmask(imageColour(vec_matchingTools[k].searchROI), vec_colourRangeHSV[vec_matchingTools[k].sheet_colour].low, vec_colourRangeHSV[vec_matchingTools[k].sheet_colour].high, imgRoiSheet);
		
		//used recently  createHSVmask(imageRGB(cp.searchROI), cp.colourRangeVals.low, cp.colourRangeVals.high, imgRoiSheet);
		//ai_resShow(std::to_string(k), imgRoiSheet, 1);
		//transformAndDraw_Rect(imgDraw, cp.searchROI, -1 * angle, center, shift);
		Rect returnROIshift = transformRect_centerOnlyFloat(cp.searchROI, -1 * angle, center, shiftFixture);

		cp.searchROI =ai_validate_andModify_RectBounds(returnROIshift, imageRGB.rows, imageRGB.cols);
		//rectangle(imgDraw, cp.searchROI, Scalar(0, 255, 200), 3);
		//cp.searchROI = transformRect_centerOnly(cp.searchROI, -1 * angle, center, shift);
		if (cp.templateThresholdMode == 1)
		{
			//cout << "condition HSV" << endl;
			createHSVmask(imageRGB(cp.searchROI), cp.colourRangeVals.low, cp.colourRangeVals.high, imgRoiSheet);
			//ai_resShow("search ROI HSV", imgRoiSheet, 1);

		}
		else if (cp.templateThresholdMode == 2)
		{
			//cout << "condition Binary" << endl;
			Mat gray;
			cvtColor(imageRGB(cp.searchROI), gray, COLOR_BGR2GRAY);
			threshold(gray, imgRoiSheet, cp.thresholdVal, 255, cp.threshType);
			//ai_resShow("Search_ROI thresh binary", imgRoiSheet, 1);
			//waitKey(0);
		}
		else
		{
			//cout << "condition Gray" << endl;
			cvtColor(imageRGB(cp.searchROI), imgRoiSheet, COLOR_BGR2GRAY);
			//ai_resShow("Search_roi gray", imgRoiSheet, 1);
		}
		//transformPoint_shift_rot_Point
	/*	Point centSearchROI =ai_get_rectCenter( cp.searchROI);
		Point shiftCent = transformPoint_shift_rot_Point(centSearchROI);
		cp.searchROI = ai_getRect_from_cent_size(shiftCent, cp.searchROI.size());*/
	
	
		vector<s_SingleTargetMatch> matchResultsTSheet = findMatch_MatchModelInstance(cp.matchModel_InstNo, imgRoiSheet, imgDraw(cp.searchROI), &matchResp);
		Rect detectedLoc = cp.templateROI;
		if (matchResultsTSheet.size() > 0)
		{
			matchLocTSheet = Point2f(cp.searchROI.x, cp.searchROI.y) + Point2f(matchResultsTSheet[0].ptCenter.x, matchResultsTSheet[0].ptCenter.y);

			locatedData_Curr.locatedCenter = matchLocTSheet;
			detectedLoc = ai_getRect_from_cent_size(matchLocTSheet, cp.templateROI.size());
			float matchAngle = matchResultsTSheet[0].dMatchedAngle;
			//float angleWRTfixture = matchAngle - angle;//18july
			float angleWRTfixture = (-1*matchAngle) - angle;
			//cout << "  location sheet::" << matchLocTSheet << endl;
			//cout << "angle sheet::" << matchAngle << endl;
			//Point shift = offset - (center - matchLocTSheet);//18july
			Point2f shift = matchLocTSheet- transformRect_Point(cp.templateROI, -1 *angle, center, shiftFixture) ;
			Point2f shiftmm = Point2f(shift.x * mmppMobile, shift.y * mmppMobile);
			//try rotated xy
			Point2f matchLocWRTsetupPt = transformBackPointF(matchLocTSheet, angle, center,-1* shiftFixture);
			Point2f setupCenter = ai_get_rectCenterFloat(cp.templateROI);
			Point2f shiftSlant = matchLocWRTsetupPt - setupCenter;
			
			locatedData_Curr.locationShift = shift;
			locatedData_Curr.located = true;
			locatedData_Curr.locatedAngle = angleWRTfixture;

			Point2f shiftSlantMM = shiftSlant * mmppMobile;
			//cout << "%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%  shift old:" << shiftmm << "  ********** new::" << shiftSlantMM << endl;
			shiftmm =  shiftSlantMM;
		
			//endl

		
			//cout << "sheet shift =" << shift << endl;
			//drawTextWithBackGround(imgDraw, cv::format("A:%3.2f", angleWRTfixture), ai_get_rectCenter(cp.searchROI)+Point(cp.searchROI.width,0), Scalar::all(255), Scalar(200, 0, 0), 2, 2);
			drawText(imgDraw, cv::format("A:%3.2f", angleWRTfixture), matchLocTSheet+Point2f(cp.templateROI.width/2,0),  Scalar(0,255,0), 3, 2);
			//drawTextWithBackGround(imgDraw, cv::format("Sh x:%3.1f,y:%3.1f", shift.x,shift.y), ai_get_rectCenter(roiSheet)+Point(0,90), Scalar::all(255), Scalar(200, 0, 0), 4, 2);
			//if (abs(shiftmm.x) <= cp.shiftTol.x && abs(shiftmm.y) <= cp.shiftTol.y && abs(angleWRTfixture) <= cp.rotationTol_deg)
			if (shiftmm.x <= cp.shiftTol.x &&  shiftmm.y <= cp.shiftTol.y && shiftmm.x >= -1*cp.shiftTol_neg.x && shiftmm.y >= -1*cp.shiftTol_neg.y && abs(angleWRTfixture) <= cp.rotationTol_deg)
			{
				//drawTextWithBackGround(imgDraw, cv::format("Sh x:%3.1f,y:%3.1f", shiftmm.x, shiftmm.y), ai_get_rectCenter(cp.searchROI) + Point(0, 60), Scalar::all(255), Scalar(0, 200, 0), 2, 2);
				drawText(imgDraw, cv::format("Sh x:%3.2f,y:%3.2f", shiftmm.x, shiftmm.y), matchLocTSheet + Point2f(cp.templateROI.width / 2, 40), Scalar(0,255,0), 3, 2);
				d.vec_CROITools[k].result = true;
				locatedData_Curr.tool_result = true;
			}
			else
			{
				drawTextWithBackGround(imgDraw, cv::format("S x:%3.2f,y:%3.2f", shiftmm.x, shiftmm.y), matchLocTSheet + Point2f(cp.templateROI.width / 2, 40), Scalar::all(255), Scalar(0, 0, 200), 3, 2);
				if (abs(angleWRTfixture) > cp.rotationTol_deg)
				{
					drawTextWithBackGround(imgDraw, cv::format("A:%3.2f", angleWRTfixture), matchLocTSheet +Point2f(cp.templateROI.width/2,0), Scalar::all(255), Scalar(0, 0, 255), 3, 2);
				}
				//drawText(imgDraw, cv::format("S x:%3.1f,y:%3.1f", shiftmm.x, shiftmm.y), ai_get_rectCenter(cp.searchROI) + Point(0, 90), Scalar(0,0,255), 2, 2);
				d.vec_CROITools[k].result = false;
				//cout << "match****************************tool NG::" << cp.templatePath << "index::" << k << endl;
				resultRet = false;
				locatedData_Curr.tool_result = false;
			}
		}
		else
		{
			Rect returnROIshift = transformRect_centerOnly(cp.templateROI, -1 * angle, center, shiftFixture);

			cp.templateROI = ai_validate_andModify_RectBounds(returnROIshift, imageRGB.rows, imageRGB.cols);
			rectangle(imgDraw, cp.templateROI, Scalar(0, 0, 255), 4);
			drawTextWithBackGround(imgDraw, "Miss",ai_get_rectCenter( cp.templateROI)+Point(cp.templateROI.width/2,0), Scalar::all(255), Scalar(0, 0, 200), 3, 2);
			d.vec_CROITools[k].result = false;
			//cout << "match****************************tool NG::" << cp.templatePath << "index::" << k << endl;
			resultRet = false;
			d.vec_CROITools[k].result = false;
			locatedData_Curr.tool_result = false;
		}
		d.vec_CROITools[k].locatedData = locatedData_Curr;
		transformAndDraw(imgDraw, -1 * angle, center, shiftFixture);
		if (d.vec_CROITools[k].result == false)
		{
			ngToolCnt++;
			//vec_defectTools.push_back( "#" + d.vec_CROITools[k].name);
			vec_defectTools.push_back(setResultTojson(cp.templateROI, "#" + d.vec_CROITools[k].name, false));
			//cout << "data pushed in defect vector::" << vec_defectTools[vec_defectTools.size() - 1] << endl;
			//drawTextWithBackGround(imgDraw, "NG_" + cp.name, Point(40, ngToolCnt * 60), Scalar::all(255), Scalar(0, 0, 200), 3, 2);
		}
		else
		{
			//vec_defectTools.push_back(setResultTojson(cp.templateROI, "#" + d.vec_CROITools[k].name, true));
			vec_defectTools.push_back(setResultTojson(detectedLoc, "#" + d.vec_CROITools[k].name, true));
		}
		//	transformAndDrawPolygon(imgDraw, -1 * angle, center, shift);
	}

	for (int k = 0; k < d.vec_boundaryGap.size(); k++)
	{
		cout << "&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&& processing Boundary )))))))))--------------------                    " << k << endl;
		vector<Point>  rotPts = transformAndDraw_Rect(imgDraw, d.vec_boundaryGap[k].roi, -1 * angle, center, shiftFixture);

	

		//		Mat warp_mat = getAffineTransform(rotPts, rectPts);

				//warpAffine(imgDraw, cropImg, warp_mat, rect_gapMeasure[k].size());
		vector<float> vec_GapsMeasured = vector<float>(4, 0);
		//vector<float> vec_gapLimits = { 0.8,0.8,0.8,0.8 };
	//	Mat presenceClone = cropImg.clone();
		bool resultAll = false;
		vector<bool> vec_result = vector<bool>(4, true);
		if (d.vec_boundaryGap[k].enabledMeasure == true)
		{
			Mat cropImg = ai_cropRotatedRectFromImage(rotPts, d.vec_boundaryGap[k].roi, imageIn);
			vec_result = checkChildPartGaps(cropImg, d.vec_boundaryGap[k].threshold, d.vec_boundaryGap[k].thresholdType, to_string(k), vec_GapsMeasured, d.vec_boundaryGap[k].gapLimits, resultAll);

		}
		else
		{
			resultAll = true;
		}
		//bool resultPresence = checkChildPartThresh(presenceClone, 230, THRESH_BINARY_INV, 70);
		//result = resultPresence;

		//----------check outer
		cout << "processing out of box###############################" << d.vec_boundaryGap[k].name << endl;
		vector<Point>  rotPtsOuter = transformAndDraw_Rect(imgDraw, d.vec_boundaryGap[k].roiOuter, -1 * angle, center, shiftFixture);
		//transformAndDraw_Rect(imgDraw, d.vec_boundaryGap[k].roi, -1 * angle, center, shiftFixture);
		Mat cropImgOuter = ai_cropRotatedRectFromImage(rotPtsOuter, d.vec_boundaryGap[k].roiOuter, imageIn);
		bool finalResultOuter = false;

		if (d.vec_boundaryGap[k].enabledOuter == true)
		{
			vector<bool> resultOuter = checkOutOfBoxThresh(cropImgOuter, d.vec_boundaryGap[k].roiOuter, d.vec_boundaryGap[k].roi, d.vec_boundaryGap[k].thresholdOuter, d.vec_boundaryGap[k].thresholdTypeOuter, d.vec_boundaryGap[k].matchPercentOuter, finalResultOuter);
			for (int k = 0; k < 4; k++)
			{
				if (resultOuter[k])
					circle(imgDraw, rotPtsOuter[k], 8, Scalar(0, 255, 0), -1);
				else
					circle(imgDraw, rotPtsOuter[k], 8, Scalar(0, 0, 255), -1);
			}
		}
		else
		{
			finalResultOuter = true;
		}
		//---end check outer
		if (testMode && d.vec_boundaryGap[k].enabledMeasure)
		{
			drawText(imgDraw, cv::format("%3.2f", vec_GapsMeasured[0]), ((rotPts[3] + rotPts[0]) / 2) - Point(80, 0), Scalar(0, 255, 0), 2, 2);
			drawText(imgDraw, cv::format("%3.2f", vec_GapsMeasured[1]), ((rotPts[1] + rotPts[2]) / 2), Scalar(0, 255, 0), 2, 2);
			drawText(imgDraw, cv::format("%3.2f", vec_GapsMeasured[2]), ((rotPts[0] + rotPts[1]) / 2) - Point(20, 12), Scalar(0, 255, 0), 2, 2);
			drawText(imgDraw, cv::format("%3.2f", vec_GapsMeasured[3]), ((rotPts[2] + rotPts[3]) / 2) + Point(-20,30), Scalar(0, 255, 0),  2, 2);
		}
		cout << "measured gaps vec::::" << format(" L=%3.2f R=%3.2f T=%3.2f B=%3.2f", vec_GapsMeasured[0], vec_GapsMeasured[1], vec_GapsMeasured[2], vec_GapsMeasured[3]) << endl;
			if (resultAll && finalResultOuter)
			{
				circle(imgDraw, rotPts[0], 8, Scalar(0, 255, 0), -1);
			}
			else
			{
				if (!finalResultOuter)
				{
					drawTextWithBackGround(imgDraw, "Outer", rotPtsOuter[3]+Point(0,40), Scalar::all(255), Scalar(0, 0, 200), 2, 2);
				}
				if (!resultAll)
				{
					drawTextWithBackGround(imgDraw, "Measure", rotPts[2]+Point(0, 60), Scalar::all(255), Scalar(0, 0, 200), 2, 2);
				}
				//circle(imgDraw, rotPts[0], 8, Scalar(0, 0, 255), -1);
				if (!vec_result[0])//left
				{
					circle(imgDraw, (rotPts[3] + rotPts[0]) / 2, 8, Scalar(0, 0, 255), -1);
					drawTextWithBackGround(imgDraw,cv::format("%3.2f",vec_GapsMeasured[0]),  ((rotPts[3] + rotPts[0]) / 2)-Point(80,0), Scalar(0, 255, 0), Scalar(0, 0, 200), 2, 2);
					//drawTextWithBackGround(imgDraw, "Miss", ai_get_rectCenter(cp.templateROI) + Point(cp.templateROI.width / 2, 0), Scalar::all(255), Scalar(0, 0, 200), 2, 2);
				}
				if (!vec_result[1])//right
				{
					circle(imgDraw, (rotPts[1] + rotPts[2]) / 2, 8, Scalar(0, 0, 255), -1);
					drawTextWithBackGround(imgDraw, cv::format("%3.2f", vec_GapsMeasured[1]), ((rotPts[1] + rotPts[2]) / 2) , Scalar(0, 255, 0), Scalar(0, 0, 200), 2, 2);
				}
				if (!vec_result[2])//top
				{
					circle(imgDraw, (rotPts[0] + rotPts[1]) / 2, 8, Scalar(0, 0, 255), -1);
					drawTextWithBackGround(imgDraw, cv::format("%3.2f", vec_GapsMeasured[2]),  ((rotPts[0] + rotPts[1]) / 2) - Point(20, 12), Scalar(0, 255, 0), Scalar(0, 0, 200), 2, 2);
				}
				if (!vec_result[3])//bottom
				{
					circle(imgDraw, (rotPts[2] + rotPts[3]) / 2, 8, Scalar(0, 0, 255), -1);
					drawTextWithBackGround(imgDraw, cv::format("%3.2f", vec_GapsMeasured[3]),  ((rotPts[2] + rotPts[3]) / 2) + Point(-20, 30), Scalar(0, 255, 0), Scalar(0, 0, 200), 2, 2);
				}
				resultRet = false;
			
					ngToolCnt++;
					//vec_defectTools.push_back("#" + d.vec_boundaryGap[k].name);
					vec_defectTools.push_back(setResultTojson(d.vec_boundaryGap[k].roi, "#" + d.vec_boundaryGap[k].name, false));

				//	cout << "data pushed in defect vector::" << vec_defectTools[vec_defectTools.size() - 1] << endl;
					//drawTextWithBackGround(imgDraw, "NG_" + d.vec_boundaryGap[k].name, Point(40, ngToolCnt * 60), Scalar::all(255), Scalar(0, 0, 200), 3, 2);
				
			}
		//imshow("CropWarp", cropImg);
	}
	///gray presence
	for (int k = 0; k < d.vec_grayPresence.size(); k++)
	{
		cout << "$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$ processing Gray )))))))))--------------------                    " << k << endl;
		GrayPresence cp = d.vec_grayPresence[k];
		vector<Point>  rotPts = transformAndDraw_Rect(imgDraw, d.vec_grayPresence[k].roi, -1 * angle, center, shiftFixture);

		cout << "looking for ref tool id ::" << cp.fixToolRef << "  fix type ::" << cp.fixType << "  mode ::" << cp.fixMode << endl;
		//
		if (cp.fixType > 2) // is not fixed or fixture
		{
			fix_locatedData locatedData_found;
			cout << "looking for ref tool id ::" << cp.fixToolRef << "  fix type ::" << cp.fixType << "  mode ::" << cp.fixMode << endl;
			for (int tr = 0; tr < d.vec_CROITools.size(); tr++)
			{
				if (d.vec_CROITools[tr].tool_id == cp.fixToolRef)
				{
					cout << "%$%$%$%$%------------------- ref tool found : found id ::" << d.vec_CROITools[tr].tool_id << "  tool name ::" << d.vec_CROITools[tr].name << endl;
					locatedData_found = d.vec_CROITools[tr].locatedData;
					if (locatedData_found.parsed && locatedData_found.tool_result)
					{
						vector<Point> rotPtsTool = rotPts;
						cout << "processing tool ref :: A::" << locatedData_found.locatedAngle << "   C:" << locatedData_found.locatedCenter << " shift:: " << locatedData_found.locationShift << endl;
						if (cp.fixMode == 1)//shift only
						{
							rotPtsTool = transformAndDraw_RotRect(imgDraw, rotPts, 0, locatedData_found.locatedCenter, locatedData_found.locationShift);
						}
						else if (cp.fixMode == 2)//rot only
						{
							rotPtsTool = transformAndDraw_RotRect(imgDraw, rotPts, -1 * locatedData_found.locatedAngle, locatedData_found.locatedCenter, Point(0, 0));
						}
						else //shit and rot
						{
							rotPtsTool = transformAndDraw_RotRect(imgDraw, rotPts, -1 * locatedData_found.locatedAngle, locatedData_found.locatedCenter, locatedData_found.locationShift);
						}
						//validateRectBounds()
						if (ai_validate_rectArea_in_Image(boundingRect(rotPtsTool), imgDraw.size()))
						{
							//ai_validate_andModify_RectBounds(boundingRect(rotPtsTool), imgDraw.rows, imgDraw.cols);
							rotPts = rotPtsTool;
						}


						//Point2f ptFix_n_toolCoordsys = transformRect_Point(rtransformedFix, -1 * angleTempl, centTempl, shiftTempl);
					}
				}
			}




		}
		polylines(imgDraw, rotPts, true, Scalar(0, 165, 255), 1);

		Mat cropImg = ai_cropRotatedRectFromImage(rotPts, d.vec_grayPresence[k].roi, imageIn);
		int measuredPaercent = 0;
		bool result = false;
		//bool result = checkChildPartThresh(cropImg, d.vec_grayPresence[k].threshold, d.vec_grayPresence[k].thresholdType, d.vec_grayPresence[k].matchPercent, measuredPaercent);
			//ai_resShow("grayPresHSV GRAY", cropImg, 1);
		if (cp.thresholdMode == 2)
		{
			//cout << "cropping HSV img" << endl;
			Mat cropColour = ai_cropRotatedRectFromImage(rotPts, d.vec_grayPresence[k].roi, imageRGB);
			cout << "condition HSV" << endl;
			createHSVmask(cropColour, cp.colourRangeVals.low, cp.colourRangeVals.high, cropImg);
			//ai_resShow("grayPresHSV HSV", cropColour, 1);
			//ai_resShow("grayPresHSV HSV thresh", cropImg, 1);
			result = checkChildPartThresh(cropImg, 100, THRESH_BINARY, d.vec_grayPresence[k].matchPercent, measuredPaercent);
			//cout << "%%%%%%%%%%%Colour mode::" << cp.name << endl;
		}
		else
		{
			cout << "condition Binary" << endl;
			result = checkChildPartThresh(cropImg, d.vec_grayPresence[k].threshold, d.vec_grayPresence[k].thresholdType, d.vec_grayPresence[k].matchPercent, measuredPaercent);
			//ai_resShow("Search_ROI thresh binary", imgRoiSheet, 1);
			//waitKey(0);
			 //cout << "%%%%%%%%%%Thresh mode::" << cp.name << endl;
		}
		if (testMode)
		{
			drawText(imgDraw, cv::format("%d", measuredPaercent), ((rotPts[1] + rotPts[2]) / 2) + Point(10, 0), Scalar(0, 255, 0), 2, 2);
		}
			if (result)
			{
				circle(imgDraw, rotPts[0], 8, Scalar(0, 255, 0), -1);
				vec_defectTools.push_back(setResultTojson(d.vec_grayPresence[k].roi, "#" + d.vec_grayPresence[k].name, true));
			}
			else
			{
				circle(imgDraw, rotPts[0], 8, Scalar(0, 0, 255), -1);
				resultRet = false;
				ngToolCnt++;
				//vec_defectTools.push_back( "#" + d.vec_grayPresence[k].name);
				vec_defectTools.push_back(setResultTojson(d.vec_grayPresence[k].roi, "#" + d.vec_grayPresence[k].name, false));
				//cout << "data pushed in defect vector::" << vec_defectTools[vec_defectTools.size() - 1] << endl;
				//drawTextWithBackGround(imgDraw, "NG_" + d.vec_grayPresence[k].name+"_"+to_string(measuredPaercent)+" %", Point(40, ngToolCnt * 60), Scalar::all(255), Scalar(0, 0, 200), 3, 2);
			}
		//imshow("CropWarp", cropImg);
		//	waitKey();
	}


	drawTextWithBackGround(imgDraw, cv::format("A:%3.2f", angle), center + Point(0, -90), Scalar::all(255), Scalar(200, 0, 0), 4, 2);
	//ai_resShow("detections", imgDraw, 0.4);
	//imwrite("baseFolder/templateMatch_detections.bmp", imgDraw);

	//waitKey();
	return 1;
}
//locatePart
int locatePart(Mat& imageColour, bool& resultRet, int camCode, bool testMode)
{
	markIns_refData d = vec_markIns_refInstances[camCode];
	resultRet = true;
	//polygon = { Point(344,824),Point(515,824) ,Point(591,863) ,Point(628,941) ,Point(628,1085),Point(516,1085),Point(344,960) };


	Mat imageIn;
	//Mat imageColour = imread(path);
	cvtColor(imageColour, imageIn, COLOR_BGR2GRAY);
	//Mat imageRGB = imageColour.clone();
	Mat imgDraw = imageColour;// .clone();
	Mat rotShiftImg = imgDraw.clone();
	m_bDebugMode = false;



	Point2f shiftF = Point2f(0, 0);
	Point2f centerF = Point2f(0, 0);
	float angleF = 0;


	int FixtureResp = d.mainFixtureC1.locatePartFixture(imageIn, imgDraw, angleF, centerF, shiftF);
	if (FixtureResp == 0)
	{
		resultRet = false;
		return 0;
	}

	/////---------------
	cv::Mat src = imageIn.clone();// ::imread("im.png", CV_LOAD_IMAGE_UNCHANGED);
	double angleRot = angleF;

	// get rotation matrix for rotating the image around its center in pixel coordinates
	cv::Point2f centerRot = centerF;
	Mat shiftMat = (Mat_<int>(2, 3) << 1, 0, 10, 0, 1, 10);// { 1, 0, shiftF.x; 0, 10, shiftF.y };
	cout << "shift ::" << shiftF << endl;
	cv::Mat Shiftrot = cv::getRotationMatrix2D(centerRot, angleRot, 1.0);

	cv::Mat dst;
	//cv::warpAffine(src, dst, rot, bbox.size());
	//cv::warpAffine(src, dst, shiftMat, src.size());
	//ai_resShow("shift", dst,0.4);

	cv::warpAffine(src, dst, Shiftrot, dst.size());
	//cout << "imageSize::" << src.size() << endl;
	//cout << "required Size::" << src.cols + (abs(shiftF.x)) << "  required Size::" << src.rows + (abs(shiftF.y)) << endl;
	Mat bigMat = Mat::zeros(Size(src.cols + 2 * (abs(shiftF.x)), src.rows + 2 * (abs(shiftF.y))), CV_8UC3);
	Rect pasteRect = Rect((bigMat.cols - src.cols) / 2, (bigMat.rows - src.rows) / 2, src.cols, src.rows);
	//cout << "bigmat size::" << bigMat.size() << endl;
	//cout << "dst size::" << dst.size() << endl;
	//cout << "paste rect" << pasteRect << endl;
	Mat drawRot;
	cv::warpAffine(rotShiftImg, drawRot, Shiftrot, dst.size());
	drawRot.copyTo(bigMat(pasteRect));


	Rect cropRect = Rect(pasteRect.x + shiftF.x, pasteRect.y + shiftF.y, pasteRect.width, pasteRect.height);// ai_getRect_from_cent_size(Point(centerRot.x + shiftF.x, centerRot.y + shiftF.y), src.size());
	//cout << "cropRect interinm" << cropRect << endl;
	//cropRect = Rect(cropRect.x + (bigMat.cols - src.cols) / 2, cropRect.y + (bigMat.rows - src.rows) / 2, cropRect.width, cropRect.height);
	//cout << "cropRect mod" << cropRect << endl;
	Mat croppedFinal = bigMat(cropRect).clone();
	Rect r = Rect(350, 814, 1887, 632);
	//rectangle(croppedFinal, r, Scalar(0, 255, 0), 4);
//	ai_resShow("shiftRot", croppedFinal, 0.4);
	imageColour = croppedFinal.clone();
	//waitKey();
	resultRet = true;
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
//	string basePath = string_to_char_array(modelPath);
//	cout << "string from c++" << basePath << endl;
//	imageBaseFolder = basePath;
//	matching_cp fixture1;
//	fixture1.addData("images/templates/t1.bmp", Rect(0, 0, 100, 100), Rect(615, 1070, 462, 418), 60, 4, Point2f(1.1, 1.1), sheetColour::black,0);
//	vec_fixture.push_back(fixture1);
//
//	matching_cp fixture2;
//	fixture2.addData("images/templates/t2.bmp", Rect(0, 0, 100, 100), Rect(1890, 850, 470, 470), 60, 4, Point2f(1.1, 1.1), sheetColour::black,0);
//	vec_fixture.push_back(fixture2);
//
//	for (int k = 0; k < vec_fixture.size(); k++)
//	{
//		vec_fixture[k].load_fixture();
//	}
//	//init child part templates
//	matching_cp cp_sheetBlue;
//	cp_sheetBlue.addData("images/templates/sheet1.bmp", Rect(443, 619, 343, 199), Rect(343, 519, 543, 499), 60, 4, Point2f(1.1, 1.1), sheetColour::blue,1);
//	vec_matchingTools.push_back(cp_sheetBlue);
//
//	matching_cp cp_sheetRed;
//	cp_sheetRed.addData("images/templates/redSheet2.bmp", Rect(2060, 892, 186, 185), Rect(2060 - 100, 892 - 100, 186 + 150, 185 + 150), 60, 4, Point2f(1.1, 1.1), sheetColour::red,2);
//	vec_matchingTools.push_back(cp_sheetRed);
//
//	matching_cp cp_sheetCam;
//	cp_sheetCam.addData("images/templates/blackCam1.bmp", Rect(321, 804, 337, 165), Rect(321 - 100, 804 - 100, 337 + 200, 165 + 200), 60, 4, Point2f(1.1, 1.1), sheetColour::black,3);
//	vec_matchingTools.push_back(cp_sheetCam);
//
//	matching_cp cp_sheetMid;
//	cp_sheetMid.addData("images/templates/blueSheet2.bmp", Rect(680, 854, 329, 457), Rect(680 - 100, 854 - 100, 329 + 200, 457 + 200), 60, 4, Point2f(1.5, 1.5), sheetColour::blue,1);
//	vec_matchingTools.push_back(cp_sheetMid);
//
//	matching_cp cp_sheetBottom;
//	cp_sheetBottom.addData("images/templates/blueSheet3.bmp", Rect(2136, 904, 290, 478), Rect(2136 - 100, 904 - 100, 290 + 200, 478 + 200), 60, 4, Point2f(1.4, 1.4), sheetColour::blue,1);
//	vec_matchingTools.push_back(cp_sheetBottom);
//
//	//matching_cp cp_sheetBott1;
//	//cp_sheetBott1.addData("images/templates/blueSheet4.bmp", Rect(2154, 570, 261, 128), Rect(2154 - 100, 570 - 100, 261 + 200, 128 + 200), 60, 4, Point2f(1.1, 1.1), sheetColour::blue);
//	//vec_matchingTools.push_back(cp_sheetBott1);
//	//---load cp templates
//	for (int k = 0; k < vec_matchingTools.size(); k++)
//	{
//		vec_matchingTools[k].loadTemplate();
//	}
//	//initiate gap vec temporarily
//
//
//	//init rects fro shitft and presence measurements
//	rect_gapMeasure = { Rect(474,1124,70,41),Rect(575,1400,70,40), Rect(786,1400,70,40),Rect(2230,1344,70,41), Rect(2143,1229,70,41),Rect(400,632,40,70) };
//	gray_vectors = { ai_getRect_from_cent_size(ai_get_rectCenter(rect_gapMeasure[0]),Size(30,20)), ai_getRect_from_cent_size(ai_get_rectCenter(rect_gapMeasure[1]),Size(30,20)), ai_getRect_from_cent_size(ai_get_rectCenter(rect_gapMeasure[2]),Size(30,20)) , ai_getRect_from_cent_size(ai_get_rectCenter(rect_gapMeasure[3]),Size(30,20)), ai_getRect_from_cent_size(ai_get_rectCenter(rect_gapMeasure[4]),Size(20,30)) };
//	m_dScore = 0.4;
//	m_dToleranceAngle = 2;
	return 1;
}
bool debugModeEn = false;

int algorithmLib::Class1::dummyProcC1(System::Drawing::Bitmap^ bitmap0, int camNo, int insMode) //insMode 0: lengthcheck 1:full check
{
	Mat imageIn0 = BitmapToMat(bitmap0);
	debugModeEn = debugModeProp;
	Mat result = imageIn0;
	cout << "#####started pose " << camNo << endl;
	//inst_WidthCamParC1.edgeMagnitude = (int)list_inspections0[Cat_TipLocation_Th].get_std_value();
		try{
			/*	for (int id = 0; id < vec_cam1Rects.size(); id++)
				{
					Rect roi = vec_cam1Rects[id].r;
					rectangle(result, roi, Scalar(255, 0, 0), 2);
				}*/
				//Mat gray;
				//cvtColor(imageIn0, gray, COLOR_BGR2GRAY);


			bool res = false;
			//testTemplMatchFast(result, res,0, insMode);
			//result = detectTip(gray, vec_cam1Rects[2], vec_cam1Rects[1], vec_cam1Rects[0], camNo, list_inspections0, res);
		//	cout << "result C1::" << res << endl;
			resultC1Prop = res;


			if (res)
			{
				drawTextWithBackGround(result, cv::format("OK"), Point(860, 120), Scalar(255, 255, 255), Scalar(0, 255, 0), 8, 4);
			}
			else
			{
				drawTextWithBackGround(result, cv::format("NG"), Point(860, 120), Scalar(255, 255, 255), Scalar(0, 0, 200), 8, 4);
			}

			//resize(draw, result, result.size());

		//	
		}
		catch (exception exx)
		{
	putText(result, "Exception", Point(480, 50), FONT_HERSHEY_SCRIPT_COMPLEX, 2, Scalar(0, 255, 0), 2);
	
	resultC1Prop = false;
	cv::resize(result, imageIn0, imageIn0.size());
	return 0;
		}

		cout << "#####End inspection  pose " << camNo << endl;
	//putText(result, to_string(camNo) + "__" + to_string(insMode), Point(50, 50), FONT_HERSHEY_SCRIPT_COMPLEX, 2, Scalar(0, 255, 0), 2);
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
		
				bool res = false;
				//testTemplMatchFast(result, res, 1, insMode);
				//result = checkTD(gray, vec_cam2Rects[2], vec_cam2Rects[1], vec_cam2Rects[0], camNo, list_inspections1, res, returnLength, bevelSampleAbsent);
				resultC2Prop = res;

				//cout << "length c2::" << LengthC2Prop << endl;
				if (res)
				{
					drawTextWithBackGround(result, cv::format("OK"), Point(860, 120), Scalar(255, 255, 255), Scalar(0, 255, 0), 8, 4);
				}
				else
				{
					drawTextWithBackGround(result, cv::format("NG"), Point(860, 120), Scalar(255, 255, 255), Scalar(0, 0, 200), 8, 4);
				}
			
	}
	catch (exception exx)
	{
		putText(result, "Exception", Point(480, 50), FONT_HERSHEY_SCRIPT_COMPLEX, 2, Scalar(0, 255, 0), 2);
		
		resultC2Prop = false;
		cv::resize(result, imageIn0, imageIn0.size());
		return 1;
	}
	//putText(result, to_string(camNo) + "__" + to_string(insMode), Point(50, 50), FONT_HERSHEY_SCRIPT_COMPLEX, 2, Scalar(0, 255, 0), 2);
	cv::resize(result, imageIn0, imageIn0.size());
	return 1;
}
int algorithmLib::Class1::alignPart(System::Drawing::Bitmap^ bitmap0, int camNo, int insMode)
{
	Mat imageIn0 = BitmapToMat(bitmap0);
	debugModeEn = debugModeProp;
	Mat result = imageIn0.clone();
	//inst_WidthCamParC2.edgeMagnitude = (int)list_inspections1[Cat_TipLocation_Th].get_std_value();
	try
	{

		bool res = false;
		locatePart(result, res, 0, insMode);
		
		//result = checkTD(gray, vec_cam2Rects[2], vec_cam2Rects[1], vec_cam2Rects[0], camNo, list_inspections1, res, returnLength, bevelSampleAbsent);
		

		//cout << "length c2::" << LengthC2Prop << endl;
		if (res)
		{
			//drawTextWithBackGround(result, cv::format("OK"), Point(860, 120), Scalar(255, 255, 255), Scalar(0, 255, 0), 8, 4);
		
		}
		else
		{
			//drawTextWithBackGround(result, cv::format("NG"), Point(860, 120), Scalar(255, 255, 255), Scalar(0, 0, 200), 8, 4);
			return 0;
		}

	}
	catch (exception exx)
	{
		putText(result, "Exception", Point(480, 50), FONT_HERSHEY_SCRIPT_COMPLEX, 2, Scalar(0, 255, 0), 2);

		resultC2Prop = false;
		cv::resize(result, imageIn0, imageIn0.size());
		return 0;
	}
	//putText(result, to_string(camNo) + "__" + to_string(insMode), Point(50, 50), FONT_HERSHEY_SCRIPT_COMPLEX, 2, Scalar(0, 255, 0), 2);
	cv::resize(result, imageIn0, imageIn0.size());
	return 1;
}


//PCB inspection

System::String^ __clrcall algorithmLib::Class1::markIns_testMode(System::Drawing::Bitmap^ bitmap0,  int camPosFromLeft_nz)
{
	cout << "###################################################size of inst vector :" << vec_markIns_refInstances.size() << endl;
	cout << "size of templ inst vector::" << vecMatchModels.size() << endl;
	//create instance of return type
	resultFrontCam::ResultFrontCam inst_result;// = resultFrontCam::FCamResult();
	inst_result.set_pose_num(camPosFromLeft_nz);
	resultFrontCam::Roi roi;
	roi.setCVrect(Rect(0, 0, 200, 200));
	inst_result.set_roi(roi);
	//vector<string> vec_defectTools = vector<string>();
	vector <resultFrontCam::ListDefectDetail>  vec_defectTools = vector <resultFrontCam::ListDefectDetail>();
	inst_result.set_list_defect_details(vec_defectTools);
	//

	//PCDetails.cameraPositionFromLeft = camPosFromLeft_nz;
	try {
		//return region
		Mat imageIn0 = BitmapToMat(bitmap0);
		debugModeEn = debugModeProp;
		Mat result = imageIn0;


		bool res = false;
		cout << "testing cam code ::" << camPosFromLeft_nz << endl;

		testTemplMatchFast(result, res, camPosFromLeft_nz, true, vec_defectTools);

		inst_result.set_final_result(res);
	;

	

		if (vec_markIns_refInstances.size() <= 0)
		{
			cout << "returned as matchinst size is 0" << endl;
			json j = inst_result;
			std::string s = j.dump();
			return gcnew System::String(s.c_str());;
		}

	
	/*	if (!ifDetected)
		{
			cout << "Main ROI not detected" << endl;
			inst_result.set_final_result(false);
			vec_defectTools.push_back("ROI not detected");
			inst_result.set_list_defect_details(vec_defectTools);
			json j = inst_result;
			std::string s = j.dump();
			return gcnew System::String(s.c_str());
		}
		else
		{
			cout << "Main ROI detected" << ROI_locInGlass << endl;
			roi.setCVrect(ROI_locInGlass);
			inst_result.set_roi(roi);
		}*/
		
		
		//for (int k = 0; k < inst_refData.vec_roiTools.size(); k++)
		//{
		//	if (inst_refData.vec_roiTools[k].get_tool_result() == false)
		//	{
		//		vec_defectTools.push_back(to_string(inst_refData.vec_roiTools[k].get_id()) + "#" + inst_refData.vec_roiTools[k].get_name());
		//		cout << "data pushed in defect vector::" << vec_defectTools[vec_defectTools.size() - 1] << endl;
		//	}
		//	//else
		//	//{

		//	//	//cout << "tool OK::"<< inst_refData.vec_roiTools[k].get_name() << endl;
		//	//}
		//}

	/*
		if (vec_defectTools.size() > 0)
		{
			inst_result.set_final_result(false);
		}
		else
		{
			inst_result.set_final_result(true);
		}*/
	

		inst_result.set_list_defect_details(vec_defectTools);
		//waitKey();
		//return "";
		cout << "#####processing end  pose " << camPosFromLeft_nz << endl;
		json j = inst_result;
		std::string s = j.dump();
		return gcnew System::String(s.c_str());
	}
	catch (exception exx)
	{
		cout << "Exception in cpp" << endl;
		inst_result.set_final_result(false);
		vec_defectTools.push_back(setResultTojson(Rect(0,0,0,0), "Exception in processing algo", false));
		inst_result.set_list_defect_details(vec_defectTools);
		json j = inst_result;
		std::string s = j.dump();
		cout << "#####processing end  pose except " << camPosFromLeft_nz << endl;
		return gcnew System::String(s.c_str());
	}
	
}

//