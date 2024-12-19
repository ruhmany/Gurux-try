using Gurux.DLMS.Objects;
using System;
using System.Collections;
using System.Data;
using System.IO;

namespace Gurux_Testing
{
    /// <summary>
    /// This was MainForm in BCS project
    /// </summary>
    public class ConnectionControl
    {

        InitializeMedia _initialize = new InitializeMedia();
        MediaSettings _media;
        Function function = new Function();
        DataTable dtSource = new DataTable();
        public static Gurux.Serial.GXSerial GX;
        string profileName = "";
        string serialNumber = "";
        string FromDate = "";
        string ToDate = "";
        public ConnectionControl(ConnectionSettings connectionSettings)
        {
            _media = new MediaSettings(connectionSettings);
        }
        public object[] ReadProfile(string profile)
        {
            object[] result = null;
            try
            {
                //if (Program._connected)
                //{
                string obValue = "";
                string scalerobis = "";
                bool scalerprofile = false;
                profileName = profile;
                ArrayList Values = null; ArrayList Obis = null;
                ArrayList ScalerValue = null; ArrayList ScalerObis = null;
                switch (profile)  /// neeraj code for obis
                {
                    case "Nameplate":
                        obValue = function.getObis(profile);
                        break;
                    case "Instant":
                        obValue = function.getObis(profile);
                        scalerprofile = true;
                        scalerobis = function.getObis("InstantScaler");
                        break;
                    case "Billing":
                        obValue = function.getObis(profile);
                        scalerprofile = true;
                        scalerobis = function.getObis("BillingScaler");
                        break;
                    case "BlockLoadProfile":
                        obValue = function.getObis(profile);
                        scalerprofile = true;
                        scalerobis = function.getObis("BlockLoadScaler");
                        break;
                    case "DailyLoadProfile":
                        obValue = function.getObis(profile);
                        scalerprofile = true;
                        scalerobis = function.getObis("DailyLoadScaler");
                        break;
                    case "VoltageEvent":
                        obValue = function.getObis(profile);
                        scalerprofile = true;
                        scalerobis = function.getObis("IndianEvents");
                        break;
                    case "CurrentEvent":
                        obValue = function.getObis(profile);
                        scalerprofile = true;
                        scalerobis = function.getObis("IndianEvents");
                        break;
                    case "PowerFailEvent":
                        obValue = function.getObis(profile);
                        scalerprofile = true;
                        scalerobis = function.getObis("IndianEvents");
                        break;
                    case "TransactionEvent":
                        obValue = function.getObis(profile);
                        scalerprofile = true;
                        scalerobis = function.getObis("IndianEvents");
                        break;
                    case "OtherEvent":
                        obValue = function.getObis(profile);
                        scalerprofile = true;
                        scalerobis = function.getObis("IndianEvents");
                        break;
                    case "CoverOpenEvent":
                        obValue = function.getObis(profile);
                        scalerprofile = true;
                        scalerobis = function.getObis("IndianEvents");
                        break;
                }

                //_media.connectServer();
                _media.connect();
                Program.entriesInUse = Convert.ToInt32(_media.reader.GetEntries(obValue, 7));
                if (Program.entriesInUse >= 1)
                {
                    GXDLMSData serial = new GXDLMSData("0.0.96.1.0.255");//Serial number                                        
                    serialNumber = _media.reader.ReadObis(serial, 2).ToString();


                    Values = _media.reader.ReadDlms(obValue, "Genric");
                    //result = _media.reader.ReadDLMS(obValue);
                    Obis = _media.reader.ReadDlms(obValue, "Obis");
                    Program.totalRecords = Values.Count;
                    Program.pageSize = (Program.totalRecords / Program.entriesInUse);
                    if (scalerprofile)
                    {
                        ScalerValue = _media.reader.ReadDlms(scalerobis, "Scaler");
                        ScalerObis = _media.reader.ReadDlms(scalerobis, "Obis");
                    }
                    if (!Program.serverMedia)
                        _media.reader.Disconnect();
                    Program._connected = false;
                    bool _success = function.decodeAllData(Obis, Values, ScalerValue, ScalerObis, Program.entriesInUse);

                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                _media.closeMedia();
            }
            _media.closeMedia();
            return result;
        }

        private void CreateFile()
        {
            Program.FileName = serialNumber + "#" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + "$" + profileName + ".txt";
            string filePath = Program.Default_Target_Directory + "\\" + Program.FileName;
            WriteIntoFile(filePath, profileName + "|" + Program.entriesInUse + "|" + Program.totalRecords);
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

        class Records2
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public string Obis { get; set; }
            public string Unit { get; set; }
        }

        public void ReadGenericProfile()
        {
            //GXDLMSProfileGeneric p = new GXDLMSProfileGeneric("0.0.94.91.10.255");//Nameplate
            GXDLMSProfileGeneric profile = new GXDLMSProfileGeneric("1.0.94.91.0.255");//Instant
                                                                                       //reader = new GXDLMSReader(settings.client, settings.media, settings.trace);
                                                                                       // settings.media.Open();
            _media.reader.InitializeConnection();
            _media.reader.GetAssociationView(true);
            object[] val = _media.reader.ReadRowsByEntry(profile, 1, 1);
            _media.reader.Disconnect();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
        }

        private void CloseConnection()
        {
            _media.reader.Close();
            Program._connected = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GXDLMSProfileGeneric it = new GXDLMSProfileGeneric("0.0.96.1.0.255");
            GXDLMSPushSetup push = new GXDLMSPushSetup();
            GXDLMSData dldata = new GXDLMSData();
            GXDLMSTcpUdpSetup tcp = new GXDLMSTcpUdpSetup();

            object[] aa = tcp.GetValues();
            object[] bb = it.GetValues();
            object bbc = it.GetType();
        }

    }
}
