using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaspberryPiNetCore.ADC;
using RaspberryPiNetCore.JoySticks;
using RaspberryPiNetCore.LCD;
using RaspberryPiNetCore.TWIST;
using System.Device.Gpio;
using System.Threading;

namespace ka
{
    class PadLCD
    {
        private GpioController controller; //difinere Rpi pins
        private SerLCD lCD;
        private TWIST tWIST;
     
        public string CPRnummber { get; set; }
        public PadLCD(SerLCD lcd, TWIST tWIST, GpioController gpioController)
        {
            /* controller = new GpioController(PinNumberingScheme.Board);*/ //dette er så jeg kan skrive pin istedet for GPIO
            this.controller = gpioController;
            this.lCD = lcd;
            this.tWIST = tWIST;




            lCD.lcdCursor();
            lCD.lcdBlink();
        }

        /// <summary>
        ///Giver muglighed for at man tester fra keypadden og viser det på skærmen.
        ///Denne viser også "Indtast CPR-nummer
        /// </summary>
        /// <returns>1-9 samt med * og #</returns>
        public void WriteCpr()
        {
            //sender et signal
            controller.OpenPin(26, PinMode.Output); // række 1
            controller.OpenPin(24, PinMode.Output);// række 2
            controller.OpenPin(23, PinMode.Output); // række 3
            controller.OpenPin(22, PinMode.Output);// række 4

            //optage single 
            controller.OpenPin(21, PinMode.InputPullDown); //col 1
            controller.OpenPin(19, PinMode.InputPullDown); // col 2
            controller.OpenPin(10, PinMode.InputPullDown); // col 3

            int tal = 0; //hvor mange gange vi har trykket på keypaden

            lCD.lcdClear();
            lCD.lcdGotoXY(0, 0);
            lCD.lcdPrint("Indtast CPR nummer:");
            lCD.lcdGotoXY(0, 1);
            while (tal < 11) //så den kører til jeg stopper den
            {
                // col 1

                controller.Write(26, PinValue.High); // række 1 er tændt
                controller.Write(24, PinValue.Low); //række 2 er slukket
                controller.Write(23, PinValue.Low);//række 3 er slukket
                controller.Write(22, PinValue.Low);//række 4 er slukket

                if (controller.Read(26) == true && controller.Read(21) == true && controller.Read(24) == false && controller.Read(23) == false && controller.Read(22) == false)
                {
                    lCD.lcdGotoXY(Convert.ToByte(tal), 1);
                    lCD.lcdPrint("1");
                    Thread.Sleep(300);
                    CPRnummber += "1";
                    tal++;
                }
                if (controller.Read(26) == true && controller.Read(19) == true && controller.Read(24) == false && controller.Read(23) == false && controller.Read(22) == false)
                {
                    lCD.lcdGotoXY(Convert.ToByte(tal), 1);
                    lCD.lcdPrint("2");
                    Thread.Sleep(300);
                    CPRnummber += "2";
                    tal++;

                }
                if (controller.Read(26) == true && controller.Read(10) == true && controller.Read(24) == false && controller.Read(23) == false && controller.Read(22) == false)
                {
                    lCD.lcdGotoXY(Convert.ToByte(tal), 1);
                    lCD.lcdPrint("3");
                    Thread.Sleep(300);
                    CPRnummber += "3";
                    tal++;

                }


                controller.Write(26, PinValue.Low);
                controller.Write(24, PinValue.High);






                if (controller.Read(24) == true && controller.Read(21) == true && controller.Read(26) == false && controller.Read(23) == false && controller.Read(22) == false)
                {
                    lCD.lcdGotoXY(Convert.ToByte(tal), 1);
                    lCD.lcdPrint("4");
                    Thread.Sleep(300);
                    CPRnummber += "4";
                    tal++;

                }
                if (controller.Read(24) == true && controller.Read(19) == true && controller.Read(26) == false && controller.Read(23) == false && controller.Read(22) == false)
                {
                    lCD.lcdGotoXY(Convert.ToByte(tal), 1);
                    lCD.lcdPrint("5");
                    Thread.Sleep(300);
                    CPRnummber += "5";
                    tal++;

                }
                if (controller.Read(24) == true && controller.Read(10) == true && controller.Read(26) == false && controller.Read(23) == false && controller.Read(22) == false)
                {
                    lCD.lcdGotoXY(Convert.ToByte(tal), 1);
                    lCD.lcdPrint("6");
                    Thread.Sleep(300);
                    CPRnummber += "6";
                    tal++;

                }


                controller.Write(26, PinValue.Low);
                controller.Write(24, PinValue.Low);
                controller.Write(23, PinValue.High);

                if (controller.Read(23) == true && controller.Read(21) == true && controller.Read(26) == false && controller.Read(24) == false && controller.Read(22) == false)
                {
                    lCD.lcdGotoXY(Convert.ToByte(tal), 1);
                    lCD.lcdPrint("7");
                    Thread.Sleep(300);
                    CPRnummber += "7";
                    tal++;

                }
                if (controller.Read(23) == true && controller.Read(19) == true && controller.Read(26) == false && controller.Read(24) == false && controller.Read(22) == false)
                {
                    lCD.lcdGotoXY(Convert.ToByte(tal), 1);
                    lCD.lcdPrint("8");
                    Thread.Sleep(300);
                    CPRnummber += "8";
                    tal++;

                }
                if (controller.Read(23) == true && controller.Read(10) == true && controller.Read(26) == false && controller.Read(24) == false && controller.Read(22) == false)
                {
                    lCD.lcdGotoXY(Convert.ToByte(tal), 1);
                    lCD.lcdPrint("9");
                    Thread.Sleep(300);
                    CPRnummber += "9";
                    tal++;

                }

                controller.Write(26, PinValue.Low);
                controller.Write(24, PinValue.Low);
                controller.Write(23, PinValue.Low);
                controller.Write(22, PinValue.High);



                if (controller.Read(22) == true && controller.Read(21) == true && controller.Read(26) == false && controller.Read(24) == false && controller.Read(23) == false)
                {
                    lCD.lcdGotoXY(Convert.ToByte(tal), 1);
                    lCD.lcdPrint("*");
                    Thread.Sleep(300);
                    tal++;

                }
                if (controller.Read(22) == true && controller.Read(19) == true && controller.Read(26) == false && controller.Read(24) == false && controller.Read(23) == false)
                {
                    lCD.lcdGotoXY(Convert.ToByte(tal), 1);
                    lCD.lcdPrint("0");
                    Thread.Sleep(300);
                    CPRnummber += "0";
                    tal++;

                }
                if (controller.Read(22) == true && controller.Read(10) == true && controller.Read(26) == false && controller.Read(24) == false && controller.Read(23) == false)
                {
                    lCD.lcdGotoXY(Convert.ToByte(tal), 1);
                    lCD.lcdPrint("#");
                    Thread.Sleep(300);
                    tal++;

                }


                if (tal == 6)
                {
                    lCD.lcdGotoXY(Convert.ToByte(tal), 1);
                    lCD.lcdPrint("-");
                    tal++;
                }


            }
            controller.ClosePin(26);
            controller.ClosePin(24);
            controller.ClosePin(23);
            controller.ClosePin(22);
            controller.ClosePin(21);
            controller.ClosePin(19);
            controller.ClosePin(10);

        }



