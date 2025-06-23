namespace tryThresholds.IP_tools
{
    partial class uc_GrayPresence
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
            this.chkThtype = new System.Windows.Forms.CheckBox();
            this.trkThreshold = new System.Windows.Forms.TrackBar();
            this.lblthreshold = new System.Windows.Forms.Label();
            this.lblTemplateName = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblRegion = new System.Windows.Forms.Label();
            this.nudMatchPercent = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.rdbThresh = new System.Windows.Forms.RadioButton();
            this.rdbColour = new System.Windows.Forms.RadioButton();
            this.cmbcolours = new System.Windows.Forms.ComboBox();
            this.lblRefTool = new System.Windows.Forms.Label();
            this.lblFixtureMode = new System.Windows.Forms.Label();
            this.lblFixtureType = new System.Windows.Forms.Label();
            this.lblD_DRefTool = new System.Windows.Forms.Label();
            this.lblD_Fmode = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trkThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMatchPercent)).BeginInit();
            this.SuspendLayout();
            // 
            // chkThtype
            // 
            this.chkThtype.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkThtype.AutoSize = true;
            this.chkThtype.Location = new System.Drawing.Point(198, 180);
            this.chkThtype.Name = "chkThtype";
            this.chkThtype.Size = new System.Drawing.Size(53, 17);
            this.chkThtype.TabIndex = 156;
            this.chkThtype.Text = "Invert";
            this.chkThtype.UseVisualStyleBackColor = true;
            this.chkThtype.CheckedChanged += new System.EventHandler(this.chkThtype_CheckedChanged);
            // 
            // trkThreshold
            // 
            this.trkThreshold.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trkThreshold.Location = new System.Drawing.Point(10, 200);
            this.trkThreshold.Maximum = 255;
            this.trkThreshold.Name = "trkThreshold";
            this.trkThreshold.Size = new System.Drawing.Size(247, 45);
            this.trkThreshold.TabIndex = 155;
            this.trkThreshold.Value = 120;
            this.trkThreshold.Scroll += new System.EventHandler(this.trkThreshold_Scroll);
            // 
            // lblthreshold
            // 
            this.lblthreshold.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblthreshold.AutoSize = true;
            this.lblthreshold.Location = new System.Drawing.Point(21, 184);
            this.lblthreshold.Name = "lblthreshold";
            this.lblthreshold.Size = new System.Drawing.Size(27, 13);
            this.lblthreshold.TabIndex = 154;
            this.lblthreshold.Text = "N/A";
            // 
            // lblTemplateName
            // 
            this.lblTemplateName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTemplateName.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblTemplateName.Location = new System.Drawing.Point(2, 320);
            this.lblTemplateName.Name = "lblTemplateName";
            this.lblTemplateName.Size = new System.Drawing.Size(274, 17);
            this.lblTemplateName.TabIndex = 148;
            this.lblTemplateName.Text = "N/A";
            this.lblTemplateName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnUpdate.Location = new System.Drawing.Point(162, 382);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(107, 31);
            this.btnUpdate.TabIndex = 147;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(78, 42);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(191, 20);
            this.txtName.TabIndex = 146;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label5.Location = new System.Drawing.Point(-3, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(276, 30);
            this.label5.TabIndex = 145;
            this.label5.Text = "       Name :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.DarkSalmon;
            this.label3.Location = new System.Drawing.Point(-3, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(276, 31);
            this.label3.TabIndex = 144;
            this.label3.Text = "Gray Presence";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRegion
            // 
            this.lblRegion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRegion.Location = new System.Drawing.Point(21, 266);
            this.lblRegion.Name = "lblRegion";
            this.lblRegion.Size = new System.Drawing.Size(230, 44);
            this.lblRegion.TabIndex = 142;
            this.lblRegion.Text = "N/A";
            this.lblRegion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nudMatchPercent
            // 
            this.nudMatchPercent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudMatchPercent.Location = new System.Drawing.Point(153, 86);
            this.nudMatchPercent.Name = "nudMatchPercent";
            this.nudMatchPercent.Size = new System.Drawing.Size(98, 20);
            this.nudMatchPercent.TabIndex = 140;
            this.nudMatchPercent.ValueChanged += new System.EventHandler(this.nudMatchPercent_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(-3, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(276, 30);
            this.label1.TabIndex = 139;
            this.label1.Text = "       Matech Percent (%) :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label4.Location = new System.Drawing.Point(-3, 247);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(276, 92);
            this.label4.TabIndex = 143;
            this.label4.Text = "Region";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label6.Location = new System.Drawing.Point(0, 117);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(276, 21);
            this.label6.TabIndex = 159;
            this.label6.Text = "Mode";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rdbThresh
            // 
            this.rdbThresh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rdbThresh.AutoSize = true;
            this.rdbThresh.Location = new System.Drawing.Point(18, 137);
            this.rdbThresh.Name = "rdbThresh";
            this.rdbThresh.Size = new System.Drawing.Size(72, 17);
            this.rdbThresh.TabIndex = 158;
            this.rdbThresh.Text = "Threshold";
            this.rdbThresh.UseVisualStyleBackColor = true;
            this.rdbThresh.CheckedChanged += new System.EventHandler(this.rdbThresh_CheckedChanged);
            // 
            // rdbColour
            // 
            this.rdbColour.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rdbColour.AutoSize = true;
            this.rdbColour.Location = new System.Drawing.Point(118, 137);
            this.rdbColour.Name = "rdbColour";
            this.rdbColour.Size = new System.Drawing.Size(55, 17);
            this.rdbColour.TabIndex = 157;
            this.rdbColour.Text = "Colour";
            this.rdbColour.UseVisualStyleBackColor = true;
            this.rdbColour.CheckedChanged += new System.EventHandler(this.rdbColour_CheckedChanged);
            // 
            // cmbcolours
            // 
            this.cmbcolours.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbcolours.FormattingEnabled = true;
            this.cmbcolours.Location = new System.Drawing.Point(18, 160);
            this.cmbcolours.Name = "cmbcolours";
            this.cmbcolours.Size = new System.Drawing.Size(155, 21);
            this.cmbcolours.TabIndex = 160;
            this.cmbcolours.Visible = false;
            // 
            // lblRefTool
            // 
            this.lblRefTool.AutoSize = true;
            this.lblRefTool.Location = new System.Drawing.Point(90, 400);
            this.lblRefTool.Name = "lblRefTool";
            this.lblRefTool.Size = new System.Drawing.Size(10, 13);
            this.lblRefTool.TabIndex = 166;
            this.lblRefTool.Text = "-";
            // 
            // lblFixtureMode
            // 
            this.lblFixtureMode.AutoSize = true;
            this.lblFixtureMode.Location = new System.Drawing.Point(106, 376);
            this.lblFixtureMode.Name = "lblFixtureMode";
            this.lblFixtureMode.Size = new System.Drawing.Size(10, 13);
            this.lblFixtureMode.TabIndex = 165;
            this.lblFixtureMode.Text = "-";
            // 
            // lblFixtureType
            // 
            this.lblFixtureType.AutoSize = true;
            this.lblFixtureType.Location = new System.Drawing.Point(77, 353);
            this.lblFixtureType.Name = "lblFixtureType";
            this.lblFixtureType.Size = new System.Drawing.Size(10, 13);
            this.lblFixtureType.TabIndex = 164;
            this.lblFixtureType.Text = "-";
            // 
            // lblD_DRefTool
            // 
            this.lblD_DRefTool.AutoSize = true;
            this.lblD_DRefTool.Location = new System.Drawing.Point(3, 400);
            this.lblD_DRefTool.Name = "lblD_DRefTool";
            this.lblD_DRefTool.Size = new System.Drawing.Size(84, 13);
            this.lblD_DRefTool.TabIndex = 163;
            this.lblD_DRefTool.Text = "Reference Tool:";
            // 
            // lblD_Fmode
            // 
            this.lblD_Fmode.AutoSize = true;
            this.lblD_Fmode.Location = new System.Drawing.Point(3, 376);
            this.lblD_Fmode.Name = "lblD_Fmode";
            this.lblD_Fmode.Size = new System.Drawing.Size(97, 13);
            this.lblD_Fmode.TabIndex = 162;
            this.lblD_Fmode.Text = "Fixture Mode(S/R):";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 353);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(68, 13);
            this.label11.TabIndex = 161;
            this.label11.Text = "Fixture Type:";
            // 
            // uc_GrayPresence
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblRefTool);
            this.Controls.Add(this.lblFixtureMode);
            this.Controls.Add(this.lblFixtureType);
            this.Controls.Add(this.lblD_DRefTool);
            this.Controls.Add(this.lblD_Fmode);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cmbcolours);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.rdbThresh);
            this.Controls.Add(this.rdbColour);
            this.Controls.Add(this.chkThtype);
            this.Controls.Add(this.trkThreshold);
            this.Controls.Add(this.lblthreshold);
            this.Controls.Add(this.lblTemplateName);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblRegion);
            this.Controls.Add(this.nudMatchPercent);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Name = "uc_GrayPresence";
            this.Size = new System.Drawing.Size(273, 419);
            ((System.ComponentModel.ISupportInitialize)(this.trkThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMatchPercent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkThtype;
        private System.Windows.Forms.TrackBar trkThreshold;
        private System.Windows.Forms.Label lblthreshold;
        private System.Windows.Forms.Label lblTemplateName;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblRegion;
        private System.Windows.Forms.NumericUpDown nudMatchPercent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton rdbThresh;
        private System.Windows.Forms.RadioButton rdbColour;
        private System.Windows.Forms.ComboBox cmbcolours;
        private System.Windows.Forms.Label lblRefTool;
        private System.Windows.Forms.Label lblFixtureMode;
        private System.Windows.Forms.Label lblFixtureType;
        private System.Windows.Forms.Label lblD_DRefTool;
        private System.Windows.Forms.Label lblD_Fmode;
        private System.Windows.Forms.Label label11;
    }
}
