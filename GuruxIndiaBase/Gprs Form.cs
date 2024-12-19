using Gurux.Common;
using Gurux.DLMS;
using Gurux.DLMS.Enums;
using Gurux.DLMS.Objects;
using Gurux.DLMS.Secure;
using Gurux.Net;
using System.Text;

namespace Gurux_Testing
{
    public class Gprs_Form
    {
        string ipaddress = "";
        string portnum = "";
        delegate void SetTextCallback(string text);
        byte[] cipher = new byte[] { 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62 };
        Function function = new Function();
        Utility utility = new Utility();
        GXNet server = new GXNet(NetworkType.Tcp, 4059);
        public IGXMedia mediagp = new GXNet();
        public GXNet gprs = null;
        public GXDLMSSecureClient _client = new GXDLMSSecureClient(true);
        GXDLMSTranslator gXDLMSTranslator = new Gurux.DLMS.GXDLMSTranslator(TranslatorOutputType.SimpleXml);
        public static Gprs_Form gprs_Form;
        GXDLMSSecureClient dlmsClient = new GXDLMSSecureClient()
        {
            Password = Encoding.ASCII.GetBytes("123456"),
            Authentication = Gurux.DLMS.Enums.Authentication.Low,
            ClientAddress = 32,
            ServerAddress = 1,
            UseLogicalNameReferencing = true,
            InterfaceType = Gurux.DLMS.Enums.InterfaceType.WRAPPER
        };
        public Gprs_Form()
        {
            gprs_Form = this;
            //_client.ServerAddress = 1;
            //_client.ClientAddress = 32;
            //_client.UseLogicalNameReferencing = true;
            //_client.Password = ASCIIEncoding.ASCII.GetBytes("123456");
            //_client.Ciphering.SystemTitle = ASCIIEncoding.ASCII.GetBytes("qwertyui");
            //_client.Ciphering.BlockCipherKey = ASCIIEncoding.ASCII.GetBytes("bbbbbbbbbbbbbbbb");
            //_client.Ciphering.AuthenticationKey = ASCIIEncoding.ASCII.GetBytes("bbbbbbbbbbbbbbbb");
            //_client.Ciphering.InvocationCounter = 0;            
            //_client.Authentication = Authentication.Low;                 
            //_client.InterfaceType = InterfaceType.WRAPPER;
            //_client.Ciphering.Security = Security.Encryption;
            ////*************************************************//
            //mediagp = new GXNet();
            //gprs = mediagp as GXNet;
            //gprs.Protocol = NetworkType.Tcp;
            //gprs.UseIPv6 = true;
            //gprs.Port = 4059;// Int32.Parse(tb_portname.Text);
            //gprs.HostName = "2402:3a80:1700:047e::2";
        }

