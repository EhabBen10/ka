using RaspberryPiNetCore.ADC;
using RaspberryPiNetCore.JoySticks;
using RaspberryPiNetCore.LCD;
using RaspberryPiNetCore.TWIST;
using System;
using System.IO;
using System.Linq;
using System.Device.Gpio;
using System.Threading;
using ka;


namespace Raspberry_Pi_Dot_Net_Core_Console_Application3
{
    class Program
    {

        static void Main(string[] args)
        {

//<<<<<<< Updated upstream

//=======
//            SerLCD lcd = new SerLCD();
//            lcd.lcdClear();
//            lcd.lcdGotoXY(0, 0);
//            lcd.lcdPrint("Systemet er klare tryk på start knappen");

//            //Communikation com = new Communikation();

//            //com.comun();
//>>>>>>> Stashed changes


            PadLCD number = new PadLCD();


            number.con();




            //StartButton start = new StartButton();


            //int tal = 0;

            //lCD.lcdClear();
            //    lCD.lcdGotoXY(0, 0);
            //    lCD.lcdPrint("Hello world");




            //while (tal < 5)
            //{
            //    if (start.ButtonIPressed() == true)
            //    {
            //        lCD.lcdClear();
            //        lCD.lcdGotoXY(0, 0);
            //        lCD.lcdPrint(Convert.ToString(tal));


            //        while (start.ButtonIPressed()) ;
            //        tal++;

            //    }
            //}


            #region number pad
            //GpioController controller = new GpioController(PinNumberingScheme.Board); //dette er så jeg kan skrive pin istedet for GPIO
            //controller.OpenPin(26, PinMode.Output); // række 1
            //controller.OpenPin(24, PinMode.Output);
            //controller.OpenPin(23, PinMode.Output);
            //controller.OpenPin(22, PinMode.Output);

            //controller.OpenPin(21, PinMode.InputPullDown); //col 1
            //controller.OpenPin(19, PinMode.InputPullDown); // col 2
            //controller.OpenPin(10, PinMode.InputPullDown); // col 3
            //int tal = 0;
            //while (true) //så den kører til jeg stopper den
            //{
            //    // col 1

            //    controller.Write(26, PinValue.High);
            //    controller.Write(24, PinValue.Low);
            //    controller.Write(23, PinValue.Low);
            //    controller.Write(22, PinValue.Low);

            //    if (controller.Read(26) == true && controller.Read(21) == true && controller.Read(24) == false && controller.Read(23) == false && controller.Read(22) == false)
            //    {
            //            Console.WriteLine("1");
            //            Thread.Sleep(300);
            //            tal++;
            //    }
            //    if (controller.Read(26) == true && controller.Read(19) == true && controller.Read(24) == false && controller.Read(23) == false && controller.Read(22) == false)
            //    {
            //        Console.WriteLine("2");
            //        Thread.Sleep(300);
            //        tal++;

            //    }
            //    if (controller.Read(26) == true && controller.Read(10) == true && controller.Read(24) == false && controller.Read(23) == false && controller.Read(22) == false)
            //    {
            //        Console.WriteLine("3");
            //        Thread.Sleep(300);
            //        tal++;

            //    }


            //    controller.Write(26, PinValue.Low);
            //    controller.Write(24, PinValue.High);






            //    if (controller.Read(24) == true && controller.Read(21) == true && controller.Read(26) == false && controller.Read(23) == false && controller.Read(22) == false)
            //    {
            //        Console.WriteLine("4");
            //        Thread.Sleep(300);
            //        tal++;

            //    }
            //    if (controller.Read(24) == true && controller.Read(19) == true && controller.Read(26) == false && controller.Read(23) == false && controller.Read(22) == false)
            //    {
            //        Console.WriteLine("5");
            //        Thread.Sleep(300);
            //        tal++;

            //    }
            //    if (controller.Read(24) == true && controller.Read(10) == true && controller.Read(26) == false && controller.Read(23) == false && controller.Read(22) == false)
            //    {
            //        Console.WriteLine("6");
            //        Thread.Sleep(300);
            //        tal++;

            //    }


            //    controller.Write(26, PinValue.Low);
            //    controller.Write(24, PinValue.Low);
            //    controller.Write(23, PinValue.High);

            //    if (controller.Read(23) == true && controller.Read(21) == true && controller.Read(26) == false && controller.Read(24) == false && controller.Read(22) == false)
            //    {
            //        Console.WriteLine("7");
            //        Thread.Sleep(300);
            //        tal++;

            //    }
            //    if (controller.Read(23) == true && controller.Read(19) == true && controller.Read(26) == false && controller.Read(24) == false && controller.Read(22) == false)
            //    {
            //        Console.WriteLine("8");
            //        Thread.Sleep(300);
            //        tal++;

            //    }
            //    if (controller.Read(23) == true && controller.Read(10) == true && controller.Read(26) == false && controller.Read(24) == false && controller.Read(22) == false)
            //    {
            //        Console.WriteLine("9");
            //        Thread.Sleep(300);
            //        tal++;

            //    }

            //    controller.Write(26, PinValue.Low);
            //    controller.Write(24, PinValue.Low);
            //    controller.Write(23, PinValue.Low);
            //    controller.Write(22, PinValue.High);



            //    if (controller.Read(22) == true && controller.Read(21) == true && controller.Read(26) == false && controller.Read(24) == false && controller.Read(23) == false)
            //    {
            //        Console.WriteLine("*");
            //        Thread.Sleep(300);
            //        tal++;

            //    }
            //    if (controller.Read(22) == true && controller.Read(19) == true && controller.Read(26) == false && controller.Read(24) == false && controller.Read(23) == false)
            //    {
            //        Console.WriteLine("0");
            //        Thread.Sleep(300);
            //        tal++;

            //    }
            //    if (controller.Read(22) == true && controller.Read(10) == true && controller.Read(26) == false && controller.Read(24) == false && controller.Read(23) == false)
            //    {
            //        Console.WriteLine("#");
            //        Thread.Sleep(300);
            //        tal++;

            //    }

            #endregion numberpad


            #region Startbutton
            //Console.WriteLine("Hello World!");

            //StartButton start = new StartButton();


            //int tal = 0;

            //while (tal < 5)
            //{
            //    if (start.ButtonIPressed() == true)
            //    {
            //        Console.WriteLine("Hello world");
            //        Console.WriteLine("Hej med dig");

            //        while (start.ButtonIPressed()) ;
            //        tal++;

            //    }
            //else

            //{
            //    Console.WriteLine("Hello");
            //}

            #endregion
        }



        }

    }
    

