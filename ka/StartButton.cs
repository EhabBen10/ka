using System;
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
        public StartButton()
        {
            controller = new GpioController(PinNumberingScheme.Board);
            controller.OpenPin(buttonPin, PinMode.InputPullUp);
        }
    
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
