using System.Collections;
using System.Data;
using System.Text;

namespace Gurux_Testing
{
    public class Test2
    {
        Utility utility = new Utility();
        MediaSettings _media = new MediaSettings();
        Function function = new Function();
        string displayMode = null;
        byte[] FirmwareBytes = null;
        string browserFileName = "";
        static public ArrayList RecordList = new ArrayList();
        public Test2()
        {
            InitializeComponent();
            statusStrip1.Text = "DlmsClient";
            _mediaSettings = this;
        }
        public static Test2 _mediaSettings;
        public void update(string message)
        {
            if (InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    richTextBox2.AppendText(message + "\n");
                }));
            }
            else
            {
                richTextBox2.AppendText(message + "\n");
            }
        }
        private void b_browse_Click(object sender, EventArgs e)
        {
            browserFileName = ""; FirmwareBytes = null;
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "BIN |*.bin";
            if (d.ShowDialog() == DialogResult.OK)
            {
                browserFileName = d.FileName;
                tb_browse.Text = browserFileName;
            }
            FirmwareBytes = getBuffer(d.FileName);//File.ReadAllBytes(browserFileName);
            //byte[] buffer = getBuffer(d.FileName);
        }
        private byte[] getBuffer(string fileLocation)
        {
            byte[] buffer = null;
            try
            {
                FileStream fileStream = new FileStream(fileLocation, FileMode.Open, FileAccess.Read);

                int length = (int)fileStream.Length;   // get file length
                buffer = new Byte[length];            // create buffer    
                BinaryReader br = new BinaryReader(fileStream);
                buffer = br.ReadBytes((int)length);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return buffer;
        }

        private void b_stop_Click(object sender, EventArgs e)
        {
            try
            {
                //_isRunning = false;
                _media.reader.Disconnect();
                b_upImage.Enabled = true; b_browse.Enabled = true;
            }
            catch
            { }
            /*    try
                {
                    ////////////////////////////////NEERAJ CODE///////////////////////////////////////
                    /// image size ,identification and signature///////////////////


                    statusStrip1.Text = "";
                    _media.Set_InvocationCounter("0.0.43.1.5.255");// _media.dissconnect4Firmware();
                    Program.fUpdate = true;
                    if (_media.connct4Firmware())
                    {
                        try { 
                               GXDLMSImageTransfer imageTransfer = new GXDLMSImageTransfer("0.0.44.0.0.255");
                            // GXDLMSGprsSetup gprs = new GXDLMSGprsSetup(); //M2M.METERIPV6   
                               string apn1 = _media.reader.ReadObis(imageTransfer, 2).ToString();
                               im1.Text = apn1.ToString();// apn.Remove(apn.Length - 1, 1);
                               string apn2 = _media.reader.ReadObis(imageTransfer, 4).ToString();
                               im2.Text = apn2.ToString();// apn.Remove(apn.Length - 1, 1);
                               string apn = _media.reader.ReadObis(imageTransfer, 3).ToString();
                              bstatus  .Text = apn.ToString();// apn.Remove(apn.Length - 1, 1);

                            object val = _media.reader.Read2(imageTransfer, 7);
                            GXDLMSImageActivateInfo[] value = (GXDLMSImageActivateInfo[])val;
                            string   dayid = value[0].Size.ToString()   ;
                            byte[] buff1 = value[0].Signature ;
                            string signature = ByteToChar(buff1, 0, buff1.Length);
                            byte[] buff2 = value[0].Identification ;
                            string Identification = ByteToChar(buff2, 0, buff2.Length);
                            _media.reader.Disconnect();
                        }
                            catch (Exception ex)
                            {

                            }


                    }
                    else
                    {
                        statusStrip1.Text = "Disconnected mode";
                    }
                }
                catch (Exception ex)
                {
                    statusStrip1.Text = ex.Message; _media.closeMedia(); //_media.changeClientAddress(48);
                }*/

        }
        public string ByteToChar(byte[] data, int start, int length)
        {
            try
            {
                string str = "";
                for (int i = start; i < (start + length); i++)
                {
                    str = str + (char)(data[i]);
                }
                str = System.Text.RegularExpressions.Regex.Replace(str, @"[^\u0000-\u007F]+", string.Empty).Replace("\0", "");
                return str;
            }
            catch
            {
                return null;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                string time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                DateTime date = DateTime.ParseExact(time, "dd/MM/yyyy HH:mm:ss", null);
                // dateTimePicker1.Text = date.ToString(); dt_calender.Text = date.ToString(); //dt2.Text = date.ToString(); dt3.Text = date.ToString(); dt4.Text = date.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void b_upImage_Click(object sender, EventArgs e)
        {
            try
            {
                statusStrip1.Text = "";
                _media.Set_InvocationCounter("0.0.43.1.5.255");// _media.dissconnect4Firmware();
                Program.fUpdate = true;
                if (_media.connct4Firmware())
                {
                    System.Threading.Tasks.Task myTask = new System.Threading.Tasks.Task(() =>
                    {
                        try
                        {
                            this.Invoke(new MethodInvoker(delegate
                            {
                                notification.Text = "Firmware Upgrade in process.."; b_upImage.Enabled = false; b_browse.Enabled = false;
                            }));
                            GXDLMSImageTransfer imageTransfer = new GXDLMSImageTransfer();
                            byte[] ident = ASCIIEncoding.ASCII.GetBytes(tb_identification.Text.Trim());
                            _media.reader.ImageUpdate(imageTransfer, ident, FirmwareBytes);
                            _media.reader.WaitTime = 20000;
                            MessageBox.Show("Firmware Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Invoke(new MethodInvoker(delegate
                            {
                                notification.Text = "Updation Completed Successfully"; b_upImage.Enabled = true; b_browse.Enabled = true;
                            }));
                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Invoke(new MethodInvoker(delegate
                            {
                                notification.Text = "Firmware process stopped,try again"; b_upImage.Enabled = true; b_browse.Enabled = true;
                            }));
                        }
                    });
                    myTask.Start();
                    /* old code
                    GXDLMSImageTransfer imageTransfer = new GXDLMSImageTransfer();
                      byte[] ident = ASCIIEncoding.ASCII.GetBytes(tb_identification.Text.Trim());
                      _media.reader.ImageUpdate(imageTransfer, ident, FirmwareBytes);
                    _media.reader.Disconnect();
                     _media.reader.WaitTime = 20000;*/
                    //_media.dissconnect4Firmware();
                    //_media.Set_InvocationCounter("0.0.43.1.3.255");

                }
                else
                {
                    statusStrip1.Text = "Disconnected mode";
                }
            }
            catch (Exception ex)
            {
                statusStrip1.Text = ex.Message; _media.closeMedia(); //_media.changeClientAddress(48);
            }
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            richTextBox2.SelectionStart = richTextBox2.Text.Length;
            // scroll it automatically
            richTextBox2.ScrollToCaret();
        }

        private void Test2_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MM/dd/yyyy hh:mm:ss";
            timer1.Start();
            detail();
        }

        public void detail()
        {
            try
            {
                ////////////////////////////////NEERAJ CODE///////////////////////////////////////
                /// image size ,identification and signature///////////////////


                statusStrip1.Text = "";
                _media.Set_InvocationCounter("0.0.43.1.5.255");// _media.dissconnect4Firmware();
                Program.fUpdate = true;
                if (_media.connct4Firmware())
                {
                    try
                    {
                        GXDLMSImageTransfer imageTransfer = new GXDLMSImageTransfer("0.0.44.0.0.255");
                        // GXDLMSGprsSetup gprs = new GXDLMSGprsSetup(); //M2M.METERIPV6   
                        string apn1 = _media.reader.ReadObis(imageTransfer, 2).ToString();
                        im1.Text = apn1.ToString();// apn.Remove(apn.Length - 1, 1);
                        string apn2 = _media.reader.ReadObis(imageTransfer, 4).ToString();
                        im2.Text = apn2.ToString();// apn.Remove(apn.Length - 1, 1);
                        string apn = _media.reader.ReadObis(imageTransfer, 3).ToString();
                        bstatus.Text = apn.ToString();// apn.Remove(apn.Length - 1, 1);

                        object val = _media.reader.Read2(imageTransfer, 7);
                        GXDLMSImageActivateInfo[] value = (GXDLMSImageActivateInfo[])val;
                        string dayid = value[0].Size.ToString();
                        byte[] buff1 = value[0].Signature;
                        string signature = ByteToChar(buff1, 0, buff1.Length);
                        byte[] buff2 = value[0].Identification;
                        string Identification = ByteToChar(buff2, 0, buff2.Length);
                        _media.reader.Disconnect();
                    }
                    catch (Exception ex)
                    {

                    }


                }
                else
                {
                    statusStrip1.Text = "Disconnected mode";
                }
            }
            catch (Exception ex)
            {
                statusStrip1.Text = ex.Message; _media.closeMedia(); //_media.changeClientAddress(48);
            }
        }
        private void activebtn_Click(object sender, EventArgs e)
        {
            try
            {

                statusStrip1.Text = "";
                _media.Set_InvocationCounter("0.0.44.0.0.255");// _media.dissconnect4Firmware();
                Program.fUpdate = true;
                if (_media.connct4Firmware())
                {
                    System.Threading.Tasks.Task myTask = new System.Threading.Tasks.Task(() =>
                    {
                        try
                        {
                            this.Invoke(new MethodInvoker(delegate
                            {
                                notification.Text = "Firmware Upgrade in process.."; b_upImage.Enabled = false; b_browse.Enabled = false;
                            }));
                            GXDLMSImageTransfer imageTransfer = new GXDLMSImageTransfer();
                            byte[] ident = ASCIIEncoding.ASCII.GetBytes(tb_identification.Text.Trim());
                            _media.reader.ImageUpdate(imageTransfer, ident, FirmwareBytes);
                            _media.reader.WaitTime = 20000;
                            MessageBox.Show("Firmware Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Invoke(new MethodInvoker(delegate
                            {
                                notification.Text = "Updation Completed Successfully"; b_upImage.Enabled = true; b_browse.Enabled = true;
                            }));
                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Invoke(new MethodInvoker(delegate
                            {
                                notification.Text = "Firmware process stopped,try again"; b_upImage.Enabled = true; b_browse.Enabled = true;
                            }));
                        }
                    });
                    myTask.Start();

                }
                else
                {
                    statusStrip1.Text = "Disconnected mode";
                }
            }
            catch (Exception ex)
            {
                statusStrip1.Text = ex.Message; _media.closeMedia(); //_media.changeClientAddress(48);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // GXDateTime gXDateTime = (GXDateTime)(dateTimePicker1.Text);
                string RTC = dateTimePicker1.Text;
                string year = RTC.Substring(6, 4);
                string mon = RTC.Substring(0, 2);
                string day = RTC.Substring(3, 2);
                string hour = RTC.Substring(11, 2);
                string min = RTC.Substring(14, 2);
                string sec = RTC.Substring(17, 2);
                GXDateTime resetdate = new GXDateTime(Convert.ToInt32(year), Convert.ToInt32(mon), Convert.ToInt32(day), Convert.ToInt32(hour), Convert.ToInt32(min), Convert.ToInt32(sec), 0);

                statusStrip1.Text = "";
                _media.Set_InvocationCounter("0.0.43.1.5.255");// _media.dissconnect4Firmware();
                Program.fUpdate = true;
                if (_media.connct4Firmware())
                {
                    ///////////for time and date  formet to the gurux function/////////
                    //  int min = Convert.ToInt32(tb_pushMin.Text.Trim());
                    //  int sec = Convert.ToInt32(tb_pushSec.Text.Trim());
                    //    GXDateTime dt = new GXDateTime(dateTimePicker1.Text );
                    //  GXDateTime dt = new GXDateTime(00, 00, 00, 00, min, sec, 00);
                    GXDLMSActionSchedule pushschedule = new GXDLMSActionSchedule("0.0.15.0.2.255");
                    pushschedule.ExecutionTime = new GXDateTime[] { new GXDateTime(resetdate) };
                    //   pushschedule.ExecutionTime = new GXDateTime[] { new GXDateTime(dt) };
                    if (_media.reader.Write(pushschedule, 4))
                    {
                        notification.Text = "Firmware Activated successfully...";
                    }
                    else
                    {
                        notification.Text = " not set,Try again";
                    }
                    _media.closeMedia();
                }
                else
                {
                    notification.Text = "Disconnected mode";
                }
            }
            catch (Exception ex)
            {
                notification.Text = ex.Message; _media.closeMedia();
            }
        }
    }
}
