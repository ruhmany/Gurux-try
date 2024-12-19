using Gurux.Common;

namespace Gurux_Testing
{
    class SerialPortCom
    {
        public bool OPEN()
        {
            bool tt = false;
            try
            {
                ConnectionControl.GX.Open();
                //COMPORT_PROPERTIES_UPDATE(PORT_NAME, BAUD_RATE, PARITY, DATA_BIT, STOP_BIT, FLOW_CONTROL);                
                //_serialPort.Open();
                Program.Port_Status = "OPEN";
                tt = true;
            }
            catch
            {
                //MessageBox.Show(ex.ToString(), "Com Port Control", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return tt;
        }

        public bool CLOSE()
        {
            bool tt = false;
            try
            {
                //_serialPort.Close();
                ConnectionControl.GX.Close();
                Program.Port_Status = "CLOSE";
                tt = true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Com Port Control", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return tt;
        }


        //public string ByteToHex(byte[] data, int start)
        //{
        //    string str = "";
        //    for (int i = start; i < (start + length); i++)
        //    {
        //        str = str + (data[i].ToString("X2").PadLeft(2, '0')) + " ";
        //    }
        //    return str;
        //}

        public byte[] SendAndRecieve(byte[] Cmd, int waitTime)
        {
            ReceiveParameters<byte[]> p = new ReceiveParameters<byte[]>
            {
                Eop = 0x0a,
                //Count = responselength,
                WaitTime = waitTime,
            };
            if (ConnectionControl.GX.IsOpen)
            {
                try
                {
                    ConnectionControl.GX.Send(Cmd);
                    System.Threading.Thread.Sleep(50);
                    string s = ConnectionControl.GX.ReadExisting();
                }
                catch
                {
                    return null;
                }
            }
            return p.Reply;
        }


        public byte[] ReadDataPacket(byte[] data, int tryCount)
        {
            if (data == null)
            {
                return null;
            }
            int pos = 0;
            bool succeeded = false;
            Gurux.Common.ReceiveParameters<byte[]> p = new ReceiveParameters<byte[]>
            {
                Eop = (byte)0x24,
                //Count = 5,
                WaitTime = 5000,
            };
            lock (ConnectionControl.GX.Synchronous)
            {
                if (data != null)
                {
                    ConnectionControl.GX.Send(data);
                }
                while (!succeeded && pos != 3)
                {
                    succeeded = ConnectionControl.GX.Receive(p);
                    if (!succeeded)
                    {
                        if (++pos != tryCount)
                        {
                            if (p.Eop == null)
                            {
                                p.Count = 1;
                            }
                            ConnectionControl.GX.Send(data);
                            continue;
                        }
                    }
                }
            }
            return p.Reply;
        }
    }
}
