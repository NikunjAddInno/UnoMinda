namespace VoltasBeko
{
    partial class InspectionForm
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
            this.label11 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelInspectionStats = new System.Windows.Forms.Panel();
            this.buttonReset = new System.Windows.Forms.Button();
            this.labelTotal = new System.Windows.Forms.Label();
            this.labelOk = new System.Windows.Forms.Label();
            this.labelResult = new System.Windows.Forms.Label();
            this.labelNg = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonLight = new System.Windows.Forms.Button();
            this.buttonReport = new System.Windows.Forms.Button();
            this.buttonFrontCamSetup = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.checkBoxSaveImg1 = new System.Windows.Forms.CheckBox();
            this.labelAppMode = new System.Windows.Forms.Label();
            this.buttonPlc = new System.Windows.Forms.Button();
            this.buttonCam1 = new System.Windows.Forms.Button();
            this.comboBoxModel = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.labelFinalResultCount = new System.Windows.Forms.Label();
            this.labelCode1 = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.backgroundWorkerDatabase = new System.ComponentModel.BackgroundWorker();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkBoxQr = new System.Windows.Forms.CheckBox();
            this.radioButtonSingleImage = new System.Windows.Forms.RadioButton();
            this.radioButtonFull = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonRecheck = new System.Windows.Forms.Button();
            this.buttonByPass = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.buttonFalseCall = new System.Windows.Forms.Button();
            this.zoomInOutPictureBoxFull = new VoltasBeko.CustomControl.ZoomInOutPictureBox();
            this.zoomInOutPictureBox1 = new VoltasBeko.CustomControl.ZoomInOutPictureBox();
            this.buttonApi = new System.Windows.Forms.Button();
            this.labelCode2 = new System.Windows.Forms.Label();
            this.panelInspectionStats.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Silver;
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(0, 1009);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1894, 22);
            this.label11.TabIndex = 3;
            this.label11.Text = "Developed by Add Innovations Pvt Ltd";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(62)))), ((int)(((byte)(138)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1894, 47);
            this.label1.TabIndex = 4;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // panelInspectionStats
            // 
            this.panelInspectionStats.BackColor = System.Drawing.Color.White;
            this.panelInspectionStats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelInspectionStats.Controls.Add(this.buttonReset);
            this.panelInspectionStats.Controls.Add(this.labelTotal);
            this.panelInspectionStats.Controls.Add(this.labelOk);
            this.panelInspectionStats.Controls.Add(this.labelResult);
            this.panelInspectionStats.Controls.Add(this.labelNg);
            this.panelInspectionStats.Controls.Add(this.label14);
            this.panelInspectionStats.Controls.Add(this.label13);
            this.panelInspectionStats.Controls.Add(this.label10);
            this.panelInspectionStats.Controls.Add(this.label6);
            this.panelInspectionStats.Controls.Add(this.label4);
            this.panelInspectionStats.Location = new System.Drawing.Point(1718, 56);
            this.panelInspectionStats.Name = "panelInspectionStats";
            this.panelInspectionStats.Size = new System.Drawing.Size(164, 461);
            this.panelInspectionStats.TabIndex = 10;
            // 
            // buttonReset
            // 
            this.buttonReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonReset.Location = new System.Drawing.Point(14, 411);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(129, 35);
            this.buttonReset.TabIndex = 5;
            this.buttonReset.Text = "Reset Count";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // labelTotal
            // 
            this.labelTotal.BackColor = System.Drawing.Color.White;
            this.labelTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTotal.ForeColor = System.Drawing.Color.Black;
            this.labelTotal.Location = new System.Drawing.Point(11, 190);
            this.labelTotal.Name = "labelTotal";
            this.labelTotal.Size = new System.Drawing.Size(134, 23);
            this.labelTotal.TabIndex = 4;
            this.labelTotal.Text = "00";
            this.labelTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelOk
            // 
            this.labelOk.BackColor = System.Drawing.Color.White;
            this.labelOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOk.ForeColor = System.Drawing.Color.Black;
            this.labelOk.Location = new System.Drawing.Point(11, 38);
            this.labelOk.Name = "labelOk";
            this.labelOk.Size = new System.Drawing.Size(134, 23);
            this.labelOk.TabIndex = 4;
            this.labelOk.Text = "00";
            this.labelOk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelResult
            // 
            this.labelResult.BackColor = System.Drawing.Color.LightGray;
            this.labelResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelResult.ForeColor = System.Drawing.Color.Black;
            this.labelResult.Location = new System.Drawing.Point(8, 255);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(140, 109);
            this.labelResult.TabIndex = 4;
            this.labelResult.Text = "RESULT";
            this.labelResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelResult.Click += new System.EventHandler(this.labelResult_Click);
            // 
            // labelNg
            // 
            this.labelNg.BackColor = System.Drawing.Color.White;
            this.labelNg.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNg.ForeColor = System.Drawing.Color.Black;
            this.labelNg.Location = new System.Drawing.Point(11, 118);
            this.labelNg.Name = "labelNg";
            this.labelNg.Size = new System.Drawing.Size(134, 23);
            this.labelNg.TabIndex = 4;
            this.labelNg.Text = "00";
            this.labelNg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelNg.Click += new System.EventHandler(this.labelNg_Click);
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.White;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(29, 147);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(99, 23);
            this.label14.TabIndex = 4;
            this.label14.Text = "Total Ng";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.White;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(29, 79);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(99, 23);
            this.label13.TabIndex = 4;
            this.label13.Text = "Total Ok";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.White;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(7, 219);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(143, 23);
            this.label10.TabIndex = 4;
            this.label10.Text = "Total Inspected";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(16, 376);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 23);
            this.label6.TabIndex = 4;
            this.label6.Text = "Inspection Result";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Gold;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(162, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "INSPECTION STATS";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(805, 86);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(902, 920);
            this.flowLayoutPanel1.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(119)))), ((int)(((byte)(182)))));
            this.label8.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(20, 55);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(770, 28);
            this.label8.TabIndex = 33;
            this.label8.Text = "CAMERA VIEW";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(119)))), ((int)(((byte)(182)))));
            this.label2.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(805, 55);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(901, 28);
            this.label2.TabIndex = 33;
            this.label2.Text = "POSE PREVIEW";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.buttonLight);
            this.panel1.Controls.Add(this.buttonReport);
            this.panel1.Controls.Add(this.buttonFrontCamSetup);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Location = new System.Drawing.Point(1718, 524);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(160, 180);
            this.panel1.TabIndex = 10;
            // 
            // buttonLight
            // 
            this.buttonLight.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonLight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLight.Location = new System.Drawing.Point(24, 138);
            this.buttonLight.Name = "buttonLight";
            this.buttonLight.Size = new System.Drawing.Size(113, 30);
            this.buttonLight.TabIndex = 4;
            this.buttonLight.Text = "Light";
            this.buttonLight.UseVisualStyleBackColor = false;
            this.buttonLight.Click += new System.EventHandler(this.buttonLight_Click);
            // 
            // buttonReport
            // 
            this.buttonReport.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonReport.Location = new System.Drawing.Point(24, 88);
            this.buttonReport.Name = "buttonReport";
            this.buttonReport.Size = new System.Drawing.Size(113, 30);
            this.buttonReport.TabIndex = 4;
            this.buttonReport.Text = "Report";
            this.buttonReport.UseVisualStyleBackColor = false;
            this.buttonReport.Click += new System.EventHandler(this.buttonReport_Click);
            // 
            // buttonFrontCamSetup
            // 
            this.buttonFrontCamSetup.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonFrontCamSetup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFrontCamSetup.Location = new System.Drawing.Point(24, 43);
            this.buttonFrontCamSetup.Name = "buttonFrontCamSetup";
            this.buttonFrontCamSetup.Size = new System.Drawing.Size(113, 30);
            this.buttonFrontCamSetup.TabIndex = 4;
            this.buttonFrontCamSetup.Text = "Camera Setup";
            this.buttonFrontCamSetup.UseVisualStyleBackColor = false;
            this.buttonFrontCamSetup.Click += new System.EventHandler(this.buttonFrontCamSetup_Click);
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Top;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Black;
            this.label19.Location = new System.Drawing.Point(0, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(158, 25);
            this.label19.TabIndex = 3;
            this.label19.Text = "SYSTEM CONTROL";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label19.Click += new System.EventHandler(this.label19_Click);
            // 
            // checkBoxSaveImg1
            // 
            this.checkBoxSaveImg1.AutoSize = true;
            this.checkBoxSaveImg1.ForeColor = System.Drawing.Color.Black;
            this.checkBoxSaveImg1.Location = new System.Drawing.Point(35, 82);
            this.checkBoxSaveImg1.Name = "checkBoxSaveImg1";
            this.checkBoxSaveImg1.Size = new System.Drawing.Size(83, 17);
            this.checkBoxSaveImg1.TabIndex = 9;
            this.checkBoxSaveImg1.Text = "Save Image";
            this.checkBoxSaveImg1.UseVisualStyleBackColor = true;
            // 
            // labelAppMode
            // 
            this.labelAppMode.BackColor = System.Drawing.Color.White;
            this.labelAppMode.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAppMode.ForeColor = System.Drawing.Color.Black;
            this.labelAppMode.Location = new System.Drawing.Point(369, 11);
            this.labelAppMode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAppMode.Name = "labelAppMode";
            this.labelAppMode.Size = new System.Drawing.Size(204, 26);
            this.labelAppMode.TabIndex = 33;
            this.labelAppMode.Text = "App Mode";
            this.labelAppMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelAppMode.Click += new System.EventHandler(this.label17_Click);
            // 
            // buttonPlc
            // 
            this.buttonPlc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPlc.ForeColor = System.Drawing.Color.DimGray;
            this.buttonPlc.Location = new System.Drawing.Point(1836, 3);
            this.buttonPlc.Name = "buttonPlc";
            this.buttonPlc.Size = new System.Drawing.Size(45, 38);
            this.buttonPlc.TabIndex = 8;
            this.buttonPlc.UseVisualStyleBackColor = true;
            this.buttonPlc.Click += new System.EventHandler(this.buttonPlc_Click);
            // 
            // buttonCam1
            // 
            this.buttonCam1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCam1.ForeColor = System.Drawing.Color.DimGray;
            this.buttonCam1.Location = new System.Drawing.Point(1775, 3);
            this.buttonCam1.Name = "buttonCam1";
            this.buttonCam1.Size = new System.Drawing.Size(45, 38);
            this.buttonCam1.TabIndex = 8;
            this.buttonCam1.UseVisualStyleBackColor = true;
            this.buttonCam1.Click += new System.EventHandler(this.buttonCam1_Click);
            // 
            // comboBoxModel
            // 
            this.comboBoxModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxModel.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxModel.FormattingEnabled = true;
            this.comboBoxModel.Location = new System.Drawing.Point(195, 8);
            this.comboBoxModel.Name = "comboBoxModel";
            this.comboBoxModel.Size = new System.Drawing.Size(158, 33);
            this.comboBoxModel.TabIndex = 43;
            this.comboBoxModel.SelectedIndexChanged += new System.EventHandler(this.comboBoxModel_SelectedIndexChanged_1);
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.DarkCyan;
            this.label23.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.White;
            this.label23.Location = new System.Drawing.Point(58, 6);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(133, 35);
            this.label23.TabIndex = 42;
            this.label23.Text = "Model Name: ";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::VoltasBeko.Properties.Resources.MindaLogo;
            this.pictureBox3.Location = new System.Drawing.Point(-1, 0);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(53, 47);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 9;
            this.pictureBox3.TabStop = false;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // labelFinalResultCount
            // 
            this.labelFinalResultCount.AutoSize = true;
            this.labelFinalResultCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFinalResultCount.Location = new System.Drawing.Point(613, 14);
            this.labelFinalResultCount.Name = "labelFinalResultCount";
            this.labelFinalResultCount.Size = new System.Drawing.Size(0, 20);
            this.labelFinalResultCount.TabIndex = 45;
            // 
            // labelCode1
            // 
            this.labelCode1.BackColor = System.Drawing.Color.White;
            this.labelCode1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCode1.ForeColor = System.Drawing.Color.Black;
            this.labelCode1.Location = new System.Drawing.Point(599, 7);
            this.labelCode1.Name = "labelCode1";
            this.labelCode1.Size = new System.Drawing.Size(410, 33);
            this.labelCode1.TabIndex = 73;
            this.labelCode1.Text = "Code 1:";
            this.labelCode1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // serialPort1
            // 
            this.serialPort1.ReadTimeout = 5000;
            // 
            // backgroundWorkerDatabase
            // 
            this.backgroundWorkerDatabase.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerDatabase_DoWork);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.checkBoxSaveImg1);
            this.panel2.Controls.Add(this.checkBoxQr);
            this.panel2.Controls.Add(this.radioButtonSingleImage);
            this.panel2.Controls.Add(this.radioButtonFull);
            this.panel2.Location = new System.Drawing.Point(1718, 712);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(164, 105);
            this.panel2.TabIndex = 74;
            // 
            // checkBoxQr
            // 
            this.checkBoxQr.AutoSize = true;
            this.checkBoxQr.Checked = true;
            this.checkBoxQr.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxQr.Location = new System.Drawing.Point(35, 56);
            this.checkBoxQr.Name = "checkBoxQr";
            this.checkBoxQr.Size = new System.Drawing.Size(85, 17);
            this.checkBoxQr.TabIndex = 1;
            this.checkBoxQr.Text = "checkBoxQr";
            this.checkBoxQr.UseVisualStyleBackColor = true;
            this.checkBoxQr.Click += new System.EventHandler(this.checkBoxQr_Click);
            // 
            // radioButtonSingleImage
            // 
            this.radioButtonSingleImage.AutoSize = true;
            this.radioButtonSingleImage.Location = new System.Drawing.Point(35, 4);
            this.radioButtonSingleImage.Name = "radioButtonSingleImage";
            this.radioButtonSingleImage.Size = new System.Drawing.Size(86, 17);
            this.radioButtonSingleImage.TabIndex = 0;
            this.radioButtonSingleImage.TabStop = true;
            this.radioButtonSingleImage.Text = "Single Image";
            this.radioButtonSingleImage.UseVisualStyleBackColor = true;
            // 
            // radioButtonFull
            // 
            this.radioButtonFull.AutoSize = true;
            this.radioButtonFull.Location = new System.Drawing.Point(35, 30);
            this.radioButtonFull.Name = "radioButtonFull";
            this.radioButtonFull.Size = new System.Drawing.Size(73, 17);
            this.radioButtonFull.TabIndex = 0;
            this.radioButtonFull.TabStop = true;
            this.radioButtonFull.Text = "Full Image";
            this.radioButtonFull.UseVisualStyleBackColor = true;
            this.radioButtonFull.CheckedChanged += new System.EventHandler(this.radioButtonFull_CheckedChanged);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(119)))), ((int)(((byte)(182)))));
            this.label3.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(19, 56);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1687, 28);
            this.label3.TabIndex = 33;
            this.label3.Text = "FULL VIEW";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label3.Visible = false;
            // 
            // buttonRecheck
            // 
            this.buttonRecheck.BackColor = System.Drawing.Color.Yellow;
            this.buttonRecheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRecheck.Location = new System.Drawing.Point(23, 16);
            this.buttonRecheck.Name = "buttonRecheck";
            this.buttonRecheck.Size = new System.Drawing.Size(113, 30);
            this.buttonRecheck.TabIndex = 4;
            this.buttonRecheck.Text = "Re-Check";
            this.buttonRecheck.UseVisualStyleBackColor = false;
            this.buttonRecheck.Click += new System.EventHandler(this.buttonRecheck_Click);
            // 
            // buttonByPass
            // 
            this.buttonByPass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonByPass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonByPass.ForeColor = System.Drawing.Color.Black;
            this.buttonByPass.Location = new System.Drawing.Point(25, 61);
            this.buttonByPass.Name = "buttonByPass";
            this.buttonByPass.Size = new System.Drawing.Size(113, 30);
            this.buttonByPass.TabIndex = 4;
            this.buttonByPass.Text = "By Pass";
            this.buttonByPass.UseVisualStyleBackColor = false;
            this.buttonByPass.Click += new System.EventHandler(this.buttonByPass_Click);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.buttonRecheck);
            this.panel3.Controls.Add(this.buttonFalseCall);
            this.panel3.Controls.Add(this.buttonByPass);
            this.panel3.Location = new System.Drawing.Point(1718, 828);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(164, 154);
            this.panel3.TabIndex = 74;
            // 
            // buttonFalseCall
            // 
            this.buttonFalseCall.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.buttonFalseCall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFalseCall.ForeColor = System.Drawing.Color.Black;
            this.buttonFalseCall.Location = new System.Drawing.Point(24, 106);
            this.buttonFalseCall.Name = "buttonFalseCall";
            this.buttonFalseCall.Size = new System.Drawing.Size(113, 30);
            this.buttonFalseCall.TabIndex = 4;
            this.buttonFalseCall.Text = "False Call";
            this.buttonFalseCall.UseVisualStyleBackColor = false;
            this.buttonFalseCall.Click += new System.EventHandler(this.buttonFalseCall_Click);
            // 
            // zoomInOutPictureBoxFull
            // 
            this.zoomInOutPictureBoxFull.BackColor = System.Drawing.Color.White;
            this.zoomInOutPictureBoxFull.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.zoomInOutPictureBoxFull.ForeColor = System.Drawing.Color.Black;
            this.zoomInOutPictureBoxFull.Location = new System.Drawing.Point(19, 87);
            this.zoomInOutPictureBoxFull.Name = "zoomInOutPictureBoxFull";
            this.zoomInOutPictureBoxFull.PictureBoxImage = null;
            this.zoomInOutPictureBoxFull.PictureBoxSize = new System.Drawing.Size(1687, 910);
            this.zoomInOutPictureBoxFull.Size = new System.Drawing.Size(1687, 910);
            this.zoomInOutPictureBoxFull.TabIndex = 11;
            this.zoomInOutPictureBoxFull.Visible = false;
            // 
            // zoomInOutPictureBox1
            // 
            this.zoomInOutPictureBox1.BackColor = System.Drawing.Color.White;
            this.zoomInOutPictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.zoomInOutPictureBox1.ForeColor = System.Drawing.Color.Black;
            this.zoomInOutPictureBox1.Location = new System.Drawing.Point(20, 86);
            this.zoomInOutPictureBox1.Name = "zoomInOutPictureBox1";
            this.zoomInOutPictureBox1.PictureBoxImage = null;
            this.zoomInOutPictureBox1.PictureBoxSize = new System.Drawing.Size(770, 920);
            this.zoomInOutPictureBox1.Size = new System.Drawing.Size(770, 920);
            this.zoomInOutPictureBox1.TabIndex = 11;
            // 
            // buttonApi
            // 
            this.buttonApi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonApi.ForeColor = System.Drawing.Color.DimGray;
            this.buttonApi.Location = new System.Drawing.Point(1718, 3);
            this.buttonApi.Name = "buttonApi";
            this.buttonApi.Size = new System.Drawing.Size(45, 38);
            this.buttonApi.TabIndex = 8;
            this.buttonApi.UseVisualStyleBackColor = true;
            this.buttonApi.Click += new System.EventHandler(this.buttonCam1_Click);
            // 
            // labelCode2
            // 
            this.labelCode2.BackColor = System.Drawing.Color.White;
            this.labelCode2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCode2.ForeColor = System.Drawing.Color.Black;
            this.labelCode2.Location = new System.Drawing.Point(1046, 7);
            this.labelCode2.Name = "labelCode2";
            this.labelCode2.Size = new System.Drawing.Size(410, 33);
            this.labelCode2.TabIndex = 73;
            this.labelCode2.Text = "Code 2:";
            this.labelCode2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelCode2.Click += new System.EventHandler(this.labelCode2_Click);
            // 
            // InspectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1894, 1031);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.zoomInOutPictureBoxFull);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.labelAppMode);
            this.Controls.Add(this.labelCode2);
            this.Controls.Add(this.labelCode1);
            this.Controls.Add(this.labelFinalResultCount);
            this.Controls.Add(this.comboBoxModel);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.buttonPlc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonApi);
            this.Controls.Add(this.buttonCam1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.zoomInOutPictureBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelInspectionStats);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label1);
            this.Name = "InspectionForm";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InspectionForm_FormClosing);
            this.Load += new System.EventHandler(this.InspectionForm_Load);
            this.Shown += new System.EventHandler(this.InspectionForm_Shown);
            this.panelInspectionStats.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Panel panelInspectionStats;
        private System.Windows.Forms.Label labelTotal;
        private System.Windows.Forms.Label labelOk;
        private System.Windows.Forms.Label labelResult;
        private System.Windows.Forms.Label labelNg;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private CustomControl.ZoomInOutPictureBox zoomInOutPictureBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button buttonFrontCamSetup;
        private System.Windows.Forms.Button buttonCam1;
        private System.Windows.Forms.Button buttonPlc;
        private System.Windows.Forms.ComboBox comboBoxModel;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label labelAppMode;
        private System.Windows.Forms.Button buttonReport;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label labelFinalResultCount;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Label labelCode1;
        private System.Windows.Forms.Button buttonLight;
        private System.IO.Ports.SerialPort serialPort1;
        private System.ComponentModel.BackgroundWorker backgroundWorkerDatabase;
        private System.Windows.Forms.CheckBox checkBoxSaveImg1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton radioButtonSingleImage;
        private System.Windows.Forms.RadioButton radioButtonFull;
        private CustomControl.ZoomInOutPictureBox zoomInOutPictureBoxFull;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonRecheck;
        private System.Windows.Forms.Button buttonByPass;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button buttonApi;
        private System.Windows.Forms.Label labelCode2;
        private System.Windows.Forms.CheckBox checkBoxQr;
        private System.Windows.Forms.Button buttonFalseCall;
    }
}

