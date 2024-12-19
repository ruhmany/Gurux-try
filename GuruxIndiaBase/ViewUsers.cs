using System.Data;

namespace Gurux_Testing
{
    public class ViewUsers
    {
        public ViewUsers()
        {
            InitializeComponent();
        }
        DataTable UserViewTable = new DataTable();
        BindingSource bs1 = new BindingSource();
        CryptoStuff csObj = new CryptoStuff();
        private void ViewUsers_Load(object sender, EventArgs e)
        {
            csObj.DecryptFile(Login.password, "Config_File.DAT", "Config_File.INI");
            //cb_search.Text = "Name";
            Load_User_Table();
            FillGridView1();
            csObj.EncryptFile(Login.password, "Config_File.INI", "Config_File.DAT");
            File.Delete("Config_File.INI");
        }
        private void Load_User_Table()
        {
            UserViewTable.Columns.Add("User_Name", typeof(string));
            UserViewTable.Columns.Add("Password", typeof(string));
            UserViewTable.Columns.Add("Privilege", typeof(string));
        }
        private void FillGridView1()
        {
            string File_Path = "Config_File.INI";
            string[] field = null;
            string line_1;
            UserViewTable.Clear();
            StreamReader file = new StreamReader(File_Path);
            while ((line_1 = file.ReadLine()) != null)
            {
                if (line_1 != "")
                {
                    field = line_1.Split('|');
                    UserViewTable.Rows.Add(new object[] { field[1], field[2], field[3] });//, field[4], field[5], field[6], field[7], field[8], field[9] });
                }
            }
            file.Close();
            bs1.DataSource = UserViewTable;
            dgvUsers.DataSource = bs1;
            dgvUsers.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.ColumnHeader);

        }

        private void b_new_Click(object sender, EventArgs e)
        {
            Login.userMode = "NEW";
            AddUser NewUserForm = new AddUser();
            NewUserForm.ShowDialog();
            csObj.DecryptFile(Login.password, "Config_File.DAT", "Config_File.INI");
            FillGridView1();
            csObj.EncryptFile(Login.password, "Config_File.INI", "Config_File.DAT");
            File.Delete("Config_File.INI");
        }

        private void b_edit_Click(object sender, EventArgs e)
        {
            Login.userMode = "EDIT";
            Login.Edit_Name = dgvUsers.CurrentRow.Cells[0].Value.ToString();
            Login.Edit_Previle = dgvUsers.CurrentRow.Cells[2].Value.ToString();
            AddUser euForm = new AddUser();
            euForm.ShowDialog();
            csObj.DecryptFile(Login.password, "Config_File.DAT", "Config_File.INI");
            FillGridView1();
            csObj.EncryptFile(Login.password, "Config_File.INI", "Config_File.DAT");
            File.Delete("Config_File.INI");
        }

        private void b_delete_Click(object sender, EventArgs e)
        {
            csObj.DecryptFile(Login.password, "Config_File.DAT", "Config_File.INI");
            DialogResult result = MessageBox.Show("Are you sure to delete the data ?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
            if (result == DialogResult.Yes)
            {
                string name = dgvUsers.CurrentRow.Cells[0].Value.ToString();
                string role = dgvUsers.CurrentRow.Cells[1].Value.ToString();
                if (name != null && role != null)
                {
                    string File_Path = "Config_File.INI";
                    string[] field = null;
                    string tempfile = Path.GetTempFileName();
                    using (StreamReader sr = new StreamReader(File_Path))
                    using (StreamWriter sw = new StreamWriter(tempfile))
                    {
                        string line_1;
                        while ((line_1 = sr.ReadLine()) != null)
                        {
                            if (line_1 != "")
                            {
                                field = line_1.Split('|');
                                if ((field[1] != name))
                                {
                                    sw.WriteLine(line_1);
                                }
                            }
                        }
                    }
                    File.Delete(File_Path);
                    File.Move(tempfile, File_Path);
                    FillGridView1();
                }
            }
            csObj.EncryptFile(Login.password, "Config_File.INI", "Config_File.DAT");
            File.Delete("Config_File.INI");
        }

        private void tb_search_TextChanged(object sender, EventArgs e)
        {
            if (cb_search.Text == "Name")
                bs1.Filter = string.Format("User_Name LIKE '%{0}%'", tb_search.Text);
            else if (cb_search.Text == "Privilege")
                bs1.Filter = string.Format("Privilege LIKE '%{0}%'", tb_search.Text);
        }
    }
}
