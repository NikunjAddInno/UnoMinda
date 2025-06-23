namespace tryThresholds.IP_tools
{
    partial class uc_CROI
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblSelectedColour = new System.Windows.Forms.Label();
            this.cmbcolours = new System.Windows.Forms.ComboBox();
            this.lblTemplateName = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblRegion = new System.Windows.Forms.Label();
            this.nudMatchThresh = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.nudRotationTolerance = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_V_shiftTol = new System.Windows.Forms.Label();
            this.lbl_H_shiftTol = new System.Windows.Forms.Label();
            this.nudVshiftTol = new System.Windows.Forms.NumericUpDown();
            this.nudHshiftTol = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.rdbGray = new System.Windows.Forms.RadioButton();
            this.rdbThresh = new System.Windows.Forms.RadioButton();
            this.rdbColour = new System.Windows.Forms.RadioButton();
            this.nudVshiftTol_neg = new System.Windows.Forms.NumericUpDown();
            this.nudHshiftTol_neg = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblRefTool = new System.Windows.Forms.Label();
            this.lblFixtureMode = new System.Windows.Forms.Label();
            this.lblFixtureType = new System.Windows.Forms.Label();
            this.lblD_DRefTool = new System.Windows.Forms.Label();
            this.lblD_Fmode = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudMatchThresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRotationTolerance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVshiftTol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHshiftTol)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVshiftTol_neg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHshiftTol_neg)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSelectedColour
            // 
            this.lblSelectedColour.AutoSize = true;
            this.lblSelectedColour.Location = new System.Drawing.Point(75, 49);
            this.lblSelectedColour.Name = "lblSelectedColour";
            this.lblSelectedColour.Size = new System.Drawing.Size(27, 13);
            this.lblSelectedColour.TabIndex = 132;
            this.lblSelectedColour.Text = "N/A";
            this.lblSelectedColour.Visible = false;
            // 
            // cmbcolours
            // 
            this.cmbcolours.FormattingEnabled = true;
            this.cmbcolours.Location = new System.Drawing.Point(47, 89);
            this.cmbcolours.Name = "cmbcolours";
            this.cmbcolours.Size = new System.Drawing.Size(155, 21);
            this.cmbcolours.TabIndex = 131;
            this.cmbcolours.Visible = false;
            this.cmbcolours.SelectedIndexChanged += new System.EventHandler(this.cmbcolours_SelectedIndexChanged);
            // 
            // lblTemplateName
            // 
            this.lblTemplateName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTemplateName.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblTemplateName.Location = new System.Drawing.Point(-2, 361);
            this.lblTemplateName.Name = "lblTemplateName";
            this.lblTemplateName.Size = new System.Drawing.Size(274, 17);
            this.lblTemplateName.TabIndex = 130;
            this.lblTemplateName.Text = "N/A";
            this.lblTemplateName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnUpdate.Location = new System.Drawing.Point(162, 408);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(107, 31);
            this.btnUpdate.TabIndex = 129;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(78, 43);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(191, 20);
            this.txtName.TabIndex = 128;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label5.Location = new System.Drawing.Point(-3, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(276, 30);
            this.label5.TabIndex = 127;
            this.label5.Text = "       Name :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.DarkSalmon;
            this.label3.Location = new System.Drawing.Point(-3, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(276, 31);
            this.label3.TabIndex = 126;
            this.label3.Text = "Complex ROI Selection";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRegion
            // 
            this.lblRegion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRegion.Location = new System.Drawing.Point(21, 331);
            this.lblRegion.Name = "lblRegion";
            this.lblRegion.Size = new System.Drawing.Size(230, 29);
            this.lblRegion.TabIndex = 124;
            this.lblRegion.Text = "N/A";
            this.lblRegion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nudMatchThresh
            // 
            this.nudMatchThresh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudMatchThresh.DecimalPlaces = 1;
            this.nudMatchThresh.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.nudMatchThresh.Location = new System.Drawing.Point(153, 110);
            this.nudMatchThresh.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudMatchThresh.Name = "nudMatchThresh";
            this.nudMatchThresh.Size = new System.Drawing.Size(98, 20);
            this.nudMatchThresh.TabIndex = 123;
            this.nudMatchThresh.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudMatchThresh.ValueChanged += new System.EventHandler(this.nudMatchThresh_ValueChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Location = new System.Drawing.Point(-3, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(276, 30);
            this.label2.TabIndex = 122;
            this.label2.Text = "       Match Percent";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudRotationTolerance
            // 
            this.nudRotationTolerance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudRotationTolerance.DecimalPlaces = 1;
            this.nudRotationTolerance.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.nudRotationTolerance.Location = new System.Drawing.Point(153, 77);
            this.nudRotationTolerance.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.nudRotationTolerance.Name = "nudRotationTolerance";
            this.nudRotationTolerance.Size = new System.Drawing.Size(98, 20);
            this.nudRotationTolerance.TabIndex = 121;
            this.nudRotationTolerance.ValueChanged += new System.EventHandler(this.nudRotationTolerance_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(-3, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(276, 30);
            this.label1.TabIndex = 120;
            this.label1.Text = "       RotationTolerance";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label4.Location = new System.Drawing.Point(-3, 312);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(276, 66);
            this.label4.TabIndex = 125;
            this.label4.Text = "Region";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbl_V_shiftTol
            // 
            this.lbl_V_shiftTol.AutoSize = true;
            this.lbl_V_shiftTol.Location = new System.Drawing.Point(21, 178);
            this.lbl_V_shiftTol.Name = "lbl_V_shiftTol";
            this.lbl_V_shiftTol.Size = new System.Drawing.Size(120, 13);
            this.lbl_V_shiftTol.TabIndex = 136;
            this.lbl_V_shiftTol.Text = "Y Shift Tolerance (mm) :";
            // 
            // lbl_H_shiftTol
            // 
            this.lbl_H_shiftTol.AutoSize = true;
            this.lbl_H_shiftTol.Location = new System.Drawing.Point(21, 153);
            this.lbl_H_shiftTol.Name = "lbl_H_shiftTol";
            this.lbl_H_shiftTol.Size = new System.Drawing.Size(120, 13);
            this.lbl_H_shiftTol.TabIndex = 135;
            this.lbl_H_shiftTol.Text = "X Shift Tolerance (mm) :";
            // 
            // nudVshiftTol
            // 
            this.nudVshiftTol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudVshiftTol.DecimalPlaces = 2;
            this.nudVshiftTol.Increment = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            this.nudVshiftTol.Location = new System.Drawing.Point(204, 175);
            this.nudVshiftTol.Name = "nudVshiftTol";
            this.nudVshiftTol.Size = new System.Drawing.Size(47, 20);
            this.nudVshiftTol.TabIndex = 134;
            this.nudVshiftTol.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudVshiftTol.ValueChanged += new System.EventHandler(this.nudVshiftTol_ValueChanged);
            // 
            // nudHshiftTol
            // 
            this.nudHshiftTol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudHshiftTol.DecimalPlaces = 2;
            this.nudHshiftTol.Increment = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            this.nudHshiftTol.Location = new System.Drawing.Point(204, 149);
            this.nudHshiftTol.Name = "nudHshiftTol";
            this.nudHshiftTol.Size = new System.Drawing.Size(47, 20);
            this.nudHshiftTol.TabIndex = 133;
            this.nudHshiftTol.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudHshiftTol.ValueChanged += new System.EventHandler(this.nudHshiftTol_ValueChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.trackBar1);
            this.panel1.Controls.Add(this.rdbGray);
            this.panel1.Controls.Add(this.rdbThresh);
            this.panel1.Controls.Add(this.rdbColour);
            this.panel1.Controls.Add(this.cmbcolours);
            this.panel1.Controls.Add(this.lblSelectedColour);
            this.panel1.Location = new System.Drawing.Point(0, 201);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(273, 110);
            this.panel1.TabIndex = 137;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label6.Location = new System.Drawing.Point(-3, 1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(276, 21);
            this.label6.TabIndex = 138;
            this.label6.Text = "Mode";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(129, 49);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(53, 17);
            this.checkBox1.TabIndex = 135;
            this.checkBox1.Text = "Invert";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(33, 72);
            this.trackBar1.Maximum = 255;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(200, 45);
            this.trackBar1.TabIndex = 134;
            this.trackBar1.Value = 120;
            this.trackBar1.Visible = false;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // rdbGray
            // 
            this.rdbGray.AutoSize = true;
            this.rdbGray.Location = new System.Drawing.Point(204, 24);
            this.rdbGray.Name = "rdbGray";
            this.rdbGray.Size = new System.Drawing.Size(47, 17);
            this.rdbGray.TabIndex = 133;
            this.rdbGray.TabStop = true;
            this.rdbGray.Text = "Gray";
            this.rdbGray.UseVisualStyleBackColor = true;
            this.rdbGray.CheckedChanged += new System.EventHandler(this.rdbGray_CheckedChanged);
            // 
            // rdbThresh
            // 
            this.rdbThresh.AutoSize = true;
            this.rdbThresh.Location = new System.Drawing.Point(97, 24);
            this.rdbThresh.Name = "rdbThresh";
            this.rdbThresh.Size = new System.Drawing.Size(72, 17);
            this.rdbThresh.TabIndex = 1;
            this.rdbThresh.TabStop = true;
            this.rdbThresh.Text = "Threshold";
            this.rdbThresh.UseVisualStyleBackColor = true;
            this.rdbThresh.CheckedChanged += new System.EventHandler(this.rdbThresh_CheckedChanged);
            // 
            // rdbColour
            // 
            this.rdbColour.AutoSize = true;
            this.rdbColour.Location = new System.Drawing.Point(15, 24);
            this.rdbColour.Name = "rdbColour";
            this.rdbColour.Size = new System.Drawing.Size(55, 17);
            this.rdbColour.TabIndex = 0;
            this.rdbColour.TabStop = true;
            this.rdbColour.Text = "Colour";
            this.rdbColour.UseVisualStyleBackColor = true;
            this.rdbColour.CheckedChanged += new System.EventHandler(this.rdbColour_CheckedChanged);
            // 
            // nudVshiftTol_neg
            // 
            this.nudVshiftTol_neg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudVshiftTol_neg.DecimalPlaces = 2;
            this.nudVshiftTol_neg.Increment = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            this.nudVshiftTol_neg.Location = new System.Drawing.Point(147, 175);
            this.nudVshiftTol_neg.Name = "nudVshiftTol_neg";
            this.nudVshiftTol_neg.Size = new System.Drawing.Size(47, 20);
            this.nudVshiftTol_neg.TabIndex = 139;
            this.nudVshiftTol_neg.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudVshiftTol_neg.ValueChanged += new System.EventHandler(this.nudVshiftTol_neg_ValueChanged);
            // 
            // nudHshiftTol_neg
            // 
            this.nudHshiftTol_neg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudHshiftTol_neg.DecimalPlaces = 2;
            this.nudHshiftTol_neg.Increment = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            this.nudHshiftTol_neg.Location = new System.Drawing.Point(147, 149);
            this.nudHshiftTol_neg.Name = "nudHshiftTol_neg";
            this.nudHshiftTol_neg.Size = new System.Drawing.Size(47, 20);
            this.nudHshiftTol_neg.TabIndex = 138;
            this.nudHshiftTol_neg.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudHshiftTol_neg.ValueChanged += new System.EventHandler(this.nudHshiftTol_neg_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(155, 135);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(22, 13);
            this.label7.TabIndex = 139;
            this.label7.Text = "-ve";
            this.label7.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(211, 135);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(25, 13);
            this.label8.TabIndex = 140;
            this.label8.Text = "+ve";
            this.label8.Visible = false;
            // 
            // lblRefTool
            // 
            this.lblRefTool.AutoSize = true;
            this.lblRefTool.Location = new System.Drawing.Point(88, 426);
            this.lblRefTool.Name = "lblRefTool";
            this.lblRefTool.Size = new System.Drawing.Size(10, 13);
            this.lblRefTool.TabIndex = 146;
            this.lblRefTool.Text = "-";
            // 
            // lblFixtureMode
            // 
            this.lblFixtureMode.AutoSize = true;
            this.lblFixtureMode.Location = new System.Drawing.Point(104, 402);
            this.lblFixtureMode.Name = "lblFixtureMode";
            this.lblFixtureMode.Size = new System.Drawing.Size(10, 13);
            this.lblFixtureMode.TabIndex = 145;
            this.lblFixtureMode.Text = "-";
            // 
            // lblFixtureType
            // 
            this.lblFixtureType.AutoSize = true;
            this.lblFixtureType.Location = new System.Drawing.Point(75, 379);
            this.lblFixtureType.Name = "lblFixtureType";
            this.lblFixtureType.Size = new System.Drawing.Size(10, 13);
            this.lblFixtureType.TabIndex = 144;
            this.lblFixtureType.Text = "-";
            // 
            // lblD_DRefTool
            // 
            this.lblD_DRefTool.AutoSize = true;
            this.lblD_DRefTool.Location = new System.Drawing.Point(1, 426);
            this.lblD_DRefTool.Name = "lblD_DRefTool";
            this.lblD_DRefTool.Size = new System.Drawing.Size(84, 13);
            this.lblD_DRefTool.TabIndex = 143;
            this.lblD_DRefTool.Text = "Reference Tool:";
            // 
            // lblD_Fmode
            // 
            this.lblD_Fmode.AutoSize = true;
            this.lblD_Fmode.Location = new System.Drawing.Point(1, 402);
            this.lblD_Fmode.Name = "lblD_Fmode";
            this.lblD_Fmode.Size = new System.Drawing.Size(97, 13);
            this.lblD_Fmode.TabIndex = 142;
            this.lblD_Fmode.Text = "Fixture Mode(S/R):";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(1, 379);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(68, 13);
            this.label11.TabIndex = 141;
            this.label11.Text = "Fixture Type:";
            // 
            // uc_CROI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblRefTool);
            this.Controls.Add(this.lblFixtureMode);
            this.Controls.Add(this.lblFixtureType);
            this.Controls.Add(this.lblD_DRefTool);
            this.Controls.Add(this.lblD_Fmode);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.nudVshiftTol_neg);
            this.Controls.Add(this.nudHshiftTol_neg);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lbl_V_shiftTol);
            this.Controls.Add(this.lbl_H_shiftTol);
            this.Controls.Add(this.nudVshiftTol);
            this.Controls.Add(this.nudHshiftTol);
            this.Controls.Add(this.lblTemplateName);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblRegion);
            this.Controls.Add(this.nudMatchThresh);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nudRotationTolerance);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Name = "uc_CROI";
            this.Size = new System.Drawing.Size(273, 447);
            this.Load += new System.EventHandler(this.uc_CROI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudMatchThresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRotationTolerance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVshiftTol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHshiftTol)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVshiftTol_neg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHshiftTol_neg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSelectedColour;
        private System.Windows.Forms.ComboBox cmbcolours;
        private System.Windows.Forms.Label lblTemplateName;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblRegion;
        private System.Windows.Forms.NumericUpDown nudMatchThresh;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudRotationTolerance;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl_V_shiftTol;
        private System.Windows.Forms.Label lbl_H_shiftTol;
        private System.Windows.Forms.NumericUpDown nudVshiftTol;
        private System.Windows.Forms.NumericUpDown nudHshiftTol;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rdbThresh;
        private System.Windows.Forms.RadioButton rdbColour;
        private System.Windows.Forms.RadioButton rdbGray;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudVshiftTol_neg;
        private System.Windows.Forms.NumericUpDown nudHshiftTol_neg;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblRefTool;
        private System.Windows.Forms.Label lblFixtureMode;
        private System.Windows.Forms.Label lblFixtureType;
        private System.Windows.Forms.Label lblD_DRefTool;
        private System.Windows.Forms.Label lblD_Fmode;
        private System.Windows.Forms.Label label11;
    }
}
