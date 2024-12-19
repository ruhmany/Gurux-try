using Gurux.Common;
using Gurux.DLMS.Secure;
using Gurux.DLMS.Enums;
using Gurux.Net;
using Gurux.Serial;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gurux_Testing
{
    class InitializeMedia
    {
        public GXDLMSReader reader = null;
        public static IGXMedia media = new GXSerial();
        public static IGXMedia mediagp = new GXNet();
        public static GXSerial serial = null;
        public static GXNet gprs = null;
        public static TraceLevel trace = TraceLevel.Info;
        public static bool iec = false;
        public static GXDLMSSecureClient _client = new GXDLMSSecureClient(true);

        public InitializeMedia()
        {
            media = new GXSerial();
            serial = media as GXSerial;
            mediagp = new GXNet();
            gprs = mediagp as GXNet;
            //_client.ServerAddress = 1;
            //_client.UseLogicalNameReferencing = true;
            //_client.Ciphering.SystemTitle = ASCIIEncoding.ASCII.GetBytes("qwertyui");
            //_client.Ciphering.BlockCipherKey = ASCIIEncoding.ASCII.GetBytes("bbbbbbbbbbbbbbbb");
            //_client.Ciphering.AuthenticationKey = ASCIIEncoding.ASCII.GetBytes("bbbbbbbbbbbbbbbb");
            ////_client.Ciphering.InvocationCounter = 0;
            ////_client.ServerSystemTitle = new byte[] { 0x45, 0x4C, 0x53, 0x30, 0x30, 0x30, 0x31, 0x30 };
            //initializeMediaSettings();
        }
        public static void initializeMediaSettings()
        {
           
            
            //**********************************************//
           
           
        }
        public static void initializeClient(string auType)
        {
           // _client.Ciphering.InvocationCounter = (uint)(Program.iCounter + 2);
            switch (auType)
            {
                case "None":                   
                    _client.ClientAddress = 16;
                    _client.Authentication = Authentication.None;
                    _client.Ciphering.Security = Security.None;
                    break;
                case "Low":                   
                    _client.ClientAddress = 32;
                    _client.Authentication = Authentication.Low;
                    _client.Ciphering.Security = Security.Encryption;
                    _client.Password = ASCIIEncoding.ASCII.GetBytes("123456");
                    break;
                case "High":                   
                    _client.ClientAddress = 48;
                    _client.Authentication = Authentication.High;
                    _client.Ciphering.Security = Security.AuthenticationEncryption;
                    _client.Password = ASCIIEncoding.ASCII.GetBytes("wwwwwwwwwwwwwwww");
                    break;
            }
        }
    }
}
