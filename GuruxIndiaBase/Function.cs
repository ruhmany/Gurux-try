using Gurux.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gurux_Testing
{
    public static class DlmsData
    {
        static public string[] Parameters = null;
        static public string[] Description = null;
        static public string[] ObisCode = null;
        static public string[] Unit = null;
        static public string[] DLMSDATA = new string[4];
        static public ArrayList RecordList = new ArrayList();
        static public ArrayList RecordList2 = new ArrayList();

    }
    class Function
    {
        public string getObis(string name)
        {
            string obis = "";
            switch(name)
            {
                case "Nameplate":
                    obis = "0.0.94.91.10.255";
                    break;
                case "Instant":
                    obis = "1.0.94.91.0.255";
                    break;
                case "InstantScaler":
                    obis = "1.0.94.91.3.255";
                    break;
                case "Billing":
                    obis = "1.0.98.1.0.255";
                    break;
                case "BillingScaler":
                    obis = "1.0.94.91.6.255";
                    break;
                case "BlockLoadProfile":
                    obis = "1.0.99.1.0.255";
                    break;
                case "BlockLoadScaler":
                    obis = "1.0.94.91.4.255";
                    break;
                case "DailyLoadProfile":
                    obis = "1.0.99.2.0.255";
                    break;
                case "DailyLoadScaler":
                    obis = "1.0.94.91.5.255";
                    break;
                case "VoltageEvent":
                    obis = "0.0.99.98.0.255";
                    break;
                case "CurrentEvent":
                    obis = "0.0.99.98.1.255";
                    break;
                case "PowerFailEvent":
                    obis = "0.0.99.98.2.255";
                    break;
                case "TransactionEvent":
                    obis = "0.0.99.98.3.255";
                    break;
                case "OtherEvent":
                    obis = "0.0.99.98.4.255";
                    break;
                case "CoverOpenEvent":
                    obis = "0.0.99.98.5.255";
                    break;
                case "IndianEvents":
                    obis = "1.0.94.91.7.255";
                    break;
            }
            return obis;
        }

        public string decodeUnit_Factor(string ob, ArrayList scalerUnits, ArrayList scalerObis)
        {
            string unit_factor = "Na|Na";
            if (scalerObis != null)
            {
                for (int i = 0; i < scalerObis.Count; i++)
                {
                    if (ob == scalerObis[i].ToString())
                    {
                        unit_factor = scalerUnits[i].ToString();
                    }                  
                }
            }
            return unit_factor;
        }

        ObisList ob = new ObisList();
        public bool decodeAllData(ArrayList obisList, ArrayList ValueList, ArrayList scalerUnits, ArrayList scalerObis,int usedEntry)
        {
            //ValueList[14] = 101;
            if (obisList.Count == 0) return false;
            try
            {
                //DlmsData.Parameters = null; DlmsData.Description = null; DlmsData.ObisCode = null; DlmsData.Unit = null;
                //Array.Resize(ref DlmsData.Parameters, ValueList.Count);
                //Array.Resize(ref DlmsData.Description, ValueList.Count);
                //Array.Resize(ref DlmsData.ObisCode, ValueList.Count);
                //Array.Resize(ref DlmsData.Unit, ValueList.Count);   
                
                DlmsData.RecordList.Clear(); DlmsData.RecordList2.Clear();
                int index = 0;
                for (int e = 0; e < usedEntry; e++)
                {                    
                    for (int i = 0; i < obisList.Count; i++)
                    {
                        //if(i==22)
                        //{

                        //}
                        Record record = new Record();
                        string obCode = obisList[i].ToString();
                        int len = ValueList[index].ToString().Length;
                        if (len == 35)//for date value
                        {
                            record.Name = ob.obisName(obCode, "val");
                            record.Obis = obCode;
                            byte[] buff = GXCommon.HexToBytes(ValueList[index].ToString().Trim());
                            record.Value = GetRTC(buff, 0);
                            record.Unit = "DateTime";                            
                        }
                        else
                        {
                            record.Name = ob.obisName(obCode, "val");
                            record.Obis = obCode;
                            string uf = "";                            
                                switch (record.Obis)
                                {
                                    //case "1.0.3.7.0.255":
                                    //    uf = "1|var";
                                    //    break;
                                    //case "1.0.8.8.0.255":
                                    //    uf = "Na2|Na";
                                    //    break;
                                    //case "1.0.5.8.0.255":
                                    //    uf = "Na3|Na";
                                    //    break;
                                    default:
                                        uf = decodeUnit_Factor(obCode, scalerUnits, scalerObis);
                                        break;
                                }
                            
                            string[] fac_Units = uf.Split('|');
                            switch (fac_Units[0])
                            {
                                case "Na":
                                    record.Value = ValueList[index].ToString();
                                    record.Unit = "    -    ";
                                    break;
                                //case "Na1":
                                //    record.Value = ValueList[index].ToString();
                                //    record.Unit = "var";
                                //    break;
                                //case "Na2":
                                //    record.Value = ValueList[index].ToString();
                                //    record.Unit = "varh";
                                //    break;
                                //case "Na3":
                                //    record.Value = ValueList[index].ToString();
                                //    record.Unit = "varh";
                                //    break;

                                default:
                                    Int64 val = Int64.Parse(ValueList[index].ToString());
                                    string factoredValue = getDivisionFactor(fac_Units[0], val);
                                    string unit = GetUnit(fac_Units[1]);
                                    if (record.Obis == "1.0.3.7.0.255")
                                    {
                                        float valc = float.Parse(factoredValue);
                                        record.Value = Math.Abs(valc).ToString();
                                    }
                                    else
                                    {
                                        record.Value = factoredValue;
                                    }
                                    record.Unit = unit;
                                    break;
                            }
                            
                            if (record.Name.Contains("Lastest Event ID"))
                            {
                                string nm = ob.temperName(record.Value);
                                if (nm != "") { record.Name = nm; }
                            }
                        }
                        string[] DLMSDATA = new string[4];
                        DLMSDATA[0] = record.Name;
                        DLMSDATA[1] = record.Value;
                        DLMSDATA[2] = record.Unit;
                        DLMSDATA[3] = record.Obis;
                        DlmsData.RecordList.Add(DLMSDATA);
                        DlmsData.RecordList2.Add(record);

                        #region switchcase
                        //switch (obCode)
                        //{
                        //    case "0.0.96.1.0.255":
                        //        DlmsData.Parameters[i] = ValueList[i].ToString();
                        //        DlmsData.Description[i] = "Manufacturing Number";
                        //        DlmsData.ObisCode[i] = obCode;
                        //        DlmsData.Unit[i] = " ";
                        //        //desArray[i] = "Manufacturing Number";                        
                        //        break;
                        //    case "0.0.96.1.2.255":
                        //        DlmsData.Parameters[i] = ValueList[i].ToString();
                        //        DlmsData.Description[i] = "Serial Number";
                        //        DlmsData.ObisCode[i] = obCode;
                        //        DlmsData.Unit[i] = " ";
                        //        //desArray[i] = "Serial Number";
                        //        break;
                        //    case "0.0.96.1.1.255":
                        //        DlmsData.Parameters[i] = ValueList[i].ToString();
                        //        DlmsData.Description[i] = "Manufacturer Name";
                        //        DlmsData.ObisCode[i] = obCode;
                        //        DlmsData.Unit[i] = " ";
                        //        //desArray[i] = "Manufacturer Name";
                        //        break;
                        //    case "1.0.0.2.0.255":
                        //        DlmsData.Parameters[i] = ValueList[i].ToString();
                        //        DlmsData.Description[i] = "Firmware Version For Meter";
                        //        DlmsData.ObisCode[i] = obCode;
                        //        DlmsData.Unit[i] = " ";
                        //        //desArray[i] = "Firmware Identifier";
                        //        break;
                        //    case "0.0.94.91.9.255":
                        //        DlmsData.Parameters[i] = ValueList[i].ToString();
                        //        DlmsData.Description[i] = "Meter Type";
                        //        DlmsData.ObisCode[i] = obCode;
                        //        DlmsData.Unit[i] = " ";
                        //        //desArray[i] = "Ch. 0 Identifiers for India";
                        //        break;
                        //    case "0.0.94.91.11.255":
                        //        DlmsData.Parameters[i] = ValueList[i].ToString();
                        //        DlmsData.Description[i] = "Meter Category";
                        //        DlmsData.ObisCode[i] = obCode;
                        //        DlmsData.Unit[i] = " ";
                        //        //desArray[i] = "Ch. 0 Identifiers for India";
                        //        break;
                        //    case "0.0.94.91.12.255":
                        //        DlmsData.Parameters[i] = ValueList[i].ToString();
                        //        DlmsData.Description[i] = "Meter Current Rating";
                        //        DlmsData.ObisCode[i] = obCode;
                        //        DlmsData.Unit[i] = " ";
                        //        //desArray[i] = "Ch. 0 Identifiers for India";
                        //        break;
                        //    case "0.0.96.1.4.255":
                        //        DlmsData.Parameters[i] = ValueList[i].ToString();
                        //        DlmsData.Description[i] = "Meter Year Of Manufacture";
                        //        DlmsData.ObisCode[i] = obCode;
                        //        DlmsData.Unit[i] = " ";
                        //        //desArray[i] = "Year";
                        //        break;
                        //    case "0.0.1.0.0.255":
                        //        byte[] buff = GXCommon.HexToBytes(ValueList[i].ToString().Trim());
                        //        DlmsData.Parameters[i] = GetRTC(buff, 0);
                        //        DlmsData.Description[i] = "RTC";
                        //        DlmsData.ObisCode[i] = obCode;
                        //        DlmsData.Unit[i] = "Clock";
                        //        //desArray[i] = "RTC";
                        //        break;
                        //    case "1.0.12.7.0.255":
                        //        DlmsData.Parameters[i] = ValueList[i].ToString();
                        //        DlmsData.Description[i] = "Voltage";
                        //        DlmsData.ObisCode[i] = obCode;
                        //        DlmsData.Unit[i] = " ";
                        //        //desArray[i] = "Voltage";
                        //        break;
                        //    case "1.0.11.7.0.255":
                        //        DlmsData.Parameters[i] = ValueList[i].ToString();
                        //        DlmsData.Description[i] = "Phase Current";
                        //        DlmsData.ObisCode[i] = obCode;
                        //        DlmsData.Unit[i] = " ";
                        //        //desArray[i] = "Current";
                        //        break;
                        //    case "1.0.91.7.0.255":
                        //        DlmsData.Parameters[i] = ValueList[i].ToString();
                        //        DlmsData.Description[i] = "Neutral Current";
                        //        DlmsData.ObisCode[i] = obCode;
                        //        DlmsData.Unit[i] = " ";
                        //        //desArray[i] = "Lo Current (neutral)";
                        //        break;
                        //    case "1.0.13.7.0.255":
                        //        DlmsData.Parameters[i] = ValueList[i].ToString();
                        //        DlmsData.Description[i] = "Power Factor";
                        //        DlmsData.ObisCode[i] = obCode;
                        //        DlmsData.Unit[i] = " ";
                        //        //desArray[i] = "Power Factor";
                        //        break;
                        //    case "1.0.14.7.0.255":
                        //        DlmsData.Parameters[i] = ValueList[i].ToString();
                        //        DlmsData.Description[i] = "Frequency";
                        //        DlmsData.ObisCode[i] = obCode;
                        //        DlmsData.Unit[i] = " ";
                        //        //desArray[i] = "Frequency";
                        //        break;
                        //    case "1.0.9.7.0.255":
                        //        DlmsData.Parameters[i] = ValueList[i].ToString();
                        //        DlmsData.Description[i] = "Apparent Power-KVA";
                        //        DlmsData.ObisCode[i] = obCode;
                        //        DlmsData.Unit[i] = " ";
                        //        //desArray[i] = "Apparent power+ (QI+QIV)";
                        //        break;
                        //    case "1.0.1.7.0.255":
                        //        DlmsData.Parameters[i] = ValueList[i].ToString();
                        //        DlmsData.Description[i] = "Active Power-kW";
                        //        DlmsData.ObisCode[i] = obCode;
                        //        DlmsData.Unit[i] = " ";
                        //        //desArray[i] = "Active power+ (QI+QIV)";
                        //        break;
                        //    case "1.0.1.8.0.255":
                        //        DlmsData.Parameters[i] = ValueList[i].ToString();
                        //        DlmsData.Description[i] = "Cumulative Energy-kWh";
                        //        DlmsData.ObisCode[i] = obCode;
                        //        DlmsData.Unit[i] = " ";
                        //        //desArray[i] = "Active power+ (QI+QIV) Time integral 1 Rate 0 (0 is total) ";
                        //        break;
                        //    case "1.0.9.8.0.255":
                        //        DlmsData.Parameters[i] = ValueList[i].ToString();
                        //        DlmsData.Description[i] = "Cumulative Energy-kVAh";
                        //        DlmsData.ObisCode[i] = obCode;
                        //        DlmsData.Unit[i] = " ";
                        //        //desArray[i] = "Apparent power+ (QI+QIV) Time integral 1 Rate 0 (0 is total)";
                        //        break;
                        //    case "1.0.1.6.0.255":
                        //        if (i == 10)
                        //        {
                        //            DlmsData.Parameters[i] = ValueList[i].ToString();
                        //            DlmsData.Description[i] = "Maximum Demand-kW";
                        //        }
                        //        else if (i == 11)
                        //        {
                        //            byte[] buff2 = GXCommon.HexToBytes(ValueList[i].ToString().Trim());
                        //            DlmsData.Parameters[i] = GetRTC(buff2, 0);
                        //            DlmsData.Description[i] = "Maximum Demand-kW Date";
                        //        }
                        //        DlmsData.ObisCode[i] = obCode;
                        //        DlmsData.Unit[i] = " ";
                        //        //desArray[i] = "Active power+ (QI+QIV) Max. 1 Rate 0 (0 is total)";
                        //        break;
                        //    case "1.0.9.6.0.255":
                        //        if (i == 12)
                        //        {
                        //            DlmsData.Parameters[i] = ValueList[i].ToString();
                        //            DlmsData.Description[i] = "Maximum Demand-kVA";
                        //        }
                        //        else if (i == 13)
                        //        {
                        //            byte[] buff3 = GXCommon.HexToBytes(ValueList[i].ToString().Trim());
                        //            DlmsData.Parameters[i] = GetRTC(buff3, 0);
                        //            DlmsData.Description[i] = "Maximum Demand-kVA Date";
                        //        }
                        //        DlmsData.ObisCode[i] = obCode;
                        //        DlmsData.Unit[i] = " ";
                        //        //desArray[i] = "Apparent power+ (QI+QIV) Max. 1 Rate 0 (0 is total)";
                        //        break;
                        //    case "0.0.94.91.14.255":
                        //        DlmsData.Parameters[i] = ValueList[i].ToString();
                        //        DlmsData.Description[i] = "Cumulative power ON Duration in Minutes";
                        //        DlmsData.ObisCode[i] = obCode;
                        //        DlmsData.Unit[i] = " ";
                        //        //desArray[i] = "Identifiers for India";
                        //        break;
                        //    case "0.0.94.91.0.255":
                        //        DlmsData.Parameters[i] = ValueList[i].ToString();
                        //        DlmsData.Description[i] = "Cumulative Tamper Count";
                        //        DlmsData.ObisCode[i] = obCode;
                        //        DlmsData.Unit[i] = " ";
                        //        //desArray[i] = "Identifiers for India";
                        //        break;
                        //    case "0.0.0.1.0.255":
                        //        DlmsData.Parameters[i] = ValueList[i].ToString();
                        //        DlmsData.Description[i] = "Cumulative Billing count";
                        //        DlmsData.ObisCode[i] = obCode;
                        //        DlmsData.Unit[i] = " ";
                        //        // desArray[i] = "Identifiers for India";
                        //        break;
                        //    case "0.0.96.2.0.255":
                        //        DlmsData.Parameters[i] = ValueList[i].ToString();
                        //        DlmsData.Description[i] = "Cumulative Programming Count";
                        //        DlmsData.ObisCode[i] = obCode;
                        //        DlmsData.Unit[i] = " ";
                        //        //desArray[i] = "No. of configuration program changes";
                        //        break;
                        //    case "1.0.2.8.0.255":
                        //        DlmsData.Parameters[i] = ValueList[i].ToString();
                        //        DlmsData.Description[i] = "Cumulative Energy-kWh";
                        //        DlmsData.ObisCode[i] = obCode;
                        //        DlmsData.Unit[i] = " ";
                        //        //desArray[i] = "Sum Li Active power- (QII+QIII) Time integral 1 Rate 0 (0 is total)";
                        //        break;
                        //    case "1.0.10.8.0.255":
                        //        DlmsData.Parameters[i] = ValueList[i].ToString();
                        //        DlmsData.Description[i] = "Cumulative Energy-kVAh";
                        //        DlmsData.ObisCode[i] = obCode;
                        //        DlmsData.Unit[i] = " ";
                        //        //desArray[i] = "Sum Li Apparent power- (QII+QIII) Time integral 1 Rate 0 (0 is total)";
                        //        break;
                        //    case "0.0.96.3.10.255":
                        //        DlmsData.Parameters[i] = ValueList[i].ToString();
                        //        DlmsData.Description[i] = "Disconnect control";
                        //        DlmsData.ObisCode[i] = obCode;
                        //        DlmsData.Unit[i] = " ";
                        //        //desArray[i] = "Disconnect control";
                        //        break;
                        //    case "0.0.17.0.0.255":
                        //        DlmsData.Parameters[i] = ValueList[i].ToString();
                        //        DlmsData.Description[i] = "Load Limit Threshold (kW)";
                        //        DlmsData.ObisCode[i] = obCode;
                        //        DlmsData.Unit[i] = " ";
                        //        //desArray[i] = "Limiter";
                        //        break;
                        //}
                        #endregion
                        if (index != (ValueList.Count-1)) { index = index + 1; }
                    }
                }
                //if (scalerObis != null)
                //{
                //    for (int i = 0; i < scalerObis.Count; i++)
                //    {
                //        int index = Array.IndexOf(DlmsData.ObisCode, scalerObis[i]);
                //        Int64 val = Int64.Parse(ValueList[index].ToString());
                //        string[] fac_Units = scalerUnits[i].ToString().Split('|');
                //        string factorValue = getDivisionFactor(fac_Units[0], val);
                //        string unit = GetUnit(fac_Units[1]);
                //        DlmsData.Parameters[index] = factorValue;
                //        DlmsData.Unit[index] = unit;
                //    }
                //}
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public string GetUnit(string un)
        {
            string unit = null;
            switch (un)
            {
                case "1":
                    unit = "Year";
                    break;
                case "2":
                    unit = "Month";
                    break;
                case "3":
                    unit = "Week";
                    break;
                case "4":
                    unit = "Day";
                    break;
                case "5":
                    unit = "Hour";
                    break;
                case "6":
                    unit = "Minute";
                    break;
                case "7":
                    unit = "Second";
                    break;
                case "8":
                    unit = "Degree";
                    break;
                case "9":
                    unit = "Degree Celsius";
                    break;
                case "11":
                    unit = "Metre";
                    break;
                case "27":
                    unit = "Watt";
                    break;
                case "28":
                    unit = "Volt Ampere(VA)";
                    break;
                case "29":
                    unit = "Volt Ampere Reactive(VAr)";
                    break;
                case "30":
                    unit = "Watt Hour(Wh)";
                    break;
                case "31":
                    unit = "Volt Ampere Hour(VAh)";
                    break;
                case "32":
                    unit = "Volt Ampere Reactive Hour(VArh)";
                    break;
                case "33":
                    unit = "Ampere";
                    break;
                case "34":
                    unit = "Voltage";
                    break;
                case "35":
                    unit = "Voltage";
                    break;
                case "44":
                    unit = "Hertz";
                    break;
                case "255":
                    unit = "Count";
                    break;
            }
            return unit;
        }

        private string getDivisionFactor(string val, Int64 data)
        {
            string res = "";
            switch (val)
            {
                case "-3"://0xFD://-3
                    res = ((float)data / 1000).ToString("N3");
                    break;

                case "-2"://0xFE://-2
                    res = ((float)data / 100).ToString("N2");
                    break;

                case "-1"://0xFF://-1
                    res = ((float)data / 10).ToString("N1");
                    break;

                case "2":
                    res = (((float)data * 100) / 1000).ToString("N3");
                    break;

                case "1":
                    res = (((float)data * 10) / 1000).ToString("N3");
                    break;

                case "0":
                    res = data.ToString("D2");
                    break;

                default:
                    if (data != 0)
                        res = (data).ToString("D2");
                    break;
            }
            return res;
        }

        public byte[] getDataBuffer(byte[] arr)
        {
            int buffLength = ByteToInt2(arr, 6, 2);
            byte[] dataArray = new byte[buffLength];
            Array.Copy(arr, 8, dataArray, 0, buffLength);
            return dataArray;
        }
        public Int32 ByteToInt2(byte[] data, int start, int length)
        {
            Int32 result = 0;
            string str = "";
            for (int i = start; i < (start + length); i++)
            {
                str = str + (data[i].ToString("X2"));
            }
            result = unchecked(Convert.ToInt32(str, 16));
            return result;
        }

        public string GetRTC(byte[] buff, int start)
        {
            string rtc = null;
            Int64 dd, mm, yy, dayofweek, hh, mn, ss, deviation_mins, status;
            yy = ByteToInt(buff, start, 2); start += 2;
            mm = ByteToInt(buff, start, 1); start += 1;
            dd = ByteToInt(buff, start, 1); start += 1;
            dayofweek = ByteToInt(buff, start, 1); start += 1;
            hh = ByteToInt(buff, start, 1); start += 1;
            mn = ByteToInt(buff, start, 1); start += 1;
            ss = ByteToInt(buff, start, 1); start += 1;
            start += 1;
            deviation_mins = ByteToInt(buff, start, 2); start += 2;
            status = ByteToInt(buff, start, 1); start += 1;
            rtc = dd.ToString("D2") + "/" + mm.ToString("D2") + "/" + yy.ToString("D4") + " " + hh.ToString("D2") + ":" + mn.ToString("D2") + ":" + ss.ToString("D2");
            if (rtc == "255/255/65535 255:255:255")
            {
                rtc = "00/00/0000 00:00:00";
            }
            return rtc;
        }

        public Int32 ByteToIntRev(byte[] data, int start, int length)
        {
            Int32 result = 0;
            string str = "";
            for (int i = ((start + length) - 1); i >= start; i--)
            {
                str = str + (data[i].ToString("X2"));
            }
            result = Convert.ToInt32(str, 16);
            return result;

        }

        public Int64 ByteToInt(byte[] data, int start, int length)
        {
            Int64 result = 0;
            string str = "";
            for (int i = start; i < (start + length); i++)
            {
                str = str + (data[i].ToString("X2"));
            }
            result = unchecked(Convert.ToInt32(str, 16));
            return result;
        }

        public string[] getVAlues(object val)
        {
            string[] arrValues = null;
            Array[] array = (Array[])val;
            
            int objectLength = array.Length;
            for (int i = 0; i < objectLength; i++)
            {
                int numofParameters = array[i].Length;
                Array.Resize(ref arrValues, numofParameters);
                for (int j = 0; j < numofParameters; j++)
                {                    
                    //if(array[i] is byte[])
                    //{
                    //    byte[] buff = GXCommon.HexToBytes(array[i].ToString().Trim());
                    //    DlmsData.Parameters[i] = GetRTC(buff, 0);
                    //}
                    arrValues[j] = array[i].GetValue(j).ToString();

                }
            }
            return arrValues;
        }

        public void FileWrite(string directory, List<string> list1)
        {
            string path = directory + "\\" + "HH-" + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + ".txt";
            TextWriter TR = new StreamWriter(path, true);
            for (int i = 0; i < list1.Count; i++)
            {
                TR.WriteLine(list1[i].ToString().TrimEnd());
            }
            TR.Close();
        }

        public string getNameFormObisCode(string obiscode)
        {
            string name = null;
            switch (obiscode)
            {
                case "0.0.0.1.0.255":
                    name = "Cumulative Billing Count";
                    break;

                case "0.0.0.1.2.255":
                    name = "Billing Date";
                    break;

                case "0.0.1.0.0.255":
                    name = "Real Time Clock Date and Time";
                    break;

                case "0.0.13.0.0.255":
                    name = "Activity Calendar for Time Zones";
                    break;

                case "0.0.15.0.0.255":
                    name = "Single Action Schedule for Billing Dates";
                    break;

                case "0.0.40.0.1.255":
                    name = "Lowest Level";
                    break;

                case "0.0.40.0.2.255":
                    name = "Low Level (LLS)";
                    break;
                case "0.0.40.0.3.255":
                    name = "High Level (HLS)";
                    break;
                case "0.0.40.0.0.255":
                    name = "Current Association";
                    break;

                case "0.0.42.0.0.255":
                    name = "Logical Device Name";
                    break;

                case "0.0.94.91.0.255":
                    name = "Cumulative Tamper Count";
                    break;

                case "0.0.94.91.9.255":
                    name = "Meter Type";
                    break;

                case "0.0.94.91.10.255":
                    name = "Name Plate Details";
                    break;

                case "0.0.94.91.11.255":
                    name = "Meter Category";
                    break;

                case "0.0.94.91.12.255":
                    name = "Current Rating";
                    break;

                case "0.0.96.1.0.255":
                    name = "Meter Serial Number";
                    break;

                case "0.0.96.1.1.255":
                    name = "Manufacturer Name";
                    break;

                case "0.0.96.1.4.255":
                    name = "Meter Year of Manufacture";
                    break;

                case "0.0.96.2.0.255":
                    name = "Cumulative Programming Count";
                    break;

                case "1.0.0.2.0.255":
                    name = "Firmware Version";
                    break;

                case "1.0.0.8.0.255":
                    name = "Demand Integration Period";
                    break;

                case "1.0.0.8.4.255":
                    name = "Profile Capture Period";
                    break;

                case "1.0.1.6.0.255":
                    name = "Maximum Demand kW";
                    break;

                case "1.0.1.7.0.255":
                    name = "Active Power kW";
                    break;

                case "1.0.1.8.0.255":
                    name = "Cumulative Energy kWh";
                    break;

                case "1.0.9.6.0.255":
                    name = "Maximum Demand kVA";
                    break;

                case "1.0.9.7.0.255":
                    name = "Apparent Power KVA";
                    break;

                case "1.0.9.8.0.255":
                    name = "Cumulative Energy kVAh";
                    break;

                case "1.0.13.7.0.255":
                    name = "Signed Power Factor";
                    break;
                case "1.0.14.7.0.255":
                    name = "Frequency Hz";
                    break;
                case "1.0.94.91.0.255":
                    name = "Instant Profile";
                    break;
                case "1.0.94.91.3.255":
                    name = "Scaler Profile(Instant)";
                    break;
                case "1.0.94.91.4.255":
                    name = "Scaler Profile(Block Load Profile)";
                    break;
                case "1.0.94.91.6.255":
                    name = "Scaler Profile(Billing)";
                    break;
                case "1.0.94.91.5.255":
                    name = "Scaler Profile(Daily Load Profile)";
                    break;
                case "1.0.94.91.7.255":
                    name = "Scaler Profile(Indian Events)";//0.0.96.7.0.255
                    break;
                case "1.0.98.1.0.255":
                    name = "Billing Profile";
                    break;
                case "1.0.99.1.0.255":
                    name = "Block Load Profile";
                    break;
                case "1.0.99.2.0.255":
                    name = "Daily Load Profile";
                    break;
                case "0.0.96.7.0.255":
                    name = "Number of Power Failure";
                    break;
                case "0.0.94.91.8.255":
                    name = "Cumulative Power Failure Duration";
                    break;
                case "0.0.94.91.14.255":
                    name = "Cumulative Power Failure Duration";
                    break;
                case "1.0.11.7.0.255":
                    name = "Phase Current";
                    break;
                case "1.0.12.7.0.255":
                    name = "Voltage";
                    break;
                case "1.0.91.7.0.255":
                    name = "Neutral Current";
                    break;
                case "0.0.96.3.10.255":
                    name = "Disconnect Control";
                    break;
                case "1.0.3.7.0.255":
                    name = "Signed Reactive Power kVAr";
                    break;
                case "1.0.5.8.0.255":
                    name = "Cumulative Energy kVArh (Lag)";
                    break;
                case "1.0.8.8.0.255":
                    name = "Cumulative Energy kVArh (Lead)";
                    break;
                case "1.0.31.7.0.255":
                    name = "R Phase Current";
                    break;
                case "1.0.32.7.0.255":
                    name = "R Phase Voltage";
                    break;
                case "1.0.33.7.0.255":
                    name = "R Phase Power Factor";
                    break;
                case "1.0.51.7.0.255":
                    name = "Y Phase Current";
                    break;
                case "1.0.52.7.0.255":
                    name = "Y Phase Voltage";
                    break;
                case "1.0.53.7.0.255":
                    name = "Y Phase Power Factor";
                    break;
                case "1.0.71.7.0.255":
                    name = "B Phase Current";
                    break;
                case "1.0.72.7.0.255":
                    name = "B Phase Voltage";
                    break;
                case "1.0.73.7.0.255":
                    name = "B Phase Power Factor";
                    break;
                case "1.0.0.8.5.255":
                    name = "Capture Period for Daily Load Profile";
                    break;
                case "0.0.96.11.0.255":
                    name = "Lastest Event ID Voltage Event";
                    break;
                case "0.0.96.11.1.255":
                    name = "Lastest Event ID Current Event";
                    break;
                case "0.0.96.11.2.255":
                    name = "Lastest Event ID Power Failure Event";
                    break;
                case "0.0.96.11.3.255":
                    name = "Lastest Event ID Transaction Event";
                    break;
                case "0.0.96.11.4.255":
                    name = "Lastest Event ID Other Event";
                    break;
                case "0.0.96.11.5.255":
                    name = "Lastest Event ID Non-rollover Event";
                    break;
                case "0.0.96.11.6.255":
                    name = "Lastest Event ID Control Event";
                    break;
                case "1.0.12.27.0.255":
                    name = "Block LP Avg. Volatge";
                    break;
                case "1.0.1.29.0.255":
                    name = "Block LP Block energy, kWh";
                    break;
                case "1.0.9.29.0.255":
                    name = "Block LP Block energy, kVAh";
                    break;
                case "1.0.13.0.0.255":
                    name = "Average Power Factor for Billing Period";
                    break;
                case "1.0.1.8.1.255":
                    name = "Billing Cumulative Energy kWh for Tariff Zone 1";
                    break;
                case "1.0.1.8.2.255":
                    name = "Billing Cumulative Energy kWh for Tariff Zone 2";
                    break;
                case "1.0.1.8.3.255":
                    name = "Billing Cumulative Energy kWh for Tariff Zone 3";
                    break;
                case "1.0.1.8.4.255":
                    name = "Billing Cumulative Energy kWh for Tariff Zone 4";
                    break;
                case "1.0.9.8.1.255":
                    name = "Billing Cumulative Energy kVAh for Tariff Zone 1";
                    break;
                case "1.0.9.8.2.255":
                    name = "Billing Cumulative Energy kVAh for Tariff Zone 2";
                    break;
                case "1.0.9.8.3.255":
                    name = "Billing Cumulative Energy kVAh for Tariff Zone 3";
                    break;
                case "1.0.9.8.4.255":
                    name = "Billing Cumulative Energy kVAh for Tariff Zone 4";
                    break;
                case "0.0.94.91.13.255":
                    name = "Billing Total Power On Duration in Minutes";
                    break;
                case "0.0.99.98.0.255":
                    name = "Voltage Event Profile";
                    break;
                case "0.0.99.98.1.255":
                    name = "Current Event Profile";
                    break;
                case "0.0.99.98.2.255":
                    name = "Power Failure Event Profile";
                    break;
                case "0.0.99.98.3.255":
                    name = "Transaction Event Profile";
                    break;
                case "0.0.99.98.4.255":
                    name = "Other Event Profile";
                    break;
                case "0.0.99.98.5.255":
                    name = "Non-rollover Event Profile";
                    break;
                case "0.0.99.98.6.255":
                    name = "Control Event Profile";
                    break;
                case "0.0.0.1.1.255":
                    name = "Customer Billing Cycle Number";
                    break;
                case "0.0.22.0.0.255":
                    name = "Physical Device Addressing";
                    break;
                case "0.150.10.150.150.255":
                    name = "Billing Generation";
                    break;
                case "0.150.13.150.150.255":
                    name = "Billing Paid";
                    break;
                case "1.0.2.29.0.255":
                    name = "Sum Li Active power- (QII+QIII)";
                    break;
                case "1.0.10.29.0.255":
                    name = "Sum Li Apparent power- (QII+QIII)";
                    break;
                case "1.0.11.27.0.255":
                    name = "Any phase Current avg.";
                    break;                
                default:
                    name = "NA";
                    break;
            }
            return name;
        }
    }
}
