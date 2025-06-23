namespace VoltasBeko.CustomControl
{
    partial class PoseControl
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
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.labelPosition = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxZ = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxY = new System.Windows.Forms.TextBox();
            this.labelY = new System.Windows.Forms.Label();
            this.textBoxX = new System.Windows.Forms.TextBox();
            this.labelX = new System.Windows.Forms.Label();
            this.labelPoseNumber = new System.Windows.Forms.Label();
            this.buttonConfig = new System.Windows.Forms.Button();
            this.labelExposure = new System.Windows.Forms.Label();
            this.numericUpDownExposure = new System.Windows.Forms.NumericUpDown();
            this.buttonMove = new System.Windows.Forms.Button();
            this.checkBoxRegisterNew = new System.Windows.Forms.CheckBox();
            this.checkBoxUpdateImage = new System.Windows.Forms.CheckBox();
            this.buttonIcon = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownExposure)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonDelete
            // 
            this.buttonDelete.BackColor = System.Drawing.Color.LightCoral;
            this.buttonDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDelete.Location = new System.Drawing.Point(286, 122);
            this.buttonDelete.Margin = new System.Windows.Forms.Padding(4);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(106, 25);
            this.buttonDelete.TabIndex = 38;
            this.buttonDelete.Text = "Delete Pose";
            this.buttonDelete.UseVisualStyleBackColor = false;
            this.buttonDelete.Visible = false;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.buttonEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEdit.Location = new System.Drawing.Point(175, 122);
            this.buttonEdit.Margin = new System.Windows.Forms.Padding(4);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(104, 25);
            this.buttonEdit.TabIndex = 36;
            this.buttonEdit.Text = "Edit Pose";
            this.buttonEdit.UseVisualStyleBackColor = false;
            this.buttonEdit.Visible = false;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // labelPosition
            // 
            this.labelPosition.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(134)))), ((int)(((byte)(212)))));
            this.labelPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPosition.ForeColor = System.Drawing.Color.White;
            this.labelPosition.Location = new System.Drawing.Point(10, 32);
            this.labelPosition.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPosition.Name = "labelPosition";
            this.labelPosition.Size = new System.Drawing.Size(74, 30);
            this.labelPosition.TabIndex = 25;
            this.labelPosition.Text = "Position";
            this.labelPosition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBoxZ);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBoxY);
            this.panel1.Controls.Add(this.labelY);
            this.panel1.Controls.Add(this.textBoxX);
            this.panel1.Controls.Add(this.labelX);
            this.panel1.Location = new System.Drawing.Point(104, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(288, 22);
            this.panel1.TabIndex = 39;
            // 
            // textBoxZ
            // 
            this.textBoxZ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxZ.Location = new System.Drawing.Point(225, 0);
            this.textBoxZ.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxZ.Name = "textBoxZ";
            this.textBoxZ.Size = new System.Drawing.Size(63, 22);
            this.textBoxZ.TabIndex = 45;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(195, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 22);
            this.label1.TabIndex = 44;
            this.label1.Text = "Z";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxY
            // 
            this.textBoxY.Dock = System.Windows.Forms.DockStyle.Left;
            this.textBoxY.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxY.Location = new System.Drawing.Point(132, 0);
            this.textBoxY.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxY.Name = "textBoxY";
            this.textBoxY.Size = new System.Drawing.Size(63, 22);
            this.textBoxY.TabIndex = 43;
            // 
            // labelY
            // 
            this.labelY.BackColor = System.Drawing.Color.Black;
            this.labelY.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelY.ForeColor = System.Drawing.Color.White;
            this.labelY.Location = new System.Drawing.Point(94, 0);
            this.labelY.Name = "labelY";
            this.labelY.Size = new System.Drawing.Size(38, 22);
            this.labelY.TabIndex = 42;
            this.labelY.Text = "Y";
            this.labelY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxX
            // 
            this.textBoxX.Dock = System.Windows.Forms.DockStyle.Left;
            this.textBoxX.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxX.Location = new System.Drawing.Point(30, 0);
            this.textBoxX.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxX.Name = "textBoxX";
            this.textBoxX.Size = new System.Drawing.Size(64, 22);
            this.textBoxX.TabIndex = 30;
            // 
            // labelX
            // 
            this.labelX.BackColor = System.Drawing.Color.Black;
            this.labelX.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelX.ForeColor = System.Drawing.Color.White;
            this.labelX.Location = new System.Drawing.Point(0, 0);
            this.labelX.Name = "labelX";
            this.labelX.Size = new System.Drawing.Size(30, 22);
            this.labelX.TabIndex = 0;
            this.labelX.Text = "X";
            this.labelX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelPoseNumber
            // 
            this.labelPoseNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(134)))), ((int)(((byte)(212)))));
            this.labelPoseNumber.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelPoseNumber.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPoseNumber.ForeColor = System.Drawing.Color.White;
            this.labelPoseNumber.Location = new System.Drawing.Point(0, 0);
            this.labelPoseNumber.Name = "labelPoseNumber";
            this.labelPoseNumber.Size = new System.Drawing.Size(424, 23);
            this.labelPoseNumber.TabIndex = 41;
            this.labelPoseNumber.Text = "Pose Number";
            this.labelPoseNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonConfig
            // 
            this.buttonConfig.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.buttonConfig.Location = new System.Drawing.Point(97, 122);
            this.buttonConfig.Name = "buttonConfig";
            this.buttonConfig.Size = new System.Drawing.Size(71, 25);
            this.buttonConfig.TabIndex = 42;
            this.buttonConfig.Text = "Configure";
            this.buttonConfig.UseVisualStyleBackColor = false;
            this.buttonConfig.Click += new System.EventHandler(this.buttonConfig_Click);
            // 
            // labelExposure
            // 
            this.labelExposure.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(134)))), ((int)(((byte)(212)))));
            this.labelExposure.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelExposure.ForeColor = System.Drawing.Color.White;
            this.labelExposure.Location = new System.Drawing.Point(10, 76);
            this.labelExposure.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelExposure.Name = "labelExposure";
            this.labelExposure.Size = new System.Drawing.Size(74, 30);
            this.labelExposure.TabIndex = 25;
            this.labelExposure.Text = "Exposure";
            this.labelExposure.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDownExposure
            // 
            this.numericUpDownExposure.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericUpDownExposure.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownExposure.Location = new System.Drawing.Point(107, 79);
            this.numericUpDownExposure.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numericUpDownExposure.Name = "numericUpDownExposure";
            this.numericUpDownExposure.Size = new System.Drawing.Size(130, 24);
            this.numericUpDownExposure.TabIndex = 40;
            this.numericUpDownExposure.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownExposure.ThousandsSeparator = true;
            this.numericUpDownExposure.Value = new decimal(new int[] {
            56,
            0,
            0,
            0});
            // 
            // buttonMove
            // 
            this.buttonMove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.buttonMove.Location = new System.Drawing.Point(245, 79);
            this.buttonMove.Name = "buttonMove";
            this.buttonMove.Size = new System.Drawing.Size(97, 25);
            this.buttonMove.TabIndex = 43;
            this.buttonMove.Text = "Move Pose";
            this.buttonMove.UseVisualStyleBackColor = false;
            this.buttonMove.Click += new System.EventHandler(this.buttonMove_Click);
            // 
            // checkBoxRegisterNew
            // 
            this.checkBoxRegisterNew.AutoSize = true;
            this.checkBoxRegisterNew.Location = new System.Drawing.Point(9, 120);
            this.checkBoxRegisterNew.Name = "checkBoxRegisterNew";
            this.checkBoxRegisterNew.Size = new System.Drawing.Size(90, 17);
            this.checkBoxRegisterNew.TabIndex = 44;
            this.checkBoxRegisterNew.Text = "Register New";
            this.checkBoxRegisterNew.UseVisualStyleBackColor = true;
            this.checkBoxRegisterNew.CheckedChanged += new System.EventHandler(this.checkBoxRegisterNew_CheckedChanged);
            // 
            // checkBoxUpdateImage
            // 
            this.checkBoxUpdateImage.AutoSize = true;
            this.checkBoxUpdateImage.Location = new System.Drawing.Point(9, 143);
            this.checkBoxUpdateImage.Name = "checkBoxUpdateImage";
            this.checkBoxUpdateImage.Size = new System.Drawing.Size(93, 17);
            this.checkBoxUpdateImage.TabIndex = 44;
            this.checkBoxUpdateImage.Text = "Update Image";
            this.checkBoxUpdateImage.UseVisualStyleBackColor = true;
            this.checkBoxUpdateImage.CheckedChanged += new System.EventHandler(this.checkBoxRegisterNew_CheckedChanged);
            // 
            // buttonIcon
            // 
            this.buttonIcon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonIcon.ForeColor = System.Drawing.Color.White;
            this.buttonIcon.Location = new System.Drawing.Point(354, 68);
            this.buttonIcon.Name = "buttonIcon";
            this.buttonIcon.Size = new System.Drawing.Size(45, 45);
            this.buttonIcon.TabIndex = 45;
            this.buttonIcon.UseVisualStyleBackColor = true;
            this.buttonIcon.Visible = false;
            // 
            // PoseControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.buttonIcon);
            this.Controls.Add(this.checkBoxUpdateImage);
            this.Controls.Add(this.checkBoxRegisterNew);
            this.Controls.Add(this.buttonMove);
            this.Controls.Add(this.buttonConfig);
            this.Controls.Add(this.labelPoseNumber);
            this.Controls.Add(this.numericUpDownExposure);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.labelPosition);
            this.Controls.Add(this.labelExposure);
            this.Name = "PoseControl";
            this.Size = new System.Drawing.Size(424, 165);
            this.Load += new System.EventHandler(this.PoseControl_Load);
            this.Click += new System.EventHandler(this.PoseControl_Click);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.PoseControl_Paint);
            this.Leave += new System.EventHandler(this.PoseControl_Leave);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownExposure)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Label labelPosition;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBoxY;
        private System.Windows.Forms.Label labelY;
        private System.Windows.Forms.TextBox textBoxX;
        private System.Windows.Forms.Label labelX;
        public System.Windows.Forms.Label labelPoseNumber;
        private System.Windows.Forms.Button buttonConfig;
        private System.Windows.Forms.Label labelExposure;
        private System.Windows.Forms.NumericUpDown numericUpDownExposure;
        private System.Windows.Forms.Button buttonMove;
        private System.Windows.Forms.CheckBox checkBoxRegisterNew;
        private System.Windows.Forms.TextBox textBoxZ;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxUpdateImage;
        private System.Windows.Forms.Button buttonIcon;
    }
}
