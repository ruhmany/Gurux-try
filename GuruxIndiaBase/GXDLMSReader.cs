using Gurux.Common;
using Gurux.DLMS;
using Gurux.DLMS.Enums;
using Gurux.DLMS.ManufacturerSettings;
using Gurux.DLMS.Objects;
using Gurux.Net;
using System.Collections;
using System.Diagnostics;
using System.Text;
using System.Xml.Serialization;

namespace Gurux_Testing
{
    class GXDLMSReader
    {
        /// <summary>
        /// Wait time.
        /// </summary>
        public int WaitTime = 20000;// 60000;// 20000;//500000;
        /// <summary>
        /// Retry count.
        /// </summary>
        public int RetryCount = 1;
        IGXMedia Media;
        TraceLevel Trace;
        GXDLMSClient Client;
        Function function = new Function();
        //MainForm mainForm = new MainForm();
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="client">DLMS Client.</param>
        /// <param name="media">Media.</param>
        public GXDLMSReader(GXDLMSClient client, IGXMedia media, TraceLevel trace)
        {
            Trace = trace;
            Media = media;
            Client = client;
            //Trace = MediaSettings.trace;// Settings.trace;
            //Media = MediaSettings.media;// Settings.media;
            //Client = MediaSettings._client;// Settings.client;
        }

        /// <summary>
        /// Read all data from the meter.
        /// </summary>
        public void ReadAll(bool useCache)
        {
            try
            {
                InitializeConnection();
                GetAssociationView(useCache);
                GetScalersAndUnits();
                GetProfileGenericColumns();
                GetReadOut();
                GetProfileGenerics();
            }
            finally
            {
                Close();
            }
        }

        /// <summary>
        /// Send SNRM Request to the meter.
        /// </summary>
        public void SNRMRequest()
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
        }
        /// <summary>
        /// Send AARQ Request to the meter.
        /// </summary>
        public void AarqRequest()
        {
            GXReplyData reply = new GXReplyData();
            //Generate AARQ request.
            //Split requests to multiple packets if needed.
            //If password is used all data might not fit to one packet.
            foreach (byte[] it in Client.AARQRequest())
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
        }

        public void getText()
        {
            //demo dm = new demo();
            //dm.getText("Message from reader");
        }

