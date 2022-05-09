﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaspberryPiNetCore.ADC;
using System.Threading;

namespace ka
{
    class Battery
    {
        public int maxVoltage = 5;
        private double voltage;
        private double voltageInput;
        
        private int resultateriProcent;
        private ADC1015 adc;
        private int antal;

        public Battery(ADC1015 adc)
        {
            this.adc = adc;
            adc.ReadADC_SingleEnded(1);
        }

        public double GetVoltage()
        {
            //resultateriProcent = 0;
            //double voltageInput = 0;
            //voltage = 0;

            //adc.ReadADC_SingleEnded(1);
            //adc.SamplingsRate = 1;
            //voltageInput = (adc.SINGLE_Measurement[1].Take() / 332.0);

            antal = adc.SINGLE_Measurement[1].Count;
            while(antal-- > 0)
            {
                adc.SINGLE_Measurement[1].Take();
            }
            voltageInput = adc.SINGLE_Measurement[1].Take();


            voltage = Convert.ToDouble(voltageInput/332.0) - 2.933; // den trækker jeg fra fordi de sidste i batteri er ikke gyldig
            resultateriProcent = Convert.ToInt32(voltage * 113.636364); //denne ganger jeg med så jeg kan får det i %
            //resultateriProcent = 5 * 6;
             
     
            return /*resultateriProcent*/ 22;
        }
    }
}
