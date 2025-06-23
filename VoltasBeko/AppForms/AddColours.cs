using VoltasBeko.classes;
using Measurement_AI.classes;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using tryThresholds.IP_tools;
using VoltasBeko;

namespace tryThresholds
{

    public partial class AddColours : Form
    {
        public AddColours(Bitmap image, String modelPath,DrawPbMmarkIns shape,bool flag_drawShape)
        {
            InitializeComponent();
            pb_Zoom.MouseDown += pb_Zoom_MouseDown;
            pb_Zoom.Paint += pb_Zoom_Paint;
            pb_Zoom.MouseUp += pb_Zoom_MouseUp;
            pb_Zoom.MouseMove += pb_Zoom_MouseMove;
            pnl_zoom.MouseWheel += OnMouseWheel;

            picPreview.Paint += pb_preview_Paint;
            imageIn = imageFns_bmp.get24BitDeepCopy(image);
            ProcessImage = imageFns_bmp.get24BitDeepCopy(image);
            modelPath_in = modelPath;

            draw = flag_drawShape;
            shape_dr = shape;
        }
        DrawPbMmarkIns shape_dr;
        bool draw = false;
        String modelPath_in = "";
        Bitmap imageIn = null;
        Bitmap ProcessImage = null;
        int pointsSelected = 0;


        colourRange_hsv temp_colourInstance = new colourRange_hsv();

        void resetColour()
        {
            PointsSelected = 0;
            temp_colourInstance = new colourRange_hsv();

        }

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

                        if (picPreview.Image != null)
                        {
                            picPreview.Width =pnl_preview.Width;
                            pbClass.setAspectRatio(ref picPreview);
                            picPreview.Top = 0;
                            picPreview.Left = 0;
                            picPreview.Refresh();
                        }
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

                        if (picPreview.Image != null)
                        {
                            picPreview.Refresh();
                        }
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


