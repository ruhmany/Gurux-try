using Gurux.Common;
using Gurux.DLMS;
using Gurux.DLMS.Enums;
using Gurux.DLMS.Objects;
using Gurux.DLMS.Secure;
using Gurux.Net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gurux_Testing
{
    public partial class PushListener : Form
    {
        int clientPort = 0;
        string clientIP, current_date = "";
        string ipaddress = "";
        string portnum = "";        
        public IGXMedia mediagp = new GXNet();
        public string addresses;
        public GXNet gprs = null;
        public GXNet cls = new GXNet();
        public GXNet gxl;
        List<GXNet> clientInfo = new List<GXNet>();
        GXDLMSData invoc_counter = new GXDLMSData("0.0.43.1.3.255");//invocation counter
        GXDLMSData serial = new GXDLMSData("0.0.96.1.0.255");//Serial number       
        GXDLMSSecureClient dlmsClient = new GXDLMSSecureClient()
        {
            Password = Encoding.ASCII.GetBytes("123456"),
            Authentication = Authentication.Low,
            ClientAddress = 32,
            ServerAddress = 1,
            UseLogicalNameReferencing = true,
            InterfaceType = InterfaceType.WRAPPER
        };
        DataTable clientTable = new DataTable();
        delegate void SetTextCallback(string text);
        byte[] cipher = new byte[] { 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62 };
        GXNet server = null; //new GXNet(NetworkType.Tcp,6060);
        Databases dbClass = new Databases();
        Function function = new Function();
        Utility utility = new Utility();
        MediaSettings mediaSettings = new MediaSettings();        
        GXDLMSTranslator gXDLMSTranslator = new Gurux.DLMS.GXDLMSTranslator(TranslatorOutputType.SimpleXml);
        public static PushListener listener;
        public PushListener()
        {
            InitializeComponent();            
            InitializeDatatable();
            InitializeFontAndColors();// InitializeBackGroundWorker();
            CheckForIllegalCrossThreadCalls = false;
            btn_StopServer.Enabled = false;
            listener = this;
        }
        public void update(string message)
        {
            if (InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    rtbLogs.AppendText(message + "\n");
                }));
            }
            else
            {
                rtbLogs.AppendText(message + "\n");
            }
        }
        private void InitializeDatatable()
        {
            //dataTable.Columns.Add("Check", typeof(bool));
            clientTable.Columns.Add("Status", typeof(string));
            clientTable.Columns.Add("IPAddress", typeof(string));
            clientTable.Columns.Add("Port", typeof(string));
            clientTable.Columns.Add("DateTime", typeof(string));
        }

        private void InitializeFontAndColors()
        {
            this.dataGridView1.DefaultCellStyle.Font = new Font("Tahoma", 9);
            this.dataGridView1.DefaultCellStyle.ForeColor = Color.DarkOliveGreen;
            this.dataGridView1.DefaultCellStyle.BackColor = Color.DarkSeaGreen;
            this.dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
            this.dataGridView1.DefaultCellStyle.SelectionBackColor = Color.Green;
        }
       
        private void btn_startServer_Click(object sender, EventArgs e)
        {           
            try
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    int portTCP = Convert.ToInt32(tb_port.Text.Trim());
                    server = new GXNet(NetworkType.Tcp, portTCP);
                    if (ch_Ipv6.Checked == true)
                    { server.UseIPv6 = true; }
                    server.Protocol = NetworkType.Tcp;
                    server.Port = portTCP;
                    server.OnClientConnected += Server_OnClientConnected;
                    server.OnClientDisconnected += Server_OnClientDisconnected;
                    server.OnReceived += Server_OnReceived;
                    server.Open();
                    cls = server;
                    if (server.IsOpen) { serverNotify.Text = "Server is On"; }
                    btn_StopServer.Enabled = true;
                    btn_startServer.Enabled = false;
                    ch_Ipv6.Enabled = false;
                    Program.dbStatus = dbClass.checkDB_Conn();
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }

        private void Cls_OnReceived(object sender, ReceiveEventArgs e)
        {
            
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            clientTable.Rows.Add("Connected", clientIP, clientPort, current_date);            
        }

        private void Server_OnReceived(object sender, Gurux.Common.ReceiveEventArgs e)
        {
            int len = 0;
            int parameter_count = 0;
            string i_string = "";
            string senderinfo = e.SenderInfo;
            var disconnect = e.SenderInfo.Split(']');
            string dataip = disconnect[0].Remove(0, 1);
            byte[] rec = (byte[])e.Data;
            bool lastGasp = false;
            //byte[] axBuff = { 0x00, 0x01, 0x00, 0x01, 0x00, 0x40, 0x00, 0x5C, 0xDB, 0x08, 0x45, 0x4C, 0x53, 0x30, 0x30, 0x30, 0x31, 0x37, 0x51, 0x20, 0x00, 0x00, 0x00, 0x00, 0x41, 0x57, 0x2D, 0x7F, 0x76, 0xFC, 0xF0, 0x23, 0xF7, 0xEA, 0x97, 0xD6, 0xD5, 0xB7, 0x06, 0x03, 0x59, 0x4E, 0xCE, 0xD0, 0x97, 0x4A, 0xFD, 0xF2, 0x9A, 0x5D, 0x3D, 0x4B, 0x10, 0x8F, 0xFC, 0xE6, 0x82, 0x59, 0xE9, 0x6B, 0x16, 0x50, 0xD1, 0x30, 0x2B, 0x07, 0xA3, 0x9F, 0xB1, 0x3F, 0xB4, 0x0C, 0x1E, 0x55, 0x45, 0x41, 0x33, 0xD6, 0xAC, 0x86, 0xDC, 0x41, 0x48, 0xED, 0x66, 0xDD, 0xCC, 0xF9, 0x97, 0xF0, 0x9C, 0x14, 0x59, 0x55, 0x16, 0x0E, 0xC6, 0x77, 0xC4, 0x46 };
            
            if (rec.Length == 100 || rec.Length == 131 || rec.Length == 116)
            {
                #region pushpacket                
                SetText("Recieved Packet = " + utility.ByteArrayToString(rec));
                byte[] ARR = gXDLMSTranslator.GetDataFromPushEncriptMessage(Security.Encryption, Encoding.ASCII.GetBytes("ELS00001"), cipher, cipher, function.getDataBuffer(rec));              
                SetText("Translated Packet = " + utility.ByteArrayToString(ARR));
                i_string = "";
                string dt = "";
                int INDEX = 0;

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
                        //instant.Cum_Kwh = ((float)utility.ByteToInt(ARR, INDEX, 4) / 1000).ToString("N2"); INDEX = INDEX + 5;
                        //instant.Cum_TemperCount = utility.ByteToInt(ARR, INDEX, 4).ToString(); INDEX = INDEX + 5;
                        //.Billing_PeriodCount = utility.ByteToInt(ARR, INDEX, 4).ToString(); INDEX = INDEX + 5;
                        dt = DateTime.Now.ToString();
                        i_string = "Client Info = " + senderinfo + "\r\n" + "Time = " + dt + "\r\n" + "Push Notification = " + wp.Push + "\r\n" + "Inwoke Id = " + wp.InvokeID + "\r\n" + "Date1 = " + wp.DateTime1 + "\r\n" + "Serial Number = " + wp.SysTitle + "\r\n" +
                            "PushSetup = " + wp.PushSetup + "\r\n" + "Date2 = " + wp.DateTime2 + "\r\n" + "Push = " + wp.Push + "\r\n" + "Voltage = " + instant.Voltage + "\r\n" +
                            "Current = " + instant.Current + "\r\n" + "Neutral Current = " + instant.NeutralCurrent + "\r\n" + "Power Factor = " + instant.PowerFactor + "\r\n" +
                            "Frequency = " + instant.Frequency + "\r\n" + "Aparent Power Kva = " + instant.App_Power + "\r\n" + "Active Power Kw = " + instant.Act_Power + "\r\n";// +
                            //"Cum Kwh = " + instant.Cum_Kwh + "\r\n" + "Cum Temper Count = " + instant.Cum_TemperCount + "\r\n" + "Billing Period Count = " + instant.Billing_PeriodCount;
                        if (dbClass.checkDB_Conn())
                        {
                            dbClass.pushInstant(clientIP, wp.SysTitle, instant.Voltage, instant.Current, instant.NeutralCurrent, instant.PowerFactor, instant.Frequency,
                                         instant.App_Power, instant.Act_Power, instant.Cum_Kwh, instant.Cum_TemperCount, instant.Billing_PeriodCount);
                        }
                        break;
                    case 4:
                        INDEX = INDEX + 2;
                        lastGasp = false;
                        MeterEvents events = new MeterEvents();
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
                        i_string = "Client Info = " + senderinfo + "\r\n" + "Time = " + dt + "\r\n" + "Push Notification = " + wp.Push + "\r\n" + "Inwoke Id = " + wp.InvokeID + "\r\n" + "Date1 = " + wp.DateTime1 + "\r\n"
                       + "Serial Number = " + wp.SysTitle + "\r\n" + "PushSetup = " + wp.PushSetup + "\r\n" + "Date2 = " + wp.DateTime2 + "\r\n" + tempers;
                        if (dbClass.checkDB_Conn())
                        {
                            dbClass.pushConnect(clientIP, wp.SysTitle, tempers);
                        }
                        break;
                }
                #endregion
            }
            else
            {
                i_string = "RECIEVE << " + utility.ByteArrayToString(rec);
            }
            SetText(i_string); // rtbLogs.Text = "";
            if (lastGasp)
            {
                if (Program.dbStatus) { dbClass.setDisconnect(dataip); }
                for (int i = clientTable.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = clientTable.Rows[i];
                    if (dr["IPAddress"].ToString() == clientIP)
                        dr.Delete();
                }
                dataGridView1.Invoke(new Action(() => { dataGridView1.DataSource = clientTable; }));

                server.DisconnectClient(e.SenderInfo);
                SetText(" >> " + clientIP + " is disconnected due to Power Off " + DateTime.Now.ToString());

                //int pos = Program.addressList.IndexOf(e.SenderInfo);

               // Program.clientsList[pos].DisconnectClient(e.SenderInfo);
                //Program.clientsList[pos].Dispose();
                //Program.addressList.RemoveAt(pos);
                //Program.clientsList.RemoveAt(pos);
            }
        }

        private void Server_OnClientDisconnected(object sender, Gurux.Common.ConnectionEventArgs e)
        {
            try
            {               
                string ip = "";
                string portnum = "";
                if(ch_Ipv6.Checked==true)
                {
                    var disconnect = e.Info.Split(']');
                    ip = disconnect[0].Remove(0, 1);
                    portnum = disconnect[1].Remove(0, 1);
                }
                else
                {
                    var disconnect = e.Info.Split(':');
                    ip = disconnect[0];
                    portnum = disconnect[1];
                }                    
                 //server.DisconnectClient(ip);           
                for (int i = clientTable.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = clientTable.Rows[i];
                    if (dr["IPAddress"].ToString() == ip)
                        dr.Delete();
                }
                dataGridView1.Invoke(new Action(() => { dataGridView1.DataSource = clientTable; }));
                SetText(" >> " + ip + " is Disconnected " + DateTime.Now.ToString());
                if (Program.dbStatus) { dbClass.setDisconnect(ip); }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }

        private void Server_OnClientConnected(object sender, Gurux.Common.ConnectionEventArgs e)
        {
            try
            {
                cls = server;
                //gxl = cls.Attach(e.Info);
                //Program.clientsList.Add(gxl);
                //Program.addressList.Add(e.Info);

                //cls.OnReceived += Cls_OnReceived;
                //cls.OnClientDisconnected += Cls_OnClientDisconnected;

                //gxl = server.Attach(addresses);
                //gxl.OnReceived += Gxl_OnReceived1;
                //gxl.OnClientDisconnected += Gxl_OnClientDisconnected;

                var IP_Port = e.Info.Split(']');                
                if (ch_Ipv6.Checked==true)
                {
                    ipaddress = IP_Port[0].Remove(0, 1);
                    portnum = IP_Port[1].Remove(0, 1); 
                }
                else
                {                    
                    string[] info = IP_Port[0].Split(':');
                    ipaddress = info[0];
                    portnum = info[1];
                }
               
                clientIP = ipaddress;
                clientPort = Int32.Parse(portnum);
                current_date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");                
                string[] act = server.GetActiveClients();
                clientTable.Rows.Add("Connected", clientIP, clientPort, current_date);
                dataGridView1.Invoke(new Action(() => { dataGridView1.DataSource = clientTable; }));
                SetText(ipaddress + "Connected");
                if (dbClass.checkDB_Conn())
                {
                    dbClass.ConnectMeter(ipaddress);
                }
                //backgroundWorker1.RunWorkerAsync();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Gxl_OnReceived1(object sender, ReceiveEventArgs e)
        {
            int len = 0;
            int parameter_count = 0;
            string i_string = "";
            string senderinfo = e.SenderInfo;
            var disconnect = e.SenderInfo.Split(']');
            string dataip = disconnect[0].Remove(0, 1);
            byte[] rec = (byte[])e.Data;
            bool lastGasp = false;
            if (rec.Length == 100 || rec.Length == 131)
            {
                #region pushpacket
                byte[] ARR = gXDLMSTranslator.GetDataFromPushEncriptMessage(Security.Encryption, Encoding.ASCII.GetBytes("qwertyui"), cipher, cipher, function.getDataBuffer(rec));
                i_string = "";
                string dt = "";
                int INDEX = 0;

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
                        i_string = "Client Info = " + senderinfo + "\r\n" + "Time = " + dt + "\r\n" + "Push Notification = " + wp.Push + "\r\n" + "Inwoke Id = " + wp.InvokeID + "\r\n" + "Date1 = " + wp.DateTime1 + "\r\n" + "Serial Number = " + wp.SysTitle + "\r\n" +
                            "PushSetup = " + wp.PushSetup + "\r\n" + "Date2 = " + wp.DateTime2 + "\r\n" + "Push = " + wp.Push + "\r\n" + "Voltage = " + instant.Voltage + "\r\n" +
                            "Current = " + instant.Current + "\r\n" + "Neutral Current = " + instant.NeutralCurrent + "\r\n" + "Power Factor = " + instant.PowerFactor + "\r\n" +
                            "Frequency = " + instant.Frequency + "\r\n" + "Aparent Power Kva = " + instant.App_Power + "\r\n" + "Active Power Kw = " + instant.Act_Power + "\r\n" +
                            "Cum Kwh = " + instant.Cum_Kwh + "\r\n" + "Cum Temper Count = " + instant.Cum_TemperCount + "\r\n" + "Billing Period Count = " + instant.Billing_PeriodCount;
                        if (dbClass.checkDB_Conn())
                        {
                            dbClass.pushInstant(clientIP, wp.SysTitle, instant.Voltage, instant.Current, instant.NeutralCurrent, instant.PowerFactor, instant.Frequency,
                                         instant.App_Power, instant.Act_Power, instant.Cum_Kwh, instant.Cum_TemperCount, instant.Billing_PeriodCount);
                        }
                        break;
                    case 4:
                        INDEX = INDEX + 2;
                        lastGasp = false;
                        MeterEvents events = new MeterEvents();
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
                        i_string = "Client Info = " + senderinfo + "\r\n" + "Time = " + dt + "\r\n" + "Push Notification = " + wp.Push + "\r\n" + "Inwoke Id = " + wp.InvokeID + "\r\n" + "Date1 = " + wp.DateTime1 + "\r\n"
                       + "Serial Number = " + wp.SysTitle + "\r\n" + "PushSetup = " + wp.PushSetup + "\r\n" + "Date2 = " + wp.DateTime2 + "\r\n" + tempers;
                        if (dbClass.checkDB_Conn())
                        {
                            dbClass.pushConnect(clientIP, wp.SysTitle, tempers);
                        }
                        break;
                }
                #endregion
            }
            else
            {
                i_string = "RECIEVE << " + utility.ByteArrayToString(rec);
            }
            SetText(i_string); // rtbLogs.Text = "";
            if (lastGasp)
            {                
                if (Program.dbStatus) { dbClass.setDisconnect(dataip); }               
                for (int i = clientTable.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = clientTable.Rows[i];
                    if (dr["IPAddress"].ToString() == clientIP)
                        dr.Delete();
                }
                dataGridView1.Invoke(new Action(() => { dataGridView1.DataSource = clientTable; }));

                SetText(" >> " + clientIP + " is disconnected due to Power Off " + DateTime.Now.ToString());

                int pos = Program.addressList.IndexOf(e.SenderInfo);
                
                Program.clientsList[pos].DisconnectClient(e.SenderInfo);
                Program.clientsList[pos].Dispose();
                Program.addressList.RemoveAt(pos);
                Program.clientsList.RemoveAt(pos);
            }
        }

        private void Cls_OnClientDisconnected(object sender, ConnectionEventArgs e)
        {
            
        }

        private void Gxl_OnClientDisconnected(object sender, ConnectionEventArgs e)
        {
            try
            {
                string ip = "";
                string portnum = "";
                if (ch_Ipv6.Checked == true)
                {
                    var disconnect = e.Info.Split(']');
                    ip = disconnect[0].Remove(0, 1);
                    portnum = disconnect[1].Remove(0, 1);
                }
                else
                {
                    var disconnect = e.Info.Split(':');
                    ip = disconnect[0];
                    portnum = disconnect[1];
                }
                //gxl.DisconnectClient(ip);
                for (int i = clientTable.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = clientTable.Rows[i];
                    if (dr["IPAddress"].ToString() == ip)
                        dr.Delete();
                }
                dataGridView1.Invoke(new Action(() => { dataGridView1.DataSource = clientTable; }));
                SetText(" >> " + ip + " is Disconnected " + DateTime.Now.ToString());
                if (Program.dbStatus) { dbClass.setDisconnect(ip); }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Gxl_OnReceived(object sender, ReceiveEventArgs e)
        {
            int len = 0;
            int parameter_count = 0;
            string i_string = "";
            string senderinfo = e.SenderInfo;
            var disconnect = e.SenderInfo.Split(']');
            string dataip = disconnect[0].Remove(0, 1);
            byte[] rec = (byte[])e.Data;
            bool lastGasp = false;
            if (rec.Length == 100 || rec.Length == 131)
            {
                #region pushpacket
                byte[] ARR = gXDLMSTranslator.GetDataFromPushEncriptMessage(Security.Encryption, Encoding.ASCII.GetBytes("qwertyui"), cipher, cipher, function.getDataBuffer(rec));
                i_string = "";
                string dt = "";
                int INDEX = 0;

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
                        i_string = "Client Info = " + senderinfo + "\r\n" + "Time = " + dt + "\r\n" + "Push Notification = " + wp.Push + "\r\n" + "Inwoke Id = " + wp.InvokeID + "\r\n" + "Date1 = " + wp.DateTime1 + "\r\n" + "Serial Number = " + wp.SysTitle + "\r\n" +
                            "PushSetup = " + wp.PushSetup + "\r\n" + "Date2 = " + wp.DateTime2 + "\r\n" + "Push = " + wp.Push + "\r\n" + "Voltage = " + instant.Voltage + "\r\n" +
                            "Current = " + instant.Current + "\r\n" + "Neutral Current = " + instant.NeutralCurrent + "\r\n" + "Power Factor = " + instant.PowerFactor + "\r\n" +
                            "Frequency = " + instant.Frequency + "\r\n" + "Aparent Power Kva = " + instant.App_Power + "\r\n" + "Active Power Kw = " + instant.Act_Power + "\r\n" +
                            "Cum Kwh = " + instant.Cum_Kwh + "\r\n" + "Cum Temper Count = " + instant.Cum_TemperCount + "\r\n" + "Billing Period Count = " + instant.Billing_PeriodCount;
                        if (dbClass.checkDB_Conn())
                        {
                            dbClass.pushInstant(clientIP, wp.SysTitle, instant.Voltage, instant.Current, instant.NeutralCurrent, instant.PowerFactor, instant.Frequency,
                                         instant.App_Power, instant.Act_Power, instant.Cum_Kwh, instant.Cum_TemperCount, instant.Billing_PeriodCount);
                        }
                        break;
                    case 4:
                        INDEX = INDEX + 2;
                        lastGasp = false;
                        MeterEvents events = new MeterEvents();
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
                        i_string = "Client Info = " + senderinfo + "\r\n" + "Time = " + dt + "\r\n" + "Push Notification = " + wp.Push + "\r\n" + "Inwoke Id = " + wp.InvokeID + "\r\n" + "Date1 = " + wp.DateTime1 + "\r\n"
                       + "Serial Number = " + wp.SysTitle + "\r\n" + "PushSetup = " + wp.PushSetup + "\r\n" + "Date2 = " + wp.DateTime2 + "\r\n" + tempers;
                        if (dbClass.checkDB_Conn())
                        {
                            dbClass.pushConnect(clientIP, wp.SysTitle, tempers);
                        }
                        break;
                }
                #endregion
            }
            else
            {
                i_string = "RECIEVE << " + utility.ByteArrayToString(rec);
            }
            SetText(i_string); // rtbLogs.Text = "";
            if (lastGasp)
            {
                if (Program.dbStatus) { dbClass.setDisconnect(dataip); }
                server.DisconnectClient(clientIP);

                for (int i = clientTable.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = clientTable.Rows[i];
                    if (dr["IPAddress"].ToString() == clientIP)
                        dr.Delete();
                }
                dataGridView1.Invoke(new Action(() => { dataGridView1.DataSource = clientTable; }));
                SetText(" >> " + clientIP + " is disconnected due to Power Off " + DateTime.Now.ToString());
                int pos = Program.addressList.IndexOf(senderinfo);
                Program.addressList.RemoveAt(pos);
                Program.clientsList.RemoveAt(pos);
            }
        }

        private void SetText(string text)
        {
            try
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void disconnectClientToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            if(dataGridView1.CurrentRow!=null)
            {
                string dis_client = "";
                string selectedip = dataGridView1.CurrentRow.Cells[1].Value.ToString().Trim();
                int dport = Convert.ToInt32(dataGridView1.CurrentRow.Cells[2].Value);
                if (ch_Ipv6.Checked == true)
                { dis_client = "[" + selectedip + "]:" + dport; }
                else
                { dis_client =  selectedip + ":" + dport; }
                
                server.DisconnectClient(dis_client);
                for (int i = clientTable.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = clientTable.Rows[i];
                    if (dr["IPAddress"].ToString() == selectedip)
                        dr.Delete();
                }
                dataGridView1.Invoke(new Action(() => { dataGridView1.DataSource = clientTable; }));
                SetText(" >> " + selectedip + " is Disconnected " + DateTime.Now.ToString());
            }
            else
            {
                MessageBox.Show("No client selected to disconnect");
            }
        }

        private void clearLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbLogs.Invoke(new Action(() => { rtbLogs.Text = ""; }));
        }

        private void saveLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile1 = new SaveFileDialog();
            // Initialize the SaveFileDialog to specify the RTF extension for the file.
            saveFile1.DefaultExt = "*.txt";
            saveFile1.Filter = "Text Files|*.txt";

            // Determine if the user selected a file name from the saveFileDialog.
            if (saveFile1.ShowDialog() == DialogResult.OK && saveFile1.FileName.Length > 0)
            {
                // Save the contents of the RichTextBox into the file.
                rtbLogs.SaveFile(saveFile1.FileName, RichTextBoxStreamType.PlainText);
                MessageBox.Show("File Saved successfully", "Log File : " + saveFile1.FileName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void PushListener_Load(object sender, EventArgs e)
        {
            mediagp = new GXNet();
            gprs = mediagp as GXNet;
            gprs.Port = Convert.ToInt32(tb_port.Text.Trim());
            if (ch_Ipv6.Checked == true) { gprs.UseIPv6 = true; }
            else { gprs.UseIPv6 = false; }
            gprs.Protocol = NetworkType.Tcp;
            Initialize_HighMode();
        }
        public void Initialize_NoneMode()
        {
            dlmsClient.ServerAddress = 1;
            dlmsClient.ClientAddress = 16;
            dlmsClient.UseLogicalNameReferencing = true;
            dlmsClient.Authentication = Authentication.None;
            dlmsClient.Ciphering.Security = Security.None;
            dlmsClient.Ciphering.SystemTitle = Encoding.ASCII.GetBytes("qwertyui");
            dlmsClient.Ciphering.AuthenticationKey = new byte[] { 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62 };
            dlmsClient.Ciphering.BlockCipherKey = new byte[] { 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62 };
            dlmsClient.InterfaceType = InterfaceType.WRAPPER;
            dlmsClient.Password = ASCIIEncoding.ASCII.GetBytes("");
            Thread.Sleep(600);
        }

        public void Initialize_HighMode()
        {
            dlmsClient.ServerAddress = 1;
            dlmsClient.ClientAddress = 48;
            dlmsClient.UseLogicalNameReferencing = true;
            dlmsClient.Authentication = Authentication.High;
            dlmsClient.Ciphering.Security = Security.AuthenticationEncryption;
            dlmsClient.Ciphering.SystemTitle = Encoding.ASCII.GetBytes("qwertyui");
            dlmsClient.Ciphering.AuthenticationKey = new byte[] { 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62 };
            dlmsClient.Ciphering.BlockCipherKey = new byte[] { 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62 };
            dlmsClient.InterfaceType = InterfaceType.WRAPPER;
            dlmsClient.Password = ASCIIEncoding.ASCII.GetBytes("wwwwwwwwwwwwwwww");
            Thread.Sleep(600);
        }

        private void nameplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {                
                string selectedip = dataGridView1.CurrentRow.Cells[1].Value.ToString().Trim();
                int dport = Convert.ToInt32(dataGridView1.CurrentRow.Cells[2].Value);
                string clvalue = selectedip.Trim() + ":" + dport.ToString().Trim();
                int pos = Program.addressList.IndexOf(clvalue);
                download_Profile("Nameplate", selectedip, dport, false, "", Program.clientsList[pos]);
            }
            else
            {
                MessageBox.Show("No client selected to download");
            }
        }
        
        public void download_Profile(string prName, string ip, int selectedport, bool scalerprofile, string scalerobis,GXNet newGX)
        {
            resultnotify.Text = "|   Trying to download " + prName + ".................";
            ArrayList Values = null; ArrayList Obis = null;
            ArrayList ScalerValue = null; ArrayList ScalerObis = null;
            //newGX.
            //GXNet gxmedia = new GXNet();
            //gxmedia = newGX;    // cls;
            //cls.Dispose();
            //cls = newGX;
            GXPUSHReader reader = new GXPUSHReader(dlmsClient, newGX, System.Diagnostics.TraceLevel.Info);
            try
            {                
                Initialize_NoneMode();
                reader.SNRMRequest();
                reader.AarqRequest();
                string ic = reader.ReadObis(invoc_counter, 2).ToString();//reading invocation counter            
                dlmsClient.Ciphering.InvocationCounter = (uint)(Convert.ToInt32(ic) + 2);
                Initialize_HighMode();
                reader.InitializeConnection();
                string obValue = function.getObis(prName);
                int entriesInUse = Convert.ToInt32(reader.GetEntries(obValue, 7));
                if (entriesInUse >= 1)
                {
                    string serialNumber = reader.ReadObis(serial, 2).ToString();
                    Values = reader.ReadDlms(obValue, "Genric");
                    Obis = reader.ReadDlms(obValue, "Obis");
                    Program.totalRecords = Values.Count;
                    Program.pageSize = (Program.totalRecords / entriesInUse);
                    if (scalerprofile)
                    {

                        ScalerValue = reader.ReadDlms(scalerobis, "Scaler");  // reader.GetScalerValues(scalerobis);
                        ScalerObis = reader.ReadDlms(scalerobis, "Obis");    // reader.GetScalerObisCodes(scalerobis);
                    }

                    bool _success = function.decodeAllData(Obis, Values, ScalerValue, ScalerObis, entriesInUse);
                    if (_success)
                    {
                        bindingNavigator1.BindingSource = bindingSource1;
                        bindingSource1.CurrentItemChanged += new System.EventHandler(BindingSource1_CurrentItemChanged);
                        bindingSource1.DataSource = new PageOffsetList();
                        CreateFile(serialNumber, prName, entriesInUse.ToString());                     
                    }
                    else
                    {                       
                        update(" Something went wrong ");
                    }
                    resultnotify.Text = "| " + prName + " Downloaded Successfully";
                    update("############## SerialNumber = "+ serialNumber +"  ProfileName = "+ prName + " Download Successfull################");
                }
                else
                {
                    MessageBox.Show("No entries found  for this Profile");
                }

            }
            catch (Exception ex)
            {
               // mediagp.Close();
                resultnotify.Text = "| Download stopped due to error";
                MessageBox.Show(ex.Message);
            }
        }

        class PageOffsetList : System.ComponentModel.IListSource
        {
            public bool ContainsListCollection { get; protected set; }

            public System.Collections.IList GetList()

            {
                // Return a list of page offsets based on "totalRecords" and "pageSize"
                var pageOffsets = new List<int>();
                for (int offset = 0; offset < Program.totalRecords; offset += Program.pageSize)
                    pageOffsets.Add(offset);
                return pageOffsets;
            }
        }

        private void BindingSource1_CurrentItemChanged(object sender, EventArgs e)
        {
            // The desired page has changed, so fetch the page of records using the "Current" offset
            int offset = (int)bindingSource1.Current;
            var records = new List<Records2>();
            var rc = DlmsData.RecordList.ToArray();
            for (int i = offset; i < offset + Program.pageSize && i < Program.totalRecords; i++)
            {
                string[] arr = (string[])rc[i];
                records.Add(new Records2 { Name = arr[0], Value = arr[1], Unit = arr[2], Obis = arr[3] });
            }
            dataGridView2.DataSource = records;
        }

        private void CreateFile(string sl_num,string profileName,string entriesInUse)
        {
            Program.FileName = sl_num + "#" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + "$" + profileName + ".txt";
            string filePath = Program.Default_Target_Directory + "\\" + Program.FileName;
            WriteIntoFile(filePath, profileName + "|" + entriesInUse + "|" + Program.totalRecords);
            var rc = DlmsData.RecordList.ToArray();
            for (int i = 0; i < DlmsData.RecordList.Count; i++)
            {
                string[] arr = (string[])rc[i];
                WriteIntoFile(filePath, arr[0] + "|" + arr[1] + "|" + arr[2] + "|" + arr[3]);
            }
        }

        public void WriteIntoFile(string path, string data)
        {
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine(data);
                sw.Close();
            }
        }

        private void instantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {                
                string selectedip = dataGridView1.CurrentRow.Cells[1].Value.ToString().Trim();
                int dport = Convert.ToInt32(dataGridView1.CurrentRow.Cells[2].Value);
                string scalerobis = function.getObis("InstantScaler");
                string clvalue = selectedip.Trim() + ":" + dport.ToString().Trim();
                int pos = Program.addressList.IndexOf(clvalue);
                download_Profile("Instant", selectedip, 4059, true, scalerobis,Program.clientsList[pos]);
            }
            else
            {
                MessageBox.Show("No client selected to download");
            }
        }

        private void billingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {               
                string selectedip = dataGridView1.CurrentRow.Cells[1].Value.ToString().Trim();
                int dport = Convert.ToInt32(dataGridView1.CurrentRow.Cells[2].Value);
                string scalerobis = function.getObis("BillingScaler");
                string clvalue = selectedip.Trim() + ":" + dport.ToString().Trim();
                int pos =  Program.addressList.IndexOf(clvalue);
                download_Profile("Billing", selectedip, 4059, true, scalerobis, Program.clientsList[pos]);
            }
            else
            {
                MessageBox.Show("No client selected to download");
            }
        }

        class Records2
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public string Obis { get; set; }
            public string Unit { get; set; }
        }

        private void bindingSource1_CurrentItemChanged(object sender, EventArgs e)
        {
           
        }

        private void rtbLogs_TextChanged(object sender, EventArgs e)
        {
            rtbLogs.SelectionStart = rtbLogs.Text.Length;
            rtbLogs.ScrollToCaret();
        }

        private void selectMeterToDownloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                string clvalue = "";
                string selectedip = dataGridView1.CurrentRow.Cells[1].Value.ToString().Trim();
                int dport = Convert.ToInt32(dataGridView1.CurrentRow.Cells[2].Value);
                if (ch_Ipv6.Checked == true)
                    clvalue = "[" + selectedip.Trim() + "]:" + dport.ToString().Trim();
                else
                    clvalue = selectedip.Trim() + ":" + dport.ToString().Trim();

                GXNet grx = new GXNet();
                Program.serverMedia = true;
                if (Program.addressList.Contains(clvalue))
                {
                    int pos = Program.addressList.IndexOf(clvalue);
                    Program.serverGX = Program.clientsList[pos];
                }
                else
                {                    
                    grx = cls.Attach(clvalue);
                    Program.serverGX = grx;
                    Program.addressList.Add(clvalue);
                    Program.clientsList.Add(grx);
                    Program.serverGX = grx;
                }               
                grx.OnReceived += Grx_OnReceived;
                grx.OnClientDisconnected += Grx_OnClientDisconnected;
                //download_Profile("Nameplate", selectedip, dport, false, "", gxnew);
            }
            else
            {
                MessageBox.Show("No client selected to download");
            }
        }

        public void findClient()
        {

        }

        private void Grx_OnClientDisconnected(object sender, ConnectionEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void Grx_OnReceived(object sender, ReceiveEventArgs e)
        {
            int len = 0;
            int parameter_count = 0;
            string i_string = "";
            string senderinfo = e.SenderInfo;
            var disconnect = e.SenderInfo.Split(']');
            string dataip = disconnect[0].Remove(0, 1);
            byte[] rec = (byte[])e.Data;
            bool lastGasp = false;
            if (rec.Length == 100 || rec.Length == 131)
            {
                #region pushpacket
                byte[] ARR = gXDLMSTranslator.GetDataFromPushEncriptMessage(Security.Encryption, Encoding.ASCII.GetBytes("qwertyui"), cipher, cipher, function.getDataBuffer(rec));
                i_string = "";
                string dt = "";
                int INDEX = 0;

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
                        i_string = "Client Info = " + senderinfo + "\r\n" + "Time = " + dt + "\r\n" + "Push Notification = " + wp.Push + "\r\n" + "Inwoke Id = " + wp.InvokeID + "\r\n" + "Date1 = " + wp.DateTime1 + "\r\n" + "Serial Number = " + wp.SysTitle + "\r\n" +
                            "PushSetup = " + wp.PushSetup + "\r\n" + "Date2 = " + wp.DateTime2 + "\r\n" + "Push = " + wp.Push + "\r\n" + "Voltage = " + instant.Voltage + "\r\n" +
                            "Current = " + instant.Current + "\r\n" + "Neutral Current = " + instant.NeutralCurrent + "\r\n" + "Power Factor = " + instant.PowerFactor + "\r\n" +
                            "Frequency = " + instant.Frequency + "\r\n" + "Aparent Power Kva = " + instant.App_Power + "\r\n" + "Active Power Kw = " + instant.Act_Power + "\r\n" +
                            "Cum Kwh = " + instant.Cum_Kwh + "\r\n" + "Cum Temper Count = " + instant.Cum_TemperCount + "\r\n" + "Billing Period Count = " + instant.Billing_PeriodCount;
                        if (dbClass.checkDB_Conn())
                        {
                            dbClass.pushInstant(clientIP, wp.SysTitle, instant.Voltage, instant.Current, instant.NeutralCurrent, instant.PowerFactor, instant.Frequency,
                                         instant.App_Power, instant.Act_Power, instant.Cum_Kwh, instant.Cum_TemperCount, instant.Billing_PeriodCount);
                        }
                        break;
                    case 4:
                        INDEX = INDEX + 2;
                        lastGasp = false;
                        MeterEvents events = new MeterEvents();
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
                        i_string = "Client Info = " + senderinfo + "\r\n" + "Time = " + dt + "\r\n" + "Push Notification = " + wp.Push + "\r\n" + "Inwoke Id = " + wp.InvokeID + "\r\n" + "Date1 = " + wp.DateTime1 + "\r\n"
                       + "Serial Number = " + wp.SysTitle + "\r\n" + "PushSetup = " + wp.PushSetup + "\r\n" + "Date2 = " + wp.DateTime2 + "\r\n" + tempers;
                        if (dbClass.checkDB_Conn())
                        {
                            dbClass.pushConnect(clientIP, wp.SysTitle, tempers);
                        }
                        break;
                }
                #endregion
            }
            else
            {
                i_string = "RECIEVE << " + utility.ByteArrayToString(rec);
            }
            SetText(i_string); // rtbLogs.Text = "";
            if (lastGasp)
            {
                if (Program.dbStatus) { dbClass.setDisconnect(dataip); }
                server.DisconnectClient(clientIP);

                for (int i = clientTable.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = clientTable.Rows[i];
                    if (dr["IPAddress"].ToString() == clientIP)
                        dr.Delete();
                }
                dataGridView1.Invoke(new Action(() => { dataGridView1.DataSource = clientTable; }));

                SetText(" >> " + clientIP + " is disconnected due to Power Off " + DateTime.Now.ToString());
            }
        }

        private void Gxnew_OnClientDisconnected(object sender, ConnectionEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void disconnectMeterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    string dis_client = "";
                    string selectedip = dataGridView1.CurrentRow.Cells[1].Value.ToString().Trim();
                    int dport = Convert.ToInt32(dataGridView1.CurrentRow.Cells[2].Value);
                    if (ch_Ipv6.Checked == true)
                    { dis_client = "[" + selectedip + "]:" + dport; }
                    else
                    {
                        dis_client = selectedip + ":" + dport;
                    }                               
                    for (int i = clientTable.Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow dr = clientTable.Rows[i];
                        if (dr["IPAddress"].ToString() == selectedip)
                            dr.Delete();
                    }
                    dataGridView1.Invoke(new Action(() => { dataGridView1.DataSource = clientTable; }));
                    
                    int pos = Program.addressList.IndexOf(dis_client);
                    if (pos >= 0)
                    {
                        Program.clientsList[pos].DisconnectClient(dis_client);
                        Program.clientsList[pos].Dispose();
                        Program.addressList.RemoveAt(pos);
                        Program.clientsList.RemoveAt(pos);
                        SetText(" >> " + selectedip + " is Disconnected " + DateTime.Now.ToString());
                    }
                    else
                    {
                        server.DisconnectClient(dis_client);
                        cls.DisconnectClient(dis_client);                        
                    }
                }
                else
                {
                    MessageBox.Show("No client selected to disconnect");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }           
        }

        //public void richText()
        //{
        //    try
        //    {
        //        rtbLogs.Invoke(new Action(() => { rtbLogs.SelectionStart = rtbLogs.Text.Length; }));
        //        rtbLogs.Invoke(new Action(() => { rtbLogs.ScrollToCaret(); }));
        //    }           
        //     catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void btn_StopServer_Click(object sender, EventArgs e)
        {
            this.Invoke(new MethodInvoker(delegate
            {              
                server.DisconnectClient(ipaddress);
                server.Close();
                server.Dispose();               
                if (!server.IsOpen) { serverNotify.Text = "Server is Off"; }
                btn_startServer.Enabled = true;
                ch_Ipv6.Enabled = true;
                btn_StopServer.Enabled = false;
            }));
        }
    }
}
