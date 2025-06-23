using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoltasBeko.CustomControl
{
    public partial class UserControlToolAck: UserControl
    {
        public UserControlToolAck()
        {
            InitializeComponent();
        }
        public UserControlToolAck(string defectName, bool markVisible = false)
        {
            InitializeComponent();
            buttonMarkOk.Visible = markVisible;
            labelDefect.Text = defectName;
        }

        public bool okMarked = false;
        private void buttonMarkOk_Click(object sender, EventArgs e)
        {
            if (okMarked)
            {
                buttonMarkOk.BackColor = Color.Orange;
                buttonMarkOk.Text = "Mark OK";
            }
            else
            {
                buttonMarkOk.BackColor = Color.LimeGreen;
                buttonMarkOk.Text = "Done";

            }
            okMarked = !okMarked;
        }
    }
}
