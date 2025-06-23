namespace tryThresholds.IP_tools
{
    partial class uc_DateCode_Dots_OCR
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
            this.txtName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.nudDotDiaMax = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.lblWidthTol = new System.Windows.Forms.Label();
            this.nudDotDiaMin = new System.Windows.Forms.NumericUpDown();
            this.lblHeightTol = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnTestTool = new System.Windows.Forms.Button();
            this.lblReadResult = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblRegion = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDotDiaMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDotDiaMin)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(78, 43);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(191, 20);
            this.txtName.TabIndex = 28;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label5.Location = new System.Drawing.Point(-3, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(276, 30);
            this.label5.TabIndex = 27;
            this.label5.Text = "       Name :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.DarkSalmon;
            this.label3.Location = new System.Drawing.Point(-3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(276, 31);
            this.label3.TabIndex = 26;
            this.label3.Text = "Read Month Year (Format : ......AB......)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.LightCyan;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.nudDotDiaMax);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lblWidthTol);
            this.panel1.Controls.Add(this.nudDotDiaMin);
            this.panel1.Controls.Add(this.lblHeightTol);
            this.panel1.Location = new System.Drawing.Point(0, 84);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(274, 80);
            this.panel1.TabIndex = 113;
            // 
            // nudDotDiaMax
            // 
            this.nudDotDiaMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudDotDiaMax.DecimalPlaces = 2;
            this.nudDotDiaMax.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudDotDiaMax.Location = new System.Drawing.Point(160, 49);
            this.nudDotDiaMax.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudDotDiaMax.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudDotDiaMax.Name = "nudDotDiaMax";
            this.nudDotDiaMax.Size = new System.Drawing.Size(60, 20);
            this.nudDotDiaMax.TabIndex = 112;
            this.nudDotDiaMax.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label4.Location = new System.Drawing.Point(-2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(274, 17);
            this.label4.TabIndex = 111;
            this.label4.Text = "Dot Size Range (mm)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblWidthTol
            // 
            this.lblWidthTol.AutoSize = true;
            this.lblWidthTol.Location = new System.Drawing.Point(42, 25);
            this.lblWidthTol.Name = "lblWidthTol";
            this.lblWidthTol.Size = new System.Drawing.Size(73, 13);
            this.lblWidthTol.TabIndex = 105;
            this.lblWidthTol.Text = "Minimum Dia :";
            // 
            // nudDotDiaMin
            // 
            this.nudDotDiaMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudDotDiaMin.DecimalPlaces = 2;
            this.nudDotDiaMin.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudDotDiaMin.Location = new System.Drawing.Point(160, 23);
            this.nudDotDiaMin.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudDotDiaMin.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudDotDiaMin.Name = "nudDotDiaMin";
            this.nudDotDiaMin.Size = new System.Drawing.Size(60, 20);
            this.nudDotDiaMin.TabIndex = 101;
            this.nudDotDiaMin.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblHeightTol
            // 
            this.lblHeightTol.AutoSize = true;
            this.lblHeightTol.Location = new System.Drawing.Point(42, 51);
            this.lblHeightTol.Name = "lblHeightTol";
            this.lblHeightTol.Size = new System.Drawing.Size(76, 13);
            this.lblHeightTol.TabIndex = 106;
            this.lblHeightTol.Text = "Maximum Dia :";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.LightCyan;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnTestTool);
            this.panel2.Controls.Add(this.lblReadResult);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(0, 270);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(274, 52);
            this.panel2.TabIndex = 114;
            // 
            // btnTestTool
            // 
            this.btnTestTool.Location = new System.Drawing.Point(225, 21);
            this.btnTestTool.Name = "btnTestTool";
            this.btnTestTool.Size = new System.Drawing.Size(45, 23);
            this.btnTestTool.TabIndex = 119;
            this.btnTestTool.Text = "Test";
            this.btnTestTool.UseVisualStyleBackColor = true;
            this.btnTestTool.Click += new System.EventHandler(this.btnTestTool_Click);
            // 
            // lblReadResult
            // 
            this.lblReadResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblReadResult.BackColor = System.Drawing.Color.LightCyan;
            this.lblReadResult.Location = new System.Drawing.Point(12, 21);
            this.lblReadResult.Name = "lblReadResult";
            this.lblReadResult.Size = new System.Drawing.Size(250, 24);
            this.lblReadResult.TabIndex = 112;
            this.lblReadResult.Text = "N/A";
            this.lblReadResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(-2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(274, 17);
            this.label1.TabIndex = 111;
            this.label1.Text = "Result";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblRegion
            // 
            this.lblRegion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRegion.Location = new System.Drawing.Point(22, 198);
            this.lblRegion.Name = "lblRegion";
            this.lblRegion.Size = new System.Drawing.Size(230, 54);
            this.lblRegion.TabIndex = 116;
            this.lblRegion.Text = "N/A";
            this.lblRegion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label9.Location = new System.Drawing.Point(-2, 175);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(276, 92);
            this.label9.TabIndex = 117;
            this.label9.Text = "Region";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnUpdate.Location = new System.Drawing.Point(165, 328);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(107, 31);
            this.btnUpdate.TabIndex = 118;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // uc_DateCode_Dots_OCR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.lblRegion);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Name = "uc_DateCode_Dots_OCR";
            this.Size = new System.Drawing.Size(273, 400);
            this.Load += new System.EventHandler(this.uc_DateCode_Dots_OCR_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDotDiaMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDotDiaMin)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblWidthTol;
        private System.Windows.Forms.NumericUpDown nudDotDiaMin;
        private System.Windows.Forms.Label lblHeightTol;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblReadResult;
        private System.Windows.Forms.Label lblRegion;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.NumericUpDown nudDotDiaMax;
        private System.Windows.Forms.Button btnTestTool;
    }
}
