using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Gurux_Testing
{    
    class Databases
    {
        public bool checkDB_Conn()
        {
            bool isConn = false;
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(Program.Connection);
                conn.Open();
                isConn = true;
            }
            catch (MySqlException ex)
            {
                isConn = false;
                switch (ex.Number)
                {
                    case 1042: // Unable to connect to any of the specified MySQL hosts (Check Server,Port)
                        break;
                    case 0: // Access denied (Check DB name,username,password)
                        break;
                    default:
                        break;
                }
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return isConn;
        }
        public void ConnectMeter(string ip)
        {
            string query = "";
            if (getiP(ip, "tb_connected_clients"))
            {
                query = "UPDATE `tb_connected_clients` SET mode=?mode WHERE ipv6 = '" + ip.Trim() + "'";
            }
            else
            {
                query = "INSERT INTO `tb_connected_clients`(`ipv6`, `serial`, `tempers`,`mode` ) VALUES(?ipv6, ?serial, ?tempers, ?mode)";
            }
            using (MySqlConnection cn = new MySqlConnection(Program.Connection))
            {
                try
                {
                    cn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, cn))
                    {
                        cmd.Parameters.Add("?ipv6", MySqlDbType.VarChar).Value = ip;
                        cmd.Parameters.Add("?serial", MySqlDbType.VarChar).Value = "-";
                        cmd.Parameters.Add("?tempers", MySqlDbType.VarChar).Value = "-";
                        cmd.Parameters.Add("?mode", MySqlDbType.VarChar).Value = "Connected";
                        cmd.ExecuteNonQuery();
                    }
                    cn.Close();
                }
                catch (MySqlException ex)
                {
                    cn.Close(); // MessageBox.Show("Error in adding mysql row. Error: " + ex.Message);
                }
            }
        }
        public void pushConnect(string ip, string serial, string tempers)
        {
            string query = "";
            if (getiP(ip, "tb_connected_clients"))
            {
                query = "UPDATE `tb_connected_clients` SET ipv6=?ipv6,serial=?serial, tempers =?tempers, mode=?mode WHERE ipv6 = '" + ip.Trim() + "'";
            }
            else
            {
                query = "INSERT INTO `tb_connected_clients`(`ipv6`, `serial`, `tempers`,`mode` ) VALUES(?ipv6, ?serial, ?tempers, ?mode)";
            }
            using (MySqlConnection cn = new MySqlConnection(Program.Connection))
            {
                try
                {
                    cn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, cn))
                    {
                        cmd.Parameters.Add("?ipv6", MySqlDbType.VarChar).Value = ip;
                        cmd.Parameters.Add("?serial", MySqlDbType.VarChar).Value = serial;
                        cmd.Parameters.Add("?tempers", MySqlDbType.VarChar).Value = tempers;
                        if (tempers.Contains("Last Gasp"))
                        {
                            cmd.Parameters.Add("?mode", MySqlDbType.VarChar).Value = "Disconnected";
                        }
                        else { cmd.Parameters.Add("?mode", MySqlDbType.VarChar).Value = "Connected"; }
                        cmd.ExecuteNonQuery();
                    }
                    cn.Close();
                }
                catch (MySqlException ex)
                {
                    cn.Close(); // MessageBox.Show("Error in adding mysql row. Error: " + ex.Message);
                }
            }
        }

        public void pushInstant(string ip, string serial, string voltage, string current, string ncurrent, string pf, string frequency, string app_pwr, string act_pwr,
            string cum_kwh, string temper_count, string bill_per_count)
        {
            string query = "";
            if (getiP(ip, "tb_pushinstant_data"))
            {
                query = "UPDATE `tb_pushinstant_data` SET ipv6 = ?ipv6, serial = ?serial, voltage = ?voltage, neutral_current = ?neutral_current," +
                    "power_factor = ?power_factor, frequency = ?frequency, app_power_kva = ?app_power_kva, act_power_kw = ?act_power_kw, cum_kwh = ?cum_kwh," +
                    "cum_temper_count = ?cum_temper_count, billing_period_count = ?billing_period_count WHERE ipv6 = '" + ip.Trim() + "'";
            }
            else
            {
                query = "INSERT INTO `tb_pushinstant_data`(`ipv6`, `serial`, `voltage`, `current`, `neutral_current`, `power_factor`, `frequency`, `app_power_kva`, `act_power_kw`, `cum_kwh`, `cum_temper_count`, `billing_period_count`) " +
                                                   "VALUES(?ipv6, ?serial, ?voltage, ?current, ?neutral_current, ?power_factor, ?frequency, ?app_power_kva, ?act_power_kw, ?cum_kwh, ?cum_temper_count, ?billing_period_count)";
            }
            using (MySqlConnection cn = new MySqlConnection(Program.Connection))
            {
                try
                {
                    cn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, cn))
                    {
                        cmd.Parameters.Add("?ipv6", MySqlDbType.VarChar).Value = ip;
                        cmd.Parameters.Add("?serial", MySqlDbType.VarChar).Value = serial;
                        cmd.Parameters.Add("?voltage", MySqlDbType.VarChar).Value = voltage;
                        cmd.Parameters.Add("?current", MySqlDbType.VarChar).Value = current;
                        cmd.Parameters.Add("?neutral_current", MySqlDbType.VarChar).Value = ncurrent;
                        cmd.Parameters.Add("?power_factor", MySqlDbType.VarChar).Value = pf;
                        cmd.Parameters.Add("?frequency", MySqlDbType.VarChar).Value = frequency;
                        cmd.Parameters.Add("?app_power_kva", MySqlDbType.VarChar).Value = app_pwr;
                        cmd.Parameters.Add("?act_power_kw", MySqlDbType.VarChar).Value = act_pwr;
                        cmd.Parameters.Add("?cum_kwh", MySqlDbType.VarChar).Value = cum_kwh;
                        cmd.Parameters.Add("?cum_temper_count", MySqlDbType.VarChar).Value = temper_count;
                        cmd.Parameters.Add("?billing_period_count", MySqlDbType.VarChar).Value = bill_per_count;
                        cmd.ExecuteNonQuery();
                    }
                    cn.Close();
                }
                catch (MySqlException ex)
                {
                    cn.Close();// MessageBox.Show("Error in adding mysql row. Error: " + ex.Message);
                }
            }
        }

        public void setDisconnect(string ip)
        {
            string query = "UPDATE `tb_connected_clients` SET mode=?mode WHERE ipv6 = '" + ip.Trim() + "'";
            using (MySqlConnection cn = new MySqlConnection(Program.Connection))
            {
                try
                {
                    cn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, cn))
                    {
                        cmd.Parameters.Add("?mode", MySqlDbType.VarChar).Value = "Disconnected";
                        cmd.ExecuteNonQuery();
                    }
                    cn.Close();
                }
                catch (MySqlException ex)
                {
                    cn.Close();// MessageBox.Show("Error in adding mysql row. Error: " + ex.Message);
                }
            }
        }

        public void disconnectAll()
        {
            string query = "UPDATE `tb_connected_clients` SET mode=?mode";
            using (MySqlConnection cn = new MySqlConnection(Program.Connection))
            {
                try
                {
                    cn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, cn))
                    {
                        cmd.Parameters.Add("?mode", MySqlDbType.VarChar).Value = "Disconnected";
                        cmd.ExecuteNonQuery();
                    }
                    cn.Close();
                }
                catch (MySqlException ex)
                {
                    cn.Close();// MessageBox.Show("Error in adding mysql row. Error: " + ex.Message);
                }
            }
        }

        public bool getiP(string ipaddress, string tableName)
        {
            bool _status = false;
            using (MySqlConnection con = new MySqlConnection(Program.Connection))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM `" + tableName + "` WHERE `ipv6`='" + ipaddress.Trim() + "'", con))
                {
                    con.Open();
                    MySqlDataReader DR = cmd.ExecuteReader();

                    if (DR.HasRows)
                    {
                        _status = true;
                    }
                    con.Close();
                }
            }
            return _status;
        }

        //public DataSet get_Meter_List()
        //{
        //    DataSet dataSet = new DataSet();
        //    List<string> aa = new List<string>();
        //    using (MySqlConnection con = new MySqlConnection(Program.Connection))
        //    {
        //        using (MySqlDataAdapter adapter = new MySqlDataAdapter("select * from tb_connected_clients", con))
        //        {
        //            con.Open();
        //            var reader = cmd.ExecuteReader();
        //            while (reader.Read())
        //            {
        //                string ipValue = reader.GetValue(1).ToString();
        //                ipList.Add(ipValue);
        //            }
        //            con.Close();
        //        }
        //    }
        //    return dataSet;
        //}

        public DataTable getiPList()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(Program.Connection))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT ipv6 FROM `tb_connected_clients`", con))
                {
                    con.Open();
                    MySqlDataAdapter adpt = new MySqlDataAdapter(cmd);
                    adpt.Fill(dt);
                    con.Close();
                }
            }
            return dt;
        }
    }
}
