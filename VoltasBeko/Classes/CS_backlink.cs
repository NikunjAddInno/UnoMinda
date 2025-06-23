using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tryThresholds;
//

using Patagames.Ocr;
using ZXing;

namespace tryThresholds.Classes
{
    class CS_backlink
    {
        double resp_uid = -1;
        Rectangle imageSize;
        enum toolType { tool_OCR, tool_QR };
        toolType type;
        public bool runWorker = false;
        public static BarcodeReader reader;
        OcrApi instOCR;
        public CS_backlink()
        {
            runWorker = false;
            reader = new BarcodeReader { AutoRotate = true, TryInverted = true };
            instOCR = OcrApi.Create();
            instOCR.Init(Patagames.Ocr.Enums.Languages.English);
        }

        public string readOCR(Bitmap imageIn)
        {
            String result = instOCR.GetTextFromImage(imageIn);
            if (result != null)
            {
                return result;
            }
            else
            {
                return "";
            }
        }

        public string readQR(Bitmap imageIn)
        {
            Result result=  reader.Decode(imageIn);
            if (result != null)
            {
                return result.ToString().Trim();
            }
            else 
            { return "";  }

        }

        public void bgWorker_dowork_processQR_OCR()
        {
            while (runWorker)
            {
                resp_uid = globalVars.algo.BL_CheckToolAvailableForProcessing();

                if (resp_uid >= 0)
                {
                    Console.WriteLine($"element fond in map :id:: {resp_uid}");
                    imageSize = globalVars.algo.BL_getImageSize(resp_uid);
                    Bitmap dummyImg = new Bitmap(imageSize.Width, imageSize.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                    globalVars.algo.BL_getImage(dummyImg, resp_uid);
                    type = (toolType)globalVars.algo.GetToolType(resp_uid);
                    string result = "";
                    if (type == toolType.tool_OCR)//ocr
                    {
                        result = readOCR(dummyImg);
                    }
                    else if (type == toolType.tool_QR)//QR
                    {
                        result = readQR(dummyImg);
                    }
                    else
                    {
                        result = "";
                    }
                   // dummyImg.Save("BL_imageToprocess.bmp");

                    globalVars.algo.BL_writeResult(resp_uid, result);
                    Console.WriteLine($"result written back :id:: {resp_uid} result::{result}");
                }
                else
                {
                    System.Threading.Thread.Sleep(10);
                }
               // Console.WriteLine("worker running");
            }
            return ;

        }
    }
}
