using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
//using Newtonsoft.Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VoltasBeko.classes;
using tryThresholds.IP_tools;
using VoltasBeko;
using Measurement_AI.classes;
using tryThresholds;
using VoltasBeko.AppForms;

namespace tryThresholds
{
    public partial class Mark_Inspection : Form
    {
        bool changesSaved = false;
        enum formOpeningMode { createNew, editTools, updateROIimage };
        userColours inst_userColours = new userColours();
        public Mark_Inspection(int openingMode, String ConfigName, long logoChkinst_id, Bitmap image)
        {
            InitializeComponent();
            pb_Zoom.MouseDown += pb_Zoom_MouseDown;
            pb_Zoom.Paint += pb_Zoom_Paint;
            pb_Zoom.MouseUp += pb_Zoom_MouseUp;
            pb_Zoom.MouseMove += pb_Zoom_MouseMove;
            pnl_zoom.MouseWheel += OnMouseWheel;
            cmbTool.DataSource = Enum.GetValues(typeof(Mark_ins_shape));
            this.DoubleBuffered = true;
            frmMode = (formOpeningMode)openingMode;
            tempID = logoChkinst_id;
            initConfigName = ConfigName;
            imageIn = imageFns_bmp.get24BitDeepCopy(image);
            GlobalItems.NewImageUpdated = false;

        }



        #region shapeDraw
        enum RegisterFeature { ROI, Check, None };
        RegisterFeature currRegFeature = RegisterFeature.None;
        DrawPbMmarkIns shape_dr = new DrawPbMmarkIns();//shapeDraw
                                                       //  Control[] arr_toolUI = { new uc_ROI(),new uc_Group_printCheck(), new uc_QR(), new uc_ROI(), new uc_QR() };
                                                       //  Control[] arr_toolUI = { new uc_ROI(),new uc_Group_printCheck(), new uc_QR(), new uc_ROI(), new uc_QR() };
        Control C = new Control();

        //test

        string resetTool(Mark_ins_shape shape)
        {
            shape_dr = new DrawPbMmarkIns();
            shape_dr.Shape = shape;
            pb_Zoom.Refresh();

            pnlToolSettings.Controls.Clear();
            //if (shape == Mark_ins_shape.Group)
            //{
            //    uc_Group_printCheck Region_Tool = new uc_Group_printCheck();
            //    Region_Tool.Dock = DockStyle.Top;
            //    //C = arr_toolUI[(int)shape];
            //    //C.Dock= DockStyle.Top;
            //    pnlToolSettings.Controls.Add(Region_Tool);
            //}
            //else
            //{
            //    uc_ROI ROISelTool = new uc_ROI();
            //    ROISelTool.Dock = DockStyle.Top;
            //    //C = arr_toolUI[(int)shape];
            //    //C.Dock= DockStyle.Top;
            //    pnlToolSettings.Controls.Add(ROISelTool);

            //}
            switch ((int)shape)
            {
                case 0:
                    C = new uc_ROI();
                    break;
                case 1:
                    C = new uc_Group_printCheck();
                    break;
                case 2:
                    C = new uc_QR();
                    break;
                case 3:
                    C = new uc_DateCode_Dots_OCR();
                    break;
                case 4:
                    C = new uc_weekCode_DOTS();
                    break;
                case 5:
                    C = new uc_Mask();
                    break;
                case 6:
                    C = new uc_fixture();
                    break;
                case 7:
                    C = new uc_CROI();
                    break;
                case 8:
                    C = new uc_BoundaryGap();
                    break;
                case 9:
                    C = new uc_GrayPresence();
                    break;
                default:
                    C = new uc_ROI();
                    break;



            }


            //Control[] arr_toolUI = { new uc_ROI(), new uc_Group_printCheck(), new uc_QR(), new uc_DateCode_Dots_OCR(), new uc_QR(),new uc_QR() };
            //C = arr_toolUI[(int)shape];
            C.Dock = DockStyle.Top;
            pnlToolSettings.Controls.Add(C);

            t_FixtureData_temp = new tool("dumm", shape_dr.shape);

            return "Line";

        }
        Point modifyCoord(Point pIn, float m)
        {
            return new Point((int)(pIn.X * m), (int)(pIn.Y * m));
        }
        #endregion



