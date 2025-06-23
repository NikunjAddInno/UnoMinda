using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VoltasBeko.CustomControl;

namespace VoltasBeko.AppForms
{
    public partial class PoseEnlargePreview : Form
    {
        public PoseEnlargePreview(PosePreview posePreview)
        {
            InitializeComponent();
            Controls.Add(posePreview);
            posePreview.Dock = DockStyle.Fill;
            posePreview.pictureBox.Visible = true;
        }

        public PoseEnlargePreview(ReworkDataControl rework)
        {
            InitializeComponent();
            Controls.Add(rework);
            rework.Dock = DockStyle.Fill;

        }

        public PoseEnlargePreview()
        {
            InitializeComponent();
        }
    }
}
