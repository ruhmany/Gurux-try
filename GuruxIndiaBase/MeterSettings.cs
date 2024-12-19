using Gurux.DLMS.Objects;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;

namespace Gurux_Testing
{
    public class MeterSettings
    {
        Utility utility = new Utility();
        MediaSettings _media = new MediaSettings();
        Function function = new Function();
        string displayMode = null;
        byte[] FirmwareBytes = null;
        string browserFileName = "";
        public byte[] unlock = new byte[] { 0x45, 0x6C, 0x53, 0x75, 0x6E, 0x6C, 0x6F, 0x63, 0x6B, 0x0D };
        public byte[] kwhSet = new byte[] { 0x45, 0x6C, 0x53, 0x63, 0x6B, 0x77, 0x68, 0x00, 0x00, 0x00, 0x00, 0x0D };
        public byte[] voltSet = new byte[] { 0x45, 0x6C, 0x53, 0x76, 0x6F, 0x6C, 0x74, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0D };
        public byte[] curSet = new byte[] { 0x45, 0x6C, 0x53, 0x63, 0x75, 0x72, 0x72, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0D };
        public byte[] relSet = new byte[] { 0x45, 0x6C, 0x53, 0x63, 0x72, 0x65, 0x6C, 0x00, 0x00, 0x0D };
        public byte[] locktimeSet = new byte[] { 0x45, 0x6C, 0x53, 0x63, 0x6C, 0x6F, 0x6B, 0x00, 0x00, 0x00, 0x00, 0x0D };
        public byte[] readSettings = new byte[] { 0x45, 0x6C, 0x53, 0x67, 0x65, 0x74, 0x70, 0x00, 0x00, 0x00, 0x3C, 0x0D };
        public byte[] relConnect = new byte[] { 0x52, 0x45, 0x4C, 0x31, 0x0D };
        public byte[] relDisconnect = new byte[] { 0x52, 0x45, 0x4C, 0x32, 0x0D };
        public byte[] enablemode = new byte[] { 0x45, 0x6C, 0x53, 0x73, 0x65, 0x74, 0x31, 0x0D };
        public byte[] cl_coveropen = new byte[] { 0x45, 0x6C, 0x53, 0x63, 0x63, 0x6C, 0x72, 0x0D };//45 6C 53 63 63 6C 72 0D
        public byte[] terminalcover = new byte[] { 0x45, 0x6C, 0x53, 0x73, 0x65, 0x74, 0x37, 0x00, 0x00, 0x00, 0x00, 0x0D };
        public byte[] disablemode = new byte[] { 0x45, 0x6C, 0x53, 0x73, 0x65, 0x74, 0x00, 0x0D };
        public byte[] enable_disableEnergy = new byte[] { 0x45, 0x6C, 0x53, 0x73, 0x65, 0x74, 0x39, 0x0D };
        public byte[] pfail = new byte[] { 0x45, 0x6C, 0x53, 0x73, 0x65, 0x74, 0x32, 0x00, 0x00, 0x0D };
        public byte[] display_time = new byte[] { 0x45, 0x6C, 0x53, 0x73, 0x65, 0x74, 0x35, 0x05, 0x0A, 0x13, 0x0D };

        public string[] DisplayItemsArr1PH = new string[] { "DISP_ALL_ON","DISP_PF(13.7.0)","DISP_DATE(0.9.2)","DISP_TIME(0.9.1)","DISP_FREQ(14.7.0)","DISP_VRMS(32.7.0)","DISP_IRMS(31.7.0)","DISP_ANGLE(81.7.4)", "DISP_IRMS_P(31.7.0)",
             "DISP_IRMS_N(51.7.0)", "DISP_POWER_ACT(16.7.0)", "DISP_POWER_APP(9.7.0)", "DISP_MD_ACT(1.6.0)", "DISP_MD_APP(9.6.0)", "DISP_ENERGY_ACTI_IMP(1.8.0)", "DISP_ENERGY_ACTI_EXP(2.8.0)", "DISP_ENERGY_ACTI_ABS(15.8.0)",
             "DISP_ENERGY_ACTI_PH(35.8.0)", "DISP_ENERGY_ACTI_NUT(55.8.0)", "DISP_ENERGY_ACTI_P_N_DIFF(C.53.1)", "DISP_POWER_REACT(3.7.0)", "DISP_ENERGY_ACTI(15.8.0)", "DISP_ENERGY_APPI(9.8.0)", "TOTAL_REACTIVE_ENERGY(3.8.0)",
             "REACTIVE_ENERGY_IMPORT(5.8.0)","REACTIVE_ENERGY_EXPORT(8.8.0)","DISP_TAMPER_COUNT", "DISP_TOU_REGS_kWH(1.8.1-2-3-4)", "DISP_TOU_REGS_kVAH(9.8.1-2-3-4)", "DISP_TOU_REGS_kW(1.6.1-2-3-4)", "DISP_TOU_REGS_kVA(9.6.1.2-3-4)",
             "DISP_SIGNAL_STATUS(S)", "DISP_BP1_ENERGY_ACTI(15.8.0)", "DISP_BP1_ENERGY_APPI(9.8.0)", "DISP_BPn_MD_ACT(1.6.0(1-12))", "DISP_BPn_MD_APP(9.6.0(1-12))", "DISP_BPn_ENERGY_ACTI(15.8.0(1-12))", "DISP_BP1_AVG_PF(13.0.0)",
             "DISP_BP1_MD_ACT(1.6.0)", "DISP_BP1_MD_APP(9.6.0)", "DISP_MD_ACT_TIME(1.6.0 with icon t)", "DISP_MD_ACT_DATE(1.6.0 with icon d)", "DISP_MD_APP_TIME(9.6.0 with icon t)", "DISP_MD_APP_DATE(9.6.0 with icon d)", "DISP_BP1_MD_ACT_TIME(1.6.0 with icon t & BP)", "DISP_BP1_MD_ACT_DATE(1.6.0 with icon d & BP)",
             "DISP_BP1_MD_APP_TIME(9.6.0 with icon t & BP)", "DISP_BP1_MD_APP_DATE(9.6.0 with icon d & BP)", "DISP_MD_RST_CNT(rC)", "DISP_MD_RST_DATE(rSt with BP& MD icon)", "DISP_MD_RST_TIME(rSt with BP& MD icon)", "DISP_METER_SNO(S icon)",
             "DISP_TAMPER_COVER_OPEN_COUNT(C.51.3)",
             "DISP_TAMPER_COPEN_OCCUR_DATE",
             "DISP_TAMPER_COPEN_OCCUR_TIME(C.51.4)",
             "DISP_TAMPER_TOPEN_COUNT(C.51.1)",
             "DISP_TAMPER_TOPEN_OCCUR_TIME(C.51.2)",
             "DISP_CMD(1.2.0)",
             "DISP_ENERGY_ACTI_HR",
             "DISP_ENERGY_APPI_HR",
             "DISP_ENERGY_REACTI_ACTI_HR",
             "DISP_ENERGY_REACTE_ACTI_HR",
              };
        public string[] tempDisplay = new string[] { "DISP_ALL_ON",
  "DISP_PF",
  "DISP_DATE",
  "DISP_TIME",
  "DISP_FREQ",
  "DISP_VRMS",
  "DISP_IRMS",
  "DISP_IRMS_P",
  "DISP_IRMS_N",
  "DISP_POWER_ACT",
  "DISP_POWER_APP",
  "DISP_MD_ACT",
  "DISP_MD_APP",
  "DISP_POWER_REACT",
  "DISP_ENERGY_ACTI",
  "DISP_ENERGY_APPI",
  "DISP_ENERGY_REACTI_ACTI",
  "DISP_ENERGY_REACTE_ACTI",
  "DISP_RISING_DEM_ACT",
  "DISP_RISING_DEM_APP",
  "DISP_RISING_DEM_TIME",
  "DISP_CUM_AVG_PF",
  "DISP_AVG_PF_BP0",
  "DISP_CP_OFFHRS",
  "DISP_CP_OFFHRS_BP0",
  "DISP_BP1_AVG_PF",
  "DISP_BP2_AVG_PF",
  "DISP_BP1_MD_ACT",
  "DISP_BP1_MD_APP",
  "DISP_BP2_MD_ACT",
  "DISP_BP2_MD_APP",
  "DISP_BP1_ENERGY_ACTI",
  "DISP_BP1_ENERGY_APPI",
  "DISP_BP2_ENERGY_ACTI",
  "DISP_BP2_ENERGY_APPI",
  "DISP_MD_ACT_TIME",
  "DISP_MD_ACT_DATE",
  "DISP_MD_APP_TIME",
  "DISP_MD_APP_DATE",
  "DISP_BP1_MD_ACT_TIME",
  "DISP_BP1_MD_ACT_DATE",
  "DISP_BP1_MD_APP_TIME",
  "DISP_BP1_MD_APP_DATE",
  "DISP_BP2_MD_ACT_TIME",
  "DISP_BP2_MD_ACT_DATE",
  "DISP_BP2_MD_APP_TIME",
  "DISP_BP2_MD_APP_DATE",
  "DISP_BPn_ENERGY_ACTI",
  "DISP_LOADLIMIT",
  "DISP_BP1_OFF_HRS",
  "DISP_NS",
  "DISP_CMD",
  "DISP_MD_RST_CNT",
  "DISP_MD_RST_DATE",
  "DISP_MD_RST_TIME",
  "DISP_TOU_REGS",
  "DISP_METER_SNO",
  "DISP_METER_REVNO",
  "DISP_TAMPER_COPEN",
  "DISP_TAMPER_COPEN_OCCUR_DATE",
  "DISP_TAMPER_COPEN_OCCUR_TIME",
  "DISP_TAMPER",
  "DISP_ENERGY_ACTI_HR",
  "DISP_ENERGY_APPI_HR",
  "DISP_ENERGY_REACTI_ACTI_HR",
  "DISP_ENERGY_REACTE_ACTI_HR",
  "DISP_TOU_REGS_kWh",
  "DISP_TOU_REGS_kVAh",
  "DISP_TOU_REGS_kW",
  "DISP_TOU_REGS_kVA",
  "DISP_BP1_TOU_REGS_kW",
  "DISP_BP1_TOU_REGS_kVA",
  "DISP_TAMPER_COUNT",
  "DISP_BATT_STATUS",
  "DISP_EEPROM_STATUS",
  "DISP_SIGNAL_STATUS",
  "DISP_RTC_STATUS",
  "DISP_CP_ONHRS",
  "DISP_ENERGY_ACTI_EXP",
  "DISP_BPn_MD_APP",
  "DISP_BPn_MD_ACT",
  "DISP_ENERGY_ACTI_IMP",
  "DISP_ENERGY_REACT",
  "DISP_ANGLE",
  "nDISPLAYS"};

