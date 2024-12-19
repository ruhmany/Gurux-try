using Gurux.DLMS.Enums;
using Gurux.DLMS.Objects;
using Gurux.DLMS;
using Gurux.Net;
using System;
using System.Collections;
using System.Text;
using System.Threading;
using Gurux.Common;
using Gurux.DLMS.Secure;
using Gurux_Testing;

namespace Gurux_try
{
    internal class NewCode
    {
        public IGXMedia media = new GXNet();
        public GXNet gprs = null;
        public GXDLMSSecureClient client = new GXDLMSSecureClient(true);
        private uint invocationCounter = 0;

        public NewCode()
        {
        }
        
        
    }
}
