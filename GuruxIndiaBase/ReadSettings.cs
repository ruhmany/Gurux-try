namespace Gurux_Testing
{
    public class ReadSettings
    {
        Function function = new Function();
        public string un_curr, ov_curr, T1, T2, un_volt, ov_volt, RElTimeout, CoverLockTimeoutDef, kwh_timeout, ov_amp_con_timeout, ov_amp_discon_timeout, RElDiscTimeout,
            CoverLockTimeout, pf_thhold1, pf_thhold2, version, energy_mode, control_feature, disp_autoTime, disp_manualsize, disp_energytime, TCoverLockTimeout, TCoverLockTimeoutDef, voltage_timeout;
        public ReadSettings(byte[] reply)
        {
            InitializeComponent();
            int index = 1;
            un_curr = (function.ByteToInt(reply, index, 2) / 100).ToString("D2"); index = index + 2;
            ov_curr = (function.ByteToInt(reply, index, 2) / 100).ToString("D2"); index = index + 2;
            T1 = function.ByteToInt(reply, index, 2).ToString("D2"); index = index + 2;
            T2 = function.ByteToInt(reply, index, 2).ToString("D2"); index = index + 2;
            un_volt = (function.ByteToInt(reply, index, 2) / 100).ToString("D2"); index = index + 2;
            ov_volt = (function.ByteToInt(reply, index, 2) / 100).ToString("D2"); index = index + 2;
            RElTimeout = function.ByteToInt(reply, index, 2).ToString("D2"); index = index + 2;
            CoverLockTimeoutDef = function.ByteToInt(reply, index, 4).ToString("D2"); index = index + 4;
            kwh_timeout = ((float)function.ByteToInt(reply, index, 4) / 1000).ToString("N2"); index = index + 4;
            ov_amp_con_timeout = function.ByteToInt(reply, index, 2).ToString("D2"); index = index + 2;
            ov_amp_discon_timeout = function.ByteToInt(reply, index, 2).ToString("D2"); index = index + 2;
            RElDiscTimeout = function.ByteToInt(reply, index, 2).ToString("D2"); index = index + 2;
            CoverLockTimeout = function.ByteToInt(reply, index, 4).ToString("D2"); index = index + 4;

            pf_thhold1 = function.ByteToInt(reply, index, 2).ToString("D2"); index = index + 2;
            // pf_thhold2 = "00";// function.ByteToInt(reply, index, 2).ToString("D2");
            //index = index + 2;
            version = ((float)function.ByteToInt(reply, index, 1) / 10).ToString("N1"); index = index + 1;
            if (reply[index] == 0x01)
            {
                energy_mode = "Enabled"; index = index + 1;
            }
            else
            {
                energy_mode = "Disabled"; index = index + 1;
            }
            control_feature = function.ByteToInt(reply, index, 1).ToString("D2"); index = index + 1;
            disp_autoTime = function.ByteToInt(reply, index, 1).ToString("D2"); index = index + 1;
            disp_manualsize = function.ByteToInt(reply, index, 1).ToString("D2"); index = index + 1;
            disp_energytime = function.ByteToInt(reply, index, 1).ToString("D2"); index = index + 1;
            TCoverLockTimeout = function.ByteToInt(reply, index, 4).ToString("D2"); index = index + 4;
            TCoverLockTimeoutDef = function.ByteToInt(reply, index, 4).ToString("D2"); index = index + 4;
            index = index + 15;
            voltage_timeout = function.ByteToInt(reply, index, 2).ToString("D2"); index = index + 2;
        }

        private void ReadSettings_Load(object sender, EventArgs e)
        {
            FillDataGridView();
        }

        private void FillDataGridView()
        {
            dgv_data.Rows.Clear();
            dgv_data.Rows.Add("Under Current", un_curr);
            dgv_data.Rows.Add("Over Current", ov_curr);
            dgv_data.Rows.Add("T1 Timeout", T1);
            dgv_data.Rows.Add("T2 Timeout", T2);
            dgv_data.Rows.Add("Under Voltage", un_volt);
            dgv_data.Rows.Add("Over Voltage", ov_volt);
            dgv_data.Rows.Add("Voltage Timeout", voltage_timeout);
            dgv_data.Rows.Add("Relay Timeout", RElTimeout);
            dgv_data.Rows.Add("Cover Open Lock Default Timeout", CoverLockTimeoutDef);
            dgv_data.Rows.Add("Energy(kwh) Timeout", kwh_timeout);
            dgv_data.Rows.Add("Over Amp Connection Timeout", ov_amp_con_timeout);
            dgv_data.Rows.Add("Over Amp Dis-Connection Timeout", ov_amp_discon_timeout);
            dgv_data.Rows.Add("Relay Disconnection Timeout", RElDiscTimeout);
            //dgv_data.Rows.Add("Cover Lock Timeout", CoverLockTimeout);
            dgv_data.Rows.Add("Power Fail Threshold Time", pf_thhold1);
            dgv_data.Rows.Add("Version", version);
            dgv_data.Rows.Add("Energy Mode", energy_mode);
            //dgv_data.Rows.Add("Control Feature Enabled", control_feature);//ControleFetureEnabled; 
            dgv_data.Rows.Add("Display Auto Time", disp_autoTime);//DispConfig.DispAutoTime; 
            //dgv_data.Rows.Add("No. of parameters set", disp_manualsize);//DispConfig.DispManualSize; 
            dgv_data.Rows.Add("Display Energy Time", disp_energytime);//DispConfig.DispEnerTime; 
            //dgv_data.Rows.Add("Terminal Cover Lock Timeout", TCoverLockTimeout);
            dgv_data.Rows.Add("Terminal Cover Lock Default Timeout", TCoverLockTimeoutDef);
        }
    }
}
