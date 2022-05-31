using System;
using RaspberryPiNetCore.ADC;
using System.Threading;
using System.Collections.Generic;
using DTO;
using DataLayer;

namespace LogicLayer

{
    public class EkgRecored
    {

        private ADC1015 adc;

        /// <summary>
        /// Hvor mange målinger vi skal tage pr.sek
        /// </summary>
        int SampleRate = 170; //Hvor mange målinger vi skal tage pr.sek
        /// <summary>
        ///  10 sekunder
        /// </summary>
        int SamplePeriode = 10; // Dvs 10 sekunder
        /// <summary>
        /// Lokal variabel der sættes til DateTime.Now i formatet ToString("dd MMMM yyyy HH: mm:ss").
        /// </summary>
        public string StartTiden;
        /// <summary>
        /// List til de EKG data vi indsamler fra patienten 
        /// </summary>
        private List<double> EkgData; // De ekg data vi indsamler fra patienten 
        /// <summary>
        /// en sample
        /// </summary>
        double sample = 0;

        private static int MalingRate = 10; // Dvs. at vi vil tage 100 målinger pr.sek
       
        private int antal;


        private DTO_EKGmaaling eKGmaaling;

        private DatabaseCon con;

        /// <summary>
        /// Constructor til klassen. Initialiserer referencen til DataLayer.
        /// </summary>
        ///
        public EkgRecored(ADC1015 adc)
        {
            this.adc = adc;
            con = new DatabaseCon();
        }

        /// <summary>
        /// Starter målingen for EKG
        /// </summary>
        ///
        public bool Startmaling()
        {
            
            bool ekgfardig = false;
            StartTiden = DateTime.Now.ToString("dd MMMM yyyy HH: mm:ss");
            //EkgData = new double[SamplePeriode * SampleRate]; // Dvs at den kører i 30 sek fordi den tager en måling hver 10 mSek - altså 100 målinger pr. sek
            adc.ReadADC_SingleEnded(0);
            EkgData = new List<double>();
            antal = SamplePeriode * SampleRate;

            while(antal-- > 0)
            {
                EkgData.Add(adc.SINGLE_Measurement[0].Take()/332.0);   
            }

            //for (int i = 0; i < SamplePeriode * SampleRate; i++)
            //{
            //    sample = (adc.ReadADC_SingleEnded(0) / 2048.0) * 6.144;
            //    //System.Diagnostics.Debug.WriteLine("input fra adc:    :  " + sample);

            //    EkgData[i] = sample;
            //    //EkgData[i] = adc.SINGLE_Measurement[0].Take();
            //    Thread.Sleep((1000 / SampleRate)); //der kører 170 målinger pr sek
            //}
            adc.Stop_SingleEnded(0);
            ekgfardig = true;
            return ekgfardig;
        }
        /// <summary>
        /// Opretter en EKG DTO
        /// </summary>
       
        public void CreateEkgDto(string Cpr)
        {
           
            eKGmaaling = new DTO_EKGmaaling(Cpr, Convert.ToDateTime(StartTiden), EkgData, SampleRate);

            con.InsertToDataBase(eKGmaaling);
        }

        /// <summary>
        ///Finder det mID målingen har i Databasen
        /// </summary>
        /// <returns>mID fra databasen</returns>
        public int CountId()
        {
            return con.CountId();
        }


    }
}
