using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTO_EKGmaaling
    {
        public string CPR { get; set; }

        public DateTime StartTid { get; set; }

        public List<double> EkgData { get; set; }

        public int SampleRate { get; set; }

        public DTO_EKGmaaling(string cpr, DateTime startTid, List<double> ekgdata, int sampleRate)
        {
            this.CPR = cpr;

            this.StartTid = startTid;
            EkgData = new List<double>();

            foreach (double ekg in ekgdata)
            {
                EkgData.Add(ekg);
            }

            this.SampleRate = sampleRate;
        }


    }
}
