namespace Gurux_Testing
{
    public class ConnectionSettings
    {
        public string MediaType { get; set; }
        public string HostName { get; set; }
        public string AuthType { get; set; }
        public string Port { get; set; }
        public string Password { get; set; }
        public int BaudRate { get; set; }
        public int InvocationCounter { get; set; } = 0;
        public string SystemSubTitle { get; set; } = "ELS70001";
        public string AuthKey { get; set; } = "bbbbbbbbbbbbbbbb";
        public string BlockCipherKey { get; set; } = "bbbbbbbbbbbbbbbb";
    }
}
