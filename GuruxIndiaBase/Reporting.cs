using System.Collections;
using System.Globalization;

namespace Gurux_Testing
{
    class Reporting
    {
        public static ArrayList Get_Meter_NUMBER_N_RTC(string TYPE, string Meter_Number, string FromDT, string ToDT)
        {
            CultureInfo tCulture = CultureInfo.InvariantCulture;
            ArrayList result = new ArrayList();
            string path = Program.Default_Target_Directory;
            string[] filePaths = Directory.GetFiles(path, Meter_Number + "*.txt", SearchOption.AllDirectories);
            ArrayList _Array_List1 = new ArrayList();
            ArrayList _Array_List_Final = new ArrayList();
            string Ext = "*#*.txt";
            string temp = null;
            string[] temp_1 = null;
            string[] temp_2 = null;
            string[] arExtensions1 = Ext.Split(';');
            int fCompare, tCompare;
            foreach (string filter in arExtensions1)
            {
                string[] strFiles1 = Directory.GetFiles(path, filter);
                string D_Date = "";
                _Array_List1.AddRange(strFiles1);
                for (int i = 0; i < strFiles1.Length; i++)
                {
                    FileInfo fiTemp1 = new FileInfo(strFiles1[i]);
                    temp = fiTemp1.Name;


                    temp_1 = temp.Split('#');
                    if (TYPE == "NUMBER")// && fiTemp1.Length == 19968) 
                    {
                        if (Meter_Number != "")
                        {
                            if (temp_1[0] == Meter_Number)
                            {
                                temp_1 = temp.Split('_');
                                _Array_List_Final.Add(temp_1[0]);
                            }
                            else
                                MessageBox.Show("Selected Meter Not Found!", "Search Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            temp_1 = temp.Split('#');
                            D_Date = temp_1[1].Substring(0, 10);
                            if (FromDT != "" && ToDT != "")
                            {
                                DateTime DT = DateTime.ParseExact(D_Date, "dd-MM-yyyy", tCulture);
                                DateTime F_DT = DateTime.ParseExact(FromDT, "dd-MM-yyyy", tCulture);
                                DateTime TO_DT = DateTime.ParseExact(ToDT, "dd-MM-yyyy", tCulture);
                                fCompare = DateTime.Compare(DT, F_DT);
                                tCompare = DateTime.Compare(DT, TO_DT);
                                if ((fCompare == 0 || fCompare == 1) && (tCompare == 0 || tCompare == -1))
                                {
                                    _Array_List_Final.Add(temp_1[0]);
                                }
                            }
                        }
                    }
                    else if (TYPE == "RTC")//&& fiTemp1.Length == 19968)
                    {
                        temp_2 = temp_1[1].Split('.');
                        D_Date = temp_2[0].Substring(0, 10);
                        if (temp_1[0] == Meter_Number)
                        {
                            DateTime DT = DateTime.ParseExact(D_Date, "dd-MM-yyyy", tCulture);
                            DateTime F_DT = DateTime.ParseExact(FromDT, "dd-MM-yyyy", tCulture);
                            DateTime TO_DT = DateTime.ParseExact(ToDT, "dd-MM-yyyy", tCulture);
                            fCompare = DateTime.Compare(DT, F_DT);
                            tCompare = DateTime.Compare(DT, TO_DT);
                            if ((fCompare == 0 || fCompare == 1) && (tCompare == 0 || tCompare == -1))
                            {
                                _Array_List_Final.Add(temp_2[0]);
                            }
                        }
                    }
                    _Array_List1.Sort();
                }
            }

            // To removing duplicate items
            Int32 index = 0;
            while (index < _Array_List_Final.Count - 1)
            {
                if (_Array_List_Final[index] as string == _Array_List_Final[index + 1] as string)
                    _Array_List_Final.RemoveAt(index);
                else
                    index++;
            }
            return _Array_List_Final;
        }


    }
}
