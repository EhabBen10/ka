using System;
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

        /// <summary>
        /// Constructor til klassen. Initialiserer referencen til DataLayer.
        /// </summary>
        public Battery(ADC1015 adc)
        {
            this.adc = adc;
            adc.ReadADC_SingleEnded(1);
        }

        public double GetVoltage()
        {
            #region gammle kode
            //resultateriProcent = 0;
            //double voltageInput = 0;
            //voltage = 0;

            //adc.ReadADC_SingleEnded(1);
            //adc.SamplingsRate = 1;
            //voltageInput = (adc.SINGLE_Measurement[1].Take() / 332.0);
            #endregion

            antal = adc.SINGLE_Measurement[1].Count;
            while(antal-- > 0) // Vi kører denne while fordi vi kun vil have en værdi og vi tager den aller sidste
            {
                adc.SINGLE_Measurement[1].Take();
            }
            voltageInput = adc.SINGLE_Measurement[1].Take(); // overveje at konvertere den til decimal 


            voltage = Math.Round(Convert.ToDouble((voltageInput / 2048.0) * 4.096),1) + 0.3 - 2.933; // Den trækker vi fra fordi det sidste i batteri er ikke gyldig, man plusser 0.3 fordi der er et tab på 0.3 V pga. ledningerne
            resultateriProcent = Convert.ToInt32(voltage * 113.636364); //Denne ganger vi med så vi kan få det i %
         
             
     
            return resultateriProcent; // Det er så værdien i procent
        }
    }
}
