
namespace VoltasBeko
{
    partial class Login_page
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login_page));
            this.panelFormbg = new System.Windows.Forms.Panel();
            this.textBoxPass = new System.Windows.Forms.TextBox();
            this.textBoxUser = new System.Windows.Forms.TextBox();
            this.buttonCreateUser = new System.Windows.Forms.Button();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.labelSignin = new System.Windows.Forms.Label();
            this.loginGif = new System.Windows.Forms.PictureBox();
            this.passIcon = new System.Windows.Forms.PictureBox();
            this.userIcon = new System.Windows.Forms.PictureBox();
            this.bgImage = new System.Windows.Forms.PictureBox();
            this.panelFormbg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loginGif)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.passIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bgImage)).BeginInit();
            this.SuspendLayout();
            // 
            // panelFormbg
            // 
            this.panelFormbg.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panelFormbg.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panelFormbg.Controls.Add(this.loginGif);
            this.panelFormbg.Controls.Add(this.textBoxPass);
            this.panelFormbg.Controls.Add(this.textBoxUser);
            this.panelFormbg.Controls.Add(this.buttonCreateUser);
            this.panelFormbg.Controls.Add(this.buttonLogin);
            this.panelFormbg.Controls.Add(this.passIcon);
            this.panelFormbg.Controls.Add(this.userIcon);
            this.panelFormbg.Controls.Add(this.labelSignin);
            this.panelFormbg.Location = new System.Drawing.Point(113, 48);
            this.panelFormbg.Name = "panelFormbg";
            this.panelFormbg.Size = new System.Drawing.Size(289, 387);
            this.panelFormbg.TabIndex = 1;
            // 
            // textBoxPass
            // 
            this.textBoxPass.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBoxPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPass.Location = new System.Drawing.Point(77, 146);
            this.textBoxPass.Name = "textBoxPass";
            this.textBoxPass.PasswordChar = '*';
            this.textBoxPass.Size = new System.Drawing.Size(159, 22);
            this.textBoxPass.TabIndex = 9;
            // 
            // textBoxUser
            // 
            this.textBoxUser.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBoxUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxUser.Location = new System.Drawing.Point(77, 102);
            this.textBoxUser.Name = "textBoxUser";
            this.textBoxUser.Size = new System.Drawing.Size(159, 22);
            this.textBoxUser.TabIndex = 8;
            // 
            // buttonCreateUser
            // 
            this.buttonCreateUser.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonCreateUser.Font = new System.Drawing.Font("Myanmar Text", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCreateUser.Location = new System.Drawing.Point(76, 231);
            this.buttonCreateUser.Name = "buttonCreateUser";
            this.buttonCreateUser.Size = new System.Drawing.Size(160, 32);
            this.buttonCreateUser.TabIndex = 7;
            this.buttonCreateUser.Text = "Create User";
            this.buttonCreateUser.UseVisualStyleBackColor = true;
            this.buttonCreateUser.Visible = false;
            this.buttonCreateUser.Click += new System.EventHandler(this.buttonCreateUser_Click);
            // 
            // buttonLogin
            // 
            this.buttonLogin.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonLogin.Font = new System.Drawing.Font("Myanmar Text", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLogin.Location = new System.Drawing.Point(76, 185);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(160, 30);
            this.buttonLogin.TabIndex = 7;
            this.buttonLogin.Text = "Login";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // labelSignin
            // 
            this.labelSignin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.labelSignin.AutoSize = true;
            this.labelSignin.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelSignin.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSignin.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.labelSignin.Location = new System.Drawing.Point(92, 43);
            this.labelSignin.Name = "labelSignin";
            this.labelSignin.Size = new System.Drawing.Size(123, 31);
            this.labelSignin.TabIndex = 2;
            this.labelSignin.Text = "SIGN IN";
            // 
            // loginGif
            // 
            this.loginGif.Image = ((System.Drawing.Image)(resources.GetObject("loginGif.Image")));
            this.loginGif.Location = new System.Drawing.Point(98, 285);
            this.loginGif.Name = "loginGif";
            this.loginGif.Size = new System.Drawing.Size(112, 69);
            this.loginGif.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.loginGif.TabIndex = 10;
            this.loginGif.TabStop = false;
            // 
            // passIcon
            // 
            this.passIcon.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.passIcon.BackColor = System.Drawing.Color.Black;
            this.passIcon.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("passIcon.BackgroundImage")));
            this.passIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.passIcon.Location = new System.Drawing.Point(42, 142);
            this.passIcon.Name = "passIcon";
            this.passIcon.Size = new System.Drawing.Size(29, 28);
            this.passIcon.TabIndex = 6;
            this.passIcon.TabStop = false;
            // 
            // userIcon
            // 
            this.userIcon.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.userIcon.BackColor = System.Drawing.Color.Black;
            this.userIcon.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("userIcon.BackgroundImage")));
            this.userIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.userIcon.Location = new System.Drawing.Point(43, 96);
            this.userIcon.Name = "userIcon";
            this.userIcon.Size = new System.Drawing.Size(29, 28);
            this.userIcon.TabIndex = 5;
            this.userIcon.TabStop = false;
            // 
            // bgImage
            // 
            this.bgImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bgImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bgImage.BackgroundImage")));
            this.bgImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bgImage.Location = new System.Drawing.Point(-4, -8);
            this.bgImage.Name = "bgImage";
            this.bgImage.Size = new System.Drawing.Size(522, 482);
            this.bgImage.TabIndex = 0;
            this.bgImage.TabStop = false;
            this.bgImage.Click += new System.EventHandler(this.bgImage_Click);
            // 
            // Login_page
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(515, 473);
            this.Controls.Add(this.panelFormbg);
            this.Controls.Add(this.bgImage);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1000, 1000);
            this.Name = "Login_page";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login Page";
            this.Load += new System.EventHandler(this.login_page_Load);
            this.panelFormbg.ResumeLayout(false);
            this.panelFormbg.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loginGif)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.passIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bgImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox bgImage;
        private System.Windows.Forms.Panel panelFormbg;
        private System.Windows.Forms.Label labelSignin;
        private System.Windows.Forms.PictureBox passIcon;
        private System.Windows.Forms.PictureBox userIcon;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.TextBox textBoxPass;
        private System.Windows.Forms.TextBox textBoxUser;
        private System.Windows.Forms.PictureBox loginGif;
        private System.Windows.Forms.Button buttonCreateUser;
    }
}