        private void OnClientConnected(object sender, ConnectionEventArgs e)
        {
            var IP_Port = e.Info.Split(']');
            //var IP_Port = e.Info.Split(':', StringSplitOptions.RemoveEmptyEntries);
            ipaddress = IP_Port[0].Remove(0, 1); portnum = IP_Port[1].Remove(0, 1);
            //rtbLogs.Invoke(new Action(() => { rtbLogs.Text = "IP = " + ipaddress + "      PORT = " + portnum + "\n"; }));

            //try
            //{
            //    using (GXNet media = new GXNet(NetworkType.Tcp, ipaddress, 4059))// Int32.Parse(portnum)))
            //    {
            //        media.UseIPv6 = true;
            //        GXDLMSSecureClient dlmsClient = new GXDLMSSecureClient()
            //        {
            //            Password = Encoding.ASCII.GetBytes("123456"),
            //            Authentication = Gurux.DLMS.Enums.Authentication.Low,
            //            ClientAddress = 32,
            //            ServerAddress = 1,
            //            UseLogicalNameReferencing = true,
            //            InterfaceType = Gurux.DLMS.Enums.InterfaceType.WRAPPER
            //        };
            //        dlmsClient.Ciphering.Security = Gurux.DLMS.Enums.Security.Encryption;
            //        dlmsClient.Ciphering.SystemTitle = Encoding.ASCII.GetBytes("qwertyui");
            //        dlmsClient.Ciphering.AuthenticationKey = new byte[] { 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62 };
            //        dlmsClient.Ciphering.BlockCipherKey = new byte[] { 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62 };

            //        GXDLMSReader reader = new GXDLMSReader(dlmsClient, media, System.Diagnostics.TraceLevel.Info);
            //        media.Open();
            //        reader.InitializeConnection();
            //        GXDLMSData serial = new GXDLMSData("0.0.96.1.0.255");
            //        string serialNumber = reader.ReadObis(serial, 2).ToString();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

            //server.Send()
            ////try
            ////{
            ////   // var IP_Port = e.Info.Split(':');
            ////    using (GXNet cl = new GXNet(NetworkType.Tcp, "", 4059))
            ////    {
            ////        GXDLMSSecureClient dlmsClient = new GXDLMSSecureClient()
            ////        {
            ////            Authentication = Gurux.DLMS.Enums.Authentication.Low,
            ////            ClientAddress = 1,
            ////            ServerAddress = 1,
            ////            UseLogicalNameReferencing = true,
            ////            InterfaceType = Gurux.DLMS.Enums.InterfaceType.HDLC,

            ////        };

            ////        dlmsClient.Ciphering.Security = Gurux.DLMS.Enums.Security.AuthenticationEncryption;
            ////        dlmsClient.Ciphering.SystemTitle = Encoding.ASCII.GetBytes("qwertyui");
            ////        dlmsClient.Ciphering.AuthenticationKey = new byte[] { 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62 };
            ////        dlmsClient.Ciphering.BlockCipherKey = new byte[] { 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62 };

            ////        GXDLMSReader cl1 = new GXDLMSReader(dlmsClient, cl, System.Diagnostics.TraceLevel.Verbose);

            ////        //cl1.ReadAll(false);

            ////        //Create own thread for each meter if you are handling multiple meters simultaneously.
            ////        //new Thread(new ThreadStart(cl.ReadAll));
            ////    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
        }

        private void btn_start_Click(object sender, EventArgs e)
        {

        }

        private void btn_start_Click_1(object sender, EventArgs e)
        {
            server.UseIPv6 = true;
            server.OnClientConnected += OnClientConnected;
            server.OnClientDisconnected += Server_OnClientDisconnected;
            server.OnReceived += Server_OnReceived;
            server.Open();
        }

