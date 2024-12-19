using Gurux.DLMS.Enums;
using Gurux.DLMS.Objects;
using Gurux.Net;
using System;

namespace Gurux_Testing
{
    class MediaSettings
    {
        public GXDLMSReader reader = null;
        DlmsSettings dlmsSettings;
        //public static IGXMedia media = new GXSerial();
        //public static IGXMedia mediagp = new GXNet();
        //public static GXSerial serial = null;
        //public static GXNet gprs = null;
        //public static TraceLevel trace = TraceLevel.Info;
        //public static bool iec = false;
        //public static GXDLMSSecureClient _client = new GXDLMSSecureClient(true);
        public MediaSettings(ConnectionSettings connectionSettings)
        {
            dlmsSettings = new DlmsSettings(connectionSettings);
            //_client.ServerAddress = 1;
            //_client.UseLogicalNameReferencing = true;
            //_client.Ciphering.SystemTitle = ASCIIEncoding.ASCII.GetBytes("qwertyui");
            //_client.Ciphering.BlockCipherKey = ASCIIEncoding.ASCII.GetBytes("bbbbbbbbbbbbbbbb");
            //_client.Ciphering.AuthenticationKey = ASCIIEncoding.ASCII.GetBytes("bbbbbbbbbbbbbbbb");
            //_client.Ciphering.InvocationCounter = 0;
            ////_client.ServerSystemTitle = new byte[] { 0x45, 0x4C, 0x53, 0x30, 0x30, 0x30, 0x31, 0x30 };
            //initializeMedia();
        }
        //public static void initializeMedia()
        //{
        //    //media = new GXSerial();
        //    //serial = media as GXSerial;
        //    //serial.BaudRate = 9600;
        //    //serial.DataBits = 8;
        //    //serial.Parity = System.IO.Ports.Parity.None;
        //    //serial.StopBits = System.IO.Ports.StopBits.One;
        //    ////**********************************************//
        //    //mediagp = new GXNet();
        //    //gprs = mediagp as GXNet;
        //    //gprs.Port = 4059;// Int32.Parse(tb_portname.Text);
        //    //gprs.UseIPv6 = true;
        //    //gprs.Protocol = NetworkType.Tcp;
        //}
        //public static void initializeClient(string auType)
        //{  
        //switch(auType)
        //{
        //    case "None":
        //        _client.Ciphering.InvocationCounter = 0;
        //        _client.ClientAddress = 16;
        //        _client.Authentication = Authentication.None;
        //        _client.Ciphering.Security = Security.None;                   
        //        break;
        //    case "Low":
        //        _client.Ciphering.InvocationCounter = 0;
        //        _client.ClientAddress = 32;
        //        _client.Authentication = Authentication.Low;
        //        _client.Ciphering.Security = Security.Encryption;                              
        //        _client.Password = ASCIIEncoding.ASCII.GetBytes("123456");
        //        break;
        //    case "High":
        //        _client.Ciphering.InvocationCounter = 0;
        //        _client.ClientAddress = 48;
        //        _client.Authentication = Authentication.High;
        //        _client.Ciphering.Security = Security.AuthenticationEncryption;                   
        //        _client.Password = ASCIIEncoding.ASCII.GetBytes("wwwwwwwwwwwwwwww");
        //        break;
        //}
        //}
        public static void setMedia(string type, string xp, string bd, bool ipv)
        {
            try
            {
                //media.close          
                switch (type)
                {
                    case "Serial":
                        InitializeMedia.serial.DataBits = 8;
                        InitializeMedia.serial.Parity = System.IO.Ports.Parity.None;
                        InitializeMedia.serial.StopBits = System.IO.Ports.StopBits.One;
                        InitializeMedia.serial.PortName = xp;
                        InitializeMedia.serial.BaudRate = int.Parse(bd);
                        InitializeMedia._client.InterfaceType = InterfaceType.HDLC;
                        break;
                    case "Gprs":
                        InitializeMedia.gprs.Port = 4059;
                        InitializeMedia.gprs.Protocol = NetworkType.Tcp;
                        InitializeMedia.gprs.UseIPv6 = ipv;
                        InitializeMedia.gprs.HostName = xp;
                        InitializeMedia._client.InterfaceType = InterfaceType.WRAPPER;
                        break;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        public bool pushConnect()
        {
            bool status = false;
            try
            {
                if (!InitializeMedia.mediagp.IsOpen)
                {
                    InitializeMedia.mediagp.Open();
                    reader = new GXDLMSReader(InitializeMedia._client, InitializeMedia.mediagp, InitializeMedia.trace);
                    status = reader.InitializeConnection();
                }
                return status;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return status;
            }

        }

        public bool connect2()
        {
            try
            {
                if (!InitializeMedia.media.IsOpen)
                {
                    InitializeMedia.media.Open();
                    reader = new GXDLMSReader(InitializeMedia._client, InitializeMedia.media, InitializeMedia.trace);
                }
                else
                {
                    reader = new GXDLMSReader(InitializeMedia._client, InitializeMedia.media, InitializeMedia.trace);
                }
                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return false;
            }
        }

        public void closeMedia()
        {
            switch (Program.Mediatype)
            {
                case "G P R S":
                    if (InitializeMedia.mediagp.IsOpen)
                    {
                        InitializeMedia.mediagp.Close();
                    }
                    break;
                case "S E R I A L":
                    if (InitializeMedia.media.IsOpen)
                    {
                        InitializeMedia.media.Close();
                    }
                    break;
            }
        }

        public void Set_InvocationCounter(string ob)
        {
            GXDLMSData counter = new GXDLMSData(ob);
            switch (Program.Mediatype)
            {
                case "G P R S":

                    if (Program._connected == true)
                    {
                        if (!InitializeMedia.mediagp.IsOpen)
                            InitializeMedia.mediagp.Open();
                        reader.Disconnect();
                    }
                    dlmsSettings.initializeClient("None");
                    if (Program.serverMedia == true)
                    {
                        reader = new GXDLMSReader(InitializeMedia._client, Program.serverGX, InitializeMedia.trace);
                        reader.SNRMRequest();
                        reader.AarqRequest();
                        string ic1 = reader.ReadObis(counter, 2).ToString();
                        Program.iCounter = Convert.ToInt32(ic1);
                        InitializeMedia._client.Ciphering.InvocationCounter = (uint)(Program.iCounter);
                    }
                    else
                    {
                        if (!InitializeMedia.mediagp.IsOpen)
                            InitializeMedia.mediagp.Open();
                        reader = new GXDLMSReader(InitializeMedia._client, InitializeMedia.mediagp, InitializeMedia.trace);

                        reader.SNRMRequest();
                        reader.AarqRequest();
                        string ic1 = reader.ReadObis(counter, 2).ToString();
                        Program.iCounter = Convert.ToInt32(ic1);
                        InitializeMedia._client.Ciphering.InvocationCounter = (uint)(Program.iCounter);
                        reader.Disconnect();
                    }
                    break;
                case "S E R I A L":
                    if (Program._connected == true)
                    {
                        if (!InitializeMedia.media.IsOpen)
                            InitializeMedia.media.Open();
                        reader.Disconnect();
                    }
                    dlmsSettings.initializeClient("None");
                    if (!InitializeMedia.media.IsOpen)
                        InitializeMedia.media.Open();

                    reader = new GXDLMSReader(InitializeMedia._client, InitializeMedia.media, InitializeMedia.trace);

                    reader.SNRMRequest();
                    reader.AarqRequest();
                    string ic = reader.ReadObis(counter, 2).ToString();
                    Program.iCounter = Convert.ToInt32(ic);
                    InitializeMedia._client.Ciphering.InvocationCounter = (uint)(Program.iCounter);
                    reader.Disconnect();
                    break;
            }
        }

        public bool connct4Firmware()
        {
            bool status = false;
            switch (Program.Mediatype)
            {
                case "G P R S":
                    dlmsSettings.initializeClient("Firmware");
                    if (Program.serverMedia)
                    {
                        reader = new GXDLMSReader(InitializeMedia._client, Program.serverGX, InitializeMedia.trace);
                        reader.WaitTime = 60000;
                        status = reader.InitializeConnection();
                    }
                    else
                    {
                        InitializeMedia.mediagp.Open();
                        reader = new GXDLMSReader(InitializeMedia._client, InitializeMedia.mediagp, InitializeMedia.trace);
                        reader.WaitTime = 60000;
                        status = reader.InitializeConnection();
                        if (!status) { InitializeMedia.mediagp.Close(); }
                    }
                    break;
                case "S E R I A L":
                    //Set_InvocationCounter("0.0.43.1.5.255");
                    dlmsSettings.initializeClient("Firmware");
                    InitializeMedia.media.Open();
                    reader = new GXDLMSReader(InitializeMedia._client, InitializeMedia.media, InitializeMedia.trace);
                    reader.WaitTime = 60000;
                    status = reader.InitializeConnection();
                    if (!status) { InitializeMedia.media.Close(); }
                    break;
            }
            Program._connected = status;
            return status;
        }
        public void dissconnect4Firmware()
        {
            if (Program._connected == true)
            {
                reader.Disconnect();
                //dlmsSettings.initializeClient(Program.selectedAuth);
                //InitializeMedia.media.Open();
                reader = new GXDLMSReader(InitializeMedia._client, InitializeMedia.media, InitializeMedia.trace);
            }
        }

        public bool connectServer()
        {
            // bool status = false;
            try
            {
                reader = new GXDLMSReader(InitializeMedia._client, Program.serverGX, InitializeMedia.trace);
                dlmsSettings.initializeClient("None");
                reader.SNRMRequest();
                reader.AarqRequest();
                dlmsSettings.initializeClient(Program.selectedAuth);
                reader.InitializeConnection();
                return true;
            }
            catch (Exception ex)
            {
                string ar = ex.ToString();
                return false;
            }
        }

        public bool connect()
        {
            bool status = false;
            GXDLMSData counter = new GXDLMSData("0.0.43.1.3.255");
            switch (Program.Mediatype)
            {
                case "G P R S":
                    //update("Trying to communicate using Gprs...");
                    if (Program.serverMedia)
                    {
                        reader = new GXDLMSReader(InitializeMedia._client, Program.serverGX, InitializeMedia.trace);
                        if (Program.counter_status == false && Program.fUpdate == false)
                        {
                            Console.WriteLine("Openning Channel");
                            dlmsSettings.initializeClient("None");
                            Console.WriteLine("Done!");
                            Console.WriteLine("Sending SNRMR Request");
                            reader.SNRMRequest();
                            Console.WriteLine("Sending AARQ Request");
                            reader.AarqRequest();
                            string ic = reader.ReadObis(counter, 2).ToString();
                            Program.iCounter = Convert.ToInt32(ic);
                            InitializeMedia._client.Ciphering.InvocationCounter = (uint)(Program.iCounter);
                            dlmsSettings.initializeClient(Program.selectedAuth);
                        }
                        status = reader.InitializeConnection();
                    }
                    else
                    {
                        if (!InitializeMedia.mediagp.IsOpen)
                        {
                            InitializeMedia.mediagp.Open();
                            reader = new GXDLMSReader(InitializeMedia._client, InitializeMedia.mediagp, InitializeMedia.trace);
                            if (Program.counter_status == false && Program.fUpdate == false)
                            {
                                dlmsSettings.initializeClient("None");
                                reader.SNRMRequest();
                                reader.AarqRequest();
                                string ic = reader.ReadObis(counter, 2).ToString();
                                Program.iCounter = Convert.ToInt32(ic);
                                InitializeMedia._client.Ciphering.InvocationCounter = (uint)(Program.iCounter);
                                reader.Disconnect();
                                Program.counter_status = true;
                                dlmsSettings.initializeClient(Program.selectedAuth);
                                status = true;
                            }
                            InitializeMedia.mediagp.Open();
                            reader = new GXDLMSReader(InitializeMedia._client, InitializeMedia.mediagp, InitializeMedia.trace);
                            status = reader.InitializeConnection();
                            string ic1 = reader.ReadObis(counter, 2).ToString();
                            Program.iCounter = Convert.ToInt32(ic1);
                        }
                        else
                        {
                            reader = new GXDLMSReader(InitializeMedia._client, InitializeMedia.mediagp, InitializeMedia.trace);
                            status = reader.InitializeConnection();
                            //MessageBox.Show("Media is closed"); return false;
                        }
                    }
                    break;
                case "S E R I A L":
                    if (Program.counter_status == false || Program.fUpdate == true)
                    {
                        Program.counter_status = true;
                        Program.fUpdate = false;
                        //Set_InvocationCounter("0.0.43.1.3.255");
                        dlmsSettings.initializeClient(Program.selectedAuth);
                        InitializeMedia.media.Open();
                        reader = new GXDLMSReader(InitializeMedia._client, InitializeMedia.media, InitializeMedia.trace);
                        status = reader.InitializeConnection();
                    }
                    else
                    {
                        dlmsSettings.initializeClient(Program.selectedAuth);
                        InitializeMedia.media.Open();
                        reader = new GXDLMSReader(InitializeMedia._client, InitializeMedia.media, InitializeMedia.trace);
                        status = reader.InitializeConnection();
                        //string ic = reader.ReadObis(counter, 2).ToString();
                        //Program.iCounter = Convert.ToInt32(ic);
                    }
                    break;
            }
            if (!status) { InitializeMedia.media.Close(); }
            Program._connected = status;
            return status;
        }

        public bool connectTemp()
        {
            //_client.Ciphering.InvocationCounter = 0;
            bool status = false;
            GXDLMSData counter = new GXDLMSData("0.0.43.1.3.255");//invocation counter
            switch (Program.Mediatype)
            {
                case "G P R S":
                    //update("Trying to communicate using Gprs...");
                    if (!InitializeMedia.mediagp.IsOpen)
                    {
                        InitializeMedia.mediagp.Open();
                        reader = new GXDLMSReader(InitializeMedia._client, InitializeMedia.mediagp, InitializeMedia.trace);
                        if (Program.counter_status == false && Program.fUpdate == false)
                        {
                            dlmsSettings.initializeClient("None");
                            reader.SNRMRequest();
                            reader.AarqRequest();
                            string ic = reader.ReadObis(counter, 2).ToString();
                            Program.iCounter = Convert.ToInt32(ic);
                            InitializeMedia._client.Ciphering.InvocationCounter = (uint)(Program.iCounter);
                            reader.Disconnect();
                            Program.counter_status = true;
                            dlmsSettings.initializeClient(Program.selectedAuth);
                            status = true;
                        }
                        //InitializeMedia.mediagp.Open();
                        //reader = new GXDLMSReader(InitializeMedia._client, InitializeMedia.mediagp, InitializeMedia.trace);
                        status = reader.InitializeConnection();
                        string ic1 = reader.ReadObis(counter, 2).ToString();
                        Program.iCounter = Convert.ToInt32(ic1);
                    }
                    else
                    {
                        reader = new GXDLMSReader(InitializeMedia._client, InitializeMedia.mediagp, InitializeMedia.trace);
                        status = reader.InitializeConnection();
                        //MessageBox.Show("Media is closed"); return false;
                    }
                    break;
                case "S E R I A L":
                    //update("Trying to communicate using Serialport...");





                    InitializeMedia.media.Close();
                    if (!InitializeMedia.media.IsOpen)
                    {
                        InitializeMedia.media.Open();
                        reader = new GXDLMSReader(InitializeMedia._client, InitializeMedia.media, InitializeMedia.trace);
                        if (Program.counter_status == false || Program.fUpdate == true)
                        {
                            Program.fUpdate = false;
                            Set_InvocationCounter("0.0.43.1.3.255");
                            Program.counter_status = true;
                            dlmsSettings.initializeClient(Program.selectedAuth);
                            status = true;
                        }
                        if (Program.selectedAuth == "None")
                        {
                            reader.SNRMRequest();
                            reader.AarqRequest();
                            string ic = reader.ReadObis(counter, 2).ToString();
                            Program.iCounter = Convert.ToInt32(ic);
                            InitializeMedia._client.Ciphering.InvocationCounter = (uint)(Program.iCounter);
                            reader.Disconnect();
                            status = true;
                        }
                        else
                        {
                            InitializeMedia.media.Open();
                            reader = new GXDLMSReader(InitializeMedia._client, InitializeMedia.media, InitializeMedia.trace);
                            status = reader.InitializeConnection();
                            string ic = reader.ReadObis(counter, 2).ToString();
                            Program.iCounter = Convert.ToInt32(ic);
                        }
                    }
                    else
                    {
                        reader = new GXDLMSReader(InitializeMedia._client, InitializeMedia.media, InitializeMedia.trace);
                        status = reader.InitializeConnection();
                        string ic = reader.ReadObis(counter, 2).ToString();
                        Program.iCounter = Convert.ToInt32(ic);
                    }
                    break;
            }
            if (!status) { InitializeMedia.media.Close(); }
            Program._connected = status;
            return status;
        }
    }
}