        /// <summary>
        ///Tjekker og CPR-nummer er gyldig. 
        /// </summary>
        /// <returns>"true" hvis det er gyldig cpr nummer og "false" hvis den er ikke gyldig </returns>
        public bool VertifayCPR()
        {
            int[] integer = new int[CPRnummber.Length]; // her laver en int array og sætter antallet til at være lige med bogstaverne i CPR-nr
            for (int i = 0; i < CPRnummber.Length; i++)
            {
                for (int j = 0; j < integer.Length; j++)
                {
                    if (j == i)
                    {
                        integer[j] = CPRnummber[i] - 48; //man er nøde til at trække 48 fra fordi 0 har værdien 48 når man laver det om
                    }
                }
            }
            //// Algoritme der kotrollerer om cifrene danner et gyldigt personnummer
            if ((4 * integer[0] + 3 * integer[1] + 2 * integer[2] + 7 * integer[3] + 6 * integer[4] + 5 * integer[5] + 4 * integer[6] + 3 * integer[7] + 2 * integer[8] + integer[9]) % 11 != 0)
                return false;
            else
                return true;
        }

       
       
        public string Gyldig()
        {
            if (VertifayCPR() == false)
            {
                return "Din cpr er ikke gyldig";
            }
            else
            {
                return "Din cpr er gyldig";
            }
        }


        /// <summary>
        /// Muligheden for at give brugeren et valg mellem henholdsvis ja eller nej. 
        /// Eksempelvis om de ønsker at fortage en måling igen.
        /// </summary>
        /// <returns>Returnerer true eller false, afhængig af værdien af encoders count funktion.</returns>
        public bool OplyseYesNo() //Ja nej funktion der fortæller om bruger vil noget eller ej
        {

            bool JaNej = true;
            tWIST.setCount(0);

            lCD.lcdGotoXY(0, 1);
            lCD.lcdPrint("1. Ja");

            lCD.lcdGotoXY(0, 2);
            lCD.lcdPrint("2. Nej");
         
            while (tWIST.isPressed() == false)
            {
                if (tWIST.getCount() % 2 == 0) //hvis det antalt af twist går op i 2 og man trykker så vil den retunere den true
                {
                    JaNej = true;
                    lCD.lcdGotoXY(0, 1);
                }
                else if (tWIST.getCount() % 2 == 1) // hvis den ikke er det så retunere den false altså nej
                {
                    JaNej = false;
                    lCD.lcdGotoXY(0, 2);
                }

            }

            return JaNej;

        }

        public bool MaleigenYesNo() //Ja nej funktion der fortæller om bruger vil noget eller ej
        {

            bool JaNej = true;
            tWIST.setCount(0);

            lCD.lcdGotoXY(0, 1);
            lCD.lcdPrint("1. Ja");

            lCD.lcdGotoXY(0, 2);
            lCD.lcdPrint("2. Nej");
     
            while (tWIST.isPressed() == false)
            {
                if (tWIST.getCount() % 2 == 0) //hvis det antalt af twist går op i 2 og man trykker så vil den retunere den true
                {
                    JaNej = true;
                    lCD.lcdGotoXY(0, 1);
                }
                else if (tWIST.getCount() % 2 == 1) // hvis den ikke er det så retunere den false altså nej
                {
                    JaNej = false;
                    lCD.lcdGotoXY(0, 2);
                }

            }

            return JaNej;

        }



    }
}