        private void Server_OnReceived(object sender, ReceiveEventArgs e)
        {
            int len = 0;
            int parameter_count = 0;
            string i_string = "";
            string senderinfo = e.SenderInfo;
            byte[] rec = (byte[])e.Data;
            byte[] ARR = gXDLMSTranslator.GetDataFromPushEncriptMessage(Security.Encryption, Encoding.ASCII.GetBytes("qwertyui"), cipher, cipher, function.getDataBuffer(rec));
            i_string = "";
            string dt = "";
            int INDEX = 0;
            bool lastGasp = false;
            PushData wp = new PushData();
            wp.Push = Convert.ToInt32(ARR[INDEX]);
            INDEX = INDEX + 1;
            wp.InvokeID = utility.ByteToHex(ARR, INDEX, 4, true);
            INDEX = INDEX + 4;
            len = ARR[INDEX];
            INDEX = INDEX + 1;
            wp.DateTime1 = utility.GetRTC(ARR, INDEX);
            INDEX = INDEX + len;
            INDEX = INDEX + 1;
            parameter_count = ARR[INDEX]; INDEX = INDEX + 1;
            INDEX = INDEX + 1;
            len = ARR[INDEX];
            INDEX = INDEX + 1;
            wp.SysTitle = utility.ByteToChar(ARR, INDEX, len);
            INDEX = INDEX + len; INDEX = INDEX + 1;
            len = ARR[INDEX];
            INDEX = INDEX + 1;
            wp.PushSetup = utility.ByteToHex(ARR, INDEX, len, true);
            INDEX = INDEX + len;
            INDEX = INDEX + 1;
            len = ARR[INDEX];
            INDEX = INDEX + 1;
            wp.DateTime2 = utility.GetRTC(ARR, INDEX);
            INDEX = INDEX + len;
            INDEX = INDEX + 1;
            switch (parameter_count)
            {
                case 13:
                    Instant instant = new Instant();
                    instant.Voltage = ((float)utility.ByteToInt(ARR, INDEX, 4) / 100).ToString("N2"); INDEX = INDEX + 5;
                    instant.Current = ((float)utility.ByteToInt(ARR, INDEX, 4) / 100).ToString("N2"); INDEX = INDEX + 5;
                    instant.NeutralCurrent = ((float)utility.ByteToInt(ARR, INDEX, 4) / 1000).ToString(); INDEX = INDEX + 5;
                    instant.PowerFactor = ((float)utility.ByteToInt(ARR, INDEX, 4) / 1000).ToString("N2"); INDEX = INDEX + 5;
                    instant.Frequency = ((float)utility.ByteToInt(ARR, INDEX, 4) / 1000).ToString("N2"); INDEX = INDEX + 5;
                    instant.App_Power = ((float)utility.ByteToInt(ARR, INDEX, 4) / 100).ToString("N2"); INDEX = INDEX + 5;
                    instant.Act_Power = ((float)utility.ByteToInt(ARR, INDEX, 4) / 100).ToString("N2"); INDEX = INDEX + 5;
                    instant.Cum_Kwh = ((float)utility.ByteToInt(ARR, INDEX, 4) / 1000).ToString("N2"); INDEX = INDEX + 5;
                    instant.Cum_TemperCount = utility.ByteToInt(ARR, INDEX, 4).ToString(); INDEX = INDEX + 5;
                    instant.Billing_PeriodCount = utility.ByteToInt(ARR, INDEX, 4).ToString(); INDEX = INDEX + 5;
                    dt = DateTime.Now.ToString();
                    i_string = "Info = " + senderinfo + "\r\n" + "Time = " + dt + "\r\n" + "Push Notification = " + wp.Push + "\r\n" + "Inwoke Id = " + wp.InvokeID + "\r\n" + "Date1 = " + wp.DateTime1 + "\r\n" + "Serial Number = " + wp.SysTitle + "\r\n" +
                        "PushSetup = " + wp.PushSetup + "\r\n" + "Date2 = " + wp.DateTime2 + "\r\n" + "Push = " + wp.Push + "\r\n" + "Voltage = " + instant.Voltage + "\r\n" +
                        "Current = " + instant.Current + "\r\n" + "Neutral Current = " + instant.NeutralCurrent + "\r\n" + "Power Factor = " + instant.PowerFactor + "\r\n" +
                        "Frequency = " + instant.Frequency + "\r\n" + "Aparent Power Kva = " + instant.App_Power + "\r\n" + "Active Power Kw = " + instant.Act_Power + "\r\n" +
                        "Cum Kwh = " + instant.Cum_Kwh + "\r\n" + "Cum Temper Count = " + instant.Cum_TemperCount + "\r\n" + "Billing Period Count = " + instant.Billing_PeriodCount;
                    break;
                case 4:
                    INDEX = INDEX + 2;
                    lastGasp = false;
                    //MeterEvents events = new MeterEvents();
                    byte[] bitArray = new byte[16];
                    Array.Copy(ARR, INDEX, bitArray, 0, 16);
                    List<MeterEvents> eventName = new List<MeterEvents>();

                    string bitString = utility.getBitString(bitArray);
                    string tempers = "";
                    for (int i = 0; i < bitString.Length; i++)
                    {
                        if (bitString[i] == '1')
                        {
                            MeterEvents _event = new MeterEvents();
                            _event.EventName = utility.getTemperName(i);
                            if (_event.EventName == "Last Gasp-Occurrence")
                            { lastGasp = true; }
                            eventName.Add(_event);
                            tempers = tempers + _event.EventName + "\r\n";
                        }
                    }
                    dt = DateTime.Now.ToString();
                    i_string = "Info = " + senderinfo + "\r\n" + "Time = " + dt + "\r\n" + "Push Notification = " + wp.Push + "\r\n" + "Inwoke Id = " + wp.InvokeID + "\r\n" + "Date1 = " + wp.DateTime1 + "\r\n"
                   + "Serial Number = " + wp.SysTitle + "\r\n" + "PushSetup = " + wp.PushSetup + "\r\n" + "Date2 = " + wp.DateTime2 + "\r\n" + tempers;
                    break;
            }
            SetText(i_string); richText();// rtbLogs.Text = "";
        }

