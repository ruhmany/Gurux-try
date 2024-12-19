using Gurux.Common;
using Gurux.Net;

namespace Gurux_Testing
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //[STAThread]
        //static void Main()
        //{
        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);
        //    //Application.Run(new MainForm());
        //    Application.Run(new Login_Form());
        //    //Application.Run(new DlmsSettings());
        //    //Application.Run(new Gprs_Form());
        //    //Application.Run(new PushListener());
        //    //Application.Run(new MeterSettings());
        //}
        public static string Port_Status = "CLOSE";
        public static string Mediatype = "S E R I A L";
        public static string password = "";
        public static string selectedPort = "";
        public static string DownloadProfile = "";
        public static string selectedAuth = "None";
        public static bool _connected = false;
        public static bool fUpdate = false;
        public static bool dbStatus = false;
        public static int iCounter = 0;
        public static bool counter_status = false;
        public static int entriesInUse = 0;
        public static int totalRecords = 0;
        public static int pageSize = 0;
        public static string FileName = "";
        public static string[] ListItems = null;
        public static bool serverMedia = false;
        public static IGXMedia serverGX = null;
        public static List<GXNet> clientsList = new List<GXNet>();
        public static List<string> addressList = new List<string>();
        public static string Default_Target_Directory = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\DLMS2_DATA";
        //public static string Connection = "Server=67.220.182.98;Database=sewedyin_smartmeter;Uid=sewedyin_varun;Pwd=meter123456";
        public static string Connection = "Server=localhost;Database=db_elsewedy2;Uid=root;Pwd=";
    }
}
