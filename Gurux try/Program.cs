using Gurux.Common;
using Gurux.DLMS;
using Gurux.DLMS.Enums;
using Gurux.DLMS.Objects;
using Gurux.DLMS.Secure;
using Gurux.Net;
using Gurux.Serial;
using Gurux_Testing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gurux_try
{
    internal class Program
    {
        /// <summary>
        /// Wait time.
        /// </summary>
        public int WaitTime = 20000;// 60000;// 20000;//500000;
        /// <summary>
        /// Retry count.
        /// </summary>
        public int RetryCount = 1;
        IGXMedia Media = new GXNet(NetworkType.Tcp, "10.0.1.29", 4059);
        TraceLevel Trace;
        GXDLMSSecureClient Client;

        static void Main(string[] args)
        {
            //NewCode newCode = new NewCode();

            ConnectionSettings settings = new ConnectionSettings
            {
                MediaType = "G P R S",
                HostName = "10.0.1.29",
                AuthType = "High",
                Port = "4059",
                Password = "wwwwwwwwwwwwwwww",
                BaudRate = 19000
            };

            ConnectionControl connectionControl = new ConnectionControl(settings);
            var rslt = connectionControl.ReadProfile("Billing");

            //var pro = new Program();
            //pro.Client = new GXDLMSSecureClient()
            //{
            //    Password = Encoding.ASCII.GetBytes("wwwwwwwwwwwwwwww"),
            //    Authentication = Gurux.DLMS.Enums.Authentication.High,
            //    ClientAddress = 48,
            //    ServerAddress = 1,
            //    UseLogicalNameReferencing = true,
            //    InterfaceType = Gurux.DLMS.Enums.InterfaceType.WRAPPER
            //};            
            //pro.Client.Ciphering.Security = Gurux.DLMS.Enums.Security.AuthenticationEncryption;
            //pro.Client.Ciphering.SystemTitle = Encoding.ASCII.GetBytes("qwertyui");            
            //pro.Client.Ciphering.AuthenticationKey = new byte[] { 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62 };
            //pro.Client.Ciphering.BlockCipherKey = new byte[] { 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62, 0x62 };
            //pro.Trace = TraceLevel.Info;
            //try
            //{
            //    using (GXNet media = new GXNet(NetworkType.Tcp, "10.0.1.29", 4059))
            //    {
            //        media.UseIPv6 = false;
            //        media.Open();
            //        pro.InitializeConnection();
            //        GXDLMSData billingProfile = new GXDLMSData("1.0.98.1.0.255");
            //        string serialNumber = pro.Read(billingProfile, 3).ToString();
            //    }
            //} 
            //catch(Exception ex) 
            //{
            //    Console.WriteLine(ex.Message);
            //}

            //foreach (GXDLMSObject it in pro.Client.Objects.GetObjects(ObjectType.ProfileGeneric))
            //{
            //    Console.WriteLine(it.Name);
            //}

            Console.ReadKey();
        }

        public object Read(GXDLMSObject it, int attributeIndex)
        {
            GXReplyData reply = new GXReplyData();
            if (!ReadDataBlock(Client.Read(it, attributeIndex), reply))
            {
                if (reply.Error != (short)ErrorCode.Rejected)
                {
                    throw new GXDLMSException(reply.Error);
                }
                reply.Clear();
                Thread.Sleep(1000);
                if (!ReadDataBlock(Client.Read(it, attributeIndex), reply))
                {
                    throw new GXDLMSException(reply.Error);
                }
            }
            //Update data type.
            if (it.GetDataType(attributeIndex) == DataType.None)
            {
                it.SetDataType(attributeIndex, reply.DataType);
            }
            return Client.UpdateValue(it, attributeIndex, reply.Value);
        }

        public bool ReadDataBlock(byte[][] data, GXReplyData reply)
        {
            if (data == null)
            {
                return true;
            }
            foreach (byte[] it in data)
            {
                reply.Clear();
                ReadDataBlock(it, reply);
            }
            return reply.Error == 0;
        }

        public bool InitializeConnection()
        {
            //bool status = false;
            try
            {
                GXReplyData reply = new GXReplyData();
                byte[] data;
                data = Client.SNRMRequest();
                if (data != null)
                {
                    if (Trace > TraceLevel.Info)
                    {
                        Console.WriteLine("Send SNRM request." + GXCommon.ToHex(data, true));
                    }
                    ReadDataBlock(data, reply);
                    if (Trace == TraceLevel.Verbose)
                    {
                        Console.WriteLine("Parsing UA reply." + reply.ToString());
                    }
                    //Has server accepted client.
                    Client.ParseUAResponse(reply.Data);
                    if (Trace > TraceLevel.Info)
                    {
                        Console.WriteLine("Parsing UA reply succeeded.");
                    }
                }
                //Generate AARQ request.
                //Split requests to multiple packets if needed.
                //If password is used all data might not fit to one packet.
                var byteData = Client.AARQRequest();
                foreach (byte[] it in byteData)
                {
                    if (Trace > TraceLevel.Info)
                    {
                        Console.WriteLine("Send AARQ request", GXCommon.ToHex(it, true));
                    }
                    reply.Clear();
                    ReadDataBlock(it, reply);
                }
                if (Trace > TraceLevel.Info)
                {
                    Console.WriteLine("Parsing AARE reply" + reply.ToString());
                }
                //Parse reply.
                Client.ParseAAREResponse(reply.Data);
                reply.Clear();
                //Get challenge Is HLS authentication is used.
                if (Client.IsAuthenticationRequired)
                {
                    foreach (byte[] it in Client.GetApplicationAssociationRequest())
                    {
                        reply.Clear();
                        ReadDataBlock(it, reply);
                    }
                    Client.ParseApplicationAssociationResponse(reply.Data);
                }
                if (Trace > TraceLevel.Info)
                {
                    Console.WriteLine("Parsing AARE reply succeeded.");
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }


        /// <summary>
        /// Read data block from the device.
        /// </summary>
        /// <param name="data">data to send</param>
        /// <param name="text">Progress text.</param>
        /// <param name="multiplier"></param>
        /// <returns>Received data.</returns>
        public void ReadDataBlock(byte[] data, GXReplyData reply)
        {
            ReadDLMSPacket(data, reply);
            lock (Media.Synchronous)
            {
                while (reply.IsMoreData)
                {
                    if (reply.IsStreaming())
                    {
                        data = null;
                    }
                    else
                    {
                        data = Client.ReceiverReady(reply.MoreData);
                    }
                    ReadDLMSPacket(data, reply);
                    if (Trace > TraceLevel.Info)
                    {
                        //If data block is read.
                        if ((reply.MoreData & RequestTypes.Frame) == 0)
                        {
                            //MainForm._mainForm.update("+");
                            // Console.Write("+");
                        }
                        else
                        {
                            //MainForm._mainForm.update("-");
                            //Console.Write("-");
                        }
                    }
                }
            }
        }

        public GXDLMSObjectCollection GetObjects(ObjectType type)
        {

            GXDLMSObjectCollection gXDLMSObjectCollection = new GXDLMSObjectCollection();
            using (IEnumerator<GXDLMSObject> enumerator = GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    GXDLMSObject current = enumerator.Current;
                    if (current.ObjectType == type)
                    {
                        gXDLMSObjectCollection.Add(current);
                    }
                }
            }

            return gXDLMSObjectCollection;
        }

        private IEnumerator<GXDLMSObject> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Read DLMS Data from the device.
        /// </summary>
        /// <param name="data">Data to send.</param>
        /// <returns>Received data.</returns>
        public void ReadDLMSPacket(byte[] data, GXReplyData reply)
        {
            if (data == null && !reply.IsStreaming())
            {
                return;
            }
            reply.Error = 0;
            object eop = (byte)0x7E;
            //In network connection terminator is not used.
            if (Client.InterfaceType == InterfaceType.WRAPPER && Media is GXNet)
            {
                eop = null;
            }
            int pos = 0;
            bool succeeded = false;
            ReceiveParameters<byte[]> p = new ReceiveParameters<byte[]>()
            {
                Eop = eop,
                Count = 5,
                WaitTime = WaitTime,
            };
            lock (Media.Synchronous)
            {
                while (!succeeded && pos != 3)
                {
                    if (!reply.IsStreaming())
                    {
                        Console.WriteLine("<- " + DateTime.Now.ToLongTimeString() + "\t" + GXCommon.ToHex(data, true));
                        //MainForm._mainForm.update("<- " + DateTime.Now.ToLongTimeString() + "\t" + GXCommon.ToHex(data, true));
                        Media.Send(data, null);
                    }
                    succeeded = Media.Receive(p);
                    if (!succeeded)
                    {
                        if (++pos >= RetryCount)
                        {
                            throw new Exception("Failed to receive reply from the device in given time.");
                        }
                        //If Eop is not set read one byte at time.
                        if (p.Eop == null)
                        {
                            p.Count = 1;
                        }
                        //Try to read again...
                        System.Diagnostics.Debug.WriteLine("Data send failed. Try to resend " + pos.ToString() + "/3");
                        //MainForm._mainForm.update("Data send failed. Trying to resend ");
                    }
                }
                try
                {
                    pos = 0;
                    //Loop until whole COSEM packet is received.
                    while (!Client.GetData(p.Reply, reply))
                    {
                        //If Eop is not set read one byte at time.
                        if (p.Eop == null)
                        {
                            p.Count = 1;
                        }
                        while (!Media.Receive(p))
                        {
                            if (++pos >= RetryCount)
                            {
                                throw new Exception("Failed to receive reply from the device in given time.");
                            }
                            //If echo.
                            if (p.Reply == null || p.Reply.Length == data.Length)
                            {
                                Media.Send(data, null);
                            }
                            //Try to read again...
                            System.Diagnostics.Debug.WriteLine("Data send failed. Try to resend " + pos.ToString() + "/3");
                            /*MainForm._mainForm.update("Data send failed. Trying to resend ")*/;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("-> " + DateTime.Now.ToLongTimeString() + "\t" + GXCommon.ToHex(p.Reply, true));
                    //MainForm._mainForm.update("-> " + DateTime.Now.ToLongTimeString() + "\t" + GXCommon.ToHex(p.Reply, true));
                    //throw ex;
                }
            }
            Console.WriteLine("-> " + DateTime.Now.ToLongTimeString() + "\t" + GXCommon.ToHex(p.Reply, true));            
            if (reply.Error != 0)
            {
                if (reply.Error == (short)ErrorCode.Rejected)
                {
                    Thread.Sleep(1000);
                    ReadDLMSPacket(data, reply);
                }
                else
                {
                    throw new GXDLMSException(reply.Error);
                }
            }
        }
    }
}
