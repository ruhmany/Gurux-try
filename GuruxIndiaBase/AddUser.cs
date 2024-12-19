namespace Gurux_Testing
{
    public class AddUser
    {
        public AddUser()
        {
        }
        CryptoStuff csObj = new CryptoStuff();
        Login lgnObj = new Login();

        private void AddUser_Load()
        {
            switch (Login.userMode)
            {
                case "NEW":
                    this.Text = "New User";
                    break;

                case "EDIT":
                    this.Text = "Edit User";
                    tb_usrname.Text = Login.Edit_Name;
                    cb_privilge.Text = Login.Edit_Previle;
                    break;
            }
        }

        private void b_reset_Click(object sender, EventArgs e)
        {
            tb_usrname.Text = "";
            tb_passwrd.Text = "";
            cb_privilge.Text = "";
            tb_usrname.Focus();
        }

        private void b_ok_Click(object sender, EventArgs e)
        {
            csObj.DecryptFile(Login.password, "Config_File.DAT", "Config_File.INI");
            switch (Login.userMode)
            {
                case "NEW":
                    Login.NU_ID = tb_usrname.Text;
                    Login.NU_PWD = tb_passwrd.Text;
                    Login.SET_Privilege = cb_privilge.Text;
                    if (tb_usrname.Text != "" && tb_passwrd.Text != "" && cb_privilge.Text != "")
                    {
                        lgnObj.Config_File("ADD_USER");
                        if (Login.result == "OK")
                        {
                            MessageBox.Show("User has been added Successfully!", "Add User", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("User already exists!", "Add User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            tb_usrname.Text = "";
                            tb_passwrd.Text = "";
                            cb_privilge.Text = "";
                            tb_usrname.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please enter all details of user..", "Add User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    break;

                case "EDIT":
                    string File_Path = "Config_File.INI";
                    string line_one = "";
                    string[] field = null;
                    bool exists = false;
                    string line_1;
                    if (blanck_Check())
                    {
                        // Open a file for reading
                        StreamReader streamReader;
                        streamReader = File.OpenText(File_Path);
                        // Now, read the entire file into a string
                        string contents = streamReader.ReadToEnd();
                        streamReader.Close();
                        line_one = "[Log_IN]|" + tb_usrname.Text + "|" + tb_passwrd.Text + "|" + cb_privilge.Text + "|";
                        System.IO.StreamReader file2 = new System.IO.StreamReader(File_Path);
                        while ((line_1 = file2.ReadLine()) != null)
                        {
                            field = line_1.Split('|');
                            if (field[1] == tb_usrname.Text && field[3] == cb_privilge.Text)
                            {
                                exists = true;
                                break;
                            }
                        }
                        file2.Close();
                        this.Refresh();
                        if (exists == true)
                        {
                            // Write the modification into the same file
                            StreamWriter sw = File.CreateText(File_Path);
                            sw.Write(contents.Replace(line_1, line_one));
                            sw.Close();
                            this.Close();
                        }
                        else
                            MessageBox.Show("User not exist\n", "SAVE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        line_one = "";
                    }
                    else
                        MessageBox.Show("Details are Blank...\n", "SAVE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
            csObj.EncryptFile(Login.password, "Config_File.INI", "Config_File.DAT");
            File.Delete("Config_File.INI");
        }
        private bool blanck_Check()
        {
            bool opt = false;
            if (tb_usrname.Text == "" || tb_passwrd.Text == "" || cb_privilge.Text == "")
            {
                opt = false;
            }
            else
            {
                opt = true;
            }
            return opt;
        }

        private void b_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
