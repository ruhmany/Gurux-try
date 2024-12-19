using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gurux_Testing
{
    class ObisList
    {
        public string obisName(string obCode,string type)
        {
            string name = "";
            switch (obCode)
            {
                case "0.0.96.1.0.255":
                    name = "Manufacturing Number";                                
                    break;
                case "0.0.96.1.2.255":
                    name = "Serial Number";                   
                    break;
                case "0.0.96.1.1.255":
                    name = "Manufacturer Name";                   
                    break;
                case "1.0.0.2.0.255":
                    name = "Firmware Version For Meter";                   
                    break;
                case "0.0.94.91.9.255":
                    name = "Meter Type";                   
                    break;
                case "0.0.94.91.11.255":
                    name = "Meter Category";                    
                    break;
                case "0.0.94.91.12.255":
                    name = "Meter Current Rating";                  
                    break;
                case "0.0.96.1.4.255":
                    name = "Meter Year Of Manufacture";                    
                    break;                
                case "1.0.12.7.0.255":
                    name = "Voltage";                   
                    break;//
                case "1.0.94.91.14.255":
                    name = "Current";
                    break;
                case "1.0.11.7.0.255":
                    name = "Phase Current";                   
                    break;
                case "1.0.91.7.0.255":
                    name = "Neutral Current";                    
                    break;                             
                case "1.0.9.7.0.255":
                    name = "Apparent Power-KVA";                   
                    break;
                case "1.0.1.7.0.255":
                    name = "Active Power-kW";                    
                    break;
                case "1.0.1.8.0.255":
                    name = "Cumulative Import Energy-kWh";    
                    break;
                case "1.0.9.8.0.255":
                    name = "Cumulative Import Energy-kVAh";
                    break;
                case "1.0.1.6.0.255":
                    if (type=="val")
                    {
                        name = "Maximum Demand-kW";                      
                    }
                    else if (type == "date")
                    {
                        name = "Maximum Demand-kW Date";                       
                    }                 
                    break;
                case "1.0.9.6.0.255":
                    if (type == "val")
                    {
                        name = "Maximum Demand-kva";
                    }
                    else if (type == "date")
                    {
                        name = "Maximum Demand-kva Date";
                    }                   
                    break;
                case "0.0.94.91.14.255":
                    name = "Cumulative Power ON Duration in Minutes";                  
                    break;
                case "0.0.94.91.0.255":
                    name = "Cumulative Tamper Count";                  
                    break;
                case "0.0.0.1.0.255":
                    name = "Cumulative Billing count";                   
                    break;
                case "0.0.96.2.0.255":
                    name = "Cumulative Programming Count";                               
                    break;
                case "1.0.2.8.0.255":
                    name = "Export Energy-kWh";                            
                    break;
                case "1.0.10.8.0.255":
                    name = "Export Energy-kVAh";                      
                    break;
                case "0.0.96.3.10.255":
                    name = "Disconnect control";                   
                    break;
                case "0.0.17.0.0.255":
                    name = "Load Limit Threshold (kW)";                   
                    break;                
                //case "1.0.13.0.0.255":
                //    name = "Sum Li Power factor Billing period avg";
                //    break;
                //case "1.0.1.8.1.255":
                //    name = "Sum Li Active power+ (QI+QIV)";
                //    break;
                //case "1.0.1.8.2.255":
                //    name = "Sum Li Active power+ (QI+QIV)";
                //    break;
                //case "1.0.1.8.3.255":
                //    name = "Sum Li Active power+ (QI+QIV)";
                //    break;
                //case "1.0.1.8.4.255":
                //    name = "Sum Li Active power+ (QI+QIV)";
                //    break;              
                //case "1.0.9.8.1.255":
                //    name = "Sum Li Apparent power+ (QI+QIV)";
                //    break;
                //case "1.0.9.8.2.255":
                //    name = "Sum Li Apparent power+ (QI+QIV)";
                //    break;
                //case "1.0.9.8.3.255":
                //    name = "Sum Li Apparent power+ (QI+QIV)";
                //    break;
                //case "1.0.9.8.4.255":
                //    name = "Sum Li Apparent power+ (QI+QIV)";
                //    break;
                //case "0.0.94.91.13.255":
                //    name = "Power ON duration(During Billing Period)";
                //    break;
                //case "1.0.1.29.0.255":
                //    name = "Sum Li Active power+ (QI+QIV)";
                //    break;
                //case "1.0.9.29.0.255":
                //    name = "Sum Li Apparent power+ (QI+QIV)";
                //    break;
                //case "1.0.2.29.0.255":
                //    name = "Sum Li Active power- (QII+QIII)";
                //    break;
                //case "1.0.10.29.0.255":
                //    name = "Sum Li Apparent power- (QII+QIII)";
                //    break;
                case "1.0.11.27.0.255":
                    name = "Any phase Current avg.";
                    break;                                         
                case "0.0.0.1.2.255":
                    name = "Billing Date";
                    break;

                case "0.0.1.0.0.255":
                    name = "Real Time Clock";
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
                case "0.0.94.91.10.255":
                    name = "Name Plate Details";
                    break;
                case "1.0.0.8.0.255":
                    name = "Demand Integration Period";
                    break;
                case "1.0.0.8.4.255":
                    name = "Profile Capture Period";
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
                    //case "":
                    //    name = "";
                    //    break;
                    //case "":
                    //    name = "";
                    //    break;
                    //case "":
                    //    name = "";
                    //    break;
                    //case "":
                    //    name = "";
                    //    break;
                    //case "":
                    //    name = "";
                    //    break;
                    //case "":
                    //    name = "";
                    //    break;
                    //case "":
                    //    name = "";
                    //    break;
                    //case "":
                    //    name = "";
                    //    break;
                    //case "":
                    //    name = "";
                    //    break;
                    //case "":
                    //    name = "";
                    //    break;
                    //case "":
                    //    name = "";
                    //    break;
                    //case "":
                    //    name = "";
                    //    break;
                    //case "":
                    //    name = "";
                    //    break;
            }
            return name;
        }                   

        public string temperName(string code)
        {
            string name = "";
            switch(code)
            {
                case "7":
                    name = "Over Voltage Occurred";
                    break;
                case "8":
                    name = "Over Voltage Restored";
                    break;
                case "9":
                    name = "Low Voltage Occurred";
                    break;
                case "10":
                    name = "Low Voltage Restored";
                    break;
                case "51":
                    name = "Phase CT Reverse Occurred";
                    break;
                case "52":
                    name = "Phase CT Reverse Restored";
                    break;
                case "67":
                    name = "Over Current Occurred";
                    break;
                case "68":
                    name = "Over Current Restored";
                    break;               
                case "69":
                    name = "Earth Loading Occurred";
                    break;
                case "70":
                    name = "Earth Loading Restored";
                    break;
                case "101":
                    name = "Power Failure Short Outage";
                    break;
                case "102":
                    name = "Power Failure Short Outage";
                    break;
                case "103":
                    name = "Power Failure Long Outage";
                    break;
                case "104":
                    name = "Power Failure Long Outage";
                    break;
                case "151":
                    name = "RTC Change Occurred";
                    break;
                case "152":
                    name = "DEMAND_INTEGRATION_PERIOD_CHANGE_OCCURRED";
                    break;
                case "153":
                    name = "PROFILE_CAPTURE_PERIOD_CHANGE_OCCURRED";
                    break;
                case "154":
                    name = "SINGLE_ACTION_BILLING_DATE_CHANGE_OCCURRED";
                    break;
                case "155":
                    name = "ACTIVITY_CALENDER_TIME_ZONE_CHANGE_OCCURRED";
                    break;
                case "157":
                    name = "New Firmware Activation";
                    break;
                case "158":
                    name = "Load Limit Set";
                    break;
                case "159":
                    name = "Load Limit Enabled";
                    break;
                case "160":
                    name = "Load Limit Disabled";
                    break;
                case "161":
                    name = "LLS_SECRET_MR_CHANGED";
                    break;
                case "162":
                    name = "HLS_SECRET_US_CHANGED";
                    break;
                case "163":
                    name = "HLS_SECRET_FW_CHANGED";
                    break;
                case "164":
                    name = "GLOBAL_KEY_CHANGED";
                    break;
                case "165":
                    name = "ESWF Changed";
                    break;
                case "166":
                    name = "MD Reset";
                    break;
                case "201":
                    name = "Magnetic Influence Occurred";
                    break;
                case "202":
                    name = "Magnetic Influence Restored";
                    break;
                case "203":
                    name = "Neutral Disturbance Occurred";
                    break;
                case "204":
                    name = "Neutral Disturbance Restored";
                    break;
                case "207":
                    name = "Single Wire Operation Occurred";
                    break;
                case "208":
                    name = "Single Wire Operation Restored";
                    break;
                case "209":
                    name = "Plugin Communication Module Removal Occurred";
                    break;
                case "210":
                    name = "Plugin Communication Module Removal Restored";
                    break;
                case "211":
                    name = "Config Change To Postpaid Mode";
                    break;
                case "212":
                    name = "Config Change To Prepaid Mode";
                    break;
                case "213":
                    name = "Config Change To FWD Only Mode";
                    break;
                case "214":
                    name = "Config Change To IMP Exp Mode";
                    break;
                case "215":
                    name = "Overload Occurred";
                    break;
                case "216":
                    name = "Overload Restored";
                    break;
                case "217":
                    name = "Terminal Cover Open Occurred";
                    break;
                case "218":
                    name = "Terminal Cover Open Restored";
                    break;
                case "251":
                    name = "Cover Open Occurred";
                    break;
                case "252":
                    name = "Cover Open Restored";
                    break;
            }
            return name;
        }
    }
}
