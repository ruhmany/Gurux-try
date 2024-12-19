using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gurux_Testing
{
    class NamePlate
    {
        static public string SL, Manufacture, ManufactureName, F_Ver, Type, Category, Rating, Year;
    }
    class Instant1
    {
        static public string Rtc, voltage, p_cur, n_cur, pf, frq, ap_pw, ac_pw, kwh, kvah, md_kw, md_kw_dt, md_kva, md_kva_dt, pw_minutes, tm_count, bill_count, prog_count, energy_kwh, energykVah, dis_control, load_thres;
    }

    class Decode
    {
        int sort_no = 0;
        Database.DataSet1 dataSet = new Database.DataSet1();
        ObisList ob = new ObisList();
        Billing Bill = new Billing();
        BlockLoad Block = new BlockLoad();
        LS_Daily Dailyload = new LS_Daily();
        Tamper Tamper = new Tamper();

        public DataSet InsertData()
        {
            int count1 = 0, count2 = 0, ind = 0, capturePeriod = 0, Totalrecords = 0, entries = 0;
            try
            {
                string dataProfile = "NA";              
                string[] AllLines = File.ReadAllLines(Program.Default_Target_Directory + "\\" + Program.FileName);
                string[] arr = Program.FileName.Split('#');
                string MeterId = arr[0];
                while (ind < AllLines.Length)
                {
                    string[] split = AllLines[ind].Split('|'); ind += 1;
                    dataProfile = split[0];
                    entries = Convert.ToInt32(split[1]);
                    Totalrecords = Convert.ToInt32(split[2]);
                    count1 = Convert.ToInt32(split[2]);
                    if (count1 == 0)
                    {
                        ind += 1;
                        continue;
                    }
                    //capturePeriod = Convert.ToInt32(split[1]);
                    //count2 = Convert.ToInt32(split[4]);
                    //if (dataProfile == "Billing")
                    //{ count1 = Convert.ToInt32(split[3]); }
                    switch (dataProfile)
                    {
                        #region Nameplate
                        case "Nameplate":
                            NamePlate.SL = GetSplitString(AllLines[ind]); ind += 1;
                            NamePlate.Manufacture = GetSplitString(AllLines[ind]); ind += 1;
                            NamePlate.ManufactureName = GetSplitString(AllLines[ind]); ind += 1;
                            NamePlate.F_Ver = GetSplitString(AllLines[ind]); ind += 1;
                            NamePlate.Type = GetSplitString(AllLines[ind]); ind += 1;
                            NamePlate.Category = GetSplitString(AllLines[ind]); ind += 1;
                            NamePlate.Rating = GetSplitString(AllLines[ind]); ind += 1;
                            //NamePlate.I_CT_Ratio = "00"; //GetSplitString(AllLines[ind]); ind += 1;
                            //NamePlate.V_CT_Ratio = "00";//GetSplitString(AllLines[ind]); ind += 1;
                            NamePlate.Year = GetSplitString(AllLines[ind]); ind += 1;
                            dataSet.Name_Plate.Rows.Add(NamePlate.SL, NamePlate.Manufacture, NamePlate.ManufactureName, NamePlate.F_Ver, NamePlate.Type, NamePlate.Category, NamePlate.Rating, NamePlate.Year);
                            break;
                        #endregion
                        #region Instant
                        case "Instant":
                            Instant1.Rtc = GetSplitString(AllLines[ind]); ind += 1;
                            Instant1.voltage = GetSplitString(AllLines[ind]); ind += 1;
                            Instant1.p_cur = GetSplitString(AllLines[ind]); ind += 1;
                            Instant1.n_cur = GetSplitString(AllLines[ind]); ind += 1;
                            Instant1.pf = GetSplitString(AllLines[ind]); ind += 1;
                            Instant1.frq = GetSplitString(AllLines[ind]); ind += 1;
                            Instant1.ap_pw = GetSplitString(AllLines[ind]); ind += 1;
                            Instant1.ac_pw = GetSplitString(AllLines[ind]); ind += 1;
                            Instant1.kwh = GetSplitString(AllLines[ind]); ind += 1;
                            Instant1.kvah = GetSplitString(AllLines[ind]); ind += 1;
                            Instant1.md_kw = GetSplitString(AllLines[ind]); ind += 1;
                            Instant1.md_kw_dt = GetSplitString(AllLines[ind]); ind += 1;
                            Instant1.md_kva = GetSplitString(AllLines[ind]); ind += 1;
                            Instant1.md_kva_dt = GetSplitString(AllLines[ind]); ind += 1;
                            Instant1.pw_minutes = GetSplitString(AllLines[ind]); ind += 1;
                            Instant1.tm_count = GetSplitString(AllLines[ind]); ind += 1;
                            Instant1.bill_count = GetSplitString(AllLines[ind]); ind += 1;
                            Instant1.prog_count = GetSplitString(AllLines[ind]); ind += 1;
                            Instant1.energy_kwh = GetSplitString(AllLines[ind]); ind += 1;
                            Instant1.energykVah = GetSplitString(AllLines[ind]); ind += 1;
                            Instant1.dis_control = GetSplitString(AllLines[ind]); ind += 1;
                            Instant1.load_thres = GetSplitString(AllLines[ind]); ind += 1;
                            dataSet.Instant.Rows.Add(Instant1.Rtc, Instant1.voltage, Instant1.p_cur, Instant1.n_cur, Instant1.pf, Instant1.frq, Instant1.ap_pw, Instant1.ac_pw, Instant1.kwh,
                                Instant1.kvah, Instant1.md_kw, Instant1.md_kw_dt, Instant1.md_kva, Instant1.md_kva_dt, Instant1.pw_minutes, Instant1.tm_count, Instant1.bill_count, Instant1.prog_count,
                                Instant1.energy_kwh, Instant1.energykVah, Instant1.dis_control, Instant1.load_thres);
                            break;
                        #endregion
                        #region Billing
                        case "Billing":
                            for (int i = 1; i <= entries; i++)
                            {
                                Bill.TimeStamp = GetSplitString(AllLines[ind]); ind += 1;
                                Bill.PowerFactor = GetSplitString(AllLines[ind]); ind += 1;
                                Bill.Energy_Kwh = GetSplitString(AllLines[ind]); ind += 1;
                                Bill.Ac_pw_1 = GetSplitString(AllLines[ind]); ind += 1;
                                Bill.Ac_pw_2 = GetSplitString(AllLines[ind]); ind += 1;
                                Bill.Ac_pw_3 = GetSplitString(AllLines[ind]); ind += 1;
                                Bill.Ac_pw_4 = GetSplitString(AllLines[ind]); ind += 1;
                                Bill.Energy_Kvah = GetSplitString(AllLines[ind]); ind += 1;
                                Bill.Ap_pw_1 = GetSplitString(AllLines[ind]); ind += 1;
                                Bill.Ap_pw_2 = GetSplitString(AllLines[ind]); ind += 1;
                                Bill.Ap_pw_3 = GetSplitString(AllLines[ind]); ind += 1;
                                Bill.Ap_pw_4 = GetSplitString(AllLines[ind]); ind += 1;
                                Bill.Md_Kw = GetSplitString(AllLines[ind]); ind += 1;
                                Bill.Md_Kw_dt = GetSplitString(AllLines[ind]); ind += 1;
                                Bill.Md_Kva = GetSplitString(AllLines[ind]); ind += 1;
                                Bill.Md_Kva_dt = GetSplitString(AllLines[ind]); ind += 1;
                                Bill.Pwr_min = GetSplitString(AllLines[ind]); ind += 1;
                                Bill.ExEnergy_Kwh = GetSplitString(AllLines[ind]); ind += 1;
                                Bill.ExEnergy_Kvah = GetSplitString(AllLines[ind]); ind += 1;

                                dataSet.Billing.Rows.Add(Bill.TimeStamp, Bill.PowerFactor, Bill.Energy_Kwh, Bill.Ac_pw_1, Bill.Ac_pw_2, Bill.Ac_pw_3, Bill.Ac_pw_4, Bill.Energy_Kvah,
                                  Bill.Ap_pw_1, Bill.Ap_pw_2, Bill.Ap_pw_3, Bill.Ap_pw_4, Bill.Md_Kw, Bill.Md_Kw_dt, Bill.Md_Kva, Bill.Md_Kva_dt, Bill.Pwr_min, Bill.ExEnergy_Kwh, Bill.ExEnergy_Kvah);                                
                                
                            }
                            for (int i = 1; i <= entries; i++)
                            {
                                dataSet.TARIFF.Rows.Add(i, MeterId, Bill.TimeStamp, i, "1", Bill.Ac_pw_1, Bill.Ap_pw_1);
                                dataSet.TARIFF.Rows.Add(i, MeterId, Bill.TimeStamp, i, "2", Bill.Ac_pw_2, Bill.Ap_pw_2);
                                dataSet.TARIFF.Rows.Add(i, MeterId, Bill.TimeStamp, i, "3", Bill.Ac_pw_3, Bill.Ap_pw_3);
                                dataSet.TARIFF.Rows.Add(i, MeterId, Bill.TimeStamp, i, "4", Bill.Ac_pw_4, Bill.Ap_pw_4);
                            }
                                break;
                        #endregion
                        #region Blockload
                        case "BlockLoadProfile":
                            DataTable tempTable = new DataTable();
                            tempTable.Columns.Add("groupNo");
                            tempTable.Columns.Add("meter_no");
                            tempTable.Columns.Add("date");
                            tempTable.Columns.Add("interval");
                            tempTable.Columns.Add("pr1");
                            tempTable.Columns.Add("pr2");
                            tempTable.Columns.Add("pr3");
                            tempTable.Columns.Add("pr4");
                            tempTable.Columns.Add("pr5");
                            tempTable.Columns.Add("pr6");
                            for (int i = 1; i <= entries; i++)
                            {
                                string date = GetSplitString(AllLines[ind]); ind += 1;
                                Block.Timestamp = date.Substring(0, 10);
                                Block.interval= date.Substring(11, 5);
                                Block.parameter1 = GetSplitString(AllLines[ind]); ind += 1;
                                Block.parameter2 = GetSplitString(AllLines[ind]); ind += 1;
                                Block.parameter3 = GetSplitString(AllLines[ind]); ind += 1;
                                Block.parameter4 = GetSplitString(AllLines[ind]); ind += 1;
                                Block.parameter5 = GetSplitString(AllLines[ind]); ind += 1;
                                Block.parameter6 = GetSplitString(AllLines[ind]); ind += 1;
                                tempTable.Rows.Add(i, MeterId, Block.Timestamp, Block.interval, Block.parameter1, Block.parameter1, Block.parameter3, Block.parameter4, Block.parameter5, Block.parameter6);
                            }                           
                            var Groups = from table in tempTable.AsEnumerable()
                                         group table by new { colName = table["date"] } into GroupBy
                                         select new
                                         {
                                             Value = GroupBy.Key,
                                             ColumnValues = GroupBy
                                         };
                            int k = Groups.Count();
                            int grupNo = 1;

                            foreach (var key in Groups)
                            {
                                foreach (var columnValue in key.ColumnValues)
                                {
                                    dataSet.BlockLoad.Rows.Add(grupNo, MeterId, columnValue.ItemArray[2], columnValue.ItemArray[3], columnValue.ItemArray[4], columnValue.ItemArray[5], columnValue.ItemArray[6], columnValue.ItemArray[7], columnValue.ItemArray[8], columnValue.ItemArray[9]);                                   
                                }
                                grupNo++;
                            }
                            //dataSet.BlockLoad.Rows.Add(Block.Timestamp,Block.parameter1, Block.parameter2, Block.parameter3, Block.parameter4, Block.parameter5, Block.parameter6);
                            break;
                        #endregion
                        #region Blockload
                        case "DailyLoadProfile":
                            /*  for (int i = 0; i < entries; i++)  //neeraj
                              {
                                  string date = GetSplitString(AllLines[ind]); ind += 1;
                                  Dailyload.Timestamp = date.Substring(0, 10);
                                  Dailyload.kWh = GetSplitString(AllLines[ind]); ind += 1;
                                  Dailyload.kVAh = GetSplitString(AllLines[ind]); ind += 1;
                                  Dailyload.kWh3 = GetSplitString(AllLines[ind]); ind += 1;
                                  Dailyload.kVAh3 = GetSplitString(AllLines[ind]); ind += 1;
                                  dataSet.Daily_LS.Rows.Add(MeterId, Dailyload.Timestamp, Dailyload.kWh, Dailyload.kVAh);
                              }
                              //for (int j = 0; j < count1; j++)
                              //{
                              //    dataSet.Daily_LS.Rows.Add(MeterId, Dailyload.Timestamp, Dailyload.kWh, Dailyload.kVAh);
                              //}*/   //neeraj
                            DataTable blockTemp = new DataTable();
                            blockTemp.Columns.Add("groupNo");
                            blockTemp.Columns.Add("meter_no");
                            blockTemp.Columns.Add("date");
                            blockTemp.Columns.Add("interval");
                            blockTemp.Columns.Add("pr1");
                            blockTemp.Columns.Add("pr2");
                            blockTemp.Columns.Add("pr3");
                           // BlockGhana1ph LSGhana1ph = new BlockGhana1ph();
                            for (int i = 0; i < entries; i++)
                            {
                                string date = GetSplitString(AllLines[ind]); ind += 1;
                                Dailyload.Date.Add(date);
                                Dailyload.Interval.Add(date.Substring(11, 5));
                                Dailyload.Volt.Add((Convert.ToDouble(GetSplitString(AllLines[ind])) / 1000).ToString("N2")); ind += 1;
                                Dailyload.kwh.Add((Convert.ToDouble(GetSplitString(AllLines[ind])) / 1000).ToString("N2")); ind += 1;
                                Dailyload.wh.Add((Convert.ToDouble(GetSplitString(AllLines[ind])) / 1000).ToString("N2")); ind += 1;
                                Dailyload.vah.Add((Convert.ToDouble(GetSplitString(AllLines[ind])) / 1000).ToString("N2")); ind += 1;
                                blockTemp.Rows.Add(i, MeterId, Dailyload.Date[i], Dailyload.Interval[i], Dailyload.Volt[i], Dailyload.kwh[i]);
                            }
                            var GroupsG1PH = from table in blockTemp.AsEnumerable()
                                             group table by new { colName = table["date"] } into GroupBy
                                             select new
                                             {
                                                 Value = GroupBy.Key,
                                                 ColumnValues = GroupBy
                                             };
                            int k1 = GroupsG1PH.Count();
                            int grupNo1 = 1;

                            foreach (var key in GroupsG1PH)
                            {
                                foreach (var columnValue in key.ColumnValues)
                                {
                                    dataSet.BlockLoad.Rows.Add(grupNo1, MeterId, columnValue.ItemArray[2], columnValue.ItemArray[3], columnValue.ItemArray[4], columnValue.ItemArray[5], "", "", "", "");
                                }
                                grupNo1++;
                            }

                            break;
                        #endregion
                        #region Events
                        case "CurrentEvent":
                        case "OtherEvent":
                            for (int i = 0; i < entries; i++)
                            {
                                Tamper.Date = (GetSplitString(AllLines[ind])); ind += 1;
                                Tamper.Code = (GetSplitString(AllLines[ind])); ind += 1;
                                Tamper.Name = ob.temperName(Tamper.Code);
                                Tamper.Vr = (GetSplitString(AllLines[ind])); ind += 1;
                                Tamper.Ir = (GetSplitString(AllLines[ind])); ind += 1;
                                Tamper.PFr = (GetSplitString(AllLines[ind])); ind += 1;
                                Tamper.CumkWh = (GetSplitString(AllLines[ind])); ind += 1;                                
                                Tamper.Tcount= (GetSplitString(AllLines[ind])); ind += 1;
                                //Tamper.Event = (GetSplitString(AllLines[ind])); ind += 1;                               
                                dataSet.TamperEvents.Rows.Add(MeterId, Tamper.Date, Tamper.Code, Tamper.Name, Tamper.Vr, Tamper.Ir, Tamper.PFr, Tamper.CumkWh, Tamper.Tcount);
                                //dataSet.TEMP_TAMPER.Rows.Add(MeterId, Tamper.Date, Tamper.Code, Tamper.Name, Tamper.Event, Tamper.Vr, Tamper.Vy, Tamper.Vb, Tamper.Ir, Tamper.Iy, Tamper.Ib, Tamper.PFr, Tamper.PFy, Tamper.PFb, Tamper.CumkWh, dataProfile, GetTamperSortNo(Convert.ToInt16(Tamper.Code[i])));
                            }                           
                            break;
                        case "PowerFailEvent":
                        case "TransactionEvent":
                            for (int i = 0; i < entries; i++)
                            {
                                Tamper.Date = (GetSplitString(AllLines[ind])); ind += 1;
                                Tamper.Code = (GetSplitString(AllLines[ind])); ind += 1;
                                Tamper.Name = ob.temperName(Tamper.Code);                                                   
                                dataSet.TamperEvents.Rows.Add(MeterId, Tamper.Date, Tamper.Code, Tamper.Name, Tamper.Vr, Tamper.Ir, Tamper.PFr, Tamper.CumkWh, Tamper.Tcount);                                
                            }
                            break;
                            #endregion
                    }
                }
                dataSet.Names.Rows.Add(MeterId, dataProfile, 0);
                    return dataSet;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return null;
            }           
        }

        private string GetSplitString(string str)
        {
            string[] val = null;
            val = str.Split('|');
            return val[1];
        }

        public int GetTamperSortNo(int id)
        {
            switch (id)
            {
                case 1:
                    sort_no = 1;
                    break;
                case 2:
                    sort_no = 1;
                    break;
                case 3:
                    sort_no = 2;
                    break;
                case 4:
                    sort_no = 2;
                    break;
                case 5:
                    sort_no = 3;
                    break;
                case 6:
                    sort_no = 3;
                    break;
                case 7:
                    sort_no = 4;
                    break;
                case 8:
                    sort_no = 4;
                    break;
                case 9:
                    sort_no = 5;
                    break;
                case 10:
                    sort_no = 5;
                    break;
                case 11:
                    sort_no = 6;
                    break;
                case 12:
                    sort_no = 6;
                    break;
                case 51:
                    sort_no = 7;
                    break;
                case 52:
                    sort_no = 7;
                    break;
                case 53:
                    sort_no = 8;
                    break;
                case 54:
                    sort_no = 8;
                    break;
                case 55:
                    sort_no = 9;
                    break;
                case 56:
                    sort_no = 9;
                    break;
                case 57:
                    sort_no = 10;
                    break;
                case 58:
                    sort_no = 10;
                    break;
                case 59:
                    sort_no = 11;
                    break;
                case 60:
                    sort_no = 11;
                    break;
                case 61:
                    sort_no = 12;
                    break;
                case 62:
                    sort_no = 12;
                    break;
                case 63:
                    sort_no = 13;
                    break;
                case 64:
                    sort_no = 13;
                    break;
                case 65:
                    sort_no = 14;
                    break;
                case 66:
                    sort_no = 14;
                    break;
                case 67:
                    sort_no = 15;
                    break;
                case 68:
                    sort_no = 15;
                    break;
                case 101:
                    sort_no = 16;
                    break;
                case 102:
                    sort_no = 16;
                    break;
                case 151:
                    sort_no = 17;
                    break;
                case 152:
                    sort_no = 18;
                    break;
                case 153:
                    sort_no = 19;
                    break;
                case 154:
                    sort_no = 20;
                    break;
                case 155:
                    sort_no = 21;
                    break;
                case 201:
                    sort_no = 22;
                    break;
                case 202:
                    sort_no = 22;
                    break;
                case 203:
                    sort_no = 23;
                    break;
                case 204:
                    sort_no = 23;
                    break;
                case 205:
                    sort_no = 24;
                    break;
                case 206:
                    sort_no = 24;
                    break;
                case 251:
                    sort_no = 25;
                    break;
                case 301:
                    sort_no = 26;
                    break;
                case 302:
                    sort_no = 26;
                    break;
                case 69:
                    sort_no = 27;
                    break;
                case 70:
                    sort_no = 27;
                    break;
                case 207:
                    sort_no = 28;
                    break;
                case 208:
                    sort_no = 28;
                    break;
                case 41:
                    sort_no = 29;
                    break;
                case 42:
                    sort_no = 29;
                    break;
            }
            return sort_no;
        }
    }
}
