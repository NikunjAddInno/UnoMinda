// algorithmLib.h

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
#include <nlohmann/json.hpp>


#include "toolCpp_cat.h"
using json = nlohmann::json;
using namespace System;
using namespace cv;
using namespace System::Runtime::InteropServices;
namespace algorithmLib {

	public ref class Class1
	{
	public:

		//-----------json------------

		//--------------cathater json----------
		System::String^ __clrcall algorithmLib::Class1::load_template_cath(System::String^ json_model_data,int camIdx);
		int algorithmLib::Class1::printCamIdx(int camIdx);
		int algorithmLib::Class1::loadInspectionRects(int camIdx, int rect_idx, int x, int y, int width, int height, int threshold);
		

		//-----------mobile-----------------
		int algorithmLib::Class1::loadTemplateDataMobile(System::String^ modelPath);
		int algorithmLib::Class1::loadColours(int index, int id, System::String^ name, int h_low, int s_low, int v_low, int h_high, int s_high, int v_high);
		//

		int algorithmLib::Class1::dummyProcC1(System::Drawing::Bitmap^ bitmap0, int camNo, int insMode); //insMode 0: lengthcheck 1:full check
		int algorithmLib::Class1::dummyProcC2(System::Drawing::Bitmap^ bitmap0, int camNo, int insMode); //insMode 0: lengthcheck 1:full check

		//align part for editing
		int algorithmLib::Class1::alignPart(System::Drawing::Bitmap^ bitmap0, int camNo, int insMode);
		
		//tools
		System::String^ __clrcall algorithmLib::Class1::Load_MarkInspectioData(System::String^ json_model_data);
		int algorithmLib::Class1::Clear_MarkInspectioData();


		//colour range
		//int algorithmLib::Class1::calculateColourRange(int reset, System::Drawing::Bitmap^ in, System::Drawing::Bitmap^ out, int x, int y, int^ hL, int^ sL, int^ vL);
		int algorithmLib::Class1::calculateColourRange(int reset, System::Drawing::Bitmap^ in, System::Drawing::Bitmap^ out, int x, int y, int colourSpread, int& hL, int& sL, int& vL, int& hH, int& sH, int& vH);
		int algorithmLib::Class1::thresholdImagePreview(int threshVal, System::Drawing::Bitmap^ in, System::Drawing::Bitmap^ out, int threshType);
		int algorithmLib::Class1::maskHSVpreview(System::Drawing::Bitmap^ in, System::Drawing::Bitmap^ out, int hL, int sL, int vL, int hH, int sH, int vH);
		// ??PCB inspection
		System::String^ __clrcall algorithmLib::Class1::markIns_testMode(System::Drawing::Bitmap^ bitmap0,  int camPosFromLeft_nz);
				//static float LengthC1_n = 0;
				//property float LengthC1Prop_n
				//{
				//	float get()
				//	{
				//		return LengthC1_n;
				//	}
				//	void set(float  value)
				//	{
				//		LengthC1_n = value;
				//	};
				//};		
				static float LengthC1 = 0;
				property float LengthC1Prop
				{
					float get()
					{
						return LengthC1;
					}
					void set(float  value)
					{
						LengthC1 = value;
					};
				};

				static bool L_ResultC1 = false;
				property bool L_ResultC1Prop
				{
					bool get()
					{
						return L_ResultC1;
					}
					void set(bool  value)
					{
						L_ResultC1 = value;
					};
				};

				static bool resultC1 = false;
				property bool resultC1Prop
				{
					bool get()
					{
						return resultC1;
					}
					void set(bool  value)
					{
						resultC1 = value;
					};
				};

		

				static float LengthC2 = 0;
				property float LengthC2Prop
				{
					float get()
					{
						return LengthC2;
					}
					void set(float  value)
					{
						LengthC2 = value;
					};
				};

				static bool L_ResultC2 = false;
				property bool L_ResultC2Prop
				{
					bool get()
					{
						return L_ResultC2;
					}
					void set(bool  value)
					{
						L_ResultC2 = value;
					};
				};

				static bool resultC2 = false;
				property bool resultC2Prop
				{
					bool get()
					{
						return resultC2;
					}
					void set(bool  value)
					{
						resultC2 = value;
					};
				};

				//signal in case bevel sample not found
				static bool bevel_Sample_absentC1 = false;
				property bool bevel_Sample_absentC1Prop
				{
					bool get()
					{
						return bevel_Sample_absentC1;
					}
					void set(bool  value)
					{
						bevel_Sample_absentC1 = value;
					};
				};
				static bool bevel_Sample_absentC2 = false;
				property bool bevel_Sample_absentC2Prop
				{
					bool get()
					{
						return bevel_Sample_absentC2;
					}
					void set(bool  value)
					{
						bevel_Sample_absentC2 = value;
					};
				};


				static bool debugMode = false;
				property bool debugModeProp
				{
					bool get()
					{
						return debugMode;
					}
					void set(bool  value)
					{
						debugMode = value;
					};
				};
				//	
				static float circleEnclosing_X = 0;
				property float circleEnclosing_XProp
				{
					float get()
					{
						return circleEnclosing_X;
					}
					void set(float  value)
					{
						circleEnclosing_X = value;
					};
				};
				static float circleEnclosing_Y = 0;
				property float circleEnclosing_YProp
				{
					float get()
					{
						return circleEnclosing_Y;
					}
					void set(float  value)
					{
						circleEnclosing_Y = value;
					};
				};
				//

				static int grooveCount = 0;
				property int grooveCountProp
				{
					int get()
					{
						return grooveCount;
					}
					void set(int  value)
					{
						grooveCount = value;
					};
				};
				static int outcode=0;
	property int outCodeProp
		{
			int get()
			{
			return outcode ;
			}
			void set (int  value)
			{
			outcode =value;
			};
		};
	static double id=0;
	property double idProp
		{
			double get()
			{
			return id ;
			}
			void set (double  value)
			{
			id =value;
			};
		};
	static double od=0;
	property double odProp
		{
			double get()
			{
			return od ;
			}
			void set (double  value)
			{
			od =value;
			};
		};


	static double idL=0;
	property double idLProp
		{
			double get()
			{
			return idL ;
			}
			void set (double  value)
			{
			idL =value;
			};
		};
	static double odL=0;
	property double odLProp
		{
			double get()
			{
			return odL ;
			}
			void set (double  value)
			{
			odL =value;
			};
		};

	static double conc=0;
	property double concProp
		{
			double get()
			{
			return conc ;
			}
			void set (double  value)
			{
			conc =value;
			};
		};

		static double distance = 0;
		property float distanceProp
		{
			float get()
			{
				return distance;
			}
			void set(float  value)
			{
				distance = value;
			};
		};


		static double concL = 0;
		property float concLProp
		{
			float get()
			{
				return concL;
			}
			void set(float  value)
			{
				concL = value;
			};
		};


		static double mmPerPix = 1;
		property float mmPerPixProp
		{
			float get()
			{
				return mmPerPix;
			}
			void set(float  value)
			{
				mmPerPix = value;
			};
		};
	};
}