                if (picPreview.Image != null)
                {
                    picPreview.Width += Convert.ToInt32(picPreview.Width * e.Delta / maxSz);
                    picPreview.Height += Convert.ToInt32(picPreview.Height * e.Delta / maxSz);

                    picPreview.Top -= (int)((e.Delta / maxSz) * (e.Y - picPreview.Top));
                    picPreview.Left -= (int)((e.Delta / maxSz) * (e.X - picPreview.Left));

                    picPreview.Invalidate();
                }
                    //shape_dr.drawShape(pb_Zoom.CreateGraphics(), GlobalItems.zoomfactor);
                    //   paintDefects(pb_Zoom.CreateGraphics());
            }

        }
        Point shiftCoord = new Point(0, 0);

       
        public int PointsSelected
        {
            get => pointsSelected;
            set
            {
                pointsSelected = value;
                lblPointsClicked.Text = value.ToString();

            }
        }
        Point modifyCoord(Point pIn, float m)
        {
            return new Point((int)(pIn.X * m), (int)(pIn.Y * m));
        }
        private void pb_Zoom_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) //image pan
            {
                shiftCoord = e.Location;
            }
            else
            {
                if (imageIn != null)
                {
                    PointsSelected++;
                    Bitmap returnImage = imageFns_bmp.get24BitDeepCopy(imageIn);
                    Point p = modifyCoord(e.Location, zoomfactor);
                    temp_colourInstance=processForHSV_Range(HSV_processmodeCPP.normal,p.X,p.Y, ProcessImage, ref returnImage);
                    pb_setImage_inv(picPreview, returnImage);
                }
            }
        }

        private void pb_Zoom_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) //image pan
            {
                shiftCoord = new Point(0, 0);
            }

        }

        private void pb_Zoom_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && shiftCoord.X != 0 && shiftCoord.Y != 0) //image pan
            {
                pb_Zoom.Top += e.Y - shiftCoord.Y;
                pb_Zoom.Left += e.X - shiftCoord.X;

                if (picPreview.Image != null)
                {
                    picPreview.Top += e.Y - shiftCoord.Y;
                    picPreview.Left += e.X - shiftCoord.X;
                }

                // Console.WriteLine("curent pb position top::"+ pb_Zoom.Top.ToString() + " Left::"+ pb_Zoom.Left.ToString());
            }
            if (shape_dr.pointsAvailable && draw)
            {
                Console.WriteLine("Points available");
                shape_dr.drawShape(pb_Zoom.CreateGraphics(), zoomfactor);
                shape_dr.drawShape(picPreview.CreateGraphics(), zoomfactor);
            }
            //else
            //{
            //    Console.WriteLine("Points Not Available");
            //}

        }

        private void pb_Zoom_Paint(object sender, PaintEventArgs e)
        {
            pbClass.setAspectRatio(ref pb_Zoom);
        }
        private void pb_preview_Paint(object sender, PaintEventArgs e)
        {
            pbClass.setAspectRatio(ref picPreview);
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listBoxColours.SelectedIndex != -1)
            {
                userColours.list_userColours.RemoveAt (listBoxColours.SelectedIndex);
                loadColours_to_Listbox(ref listBoxColours, userColours.list_userColours);

            }
        }
        int loadColours_to_Listbox(ref ListBox l,List<colourRange_hsv> lst)
        {
            l.Items.Clear();
            foreach (colourRange_hsv i in lst)
            {
                l.Items.Add(i.name);
            }
            Console.WriteLine("Remaining element count :"+lst.Count);
            return lst.Count();

        }
        private void AddColours_Load(object sender, EventArgs e)
        {
            loadColours_to_Listbox(ref listBoxColours, userColours.list_userColours);
            if (imageIn != null)
            {
                resetimage();
            }
        }

        private void pb_Zoom_Click(object sender, EventArgs e)
        {
 


        }
        enum HSV_processmodeCPP {normal,reset,undo };
        colourRange_hsv processForHSV_Range(HSV_processmodeCPP m,int X, int Y, Bitmap ProcessImage, ref Bitmap returnImage)
        {
            colourRange_hsv retRange= new colourRange_hsv();
            unsafe
            {
                int hl = 0;
                int sl = 0;
                int vl = 0;
                int hh = 0;
                int sh = 0;
                int vh = 0;
                //globalVars.algo.calculateColourRange(0, ProcessImage, returnImage, p.X,p.Y, h, s, v);
                GlobalItems.algoC1.calculateColourRange((int)m, ProcessImage, returnImage, X,Y,(int)nudColourSpread.Value, &hl, &sl, &vl, &hh, &sh, &vh);
                Console.WriteLine($" Clicked low=({hl},{sl},{vl}) high=({hh},{sh},{vh})");

                retRange = new colourRange_hsv("temp", "", hl, sl, vl, hh, sh, vh);
            }
            return retRange;
        }

        void resetimage()
        {
            //txtColourName.Text = "";
            Bitmap returnImage = imageFns_bmp.get24BitDeepCopy(imageIn);
            processForHSV_Range(HSV_processmodeCPP.reset, 0, 0, ProcessImage, ref returnImage);

            temp_colourInstance = new colourRange_hsv();
            PointsSelected = 0;
            pb_setImage_inv(pb_Zoom, imageIn);
            pb_setImage_inv(picPreview, returnImage);
        }
        private void btnSelectC_Click(object sender, EventArgs e)
        {
            resetimage();
        }

        private void btnUndoPoint_Click(object sender, EventArgs e)
        {
            if (imageIn != null)
            {
                PointsSelected++;
                Bitmap returnImage = imageFns_bmp.get24BitDeepCopy(imageIn);
                processForHSV_Range(HSV_processmodeCPP.undo, 0, 0, ProcessImage, ref returnImage);
                pb_setImage_inv(picPreview, returnImage);
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            if (!(txtColourName.Text == ""))
            { colourRange_hsv ch=new colourRange_hsv(temp_colourInstance);
                ch.name = txtColourName.Text;
                userColours.addColour(ch);
            }
            loadColours_to_Listbox(ref listBoxColours, userColours.list_userColours);
        }

        private void listBoxColours_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxColours.SelectedIndex != -1)
            {
                txtColourName.Text=userColours.list_userColours[ listBoxColours.SelectedIndex].name;
                temp_colourInstance = new colourRange_hsv(userColours.list_userColours[listBoxColours.SelectedIndex]);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (listBoxColours.SelectedIndex != -1)
            {
                if (txtColourName.Text == "")
                {
                    MessageBox.Show("Colour name is Blank.Please Enter Colour name");
                    return;
                }
                colourRange_hsv c = new colourRange_hsv(temp_colourInstance);
                c.name = txtColourName.Text;
                colourRange_hsv oldValue = userColours.list_userColours[listBoxColours.SelectedIndex];
                c.id = oldValue.id;
                userColours.list_userColours[listBoxColours.SelectedIndex] = c;
                loadColours_to_Listbox(ref listBoxColours, userColours.list_userColours);

            }
            else 
            { MessageBox.Show("No colour selected. Please slect the colour from list to update"); }
        }
        int ThresholdType = 0;
        private void trackBar1_Scroll(object sender, EventArgs e)
        {

            lblSelectedColour.Text = trackBar1.Value.ToString();
            if (pb_Zoom.Image != null)
            {
                if (!checkBox1.Checked)
                {
                    ThresholdType = 0;
                }
                else
                { ThresholdType = 1; }
                Bitmap returnImage = imageFns_bmp.get24BitDeepCopy(imageIn);
                GlobalItems.algoC1.thresholdImagePreview(trackBar1.Value, imageIn, returnImage, ThresholdType);

                pb_setImage_inv(picPreview, returnImage);
                // picPreview.Image = returnImage;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap returnImage = imageFns_bmp.get24BitDeepCopy(imageIn);
            GlobalItems.algoC1.thresholdImagePreview(100, imageIn, returnImage, 0);

            pb_setImage_inv( picPreview, returnImage);
            //picPreview.Image = returnImage;
        }

        private void btnTestSelected_Click(object sender, EventArgs e)
        {
            if (listBoxColours.SelectedIndex != -1)
            {
                txtColourName.Text = userColours.list_userColours[listBoxColours.SelectedIndex].name;
                temp_colourInstance = new colourRange_hsv(userColours.list_userColours[listBoxColours.SelectedIndex]);
                Bitmap returnImage = imageFns_bmp.get24BitDeepCopy(imageIn);
                GlobalItems.algoC1.maskHSVpreview(imageIn, returnImage, temp_colourInstance.H_low, temp_colourInstance.S_low, temp_colourInstance.V_low, temp_colourInstance.H_high, temp_colourInstance.S_high, temp_colourInstance.V_high);
                pb_setImage_inv(picPreview, returnImage);
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!(modelPath_in == ""))
            {
                ConsoleExtension.WriteError($"Model path in {modelPath_in}  size of list {userColours.list_userColours.Count}");
                userColours.Save_to_file(modelPath_in+"/colours.json", userColours.list_userColours);
            }
            else
            {
                MessageBox.Show("Model Path is blank");
            }
        }

        private void picPreview_Paint(object sender, PaintEventArgs e)
        {
            if (shape_dr.pointsAvailable && draw)
            {
                shape_dr.drawShape(picPreview.CreateGraphics(), zoomfactor);
            }
        }
    }


}