        #region picBoxZoom
        float zoomfactor = 1;
        private void OnMouseWheel(object sender, MouseEventArgs e)
        {
            //Console.WriteLine("Delta::" + e.Delta.ToString());
            if (e.Delta != 0 && (Math.Abs(e.Delta) <= 320))//&& 
            {
                if (e.Delta <= 0)
                {
                    //set minimum size to zoom
                    if (pb_Zoom.Width <= pnl_zoom.Width)
                    {

                        pb_Zoom.Width = pnl_zoom.Width;
                        pbClass.setAspectRatio(ref pb_Zoom);
                        //pb_Zoom.Height = (int)((float)pb_Zoom.Width / GlobalItems.getImageAspectRatio());
                        pb_Zoom.Top = 0;
                        pb_Zoom.Left = 0;
                        zoomfactor = (float)pb_Zoom.Image.Width / (float)pb_Zoom.Width;
                        // lbl_Zoom.Text = pictureBox1.Image.Size; 
                        //  if (shape_dr.pointsAvailable)
                        //  {
                        zoomfactor = (float)pb_Zoom.Image.Width / (float)pb_Zoom.Width;
                        pb_Zoom.Refresh();
                        //     shape_dr.drawShape(pb_Zoom.CreateGraphics(), GlobalItems.zoomfactor);
                        //  }
                        //   paintDefects(pb_Zoom.CreateGraphics());
                        return;
                    }
                }
                else
                {
                    //    Console.WriteLine("stuck in zoomin with ::" + e.Delta.ToString());
                    //set maximum size to zoom
                    if (pb_Zoom.Width > 10 * pnl_zoom.Width)
                    {
                        //if (shape_dr.pointsAvailable)
                        //{
                        zoomfactor = (float)pb_Zoom.Image.Width / (float)pb_Zoom.Width;
                        pb_Zoom.Refresh();
                        //    shape_dr.drawShape(pb_Zoom.CreateGraphics(), GlobalItems.zoomfactor);
                        //}
                        //   paintDefects(pb_Zoom.CreateGraphics());
                        return;
                    }

                }
                //    Console.WriteLine("stuck in outer fn with ::" + e.Delta.ToString());
                float maxSz = 1200;//scale factor for mouse wheel step
                                   // Console.WriteLine("delta values :" + e.Delta.ToString());
                pb_Zoom.Width += Convert.ToInt32(pb_Zoom.Width * e.Delta / maxSz);
                pb_Zoom.Height += Convert.ToInt32(pb_Zoom.Height * e.Delta / maxSz);

                pb_Zoom.Top -= (int)((e.Delta / maxSz) * (e.Y - pb_Zoom.Top));
                pb_Zoom.Left -= (int)((e.Delta / maxSz) * (e.X - pb_Zoom.Left));

                zoomfactor = (float)pb_Zoom.Image.Width / (float)pb_Zoom.Width;
                pb_Zoom.Invalidate();
                //shape_dr.drawShape(pb_Zoom.CreateGraphics(), GlobalItems.zoomfactor);
                //   paintDefects(pb_Zoom.CreateGraphics());
            }

        }
        Point shiftCoord = new Point(0, 0);
        private void pb_Zoom_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) //image pan
            {
                shiftCoord = e.Location;
            }
            else//tool point sleection
            {
                //Console.WriteLine("mouse down __calling x::"+ (e.Location.X*zoomfactor).ToString()+"  y::"+(e.Location.Y*zoomfactor).ToString());
                if (currRegFeature != RegisterFeature.None)
                {
                    shape_dr.add_or_move_anchor(new cvPoint(modifyCoord(e.Location, zoomfactor)), pb_Zoom.CreateGraphics(), zoomfactor);
                }
            }
        }

        private void pb_Zoom_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) //image pan
            {
                shiftCoord = new Point(0, 0);
            }
            else
            {
                if (currRegFeature != RegisterFeature.None)
                {
                    if (shape_dr.pointsAvailable)
                    {
                        pb_Zoom.Refresh();
                        shape_dr.updateAnchorPosition(new cvPoint(modifyCoord(e.Location, zoomfactor)));
                        shape_dr.drawShape(((PictureBox)sender).CreateGraphics(), zoomfactor);
                    }
                }

            }
            //if (shape_dr.pointsAvailable)
            //{
            //    // Console.WriteLine("mouse up _drawing shapes points completed ");

            //    pb_Zoom.Refresh();
            //    shape_dr.drawShape(pb_Zoom.CreateGraphics(), GlobalItems.zoomfactor);
            //}
        }

        private void pb_Zoom_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && shiftCoord.X != 0 && shiftCoord.Y != 0) //image pan
            {
                pb_Zoom.Top += e.Y - shiftCoord.Y;
                pb_Zoom.Left += e.X - shiftCoord.X;

                // Console.WriteLine("curent pb position top::"+ pb_Zoom.Top.ToString() + " Left::"+ pb_Zoom.Left.ToString());
            }
            else
            {
                if (currRegFeature != RegisterFeature.None)
                {
                    //  if (shape_dr.pointsAvailable)
                    // {
                    shape_dr.drawShape(pb_Zoom.CreateGraphics(), zoomfactor);
                    // }
                }

            }
            //if (shape_dr.pointsAvailable && e.Button == MouseButtons.Left)
            //{
            //    //shape_dr.drawShape(pb_Zoom.CreateGraphics(), GlobalItems.zoomfactor);
            //    pb_Zoom.Refresh();
            //    //  Console.WriteLine("mousemove __start ");
            //    shape_dr.updateAnchorPosition(modifyCoord(e.Location, GlobalItems.zoomfactor));
            //    shape_dr.drawShape(((PictureBox)sender).CreateGraphics(), GlobalItems.zoomfactor);
            //    // Console.WriteLine("mouse move __update anchor called ");
            //}
        }

        private void pb_Zoom_Paint(object sender, PaintEventArgs e)
        {
            pbClass.setAspectRatio(ref pb_Zoom);
            if (currRegFeature != RegisterFeature.None)
            {
                // if (shape_dr.pointsAvailable)
                // {
                shape_dr.drawShape(pb_Zoom.CreateGraphics(), zoomfactor);
                // }
            }
        }
        private void pb_setImage_inv(PictureBox pb, Bitmap image)
        {

            if (pb.InvokeRequired)
            {
                pb.Invoke(new Action(delegate () { pb.Image = image; }));

            }
            else
            {
                pb.Image = image;
            }

            pbClass.setAspectRatio(ref pb);
            zoomfactor = (float)pb_Zoom.Image.Width / (float)pb_Zoom.Width;
        }
        #endregion

        Bitmap imageIn;
        int selectedSubPC = 1;
        formOpeningMode frmMode = formOpeningMode.createNew;
        string initConfigName = "";
        long tempID = 0;
        Bitmap fullImg;
        Bitmap testImage;
        private void Mark_Inspection_Load(object sender, EventArgs e)
        {
            fullImg = imageFns_bmp.get24BitDeepCopy(imageIn);
            this.CenterToScreen();
            if (frmMode == formOpeningMode.createNew)//locate glass only
            {
                if (!Directory.Exists(initConfigName))
                {
                    Directory.CreateDirectory(initConfigName);
                }
                modelPath = initConfigName + "/";
                Obj_UserTools_applied.markConfigId = tempID;
                Obj_UserTools_applied.markConfigname = initConfigName;
                Obj_UserTools_applied.subPCno = selectedSubPC;
                pb_setImage_inv(pb_Zoom, imageIn);

                //pb_setImage_inv(pb_Zoom, imageIn);
                //string imgPath = "logo.bmp";//PC2
                fullImg = imageFns_bmp.get24BitDeepCopy(imageIn);

                testImage = imageFns_bmp.get24BitDeepCopy(fullImg);
                //Rectangle glassROI = globalVars.algo.markIns_reg_locateGlassTemplRect(testImage, selectedSubPC);
                //if (glassROI.X != -1 && glassROI.Y != -1)
                //{
                //    //crop template
                //    glassROI = imageFns_bmp.correctSize(glassROI);
                //    imageIn = imageFns_bmp.cropFromBitmap(fullImg, glassROI);
                //    //update data
                //    String glassTemplateFileName = $"{modelPath}glassTemplate_{Obj_UserTools_applied.markConfigId}.bmp";
                //    Obj_UserTools_applied.glass_templateFileName = glassTemplateFileName;
                //    imageIn.Save(Obj_UserTools_applied.glass_templateFileName);
                //    Obj_UserTools_applied.glassLocation = new cvRect(glassROI);



                //    //  imageIn.Save("modelDataTrial/cSimage.bmp");
                //    pb_setImage_inv(pb_Zoom, imageIn);
                //    pnlToolSettings.Visible = true;
                //    pnlToolList.Visible = true;
                //    pnlCrud.Visible = true;
                //    pnlRoiAct.Visible = true;
                //    pnlDebug.Visible = true;
                //}
                //else
                //{
                //    pnlToolSettings.Visible = false;
                //    pnlToolList.Visible = false;
                //    pnlCrud.Visible = false;
                //    pnlRoiAct.Visible = false;
                //    pnlDebug.Visible = false;
                //    MessageBox.Show("No glass detected in ROI.Cannot proceed with mark registration");

                //}
                // glassROI = imageFns_bmp.correctSize(glassROI);
                imageIn = imageFns_bmp.get24BitDeepCopy(fullImg);
                //update data
                //glassROI =
                String glassTemplateFileName = $"{modelPath}glassTemplate_{Obj_UserTools_applied.markConfigId}.bmp";
                Obj_UserTools_applied.glass_templateFileName = glassTemplateFileName;
                Obj_UserTools_applied.rotatedROIFileName = glassTemplateFileName;
                imageIn.Save(Obj_UserTools_applied.glass_templateFileName);
                //Obj_UserTools_applied.glassLocation = new cvRect(glassROI);



                //  imageIn.Save("modelDataTrial/cSimage.bmp");
                pb_setImage_inv(pb_Zoom, imageIn);
                pnlToolSettings.Visible = true;
                pnlToolList.Visible = true;
                pnlCrud.Visible = true;
                pnlRoiAct.Visible = false;
                pnlDebug.Visible = true;


                // Console.WriteLine($"Rect return glass loc ::X::{glassROI.X} y:{glassROI.Y} W::{glassROI.Width}  H:{glassROI.Height}");
            }
            else if (frmMode == formOpeningMode.editTools) //show mark roi and load tools for editing
            {
                modelPath = initConfigName + "/";
                load_model_forEditing(true);
                pnlRoiAct.Visible = false;
                pnlDebug.Visible = true;
            }

            else if (frmMode == formOpeningMode.updateROIimage) //update glass and logo template and show last tools for editing
            {
                Bitmap imgTep = imageFns_bmp.get24BitDeepCopy(imageIn);
                modelPath = initConfigName + "/";
                load_model_forEditing(true);
                pnlRoiAct.Visible = false;
                pnlDebug.Visible = true;
                //pnlToolSettings.Visible = false;
                //pnlToolList.Visible = false;
                //pnlCrud.Visible = false;
                //pnlRoiAct.Visible = false;
                //pnlDebug.Visible = false;
                //modelPath = "markInsModel/";
                //string imgPath = "logo.bmp";//PC2
                // string imgPath = "L1.bmp";//PC2
                //string imgPath = "Lt.bmp";//PC2
                //imageIn = (Bitmap)Bitmap.FromFile(imgPath);
                //globalVars.algo.alignPart()

                if (GlobalItems.algoC1.alignPart(imgTep, 0, 1) == 1)
                {
                    imageIn = imageFns_bmp.get24BitDeepCopy(imgTep);
                    fullImg = imageFns_bmp.get24BitDeepCopy(imgTep);
                    testImage = imageFns_bmp.get24BitDeepCopy(fullImg);
                    imgTep.Save(Obj_UserTools_applied.glass_templateFileName);
                    pb_setImage_inv(pb_Zoom, imgTep);
                    GlobalItems.NewImageUpdated = true;
                    img_roiRotated =imageFns_bmp.get24BitDeepCopy(imgTep);
                    imageIn = imageFns_bmp.get24BitDeepCopy(imgTep);
                }
                else
                {
                    MessageBox.Show("Unable to locate part in current image, loading default image");
                }
                // pb_setImage_inv(pb_Zoom, imageIn);
                //string imgPath = "logo.bmp";//PC2

                //Rectangle glassROI = globalVars.algo.markIns_reg_locateGlassTemplRect(testImage, selectedSubPC);
                //if (glassROI.X != -1 && glassROI.Y != -1)
                //{
                //    //crop template
                //    glassROI = imageFns_bmp.correctSize(glassROI);
                //    imageIn = imageFns_bmp.cropFromBitmap(fullImg, glassROI);
                //    //update data
                //    String glassTemplateFileName = $"{modelPath}glassTemplate_{Obj_UserTools_applied.markConfigId}.bmp";
                //    Obj_UserTools_applied.glass_templateFileName = glassTemplateFileName;
                //    // imageIn.Save(Obj_UserTools_applied.glass_templateFileName);
                //    Obj_UserTools_applied.glassLocation = new cvRect(glassROI);

                //    //locate roi 
                //    Bitmap newROIImage_rot = imageFns_bmp.get24BitDeepCopy(img_roiRotated);
                //    Bitmap testImage2 = imageFns_bmp.get24BitDeepCopy(fullImg);
                //    Rectangle markROI = globalVars.algo.markIns_reg_locateROIinFullImage(testImage2, newROIImage_rot, Obj_UserTools_applied.subPCno);
                //    if (markROI.X != -1 && markROI.Y != -1)
                //    {
                //        img_roiRotated = imageFns_bmp.get24BitDeepCopy(newROIImage_rot);
                //        pb_setImage_inv(pb_Zoom, img_roiRotated);
                //        pnlToolSettings.Visible = true;
                //        pnlToolList.Visible = true;
                //        pnlCrud.Visible = true;
                //        pnlRoiAct.Visible = true;
                //        pnlDebug.Visible = true;
                //    }
                //    //  imageIn.Save("modelDataTrial/cSimage.bmp");
                //    else
                //    {
                //        MessageBox.Show("No ROI detected in GLASS.Cannot proceed with mark registration");
                //    }
                //}
                //else
                //{
                //    MessageBox.Show("No glass detected in Image.Cannot proceed with mark registration");
                //}
                //Console.WriteLine($"Rect return glass loc ::X::{glassROI.X} y:{glassROI.Y} W::{glassROI.Width}  H:{glassROI.Height}");

            }


        }

        private void btnSaveTool_Click(object sender, EventArgs e)
        {
            currRegFeature = RegisterFeature.None;
        }

        private void cmbTool_SelectedIndexChanged(object sender, EventArgs e)
        {
            resetTool((Mark_ins_shape)cmbTool.SelectedItem);
            //shape_dr.shape = (Mark_ins_shape) cmbTool.SelectedItem;
            if (shape_dr.shape == Mark_ins_shape.ROI)
            {
                currRegFeature = RegisterFeature.ROI;
            }
            else
            {
                currRegFeature = RegisterFeature.Check;
            }
        }
        Bitmap img_markROI;
        Bitmap img_roiRotated;
        DrawPbMmarkIns roi_shape;

        void saveFullROIdetails(DrawPbMmarkIns roiShape, Bitmap imageROI, float rotationValue, String imageSavePathName, int flipMode)
        {
            if (roiShape.pointsAvailable)
            {
                //Bitmap saveImg = imageFns_bmp.get24BitDeepCopy(imageROI);
                //saveImg.Save(imageSavePathName);
                // Obj_UserTools_applied.roi_fileName = imageSavePathName;
                // Obj_UserTools_applied.fullROI =new cvRect( imageFns_bmp.getRectFromPoints(roiShape.list_points[0].ToPoint(), roiShape.list_points[1].ToPoint()));
                Obj_UserTools_applied.roi_rotation = rotationValue;
                Obj_UserTools_applied.roi_flipMode = flipMode;


            }
            else
            {
                MessageBox.Show("ROI data not availble to save.");
            }
        }

        String modelPath = "markInsModel/";

        private void btnSelectROI_Click(object sender, EventArgs e)
        {
            roi_shape = shape_dr;
            if (roi_shape.pointsAvailable)
            {
                img_markROI = imageFns_bmp.getCroppedImage(pb_Zoom, roi_shape.list_points[0].ToPoint(), roi_shape.list_points[1].ToPoint());
                img_roiRotated = imageFns_bmp.getCroppedImage(pb_Zoom, roi_shape.list_points[0].ToPoint(), roi_shape.list_points[1].ToPoint());
                //update roi filename file and coord
                String roiFilename = $"{modelPath}markROI_{Obj_UserTools_applied.markConfigId}.bmp";
                Obj_UserTools_applied.roi_fileName = roiFilename;
                img_markROI.Save(Obj_UserTools_applied.roi_fileName);
                Obj_UserTools_applied.fullROI = new cvRect(imageFns_bmp.getRectFromPoints(roi_shape.list_points[0].ToPoint(), roi_shape.list_points[1].ToPoint()));

                String rotatedROIFilename = $"{modelPath}rotatedROI_{Obj_UserTools_applied.markConfigId}.bmp";
                Obj_UserTools_applied.rotatedROIFileName = rotatedROIFilename;
                img_roiRotated.Save(Obj_UserTools_applied.rotatedROIFileName);

                pb_setImage_inv(pb_Zoom, img_markROI);
            }
            else
            {
                MessageBox.Show("ROI not selected. Please select ROI first");
            }
        }

        private void ReselectROI_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Re-selecting ROI will eraise all mark setup tool data. Do you want to continue?", "Re-selct ROI", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                roi_shape = new DrawPbMmarkIns();
                pb_setImage_inv(pb_Zoom, imageIn);
            }
            else
            {
                return;
                // Do something
            }

        }

        private void btnRotate_Click(object sender, EventArgs e)
        {
            //if (img_markROI != null)
            //{
            //    Bitmap imageFullCopy = imageFns_bmp.get24BitDeepCopy(imageIn);
            //    img_roiRotated = imageFns_bmp.get24BitDeepCopy(img_markROI);
            //    globalVars.algo.RotateAndCropImage(imageFullCopy, img_roiRotated,(float) nudRotationAngle.Value,cmbFlipmode.SelectedIndex, roi_shape.list_points[0].X, roi_shape.list_points[0].Y);
            //    saveFullROIdetails(roi_shape, img_roiRotated, (float)nudRotationAngle.Value, "markInsImage.bmp",cmbFlipmode.SelectedIndex);
            //    pb_setImage_inv(pb_Zoom, img_roiRotated);

            //    String rotatedROIFilename = $"{modelPath}rotatedROI_{Obj_UserTools_applied.markConfigId}.bmp";
            //    Obj_UserTools_applied.rotatedROIFileName = rotatedROIFilename;
            //    img_roiRotated.Save(Obj_UserTools_applied.rotatedROIFileName);

            //}
        }
        applied_tools Obj_UserTools_applied = new applied_tools();

        void updateUIappliedTools(ref ListBox lb)
        {
            List<int> lst_id = Obj_UserTools_applied.get_toolID_list();
            lb.Items.Clear();
            foreach (int i in lst_id)
            {
                Mark_ins_shape s = 0;
                String name = "";
                Obj_UserTools_applied.get_toolShapeType(i, ref s, ref name);
                //lb.Items.Add(i+"_"+name);
                lb.Items.Add(name + "_" + i);
            }
            lblToolCnt.Text = Obj_UserTools_applied.AppliedToolCnt.ToString();
        }
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            if (shape_dr.pointsAvailable)
            {
                // Bitmap crop = imageFns_bmp.getCroppedImage(pb_Zoom, shape_dr.list_points[0].ToPoint(), shape_dr.list_points[1].ToPoint());
                tool_helper.update_tool_shape(ref C, shape_dr.DeepCopy(), pb_Zoom, modelPath);
                //tool_helper.add_tool(ref C, shape_dr.DeepCopy(), ref Obj_UserTools_applied);
                tool_helper.add_tool(ref C, shape_dr.DeepCopy(), ref Obj_UserTools_applied, new tool("dumm", shape_dr.shape));
                updateUIappliedTools(ref listBoxAppliedTools);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // g.Set_RoiRegion(shape_dr, "test.bmp");
            if (C != null)
            {
                if (shape_dr.pointsAvailable)
                {
                    // Bitmap crop = imageFns_bmp.getCroppedImage(pb_Zoom, shape_dr.list_points[0].ToPoint(), shape_dr.list_points[1].ToPoint());
                    tool_helper.update_tool_shape(ref C, shape_dr.DeepCopy(), pb_Zoom, modelPath);
                    //     tool_helper.update_tool(ref C, shape_dr, ref Obj_UserTools_applied);
                    tool_helper.update_tool(ref C, shape_dr, ref Obj_UserTools_applied, t_FixtureData_temp);
                    updateUIappliedTools(ref listBoxAppliedTools);
                }
            }
        }
        int getToolID_Fromstring(string fullname)
        {
            int id = 0;
            int underscoreIndex = fullname.LastIndexOf('_');
            if (underscoreIndex >= 0 && underscoreIndex < fullname.Length)
            {
                int.TryParse(fullname.Substring(underscoreIndex + 1, fullname.Length - 1 - underscoreIndex), out id);

            }
            return id;
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            Mark_ins_shape shapeType = 0;
            if (listBoxAppliedTools.SelectedIndex != -1)
            {
                //  String selectedItem_str = (listBoxAppliedTools.SelectedItem.ToString());

                //  int id = 0;
                int id = getToolID_Fromstring(listBoxAppliedTools.SelectedItem.ToString()); ;
                try
                {
                    //  int.TryParse(selectedItem_str, out id);

                    tool_helper.delete_tool(id, ref Obj_UserTools_applied);
                    updateUIappliedTools(ref listBoxAppliedTools);

                }
                catch (Exception exx)
                {
                    Console.WriteLine("Exception while tool deletion");
                }
            }

        }

        private void listBoxAppliedTools_SelectedIndexChanged(object sender, EventArgs e)
        {
            Mark_ins_shape shapeType = 0;
            if (listBoxAppliedTools.SelectedIndex != -1)
            {
                Console.WriteLine($"Selected tool ID {getToolID_Fromstring(listBoxAppliedTools.SelectedItem.ToString())}");
                // String selectedItem_str = (listBoxAppliedTools.SelectedItem.ToString());
                // int id = 0;
                int id = getToolID_Fromstring(listBoxAppliedTools.SelectedItem.ToString());
                try
                {
                    //int.TryParse(selectedItem_str, out id);
                    String name = "";
                    Obj_UserTools_applied.get_toolShapeType(id, ref shapeType, ref name);
                    // resetTool(shapeType);
                    cmbTool.SelectedItem = shapeType;
                    tool_helper.select_tool(ref C, id, shapeType, Obj_UserTools_applied, ref shape_dr);
                    cmbTool.SelectedItem = shapeType;
                    pb_Zoom.Refresh();

                    if (shape_dr.pointsAvailable)
                    {
                        shape_dr.drawShape(pb_Zoom.CreateGraphics(), zoomfactor);
                    }
                    else
                    {
                        Console.WriteLine("shape points not available");
                    }
                    // pb_Zoom.Invalidate();
                    tool_helper.get_fixtureReferenceData(ref C, shape_dr, ref t_FixtureData_temp);
                }
                catch (Exception exx)
                {

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (frmMode == formOpeningMode.updateROIimage)
            {
                imageIn.Save(Obj_UserTools_applied.glass_templateFileName);
                img_roiRotated.Save(Obj_UserTools_applied.rotatedROIFileName);
            }
            applied_tools.Save_to_file(modelPath + "MarkInsTools.json", Obj_UserTools_applied);
            changesSaved = true;
        }
        int load_model_forEditing_old()
        {
            try
            {
                Obj_UserTools_applied = applied_tools.Load_from_file(modelPath + "MarkInsTools.json");

                //copy data in cpp
                String allTools_Ser = Newtonsoft.Json.JsonConvert.SerializeObject(Obj_UserTools_applied, Formatting.Indented);
                globalVars.algo.Load_MarkInspectioData(allTools_Ser); //15july_kk
                                                                      // Console.WriteLine(allTools_Ser);
                                                                      //read rotated image and show + reflect all tools in UI
                imageFns_bmp.readImage_nonBlocking(Obj_UserTools_applied.rotatedROIFileName, ref img_roiRotated);
                pb_setImage_inv(pb_Zoom, img_roiRotated);
                updateUIappliedTools(ref listBoxAppliedTools);
                return 1;
            }
            catch (Exception exx)
            {
                MessageBox.Show("Unable to load Mark inspection data or image for editing" + exx.Message);
                return -1;
            }

        }
      
        int load_model_forEditing(bool loadFromFile)
        {
            try
            {
                string modelPathColourFile = Directory.GetParent(modelPath).Parent.FullName.ToString();

                

                Console.WriteLine($" colourfilePath is ::{modelPathColourFile}");

                userColours.list_userColours = userColours.Load_from_file(modelPathColourFile + "/colours.json");
                loadColorsinCpp();
                if (loadFromFile)
                {
                    Obj_UserTools_applied = applied_tools.Load_from_file(modelPath + "/MarkInsTools.json");
                    updateUIappliedTools(ref listBoxAppliedTools);
                }
                //  Obj_UserTools_applied_C2 = applied_tools.Load_from_file(AppSettings.currentModelPath + "/c2" + "/MarkInsTools.json");

                String allTools_Ser = Newtonsoft.Json.JsonConvert.SerializeObject(Obj_UserTools_applied, Formatting.Indented);
                // String allTools_SerC2 = Newtonsoft.Json.JsonConvert.SerializeObject(Obj_UserTools_applied_C2, Formatting.Indented);
                //
                //Load to cpp
                globalVars.algo.Clear_MarkInspectioData();
                globalVars.algo.Load_MarkInspectioData(allTools_Ser);
                // globalVars.algo.Load_MarkInspectioData(allTools_SerC2);

                imageFns_bmp.readImage_nonBlocking(Obj_UserTools_applied.rotatedROIFileName, ref img_roiRotated);
                imageFns_bmp.readImage_nonBlocking(Obj_UserTools_applied.rotatedROIFileName, ref imageIn);
                fullImg = imageFns_bmp.get24BitDeepCopy(imageIn);
                pb_setImage_inv(pb_Zoom, img_roiRotated);

                return 1;
                //15july_kk
                //Console.WriteLine(allTools_SerC1);
                //Console.WriteLine(allTools_SerC2);
                //read rotated image and show + reflect all tools in UI


            }
            catch (Exception exx)
            {
                MessageBox.Show("Unable to load Mark inspection data or image for editing" + exx.Message);
                return -1;
            }

        }
        userColours u = new userColours();
        void loadColorsinCpp()
        {

            for (int k = 0; k < userColours.list_userColours.Count(); k++)
            {
                colourRange_hsv c = userColours.list_userColours[k];
                GlobalItems.algoC1.loadColours(k, c.id, c.name, c.H_low, c.S_low, c.V_low, c.H_high, c.S_high, c.V_high);
            }
            Console.WriteLine("*******************************************colour count in cs::" + userColours.list_userColours.Count().ToString());
        }
        private void btnLoadTools_Click(object sender, EventArgs e)
        {
            load_model_forEditing(true);
            //Obj_UserTools_applied = applied_tools.Load_from_file(modelPath + "MarkInsTools.json");

            ////copy data in cpp
            //String allTools_Ser = Newtonsoft.Json.JsonConvert.SerializeObject(Obj_UserTools_applied, Formatting.Indented);
            //globalVars.algo.Load_MarkInspectioData(allTools_Ser);

            ////read rotated image and show + reflect all tools in UI
            //imageFns_bmp.readImage_nonBlocking(Obj_UserTools_applied.rotatedROIFileName, ref img_roiRotated);
            //pb_setImage_inv(pb_Zoom, img_roiRotated);
            //updateUIappliedTools(ref listBoxAppliedTools);
            //return;
            //applied_tools modelTemp= applied_tools.Load_from_file(modelPath+"MarkInsTools.json");
            //String allTools_Ser = Newtonsoft.Json.JsonConvert.SerializeObject(modelTemp, Formatting.Indented);
            //globalVars.algo.Load_MarkInspectioData(allTools_Ser);


            //string returnData= globalVars.algo.getMarkInsDataResTest();
            //Obj_UserTools_applied = JsonConvert.DeserializeObject<applied_tools>(returnData);
            //imageFns_bmp.readImage_nonBlocking(Obj_UserTools_applied.rotatedROIFileName, ref img_roiRotated);
            //pb_setImage_inv(pb_Zoom, img_roiRotated);
            //updateUIappliedTools(ref listBoxAppliedTools);

            //Console.WriteLine("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
            // Console.WriteLine(returnData);
            //JSchemaGenerator generator = new JSchemaGenerator();
            //JSchema schema = generator.Generate(typeof(applied_tools));
            //Console.WriteLine("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
            //Console.WriteLine(schema.ToString());
        }

        String testImagePath;
        private void btnTestImage_Click(object sender, EventArgs e)
        {
            pnlToolSettings.Visible = false;
            pnlToolList.Visible = false;
            //reload data
            load_model_forEditing(false);

            Bitmap Inimage;
            if (Obj_UserTools_applied != null)
            {

                Bitmap tempImg;// = imageFns_bmp.get24BitDeepCopy(testImage);

                if (File.Exists(testImagePath))
                {
                    testImage = (Bitmap)Bitmap.FromFile(testImagePath);
                    tempImg = imageFns_bmp.get24BitDeepCopy(testImage);
                }
                else
                {
                    MessageBox.Show("image not selected. Loading setup image");
                    imageFns_bmp.readImage_nonBlocking(Obj_UserTools_applied.rotatedROIFileName, ref testImage);
                    tempImg = imageFns_bmp.get24BitDeepCopy(testImage);
                }


                //Bitmap Inimage = (Bitmap)Bitmap.FromFile("LtD.bmp");
                //globalVars.algo.dummyProcC1(tempImg, 0, 1);
                globalVars.algo.markIns_testMode(tempImg, 0);
                
                pb_setImage_inv(pb_Zoom, tempImg);


            }
            else
            {
                MessageBox.Show($"No file selected");
            }

        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                // MessageBox.Show($"Selected file path : {openFileDialog1.FileName}");
                testImagePath = openFileDialog1.FileName;
                lblDebugImgName.Text = "Debug Image:" + testImagePath;
            }
            else
            {
                lblDebugImgName.Text = "Debug Image: No file selected";
                // MessageBox.Show($"No file selected");
            }
            lblDebugImgName.Visible = true;

        }

        private void Mark_Inspection_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void Mark_Inspection_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                string modelPathBase = Directory.GetParent(modelPath).Parent.FullName.ToString();

                

                Console.WriteLine($"colourfilePath is ::{modelPathBase}");

                userColours.list_userColours = userColours.Load_from_file(modelPathBase + "/colours.json");
                loadColorsinCpp();

                //applied_tools Obj_UserTools_applied_C1 = applied_tools.Load_from_file(modelPathBase + "/MarkInsTools.json");


                //applied_tools Obj_UserTools_applied_C2 = applied_tools.Load_from_file(modelPathBase + "/c2/MarkInsTools.json");

                //String allTools_Ser = Newtonsoft.Json.JsonConvert.SerializeObject(Obj_UserTools_applied_C1, Formatting.Indented);
                //String allTools_SerC2 = Newtonsoft.Json.JsonConvert.SerializeObject(Obj_UserTools_applied_C2, Formatting.Indented);
                //
                //Load to cpp
                globalVars.algo.Clear_MarkInspectioData();
                //globalVars.algo.Load_MarkInspectioData(allTools_Ser);
                //globalVars.algo.Load_MarkInspectioData(allTools_SerC2);

                // imageFns_bmp.readImage_nonBlocking(Obj_UserTools_applied.rotatedROIFileName, ref img_roiRotated);
                // imageFns_bmp.readImage_nonBlocking(Obj_UserTools_applied.rotatedROIFileName, ref imageIn);
                // fullImg = imageFns_bmp.get24BitDeepCopy(imageIn);
                // pb_setImage_inv(pb_Zoom, img_roiRotated);


                //15july_kk
                //Console.WriteLine(allTools_SerC1);
                //Console.WriteLine(allTools_SerC2);
                //read rotated image and show + reflect all tools in UI


            }
            catch (Exception exx)
            {
                MessageBox.Show("Unable to load data for inspection automatically. Please select from model name dropdown and start." + exx.Message);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!changesSaved)
            {

                DialogResult result = MessageBox.Show("Do you want to continue without saving changes?" + Environment.NewLine + "Changes not saved to file.Before you exit, You can click on save button to save changes.", "Exit Warning", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    return;
                }
                else
                {
                    this.Close();
                }
            }
            this.Close();
        }

        private void btnResetView_Click(object sender, EventArgs e)
        {
            Bitmap temp = imageFns_bmp.get24BitDeepCopy(imageIn);
            pb_setImage_inv(pb_Zoom, temp);
            lblDebugImgName.Visible = false;
            pnlToolSettings.Visible = true;
            pnlToolList.Visible = true;
        }

        private void btntestColour_Click(object sender, EventArgs e)
        {
            string modelPathColourFile = Directory.GetParent(modelPath).Parent.FullName.ToString();

          
            Console.WriteLine($"path of colour file{modelPathColourFile}");
            Bitmap fullImg = imageFns_bmp.get24BitDeepCopy(imageIn);
            AddColours frmSelColour = new AddColours(fullImg, modelPathColourFile, shape_dr, true);
            frmSelColour.Show();
        }

        private void buttonScanCode_Click(object sender, EventArgs e)
        {
            Bitmap defectImage = imageFns_bmp.get24BitDeepCopy(imageIn);
            string result_str = globalVars.algo.markIns_testMode(defectImage, Obj_UserTools_applied.subPCno);
            Console.WriteLine($"Return json ::: {result_str}");
            tryThresholds.IP_tools.resultFrontCam result;

            result = JsonConvert.DeserializeObject<tryThresholds.IP_tools.resultFrontCam>(result_str);

            Bitmap cropImage = defectImage.Clone(new Rectangle(result.ROI.X, result.ROI.Y, result.ROI.Width, result.ROI.Height), System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            string apiResult = "";
            try
            {
                apiResult = ApiController.ProcessImage((Bitmap)pb_Zoom.Image.Clone(), "get_code", "5003");

            }
            catch (Newtonsoft.Json.JsonReaderException ex)
            {
                Console.WriteLine(ex);
                apiResult = ApiController.ProcessImage((Bitmap)pb_Zoom.Image.Clone(), "get_code", "5003");
            }
            catch (Exception ex)
            {
                ConsoleExtension.WriteWithColor(ex, ConsoleColor.Red);
                MessageBox.Show("Error in Getting code Reading. Try again");
                return;
            }

            ConsoleExtension.WriteWithColor(apiResult, ConsoleColor.Yellow);
            
            labelCode.Text = $"Code: {apiResult}";
            
            //string apiRes = JsonConvert.SerializeObject(labelData);

            File.WriteAllText($@"{AppData.ModelPath}\{tempID}\Code.json", apiResult);
        }
        tool t_FixtureData_temp = new tool();
        private void btnFIxtureConfig_Click(object sender, EventArgs e)
        {
            List<string> refToolList = new List<string>();
            Mark_ins_shape shapeType = 0;
            for (int k = 0; k < listBoxAppliedTools.Items.Count; k++)

            {
                Console.WriteLine($"Selected tool ID {tool_helper.getToolID_Fromstring(listBoxAppliedTools.Items[k].ToString())}");
                // String selectedItem_str = (listBoxAppliedTools.SelectedItem.ToString());
                // int id = 0;
                int id = tool_helper.getToolID_Fromstring(listBoxAppliedTools.Items[k].ToString());
                if (t_FixtureData_temp.Id == id)//skip the current tool
                    continue;
                try
                {
                    //int.TryParse(selectedItem_str, out id);
                    String name = "";
                    Obj_UserTools_applied.get_toolShapeType(id, ref shapeType, ref name);
                    // resetTool(shapeType);
                    // cmbTool.SelectedItem = shapeType;
                    if (shapeType == Mark_ins_shape.CROI)
                    {
                        refToolList.Add(listBoxAppliedTools.Items[k].ToString());
                    }




                    // pb_Zoom.Invalidate();
                }
                catch (Exception exx)
                {

                }

            }

            frmCoordReference frmFixture = new frmCoordReference(t_FixtureData_temp, refToolList, "dumm");
            frmFixture.ShowDialog();
            t_FixtureData_temp = frmFixture.t;
            Console.WriteLine($" type {t_FixtureData_temp.FixtureType}   mode={t_FixtureData_temp.Fixture_mode}  reftool={t_FixtureData_temp.FixtureToolReference}");

        }
    }
}
