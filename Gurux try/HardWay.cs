using Gurux.Common;
using Gurux.DLMS;
using Gurux.DLMS.Enums;
using Gurux.DLMS.Objects;
using Gurux.DLMS.Secure;
using Gurux.Net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gurux_try
{
    internal class HardWay
    {
        public IGXMedia mediagpgp = new GXNet();
        public GXNet gprs = null;
        public GXDLMSSecureClient _client = new GXDLMSSecureClient(true);
        TraceLevel Trace = TraceLevel.Verbose;
        int InovationCounter;
        public HardWay()
        {
            ConnectLowLevel();
        }
        void ReadDate()
        {

        }

        void ConnectLowLevel()
        {
            _client.ServerAddress = 1;
            _client.ClientAddress = 16;
            _client.Ciphering.InvocationCounter = 0;
            _client.ClientAddress = 16;
            _client.Authentication = Authentication.None;
            _client.Ciphering.Security = Security.None;
            _client.UseLogicalNameReferencing = true;
            //**************************************************************************************************************//
            mediagpgp = new GXNet();
            gprs = mediagpgp as GXNet;
            gprs.Protocol = NetworkType.Tcp;
            gprs.UseIPv6 = false;
            gprs.Port = 4059;// Int32.Parse(tb_portname.Text);
            gprs.HostName = "10.0.1.29";
            gprs.Open();
            SNRMRequest();
            AarqRequest();
            var iCounter = ReadObis(new GXDLMSData("0.0.43.1.3.255"), 2).ToString();
            Console.WriteLine(iCounter);
        }
        void ConnectHighLevel()
        {
            _client.ServerAddress = 1;
            _client.ClientAddress = 64;
            _client.UseLogicalNameReferencing = true;
            _client.Password = ASCIIEncoding.ASCII.GetBytes("wwwwwwwwwwwwwwww");
            _client.Ciphering.SystemTitle = ASCIIEncoding.ASCII.GetBytes("qwertyui");
            _client.Ciphering.BlockCipherKey = ASCIIEncoding.ASCII.GetBytes("bbbbbbbbbbbbbbbb");
            _client.Ciphering.AuthenticationKey = ASCIIEncoding.ASCII.GetBytes("bbbbbbbbbbbbbbbb");
            _client.Ciphering.InvocationCounter = 0;
            _client.Authentication = Authentication.High;
            _client.InterfaceType = InterfaceType.WRAPPER;
            _client.Ciphering.Security = Security.AuthenticationEncryption;
            //**************************************************************************************************************//
            mediagpgp = new GXNet();
            gprs = mediagpgp as GXNet;
            gprs.Protocol = NetworkType.Tcp;
            gprs.UseIPv6 = true;
            gprs.Port = 4059;// Int32.Parse(tb_portname.Text);
            gprs.HostName = "10.0.1.29";
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
            lock (mediagpgp.Synchronous)
            {
                while (reply.IsMoreData)
                {
                    if (reply.IsStreaming())
                    {
                        data = null;
                    }
                    else
                    {
                        data = _client.ReceiverReady(reply.MoreData);
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

        public void ReadDLMSPacket(byte[] data, GXReplyData reply)
        {
            if (data == null && !reply.IsStreaming())
            {
                return;
            }
            reply.Error = 0;
            object eop = (byte)0x7E;
            //In network connection terminator is not used.
            if (_client.InterfaceType == InterfaceType.WRAPPER && mediagpgp is GXNet)
            {
                eop = null;
            }
            int pos = 0;
            bool succeeded = false;
            ReceiveParameters<byte[]> p = new ReceiveParameters<byte[]>()
            {
                Eop = eop,
                Count = 5,
                WaitTime = 6000,
            };
            lock (mediagpgp.Synchronous)
            {
                while (!succeeded && pos != 3)
                {
                    if (!reply.IsStreaming())
                    {
                        //WriteTrace("<- " + DateTime.Now.ToLongTimeString() + "\t" + GXCommon.ToHex(data, true));
                        //MainForm._mainForm.update("<- " + DateTime.Now.ToLongTimeString() + "\t" + GXCommon.ToHex(data, true));
                        mediagpgp.Send(data, null);
                    }
                    succeeded = mediagpgp.Receive(p);
                    if (!succeeded)
                    {
                        if (++pos >= 3)
                        {
                            throw new Exception("Failed to receive reply from the device in given time.");
                        }
                        //If Eop is not set read one byte at time.
                        if (p.Eop == null)
                        {
                            p.Count = 1;
                        }
                        //Try to read again...
                        //System.Diagnostics.Debug.WriteLine("Data send failed. Try to resend " + pos.ToString() + "/3");
                        //MainForm._mainForm.update("Data send failed. Trying to resend ");
                    }
                }
                try
                {
                    pos = 0;
                    //Loop until whole COSEM packet is received.
                    while (!_client.GetData(p.Reply, reply))
                    {
                        //If Eop is not set read one byte at time.
                        if (p.Eop == null)
                        {
                            p.Count = 1;
                        }
                        while (!mediagpgp.Receive(p))
                        {
                            if (++pos >= 3)
                            {
                                throw new Exception("Failed to receive reply from the device in given time.");
                            }
                            //If echo.
                            if (p.Reply == null || p.Reply.Length == data.Length)
                            {
                                mediagpgp.Send(data, null);
                            }
                            //Try to read again...
                            //System.Diagnostics.Debug.WriteLine("Data send failed. Try to resend " + pos.ToString() + "/3");
                            //MainForm._mainForm.update("Data send failed. Trying to resend ");
                        }
                    }
                }
                catch (Exception ex)
                {
                    //WriteTrace("-> " + DateTime.Now.ToLongTimeString() + "\t" + GXCommon.ToHex(p.Reply, true));
                    //MainForm._mainForm.update("-> " + DateTime.Now.ToLongTimeString() + "\t" + GXCommon.ToHex(p.Reply, true));
                    //throw ex;
                }
            }
            //WriteTrace("-> " + DateTime.Now.ToLongTimeString() + "\t" + GXCommon.ToHex(p.Reply, true));
            //MainForm._mainForm.update("-> " + DateTime.Now.ToLongTimeString() + "\t" + GXCommon.ToHex(p.Reply, true));
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

        public void SNRMRequest()
        {
            GXReplyData reply = new GXReplyData();
            byte[] data;
            data = _client.SNRMRequest();
            if (data != null)
            {
                if (Trace > TraceLevel.Info)
                {
                    //Console.WriteLine("Send SNRM request." + GXCommon.ToHex(data, true));
                    //MainForm._mainForm.update("Send SNRM request." + GXCommon.ToHex(data, true));
                }
                ReadDataBlock(data, reply);
                if (Trace == TraceLevel.Verbose)
                {
                    //Console.WriteLine("Parsing UA reply." + reply.ToString());
                    //MainForm._mainForm.update("Parsing UA reply." + reply.ToString());
                }
                //Has server accepted client.
                _client.ParseUAResponse(reply.Data);
                if (Trace > TraceLevel.Info)
                {
                    //Console.WriteLine("Parsing UA reply succeeded.");
                    //MainForm._mainForm.update("Parsing UA reply succeeded.");
                }
            }
        }

        public void AarqRequest()
        {
            GXReplyData reply = new GXReplyData();
            //Generate AARQ request.
            //Split requests to multiple packets if needed.
            //If password is used all data might not fit to one packet.
            foreach (byte[] it in _client.AARQRequest())
            {
                if (Trace > TraceLevel.Info)
                {
                    //Console.WriteLine("Send AARQ request", GXCommon.ToHex(it, true));
                    //MainForm._mainForm.update("Send AARQ request" + GXCommon.ToHex(it, true));
                }
                reply.Clear();
                ReadDataBlock(it, reply);
            }
            if (Trace > TraceLevel.Info)
            {
                //Console.WriteLine("Parsing AARE reply" + reply.ToString());
                //MainForm._mainForm.update("Parsing AARE reply" + reply.ToString());
            }
            //Parse reply.
            _client.ParseAAREResponse(reply.Data);
            reply.Clear();
            //Get challenge Is HLS authentication is used.
            if (_client.IsAuthenticationRequired)
            {
                foreach (byte[] it in _client.GetApplicationAssociationRequest())
                {
                    reply.Clear();
                    ReadDataBlock(it, reply);
                }
                _client.ParseApplicationAssociationResponse(reply.Data);
            }
            if (Trace > TraceLevel.Info)
            {
                //Console.WriteLine("Parsing AARE reply succeeded.");
                //MainForm._mainForm.update("Parsing AARE reply succeeded.");
            }
        }

        public object ReadObis(GXDLMSObject it, int attributeIndex)
        {
            GXReplyData reply = new GXReplyData();
            if (!ReadDataBlock(_client.Read(it, attributeIndex), reply))
            {
                if (reply.Error != (short)ErrorCode.Rejected)
                {
                    throw new GXDLMSException(reply.Error);
                }
                reply.Clear();
                Thread.Sleep(1000);
                if (!ReadDataBlock(_client.Read(it, attributeIndex), reply))
                {
                    throw new GXDLMSException(reply.Error);
                }
            }
            //Update data type.
            if (it.GetDataType(attributeIndex) == DataType.None)
            {
                it.SetDataType(attributeIndex, reply.DataType);
            }
            return _client.UpdateValue(it, attributeIndex, reply.Value);
        }

        /// <summary>
        /// Send data block(s) to the meter.
        /// </summary>
        /// <param name="data">Send data block(s).</param>
        /// <param name="reply">Received reply from the meter.</param>
        /// <returns>Return false if frame is rejected.</returns>
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
    }
}
