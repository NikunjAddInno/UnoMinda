namespace tryThresholds.IP_tools
{
    partial class uc_ROI
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
            this.label1 = new System.Windows.Forms.Label();
            this.nudRotationTolerance = new System.Windows.Forms.NumericUpDown();
            this.nudMatchThresh = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.lblRegion = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.lblTemplateName = new System.Windows.Forms.Label();
            this.cmbcolours = new System.Windows.Forms.ComboBox();
            this.lblSelectedColour = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudRotationTolerance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMatchThresh)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(-3, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(276, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "       RotationTolerance";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.nudRotationTolerance.Location = new System.Drawing.Point(153, 86);
            this.nudRotationTolerance.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudRotationTolerance.Name = "nudRotationTolerance";
            this.nudRotationTolerance.Size = new System.Drawing.Size(98, 20);
            this.nudRotationTolerance.TabIndex = 1;
            this.nudRotationTolerance.ValueChanged += new System.EventHandler(this.nudRotationTolerance_ValueChanged);
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
            this.nudMatchThresh.Location = new System.Drawing.Point(153, 125);
            this.nudMatchThresh.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudMatchThresh.Name = "nudMatchThresh";
            this.nudMatchThresh.Size = new System.Drawing.Size(98, 20);
            this.nudMatchThresh.TabIndex = 3;
            this.nudMatchThresh.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudMatchThresh.ValueChanged += new System.EventHandler(this.nudMatchThresh_ValueChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Location = new System.Drawing.Point(-3, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(276, 30);
            this.label2.TabIndex = 2;
            this.label2.Text = "       Match Percent";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRegion
            // 
            this.lblRegion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRegion.Location = new System.Drawing.Point(21, 188);
            this.lblRegion.Name = "lblRegion";
            this.lblRegion.Size = new System.Drawing.Size(230, 44);
            this.lblRegion.TabIndex = 5;
            this.lblRegion.Text = "N/A";
            this.lblRegion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label4.Location = new System.Drawing.Point(-3, 169);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(276, 92);
            this.label4.TabIndex = 6;
            this.label4.Text = "Region";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.DarkSalmon;
            this.label3.Location = new System.Drawing.Point(-3, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(276, 31);
            this.label3.TabIndex = 7;
            this.label3.Text = "ROI Selection";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(78, 42);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(191, 20);
            this.txtName.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label5.Location = new System.Drawing.Point(-3, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(276, 30);
            this.label5.TabIndex = 20;
            this.label5.Text = "       Name :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnUpdate.Location = new System.Drawing.Point(166, 272);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(107, 31);
            this.btnUpdate.TabIndex = 22;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // lblTemplateName
            // 
            this.lblTemplateName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTemplateName.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblTemplateName.Location = new System.Drawing.Point(2, 242);
            this.lblTemplateName.Name = "lblTemplateName";
            this.lblTemplateName.Size = new System.Drawing.Size(274, 17);
            this.lblTemplateName.TabIndex = 117;
            this.lblTemplateName.Text = "N/A";
            this.lblTemplateName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cmbcolours
            // 
            this.cmbcolours.FormattingEnabled = true;
            this.cmbcolours.Location = new System.Drawing.Point(5, 279);
            this.cmbcolours.Name = "cmbcolours";
            this.cmbcolours.Size = new System.Drawing.Size(155, 21);
            this.cmbcolours.TabIndex = 118;
            this.cmbcolours.Visible = false;
            this.cmbcolours.SelectedIndexChanged += new System.EventHandler(this.cmbcolours_SelectedIndexChanged);
            // 
            // lblSelectedColour
            // 
            this.lblSelectedColour.AutoSize = true;
            this.lblSelectedColour.Location = new System.Drawing.Point(3, 263);
            this.lblSelectedColour.Name = "lblSelectedColour";
            this.lblSelectedColour.Size = new System.Drawing.Size(27, 13);
            this.lblSelectedColour.TabIndex = 119;
            this.lblSelectedColour.Text = "N/A";
            this.lblSelectedColour.Visible = false;
            // 
            // uc_ROI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblSelectedColour);
            this.Controls.Add(this.cmbcolours);
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
            this.Name = "uc_ROI";
            this.Size = new System.Drawing.Size(273, 306);
            this.Load += new System.EventHandler(this.uc_ROI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudRotationTolerance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMatchThresh)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudRotationTolerance;
        private System.Windows.Forms.NumericUpDown nudMatchThresh;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblRegion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label lblTemplateName;
        private System.Windows.Forms.ComboBox cmbcolours;
        private System.Windows.Forms.Label lblSelectedColour;
    }
}
