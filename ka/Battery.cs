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
            while(antal-- > 0) // vi kører denne while fordi vi vil kun have en værdi og vi tager den alle sidste
            {
                adc.SINGLE_Measurement[1].Take();
            }
            voltageInput = adc.SINGLE_Measurement[1].Take(); // overveje at konvertere den til decimal 


            voltage = Math.Round(Convert.ToDouble((voltageInput / 2048.0) * 4.096),1) + 0.3 - 2.933; // den trækker jeg fra fordi de sidste i batteri er ikke gyldig, man pluser 0.3 fordi der er tab på 0.3 V pga lednerne
            resultateriProcent = Convert.ToInt32(voltage * 113.636364); //denne ganger jeg med så jeg kan får det i %
         
             
     
            return resultateriProcent; // det er så værdien i procent
        }
    }
}