        private void Server_OnClientDisconnected(object sender, ConnectionEventArgs e)
        {
            var dis = e.Info;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GXPUSHReader reader = new GXPUSHReader(dlmsClient, mediagp, System.Diagnostics.TraceLevel.Info);
            try
            {
                mediagp.Open();
                reader.InitializeConnection();
                GXDLMSGprsSetup gprs = new GXDLMSGprsSetup(); //M2M.METERIPV6                   
                string apn = reader.ReadObis(gprs, 2).ToString();
                GXDLMSData serial = new GXDLMSData("0.0.96.1.0.255");//Serial number                    
                GXDLMSData counter = new GXDLMSData("0.0.43.1.2.255");//invocation counter
                string serialNumber = reader.ReadObis(serial, 2).ToString();
                string ic = reader.ReadObis(counter, 2).ToString();
                reader.Disconnect();
                mediagp.Close();
                SetText("Serial number = " + serialNumber);
                SetText("Invocation Counter = " + ic);
                SetText("Apn = " + apn);
                //media.OnReceived += Media_OnReceived;                  
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //}
        }

        private void Media_OnReceived(object sender, ReceiveEventArgs e)
        {
            SetText(e.Data.ToString());
        }

        private void Cl_OnReceived(object sender, ReceiveEventArgs e)
        {
            SetText(e.Data.ToString());
        }

        private void SetText(string text)
        {
            if (this.rtbLogs.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text + Environment.NewLine });
            }
            else
            {
                this.rtbLogs.Text = this.rtbLogs.Text + text + Environment.NewLine;
            }
        }
        public void richText()
        {
            rtbLogs.Invoke(new Action(() => { rtbLogs.SelectionStart = rtbLogs.Text.Length; }));
            rtbLogs.Invoke(new Action(() => { rtbLogs.ScrollToCaret(); }));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GXDLMSSecureClient dlmsClient = new GXDLMSSecureClient()
            {
                Password = Encoding.ASCII.GetBytes("123456"),
                Authentication = Gurux.DLMS.Enums.Authentication.Low,
                ClientAddress = 32,
                ServerAddress = 1,
                UseLogicalNameReferencing = true,
                InterfaceType = Gurux.DLMS.Enums.InterfaceType.WRAPPER
            };
            dlmsClient.Ciphering.Security = Gurux.DLMS.Enums.Security.Encryption;
            dlmsClient.Ciphering.SystemTitle = Encoding.ASCII.GetBytes("qwertyui");
            dlmsClient.Ciphering.AuthenticationKey = new byte[] { 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62 };
            dlmsClient.Ciphering.BlockCipherKey = new byte[] { 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62 };
            //GXNet server = (GXNet)sender;
            try
            {
                using (GXNet media = new GXNet(NetworkType.Tcp, "2402:3a80:1700:0482::2", 4059))
                {
                    media.UseIPv6 = true;
                    GXDLMSReader read = new GXDLMSReader(dlmsClient, media, System.Diagnostics.TraceLevel.Info);
                    media.Open();
                    read.InitializeConnection();
                    GXDLMSData serial = new GXDLMSData("0.0.96.1.0.255");
                    string serialNumber = read.ReadObis(serial, 2).ToString();
                    //Create own thread for each meter if you are handling multiple meters simultaneously.
                    //new Thread(new ThreadStart(cl.ReadAll));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Gprs_Form_Load(object sender, EventArgs e)
        {
            mediagp = new GXNet();
            gprs = mediagp as GXNet;
            gprs.Port = 4059;// Convert.ToInt32(portnum); 
            gprs.UseIPv6 = true;
            gprs.Protocol = NetworkType.Tcp;
            gprs.HostName = "2402:3a80:1700:0482::2";// ipaddress;            
            dlmsClient.Ciphering.InvocationCounter = 170;
            dlmsClient.Ciphering.Security = Gurux.DLMS.Enums.Security.Encryption;
            dlmsClient.Ciphering.SystemTitle = Encoding.ASCII.GetBytes("qwertyui");
            dlmsClient.Ciphering.AuthenticationKey = new byte[] { 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62 };
            dlmsClient.Ciphering.BlockCipherKey = new byte[] { 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62 };
        }
    }
}
