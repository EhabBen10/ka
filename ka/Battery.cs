using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ka
{
    class Battery
    {
        public int maxVoltage = 5;
        private double voltage;

        public double GetVoltage()
        {
            double voltageInput = 3480;//adc.readADC_SingleEnded(1);
            voltage = 2 * maxVoltage * (voltageInput / (Math.Pow(2, 12) / 100)) / 100;
            return 21;
        }
    }
}
