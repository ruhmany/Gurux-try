using Gurux.DLMS.Enums;
using Gurux.Net;
using System.Text;

namespace Gurux_Testing
{

    public class DlmsSettings
    {
        string filePath = Program.Default_Target_Directory + "\\" + "PortSettings.txt";
        ConnectionSettings _connection;
        public DlmsSettings(ConnectionSettings connection)
        {
            this._connection = connection;
        }


        public void SetCommunication()
        {
            try
            {

                Program.iCounter = Program.iCounter + _connection.InvocationCounter;
                Program.Mediatype = _connection.MediaType;
                Program.selectedAuth = _connection.AuthType;
                Program.selectedPort = _connection.Port;
                Program.password = _connection.Password;
                //InitializeMedia.initializeClient(_connection.AuthType.Trim());
                initializeClient(_connection.AuthType);
                string p_settings = "";
                switch (Program.Mediatype)
                {
                    case "S E R I A L":
                        InitializeMedia.serial.DataBits = 8;
                        InitializeMedia.serial.Parity = System.IO.Ports.Parity.None;
                        InitializeMedia.serial.StopBits = System.IO.Ports.StopBits.One;
                        InitializeMedia.serial.PortName = _connection.Port;
                        InitializeMedia.serial.BaudRate = _connection.BaudRate;
                        InitializeMedia._client.InterfaceType = InterfaceType.HDLC;
                        p_settings = "serial" + "|" + _connection.Port + "|" + _connection.BaudRate + "|" + _connection.AuthType + "|" + _connection.Password;
                        break;
                    case "G P R S":
                        InitializeMedia.gprs.UseIPv6 = false;
                        InitializeMedia.gprs.Port = Convert.ToInt32(_connection.Port); //4059;
                        InitializeMedia.gprs.Protocol = NetworkType.Tcp;
                        InitializeMedia.gprs.HostName = _connection.HostName;
                        InitializeMedia._client.InterfaceType = InterfaceType.WRAPPER;
                        p_settings = "gprs" + "|" + _connection.HostName + "|" + _connection.Port + "|" + _connection.AuthType + "|" + _connection.Password;
                        break;
                }
                CreateFile(p_settings);
                //if (_connection.MediaType == "S E R I A L")
                //{
                //    MediaSettings.setMedia("Serial", _connection.Port, cb_baudrate.Text, false);
                //}
                //else
                //{
                //    if (chk_ipv6.Checked == true)
                //        MediaSettings.setMedia("Gprs", tb_hostname.Text.Trim(), "", true);
                //    else
                //        MediaSettings.setMedia("Gprs", tb_hostname.Text.Trim(), "", false);
                //}             
                //MainForm obj = new MainForm();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }

        }

        private void CreateFile(string settings)
        {
            File.WriteAllText(filePath, String.Empty);
            WriteIntoFile(filePath, settings);
        }

        public void WriteIntoFile(string path, string data)
        {
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine(data);
                sw.Close();
            }
        }

        private static Random RNG = new Random();

        public string Create16DigitString()
        {
            var builder = new StringBuilder();
            while (builder.Length < 16)
            {
                builder.Append(RNG.Next(10).ToString());
            }
            return builder.ToString();
        }

        public void initializeClient(string auType)
        {
            // InitializeMedia._client.Ciphering.InvocationCounter = (uint)Program.iCounter;
            string randomkey = Create16DigitString();
            InitializeMedia._client.ServerAddress = 1;
            InitializeMedia._client.UseLogicalNameReferencing = true;
            InitializeMedia._client.Ciphering.SystemTitle = ASCIIEncoding.ASCII.GetBytes(_connection.SystemSubTitle);
            InitializeMedia._client.Ciphering.BlockCipherKey = ASCIIEncoding.ASCII.GetBytes(_connection.BlockCipherKey);
            InitializeMedia._client.Ciphering.AuthenticationKey = ASCIIEncoding.ASCII.GetBytes(_connection.AuthKey);
            //InitializeMedia._client.Ciphering.DedicatedKey = ASCIIEncoding.ASCII.GetBytes(key);
            //InitializeMedia._client.ServerSystemTitle = new byte[] { 0x45, 0x4C, 0x53, 0x30, 0x30, 0x30, 0x31, 0x30 };
            switch (auType)
            {
                case "None":
                    InitializeMedia._client.ClientAddress = 16;
                    InitializeMedia._client.Authentication = Authentication.None;
                    InitializeMedia._client.Ciphering.Security = Security.None;
                    InitializeMedia._client.Password = ASCIIEncoding.ASCII.GetBytes(Program.password);
                    break;
                case "Low":
                    InitializeMedia._client.ClientAddress = 32;
                    InitializeMedia._client.Authentication = Authentication.Low;
                    InitializeMedia._client.Ciphering.Security = Security.Encryption;
                    InitializeMedia._client.Password = ASCIIEncoding.ASCII.GetBytes(Program.password);
                    break;
                case "High":
                    InitializeMedia._client.ClientAddress = 48;
                    InitializeMedia._client.Authentication = Authentication.High;
                    InitializeMedia._client.Ciphering.Security = Security.AuthenticationEncryption;
                    InitializeMedia._client.Password = ASCIIEncoding.ASCII.GetBytes(Program.password);
                    break;
                case "Firmware":
                    InitializeMedia._client.ClientAddress = 80;
                    InitializeMedia._client.Authentication = Authentication.High;
                    InitializeMedia._client.Ciphering.Security = Security.AuthenticationEncryption;
                    InitializeMedia._client.Password = ASCIIEncoding.ASCII.GetBytes(Program.password);

                    break;
            }
            Thread.Sleep(600);
        }

