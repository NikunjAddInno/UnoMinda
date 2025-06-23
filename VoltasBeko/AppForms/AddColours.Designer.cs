namespace tryThresholds
{
    partial class AddColours
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
            this.listBoxColours = new System.Windows.Forms.ListBox();
            this.pnl_zoom = new System.Windows.Forms.Panel();
            this.pb_Zoom = new System.Windows.Forms.PictureBox();
            this.pnlCrud = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.btnSelectC = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.txtColourName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nudColourSpread = new System.Windows.Forms.NumericUpDown();
            this.btnUndoPoint = new System.Windows.Forms.Button();
            this.lblPointsClicked = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.lblSelectedColour = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pnl_preview = new System.Windows.Forms.Panel();
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnTestSelected = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pnl_zoom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Zoom)).BeginInit();
            this.pnlCrud.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudColourSpread)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.pnl_preview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxColours
            // 
            this.listBoxColours.FormattingEnabled = true;
            this.listBoxColours.Location = new System.Drawing.Point(3, 20);
            this.listBoxColours.Name = "listBoxColours";
            this.listBoxColours.Size = new System.Drawing.Size(209, 108);
            this.listBoxColours.TabIndex = 23;
            this.listBoxColours.SelectedIndexChanged += new System.EventHandler(this.listBoxColours_SelectedIndexChanged);
            // 
            // pnl_zoom
            // 
            this.pnl_zoom.Controls.Add(this.pb_Zoom);
            this.pnl_zoom.Location = new System.Drawing.Point(271, 10);
            this.pnl_zoom.Name = "pnl_zoom";
            this.pnl_zoom.Size = new System.Drawing.Size(800, 600);
            this.pnl_zoom.TabIndex = 24;
            // 
            // pb_Zoom
            // 
            this.pb_Zoom.Location = new System.Drawing.Point(3, 3);
            this.pb_Zoom.Name = "pb_Zoom";
            this.pb_Zoom.Size = new System.Drawing.Size(794, 573);
            this.pb_Zoom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_Zoom.TabIndex = 0;
            this.pb_Zoom.TabStop = false;
            this.pb_Zoom.Click += new System.EventHandler(this.pb_Zoom_Click);
            // 
            // pnlCrud
            // 
            this.pnlCrud.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCrud.Controls.Add(this.btnDelete);
            this.pnlCrud.Controls.Add(this.btnUpdate);
            this.pnlCrud.Controls.Add(this.btnAddNew);
            this.pnlCrud.Location = new System.Drawing.Point(0, 167);
            this.pnlCrud.Name = "pnlCrud";
            this.pnlCrud.Size = new System.Drawing.Size(218, 53);
            this.pnlCrud.TabIndex = 25;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(146, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(72, 45);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete Selected";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(73, 3);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(72, 45);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "Update Selected";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnAddNew
            // 
            this.btnAddNew.Location = new System.Drawing.Point(1, 3);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(72, 45);
            this.btnAddNew.TabIndex = 0;
            this.btnAddNew.Text = "Add New";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // btnSelectC
            // 
            this.btnSelectC.Location = new System.Drawing.Point(25, 10);
            this.btnSelectC.Name = "btnSelectC";
            this.btnSelectC.Size = new System.Drawing.Size(224, 46);
            this.btnSelectC.TabIndex = 1;
            this.btnSelectC.Text = "Load Image / Reset";
            this.btnSelectC.UseVisualStyleBackColor = true;
            this.btnSelectC.Click += new System.EventHandler(this.btnSelectC_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtColourName);
            this.panel1.Controls.Add(this.pnlCrud);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.nudColourSpread);
            this.panel1.Controls.Add(this.btnUndoPoint);
            this.panel1.Controls.Add(this.lblPointsClicked);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(24, 277);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(221, 224);
            this.panel1.TabIndex = 27;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 37;
            this.label3.Text = "Colour Name:";
            // 
            // txtColourName
            // 
            this.txtColourName.Location = new System.Drawing.Point(91, 133);
            this.txtColourName.Name = "txtColourName";
            this.txtColourName.Size = new System.Drawing.Size(100, 20);
            this.txtColourName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 36;
            this.label2.Text = "Colour Spread";
            // 
            // nudColourSpread
            // 
            this.nudColourSpread.Location = new System.Drawing.Point(110, 49);
            this.nudColourSpread.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudColourSpread.Name = "nudColourSpread";
            this.nudColourSpread.Size = new System.Drawing.Size(81, 20);
            this.nudColourSpread.TabIndex = 35;
            this.nudColourSpread.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // btnUndoPoint
            // 
            this.btnUndoPoint.Location = new System.Drawing.Point(110, 75);
            this.btnUndoPoint.Name = "btnUndoPoint";
            this.btnUndoPoint.Size = new System.Drawing.Size(81, 46);
            this.btnUndoPoint.TabIndex = 34;
            this.btnUndoPoint.Text = "Undo Point";
            this.btnUndoPoint.UseVisualStyleBackColor = true;
            this.btnUndoPoint.Click += new System.EventHandler(this.btnUndoPoint_Click);
            // 
            // lblPointsClicked
            // 
            this.lblPointsClicked.AutoSize = true;
            this.lblPointsClicked.Location = new System.Drawing.Point(131, 28);
            this.lblPointsClicked.Name = "lblPointsClicked";
            this.lblPointsClicked.Size = new System.Drawing.Size(13, 13);
            this.lblPointsClicked.TabIndex = 33;
            this.lblPointsClicked.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "Points Selected:";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.MediumOrchid;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(219, 20);
            this.label4.TabIndex = 31;
            this.label4.Text = "Color Setup";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.checkBox1);
            this.panel2.Controls.Add(this.trackBar1);
            this.panel2.Controls.Add(this.lblSelectedColour);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Location = new System.Drawing.Point(23, 508);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(220, 100);
            this.panel2.TabIndex = 28;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(140, 29);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(53, 17);
            this.checkBox1.TabIndex = 138;
            this.checkBox1.Text = "Invert";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(6, 52);
            this.trackBar1.Maximum = 255;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(200, 45);
            this.trackBar1.TabIndex = 137;
            this.trackBar1.Value = 120;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // lblSelectedColour
            // 
            this.lblSelectedColour.AutoSize = true;
            this.lblSelectedColour.Location = new System.Drawing.Point(39, 29);
            this.lblSelectedColour.Name = "lblSelectedColour";
            this.lblSelectedColour.Size = new System.Drawing.Size(27, 13);
            this.lblSelectedColour.TabIndex = 136;
            this.lblSelectedColour.Text = "N/A";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.MediumOrchid;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(218, 20);
            this.label5.TabIndex = 32;
            this.label5.Text = "Threshold Test";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnl_preview
            // 
            this.pnl_preview.Controls.Add(this.picPreview);
            this.pnl_preview.Location = new System.Drawing.Point(1088, 10);
            this.pnl_preview.Name = "pnl_preview";
            this.pnl_preview.Size = new System.Drawing.Size(800, 600);
            this.pnl_preview.TabIndex = 29;
            // 
            // picPreview
            // 
            this.picPreview.Location = new System.Drawing.Point(3, 3);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(794, 573);
            this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPreview.TabIndex = 0;
            this.picPreview.TabStop = false;
            this.picPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.picPreview_Paint);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(26, 226);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(223, 45);
            this.button1.TabIndex = 3;
            this.button1.Text = "Save Data";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btnTestSelected
            // 
            this.btnTestSelected.Location = new System.Drawing.Point(92, 130);
            this.btnTestSelected.Name = "btnTestSelected";
            this.btnTestSelected.Size = new System.Drawing.Size(120, 28);
            this.btnTestSelected.TabIndex = 38;
            this.btnTestSelected.Text = "Test Selected";
            this.btnTestSelected.UseVisualStyleBackColor = true;
            this.btnTestSelected.Click += new System.EventHandler(this.btnTestSelected_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(2, 2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 39;
            this.label6.Text = "Colour List :";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.listBoxColours);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.btnTestSelected);
            this.panel3.Location = new System.Drawing.Point(30, 62);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(219, 161);
            this.panel3.TabIndex = 40;
            // 
            // AddColours
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1914, 614);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pnl_preview);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnl_zoom);
            this.Controls.Add(this.btnSelectC);
            this.Name = "AddColours";
            this.Text = "AddColours";
            this.Load += new System.EventHandler(this.AddColours_Load);
            this.pnl_zoom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_Zoom)).EndInit();
            this.pnlCrud.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudColourSpread)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.pnl_preview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxColours;
        private System.Windows.Forms.Panel pnl_zoom;
        private System.Windows.Forms.PictureBox pb_Zoom;
        private System.Windows.Forms.Panel pnlCrud;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.Button btnSelectC;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblPointsClicked;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnUndoPoint;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudColourSpread;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtColourName;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label lblSelectedColour;
        private System.Windows.Forms.Panel pnl_preview;
        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnTestSelected;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel3;
    }
}