        public MeterSettings()
        {
            InitializeComponent();
            notification.Text = "DlmsClient";
            _mediaSettings = this;
        }
        public static MeterSettings _mediaSettings;
        public void update(string message)
        {
            if (InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    richTextBox2.AppendText(message + "\n");
                }));
            }
            else
            {
                richTextBox2.AppendText(message + "\n");
            }
        }

        private void rb_auto_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void rb_manual_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void btn_writeRTC_Click(object sender, EventArgs e)
        {
            if (Program.selectedAuth != "High")
            { notification.Text = "RTC cannot be set in " + Program.selectedAuth + " authentication mode"; }
            else
            {
                try
                {
                    notification.Text = "";
                    if (_media.connect())
                    {
                        string RTC = dateTimePicker1.Text;
                        string year = "20" + RTC.Substring(6, 2);
                        string mon = RTC.Substring(3, 2);
                        string day = RTC.Substring(0, 2);
                        string hour = RTC.Substring(9, 2);
                        string min = RTC.Substring(12, 2);
                        string sec = RTC.Substring(15, 2);
                        GXDLMSClock clock = new GXDLMSClock();
                        GXDateTime date1 = new GXDateTime(Convert.ToInt32(year), Convert.ToInt32(mon), Convert.ToInt32(day), Convert.ToInt32(hour), Convert.ToInt32(min), Convert.ToInt32(sec), 0);
                        clock.Time = date1;
                        _media.reader.Write(clock, 2);
                        notification.Text = "RTC set successfully";
                        _media.closeMedia();
                    }
                    else
                    {
                        notification.Text = "Disconnected mode";
                    }
                }
                catch (Exception ex)
                {
                    notification.Text = ex.Message; _media.closeMedia();
                }
            }

        }

        //public bool connect()
        //{
        //    if (!MediaSettings.media.IsOpen) Settings.media.Open();
        //    reader = new GXDLMSReader(MediaSettings._client, MediaSettings.media, MediaSettings.trace);
        //    reader.InitializeConnection();
        //    Program._connected = true;
        //    return true;
        //}

        private void btn_readRTC_Click(object sender, EventArgs e)
        {
            try
            {
                notification.Text = "";
                if (_media.connect())
                {
                    GXDLMSClock clock = new GXDLMSClock("0.0.1.0.0.255");
                    //object rtc = _media.reader.ReadObis(clock, 2);
                    GXDateTime gXDateTime = (GXDateTime)_media.reader.ReadObis(clock, 2);
                    //reader.Disconnect();
                    notification.Text = gXDateTime.ToString();
                    _media.closeMedia();
                }
                else
                {
                    notification.Text = "Disconnected mode";
                }
            }
            catch (Exception ex)
            {
                notification.Text = ex.Message; _media.closeMedia();
            }
        }

        private void MeterSettings_Load(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabPage7);
            cb_hh1.SelectedIndex = 0; cb_mn1.SelectedIndex = 0; cb_mn2.SelectedIndex = 0; cb_mn3.SelectedIndex = 0; cb_mn4.SelectedIndex = 0;
            cb_lsipInterval.SelectedIndex = 0;
            cb_mdipinterval.SelectedIndex = 0;
            dt_calender.Format = DateTimePickerFormat.Custom;
            dt_calender.CustomFormat = "dd/MM/yy HH:mm:ss";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yy HH:mm:ss";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd/MM/yy HH:mm:ss";
            dateTimePicker3.Format = DateTimePickerFormat.Custom;
            dateTimePicker3.CustomFormat = "dd/MM/yy HH:mm:ss";
            dt_deviationHour.Format = DateTimePickerFormat.Custom;
            dt_deviationHour.CustomFormat = "HH";
            dt_deviationHour.ShowUpDown = true;
            //dt1.Format = DateTimePickerFormat.Custom;
            //dt1.CustomFormat = "HH:mm";
            //dt1.ShowUpDown = true;
            //dt2.Format = DateTimePickerFormat.Custom;
            //dt2.CustomFormat = "HH:mm";
            //dt2.ShowUpDown = true;
            //dt3.Format = DateTimePickerFormat.Custom;
            //dt3.CustomFormat = "HH:mm";
            //dt3.ShowUpDown = true;
            //dt4.Format = DateTimePickerFormat.Custom;
            //dt4.CustomFormat = "HH:mm";
            //dt4.ShowUpDown = true;           
            timer1.Start(); //timer2.Start();
            rb_auto.Checked = true;
            rb_autotarrif.Checked = true;
            rb_ascii.Checked = true;
            //rb_manualdisplay.Checked = true;//push parameter radio button
            listBox1.Items.Clear();
            listBox1.Items.AddRange(DisplayItemsArr1PH);
            //listBox1.Items.AddRange(tempDisplay);
            Program.ListItems = null;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                string time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                DateTime date = DateTime.ParseExact(time, "dd/MM/yyyy HH:mm:ss", null);
                dateTimePicker1.Text = date.ToString(); dt_calender.Text = date.ToString(); //dt2.Text = date.ToString(); dt3.Text = date.ToString(); dt4.Text = date.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_readSerial_Click(object sender, EventArgs e)
        {
            try
            {
                notification.Text = "";
                if (_media.connect())
                {
                    GXDLMSData serial = new GXDLMSData("0.0.96.1.0.255");
                    object val = _media.reader.ReadObis(serial, 2);
                    if (val != null)
                        notification.Text = val.ToString();
                    _media.closeMedia();
                }
                else
                {
                    notification.Text = "Disconnected mode";
                }
            }
            catch (Exception ex)
            {
                notification.Text = ex.Message; _media.closeMedia();
            }
        }

        private void btn_readobis_Click(object sender, EventArgs e)
        {
            try
            {
                notification.Text = "";
                if (_media.connect())
                {
                    string name = function.getNameFormObisCode(tb_obis.Text.Trim());
                    GXDLMSData gXDLMSData = new GXDLMSData(tb_obis.Text.Trim());
                    object val = _media.reader.ReadObis(gXDLMSData, 2);
                    if (val != null)
                        notification.Text = name + " = " + val.ToString();
                    _media.closeMedia();
                }
                else
                {
                    notification.Text = "Disconnected mode";
                }
            }
            catch (Exception ex)
            {
                notification.Text = ex.Message; _media.closeMedia();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string ob = "1.0.94.91.3.255";//Instant scaler profile
            GXDLMSProfileGeneric it = new GXDLMSProfileGeneric(ob);
            //GXDLMSProfileGeneric profile = new GXDLMSProfileGeneric();//Instant
            //reader = new GXDLMSReader(Settings.client, Settings.media, Settings.trace);
            //Settings.media.Open();
            //reader.InitializeConnection();
            //reader.GetAssociationView(true);

            // ArrayList Values = reader.GetProfileGenericsValues(ob);
            //ArrayList ScalerValues  = reader.GetScalerValues(ob);          
            //ArrayList ScalerObis = reader.GetScalerObisCodes(ob);   
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //string ob = "0.4.25.9.0.255";//Instant scaler profile
            //ArrayList aaaaaa = reader.ReadDlms(ob, "Genric");
            string ob = "0.0.96.1.0.255";//serial number
            GXDLMSData serial = new GXDLMSData(ob);
            GXDLMSImageTransfer image = new GXDLMSImageTransfer();

            string pushobj = _media.reader.ReadObis(serial, 2).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //demo.getText();
            _media.reader.getText();
            //StringBuilder sb = new StringBuilder();
            //settings.readObjects.Add(new KeyValuePair<string, int>("0.0.94.91.10.255", 1));
            //settings.readObjects.Add(new KeyValuePair<string, int>("1.0.94.91.0.255", ));instant
            //settings.readObjects.Add(new KeyValuePair<string, int>("0.0.94.91.10.255", 3));
            // settings.readObjects.Add(new KeyValuePair<string, int>("0.0.94.91.10.255", 3));
            //GXDLMSProfileGeneric gp = new GXDLMSProfileGeneric("1.0.94.91.0.255");



            //object row = reader.Read(gp, 2);
            //GXDLMSObject[] cols = (gp as GXDLMSProfileGeneric).GetCaptureObject();

            //foreach (GXDLMSObject cell in cols)
            //{
            //    string aa = cell.LogicalName;
            //    sb.Append(Convert.ToString(cell));
            //    sb.Append(" | ");
            //}
            //sb.Append("\r\n");

            //if (settings.readObjects.Count != 0)
            //{
            //foreach (KeyValuePair<string, int> it in settings.readObjects)
            //{
            //    GXDLMSProfileGeneric gp = new GXDLMSProfileGeneric("0.0.94.91.10.255");
            //    object obj = reader.Read(gp, 3);
            //    GXDLMSObject[] cols = (obj as GXDLMSProfileGeneric).GetCaptureObject();
            //    StringBuilder sb = new StringBuilder();
            //    bool First = true;
            //    foreach (GXDLMSObject col in cols)
            //    {
            //        if (!First)
            //        {
            //            sb.Append(" | ");
            //        }
            //        First = false;
            //        sb.Append(col.Name);
            //        sb.Append(" ");
            //        sb.Append(col.Description);
            //    }
            //string[] Values = getVAlues(obj);
            //for (int i = 0; i < Values.Length; i++)
            //{
            //    richTextBox1.Text += Values[i] +"\n";
            //}
            // showObject(obj, it.Value);
            //}
            // }       
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string ob = "1.0.94.91.3.255";//Instant scaler profile            
            ArrayList aaaaaa = _media.reader.ReadDlms(ob, "obis");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string ob = "0.0.25.9.0.255";//Instant scaler profile            
            // ArrayList aaaaaa=  reader.ReadDlms(ob, "Genric");
            //reader = new GXDLMSReader(Settings.client, Settings.media, Settings.trace);
            //Settings.media.Open();
            //reader.InitializeConnection();
            //reader.GetAssociationView(true);
            GXDLMSData counter = new GXDLMSData("0.0.43.1.2.255");//invocation counter
            //GXDLMSData r = new GXDLMSData("0.0.25.9.0.255");
            object val = _media.reader.ReadObis(counter, 2);
            //double ax = r.Scaler;
            //string axx = r.Unit.ToString();
            //Read data r.Scaler r.Unit
            //GXDLMSObject it = new GXDLMSObject("0.0.96.1.2.255");
            //object vals = reader.ReadObis(it, 2);

            GXDLMSProfileGeneric it = new GXDLMSProfileGeneric(ob);
            object vals = _media.reader.Read(it, 2);

            //GXDLMSObject gg = (GXDLMSObject)vals;
            // gg.GetDataType(0);
            //string[] dd = gg.GetValues(0).ToArray().ToString();
            //string[] vv = function.getVAlues(vals);
        }

        private void btn_setDesAddress_Click(object sender, EventArgs e)
        {
            if (Program.selectedAuth != "High")
            { notification.Text = "Server address cannot be set in " + Program.selectedAuth + " authentication mode"; }
            else
            {
                try
                {
                    notification.Text = "";
                    if (_media.connect())
                    {
                        GXDLMSPushSetup pushSetup = new GXDLMSPushSetup("0.4.25.9.0.255");
                        pushSetup.Destination = tb_destination.Text.Trim();// "2402:3a80:1700:486:d11a:3a3c:4482:42xx";
                                                                           //pushSetup.Service = Gurux.DLMS.Objects.Enums.ServiceType.Tcp;
                                                                           //pushSetup.Message = Gurux.DLMS.Objects.Enums.MessageType.CosemApdu;
                                                                           //pushSetup.RandomisationStartInterval = 300;
                                                                           //pushSetup.CommunicationWindow=List
                                                                           //pushSetup.NumberOfRetries = 5;
                                                                           //pushSetup.RepetitionDelay = 10;
                        if (_media.reader.Write(pushSetup, 3)) { notification.Text = "Address set successfully"; }
                        else { notification.Text = "Address not set,Try again"; }
                    }
                    else
                    {
                        notification.Text = "Disconnected mode";
                    }
                }
                catch (Exception ex)
                {
                    notification.Text = ex.Message;
                }
            }
        }

        private void btn_readDesAddress_Click(object sender, EventArgs e)
        {
            try
            {
                notification.Text = ""; tb_destination.Text = "";
                if (_media.connect())
                {
                    GXDLMSPushSetup push = new GXDLMSPushSetup("0.4.25.9.0.255");
                    string pushobj = _media.reader.ReadObis(push, 3).ToString();
                    string[] pishList = pushobj.Split(' ');
                    notification.Text = pishList[1];
                    tb_destination.Text = pishList[1];
                }
                else
                {
                    notification.Text = "Disconnected mode";
                }
            }
            catch (Exception ex)
            {
                notification.Text = ex.Message;
            }
        }

        private void btn_setPush_Click(object sender, EventArgs e)
        {
            try
            {
                notification.Text = "";
                if (_media.connect())
                {
                    int min = Convert.ToInt32(tb_pushMin.Text.Trim());
                    int sec = Convert.ToInt32(tb_pushSec.Text.Trim());
                    GXDateTime dt = new GXDateTime(00, 00, 00, 00, min, sec, 00);
                    GXDLMSActionSchedule pushschedule = new GXDLMSActionSchedule("0.0.15.0.4.255");
                    pushschedule.ExecutionTime = new GXDateTime[] { new GXDateTime(dt) };
                    if (_media.reader.Write(pushschedule, 4)) { notification.Text = "Push interval set successfully"; }
                    else { notification.Text = "Push interval not set,Try again"; }
                    _media.closeMedia();
                }
                else
                {
                    notification.Text = "Disconnected mode";
                }
            }
            catch (Exception ex)
            {
                notification.Text = ex.Message; _media.closeMedia();
            }
        }

        private void btn_pushschedule_Click(object sender, EventArgs e)
        {
            try
            {
                notification.Text = "";
                if (_media.connect())
                {
                    GXDLMSActionSchedule pushschedule = new GXDLMSActionSchedule("0.0.15.0.4.255");
                    object[] pushobj = _media.reader.Read(pushschedule, 4);
                    GXDateTime gXDateTime = (GXDateTime)pushobj[0];
                    string hour = gXDateTime.Value.Hour.ToString();
                    string min = gXDateTime.Value.Minute.ToString();
                    string sec = gXDateTime.Value.Second.ToString();
                    notification.Text = hour + " Hour " + min + " Minutes " + sec + " Seconds";
                    _media.closeMedia();
                }
                else
                {
                    notification.Text = "Disconnected mode";
                }
            }
            catch (Exception ex)
            {
                notification.Text = ex.Message; _media.closeMedia();
            }
        }

        private void btn_writeApn_Click(object sender, EventArgs e)
        {
            if (Program.selectedAuth != "High")
            { notification.Text = "APN cannot be set in " + Program.selectedAuth + " authentication mode"; }
            else
            {
                try
                {
                    notification.Text = "";
                    if (_media.connect())
                    {
                        GXDLMSGprsSetup gprsSetup = new GXDLMSGprsSetup(); //M2M.METERIPV6
                        gprsSetup.APN = tb_apn.Text.Trim();
                        if (_media.reader.Write(gprsSetup, 2)) { notification.Text = "Apn set successfully"; }
                        else { notification.Text = "Apn not set,Try again"; }
                        _media.closeMedia();
                    }
                    else
                    {
                        notification.Text = "Disconnected mode";
                    }
                }
                catch (Exception ex)
                {
                    notification.Text = ex.Message; _media.closeMedia();
                }
            }
        }

        private void btn_readApn_Click(object sender, EventArgs e)
        {
            try
            {
                notification.Text = ""; tb_apn.Text = "";
                if (_media.connect())
                {
                    GXDLMSGprsSetup gprs = new GXDLMSGprsSetup(); //M2M.METERIPV6                   
                    string apn = _media.reader.ReadObis(gprs, 2).ToString();
                    notification.Text = apn;// apn.Remove(apn.Length - 1, 1);
                    tb_apn.Text = apn;
                    _media.closeMedia();
                }
                else
                {
                    notification.Text = "Disconnected mode";
                }
            }
            catch (Exception ex)
            {
                notification.Text = ex.Message;
                _media.closeMedia();
            }
        }

        private void btn_media_Click(object sender, EventArgs e)
        {
            //DlmsSettings dlmsSettings = new DlmsSettings();
            //dlmsSettings.ShowDialog();
        }

        private void btn_convert_Click(object sender, EventArgs e)
        {
            tb_hexbox.Text = "";
            if (rb_ascii.Checked == true)
                tb_hexbox.Text = utility.Hextochar(tb_iptext.Text.ToLower().Trim());
            else
                tb_hexbox.Text = utility.chartoHex(tb_iptext.Text.ToUpper().Trim());
        }

        private void b_setKwh_Click(object sender, EventArgs e)
        {
            if (tb_E1.Text.Length > 0 && Convert.ToInt32(tb_E1.Text) > 0)
            {
                try
                {
                    notification.Text = "";
                    string E1 = Convert.ToInt32((Convert.ToDouble(tb_E1.Text) * 100)).ToString("X2").PadLeft(8, '0'); ;
                    //byte x = Convert.ToByte(aa);
                    //string ab=aa.ToString("X2").PadLeft(8, '0');
                    //string E1 = (Convert.ToInt32(Convert.ToDouble(tb_E1.Text)) * 100).ToString("X2").PadLeft(8, '0');
                    kwhSet[7] = Convert.ToByte(E1.Substring(0, 2), 16);
                    kwhSet[8] = Convert.ToByte(E1.Substring(2, 2), 16);
                    kwhSet[9] = Convert.ToByte(E1.Substring(4, 2), 16);
                    kwhSet[10] = Convert.ToByte(E1.Substring(6, 2), 16);

                    if (_media.connect2())
                    {
                        byte[] buffer = _media.reader.SendAndRecieve(unlock, 400, 12);
                        string response = ASCIIEncoding.ASCII.GetString(buffer);
                        if (response.Contains("UnLocked"))
                        {
                            byte[] reply = _media.reader.SendAndRecieve(kwhSet, 400, 5);
                            if (reply[0] == 0x02 && reply[1] == 0x06 && reply[2] == 0x03 && reply[3] == 0x0d && reply[4] == 0x0a)
                            {
                                notification.Text = "Energy set pass";
                            }
                            else if (reply[0] == 0x02 && reply[1] == 0x15 && reply[2] == 0x03 && reply[3] == 0x0d && reply[4] == 0x0a)
                            {
                                notification.Text = "Energy set fail";
                            }
                            else
                            {
                                notification.Text = "Error";
                            }
                        }
                        else
                        {
                            notification.Text = "Meter is locked";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                _media.closeMedia();
            }
            else
            {
                MessageBox.Show("Please enter value greater than zero");
            }

        }

        private void b_setVoltage_Click(object sender, EventArgs e)
        {
            try
            {
                notification.Text = "";
                string V1 = (Convert.ToInt32(Convert.ToDouble(tb_underVolt.Text)) * 100).ToString("X2").PadLeft(4, '0');
                string V2 = (Convert.ToInt32(Convert.ToDouble(tb_overVolt.Text)) * 100).ToString("X2").PadLeft(4, '0');
                string t1 = Convert.ToInt32(tb_volt_timeout.Text).ToString("X2").PadLeft(4, '0');
                voltSet[7] = Convert.ToByte(V1.Substring(0, 2), 16);
                voltSet[8] = Convert.ToByte(V1.Substring(2, 2), 16);
                voltSet[9] = Convert.ToByte(V2.Substring(0, 2), 16);
                voltSet[10] = Convert.ToByte(V2.Substring(2, 2), 16);
                voltSet[11] = Convert.ToByte(t1.Substring(0, 2), 16);
                voltSet[12] = Convert.ToByte(t1.Substring(2, 2), 16);
                if (_media.connect2())
                {
                    byte[] buffer = _media.reader.SendAndRecieve(unlock, 400, 12);
                    string response = ASCIIEncoding.ASCII.GetString(buffer);
                    if (response.Contains("UnLocked"))
                    {
                        byte[] reply = _media.reader.SendAndRecieve(voltSet, 400, 4);
                        if (reply[0] == 0x02 && reply[1] == 0x03 && reply[2] == 0x0d && reply[3] == 0x0a)
                        {
                            notification.Text = "Voltage Parameters Set Successfully";
                        }
                        else
                        {
                            notification.Text = "Error";
                        }
                    }
                    else
                    {
                        notification.Text = "Meter is locked";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            _media.closeMedia();
        }

        private void b_cuSet_Click(object sender, EventArgs e)
        {
            try
            {
                notification.Text = "";
                string C1 = (Convert.ToInt64(Convert.ToDouble(tb_unCurrent.Text)) * 100).ToString("X2").PadLeft(4, '0');
                string C2 = (Convert.ToInt64(Convert.ToDouble(tb_ovCurrent.Text)) * 100).ToString("X2").PadLeft(4, '0');
                string t1 = Convert.ToInt32(tb_seconds1.Text).ToString("X2").PadLeft(4, '0');
                string t2 = Convert.ToInt32(tb_seconds2.Text).ToString("X2").PadLeft(4, '0');
                curSet[7] = Convert.ToByte(C1.Substring(0, 2), 16);
                curSet[8] = Convert.ToByte(C1.Substring(2, 2), 16);
                curSet[9] = Convert.ToByte(C2.Substring(0, 2), 16);
                curSet[10] = Convert.ToByte(C2.Substring(2, 2), 16);
                curSet[11] = Convert.ToByte(t1.Substring(0, 2), 16);
                curSet[12] = Convert.ToByte(t1.Substring(2, 2), 16);
                curSet[13] = Convert.ToByte(t2.Substring(0, 2), 16);
                curSet[14] = Convert.ToByte(t2.Substring(2, 2), 16);
                if (_media.connect2())
                {
                    byte[] buffer = _media.reader.SendAndRecieve(unlock, 400, 12);
                    string response = ASCIIEncoding.ASCII.GetString(buffer);
                    if (response.Contains("UnLocked"))
                    {
                        byte[] reply = _media.reader.SendAndRecieve(curSet, 400, 4);
                        if (reply[0] == 0x02 && reply[1] == 0x03 && reply[2] == 0x0d && reply[3] == 0x0a)
                        {
                            notification.Text = "Current Threshold Set Successfully";
                        }
                        else
                        {
                            notification.Text = "Error";
                        }
                    }
                    else
                    {
                        notification.Text = "Meter is locked";
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            _media.closeMedia();
        }

        private void b_relayControl_Click(object sender, EventArgs e)
        {
            try
            {
                notification.Text = "";
                string t1 = Convert.ToInt32(tb_relaycontrol.Text).ToString("X2").PadLeft(4, '0');
                relSet[7] = Convert.ToByte(t1.Substring(0, 2), 16);
                relSet[8] = Convert.ToByte(t1.Substring(2, 2), 16);
                if (_media.connect2())
                {
                    byte[] buffer = _media.reader.SendAndRecieve(unlock, 400, 12);
                    string response = ASCIIEncoding.ASCII.GetString(buffer);
                    if (response.Contains("UnLocked"))
                    {
                        byte[] reply = _media.reader.SendAndRecieve(relSet, 400, 4);
                        if (reply[0] == 0x02 && reply[1] == 0x03 && reply[2] == 0x0d && reply[3] == 0x0a)
                        {
                            notification.Text = "Relay Control Timeout Set Successfully";
                        }
                        else
                        {
                            notification.Text = "Error";
                        }
                    }
                    else
                    {
                        notification.Text = "Meter is locked";
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            _media.closeMedia();
        }

        private void b_locktime_Click(object sender, EventArgs e)
        {
            try
            {
                notification.Text = "";
                //string t0 = Convert.ToInt32(tb_locktime.Text).ToString("X8").PadLeft(8, '0');
                string t1 = Convert.ToInt64(tb_locktime.Text).ToString("X8").PadLeft(8, '0');
                string val = (t1.Length > 8) ? t1.Substring(t1.Length - 8, 8) : t1;
                locktimeSet[7] = Convert.ToByte(val.Substring(0, 2), 16);
                locktimeSet[8] = Convert.ToByte(val.Substring(2, 2), 16);
                locktimeSet[9] = Convert.ToByte(val.Substring(4, 2), 16);
                locktimeSet[10] = Convert.ToByte(val.Substring(6, 2), 16);
                if (_media.connect2())
                {
                    byte[] buffer = _media.reader.SendAndRecieve(unlock, 400, 12);
                    string response = ASCIIEncoding.ASCII.GetString(buffer);
                    if (response.Contains("UnLocked"))
                    {
                        byte[] reply = _media.reader.SendAndRecieve(locktimeSet, 400, 4);
                        if (reply[0] == 0x02 && reply[1] == 0x03 && reply[2] == 0x0d && reply[3] == 0x0a)
                        {
                            notification.Text = "Cover Open Lock Time Set Successfully";
                        }
                        else
                        {
                            notification.Text = "Error";
                        }
                    }
                    else
                    {
                        notification.Text = "Meter is locked";
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            _media.closeMedia();
        }

        private void b_readsettings_Click(object sender, EventArgs e)
        {
            try
            {
                notification.Text = "";
                if (_media.connect2())
                {
                    byte[] buffer = _media.reader.SendAndRecieve(unlock, 400, 12);
                    string response = ASCIIEncoding.ASCII.GetString(buffer);
                    if (response.Contains("UnLocked"))
                    {
                        byte[] reply = _media.reader.SendAndRecieve(readSettings, 400, 36);
                        if (reply != null && reply.Length == 36)
                        {
                            notification.Text = "Settings read successfully";
                            ReadSettings re = new ReadSettings(reply);
                            re.ShowDialog();
                        }
                        else
                        {
                            notification.Text = "Error";
                        }
                    }
                    else
                    {
                        notification.Text = "Meter is locked";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            _media.closeMedia();
        }

        private void b_relconnect_Click(object sender, EventArgs e)
        {

        }

        private void b_reldisconnect_Click(object sender, EventArgs e)
        {

        }
        //GXDLMSActivityCalendar activity = new GXDLMSActivityCalendar();
        //activity.CalendarNameActive = "Active";
        //activity.WeekProfileTableActive = new GXDLMSWeekProfile[] { new GXDLMSWeekProfile("Monday", 1, 1, 1, 1, 1, 1, 1) };
        //activity.DayProfileTableActive = new GXDLMSDayProfile[] { new GXDLMSDayProfile(1, new GXDLMSDayProfileAction[] { new GXDLMSDayProfileAction(new GXTime(DateTime.Now), "0.1.10.1.101.255", 1) }) };
        //activity.SeasonProfileActive = new GXDLMSSeasonProfile[] { new GXDLMSSeasonProfile("Summer time", new GXDateTime(-1, 3, 31, -1, -1, -1, -1), activity.WeekProfileTableActive[0]) };

        //activity.CalendarNamePassive = "Passive";
        //activity.WeekProfileTablePassive = new GXDLMSWeekProfile[] { new GXDLMSWeekProfile("Tuesday", 1, 1, 1, 1, 1, 1, 1) };
        //activity.DayProfileTablePassive = new GXDLMSDayProfile[] { new GXDLMSDayProfile(1, new GXDLMSDayProfileAction[] { new GXDLMSDayProfileAction(new GXTime(DateTime.Now), "0.1.10.1.101.255", 1) }) };
        //activity.SeasonProfilePassive = new GXDLMSSeasonProfile[] { new GXDLMSSeasonProfile("Winter time", new GXDateTime(-1, 10, 30, -1, -1, -1, -1), activity.WeekProfileTablePassive[0]) };
        //activity.Time = new GXDateTime(DateTime.Now);
        //Items.Add(activity);
        //foreach (GXDLMSDayProfile item in (GXDLMSDayProfile[])value)
        //{
        //    GXDLMSDayProfile it = new GXDLMSDayProfile();
        //    it.DayId = Convert.ToInt32(item);
        //    List<GXDLMSDayProfileAction> actions = new List<GXDLMSDayProfileAction>();
        //    //foreach (GXDLMSDayProfile it2 in (GXDLMSDayProfile)item)
        //    //{
        //    //    GXDLMSDayProfileAction ac = new GXDLMSDayProfileAction();
        //    //    ac.StartTime = (GXTime)GXDLMSClient.ChangeType((byte[])it2, DataType.Time);
        //    //    ac.ScriptLogicalName = GXDLMSClient.ChangeType((byte[])it2, DataType.String).ToString();
        //    //    ac.ScriptSelector = Convert.ToUInt16(it2);
        //    //    actions.Add(ac);
        //    //}
        //    it.DaySchedules = actions.ToArray();
        //    //items.Add(it);
        //}
        //GXDLMSSeasonProfile sp = new GXDLMSSeasonProfile();
        //byte[] name = { 0x53, 0x53,0x53,0x53,0x53,0x53,};
        //sp.Name = name;
        //string RTC = dateTimePicker1.Text;
        //string year = "20" + RTC.Substring(6, 2);
        //string mon = RTC.Substring(3, 2);
        //string day = RTC.Substring(0, 2);
        //string hour = RTC.Substring(9, 2);
        //string min = RTC.Substring(12, 2);
        //string sec = RTC.Substring(15, 2);
        //GXDLMSClock clock = new GXDLMSClock();
        //GXDateTime date1 = new GXDateTime(Convert.ToInt32(year), Convert.ToInt32(mon), Convert.ToInt32(day), Convert.ToInt32(hour), Convert.ToInt32(min), Convert.ToInt32(sec), 0);
        //clock.Time = date1;
        //// sp.Start = DateTime.Now.ToString(); ;
        //byte[] week = { 0x49 };
        //sp.WeekName = week;
        //GXDLMSWeekProfile wp = new GXDLMSWeekProfile();
        //wp.Monday = 1; wp.Tuesday = 1; wp.Wednesday = 1;
        //wp.Thursday = 1; wp.Friday = 1; wp.Saturday = 1; wp.Sunday = 1;
        //GXDLMSDayProfile dp = new GXDLMSDayProfile();
        //List<GXDLMSDayProfileAction> actions = new List<GXDLMSDayProfileAction>();
        //GXDLMSDayProfileAction pc = new GXDLMSDayProfileAction();
        //for (int i = 1; i < 5; i++)
        //{
        //    //byte[] buff = GXCommon.GetAsByteArray(RTC);
        //    pc.StartTime = new GXTime(DateTime.Now);// (GXTime)GXDLMSClient.ChangeType(buff, DataType.Time);
        //    pc.ScriptLogicalName = "0.0.11.0.101.255";
        //    pc.ScriptSelector = Convert.ToUInt16(i);
        //    actions.Add(pc);
        //}
        //dp.DaySchedules = actions.ToArray();
        //ac.DayProfileTablePassive = new GXDLMSDayProfile[] { dp };
        //activity.CalendarNamePassive = "PPPPPPP";
        //_media.reader.Write(activity, 6);
        //activity.Time = new GXDateTime(DateTime.Now.ToString());
        //if (_media.reader.Write(activity, 10)) { notification.Text = "Day tarrif profile set successfully"; }
        //else { notification.Text = "Error"; }
        //_media.closeMedia();
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                notification.Text = "";
                if (_media.connect())
                {
                    rb_tarrif.Text = "";
                    GXDLMSActivityCalendar activity = new GXDLMSActivityCalendar();
                    var val1 = _media.reader.Read2(activity, 2); //Calender name ACTIVE
                    var val2 = _media.reader.Read2(activity, 10); //Calender date
                    object val = _media.reader.Read2(activity, 5);  //Day profile ACTIVE
                    _media.closeMedia();

                    GXDLMSDayProfile[] value = (GXDLMSDayProfile[])val;
                    int dayid = value[0].DayId;
                    GXDLMSDayProfileAction[] arr = value[0].DaySchedules;
                    string[] time = new string[4];
                    string[] zone = new string[4];
                    for (int i = 0; i < 4; i++)
                    {
                        time[i] = arr[i].StartTime.ToString();
                        zone[i] = arr[i].ScriptSelector.ToString(); ;
                    }
                    string[] st1 = time[0].Split(' ');
                    string[] st2 = time[1].Split(' ');
                    string[] st3 = time[2].Split(' ');
                    string[] st4 = time[3].Split(' ');
                    if (st1.Length == 1)
                    {
                        rb_tarrif.Text = "-------ACTIVE DAY PROFILE------" + "\n" +
                                     "Active Calender Activation Date = " + val2.ToString() + "\n" +
                                     "Selector      Start Time " + "\n" +
                                     "    1                " + st1[0] + "\n" +
                                     "    2                " + st2[0] + "\n" +
                                     "    3                " + st3[0] + "\n" +
                                     "    4                " + st4[0] + "\n";
                    }
                    else
                    {
                        rb_tarrif.Text = "-------ACTIVE DAY PROFILE------" + "\n" +
                                     "Active Calender Activation Date = " + val2.ToString() + "\n" +
                                     "Selector      Start Time " + "\n" +
                                     "    1                " + st1[1] + "\n" +
                                     "    2                " + st2[1] + "\n" +
                                     "    3                " + st3[1] + "\n" +
                                     "    4                " + st4[1] + "\n";
                    }
                    tb_calendername.Text = val1.ToString();
                }
                else
                {
                    notification.Text = "Disconnected mode";
                }
            }
            catch (Exception ex)
            {
                notification.Text = ex.Message; _media.closeMedia();
            }
        }

        private void rbtarrif_manual_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void rbtarrif_auto_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void b_set_dayprofile_Click(object sender, EventArgs e)
        {
            notification.Text = "";
            if (ck_zone1.Checked == true && ck_zone2.Checked == true && ck_zone3.Checked == true && cb_hh2.Text != "" && cb_hh3.Text != "" && cb_hh4.Text != "")
            {
                try
                {
                    string cl_date = dt_calender.Text;
                    DateTime dateTime5 = DateTime.ParseExact(cl_date, "dd/MM/yy HH:mm:ss", CultureInfo.InvariantCulture);
                    string[] getDate = cl_date.Split(' ');

                    string Time1 = cb_hh1.Text.Trim() + ":" + cb_mn1.Text.Trim();// dt1.Text;
                    string zone1 = getDate[0] + " " + Time1 + ":00";
                    DateTime dateTime1 = DateTime.ParseExact(zone1, "dd/MM/yy HH:mm:ss", CultureInfo.InvariantCulture);

                    string Time2 = cb_hh2.Text.Trim() + ":" + cb_mn2.Text.Trim(); //dt2.Text;
                    string zone2 = getDate[0] + " " + Time2 + ":00";
                    DateTime dateTime2 = DateTime.ParseExact(zone2, "dd/MM/yy HH:mm:ss", CultureInfo.InvariantCulture);

                    string Time3 = cb_hh3.Text.Trim() + ":" + cb_mn3.Text.Trim(); //dt3.Text;
                    string zone3 = getDate[0] + " " + Time3 + ":00";
                    DateTime dateTime3 = DateTime.ParseExact(zone3, "dd/MM/yy HH:mm:ss", CultureInfo.InvariantCulture);

                    string Time4 = cb_hh4.Text.Trim() + ":" + cb_mn4.Text.Trim(); //dt4.Text;
                    string zone4 = getDate[0] + " " + Time4 + ":00";
                    DateTime dateTime4 = DateTime.ParseExact(zone4, "dd/MM/yy HH:mm:ss", CultureInfo.InvariantCulture);

                    if (_media.connect())
                    {
                        GXDLMSActivityCalendar activity = new GXDLMSActivityCalendar();
                        activity.DayProfileTablePassive = new GXDLMSDayProfile[] { new GXDLMSDayProfile(1, new GXDLMSDayProfileAction[] {
                    new GXDLMSDayProfileAction(new GXTime(dateTime1), "0.0.10.0.100.255", 1),
                    new GXDLMSDayProfileAction(new GXTime(dateTime2), "0.0.10.0.100.255", 2),
                    new GXDLMSDayProfileAction(new GXTime(dateTime3), "0.0.10.0.100.255", 3),
                    new GXDLMSDayProfileAction(new GXTime(dateTime4), "0.0.10.0.100.255", 4)})};

                        activity.Time = new GXDateTime(dateTime5);
                        _media.reader.Write(activity, 10);
                        activity.CalendarNamePassive = tb_calendername.Text.Trim();
                        _media.reader.Write(activity, 6);

                        if (_media.reader.Write(activity, 9))
                        {
                            notification.Text = "Day Profile set successfully";
                        }
                        else { notification.Text = "Error"; }
                        _media.closeMedia();
                    }
                    else
                    {
                        notification.Text = "Disconnected mode";
                    }
                }
                catch (Exception ex)
                {
                    notification.Text = ex.Message; _media.closeMedia();
                }
            }
            else
            {
                notification.Text = "Please fill values in all four zones";
            }

        }

        private void b_read_day_profile_passive_Click(object sender, EventArgs e)
        {
            try
            {
                notification.Text = "";
                if (_media.connect())
                {
                    rb_tarrif.Text = "";
                    GXDLMSActivityCalendar activity = new GXDLMSActivityCalendar();
                    var val1 = _media.reader.Read2(activity, 6); //Calender name passive
                    var val2 = _media.reader.Read2(activity, 10); //Calender date
                    object val = _media.reader.Read2(activity, 9);  //Day profile passive
                    _media.closeMedia();

                    GXDLMSDayProfile[] value = (GXDLMSDayProfile[])val;
                    int dayid = value[0].DayId;
                    GXDLMSDayProfileAction[] arr = value[0].DaySchedules;
                    string[] time = new string[4]; string[] zone = new string[4];

                    GXDateTime[] gXDateTime = new GXDateTime[4];
                    for (int i = 0; i < 4; i++)
                    {
                        gXDateTime[i] = arr[i].StartTime;
                        time[i] = arr[i].StartTime.ToString();
                        zone[i] = arr[i].ScriptSelector.ToString(); ;
                    }
                    string[] st1 = time[0].Split(' ');
                    string[] st2 = time[1].Split(' ');
                    string[] st3 = time[2].Split(' ');
                    string[] st4 = time[3].Split(' ');
                    if (st1.Length == 1)
                    {
                        rb_tarrif.Text = "-------PASSIVE DAY PROFILE------" + "\n" +
                                     "Active Calender Activation Date = " + val2.ToString() + "\n" +
                                     "Selector      Start Time " + "\n" +
                                     "    1                " + st1[0] + "\n" +
                                     "    2                " + st2[0] + "\n" +
                                     "    3                " + st3[0] + "\n" +
                                     "    4                " + st4[0] + "\n";
                    }
                    else
                    {
                        rb_tarrif.Text = "-------PASSIVE DAY PROFILE------" + "\n" +
                                     "Active Calender Activation Date = " + val2.ToString() + "\n" +
                                     "Selector      Start Time " + "\n" +
                                     "    1                " + st1[1] + "\n" +
                                     "    2                " + st2[1] + "\n" +
                                     "    3                " + st3[1] + "\n" +
                                     "    4                " + st4[1] + "\n";
                    }
                    tb_calendername.Text = val1.ToString();
                }
                else
                {
                    notification.Text = "Disconnected mode";
                }
            }
            catch (Exception ex)
            {
                notification.Text = ex.Message; _media.closeMedia();
            }
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            addListBoxItems(listBox1, listBox2);
        }

        private void addListBoxItems(ListBox source, ListBox destination)
        {
            if (listBox1.SelectedItem != null)
            {
                string parameter = listBox1.SelectedItem.ToString();
                ListBox.SelectedObjectCollection sourceItems = source.SelectedItems;
                if (Program.ListItems == null)
                {
                    foreach (var item in sourceItems)
                    {
                        destination.Items.Add(item);
                        Program.ListItems = listBox2.Items.OfType<string>().ToArray();
                    }
                }
                else if (Program.ListItems.Contains(parameter))
                {
                    MessageBox.Show("This parameter is already in the list");
                }
                else
                {
                    foreach (var item in sourceItems)
                    {
                        destination.Items.Add(item);
                        Program.ListItems = listBox2.Items.OfType<string>().ToArray();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select 1PH OR 3PH parameters", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


            //while (source.SelectedItems.Count > 0)
            //{
            //    source.Items.Remove(source.SelectedItems[0]);
            //}
        }

        private void listBox2_Click(object sender, EventArgs e)
        {
            try
            {
                int listIndex = listBox2.SelectedIndex;
                removeListBoxItems(listBox2);
                Program.ListItems = Program.ListItems.Where((val, idx) => idx != listIndex).ToArray();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void removeListBoxItems(ListBox source)
        {
            try
            {
                ListBox.SelectedObjectCollection sourceItems = source.SelectedItems;
                source.Items.Remove(source.SelectedItems[0]);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Don't click on emplty list", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                //RefreshDisplayLable();          
                //Flash_Status_Strip();
                notification.Text = "";
                int PCount = 0, k = 7;
                PCount = listBox2.Items.Count;
                if (displayMode != null)
                {
                    if (PCount != 0)
                    {
                        string[] ListItems = listBox2.Items.OfType<string>().ToArray();//transfering listbox1 items into a array
                        string SettingStr = PCount.ToString("X2") + CreateSettingString(ListItems, PCount);//adding parameter count + parameter string
                        SettingStr += RTC_CheckSum(SettingStr);//adding checksum
                        byte[] cmdArray = new byte[(SettingStr.Length / 2) + 8];//creating command array
                        cmdArray[0] = 0x45; cmdArray[1] = 0x6C; cmdArray[2] = 0x53; cmdArray[3] = 0x73; cmdArray[4] = 0x65; cmdArray[5] = 0x74;
                        if (displayMode == "AUTO")
                            cmdArray[6] = 0x33;
                        else if (displayMode == "PUSH")
                            cmdArray[6] = 0x34;
                        //else if (displayMode == "BATTERY")
                        //    cmdArray[3] = 0x62;
                        for (int i = 0; i < SettingStr.Length; i += 2)//adding parameter array in command array
                        {
                            cmdArray[k] = Convert.ToByte(SettingStr.Substring(i, 2), 16);
                            k++;
                        }
                        cmdArray[k] = 0x0D;//adding OD in last index
                        if (_media.connect2())
                        {
                            byte[] buffer = _media.reader.SendAndRecieve(unlock, 400, 12);
                            string response = ASCIIEncoding.ASCII.GetString(buffer);
                            if (response.Contains("UnLocked"))
                            {
                                byte[] reply = _media.reader.SendAndRecieve(cmdArray, 400, 4);
                                if (reply[0] == 0x02 && reply[1] == 0x03 && reply[2] == 0x0d && reply[3] == 0x0a)
                                {
                                    notification.Text = displayMode + "Parameters Set Successfully";
                                }
                                else
                                {
                                    notification.Text = "ERROR : Meter response not matched";
                                }
                            }
                            else
                            {
                                notification.Text = "Meter not unlocked";
                            }
                        }
                    }
                    else
                    { notification.Text = "No parameter is selected"; notification.ForeColor = Color.DarkRed; }

                }
                else
                { notification.Text = "Select Auto or Push Mode"; notification.ForeColor = Color.DarkRed; }
                _media.closeMedia();
            }
            catch (Exception ex)
            {
                _media.closeMedia();
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public byte SendComandToDLMS(byte[] cmdArray)
        {
            byte status = 0x00;
            byte[] buff1 = null;
            //if (UnLock3PhDLMSMeter())
            {
                if (_media.connect2())
                {
                    buff1 = _media.reader.SendAndRecieve(cmdArray, 500, 1);// comObj.SendComandToDLMS(cmdArray);
                    _media.closeMedia();
                }
                if (buff1 != null)
                {
                    status = buff1[0];
                }
                else
                    status = 0x00;
            }
            //else
            //    status = 0x00;
            return status;
        }


        private string CreateSettingString(string[] ListItems, int count)
        {
            int Index = 0;
            string[] SetPara = new string[count];
            for (int i = 0; i < ListItems.Length; i++)
            {
                Index = Array.IndexOf(DisplayItemsArr1PH, ListItems[i]);
                //Index = Index + 10;
                if (ListItems[i] == "DISP_CMD(1.2.0)")
                {
                    Index = Index + 19;
                }
                if (ListItems[i] == "DISP_ENERGY_ACTI_HR")
                {
                    Index = Index + 20;
                }
                if (ListItems[i] == "DISP_ENERGY_APPI_HR")
                {
                    Index = Index + 20;
                }
                if (ListItems[i] == "DISP_ENERGY_REACTI_ACTI_HR")
                {
                    Index = Index + 20;
                }
                if (ListItems[i] == "DISP_ENERGY_REACTE_ACTI_HR")
                {
                    Index = Index + 20;
                }
                SetPara[i] = (Index + 16).ToString("X2").PadLeft(2, '0');
            }
            string Str = string.Join("", SetPara.Select(x => x.ToString()).ToArray());//Joining string array into a single string
            return Str;
        }

        public int getDisplayIndex(string displayItem)
        {
            int index = -1;
            switch (displayItem)
            {
                case "DISP_ALL_ON":
                    index = 0;
                    break;
                case "DISP_PF":
                    index = 1;
                    break;
                case "DISP_DATE":
                    index = 2;
                    break;
                case "DISP_TIME":
                    index = 3;
                    break;
                case "DISP_FREQ":
                    index = 4;
                    break;
                case "DISP_VRMS":
                    index = 5;
                    break;
                case "DISP_IRMS":
                    index = 6;
                    break;
                case "DISP_ANGLE":
                    index = 7;
                    break;
                case "DISP_IRMS_P":
                    index = 8;
                    break;
                case "DISP_IRMS_N":
                    index = 9;
                    break;
                case "DISP_POWER_ACT":
                    index = 10;
                    break;
                case "DISP_POWER_APP":
                    index = 11;
                    break;
                case "DISP_MD_ACT":
                    index = 12;
                    break;
                case "DISP_MD_APP":
                    index = 13;
                    break;
                case "DISP_ENERGY_ACTI_IMP":
                    index = 14;
                    break;
                case "DISP_ENERGY_ACTI_EXP":
                    index = 15;
                    break;
                case "DISP_ENERGY_ACTI_ABS":
                    index = 16;
                    break;
                case "DISP_ENERGY_ACTI_PH":
                    index = 17;
                    break;
                case "DISP_ENERGY_ACTI_NUT":
                    index = 18;
                    break;
                case "DISP_ENERGY_ACTI_P_N_DIFF":
                    index = 19;
                    break;
                case "DISP_POWER_REACT":
                    index = 20;
                    break;
                case "DISP_ENERGY_ACTI":
                    index = 21;
                    break;
                case "DISP_ENERGY_APPI":
                    index = 22;
                    break;
                case "DISP_ENERGY_REACT":
                    index = 23;
                    break;
                case "DISP_ENERGY_REACTI_ACTI":
                    index = 24;
                    break;
                case "DISP_ENERGY_REACTE_ACTI":
                    index = 25;
                    break;
                case "DISP_TAMPER_COUNT":
                    index = 26;
                    break;
                case "DISP_TOU_REGS_kWh":
                    index = 27;
                    break;
                case "DISP_TOU_REGS_kVAh":
                    index = 28;
                    break;
                case "DISP_TOU_REGS_kW":
                    index = 29;
                    break;
                case "DISP_TOU_REGS_kVA":
                    index = 30;
                    break;
                case "DISP_SIGNAL_STATUS":
                    index = 31;
                    break;
                case "DISP_BP1_ENERGY_ACTI":
                    index = 32;
                    break;
                case "DISP_BP1_ENERGY_APPI":
                    index = 33;
                    break;
                case "DISP_BPn_MD_ACT":
                    index = 34;
                    break;
                case "DISP_BPn_MD_APP":
                    index = 35;
                    break;
                case "DISP_BPn_ENERGY_ACTI":
                    index = 36;
                    break;
                case "DISP_BP1_AVG_PF":
                    index = 37;
                    break;
                case "DISP_BP1_MD_ACT":
                    index = 38;
                    break;
                case "DISP_BP1_MD_APP":
                    index = 39;
                    break;
                case "DISP_MD_ACT_TIME":
                    index = 40;
                    break;
                case "DISP_MD_ACT_DATE":
                    index = 41;
                    break;
                case "DISP_MD_APP_TIME":
                    index = 42;
                    break;
                case "DISP_MD_APP_DATE":
                    index = 43;
                    break;
                case "DISP_BP1_MD_ACT_TIME":
                    index = 44;
                    break;
                case "DISP_BP1_MD_ACT_DATE":
                    index = 45;
                    break;
                case "DISP_BP1_MD_APP_TIME":
                    index = 46;
                    break;
                case "DISP_BP1_MD_APP_DATE":
                    index = 47;
                    break;
                case "DISP_MD_RST_CNT":
                    index = 48;
                    break;
                case "DISP_MD_RST_DATE":
                    index = 49;
                    break;
                case "DISP_MD_RST_TIME":
                    index = 50;
                    break;
                case "DISP_METER_SNO":
                    index = 51;
                    break;
                case "DISP_TAMPER_COPEN":
                    index = 52;
                    break;
                case "DISP_TAMPER_COPEN_OCCUR_DATE":
                    index = 53;
                    break;
                case "DISP_TAMPER_COPEN_OCCUR_TIME":
                    index = 54;
                    break;
                case "DISP_TAMPER_TOPEN":
                    index = 55;
                    break;
                case "DISP_TAMPER_TOPEN_OCCUR_TIME":
                    index = 56;
                    break;
                case "DISP_TAMPER_TOPEN_OCCUR_DATE":
                    index = 57;
                    break;
                case "DISP_TOU_REGS":
                    index = 58;
                    break;
                case "DISP_RISING_DEM_ACT":
                    index = 59;
                    break;
                case "DISP_RISING_DEM_APP":
                    index = 60;
                    break;
                case "DISP_RISING_DEM_TIME":
                    index = 61;
                    break;
                case "DISP_CUM_AVG_PF":
                    index = 62;
                    break;
                case "DISP_AVG_PF_BP0":
                    index = 63;
                    break;
                case "DISP_CP_OFFHRS":
                    index = 64;
                    break;
                case "DISP_CP_OFFHRS_BP0":
                    index = 65;
                    break;
                case "DISP_LOADLIMIT":
                    index = 66;
                    break;
                case "DISP_BP1_OFF_HRS":
                    index = 67;
                    break;
                case "DISP_BP1_TOU_REGS_kW":
                    index = 68;
                    break;
                case "DISP_BP1_TOU_REGS_kVA":
                    index = 69;
                    break;
                case "DISP_BATT_STATUS":
                    index = 70;
                    break;
                case "DISP_EEPROM_STATUS":
                    index = 71;
                    break;
                case "DISP_RTC_STATUS":
                    index = 72;
                    break;
                case "DISP_CP_ONHRS":
                    index = 73;
                    break;
                case "DISP_TAMPER":
                    index = 74;
                    break;
                case "DISP_NS":
                    index = 75;
                    break;
                case "DISP_CMD":
                    index = 76;
                    break;
                case "DISP_METER_REVNO":
                    index = 77;
                    break;
                case "DISP_ENERGY_ACTI_HR":
                    index = 78;
                    break;
                case "DISP_ENERGY_APPI_HR":
                    index = 79;
                    break;
                case "DISP_ENERGY_REACTI_ACTI_HR":
                    index = 80;
                    break;
                case "DISP_ENERGY_REACTE_ACTI_HR":
                    index = 81;
                    break;
            }
            return index;
        }

        public string RTC_CheckSum(string msg)
        {
            Int32 AddBytes = 0;
            for (int i = 0; i < msg.Length; i += 2)
                AddBytes = AddBytes + Convert.ToInt32(msg.Substring(i, 2), 16);
            string hex = Convert.ToString(AddBytes, 16).PadLeft(2, '0').ToUpper();
            if (hex.Length >= 3)
            {
                hex = (hex.Substring(hex.Length - 2, 2));
                return hex;
            }
            return hex;
        }

        private void rb_manualdisplay_Click(object sender, EventArgs e)
        {
            if (rb_manualdisplay.Checked == true)
                displayMode = "PUSH";
            else
                displayMode = null;
        }

        private void rb_autodisplay_Click(object sender, EventArgs e)
        {
            if (rb_autodisplay.Checked == true)
                displayMode = "AUTO";
            else
                displayMode = null;
        }

        private void rb_battery_Click(object sender, EventArgs e)
        {

        }

        private void b_enable_Click(object sender, EventArgs e)
        {
            try
            {
                notification.Text = "";
                if (_media.connect2())
                {
                    byte[] buffer = _media.reader.SendAndRecieve(unlock, 400, 12);
                    string response = ASCIIEncoding.ASCII.GetString(buffer);
                    if (response.Contains("UnLocked"))
                    {
                        byte[] reply = _media.reader.SendAndRecieve(enablemode, 400, 4);
                        if (reply[0] == 0x02 && reply[1] == 0x03 && reply[2] == 0x0d && reply[3] == 0x0a)
                        {
                            notification.Text = "Enable mode active";
                        }
                        else
                        {
                            notification.Text = "Error";
                        }
                    }
                    else
                    {
                        notification.Text = "Meter is locked";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            _media.closeMedia();
        }

        private void b_pfail_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt32(tb_pftime1.Text);
            if (value > 59)
            {
                try
                {
                    notification.Text = "";
                    string t1 = Convert.ToInt32(tb_pftime1.Text).ToString("X2").PadLeft(4, '0');
                    //string t2 = Convert.ToInt32(tb_pftime2.Text).ToString("X2").PadLeft(4, '0');                
                    pfail[7] = Convert.ToByte(t1.Substring(0, 2), 16);
                    pfail[8] = Convert.ToByte(t1.Substring(2, 2), 16);
                    //pfail[9] = Convert.ToByte(t2.Substring(0, 2), 16);
                    //pfail[10] = Convert.ToByte(t2.Substring(2, 2), 16);
                    if (_media.connect2())
                    {
                        byte[] buffer = _media.reader.SendAndRecieve(unlock, 400, 12);
                        string response = ASCIIEncoding.ASCII.GetString(buffer);
                        if (response.Contains("UnLocked"))
                        {
                            byte[] reply = _media.reader.SendAndRecieve(pfail, 400, 4);
                            if (reply[0] == 0x02 && reply[1] == 0x03 && reply[2] == 0x0d && reply[3] == 0x0a)
                            {
                                notification.Text = "Power Fail Time Set Successfully";
                            }
                            else
                            {
                                notification.Text = "Error";
                            }
                        }
                        else
                        {
                            notification.Text = "Meter is locked";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                _media.closeMedia();
            }
            else
            {
                MessageBox.Show("Please enter value greater or equal to 60");
            }

        }

        private void b_display_time_Click(object sender, EventArgs e)
        {
            try
            {
                notification.Text = "";
                string t1 = Convert.ToInt32(tb_autotime.Text).ToString("X2").PadLeft(2, '0');
                string t2 = Convert.ToInt32(tb_pushtime.Text).ToString("X2").PadLeft(2, '0');
                string t3 = Convert.ToInt32(tb_energytime.Text).ToString("X2").PadLeft(2, '0');
                display_time[7] = Convert.ToByte(t1.Substring(0, 2), 16);
                display_time[8] = Convert.ToByte(t2.Substring(0, 2), 16);
                display_time[9] = Convert.ToByte(t3.Substring(0, 2), 16);
                if (_media.connect2())
                {
                    byte[] buffer = _media.reader.SendAndRecieve(unlock, 400, 12);
                    string response = ASCIIEncoding.ASCII.GetString(buffer);
                    if (response.Contains("UnLocked"))
                    {
                        byte[] reply = _media.reader.SendAndRecieve(display_time, 400, 4);
                        if (reply[0] == 0x02 && reply[1] == 0x03 && reply[2] == 0x0d && reply[3] == 0x0a)
                        {
                            notification.Text = "Display Time Set Successfully";
                        }
                        else
                        {
                            notification.Text = "Error";
                        }
                    }
                    else
                    {
                        notification.Text = "Meter is locked";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            _media.closeMedia();
        }

        private void b_readsettings_Click_1(object sender, EventArgs e)
        {
            try
            {
                notification.Text = "";
                if (_media.connect2())
                {
                    byte[] buffer = _media.reader.SendAndRecieve(unlock, 400, 12);
                    string response = ASCIIEncoding.ASCII.GetString(buffer);
                    if (response.Contains("UnLocked"))
                    {
                        byte[] reply = _media.reader.SendAndRecieve(readSettings, 800, 69);
                        if (reply != null && reply.Length == 69)
                        {
                            notification.Text = "Settings read successfully";
                            ReadSettings re = new ReadSettings(reply);
                            re.ShowDialog();
                        }
                        else
                        {
                            notification.Text = "No Response";
                        }
                    }
                    else
                    {
                        notification.Text = "Meter is locked";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            _media.closeMedia();
        }

        public byte GetMinutes(string hour)
        {
            int num = Convert.ToInt32(hour);
            int minutes = num * 60;
            string hex = num.ToString("X2");
            byte hx = Convert.ToByte(hex, 16);
            return hx;
        }

        public byte GetHexByte(string val)
        {
            int num = Convert.ToInt32(val);
            string hex = num.ToString("X2");
            byte hx = Convert.ToByte(hex, 16);
            return hx;
        }
        private void b_daylight_Click(object sender, EventArgs e)
        {
            try
            {
                notification.Text = "";
                if (_media.connect2())
                {
                    byte[] daylight = new byte[] { 0x45, 0x6C, 0x53, 0x73, 0x65, 0x74, 0x36, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0D };
                    string RTC1 = dateTimePicker2.Text;
                    DateTime No_Of_Week1 = DateTime.ParseExact(RTC1, "dd/MM/yy HH:mm:ss", null);
                    string yy1 = RTC1.Substring(6, 2).PadLeft(2, '0');
                    string mm1 = RTC1.Substring(3, 2).PadLeft(2, '0');
                    string dd1 = RTC1.Substring(0, 2).PadLeft(2, '0');
                    string hh1 = RTC1.Substring(9, 2).PadLeft(2, '0');
                    string mn1 = RTC1.Substring(12, 2).PadLeft(2, '0');
                    string sec1 = RTC1.Substring(15, 2).PadLeft(2, '0');
                    string wk1 = ((int)No_Of_Week1.DayOfWeek).ToString().PadLeft(2, '0');

                    string RTC2 = dateTimePicker3.Text;
                    DateTime No_Of_Week2 = DateTime.ParseExact(RTC2, "dd/MM/yy HH:mm:ss", null);
                    string yy2 = RTC2.Substring(6, 2).PadLeft(2, '0');
                    string mm2 = RTC2.Substring(3, 2).PadLeft(2, '0');
                    string dd2 = RTC2.Substring(0, 2).PadLeft(2, '0');
                    string hh2 = RTC2.Substring(9, 2).PadLeft(2, '0');
                    string mn2 = RTC2.Substring(12, 2).PadLeft(2, '0');
                    string sec2 = RTC2.Substring(15, 2).PadLeft(2, '0');
                    string wk2 = ((int)No_Of_Week2.DayOfWeek).ToString().PadLeft(2, '0');

                    string devition = dt_deviationHour.Text.Substring(0, 2).PadLeft(2, '0');

                    daylight[7] = GetHexByte(dd1);
                    daylight[8] = GetHexByte(mm1);
                    daylight[9] = GetHexByte(yy1);
                    daylight[10] = GetHexByte(hh1);
                    daylight[11] = GetHexByte(mn1);
                    daylight[12] = GetHexByte(sec1);
                    daylight[13] = GetHexByte(wk1);
                    daylight[14] = GetHexByte(dd2);
                    daylight[15] = GetHexByte(mm2);
                    daylight[16] = GetHexByte(yy2);
                    daylight[17] = GetHexByte(hh2);
                    daylight[18] = GetHexByte(mn2);
                    daylight[19] = GetHexByte(sec2);
                    daylight[20] = GetHexByte(wk2);
                    //daylight[21] = 0x3c;
                    if (devition == "00")
                        daylight[21] = 0x3c;
                    else
                        daylight[21] = GetHexByte(devition);
                    byte[] buffer = _media.reader.SendAndRecieve(unlock, 400, 12);
                    string response = ASCIIEncoding.ASCII.GetString(buffer);
                    if (response.Contains("UnLocked"))
                    {
                        byte[] reply = _media.reader.SendAndRecieve(daylight, 400, 4);
                        if (reply[0] == 0x02 && reply[1] == 0x03 && reply[2] == 0x0d && reply[3] == 0x0a)
                        {
                            notification.Text = "Daylight savings set successfully";
                        }
                        else
                        {
                            notification.Text = "Error";
                        }
                    }
                    else
                    {
                        notification.Text = "Meter is locked";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            _media.closeMedia();
        }

        private void b_read_dcontrol_Click(object sender, EventArgs e)
        {
            try
            {
                notification.Text = "";
                if (_media.connect())
                {
                    rb_tarrif.Text = "";
                    GXDLMSDisconnectControl d_control = new GXDLMSDisconnectControl();
                    var control = _media.reader.Read2(d_control, 3); //Control state                   
                    _media.closeMedia();
                    notification.Text = control.ToString().Trim();
                }
                else
                {
                    notification.Text = "Disconnected mode";
                }
            }
            catch (Exception ex)
            {
                notification.Text = ex.Message; _media.closeMedia();
            }
        }

        private void b_disconnect_Click(object sender, EventArgs e)
        {
            try
            {
                notification.Text = "";
                if (_media.connect())
                {
                    GXReplyData reply = new GXReplyData();
                    GXDLMSDisconnectControl dc = new GXDLMSDisconnectControl();

                    bool status = _media.reader.ReadDataBlock(dc.RemoteDisconnect(InitializeMedia._client), reply);
                    _media.closeMedia();
                    if (status)
                        notification.Text = "Disconnection Successfull";
                    else
                        notification.Text = "Control state Error";
                }
                else
                {
                    notification.Text = "Disconnected mode";
                }
            }
            catch (Exception ex)
            {
                notification.Text = ex.Message; _media.closeMedia();
            }
        }

        private void b_reconnect_Click(object sender, EventArgs e)
        {
            try
            {
                notification.Text = "";
                if (_media.connect())
                {
                    GXReplyData reply = new GXReplyData();
                    GXDLMSDisconnectControl dc = new GXDLMSDisconnectControl();

                    bool status = _media.reader.ReadDataBlock(dc.RemoteReconnect(InitializeMedia._client), reply);
                    _media.closeMedia();
                    if (status)
                        notification.Text = "Reconnection Successfull";
                    else
                        notification.Text = "Control state Error";
                }
                else
                {
                    notification.Text = "Disconnected mode";
                }
            }
            catch (Exception ex)
            {
                notification.Text = ex.Message; _media.closeMedia();
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            try
            {
                Array.Clear(Program.ListItems, 0, Program.ListItems.Length);
                listBox2.Items.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void rb_autotarrif_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void rb_manualtarrif_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void b_coveropen_Click(object sender, EventArgs e)
        {
            try
            {
                notification.Text = "";
                if (_media.connect2())
                {
                    byte[] buffer = _media.reader.SendAndRecieve(unlock, 400, 12);
                    // byte[] buffer2 = _media.reader.ReadTCP(unlock, null);
                    string response = ASCIIEncoding.ASCII.GetString(buffer);
                    if (response.Contains("UnLocked"))
                    {
                        byte[] reply = _media.reader.SendAndRecieve(cl_coveropen, 400, 4);
                        if (reply[0] == 0x02 && reply[1] == 0x03 && reply[2] == 0x0d && reply[3] == 0x0a)
                        {
                            notification.Text = "Cover Open Tamper Cleared";
                        }
                        else
                        {
                            notification.Text = "Error";
                        }
                    }
                    else
                    {
                        notification.Text = "Meter is locked";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            _media.closeMedia();
        }

        private void b_mdipSet_Click(object sender, EventArgs e)
        {
            try
            {
                notification.Text = "";
                if (_media.connect())
                {
                    UInt16 val = Convert.ToUInt16(cb_mdipinterval.Text.Trim());
                    //switch (cb_mdipinterval.Text.Trim())
                    //{
                    //    case "15":
                    //        val = (UInt16)900;
                    //        break;
                    //    case "30":
                    //        val = (UInt16)900;
                    //        break;
                    //}
                    GXDLMSData mdip = new GXDLMSData("1.0.0.8.0.255");
                    mdip.Value = val;
                    var md = _media.reader.Write(mdip, 2);
                    _media.closeMedia();
                    if (md.ToString().Trim() == "True")
                        notification.Text = "Set Successfully";
                    else
                        notification.Text = "This value cannot be set";
                }
                else
                {
                    notification.Text = "Disconnected mode";
                }
            }
            catch (Exception ex)
            {
                notification.Text = ex.Message; _media.closeMedia();
            }

        }

        private void b_mdipRead_Click(object sender, EventArgs e)
        {
            try
            {
                notification.Text = "";
                if (_media.connect())
                {
                    GXDLMSData mdip = new GXDLMSData("1.0.0.8.0.255");
                    var md = _media.reader.ReadObis(mdip, 2);
                    _media.closeMedia();
                    notification.Text = md.ToString().Trim();
                }
                else
                {
                    notification.Text = "Disconnected mode";
                }
            }
            catch (Exception ex)
            {
                notification.Text = ex.Message; _media.closeMedia();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                notification.Text = "";
                if (_media.connect())
                {
                    GXDLMSData lsp1 = new GXDLMSData("1.0.0.8.4.255");
                    var md = _media.reader.ReadObis(lsp1, 2);
                    _media.closeMedia();
                    notification.Text = md.ToString().Trim();
                }
                else
                {
                    notification.Text = "Disconnected mode";
                }
            }
            catch (Exception ex)
            {
                notification.Text = ex.Message; _media.closeMedia();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                notification.Text = "";
                if (_media.connect())
                {
                    GXDLMSData mdip = new GXDLMSData("1.0.0.8.5.255");
                    var lsp2 = _media.reader.ReadObis(mdip, 2);
                    _media.closeMedia();
                    notification.Text = lsp2.ToString().Trim();
                }
                else
                {
                    notification.Text = "Disconnected mode";
                }
            }
            catch (Exception ex)
            {
                notification.Text = ex.Message; _media.closeMedia();
            }
        }

        private void b_lsp1Set_Click(object sender, EventArgs e)
        {
            try
            {
                notification.Text = "";
                if (_media.connect())
                {
                    UInt16 val = Convert.ToUInt16(cb_lsipInterval.Text.Trim());
                    GXDLMSData lsip1 = new GXDLMSData("1.0.0.8.4.255");
                    lsip1.Value = val;
                    var md = _media.reader.Write(lsip1, 2);
                    _media.closeMedia();
                    if (md.ToString().Trim() == "True")
                        notification.Text = "Set Successfully";
                    else
                        notification.Text = "This value cannot be set";
                }
                else
                {
                    notification.Text = "Disconnected mode";
                }
            }
            catch (Exception ex)
            {
                notification.Text = ex.Message; _media.closeMedia();
            }
        }

        private void b_mdreset_Click(object sender, EventArgs e)
        {
            try
            {
                notification.Text = "";
                if (_media.connect())
                {
                    GXDLMSClock clock = new GXDLMSClock("0.0.1.0.0.255");
                    //object rtc = _media.reader.ReadObis(clock, 2);
                    GXDateTime gXDateTime = (GXDateTime)_media.reader.ReadObis(clock, 2);
                    //reader.Disconnect();
                    //notification.Text = gXDateTime.ToString();
                    //string RTC = dateTimePicker1.Text;
                    //string year = RTC.Substring(6, 2);
                    //string mon = RTC.Substring(3, 2);
                    //string day = RTC.Substring(0, 2);
                    //string hour = RTC.Substring(9, 2);
                    //string min = RTC.Substring(12, 2);
                    //string sec = RTC.Substring(15, 2);                
                    //GXDateTime resetdate = new GXDateTime(Convert.ToInt32(year), Convert.ToInt32(mon), Convert.ToInt32(day), Convert.ToInt32(hour), Convert.ToInt32(min), Convert.ToInt32(sec), 0);                   
                    GXDLMSActionSchedule endBillingPeriod = new GXDLMSActionSchedule("0.0.15.0.0.255");
                    endBillingPeriod.ExecutionTime = new GXDateTime[] { new GXDateTime(gXDateTime) };
                    if (_media.reader.Write(endBillingPeriod, 4))
                    { notification.Text = "MD reset successfully"; }
                    else
                    { notification.Text = "MD Reset Eerror,Try again"; }
                    _media.closeMedia();
                }
                else
                {
                    notification.Text = "Disconnected mode";
                }
            }
            catch (Exception ex)
            {
                notification.Text = ex.Message; _media.closeMedia();
            }
        }

        private void b_import_Click(object sender, EventArgs e)
        {
            try
            {
                notification.Text = "";
                if (_media.connect())
                {
                    string energy = "";
                    GXDLMSData import = new GXDLMSData("0.0.94.96.19.255");
                    var im = _media.reader.ReadObis(import, 2);
                    _media.closeMedia();
                    switch (im.ToString())
                    {
                        case "0":
                            energy = "Export Energy ";
                            break;
                        case "1":
                            energy = "Import Energy ";
                            break;
                    }
                    notification.Text = energy + "Value = " + im.ToString().Trim();
                }
                else
                {
                    notification.Text = "Disconnected mode";
                }
            }
            catch (Exception ex)
            {
                notification.Text = ex.Message; _media.closeMedia();
            }
        }

        private void b_angle_Click(object sender, EventArgs e)
        {
            try
            {
                notification.Text = "";
                if (_media.connect())
                {
                    GXDLMSRegister angle = new GXDLMSRegister("1.0.81.7.4.255");
                    var value = _media.reader.ReadObis(angle, 2);
                    var scaler = _media.reader.ReadObis(angle, 3);
                    _media.closeMedia();
                    UInt32 aa = (UInt32)value;
                    string res = ((float)aa / 10).ToString();
                    notification.Text = "Value = " + res + "  " + scaler.ToString().Trim();
                }
                else
                {
                    notification.Text = "Disconnected mode";
                }
            }
            catch (Exception ex)
            {
                notification.Text = ex.Message; _media.closeMedia();
            }
        }

        private void b_activeL1_Click(object sender, EventArgs e)
        {
            try
            {
                notification.Text = "";
                if (_media.connect())
                {
                    GXDLMSRegister L1 = new GXDLMSRegister("1.0.35.8.0.255");
                    var value = _media.reader.ReadObis(L1, 2);
                    var scaler = _media.reader.ReadObis(L1, 3);
                    _media.closeMedia();
                    notification.Text = "Value = " + value.ToString().Trim() + "  " + scaler.ToString().Trim();
                }
                else
                {
                    notification.Text = "Disconnected mode";
                }
            }
            catch (Exception ex)
            {
                notification.Text = ex.Message; _media.closeMedia();
            }
        }

        private void b_activeL2_Click(object sender, EventArgs e)
        {
            try
            {
                notification.Text = "";
                if (_media.connect())
                {
                    GXDLMSRegister L1 = new GXDLMSRegister("1.0.55.8.0.255");
                    var value = _media.reader.ReadObis(L1, 2);
                    var scaler = _media.reader.ReadObis(L1, 3);
                    _media.closeMedia();
                    notification.Text = "Value = " + value.ToString().Trim() + "  " + scaler.ToString().Trim();
                }
                else
                {
                    notification.Text = "Disconnected mode";
                }
            }
            catch (Exception ex)
            {
                notification.Text = ex.Message; _media.closeMedia();
            }
        }

        private void cb_zone1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cb_hh2.Items.Clear();
            int val = Convert.ToInt32(cb_hh1.Text.Trim());
            for (int i = val + 1; i < 24; i++)
            {
                string item = i.ToString().PadLeft(2, '0');
                cb_hh2.Items.Add(item);
            }
        }

        private void ck_zone1_CheckedChanged(object sender, EventArgs e)
        {
            if (ck_zone1.Checked == true)
            {
                cb_hh2.Enabled = true; cb_mn2.Enabled = true; ck_zone2.Enabled = true;
            }
            else
            {
                cb_hh2.Enabled = false; cb_mn2.Enabled = false; ck_zone2.Enabled = false;
            }

        }

        private void ck_zone2_CheckedChanged(object sender, EventArgs e)
        {
            if (ck_zone2.Checked == true)
            {
                cb_hh3.Enabled = true; cb_mn3.Enabled = true; ck_zone3.Enabled = true;
            }
            else
            {
                cb_hh3.Enabled = false; cb_mn3.Enabled = false; ck_zone3.Enabled = false;
            }
        }

        private void ck_zone3_CheckedChanged(object sender, EventArgs e)
        {
            if (ck_zone3.Checked == true)
            {
                cb_hh4.Enabled = true; cb_mn4.Enabled = true; ck_zone4.Enabled = true;
            }
            else
            {
                cb_hh4.Enabled = false; cb_mn4.Enabled = false; ck_zone4.Enabled = false;
            }
        }

        private void b_read_daylight_Click(object sender, EventArgs e)
        {
            try
            {
                notification.Text = "";
                if (_media.connect())
                {
                    GXDLMSClock clock = new GXDLMSClock();
                    object begin = _media.reader.ReadObis(clock, 5);//Bigin
                    object end = _media.reader.ReadObis(clock, 6);//End
                    object deviation = _media.reader.ReadObis(clock, 7);//End       
                    notification.Text = "BEGIN = " + begin.ToString() + "  END = " + end.ToString() + "  DEVIATION = " + deviation.ToString();
                    _media.closeMedia();
                }
                else
                {
                    notification.Text = "Disconnected mode";
                }
            }
            catch (Exception ex)
            {
                notification.Text = ex.Message; _media.closeMedia();
            }
        }

        private void b_terminalset_Click(object sender, EventArgs e)
        {
            try
            {
                notification.Text = "";
                string t1 = Convert.ToInt64(tb_terminalTimeout.Text).ToString("X8").PadLeft(8, '0');
                string val = (t1.Length > 8) ? t1.Substring(t1.Length - 8, 8) : t1;
                terminalcover[7] = Convert.ToByte(val.Substring(0, 2), 16);
                terminalcover[8] = Convert.ToByte(val.Substring(2, 2), 16);
                terminalcover[9] = Convert.ToByte(val.Substring(4, 2), 16);
                terminalcover[10] = Convert.ToByte(val.Substring(6, 2), 16);
                if (_media.connect2())
                {
                    byte[] buffer = _media.reader.SendAndRecieve(unlock, 400, 12);
                    string response = ASCIIEncoding.ASCII.GetString(buffer);
                    if (response.Contains("UnLocked"))
                    {
                        byte[] reply = _media.reader.SendAndRecieve(terminalcover, 400, 4);
                        if (reply[0] == 0x02 && reply[1] == 0x03 && reply[2] == 0x0d && reply[3] == 0x0a)
                        {
                            notification.Text = "Terminalcover Timeout Set Successfully";
                        }
                        else
                        {
                            notification.Text = "Error";
                        }
                    }
                    else
                    {
                        notification.Text = "Meter is locked";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            _media.closeMedia();
        }

        private void b_importSet_Click(object sender, EventArgs e)
        {
            try
            {
                notification.Text = "";
                if (_media.connect())
                {
                    string energy = "";
                    byte val = 0000;
                    GXDLMSData import = new GXDLMSData("0.0.94.96.19.255");
                    var im = _media.reader.ReadObis(import, 2);
                    switch (im.ToString())
                    {
                        case "1":
                            val = Convert.ToByte("0"); energy = "Export Energy ";
                            break;
                        case "0":
                            val = Convert.ToByte("1"); energy = "Import Energy ";
                            break;
                    }
                    import.Value = val;
                    bool status = _media.reader.Write(import, 2);
                    _media.closeMedia();
                    if (status.ToString().Trim() == "True")
                        notification.Text = energy + "Set Successfully";
                    else
                        notification.Text = "This value cannot be set";
                }
                else
                {
                    notification.Text = "Disconnected mode";
                }
            }
            catch (Exception ex)
            {
                notification.Text = ex.Message; _media.closeMedia();
            }
        }

        private void cb_hh2_SelectedIndexChanged(object sender, EventArgs e)
        {
            cb_hh3.Items.Clear();
            int val = Convert.ToInt32(cb_hh2.Text.Trim());
            for (int i = val + 1; i < 24; i++)
            {
                string item = i.ToString().PadLeft(2, '0');
                cb_hh3.Items.Add(item);
            }
        }

        private void cb_hh3_SelectedIndexChanged(object sender, EventArgs e)
        {
            cb_hh4.Items.Clear();
            int val = Convert.ToInt32(cb_hh3.Text.Trim());
            for (int i = val + 1; i < 24; i++)
            {
                string item = i.ToString().PadLeft(2, '0');
                cb_hh4.Items.Add(item);
            }
        }

        private void b_settemper_Click(object sender, EventArgs e)
        {
            try
            {
                notification.Text = ""; richTextBox1.Text = "";
                if (_media.connect())
                {
                    GXDLMSData tampers = new GXDLMSData("0.0.94.91.26.255");
                    object val = _media.reader.ReadObis(tampers, 2);
                    if (val != null)
                    {
                        string bitString = val.ToString();
                        string tempers = "";
                        for (int i = 0; i < bitString.Length; i++)
                        {
                            if (bitString[i] == '1')
                            {
                                MeterEvents _event = new MeterEvents();
                                _event.EventName = utility.getTemperName(i);
                                tempers = tempers + _event.EventName + "\r\n";
                            }
                        }
                        notification.Text = "Tempers for notification read successfully";
                        richTextBox1.Text = tempers;
                    }
                    _media.closeMedia();
                }
                else
                {
                    notification.Text = "Disconnected mode";
                }
            }
            catch (Exception ex)
            {
                notification.Text = ex.Message; _media.closeMedia();
            }
        }

        private void b_settemperNotification_Click(object sender, EventArgs e)
        {
            try
            {
                notification.Text = "";
                string[] bitsArr = new string[128] { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0",
                 "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0",
                 "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0",
                 "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0",
                 "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0"};

                string bitString = "";
                if (ch_tm0.Checked == true) bitsArr[0] = "1";
                if (ch_tm1.Checked == true) bitsArr[1] = "1";
                if (ch_tm2.Checked == true) bitsArr[2] = "1";
                if (ch_tm3.Checked == true) bitsArr[3] = "1";
                if (ch_tm4.Checked == true) bitsArr[4] = "1";
                if (ch_tm5.Checked == true) bitsArr[5] = "1";
                if (ch_tm6.Checked == true) bitsArr[6] = "1";
                if (ch_tm7.Checked == true) bitsArr[7] = "1";
                if (ch_tm8.Checked == true) bitsArr[8] = "1";
                if (ch_tm9.Checked == true) bitsArr[9] = "1";
                if (ch_tm10.Checked == true) bitsArr[10] = "1";
                if (ch_tm11.Checked == true) bitsArr[11] = "1";
                if (ch_tm12.Checked == true) bitsArr[12] = "1";
                if (ch_tm51.Checked == true) bitsArr[51] = "1";
                if (ch_tm81.Checked == true) bitsArr[81] = "1";
                if (ch_tm82.Checked == true) bitsArr[82] = "1";
                if (ch_tm83.Checked == true) bitsArr[83] = "1";
                if (ch_tm84.Checked == true) bitsArr[84] = "1";
                if (ch_tm85.Checked == true) bitsArr[85] = "1";
                if (ch_tm86.Checked == true) bitsArr[86] = "1";
                if (ch_tm87.Checked == true) bitsArr[87] = "1";
                for (int i = 0; i < bitsArr.Length; i++)
                {
                    bitString += bitsArr[i];
                }
                int len = bitString.Length;
                object bits = (object)bitString;
                if (_media.connect())
                {
                    GXDLMSData d = new GXDLMSData("0.0.94.91.26.255");
                    d.SetDataType(2, DataType.BitString);
                    d.Value = bits;
                    if (_media.reader.Write(d, 2)) { notification.Text = "Notifications activated successfully"; }
                    else { notification.Text = "Notifications not set,Try again"; }
                    _media.closeMedia();
                }
                else
                {
                    notification.Text = "Disconnected mode";
                }
            }
            catch (Exception ex)
            {
                notification.Text = ex.Message; _media.closeMedia();
            }
        }

        private void ck_zone4_CheckedChanged(object sender, EventArgs e)
        {
            if (ck_zone4.Checked == true)
            {
                cb_hh4.Enabled = true; cb_mn4.Enabled = true; b_set_dayprofile.Enabled = true;
            }
            else
            {
                cb_hh4.Enabled = false; cb_mn4.Enabled = false; b_set_dayprofile.Enabled = false;
            }
        }

        private void b_readPort_Click(object sender, EventArgs e)
        {
            try
            {
                notification.Text = ""; tb_port.Text = "";
                if (_media.connect())
                {
                    GXDLMSData gprsPort = new GXDLMSData("0.128.162.50.128.255");
                    _media.reader.ReadObis(gprsPort, 2).ToString();
                    var valArray = gprsPort.GetValues();
                    byte[] buff1 = GXCommon.GetAsByteArray(valArray[1]);
                    string port = ByteToChar(buff1, 0, buff1.Length);
                    notification.Text = port;
                    tb_port.Text = port;
                    _media.closeMedia();
                }
                else
                {
                    notification.Text = "Disconnected mode";
                }
            }
            catch (Exception ex)
            {
                notification.Text = ex.Message;
                _media.closeMedia();
            }
        }

        public string ByteToChar(byte[] data, int start, int length)
        {
            try
            {
                string str = "";
                for (int i = start; i < (start + length); i++)
                {
                    str = str + (char)(data[i]);
                }
                str = System.Text.RegularExpressions.Regex.Replace(str, @"[^\u0000-\u007F]+", string.Empty).Replace("\0", "");
                return str;
            }
            catch
            {
                return null;
            }
        }

        public Int32 ByteToInt(byte[] data, int start, int length)
        {
            Int32 result = 0;
            string str = "";
            for (int i = start; i < (start + length); i++)
            {
                str = str + (data[i].ToString(""));
            }
            result = unchecked(Convert.ToInt32(str, 16));
            return result;
        }

        private void b_disableEnergy_Click(object sender, EventArgs e)
        {
            try
            {
                notification.Text = "";
                if (_media.connect2())
                {
                    byte[] buffer = _media.reader.SendAndRecieve(unlock, 400, 12);
                    string response = ASCIIEncoding.ASCII.GetString(buffer);
                    if (response.Contains("UnLocked"))
                    {
                        byte[] reply = _media.reader.SendAndRecieve(enable_disableEnergy, 400, 4);
                        if (reply[0] == 0x02 && reply[1] == 0x03 && reply[2] == 0x0d && reply[3] == 0x0a)
                        {
                            notification.Text = "Success";
                        }
                        else
                        {
                            notification.Text = "Error";
                        }
                    }
                    else
                    {
                        notification.Text = "Meter is locked";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            _media.closeMedia();
        }

        //public void imageTransfer()//For threading purpose
        //{
        //    try
        //    {
        //        GXDLMSImageTransfer imageTransfer = new GXDLMSImageTransfer();
        //        byte[] ident = ASCIIEncoding.ASCII.GetBytes(tb_identification.Text.Trim());
        //        if (InvokeRequired)
        //        {
        //            this.Invoke(new MethodInvoker(delegate
        //            {

        //                _media.reader.ImageUpdate(imageTransfer, ident, FirmwareBytes);
        //               // _isRunning = false;
        //                MessageBox.Show("Firmware Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                //Program.fUpdate = false;
        //                notification.Text = "Updation Completed Successfully";

        //            }));
        //        }
        //        else
        //        {
        //            // Your code here, like set text box content or get text box contents etc..
        //            // SAME CODE AS ABOVE
        //        }
        //        //_media.reader.Disconnect();
        //        _media.reader.WaitTime = 20000;
        //        //return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        //return false;
        //    }            
        //}

        private void b_upImage_Click(object sender, EventArgs e)
        {
            try
            {
                notification.Text = "";
                _media.Set_InvocationCounter("0.0.43.1.5.255");// _media.dissconnect4Firmware();
                Program.fUpdate = true;
                if (_media.connct4Firmware())
                {
                    System.Threading.Tasks.Task myTask = new System.Threading.Tasks.Task(() =>
                    {
                        try
                        {
                            this.Invoke(new MethodInvoker(delegate
                            {
                                notification.Text = "Firmware Upgrade in process.."; b_upImage.Enabled = false; b_browse.Enabled = false;
                            }));
                            GXDLMSImageTransfer imageTransfer = new GXDLMSImageTransfer();
                            byte[] ident = ASCIIEncoding.ASCII.GetBytes(tb_identification.Text.Trim());
                            _media.reader.ImageUpdate(imageTransfer, ident, FirmwareBytes);
                            _media.reader.WaitTime = 20000;
                            MessageBox.Show("Firmware Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Invoke(new MethodInvoker(delegate
                            {
                                notification.Text = "Updation Completed Successfully"; b_upImage.Enabled = true; b_browse.Enabled = true;
                            }));
                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Invoke(new MethodInvoker(delegate
                            {
                                notification.Text = "Firmware process stopped,try again"; b_upImage.Enabled = true; b_browse.Enabled = true;
                            }));
                        }
                    });
                    myTask.Start();
                    /* old code
                    GXDLMSImageTransfer imageTransfer = new GXDLMSImageTransfer();
                      byte[] ident = ASCIIEncoding.ASCII.GetBytes(tb_identification.Text.Trim());
                      _media.reader.ImageUpdate(imageTransfer, ident, FirmwareBytes);
                    _media.reader.Disconnect();
                     _media.reader.WaitTime = 20000;*/
                    //_media.dissconnect4Firmware();
                    //_media.Set_InvocationCounter("0.0.43.1.3.255");

                }
                else
                {
                    notification.Text = "Disconnected mode";
                }
            }
            catch (Exception ex)
            {
                notification.Text = ex.Message; _media.closeMedia(); //_media.changeClientAddress(48);
            }
        }

        private void b_browse_Click(object sender, EventArgs e)
        {
            browserFileName = ""; FirmwareBytes = null;
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "BIN |*.bin";
            if (d.ShowDialog() == DialogResult.OK)
            {
                browserFileName = d.FileName;
                tb_browse.Text = browserFileName;
            }
            FirmwareBytes = getBuffer(d.FileName);//File.ReadAllBytes(browserFileName);
            //byte[] buffer = getBuffer(d.FileName);
        }

        private byte[] getBuffer(string fileLocation)
        {
            byte[] buffer = null;
            try
            {
                FileStream fileStream = new FileStream(fileLocation, FileMode.Open, FileAccess.Read);

                int length = (int)fileStream.Length;   // get file length
                buffer = new Byte[length];            // create buffer    
                BinaryReader br = new BinaryReader(fileStream);
                buffer = br.ReadBytes((int)length);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return buffer;
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            // set the current caret position to the end
            richTextBox2.SelectionStart = richTextBox2.Text.Length;
            // scroll it automatically
            richTextBox2.ScrollToCaret();
        }

        private void b_stop_Click(object sender, EventArgs e)
        {
            try
            {
                //_isRunning = false;
                _media.reader.Disconnect();
                b_upImage.Enabled = true; b_browse.Enabled = true;
            }
            catch
            { }
        }

        private void b_writePort_Click(object sender, EventArgs e)
        {
            if (Program.selectedAuth != "High")
            { notification.Text = "APN cannot be set in " + Program.selectedAuth + " authentication mode"; }
            else
            {
                try
                {
                    notification.Text = "";
                    if (_media.connect())
                    {
                        GXDLMSData gprsPort = new GXDLMSData("0.128.162.50.128.255"); //GXDLMSGprsSetup gprsSetup = new GXDLMSGprsSetup(); //M2M.METERIPV6                                      
                        gprsPort.Value = Encoding.ASCII.GetBytes(tb_port.Text.Trim());
                        if (_media.reader.Write(gprsPort, 2)) { notification.Text = "Port set successfully"; }
                        else { notification.Text = "Port not set,Try again"; }
                        _media.closeMedia();
                    }
                    else
                    {
                        notification.Text = "Disconnected mode";
                    }
                }
                catch (Exception ex)
                {
                    notification.Text = ex.Message; _media.closeMedia();
                }
            }
        }

        private void cb_hh4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