        /// <summary>
        /// Initialize connection to the meter.
        /// </summary>
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
                foreach (byte[] it in Client.AARQRequest())
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
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// This method is used to update meter firmware.
        /// </summary>
        /// <param name="target"></param>
        public void ImageUpdate(GXDLMSImageTransfer target, byte[] identification, byte[] data)
        {
            //Check that image transfer is enabled.
            GXReplyData reply = new GXReplyData();
            ReadDataBlockImage(Client.Read(target, 5), reply);
            Client.UpdateValue(target, 5, reply.Value);
            if (!target.ImageTransferEnabled)
            {
                throw new Exception("Image transfer is not enabled");
            }
            //if (InvokeRequired)
            //    {
            //        this.Invoke(new MethodInvoker(delegate
            //        {

            //           //Your code here, like set text box content or get text box contents etc..

            //        }));                
            //    }            
            //    else
            //    {

            //         // Your code here, like set text box content or get text box contents etc..
            //         // SAME CODE AS ABOVE
            //    }
            //   //MeterSettings._mediaSettings.update("Image transfer is enabled");
            //   //MeterSettings._mediaSettings.update("Reading image block size");

            //Test2._mediaSettings.update("Image transfer is enabled");
            //Test2._mediaSettings.update("Reading image block size");
            //Step 1: Read image block size.
            ReadDataBlockImage(Client.Read(target, 2), reply);
            Client.UpdateValue(target, 2, reply.Value);

            //  //MeterSettings._mediaSettings.update("Initiateing the Image transfer process");
            //Test2._mediaSettings.update("Initiateing the Image transfer process");
            // Step 2: Initiate the Image transfer process.
            ReadDataBlockImage(target.ImageTransferInitiate(Client, identification, data.Length), reply);

            // Step 3: Transfers ImageBlocks.
            //  //MeterSettings._mediaSettings.update("Transfering ImageBlocks");
            //Test2._mediaSettings.update("Transfering ImageBlocks");
            int imageBlockCount;
            ReadDataBlockImage(target.ImageBlockTransfer(Client, data, out imageBlockCount), reply);
            Console.WriteLine("Total Block count === " + imageBlockCount);
            //Step 4: Check the completeness of the Image.
            //  //MeterSettings._mediaSettings.update("Checking the completeness of the Image");
            //Test2._mediaSettings.update("Checking the completeness of the Image");
            ReadDataBlockImage(Client.Read(target, 3), reply);
            Client.UpdateValue(target, 3, reply.Value);

            // Step 5: The Image is verified;
            //  //MeterSettings._mediaSettings.update("The Image is verified");
            //Test2._mediaSettings.update("The Image is verified");
            Console.WriteLine("Image is verifie Success");
            ReadDataBlockImage(target.ImageVerify(Client), reply);
            // Step 6: Before activation, the Image is checked;

            //Get list to images to activate.
            ReadDataBlockImage(Client.Read(target, 7), reply);
            Client.UpdateValue(target, 7, reply.Value);
            bool bFound = false;
            foreach (GXDLMSImageActivateInfo it in target.ImageActivateInfo)
            {
                if (GXCommon.EqualBytes(it.Identification, identification)) //if (it.Identification == identification)
                {
                    bFound = true;
                    break;
                }
            }
            //Read image transfer status.
            ReadDataBlockImage(Client.Read(target, 6), reply);
            Client.UpdateValue(target, 6, reply.Value);
            if (target.ImageTransferStatus != Gurux.DLMS.Objects.Enums.ImageTransferStatus.VerificationSuccessful)
            {
                throw new Exception("Image transfer status is " + target.ImageTransferStatus.ToString());
            }

            if (!bFound)
            {
                throw new Exception("Image not found.");
            }
            //MessageBox.Show("Press OK for Activating image ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //DialogResult dialogResult = MessageBox.Show("Press OK for Activating image", "No", MessageBoxButtons.YesNo);
            //if (dialogResult == DialogResult.Yes)
            //{
            //    //Test2._mediaSettings.update("Activating image");
            //    ReadDataBlockImage(target.ImageActivate(Client), reply);
            //    MessageBox.Show("Image is Activated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //else if (dialogResult == DialogResult.No)
            //{

            //}
            //Step 7: Activate image.
            //  //MeterSettings._mediaSettings.update("Activating image");
            /// //Test2 ._mediaSettings.update("Activating image");
            ///    ReadDataBlockImage(target.ImageActivate(Client), reply);
            ///   MessageBox.Show("Image is Activated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Read association view.
        /// </summary>
        public void GetAssociationView(bool useCache)
        {
            if (useCache)
            {
                string path = GetCacheName();
                List<Type> extraTypes = new List<Type>(Gurux.DLMS.GXDLMSClient.GetObjectTypes());
                extraTypes.Add(typeof(GXDLMSAttributeSettings));
                extraTypes.Add(typeof(GXDLMSAttribute));
                XmlSerializer x = new XmlSerializer(typeof(GXDLMSObjectCollection), extraTypes.ToArray());
                //You can save association view, but make sure that it is not change.
                //Save Association view to the cache so it is not needed to retrieve every time.
                if (File.Exists(path))
                {
                    try
                    {
                        using (Stream stream = File.Open(path, FileMode.Open))
                        {
                            Console.WriteLine("Get available objects from the cache.");
                            Client.Objects.AddRange(x.Deserialize(stream) as GXDLMSObjectCollection);
                            stream.Close();
                        }
                        return;
                    }
                    catch (Exception ex)
                    {
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }
                        throw ex;
                    }
                }
            }
            GXReplyData reply = new GXReplyData();
            ReadDataBlock(Client.GetObjectsRequest(), reply);
            Client.ParseObjects(reply.Data, true);
        }

        /// <summary>
        /// Read scalers and units.
        /// </summary>
        public void GetScalersAndUnits()
        {
            ArrayList arr = new ArrayList();
            //  ((GXDLMSRegister)(it_data as GXDLMSProfileGeneric).CaptureObjects.Key).Scaler = value_to_be_updated_Scaler;
            GXDLMSObjectCollection objs = Client.Objects.GetObjects(new ObjectType[] { ObjectType.Register, ObjectType.ExtendedRegister, ObjectType.DemandRegister });
            //If trace is info.
            if (Trace > TraceLevel.Warning)
            {
                Console.WriteLine("Read scalers and units from the device.");
            }
            if ((Client.NegotiatedConformance & Gurux.DLMS.Enums.Conformance.MultipleReferences) != 0)
            {
                List<KeyValuePair<GXDLMSObject, int>> list = new List<KeyValuePair<GXDLMSObject, int>>();
                foreach (GXDLMSObject it in objs)
                {
                    if (it is GXDLMSRegister)
                    {
                        list.Add(new KeyValuePair<GXDLMSObject, int>(it, 3));
                    }
                    if (it is GXDLMSDemandRegister)
                    {
                        list.Add(new KeyValuePair<GXDLMSObject, int>(it, 4));
                    }
                }
                if (list.Count != 0)
                {
                    ReadList(list);
                }
            }
            else
            {
                //Read values one by one.
                foreach (GXDLMSObject it in objs)
                {
                    try
                    {
                        if (it is GXDLMSRegister)
                        {
                            //Console.WriteLine(it.Name);
                            arr.Add(it.Name);
                            Read(it, 3);
                        }
                        if (it is GXDLMSDemandRegister)
                        {
                            //Console.WriteLine(it.Name);
                            Read(it, 4);
                        }
                    }
                    catch
                    {
                        //Actaric SL7000 can return error here. Continue reading.
                    }
                }
            }
        }

        public string GetCacheName()
        {
            return Media.ToString().Replace(":", "") + ".xml";
        }

