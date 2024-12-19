using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gurux_Testing
{
    public partial class Login_Form : Form
    {
        public Login_Form()
        {
            InitializeComponent();
        }
        Login loginObj = new Login();
        CryptoStuff csObj = new CryptoStuff();

        private void b_login_Click(object sender, EventArgs e)
        {
            //Forms.AddNew comset = new AddNew();
            Login.U_ID = cb_userList.Text;
            Login.U_PWD = tb_password.Text;
            csObj.DecryptFile(Login.password, "Config_File.DAT", "Config_File.INI");
            if (cb_userList.Text != "" && tb_password.Text != "")
            {
                loginObj.Config_File("LOGIN");
                if (Login.result == "OK")
                {
                    Login.UserName = cb_userList.Text;
                    ConnectionControl UI = new ConnectionControl();
                    UI.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("INVALID USER NAME OR PASSWORD!", "Login Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tb_password.Text = "";
                    tb_password.Focus();
                }
            }
            else
            { MessageBox.Show("Fileds are blank!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            csObj.EncryptFile(Login.password, "Config_File.INI", "Config_File.DAT");
            File.Delete("Config_File.INI");
        }

        private void Login_Form_Load(object sender, EventArgs e)
        {
            label3.Text = "DLMS TOOL";
            tb_password.Focus();
            if (File.Exists("Config_File.DAT"))
            {
                csObj.DecryptFile(Login.password, "Config_File.DAT", "Config_File.INI");
                Load_User_Name();
                csObj.EncryptFile(Login.password, "Config_File.INI", "Config_File.DAT");
                File.Delete("Config_File.INI");
            }
            else
            {
                loginObj.Config_File("CREATE_FILE");                
                csObj.EncryptFile(Login.password, "Config_File.INI", "Config_File.DAT");
                csObj.DecryptFile(Login.password, "Config_File.DAT", "Config_File.INI");
                Load_User_Name();
                File.Delete("Config_File.INI");
                //MessageBox.Show("Configuration File is not found", "Message", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                //Application.Exit();
            }
        }

        private void Load_User_Name()
        {
            string File_Path = "Config_File.INI";
            string[] field = null;
            string line_1;
            cb_userList.Items.Clear();
            StreamReader file = new StreamReader(File_Path);
            while ((line_1 = file.ReadLine()) != null)
            {
                if (line_1 != "")
                {
                    field = line_1.Split('|');
                    //if (field[1] != "Admin")
                    cb_userList.Items.Add(field[1]);
                }
            }
            file.Close();
            if (cb_userList.Items.Count != 0)
                cb_userList.Text = cb_userList.Items[0].ToString();
        }
    }
}
