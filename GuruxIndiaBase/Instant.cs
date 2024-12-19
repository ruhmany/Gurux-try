using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gurux_Testing
{
    class Instant
    {
        public string Voltage { get; set; }
        public string Current { get; set; }
        public string NeutralCurrent { get; set; }
        public string PowerFactor { get; set; }
        public string Frequency { get; set; }
        public string App_Power { get; set; }
        public string Act_Power { get; set; }
        public string Cum_Kwh { get; set; }
        public string Cum_TemperCount { get; set; }
        public string Billing_PeriodCount { get; set; }
    }
}