        /// <summary>
        /// Read profile generic columns.
        /// </summary>
        public void GetProfileGenericColumns()
        {
            //Read Profile Generic columns first.
            foreach (GXDLMSObject it in Client.Objects.GetObjects(ObjectType.ProfileGeneric))
            {
                try
                {
                    //If info.
                    if (Trace > TraceLevel.Warning)
                    {
                        Console.WriteLine(it.LogicalName);
                    }
                    Read(it, 3);
                    //If info.
                    if (Trace > TraceLevel.Warning)
                    {
                        GXDLMSObject[] cols = (it as GXDLMSProfileGeneric).GetCaptureObject();
                        StringBuilder sb = new StringBuilder();
                        bool First = true;
                        foreach (GXDLMSObject col in cols)
                        {
                            if (!First)
                            {
                                sb.Append(" | ");
                            }
                            First = false;
                            sb.Append(col.Name);
                            sb.Append(" ");
                            sb.Append(col.Description);
                        }
                        Console.WriteLine(sb.ToString());
                    }
                }
                catch (Exception ex)
                {
                    //Continue reading.
                }
            }
            string path = GetCacheName();
            try
            {
                List<Type> extraTypes = new List<Type>(Gurux.DLMS.GXDLMSClient.GetObjectTypes());
                extraTypes.Add(typeof(GXDLMSAttributeSettings));
                extraTypes.Add(typeof(GXDLMSAttribute));
                XmlSerializer x = new XmlSerializer(typeof(GXDLMSObjectCollection), extraTypes.ToArray());
                using (Stream stream = File.Open(path, FileMode.Create))
                {
                    TextWriter writer = new StreamWriter(stream);
                    x.Serialize(writer, Client.Objects);
                    writer.Close();
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                throw ex;
            }
        }


        public ArrayList GetProfileGenericColumnsNames(string obisCode)
        {
            //List<KeyValuePair<string, int>> readObjects = new List<KeyValuePair<string, int>>();            
            ArrayList arrayList = new ArrayList();
            //Read Profile Generic columns first.
            GXDLMSProfileGeneric it = new GXDLMSProfileGeneric(obisCode);
            try
            {
                //If info.
                if (Trace > TraceLevel.Warning)
                {
                    //Console.WriteLine(it.LogicalName);
                }
                Read(it, 3);
                //If info.
                if (Trace > TraceLevel.Warning)
                {
                    GXDLMSObject[] cols = (it as GXDLMSProfileGeneric).GetCaptureObject();
                    foreach (GXDLMSObject col in cols)
                    {
                        string data = col.Name + "|" + col.Description;
                        arrayList.Add(data);
                    }
                }
            }
            catch (Exception ex)
            {
                //Continue reading.
            }
            return arrayList;
        }

        public ArrayList GetScalerObisCodes(string obisCode)
        {
            //List<KeyValuePair<string, int>> readObjects = new List<KeyValuePair<string, int>>();            
            ArrayList arrayList = new ArrayList();
            //Read Profile Generic columns first.
            GXDLMSProfileGeneric it = new GXDLMSProfileGeneric(obisCode);
            try
            {
                //If info.
                if (Trace > TraceLevel.Warning)
                {
                    //Console.WriteLine(it.LogicalName);
                }
                Read(it, 3);
                //If info.
                if (Trace > TraceLevel.Warning)
                {
                    GXDLMSObject[] cols = (it as GXDLMSProfileGeneric).GetCaptureObject();
                    foreach (GXDLMSObject col in cols)
                    {
                        string data = col.Name.ToString().Trim();
                        arrayList.Add(data);
                    }
                }
            }
            catch (Exception ex)
            {
                //Continue reading.
            }
            return arrayList;
        }

        public void ShowValue(object val, int pos)
        {
            //If trace is info.
            if (Trace > TraceLevel.Warning)
            {
                //If data is array.
                if (val is byte[])
                {
                    val = GXCommon.ToHex((byte[])val, true);
                }
                else if (val is Array)
                {
                    string str = "";
                    for (int pos2 = 0; pos2 != (val as Array).Length; ++pos2)
                    {

                        if (str != "")
                        {
                            str += ", ";
                        }
                        if ((val as Array).GetValue(pos2) is byte[])
                        {
                            str += GXCommon.ToHex((byte[])(val as Array).GetValue(pos2), true);
                        }
                        else
                        {
                            str += (val as Array).GetValue(pos2).ToString();
                        }
                    }
                    val = str;
                }
                else if (val is System.Collections.IList)
                {
                    string str = "[";
                    bool empty = true;
                    foreach (object it2 in val as System.Collections.IList)
                    {
                        if (!empty)
                        {
                            str += ", ";
                        }
                        empty = false;
                        if (it2 is byte[])
                        {
                            str += GXCommon.ToHex((byte[])it2, true);
                        }
                        else
                        {
                            str += it2.ToString();
                        }
                    }
                    str += "]";
                    val = str;
                }
                Console.WriteLine("Index: " + pos + " Value: " + val);
            }
        }

        public void GetProfileGenerics()
        {
            //Find profile generics register objects and read them.
            foreach (GXDLMSObject it in Client.Objects.GetObjects(ObjectType.ProfileGeneric))
            {
                //If trace is info.
                if (Trace > TraceLevel.Warning)
                {
                    Console.WriteLine("-------- Reading " + it.GetType().Name + " " + it.Name + " " + it.Description);
                    //ConnectionControl._mainForm.update("-------- Reading " + it.GetType().Name + " " + it.Name + " " + it.Description);
                }
                long entriesInUse = Convert.ToInt64(Read(it, 7));
                long entries = Convert.ToInt64(Read(it, 8));
                //If trace is info.
                if (Trace > TraceLevel.Warning)
                {
                    Console.WriteLine("Entries: " + entriesInUse + "/" + entries);
                    //ConnectionControl._mainForm.update("Entries: " + entriesInUse + "/" + entries);
                }
                //If there are no columns or rows.
                if (entriesInUse == 0 || (it as GXDLMSProfileGeneric).CaptureObjects.Count == 0)
                {
                    continue;
                }
                //All meters are not supporting parameterized read.
                if ((Client.NegotiatedConformance & (Gurux.DLMS.Enums.Conformance.ParameterizedAccess | Gurux.DLMS.Enums.Conformance.SelectiveAccess)) != 0)
                {
                    try
                    {
                        //Read first row from Profile Generic.
                        object[] rows = ReadRowsByEntry(it as GXDLMSProfileGeneric, 1, 1);
                        //If trace is info.
                        if (Trace > TraceLevel.Warning)
                        {
                            StringBuilder sb = new StringBuilder();
                            foreach (object[] row in rows)
                            {
                                foreach (object cell in row)
                                {
                                    if (cell is byte[])
                                    {
                                        sb.Append(GXCommon.ToHex((byte[])cell, true));
                                    }
                                    else
                                    {
                                        sb.Append(Convert.ToString(cell));
                                    }
                                    sb.Append(" | ");
                                }
                                sb.Append("\r\n");
                            }
                            Console.WriteLine(sb.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error! Failed to read first row: " + ex.Message);
                    }
                }
                //All meters are not supporting parameterized read.
                if ((Client.NegotiatedConformance & (Gurux.DLMS.Enums.Conformance.ParameterizedAccess | Gurux.DLMS.Enums.Conformance.SelectiveAccess)) != 0)
                {
                    try
                    {
                        //Read last day from Profile Generic.
                        object[] rows = ReadRowsByRange(it as GXDLMSProfileGeneric, DateTime.Now.Date, DateTime.MaxValue);
                        //If trace is info.
                        if (Trace > TraceLevel.Warning)
                        {
                            StringBuilder sb = new StringBuilder();
                            foreach (object[] row in rows)
                            {
                                foreach (object cell in row)
                                {
                                    if (cell is byte[])
                                    {
                                        sb.Append(GXCommon.ToHex((byte[])cell, true));
                                    }
                                    else
                                    {
                                        sb.Append(Convert.ToString(cell));
                                    }
                                    sb.Append(" | ");
                                }
                                sb.Append("\r\n");
                            }
                            Console.WriteLine(sb.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error! Failed to read last day: " + ex.Message);
                        //Continue reading.
                    }
                }
            }
        }

        public ArrayList GetProfileGenericsValues(string obisCode)
        {
            ArrayList valList = new ArrayList();
            //Find profile generics register objects and read them.
            //StringBuilder sb = new StringBuilder();
            GXDLMSProfileGeneric it = new GXDLMSProfileGeneric(obisCode);
            //foreach (GXDLMSObject it in Client.Objects.GetObjects(ObjectType.ProfileGeneric))
            //{
            //If trace is info.
            if (Trace > TraceLevel.Warning)
            {
                Console.WriteLine("-------- Reading " + it.GetType().Name + " " + it.Name + " " + it.Description);
            }
            long entriesInUse = Convert.ToInt64(Read(it, 7));
            long entries = Convert.ToInt64(Read(it, 8));
            //If trace is info.
            if (Trace > TraceLevel.Warning)
            {
                Console.WriteLine("Entries: " + entriesInUse + "/" + entries);
            }
            //If there are no columns or rows.
            if (entriesInUse == 0 || (it as GXDLMSProfileGeneric).CaptureObjects.Count == 0)
            {
                // continue;
            }
            //All meters are not supporting parameterized read.
            if ((Client.NegotiatedConformance & (Gurux.DLMS.Enums.Conformance.ParameterizedAccess | Gurux.DLMS.Enums.Conformance.SelectiveAccess)) != 0)
            {
                try
                {
                    //Read first row from Profile Generic.
                    object[] rows = ReadRowsByEntry(it as GXDLMSProfileGeneric, 1, 1);

                    //If trace is info.
                    if (Trace > TraceLevel.Warning)
                    {

                        foreach (object[] row in rows)
                        {
                            foreach (object cell in row)
                            {
                                if (cell is byte[])
                                {
                                    valList.Add(GXCommon.ToHex((byte[])cell, true));
                                    //sb.Append(GXCommon.ToHex((byte[])cell, true));
                                }
                                else
                                {
                                    valList.Add(Convert.ToString(cell));
                                    //sb.Append(Convert.ToString(cell));
                                }
                                //sb.Append(" | ");
                            }
                            //sb.Append("\r\n");
                        }
                        //Console.WriteLine(sb.ToString());
                    }
                }
                catch (Exception ex)
                {
                    //Console.WriteLine("Error! Failed to read first row: " + ex.Message);
                    //Continue reading.
                }
            }
            //All meters are not supporting parameterized read.
            //if ((Client.NegotiatedConformance & (Gurux.DLMS.Enums.Conformance.ParameterizedAccess | Gurux.DLMS.Enums.Conformance.SelectiveAccess)) != 0)
            //{
            //    try
            //    {
            //        //Read last day from Profile Generic.
            //        object[] rows = ReadRowsByRange(it as GXDLMSProfileGeneric, DateTime.Now.Date, DateTime.MaxValue);
            //        //If trace is info.
            //        if (Trace > TraceLevel.Warning)
            //        {
            //            StringBuilder sb = new StringBuilder();
            //            foreach (object[] row in rows)
            //            {
            //                foreach (object cell in row)
            //                {
            //                    if (cell is byte[])
            //                    {
            //                        sb.Append(GXCommon.ToHex((byte[])cell, true));
            //                    }
            //                    else
            //                    {
            //                        sb.Append(Convert.ToString(cell));
            //                    }
            //                    sb.Append(" | ");
            //                }
            //                sb.Append("\r\n");
            //            }
            //            Console.WriteLine(sb.ToString());
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine("Error! Failed to read last day: " + ex.Message);
            //        //Continue reading.
            //    }
            //}
            //}
            return valList;
        }

        public ArrayList ReadDlms(string obisCode, string type)
        {
            ArrayList LIST = new ArrayList();
            GXDLMSProfileGeneric it = new GXDLMSProfileGeneric(obisCode);
            //it.AccessSelector = Gurux.DLMS.Objects.Enums.AccessRange.Range;
            //it.From = DateTimeOffset.Now.AddDays(-1);
            //it.To = DateTimeOffset.Now;
            switch (type)
            {
                case "Genric":
                    object[] rows = Read(it, 2);
                    foreach (object[] row in rows)
                    {
                        foreach (object cell in row)
                        {
                            if (cell is byte[])
                            {
                                LIST.Add(GXCommon.ToHex((byte[])cell, true));
                            }
                            else
                            {
                                LIST.Add(Convert.ToString(cell));
                            }
                        }
                    }
                    break;
                case "Scaler":
                    object Scalervals = Read(it, 2);
                    System.Object[][] gx = (System.Object[][])Scalervals;
                    foreach (object[] row in gx)
                    {
                        foreach (object cell in row)
                        {
                            System.Object[] arr = (System.Object[])cell;
                            string aa1 = arr[0].ToString() + "|" + arr[1].ToString();
                            LIST.Add(aa1);
                        }
                    }
                    break;
                case "Obis":
                    ReadObis(it, 3);
                    GXDLMSObject[] cols = (it as GXDLMSProfileGeneric).GetCaptureObject();
                    foreach (GXDLMSObject col in cols)
                    {
                        GXDLMSRegister r = new GXDLMSRegister();
                        double ax = r.Scaler;
                        string axx = r.Unit.ToString();
                        string data = col.Name.ToString().Trim();// + "|" + col.Description;
                        LIST.Add(data);
                    }
                    break;
            }
            return LIST;
        }

        public long GetEntries(string obisCode, int attribute)
        {
            GXDLMSProfileGeneric it = new GXDLMSProfileGeneric(obisCode);
            long entries = Convert.ToInt64(ReadObis(it, attribute));
            return entries;
        }

        public ArrayList GetScalerValues(string obisCode)
        {
            Function fn = new Function();
            ArrayList scalerList = new ArrayList();
            //Find profile generics register objects and read them.
            //StringBuilder sb = new StringBuilder();
            GXDLMSProfileGeneric it = new GXDLMSProfileGeneric(obisCode);
            //foreach (GXDLMSObject it in Client.Objects.GetObjects(ObjectType.ProfileGeneric))
            //{
            //If trace is info.
            if (Trace > TraceLevel.Warning)
            {
                //Console.WriteLine("-------- Reading " + it.GetType().Name + " " + it.Name + " " + it.Description);
            }
            long entriesInUse = Convert.ToInt64(Read(it, 7));
            long entries = Convert.ToInt64(Read(it, 8));
            //If trace is info.
            if (Trace > TraceLevel.Warning)
            {
                Console.WriteLine("Entries: " + entriesInUse + "/" + entries);
            }
            //If there are no columns or rows.
            if (entriesInUse == 0 || (it as GXDLMSProfileGeneric).CaptureObjects.Count == 0)
            {
                // continue;
            }
            //All meters are not supporting parameterized read.
            if ((Client.NegotiatedConformance & (Gurux.DLMS.Enums.Conformance.ParameterizedAccess | Gurux.DLMS.Enums.Conformance.SelectiveAccess)) != 0)
            {
                try
                {
                    //Read first row from Profile Generic.
                    object[] rows = ReadRowsByEntry(it as GXDLMSProfileGeneric, 1, 1);
                    int i = rows.Length;
                    //If trace is info.
                    if (Trace > TraceLevel.Warning)
                    {
                        foreach (object[] row in rows)
                        {
                            foreach (object cell in row)
                            {
                                // GXStructure gXStructure = (GXStructure)cell;                               
                                //scalerList.Add(gXStructure[0].ToString() + "|" + gXStructure[1].ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error! Failed to read first row: " + ex.Message);
                }
            }
            return scalerList;
        }

        /// <summary>
        /// Read all objects from the meter.
        /// </summary>
        /// <remarks>
        /// It's not normal to read all data from the meter. This is just an example.
        /// </remarks>
        public void GetReadOut()
        {
            foreach (GXDLMSObject it in Client.Objects)
            {
                // Profile generics are read later because they are special cases.
                // (There might be so lots of data and we so not want waste time to read all the data.)
                if (it is GXDLMSProfileGeneric)
                {
                    continue;
                }
                if (!(it is IGXDLMSBase))
                {
                    //If interface is not implemented.
                    //Example manufacturer spesific interface.
                    if (Trace > TraceLevel.Error)
                    {
                        Console.WriteLine("Unknown Interface: " + it.ObjectType.ToString());
                    }
                    continue;
                }
                if (Trace > TraceLevel.Warning)
                {
                    Console.WriteLine("-------- Reading " + it.GetType().Name + " " + it.Name + " " + it.Description);
                }
                foreach (int pos in (it as IGXDLMSBase).GetAttributeIndexToRead(true))
                {
                    try
                    {
                        object val = Read(it, pos);
                        ShowValue(val, pos);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error! " + it.GetType().Name + " " + it.Name + "Index: " + pos + " " + ex.Message);
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
        }

        public byte[] ReadTCP(byte[] data, GXReplyData reply)
        {
            byte[] bb = { 0x00, 0x01, 0x00, 0x10, 0x00, 0x01, 0x00, 0x1F, 0x60, 0x1D, 0xA1, 0x09, 0x06, 0x07, 0x60, 0x85, 0x74, 0x05, 0x08, 0x01, 0x01, 0xBE, 0x10, 0x04, 0x0E, 0x01, 0x00, 0x00, 0x00, 0x06, 0x5F, 0x1F, 0x04, 0x00, 0x00, 0x18, 0x1D, 0xFF, 0xFF };
            object eop = (byte)0x7E;
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
                    //if (!reply.IsStreaming())
                    //{  
                    //    Media.Send(data, null);
                    //}
                    Media.Send(bb, null);
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
                        Debug.WriteLine("Data send failed. Try to resend " + pos.ToString() + "/3");
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
                            Debug.WriteLine("Data send failed. Try to resend " + pos.ToString() + "/3");
                        }
                    }
                    return p.Reply;
                }
                catch (Exception ex)
                {
                    WriteTrace("-> " + DateTime.Now.ToLongTimeString() + "\t" + GXCommon.ToHex(p.Reply, true));
                    //throw ex;

                    return null;
                }
            }

        }
        public byte[] SendAndRecieve(byte[] Cmd, int waitTime, int responselength)
        {
            int iWait = 2000;
            int retry = 0;
            ReceiveParameters<byte[]> p = new ReceiveParameters<byte[]>
            {
                Count = responselength,
                WaitTime = 3000,
            };
            if (Media.IsOpen)
            {
                try
                {
                    while (retry < 1)
                    {
                        Media.ResetSynchronousBuffer();
                        Media.Send(Cmd, null);
                        System.Threading.Thread.Sleep(1000);
                        Media.Receive(p);
                        if (p.Reply != null && p.Reply.Length >= 1)
                        {
                            p.WaitTime = 100;
                            do
                            {
                                p.Count = 1;
                                Media.Receive(p);
                                iWait--;
                            } while (p.Reply.Length != responselength && iWait != 0);
                            retry = 1;
                        }
                        retry++;
                    }
                }
                catch
                {
                    return null;
                }
            }
            return p.Reply;
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
                        WriteTrace("<- " + DateTime.Now.ToLongTimeString() + "\t" + GXCommon.ToHex(data, true));
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
                            Debug.WriteLine("Data send failed. Try to resend " + pos.ToString() + "/3");
                        }
                    }
                }
                catch (Exception ex)
                {
                    WriteTrace("-> " + DateTime.Now.ToLongTimeString() + "\t" + GXCommon.ToHex(p.Reply, true));
                    //throw ex;
                }
            }
            WriteTrace("-> " + DateTime.Now.ToLongTimeString() + "\t" + GXCommon.ToHex(p.Reply, true));
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

        public void ReadDLMSPacketImage(byte[] data, GXReplyData reply)
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
                        //  //MeterSettings._mediaSettings.update("<- " + DateTime.Now.ToLongTimeString() + "\t" + GXCommon.ToHex(data, true));
                        //Test2._mediaSettings.update("<- " + DateTime.Now.ToLongTimeString() + "\t" + GXCommon.ToHex(data, true));
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
                        //MeterSettings._mediaSettings.update("Data send failed. Trying to resend ");
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
                            //MeterSettings._mediaSettings.update("Data send failed. Trying to resend ");
                        }
                    }
                }
                catch (Exception ex)
                {
                    //MeterSettings._mediaSettings.update("-> " + DateTime.Now.ToLongTimeString() + "\t" + GXCommon.ToHex(p.Reply, true));
                    //throw ex;
                }
            }
            //  //MeterSettings._mediaSettings.update("-> " + DateTime.Now.ToLongTimeString() + "\t" + GXCommon.ToHex(p.Reply, true));
            //Test2._mediaSettings.update("-> " + DateTime.Now.ToLongTimeString() + "\t" + GXCommon.ToHex(p.Reply, true));
            if (reply.Error != 0)
            {
                if (reply.Error == (short)ErrorCode.Rejected)
                {
                    Thread.Sleep(1000);
                    ReadDLMSPacketImage(data, reply);
                }
                else
                {
                    throw new GXDLMSException(reply.Error);
                }
            }
        }

        public bool ReadDataBlockImage(byte[][] data, GXReplyData reply)
        {
            if (data == null)
            {
                return true;
            }
            foreach (byte[] it in data)
            {
                reply.Clear();
                ReadDataBlockImage(it, reply);
            }
            return reply.Error == 0;
        }
        public void ReadDataBlockImage(byte[] data, GXReplyData reply)
        {
            ReadDLMSPacketImage(data, reply);
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
                    ReadDLMSPacketImage(data, reply);
                    if (Trace > TraceLevel.Info)
                    {
                        //If data block is read.
                        if ((reply.MoreData & RequestTypes.Frame) == 0)
                        {
                            //MeterSettings._mediaSettings.update("+");
                        }
                        else
                        {
                            //MeterSettings._mediaSettings.update("-");
                        }
                    }
                }
            }
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
        /// <summary>
        /// Read attribute value.
        /// </summary>
        /// <param name="it">COSEM object to read.</param>
        /// <param name="attributeIndex">Attribute index.</param>
        /// <returns>Read value.</returns>
        public object[] Read(GXDLMSObject it, int attributeIndex)
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
            return (object[])Client.UpdateValue(it, attributeIndex, reply.Value);
            //return Client.UpdateValue(it, attributeIndex, reply.Value);            
        }
        public object Read2(GXDLMSObject it, int attributeIndex)
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
            //return (object[])Client.UpdateValue(it, attributeIndex, reply.Value);
            return Client.UpdateValue(it, attributeIndex, reply.Value);
        }

        public object ReadObis(GXDLMSObject it, int attributeIndex)
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
        /// <summary>
        /// Read list of attributes.
        /// </summary>
        public void ReadList(List<KeyValuePair<GXDLMSObject, int>> list)
        {
            byte[][] data = Client.ReadList(list);
            GXReplyData reply = new GXReplyData();
            List<object> values = new List<object>();
            foreach (byte[] it in data)
            {
                ReadDataBlock(it, reply);
                if (list.Count != 1 && reply.Value is object[])
                {
                    values.AddRange((object[])reply.Value);
                }
                else if (reply.Value != null)
                {
                    //Value is null if data is send in multiple frames.
                    values.Add(reply.Value);
                }
                reply.Clear();
            }
            if (values.Count != list.Count)
            {
                throw new Exception("Invalid reply. Read items count do not match.");
            }
            Client.UpdateValues(list, values);
        }
        /// <summary>
        /// Write attribute value.
        /// </summary>
        public bool Write(GXDLMSObject it, int attributeIndex)
        {
            try
            {
                GXReplyData reply = new GXReplyData();
                ReadDataBlock(Client.Write(it, attributeIndex), reply);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// Method attribute value.
        /// </summary>
        public void Method(GXDLMSObject it, int attributeIndex, object value, DataType type)
        {
            GXReplyData reply = new GXReplyData();
            ReadDataBlock(Client.Method(it, attributeIndex, value, type), reply);
        }

        /// <summary>
        /// Read Profile Generic Columns by entry.
        /// </summary>
        public object[] ReadRowsByEntry(GXDLMSProfileGeneric it, int index, int count)
        {
            GXReplyData reply = new GXReplyData();
            ReadDataBlock(Client.ReadRowsByEntry(it, index, count), reply);
            return (object[])Client.UpdateValue(it, 2, reply.Value);
        }

        /// <summary>
        /// Read Profile Generic Columns by range.
        /// </summary>
        public object[] ReadRowsByRange(GXDLMSProfileGeneric it, DateTime start, DateTime end)
        {
            GXReplyData reply = new GXReplyData();
            ReadDataBlock(Client.ReadRowsByRange(it, start, end), reply);
            return (object[])Client.UpdateValue(it, 2, reply.Value);
        }
        public ArrayList ReadRowsByRange1(GXDLMSProfileGeneric it, DateTime start, DateTime end, string type)
        {
            ArrayList LIST = new ArrayList();
            GXReplyData reply = new GXReplyData();
            ReadDataBlock(Client.ReadRowsByRange(it, start, end), reply);
            switch (type)
            {
                case "Genric":
                    object[] rows = (object[])Client.UpdateValue(it, 2, reply.Value);
                    Program.entriesInUse = reply.Count;
                    foreach (object[] row in rows)
                    {
                        foreach (object cell in row)
                        {
                            if (cell is byte[])
                            {
                                LIST.Add(GXCommon.ToHex((byte[])cell, true));
                            }
                            else
                            {
                                LIST.Add(Convert.ToString(cell));
                            }
                        }
                    }
                    break;
                case "Scaler":
                    object Scalervals = Read(it, 2);
                    System.Object[][] gx = (System.Object[][])Scalervals;
                    foreach (object[] row in gx)
                    {
                        foreach (object cell in row)
                        {
                            System.Object[] arr = (System.Object[])cell;
                            string aa1 = arr[0].ToString() + "|" + arr[1].ToString();
                            LIST.Add(aa1);
                        }
                    }
                    break;
                case "Obis":
                    ReadObis(it, 3);
                    GXDLMSObject[] cols = (it as GXDLMSProfileGeneric).GetCaptureObject();
                    foreach (GXDLMSObject col in cols)
                    {
                        GXDLMSRegister r = new GXDLMSRegister();
                        double ax = r.Scaler;
                        string axx = r.Unit.ToString();
                        string data = col.Name.ToString().Trim();// + "|" + col.Description;
                        LIST.Add(data);
                    }
                    break;
            }
            return LIST;
        }

        /// <summary>
        /// Disconnect.
        /// </summary>
        public void Disconnect()
        {
            if (Media != null && Client != null)
            {
                try
                {
                    if (Trace > TraceLevel.Info)
                    {
                        Console.WriteLine("Disconnecting from the meter.");
                    }
                    GXReplyData reply = new GXReplyData();
                    ReadDLMSPacket(Client.DisconnectRequest(), reply);
                    Media.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Media = null;
                Client = null;
            }
        }

        /// <summary>
        /// Close connection to the meter.
        /// </summary>
        public void Close()
        {
            if (Media != null && Client != null)
            {
                try
                {
                    if (Trace > TraceLevel.Info)
                    {
                        Console.WriteLine("Disconnecting from the meter.");
                    }
                    GXReplyData reply = new GXReplyData();
                    try
                    {
                        ReadDataBlock(Client.ReleaseRequest(), reply);
                    }
                    catch (Exception ex)
                    {
                        //All meters don't support Release.
                        Console.WriteLine("Release failed. " + ex.Message);
                    }
                    reply.Clear();
                    ReadDLMSPacket(Client.DisconnectRequest(), reply);
                    Media.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Media = null;
                Client = null;
            }
        }

        /// <summary>
        /// Write trace.
        /// </summary>
        /// <param name="line"></param>
        void WriteTrace(string line)
        {
            if (Trace > TraceLevel.Info)
            {
                Console.WriteLine(line);
            }
            using (FileStream fs = File.Open("trace.txt", FileMode.Append))
            {
                using (TextWriter writer = new StreamWriter(fs))
                {
                    writer.WriteLine(line);
                }
            }
        }

        public void ImageUpdate2(GXDLMSImageTransfer target)
        {
            //Check that image transfer is enabled.
            GXReplyData reply = new GXReplyData();
            //Test2._mediaSettings.update("Image transfer is enabled");
            //Test2._mediaSettings.update("Reading image block size");
            //Step 1: Read image block size.
            ReadDataBlockImage(Client.Read(target, 2), reply);
            Client.UpdateValue(target, 2, reply.Value);
            // Step 3: Transfers ImageBlocks.
            //  //MeterSettings._mediaSettings.update("Transfering ImageBlocks");
            //Test2._mediaSettings.update("Transfering ImageBlocks");
            int imageBlockCount;
            ReadDataBlockImage(Client.Read(target, 3), reply);
            // MessageBox.Show("Total Block count === " + imageBlockCount, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //Step 4: Check the completeness of the Image.
            //  //MeterSettings._mediaSettings.update("Checking the completeness of the Image");
            //Test2._mediaSettings.update("Checking the completeness of the Image");
            ReadDataBlockImage(Client.Read(target, 3), reply);
            Client.UpdateValue(target, 3, reply.Value);

            // Step 5: The Image is verified;
            //  //MeterSettings._mediaSettings.update("The Image is verified");
            //Test2._mediaSettings.update("The Image is verified");
            Console.WriteLine("Image is verified");
            ReadDataBlockImage(target.ImageVerify(Client), reply);
            // Step 6: Before activation, the Image is checked;


        }
    }
}
