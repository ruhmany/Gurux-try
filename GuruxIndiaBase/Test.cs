using System.Text;

namespace Gurux_Testing
{
    public class Test
    {
        public IGXMedia mediagp = new GXNet();
        public GXNet gprs = null;
        public GXDLMSSecureClient _client = new GXDLMSSecureClient(true);
        public Test()
        {
            InitializeComponent();
            _client.ServerAddress = 1;
            _client.ClientAddress = 32;
            _client.UseLogicalNameReferencing = true;
            _client.Password = ASCIIEncoding.ASCII.GetBytes("123456");
            _client.Ciphering.SystemTitle = ASCIIEncoding.ASCII.GetBytes("qwertyui");
            _client.Ciphering.BlockCipherKey = ASCIIEncoding.ASCII.GetBytes("bbbbbbbbbbbbbbbb");
            _client.Ciphering.AuthenticationKey = ASCIIEncoding.ASCII.GetBytes("bbbbbbbbbbbbbbbb");
            _client.Ciphering.InvocationCounter = 0;
            _client.Authentication = Authentication.Low;
            _client.InterfaceType = InterfaceType.WRAPPER;
            _client.Ciphering.Security = Security.Encryption;
            //**************************************************************************************************************//
            mediagp = new GXNet();
            gprs = mediagp as GXNet;
            gprs.Protocol = NetworkType.Tcp;
            gprs.UseIPv6 = true;
            gprs.Port = 4059;// Int32.Parse(tb_portname.Text);
            gprs.HostName = "2402:3a80:1700:047e::2";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GXDLMSReader reader = new GXDLMSReader(_client, mediagp, System.Diagnostics.TraceLevel.Info);
            try
            {
                mediagp.Open();
                if (reader.InitializeConnection())
                {
                    GXDLMSGprsSetup gprs = new GXDLMSGprsSetup(); //M2M.METERIPV6
                    object val = reader.ReadObis(gprs, 2);
                    reader.Disconnect();
                    string aa = val.ToString();
                    rtbLogs.Text = aa;
                    //media.OnReceived += Media_OnReceived;  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
