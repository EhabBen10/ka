﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Device.Gpio;
using System.Threading;

namespace ka
{
    class StartButton
    {
        public GpioController controller;
        private static int buttonPin = 8;
        public StartButton(GpioController controller)
        {
            //controller = new GpioController(PinNumberingScheme.Board);
            this.controller = controller;
            controller.OpenPin(buttonPin, PinMode.InputPullUp); //der bliver brugt en interen pullup
        }

        /// <summary>
        /// Om man trykker på knappen
        /// </summary> 
        /// <returns>true hvis man trykker på knappe, ellers false </returns>
        public bool ButtonIPressed()
        {
        

            if (controller.Read(buttonPin) == false)
            {
                return true;
           
            }
            else
            {
                return false;
               
            }


           

        }
    }
}
