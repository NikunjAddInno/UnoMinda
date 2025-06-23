#pragma once
#pragma once
#include <opencv2/opencv.hpp>
#include <vector>
//#include "json.hpp"
#include "AI_OCVLib.h"

using namespace cv;
using namespace std;
//using namespace std::chrono;


bool compareHue(Scalar a, Scalar b)
{
	return a[0] < b[0];
}
bool compareSaturation(Scalar a, Scalar b)
{
	return a[1] < b[1];
}
bool compareValue(Scalar a, Scalar b)
{
	return a[2] < b[2];
}



void createHSVmask(Mat imageColourRGB,Scalar vLow,Scalar vHigh, Mat& returnMask)
{
	returnMask = ai_thresholdHSV_circularH(imageColourRGB, vLow[0], vHigh[0], vLow[1], vHigh[1], vLow[2], vHigh[2]);
}

int createHSVLimits(vector<Scalar> colourPoints,bool straight_invert, Scalar& vLow, Scalar& vHigh)
{
	//vLow = Scalar(0, 0, 0);
	//vHigh = Scalar(0, 0, 0);

	std::sort(colourPoints.begin(), colourPoints.end(), compareHue);
	if (straight_invert)
	{
		vLow[0] = colourPoints[0][0];
		vHigh[0] = colourPoints[colourPoints.size() - 1][0];
	}
	else
	{
		vLow[0] = colourPoints[colourPoints.size() - 1][0];
		vHigh[0] = colourPoints[0][0];
	}
	std::sort(colourPoints.begin(), colourPoints.end(), compareSaturation);
	vLow[1] = colourPoints[0][1];
	vHigh[1] = colourPoints[colourPoints.size() - 1][1];
	std::sort(colourPoints.begin(), colourPoints.end(), compareValue);
	vLow[2] = colourPoints[0][2];
	vHigh[2] = colourPoints[colourPoints.size() - 1][2];

	return 1;
}