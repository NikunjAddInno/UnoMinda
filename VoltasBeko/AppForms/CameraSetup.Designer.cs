namespace VoltasBeko.AppForms
{
    partial class CameraSetup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanelPose = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonSavePose = new System.Windows.Forms.Button();
            this.buttonSaveSetup = new System.Windows.Forms.Button();
            this.labelPoseCount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxModel = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.labelActPos = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.buttonClear = new System.Windows.Forms.Button();
            this.labelCode = new System.Windows.Forms.Label();
            this.buttonSelectPath = new System.Windows.Forms.Button();
            this.zoomPictureBoxPose = new VoltasBeko.CustomControl.ZoomInOutPictureBox();
            this.poseControl1 = new VoltasBeko.CustomControl.PoseControl();
            this.zoomInOutPictureBox1 = new VoltasBeko.CustomControl.ZoomInOutPictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxOffsetY = new System.Windows.Forms.TextBox();
            this.buttonStartCopy = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1894, 39);
            this.label1.TabIndex = 6;
            this.label1.Text = "CAMERA SETUP";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanelPose
            // 
            this.flowLayoutPanelPose.AllowDrop = true;
            this.flowLayoutPanelPose.AutoScroll = true;
            this.flowLayoutPanelPose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(223)))), ((int)(((byte)(243)))));
            this.flowLayoutPanelPose.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanelPose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flowLayoutPanelPose.Location = new System.Drawing.Point(735, 80);
            this.flowLayoutPanelPose.Name = "flowLayoutPanelPose";
            this.flowLayoutPanelPose.Size = new System.Drawing.Size(458, 763);
            this.flowLayoutPanelPose.TabIndex = 9;
            // 
            // buttonSavePose
            // 
            this.buttonSavePose.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.buttonSavePose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSavePose.Location = new System.Drawing.Point(340, 978);
            this.buttonSavePose.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSavePose.Name = "buttonSavePose";
            this.buttonSavePose.Size = new System.Drawing.Size(126, 41);
            this.buttonSavePose.TabIndex = 37;
            this.buttonSavePose.Text = "Save Pose";
            this.buttonSavePose.UseVisualStyleBackColor = false;
            this.buttonSavePose.Click += new System.EventHandler(this.buttonSavePose_Click);
            // 
            // buttonSaveSetup
            // 
            this.buttonSaveSetup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.buttonSaveSetup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSaveSetup.Location = new System.Drawing.Point(1704, 914);
            this.buttonSaveSetup.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSaveSetup.Name = "buttonSaveSetup";
            this.buttonSaveSetup.Size = new System.Drawing.Size(153, 72);
            this.buttonSaveSetup.TabIndex = 37;
            this.buttonSaveSetup.Text = "Complete Setup";
            this.buttonSaveSetup.UseVisualStyleBackColor = false;
            this.buttonSaveSetup.Click += new System.EventHandler(this.buttonSaveSetup_Click);
            // 
            // labelPoseCount
            // 
            this.labelPoseCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(134)))), ((int)(((byte)(212)))));
            this.labelPoseCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPoseCount.ForeColor = System.Drawing.Color.White;
            this.labelPoseCount.Location = new System.Drawing.Point(735, 46);
            this.labelPoseCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPoseCount.Name = "labelPoseCount";
            this.labelPoseCount.Size = new System.Drawing.Size(458, 30);
            this.labelPoseCount.TabIndex = 38;
            this.labelPoseCount.Text = "Total Pose:";
            this.labelPoseCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(134)))), ((int)(((byte)(212)))));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(60, 46);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(638, 30);
            this.label2.TabIndex = 38;
            this.label2.Text = "Live View";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(134)))), ((int)(((byte)(212)))));
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(1219, 46);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(638, 30);
            this.label3.TabIndex = 38;
            this.label3.Text = "Selected Pose Image";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBoxModel
            // 
            this.comboBoxModel.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxModel.FormattingEnabled = true;
            this.comboBoxModel.Location = new System.Drawing.Point(140, 3);
            this.comboBoxModel.Name = "comboBoxModel";
            this.comboBoxModel.Size = new System.Drawing.Size(158, 33);
            this.comboBoxModel.TabIndex = 41;
            this.comboBoxModel.Text = "My Model";
            this.comboBoxModel.TextChanged += new System.EventHandler(this.comboBoxModel_TextChanged);
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.DarkCyan;
            this.label23.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.White;
            this.label23.Location = new System.Drawing.Point(3, 2);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(133, 35);
            this.label23.TabIndex = 40;
            this.label23.Text = "Model Name: ";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelActPos
            // 
            this.labelActPos.AutoSize = true;
            this.labelActPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelActPos.Location = new System.Drawing.Point(60, 983);
            this.labelActPos.Name = "labelActPos";
            this.labelActPos.Size = new System.Drawing.Size(41, 15);
            this.labelActPos.TabIndex = 43;
            this.labelActPos.Text = "label4";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 700;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(307, 3);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(104, 33);
            this.buttonClear.TabIndex = 44;
            this.buttonClear.Text = "New Model";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // labelCode
            // 
            this.labelCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(134)))), ((int)(((byte)(212)))));
            this.labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCode.ForeColor = System.Drawing.Color.White;
            this.labelCode.Location = new System.Drawing.Point(1219, 856);
            this.labelCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCode.Name = "labelCode";
            this.labelCode.Size = new System.Drawing.Size(638, 45);
            this.labelCode.TabIndex = 70;
            this.labelCode.Text = "CSV Path:";
            this.labelCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonSelectPath
            // 
            this.buttonSelectPath.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSelectPath.Location = new System.Drawing.Point(1219, 916);
            this.buttonSelectPath.Name = "buttonSelectPath";
            this.buttonSelectPath.Size = new System.Drawing.Size(144, 45);
            this.buttonSelectPath.TabIndex = 71;
            this.buttonSelectPath.Text = "Select csv path";
            this.buttonSelectPath.UseVisualStyleBackColor = true;
            this.buttonSelectPath.Click += new System.EventHandler(this.buttonScanCode_Click);
            // 
            // zoomPictureBoxPose
            // 
            this.zoomPictureBoxPose.BackColor = System.Drawing.Color.White;
            this.zoomPictureBoxPose.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.zoomPictureBoxPose.ForeColor = System.Drawing.Color.Black;
            this.zoomPictureBoxPose.Location = new System.Drawing.Point(1219, 79);
            this.zoomPictureBoxPose.Name = "zoomPictureBoxPose";
            this.zoomPictureBoxPose.PictureBoxImage = null;
            this.zoomPictureBoxPose.PictureBoxSize = new System.Drawing.Size(638, 763);
            this.zoomPictureBoxPose.Size = new System.Drawing.Size(638, 763);
            this.zoomPictureBoxPose.TabIndex = 39;
            // 
            // poseControl1
            // 
            this.poseControl1.BackColor = System.Drawing.Color.White;
            this.poseControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.poseControl1.Location = new System.Drawing.Point(60, 856);
            this.poseControl1.Name = "poseControl1";
            this.poseControl1.Selected = false;
            this.poseControl1.Size = new System.Drawing.Size(406, 117);
            this.poseControl1.TabIndex = 10;
            // 
            // zoomInOutPictureBox1
            // 
            this.zoomInOutPictureBox1.BackColor = System.Drawing.Color.White;
            this.zoomInOutPictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.zoomInOutPictureBox1.ForeColor = System.Drawing.Color.Black;
            this.zoomInOutPictureBox1.Location = new System.Drawing.Point(60, 79);
            this.zoomInOutPictureBox1.Name = "zoomInOutPictureBox1";
            this.zoomInOutPictureBox1.PictureBoxImage = null;
            this.zoomInOutPictureBox1.PictureBoxSize = new System.Drawing.Size(638, 763);
            this.zoomInOutPictureBox1.Size = new System.Drawing.Size(638, 763);
            this.zoomInOutPictureBox1.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Black;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(7, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 22);
            this.label4.TabIndex = 74;
            this.label4.Text = "Y AXIS OFFSET";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(234, 55);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 75;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.textBoxOffsetY);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.buttonStartCopy);
            this.panel1.Location = new System.Drawing.Point(485, 856);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(213, 117);
            this.panel1.TabIndex = 76;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Yellow;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(211, 23);
            this.label5.TabIndex = 76;
            this.label5.Text = "COPY PCB PROGRAM";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxOffsetY
            // 
            this.textBoxOffsetY.Location = new System.Drawing.Point(109, 34);
            this.textBoxOffsetY.Name = "textBoxOffsetY";
            this.textBoxOffsetY.Size = new System.Drawing.Size(100, 20);
            this.textBoxOffsetY.TabIndex = 77;
            // 
            // buttonStartCopy
            // 
            this.buttonStartCopy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.buttonStartCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStartCopy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStartCopy.Location = new System.Drawing.Point(109, 70);
            this.buttonStartCopy.Margin = new System.Windows.Forms.Padding(4);
            this.buttonStartCopy.Name = "buttonStartCopy";
            this.buttonStartCopy.Size = new System.Drawing.Size(98, 39);
            this.buttonStartCopy.TabIndex = 37;
            this.buttonStartCopy.Text = "Start Copy";
            this.buttonStartCopy.UseVisualStyleBackColor = false;
            this.buttonStartCopy.Click += new System.EventHandler(this.buttonStartCopy_Click);
            // 
            // CameraSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1894, 1031);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonSelectPath);
            this.Controls.Add(this.labelCode);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.labelActPos);
            this.Controls.Add(this.comboBoxModel);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelPoseCount);
            this.Controls.Add(this.zoomPictureBoxPose);
            this.Controls.Add(this.buttonSaveSetup);
            this.Controls.Add(this.buttonSavePose);
            this.Controls.Add(this.poseControl1);
            this.Controls.Add(this.flowLayoutPanelPose);
            this.Controls.Add(this.zoomInOutPictureBox1);
            this.Controls.Add(this.label1);
            this.Name = "CameraSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CameraSetup";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CameraSetup_FormClosing);
            this.Load += new System.EventHandler(this.CameraSetup_Load);
            this.Shown += new System.EventHandler(this.CameraSetup_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private CustomControl.ZoomInOutPictureBox zoomInOutPictureBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelPose;
        private CustomControl.PoseControl poseControl1;
        private System.Windows.Forms.Button buttonSavePose;
        private System.Windows.Forms.Button buttonSaveSetup;
        private System.Windows.Forms.Label labelPoseCount;
        private CustomControl.ZoomInOutPictureBox zoomPictureBoxPose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxModel;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label labelActPos;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Label labelCode;
        private System.Windows.Forms.Button buttonSelectPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxOffsetY;
        private System.Windows.Forms.Button buttonStartCopy;
    }
}