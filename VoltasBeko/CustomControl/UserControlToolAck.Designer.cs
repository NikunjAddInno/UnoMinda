namespace VoltasBeko.CustomControl
{
    partial class UserControlToolAck
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonMarkOk = new System.Windows.Forms.Button();
            this.labelDefect = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.3833F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.6167F));
            this.tableLayoutPanel1.Controls.Add(this.buttonMarkOk, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelDefect, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(467, 45);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // buttonMarkOk
            // 
            this.buttonMarkOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.buttonMarkOk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonMarkOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMarkOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMarkOk.Location = new System.Drawing.Point(299, 3);
            this.buttonMarkOk.Name = "buttonMarkOk";
            this.buttonMarkOk.Size = new System.Drawing.Size(165, 39);
            this.buttonMarkOk.TabIndex = 68;
            this.buttonMarkOk.Text = "Mark OK";
            this.buttonMarkOk.UseVisualStyleBackColor = false;
            this.buttonMarkOk.Click += new System.EventHandler(this.buttonMarkOk_Click);
            // 
            // labelDefect
            // 
            this.labelDefect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.labelDefect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelDefect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelDefect.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDefect.ForeColor = System.Drawing.Color.Black;
            this.labelDefect.Location = new System.Drawing.Point(3, 0);
            this.labelDefect.Name = "labelDefect";
            this.labelDefect.Size = new System.Drawing.Size(290, 45);
            this.labelDefect.TabIndex = 65;
            this.labelDefect.Text = "Tool";
            this.labelDefect.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UserControlToolAck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UserControlToolAck";
            this.Size = new System.Drawing.Size(467, 45);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.Label labelDefect;
        public System.Windows.Forms.Button buttonMarkOk;
    }
}
