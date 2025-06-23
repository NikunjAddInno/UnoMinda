using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VoltasBeko.Classes;



namespace VoltasBeko
{
   
    public partial class Login_page : Form
    {
        
        public Login_page()
        {

            InitializeComponent();
            AppData.AdminUser = false;

        }

        private void login_page_Load(object sender, EventArgs e)
        {

        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (UserAuthentication.VerifyUserCredentials(textBoxUser.Text, textBoxPass.Text))
                {
                    AppData.AdminUser = true;
                    buttonCreateUser.Visible = true;
                }
                else
                {
                    AppData.AdminUser = false;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
           
        }

        private void bgImage_Click(object sender, EventArgs e)
        {

        }

        //private void checkBoxSettings_CheckedChanged(object sender, EventArgs e)
        //{
        //    AppData.AdminUser = checkBoxSettings.Checked;
        //    if (checkBoxSettings.Checked)
        //    {
        //        Program.starting_page.ShowSettings();
        //        buttonCreateUser.Visible = true;
        //    }
        //    else
        //    {
        //        Program.starting_page.HideSettings();
        //        buttonCreateUser.Visible = false;

        //    }
        //}

        private void buttonCreateUser_Click(object sender, EventArgs e)
        {
            try
            {
                UserAuthentication.CreateUser(textBoxUser.Text, textBoxPass.Text);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
