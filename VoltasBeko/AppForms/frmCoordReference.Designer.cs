namespace tryThresholds
{
    partial class frmCoordReference
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
            this.listBoxAppliedTools = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdbFixture_n_othertool = new System.Windows.Forms.RadioButton();
            this.rdb_otherTool = new System.Windows.Forms.RadioButton();
            this.rdbFixtureTool = new System.Windows.Forms.RadioButton();
            this.rdbFixed = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rdbRotOnly = new System.Windows.Forms.RadioButton();
            this.rdbShiftOnly = new System.Windows.Forms.RadioButton();
            this.rdbShiftnRot = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlReftool = new System.Windows.Forms.Panel();
            this.lblCurrentRefTool = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlMode = new System.Windows.Forms.Panel();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlReftool.SuspendLayout();
            this.pnlMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxAppliedTools
            // 
            this.listBoxAppliedTools.FormattingEnabled = true;
            this.listBoxAppliedTools.Location = new System.Drawing.Point(0, 52);
            this.listBoxAppliedTools.Name = "listBoxAppliedTools";
            this.listBoxAppliedTools.Size = new System.Drawing.Size(270, 95);
            this.listBoxAppliedTools.TabIndex = 23;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdbFixture_n_othertool);
            this.panel1.Controls.Add(this.rdb_otherTool);
            this.panel1.Controls.Add(this.rdbFixtureTool);
            this.panel1.Controls.Add(this.rdbFixed);
            this.panel1.Location = new System.Drawing.Point(12, 63);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(273, 113);
            this.panel1.TabIndex = 138;
            // 
            // rdbFixture_n_othertool
            // 
            this.rdbFixture_n_othertool.AutoSize = true;
            this.rdbFixture_n_othertool.Location = new System.Drawing.Point(15, 83);
            this.rdbFixture_n_othertool.Name = "rdbFixture_n_othertool";
            this.rdbFixture_n_othertool.Size = new System.Drawing.Size(118, 17);
            this.rdbFixture_n_othertool.TabIndex = 134;
            this.rdbFixture_n_othertool.TabStop = true;
            this.rdbFixture_n_othertool.Tag = "3";
            this.rdbFixture_n_othertool.Text = "Fixture + Other Tool";
            this.rdbFixture_n_othertool.UseVisualStyleBackColor = true;
            // 
            // rdb_otherTool
            // 
            this.rdb_otherTool.AutoSize = true;
            this.rdb_otherTool.Location = new System.Drawing.Point(15, 60);
            this.rdb_otherTool.Name = "rdb_otherTool";
            this.rdb_otherTool.Size = new System.Drawing.Size(99, 17);
            this.rdb_otherTool.TabIndex = 133;
            this.rdb_otherTool.TabStop = true;
            this.rdb_otherTool.Tag = "2";
            this.rdb_otherTool.Text = "Other Tool Only";
            this.rdb_otherTool.UseVisualStyleBackColor = true;
            // 
            // rdbFixtureTool
            // 
            this.rdbFixtureTool.AutoSize = true;
            this.rdbFixtureTool.Location = new System.Drawing.Point(15, 37);
            this.rdbFixtureTool.Name = "rdbFixtureTool";
            this.rdbFixtureTool.Size = new System.Drawing.Size(80, 17);
            this.rdbFixtureTool.TabIndex = 1;
            this.rdbFixtureTool.TabStop = true;
            this.rdbFixtureTool.Tag = "1";
            this.rdbFixtureTool.Text = "Fixture Tool";
            this.rdbFixtureTool.UseVisualStyleBackColor = true;
            // 
            // rdbFixed
            // 
            this.rdbFixed.AutoSize = true;
            this.rdbFixed.Location = new System.Drawing.Point(15, 12);
            this.rdbFixed.Name = "rdbFixed";
            this.rdbFixed.Size = new System.Drawing.Size(94, 17);
            this.rdbFixed.TabIndex = 0;
            this.rdbFixed.TabStop = true;
            this.rdbFixed.Tag = "0";
            this.rdbFixed.Text = "Fixed Location";
            this.rdbFixed.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label6.Location = new System.Drawing.Point(12, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(273, 21);
            this.label6.TabIndex = 138;
            this.label6.Text = "Type";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(-2, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(276, 21);
            this.label1.TabIndex = 139;
            this.label1.Text = "Tracking Mode";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rdbRotOnly);
            this.panel2.Controls.Add(this.rdbShiftOnly);
            this.panel2.Controls.Add(this.rdbShiftnRot);
            this.panel2.Location = new System.Drawing.Point(0, 27);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(276, 63);
            this.panel2.TabIndex = 140;
            // 
            // rdbRotOnly
            // 
            this.rdbRotOnly.AutoSize = true;
            this.rdbRotOnly.Location = new System.Drawing.Point(156, 36);
            this.rdbRotOnly.Name = "rdbRotOnly";
            this.rdbRotOnly.Size = new System.Drawing.Size(89, 17);
            this.rdbRotOnly.TabIndex = 136;
            this.rdbRotOnly.TabStop = true;
            this.rdbRotOnly.Text = "Rotation Only";
            this.rdbRotOnly.UseVisualStyleBackColor = true;
            // 
            // rdbShiftOnly
            // 
            this.rdbShiftOnly.AutoSize = true;
            this.rdbShiftOnly.Location = new System.Drawing.Point(156, 13);
            this.rdbShiftOnly.Name = "rdbShiftOnly";
            this.rdbShiftOnly.Size = new System.Drawing.Size(70, 17);
            this.rdbShiftOnly.TabIndex = 135;
            this.rdbShiftOnly.TabStop = true;
            this.rdbShiftOnly.Text = "Shift Only";
            this.rdbShiftOnly.UseVisualStyleBackColor = true;
            this.rdbShiftOnly.CheckedChanged += new System.EventHandler(this.rdbShiftOnly_CheckedChanged);
            // 
            // rdbShiftnRot
            // 
            this.rdbShiftnRot.AutoSize = true;
            this.rdbShiftnRot.Location = new System.Drawing.Point(15, 13);
            this.rdbShiftnRot.Name = "rdbShiftnRot";
            this.rdbShiftnRot.Size = new System.Drawing.Size(98, 17);
            this.rdbShiftnRot.TabIndex = 134;
            this.rdbShiftnRot.TabStop = true;
            this.rdbShiftnRot.Text = "Shift + Rotation";
            this.rdbShiftnRot.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(276, 21);
            this.label2.TabIndex = 141;
            this.label2.Text = "Select Reference Tool";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(37, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(222, 30);
            this.label3.TabIndex = 142;
            this.label3.Text = "Select how current tool shifts in the image";
            // 
            // pnlReftool
            // 
            this.pnlReftool.Controls.Add(this.lblCurrentRefTool);
            this.pnlReftool.Controls.Add(this.label4);
            this.pnlReftool.Controls.Add(this.listBoxAppliedTools);
            this.pnlReftool.Controls.Add(this.label2);
            this.pnlReftool.Location = new System.Drawing.Point(12, 187);
            this.pnlReftool.Name = "pnlReftool";
            this.pnlReftool.Size = new System.Drawing.Size(273, 152);
            this.pnlReftool.TabIndex = 143;
            // 
            // lblCurrentRefTool
            // 
            this.lblCurrentRefTool.AutoSize = true;
            this.lblCurrentRefTool.Location = new System.Drawing.Point(79, 29);
            this.lblCurrentRefTool.Name = "lblCurrentRefTool";
            this.lblCurrentRefTool.Size = new System.Drawing.Size(10, 13);
            this.lblCurrentRefTool.TabIndex = 143;
            this.lblCurrentRefTool.Text = ":";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 142;
            this.label4.Text = "Current Value:";
            // 
            // pnlMode
            // 
            this.pnlMode.Controls.Add(this.label1);
            this.pnlMode.Controls.Add(this.panel2);
            this.pnlMode.Location = new System.Drawing.Point(12, 345);
            this.pnlMode.Name = "pnlMode";
            this.pnlMode.Size = new System.Drawing.Size(276, 99);
            this.pnlMode.TabIndex = 144;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(12, 477);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(88, 23);
            this.btnUpdate.TabIndex = 137;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(200, 477);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(88, 23);
            this.btnClose.TabIndex = 145;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmCoordReference
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 508);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.pnlMode);
            this.Controls.Add(this.pnlReftool);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel1);
            this.Name = "frmCoordReference";
            this.Text = "Tool Tracking";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlReftool.ResumeLayout(false);
            this.pnlReftool.PerformLayout();
            this.pnlMode.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxAppliedTools;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rdb_otherTool;
        private System.Windows.Forms.RadioButton rdbFixtureTool;
        private System.Windows.Forms.RadioButton rdbFixed;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rdbFixture_n_othertool;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlReftool;
        private System.Windows.Forms.Panel pnlMode;
        private System.Windows.Forms.RadioButton rdbRotOnly;
        private System.Windows.Forms.RadioButton rdbShiftOnly;
        private System.Windows.Forms.RadioButton rdbShiftnRot;
        private System.Windows.Forms.Label lblCurrentRefTool;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnClose;
    }
}