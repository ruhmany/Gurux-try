using System.Diagnostics;

namespace Gurux_Testing
{
    public static class Settings
    {
        public static IGXMedia media = null;
        public static TraceLevel trace = TraceLevel.Info;
        public static bool iec = false;
        public static GXDLMSSecureClient client = new GXDLMSSecureClient(true);
        //Objects to read.
        public static List<KeyValuePair<string, int>> readObjects = new List<KeyValuePair<string, int>>();
    }
}
