using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoltasBeko.CustomControl
{
    public partial class ZoomInOutPictureBox : UserControl
    {
        //Feilds
        private int CheckX = 0;
        private int CheckY = 0;
        private int cblWidth = 0;
        private int cblHeight = 0;
        private int m_x = 0;
        private int m_y = 0;

        private int PictureBoxX = 0;
        private int PictureBoxY = 0;
        private int PictureBoxWidth = 0;
        private int PictureBoxHeight = 0;
        private Point StartPoint;
        public int cbl_x = 0;
        public int cbl_y = 0;
        private MouseButtons btn_Left;
        private Point PickBox_XY;
        private Bitmap pictureBoxImage;
        private Size pictureBoxSize;

        private Point previousMousePosition;




        //Constructor
        public ZoomInOutPictureBox()
        {
            InitializeComponent();
            this.panel1.MouseWheel += Panel1_MouseWheel;
            PictureBoxX = PictureBox1.Left;
            PictureBoxY = PictureBox1.Top;
            PictureBoxWidth = PictureBox1.Width;
            PictureBoxHeight = PictureBox1.Height;
            PictureBox1.Image = pictureBoxImage;
            pictureBoxSize = this.Size;

        }

       

        //Properties
        public Bitmap PictureBoxImage
        {
            get
            {
                return pictureBoxImage;
            }
            set
            {
                pictureBoxImage = value;
                PictureBox1.Image = pictureBoxImage;
                this.Invalidate();
                PictureBox1.Refresh();

            }
        }

        public void SetImage(Bitmap bitmap)
        {
            this.Invoke(new Action(() => { PictureBox1.Image = bitmap; }));
        }

        public Size PictureBoxSize
        {
            get
            {
                return pictureBoxSize;
            }
            set
            {
                pictureBoxSize = value;
                this.Size = pictureBoxSize;
                panel1.Size = new Size(this.Size.Width - 7, this.Height - 7);
                PictureBox1.Size = new Size(panel1.Size.Width - 7, panel1.Size.Height - 7);
                PictureBoxWidth = PictureBox1.Size.Width;
                PictureBoxHeight = PictureBox1.Size.Height;
                this.Invalidate();

            }
        }



        private void Panel1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (PictureBox1.Image != null)
            {
                if (e.Delta >= 0)
                {

                    if (PictureBox1.Width < (15 * this.Width) && (PictureBox1.Height < (15 * this.Height)))
                    {
                        Console.WriteLine("Zoom in Delta Value" + e.Delta);
                        //Change pictureBox Size and Multiply Zoomfactor
                        Console.WriteLine("wheel X=" + e.X);
                        PictureBox1.Width = (int)(PictureBox1.Width * 1.25);
                        Console.WriteLine("PicBox Width=" + PictureBox1.Width);
                        PictureBox1.Height = (int)(PictureBox1.Height * 1.25);

                        PictureBox1.Top = (int)(e.Y - 1.25 * (e.Y - PictureBox1.Top));
                        PictureBox1.Left = (int)(e.X - 1.25 * (e.X - PictureBox1.Left));
                        Console.WriteLine("PicBox X=" + PictureBox1.Left);


                    }
                    this.Refresh();
                }
                else
                {

                    CheckY = (int)(e.Y - 0.80 * (e.Y - PictureBox1.Top));
                    CheckX = (int)(e.X - 0.80 * (e.X - PictureBox1.Left));


                    cblWidth = (int)(PictureBox1.Width / 1.25);
                    cblHeight = (int)(PictureBox1.Height / 1.25);
                    m_x = cblWidth - panel1.Width;
                    m_y = cblHeight - panel1.Height;

                    if ((CheckY < PictureBoxY) && (CheckY > -m_y) && (CheckX < PictureBoxX) && (CheckX > -m_x) && (cblWidth > PictureBoxWidth) && (cblHeight > PictureBoxHeight))
                    {
                        Console.WriteLine("After zoom out mx value =" + m_x);
                        PictureBox1.Top = CheckY;
                        PictureBox1.Left = CheckX;
                        PictureBox1.Width = cblWidth;
                        PictureBox1.Height = cblHeight;
                        PictureBox1.Invalidate();

                    }
                    else
                    {
                        PictureBox1.Top = PictureBoxY;
                        PictureBox1.Left = PictureBoxX;
                        PictureBox1.Width = PictureBoxWidth;
                        PictureBox1.Height = PictureBoxHeight;
                        PictureBox1.Invalidate();
                    }

                    PictureBox1.Invalidate();

                }
            }
           

        }





        private void ZoomInOutPictureBox_Load(object sender, EventArgs e)
        {

        }


        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            //if (PictureBox1.Image != null)
            //{

            //    if (e.Button == System.Windows.Forms.MouseButtons.Left)
            //    {
            //        StartPoint = new Point(e.X, e.Y);
            //    }

            //}
            //else
            //{
            //    MessageBox.Show("Image Not Exist");
            //}

        }


        void PicKBoxMove(PictureBox pb, Panel pnl, Point pxy)
        {
            //if ((pb.Width > pnl.Width) && (pb.Height > pnl.Height))
            //{
            //    m_x = pb.Width - pnl.Width;
            //    m_y = pb.Height - pnl.Height;
            //    if (btn_Left == System.Windows.Forms.MouseButtons.Left)
            //    {
            //        cbl_x = pxy.X + (pb.Left - StartPoint.X);
            //        cbl_y = pxy.Y + (pb.Top - StartPoint.Y);
            //        if ((cbl_x < 1) && (cbl_x > -m_x) && (cbl_y < 1) && (cbl_y > -m_y))
            //        {
            //            pb.Left = cbl_x;
            //            pb.Top = cbl_y;
            //        }
            //        pb.Refresh();

            //    }
            //}
        }
        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (PictureBox1.Image != null)
            {
                btn_Left = e.Button;
                PickBox_XY = new Point(e.X, e.Y);
                PicKBoxMove(PictureBox1, panel1, PickBox_XY);
                //int imageX = e.X * PictureBox1.Image.Width / PictureBox1.Width;
                //int imageY = e.Y * PictureBox1.Image.Height / PictureBox1.Height;

                //Console.WriteLine("e.X = " + e.X + ", e.Y = " + e.Y);
                //Console.WriteLine("Image Width = " + PictureBox1.Image.Width + ", Image Height = " + PictureBox1.Image.Height);
                //Console.WriteLine("PictureBox Width = " + PictureBox1.Width + ", PictureBox Height = " + PictureBox1.Height);

                //Console.WriteLine("PictureBox X=" +imageX+" ,PictureBox Y" + imageY);
                previousMousePosition = e.Location;

            }

        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
