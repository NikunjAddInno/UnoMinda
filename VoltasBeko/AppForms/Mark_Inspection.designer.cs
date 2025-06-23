namespace tryThresholds
{
    partial class Mark_Inspection
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
            this.pnl_zoom = new System.Windows.Forms.Panel();
            this.pb_Zoom = new System.Windows.Forms.PictureBox();
            this.cmbTool = new System.Windows.Forms.ComboBox();
            this.pnlToolSettings = new System.Windows.Forms.Panel();
            this.lblROI_status = new System.Windows.Forms.Label();
            this.btnSelectROI = new System.Windows.Forms.Button();
            this.ReselectROI = new System.Windows.Forms.Button();
            this.pnlRoiAct = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbFlipmode = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.btnRotate = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.nudRotationAngle = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlCrud = new System.Windows.Forms.Panel();
            this.buttonScanCode = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.listBoxAppliedTools = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblToolCnt = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnLoadTools = new System.Windows.Forms.Button();
            this.pnlToolList = new System.Windows.Forms.Panel();
            this.btntestColour = new System.Windows.Forms.Button();
            this.btnTestImage = new System.Windows.Forms.Button();
            this.btnSelectImage = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pnlDebug = new System.Windows.Forms.Panel();
            this.btnResetView = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lblDebugImgName = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.labelCode = new System.Windows.Forms.Label();
            this.btnFIxtureConfig = new System.Windows.Forms.Button();
            this.pnl_zoom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Zoom)).BeginInit();
            this.pnlRoiAct.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRotationAngle)).BeginInit();
            this.pnlCrud.SuspendLayout();
            this.pnlToolList.SuspendLayout();
            this.pnlDebug.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_zoom
            // 
            this.pnl_zoom.Controls.Add(this.pb_Zoom);
            this.pnl_zoom.Location = new System.Drawing.Point(492, 12);
            this.pnl_zoom.Name = "pnl_zoom";
            this.pnl_zoom.Size = new System.Drawing.Size(960, 1003);
            this.pnl_zoom.TabIndex = 6;
            // 
            // pb_Zoom
            // 
            this.pb_Zoom.Location = new System.Drawing.Point(3, 3);
            this.pb_Zoom.Name = "pb_Zoom";
            this.pb_Zoom.Size = new System.Drawing.Size(954, 997);
            this.pb_Zoom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_Zoom.TabIndex = 0;
            this.pb_Zoom.TabStop = false;
            // 
            // cmbTool
            // 
            this.cmbTool.FormattingEnabled = true;
            this.cmbTool.Location = new System.Drawing.Point(78, 40);
            this.cmbTool.Name = "cmbTool";
            this.cmbTool.Size = new System.Drawing.Size(154, 21);
            this.cmbTool.TabIndex = 7;
            this.cmbTool.SelectedIndexChanged += new System.EventHandler(this.cmbTool_SelectedIndexChanged);
            // 
            // pnlToolSettings
            // 
            this.pnlToolSettings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlToolSettings.Location = new System.Drawing.Point(53, 308);
            this.pnlToolSettings.Name = "pnlToolSettings";
            this.pnlToolSettings.Size = new System.Drawing.Size(360, 454);
            this.pnlToolSettings.TabIndex = 8;
            // 
            // lblROI_status
            // 
            this.lblROI_status.AutoSize = true;
            this.lblROI_status.Location = new System.Drawing.Point(8, 43);
            this.lblROI_status.Name = "lblROI_status";
            this.lblROI_status.Size = new System.Drawing.Size(64, 13);
            this.lblROI_status.TabIndex = 9;
            this.lblROI_status.Text = "Select Tool:";
            // 
            // btnSelectROI
            // 
            this.btnSelectROI.Location = new System.Drawing.Point(226, 38);
            this.btnSelectROI.Name = "btnSelectROI";
            this.btnSelectROI.Size = new System.Drawing.Size(118, 27);
            this.btnSelectROI.TabIndex = 11;
            this.btnSelectROI.Text = "Select ROI";
            this.btnSelectROI.UseVisualStyleBackColor = true;
            this.btnSelectROI.Click += new System.EventHandler(this.btnSelectROI_Click);
            // 
            // ReselectROI
            // 
            this.ReselectROI.Location = new System.Drawing.Point(226, 76);
            this.ReselectROI.Name = "ReselectROI";
            this.ReselectROI.Size = new System.Drawing.Size(118, 27);
            this.ReselectROI.TabIndex = 12;
            this.ReselectROI.Text = "Reselect ROI";
            this.ReselectROI.UseVisualStyleBackColor = true;
            this.ReselectROI.Click += new System.EventHandler(this.ReselectROI_Click);
            // 
            // pnlRoiAct
            // 
            this.pnlRoiAct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRoiAct.Controls.Add(this.label5);
            this.pnlRoiAct.Controls.Add(this.cmbFlipmode);
            this.pnlRoiAct.Controls.Add(this.label15);
            this.pnlRoiAct.Controls.Add(this.btnRotate);
            this.pnlRoiAct.Controls.Add(this.label2);
            this.pnlRoiAct.Controls.Add(this.nudRotationAngle);
            this.pnlRoiAct.Controls.Add(this.ReselectROI);
            this.pnlRoiAct.Controls.Add(this.btnSelectROI);
            this.pnlRoiAct.Location = new System.Drawing.Point(457, 31);
            this.pnlRoiAct.Name = "pnlRoiAct";
            this.pnlRoiAct.Size = new System.Drawing.Size(360, 114);
            this.pnlRoiAct.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Flip:";
            // 
            // cmbFlipmode
            // 
            this.cmbFlipmode.FormattingEnabled = true;
            this.cmbFlipmode.Items.AddRange(new object[] {
            "flipX",
            "flipY",
            "flipBoth_XY",
            "No_flipping"});
            this.cmbFlipmode.Location = new System.Drawing.Point(44, 34);
            this.cmbFlipmode.Name = "cmbFlipmode";
            this.cmbFlipmode.Size = new System.Drawing.Size(91, 21);
            this.cmbFlipmode.TabIndex = 20;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.MediumOrchid;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(-5, 0);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(365, 31);
            this.label15.TabIndex = 19;
            this.label15.Text = "ROI Actions";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRotate
            // 
            this.btnRotate.Location = new System.Drawing.Point(71, 83);
            this.btnRotate.Name = "btnRotate";
            this.btnRotate.Size = new System.Drawing.Size(64, 21);
            this.btnRotate.TabIndex = 15;
            this.btnRotate.Text = "Rotate";
            this.btnRotate.UseVisualStyleBackColor = true;
            this.btnRotate.Click += new System.EventHandler(this.btnRotate_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Rotation (-135 to + 135):";
            // 
            // nudRotationAngle
            // 
            this.nudRotationAngle.DecimalPlaces = 1;
            this.nudRotationAngle.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.nudRotationAngle.Location = new System.Drawing.Point(15, 83);
            this.nudRotationAngle.Maximum = new decimal(new int[] {
            135,
            0,
            0,
            0});
            this.nudRotationAngle.Minimum = new decimal(new int[] {
            135,
            0,
            0,
            -2147483648});
            this.nudRotationAngle.Name = "nudRotationAngle";
            this.nudRotationAngle.Size = new System.Drawing.Size(50, 20);
            this.nudRotationAngle.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.MediumOrchid;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(-2, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(361, 31);
            this.label1.TabIndex = 20;
            this.label1.Text = "Tool Setup";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlCrud
            // 
            this.pnlCrud.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCrud.Controls.Add(this.buttonScanCode);
            this.pnlCrud.Controls.Add(this.btnDelete);
            this.pnlCrud.Controls.Add(this.btnUpdate);
            this.pnlCrud.Controls.Add(this.btnAddNew);
            this.pnlCrud.Location = new System.Drawing.Point(53, 766);
            this.pnlCrud.Name = "pnlCrud";
            this.pnlCrud.Size = new System.Drawing.Size(360, 58);
            this.pnlCrud.TabIndex = 21;
            // 
            // buttonScanCode
            // 
            this.buttonScanCode.Location = new System.Drawing.Point(269, 2);
            this.buttonScanCode.Name = "buttonScanCode";
            this.buttonScanCode.Size = new System.Drawing.Size(72, 52);
            this.buttonScanCode.TabIndex = 2;
            this.buttonScanCode.Text = "Scan Code";
            this.buttonScanCode.UseVisualStyleBackColor = true;
            this.buttonScanCode.Click += new System.EventHandler(this.buttonScanCode_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(183, 2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(72, 52);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete Selected";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(97, 2);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(72, 52);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "Update Selected";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnAddNew
            // 
            this.btnAddNew.Location = new System.Drawing.Point(11, 2);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(72, 52);
            this.btnAddNew.TabIndex = 0;
            this.btnAddNew.Text = "Add New";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // listBoxAppliedTools
            // 
            this.listBoxAppliedTools.FormattingEnabled = true;
            this.listBoxAppliedTools.Location = new System.Drawing.Point(9, 72);
            this.listBoxAppliedTools.Name = "listBoxAppliedTools";
            this.listBoxAppliedTools.Size = new System.Drawing.Size(223, 212);
            this.listBoxAppliedTools.TabIndex = 22;
            this.listBoxAppliedTools.SelectedIndexChanged += new System.EventHandler(this.listBoxAppliedTools_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(247, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Tool Count:";
            // 
            // lblToolCnt
            // 
            this.lblToolCnt.AutoSize = true;
            this.lblToolCnt.Location = new System.Drawing.Point(317, 43);
            this.lblToolCnt.Name = "lblToolCnt";
            this.lblToolCnt.Size = new System.Drawing.Size(27, 13);
            this.lblToolCnt.TabIndex = 24;
            this.lblToolCnt.Text = "N/A";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(236, 215);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 69);
            this.button1.TabIndex = 25;
            this.button1.Text = "Save Data";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnLoadTools
            // 
            this.btnLoadTools.Location = new System.Drawing.Point(238, 72);
            this.btnLoadTools.Name = "btnLoadTools";
            this.btnLoadTools.Size = new System.Drawing.Size(119, 29);
            this.btnLoadTools.TabIndex = 26;
            this.btnLoadTools.Text = "Load From File";
            this.btnLoadTools.UseVisualStyleBackColor = true;
            this.btnLoadTools.Visible = false;
            this.btnLoadTools.Click += new System.EventHandler(this.btnLoadTools_Click);
            // 
            // pnlToolList
            // 
            this.pnlToolList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlToolList.Controls.Add(this.btntestColour);
            this.pnlToolList.Controls.Add(this.button1);
            this.pnlToolList.Controls.Add(this.listBoxAppliedTools);
            this.pnlToolList.Controls.Add(this.btnLoadTools);
            this.pnlToolList.Controls.Add(this.lblROI_status);
            this.pnlToolList.Controls.Add(this.cmbTool);
            this.pnlToolList.Controls.Add(this.lblToolCnt);
            this.pnlToolList.Controls.Add(this.label1);
            this.pnlToolList.Controls.Add(this.label3);
            this.pnlToolList.Location = new System.Drawing.Point(53, 15);
            this.pnlToolList.Name = "pnlToolList";
            this.pnlToolList.Size = new System.Drawing.Size(360, 287);
            this.pnlToolList.TabIndex = 27;
            // 
            // btntestColour
            // 
            this.btntestColour.Location = new System.Drawing.Point(236, 72);
            this.btntestColour.Name = "btntestColour";
            this.btntestColour.Size = new System.Drawing.Size(119, 59);
            this.btntestColour.TabIndex = 33;
            this.btntestColour.Text = "Test Colour";
            this.btntestColour.UseVisualStyleBackColor = true;
            this.btntestColour.Click += new System.EventHandler(this.btntestColour_Click);
            // 
            // btnTestImage
            // 
            this.btnTestImage.Location = new System.Drawing.Point(133, 26);
            this.btnTestImage.Name = "btnTestImage";
            this.btnTestImage.Size = new System.Drawing.Size(99, 23);
            this.btnTestImage.TabIndex = 28;
            this.btnTestImage.Text = "Test Image";
            this.btnTestImage.UseVisualStyleBackColor = true;
            this.btnTestImage.Click += new System.EventHandler(this.btnTestImage_Click);
            // 
            // btnSelectImage
            // 
            this.btnSelectImage.Location = new System.Drawing.Point(18, 26);
            this.btnSelectImage.Name = "btnSelectImage";
            this.btnSelectImage.Size = new System.Drawing.Size(99, 23);
            this.btnSelectImage.TabIndex = 29;
            this.btnSelectImage.Text = "Select Image";
            this.btnSelectImage.UseVisualStyleBackColor = true;
            this.btnSelectImage.Click += new System.EventHandler(this.btnSelectImage_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Crimson;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(292, 968);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(138, 44);
            this.button2.TabIndex = 30;
            this.button2.Text = "Exit";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // pnlDebug
            // 
            this.pnlDebug.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDebug.Controls.Add(this.btnResetView);
            this.pnlDebug.Controls.Add(this.label4);
            this.pnlDebug.Controls.Add(this.btnTestImage);
            this.pnlDebug.Controls.Add(this.btnSelectImage);
            this.pnlDebug.Location = new System.Drawing.Point(53, 828);
            this.pnlDebug.Name = "pnlDebug";
            this.pnlDebug.Size = new System.Drawing.Size(360, 58);
            this.pnlDebug.TabIndex = 31;
            // 
            // btnResetView
            // 
            this.btnResetView.Location = new System.Drawing.Point(250, 26);
            this.btnResetView.Name = "btnResetView";
            this.btnResetView.Size = new System.Drawing.Size(99, 23);
            this.btnResetView.TabIndex = 31;
            this.btnResetView.Text = "Reset Image";
            this.btnResetView.UseVisualStyleBackColor = true;
            this.btnResetView.Click += new System.EventHandler(this.btnResetView_Click);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.MediumOrchid;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(-2, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(361, 20);
            this.label4.TabIndex = 30;
            this.label4.Text = "Debug";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDebugImgName
            // 
            this.lblDebugImgName.BackColor = System.Drawing.Color.MediumOrchid;
            this.lblDebugImgName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDebugImgName.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDebugImgName.ForeColor = System.Drawing.Color.White;
            this.lblDebugImgName.Location = new System.Drawing.Point(495, 1032);
            this.lblDebugImgName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDebugImgName.Name = "lblDebugImgName";
            this.lblDebugImgName.Size = new System.Drawing.Size(954, 20);
            this.lblDebugImgName.TabIndex = 32;
            this.lblDebugImgName.Text = "Debug Image:";
            this.lblDebugImgName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // labelCode
            // 
            this.labelCode.AutoSize = true;
            this.labelCode.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCode.Location = new System.Drawing.Point(53, 893);
            this.labelCode.Name = "labelCode";
            this.labelCode.Size = new System.Drawing.Size(60, 25);
            this.labelCode.TabIndex = 33;
            this.labelCode.Text = "Code:";
            // 
            // btnFIxtureConfig
            // 
            this.btnFIxtureConfig.Location = new System.Drawing.Point(419, 765);
            this.btnFIxtureConfig.Name = "btnFIxtureConfig";
            this.btnFIxtureConfig.Size = new System.Drawing.Size(55, 61);
            this.btnFIxtureConfig.TabIndex = 34;
            this.btnFIxtureConfig.Text = "Fixture Setting";
            this.btnFIxtureConfig.UseVisualStyleBackColor = true;
            this.btnFIxtureConfig.Click += new System.EventHandler(this.btnFIxtureConfig_Click);
            // 
            // Mark_Inspection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(1471, 1061);
            this.ControlBox = false;
            this.Controls.Add(this.btnFIxtureConfig);
            this.Controls.Add(this.labelCode);
            this.Controls.Add(this.lblDebugImgName);
            this.Controls.Add(this.pnlDebug);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pnlToolList);
            this.Controls.Add(this.pnlCrud);
            this.Controls.Add(this.pnlToolSettings);
            this.Controls.Add(this.pnl_zoom);
            this.Controls.Add(this.pnlRoiAct);
            this.Name = "Mark_Inspection";
            this.Text = "Mark_Inspection";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Mark_Inspection_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Mark_Inspection_FormClosed);
            this.Load += new System.EventHandler(this.Mark_Inspection_Load);
            this.pnl_zoom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_Zoom)).EndInit();
            this.pnlRoiAct.ResumeLayout(false);
            this.pnlRoiAct.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRotationAngle)).EndInit();
            this.pnlCrud.ResumeLayout(false);
            this.pnlToolList.ResumeLayout(false);
            this.pnlToolList.PerformLayout();
            this.pnlDebug.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnl_zoom;
        private System.Windows.Forms.PictureBox pb_Zoom;
        private System.Windows.Forms.ComboBox cmbTool;
        private System.Windows.Forms.Panel pnlToolSettings;
        private System.Windows.Forms.Label lblROI_status;
        private System.Windows.Forms.Button btnSelectROI;
        private System.Windows.Forms.Button ReselectROI;
        private System.Windows.Forms.Panel pnlRoiAct;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnRotate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudRotationAngle;
        private System.Windows.Forms.Panel pnlCrud;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.ListBox listBoxAppliedTools;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblToolCnt;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnLoadTools;
        private System.Windows.Forms.Panel pnlToolList;
        private System.Windows.Forms.Button btnTestImage;
        private System.Windows.Forms.Button btnSelectImage;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel pnlDebug;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnResetView;
        private System.Windows.Forms.Label lblDebugImgName;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbFlipmode;
        private System.Windows.Forms.Button btntestColour;
        private System.Windows.Forms.Label labelCode;
        private System.Windows.Forms.Button buttonScanCode;
        private System.Windows.Forms.Button btnFIxtureConfig;
    }
}