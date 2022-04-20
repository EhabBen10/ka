using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaspberryPiNetCore.ADC;

namespace ka
{
    class Battery
    {
        public int maxVoltage = 5;
        private double voltage;
        private ADC1015 adc;

        public Battery(ADC1015 adc)
        {
            this.adc = adc;
        }

        public double GetVoltage()
        {
            //double voltageInput = /*3480;*/adc.ReadADC_SingleEnded(1);
            //voltage = 2 * maxVoltage * (voltageInput / (Math.Pow(2, 12) / 100)) / 100;
            voltage = 5 * 6;
            return voltage;
        }
    }
}
