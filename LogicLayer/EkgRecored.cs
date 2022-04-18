using System;
using RaspberryPiNetCore.ADC;
using System.Threading;
namespace LogicLayer
{
    public class EkgRecored
    {

        private ADC1015 adc;

        /// <summary>
        /// hvor mange måling vi skal tage pr.min
        /// </summary>
        int SampleRate = 170; //hvor mange måling vi skal tage pr.min
        /// <summary>
        ///  10 sekunder
        /// </summary>
        int SamplePeriode = 10; // dvs 10 sekunder
        /// <summary>
        /// Lokal variabel der sættes til DateTime.Now i formatet ToString("dd MMMM yyyy HH: mm:ss").
        /// </summary>
        public string StartTiden;
        /// <summary>
        /// Array til de ekg data vi insamler fra patienten 
        /// </summary>
        private double[] EkgData; // de ekg data vi insamler fra patienten 
        /// <summary>
        /// en sample
        /// </summary>
        double sample = 0;

        private static int MalingRate = 10; // Dvs. at vi vil tage 100 målinger pr.sek
        /// <summary>
        /// Constructor til klassen. Initialiserer referencen til DataLayer.
        /// </summary>
        public EkgRecored(ADC1015 adc)
        {
            this.adc = adc;
        }
        public bool Startmaling()
        {
            
            bool ekgfardig = false;
            StartTiden = DateTime.Now.ToString("dd MMMM yyyy HH: mm:ss");
            EkgData = new double[SamplePeriode * SampleRate]; // dvs at den kører i 30 sek fordi den tager en måling hver 10 mSek dvs. 100 målinger pr. sek


            for (int i = 0; i < SamplePeriode * SampleRate; i++)
            {
                sample = (adc.ReadADC_SingleEnded(0) / 2048.0) * 6.144;
                //System.Diagnostics.Debug.WriteLine("input fra adc:    :  " + sample);

                EkgData[i] = sample;

                Thread.Sleep((1000 / SampleRate)); //der kører 170 målinger pr sek
            }

            ekgfardig = true;
            return ekgfardig;
        }


    }
}
