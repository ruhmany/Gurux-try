using System.Net.NetworkInformation;

namespace Gurux_Testing
{
    class Login
    {
        public static string File_Name = "";
        public static string U_ID = "";
        public static string U_PWD = "";
        public static string NU_ID = "";
        public static string NU_PWD = "";
        public static string SET_Privilege = "";
        public static string N_PWD = "";
        public static string result = "";
        public static string Privilege = "";
        public static string UserName = "";
        public static string password = "varun";
        public static string Edit_Name = "";
        public static string Edit_Previle = "";
        public static string userMode = "";
        // CryptorEngine csObj = new CryptorEngine();
        //public static bool CheckLicence()
        //{
        //    bool status = false;
        //    string[] key = File.ReadAllLines("security.lin");
        //    if (key.Length != 0)
        //    {
        //        string deText = CryptorEngine.Decrypt(key[0], true);
        //        if (deText == GetBiosProcessorID())
        //            status = true;
        //        else
        //            status = false;
        //    }
        //    else
        //        status = false;
        //    return status;
        //}
        private string GetMACAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet)// && adapter.OperationalStatus == OperationalStatus.Up)
                {
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                    break;
                }
            }
            return sMacAddress;
        }
        //private static string GetBiosProcessorID()
        //{
        //    ManagementObjectCollection mbsList = null;
        //    ManagementObjectSearcher mbs = new ManagementObjectSearcher("Select * From Win32_processor");
        //    mbsList = mbs.Get();
        //    string id = "";
        //    foreach (ManagementObject mo in mbsList)
        //    {
        //        id = mo["ProcessorID"].ToString();
        //    }
        //    ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
        //    ManagementObjectCollection moc = mos.Get();
        //    string motherBoard = "";
        //    foreach (ManagementObject mo in moc)
        //    {
        //        motherBoard = (string)mo["SerialNumber"];
        //    }
        //    return id + motherBoard;
        //}
        public void createLogFile()
        {
            //if (!System.IO.Directory.Exists(Program.Default_Target_Directory)) //!Directory.Exists(path))
            //{
            //    // Create the directory it does not exist.
            //    System.IO.Directory.CreateDirectory(Program.Default_Target_Directory);
            //}
            //if (!System.IO.Directory.Exists(Program.Log_Directory)) //!Directory.Exists(path))
            //{
            //    // Create the directory it does not exist.
            //    System.IO.Directory.CreateDirectory(Program.Log_Directory);
            //}
            //if (!File.Exists(Program.Log_Directory + "\\" + Program.LogFile))
            //{
            //    File.Create(Program.Log_Directory + "\\" + Program.LogFile).Dispose();
            //}
        }
        public void Config_File(string WORK)
        {
            try
            {
                File_Name = "Config_File.INI";
                bool exists;
                result = "";
                switch (WORK)
                {
                    case "CREATE_FILE":
                        //creating target directory first time if not exist
                        //if (!System.IO.Directory.Exists(Program.Default_Target_Directory)) //!Directory.Exists(path))
                        //{
                        //    // Create the directory it does not exist.
                        //    System.IO.Directory.CreateDirectory(Program.Default_Target_Directory);
                        //}
                        if (!File.Exists(File_Name))
                        {
                            File.WriteAllText(File_Name, "");
                        }
                        if ((new FileInfo(File_Name).Length == 0))
                        {
                            //File.WriteAllText(File_Name, "[Log_IN]|Admin|Admin|ADMIN");
                            StreamWriter streamWriter = new StreamWriter(File_Name);
                            streamWriter.WriteLine("[Log_IN]|Admin|admin|ADMIN");
                            streamWriter.Close();
                        }
                        break;

                    case "LOGIN":
                        {
                            // One way to see if a certain string is a line
                            // ... in the specified file. Uses LINQ to count elements
                            // ... (matching lines), and then sets |exists| to true
                            // ... if more than 0 matches were found.
                            //string[] Lines = File.ReadAllLines(File_Name);
                            string Line;
                            using (StreamReader SR = new StreamReader(File_Name))
                            {
                                while ((Line = SR.ReadLine()) != null)
                                {
                                    if (Line.Contains("[Log_IN]|" + U_ID + "|" + U_PWD + "|"))
                                    {
                                        string[] Fields = Line.Split('|');
                                        Privilege = Fields[3];
                                        result = "OK";
                                    }
                                }
                                SR.Close();
                            }
                            //exists = (from line in File.ReadAllLines(File_Name)
                            //          where line == "[Log_IN]|" + U_ID + "|" + U_PWD + "|"
                            //          select line).Count() > 0;
                            //if (exists)
                            //    result = "OK";
                            //else
                            //    result = "NO";
                        }
                        break;

                    case "ADD_USER":
                        {
                            // One way to see if a certain string is a line
                            // ... in the specified file. Uses LINQ to count elements
                            // ... (matching lines), and then sets |exists| to true
                            // ... if more than 0 matches were found.
                            exists = (from line in File.ReadAllLines(File_Name)
                                      where line.Contains("[Log_IN]|" + NU_ID + "|" + NU_PWD + "|" + Privilege)// == "[Log_IN]|" + NU_ID + "|" + NU_PWD + "|" + Privilege
                                      select line).Count() > 0;
                            if (!exists)
                            {
                                StreamWriter streamWriter = File.AppendText(File_Name);
                                streamWriter.WriteLine("[Log_IN]|" + NU_ID + "|" + NU_PWD + "|" + SET_Privilege);// + "|" + nconfig1 + "|" + nconfig2 + "|" + nconfig3 + "|" + nconfig4 + "|" + nconfig5 + "|" + nconfig6);
                                streamWriter.Close();
                                result = "OK";
                            }
                            else
                                result = "NO";
                        }
                        break;

                    case "EDIT_USER":
                        // Open a file for reading
                        StreamReader streamReader;
                        streamReader = File.OpenText(File_Name);
                        // Now, read the entire file into a string
                        string contents = streamReader.ReadToEnd();
                        streamReader.Close();
                        // Write the modification into the same file

                        exists = (from line in File.ReadAllLines(File_Name)
                                  where line.Contains("[Log_IN]|" + NU_ID + "|" + NU_PWD + "|" + Privilege)// == "[Log_IN]|" + U_ID + "|" + U_PWD + "|" + Privilege
                                  select line).Count() > 0;
                        if (exists)
                        {
                            StreamWriter streamWriter = new StreamWriter(File_Name);
                            streamWriter.Write(contents.Replace("[Log_IN]|" + U_ID + "|" + U_PWD + "|" + Privilege, "[Log_IN]|" + NU_ID + "|" + N_PWD + "|" + SET_Privilege));
                            streamWriter.Close();
                            result = "OK";
                        }
                        else
                            result = "NO";
                        break;
                }
            }
            catch (System.Exception e) { MessageBox.Show(e.Message + "get config"); }
            finally { }
        }
    }
}