        private void cb_authentication_SelectedIndexChanged(object sender, EventArgs e)
        {
            string security = _connection.AuthType;  // neeraj auth 2 for test
            switch (security)
            {
                case "None":
                    _connection.Password = "";
                    //MediaSettings._client.ClientAddress = 16;
                    //MediaSettings._client.Authentication = Authentication.None;
                    //MediaSettings._client.Ciphering.Security = Security.None;
                    //MediaSettings._client.Ciphering.InvocationCounter = 90074;
                    break;
                case "Low":
                    _connection.Password = Program.password;
                    Global.process = "Low";
                    //MediaSettings._client.ClientAddress = 32;
                    //MediaSettings._client.Authentication = Authentication.Low;
                    //MediaSettings._client.Ciphering.Security = Security.Encryption;
                    //MediaSettings._client.Password = ASCIIEncoding.ASCII.GetBytes(_connection.Password.Trim());
                    //MediaSettings._client.Ciphering.InvocationCounter = 90074;
                    break;
                case "High":
                    _connection.Password = Program.password;
                    Global.process = "High";
                    //MediaSettings._client.ClientAddress = 48;
                    //MediaSettings._client.Authentication = Authentication.High;
                    //MediaSettings._client.Ciphering.Security = Security.AuthenticationEncryption;
                    //MediaSettings._client.Password = ASCIIEncoding.ASCII.GetBytes(_connection.Password.Trim());
                    //MediaSettings._client.Ciphering.InvocationCounter = 90074;
                    break;
                case "Firmware":
                    _connection.Password = Program.password;
                    Global.process = "Firmware";
                    //MediaSettings._client.ClientAddress = 48;
                    //MediaSettings._client.Authentication = Authentication.High;
                    //MediaSettings._client.Ciphering.Security = Security.AuthenticationEncryption;
                    //MediaSettings._client.Password = ASCIIEncoding.ASCII.GetBytes(_connection.Password.Trim());
                    //MediaSettings._client.Ciphering.InvocationCounter = 90074;
                    break;
            }
        }

        private void setMedia(string media)
        {
            //switch (media)
            //{
            //    //case "Serial":
            //    //    cb_ports.Enabled = true;
            //    //    tb_hostname.Enabled = false;
            //    //    tb_portname.Enabled = false;
            //    //    cb_protocol.Enabled = false;
            //    //    Settings.media = new GXSerial();
            //    //    GXSerial serial = Settings.media as GXSerial;
            //    //    serial.PortName = _connection.Port.Trim();
            //    //    serial.BaudRate = 9600;
            //    //    serial.DataBits = 8;
            //    //    serial.Parity = System.IO.Ports.Parity.None;
            //    //    serial.StopBits = System.IO.Ports.StopBits.One;
            //    //    Settings.client.InterfaceType = InterfaceType.HDLC;
            //    //    break;
            //    //case "Net":
            //    //    cb_ports.Enabled = false;
            //    //    tb_hostname.Enabled = true;
            //    //    tb_portname.Enabled = true;
            //    //    cb_protocol.Enabled = true;
            //    //    Settings.media = new GXNet();
            //    //    GXNet xNet = Settings.media as GXNet;
            //    //    xNet.HostName = tb_hostname.Text.Trim(); ;
            //    //    xNet.Port = Int32.Parse(tb_portname.Text);
            //    //    xNet.UseIPv6 = true;
            //    //    xNet.Protocol = NetworkType.Tcp;
            //    //    Settings.client.InterfaceType = InterfaceType.WRAPPER;
            //    //    break;
            //    //case "Serial":
            //    //    cb_ports.Enabled = true;
            //    //    tb_hostname.Enabled = false;
            //    //    tb_portname.Enabled = false;
            //    //    cb_protocol.Enabled = false;                    
            //    //    break;
            //    //case "Net":
            //    //    cb_ports.Enabled = false;
            //    //    tb_hostname.Enabled = true;
            //    //    tb_portname.Enabled = true;
            //    //    cb_protocol.Enabled = true;                  
            //    //    break;
            //}
        }
    }
}
