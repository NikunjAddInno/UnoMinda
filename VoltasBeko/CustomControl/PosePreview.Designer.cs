namespace VoltasBeko.CustomControl
{
    partial class PosePreview
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
            this.labelPoseNumber = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listBoxDefects = new System.Windows.Forms.ListBox();
            this.labelResult = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // labelPoseNumber
            // 
            this.labelPoseNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(134)))), ((int)(((byte)(212)))));
            this.labelPoseNumber.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelPoseNumber.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPoseNumber.ForeColor = System.Drawing.Color.White;
            this.labelPoseNumber.Location = new System.Drawing.Point(0, 0);
            this.labelPoseNumber.Name = "labelPoseNumber";
            this.labelPoseNumber.Size = new System.Drawing.Size(310, 23);
            this.labelPoseNumber.TabIndex = 53;
            this.labelPoseNumber.Text = "Pose Number";
            this.labelPoseNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listBoxDefects);
            this.panel1.Controls.Add(this.labelResult);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(172, 23);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(138, 221);
            this.panel1.TabIndex = 65;
            // 
            // listBoxDefects
            // 
            this.listBoxDefects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxDefects.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxDefects.FormattingEnabled = true;
            this.listBoxDefects.ItemHeight = 24;
            this.listBoxDefects.Location = new System.Drawing.Point(0, 23);
            this.listBoxDefects.Name = "listBoxDefects";
            this.listBoxDefects.Size = new System.Drawing.Size(138, 198);
            this.listBoxDefects.TabIndex = 64;
            this.listBoxDefects.Click += new System.EventHandler(this.listBoxDefects_Click);
            this.listBoxDefects.SelectedIndexChanged += new System.EventHandler(this.listBoxDefects_SelectedIndexChanged);
            // 
            // labelResult
            // 
            this.labelResult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.labelResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelResult.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelResult.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelResult.ForeColor = System.Drawing.Color.Black;
            this.labelResult.Location = new System.Drawing.Point(0, 0);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(138, 23);
            this.labelResult.TabIndex = 63;
            this.labelResult.Text = "Result";
            this.labelResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox
            // 
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(0, 23);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(172, 221);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 66;
            this.pictureBox.TabStop = false;
            this.pictureBox.Click += new System.EventHandler(this.pictureBox_Click);
            // 
            // PosePreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelPoseNumber);
            this.Name = "PosePreview";
            this.Size = new System.Drawing.Size(310, 244);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.Label labelPoseNumber;
        public System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox listBoxDefects;
        public System.Windows.Forms.Label labelResult;
        public System.Windows.Forms.PictureBox pictureBox;
    }
}
