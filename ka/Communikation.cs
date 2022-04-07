using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaspberryPiNetCore.ADC;
using RaspberryPiNetCore.JoySticks;
using RaspberryPiNetCore.LCD;
using RaspberryPiNetCore.TWIST;
using System.Threading;
using System.Device.Gpio;

namespace ka
{
    class Communikation
    {
        private GpioController controller;

        private PadLCD padlcd;

        private SerLCD lcd;

        private TWIST tWIST;

        private Battery battery;

        private StartButton startButton;



        public Communikation()
        {
            controller = new GpioController(PinNumberingScheme.Board);
            lcd = new SerLCD();
            tWIST = new TWIST();
            padlcd = new PadLCD(lcd,tWIST, controller);
            battery = new Battery();
            startButton = new StartButton(controller);

            tWIST.setCount(0);


        }


        public void RUN()
        {
            int i = 0;
            double batterystaues = battery.GetVoltage();

            while (true)
            {
                if (startButton.ButtonIPressed() == true && battery.GetVoltage() > 20)
                {
                    i--;
                 
                    lcd.lcdClear();
                        lcd.lcdGotoXY(0, 0);
                        padlcd.CPRnummber = null;
                        lcd.lcdPrint("oplyse cpr?");

                        if(padlcd.OplyseYesNo() == true)
                    {
                       
                        bool Måleigen = true;
                            while (Måleigen == true)
                            {
                                padlcd.WriteCpr();
                                if (padlcd.VertifayCPR() == true)
                                {
                                    lcd.lcdClear();
                                    lcd.lcdGotoXY(0, 0);
                                    lcd.lcdPrint("Maaling paabegyndt");
                                    Thread.Sleep(3000);
                                    lcd.lcdClear();
                                    lcd.lcdPrint("Maaling afsluttet");
                                Thread.Sleep(2000);
                          

                                //her skal den bare måle
                                //ekgRecordRef.CreateEKGDTO(displayRef.EmployeeIdAsString, displayRef.SocSecNumberAsString); //Starter målingen); //Opretter en DTO

                                //while (ekgRecordRef.StartEkgRecord() == false) // Venter her indtil metoden returnerer true = måling færdig
                                //{ }
                                //lcd.lcdClear();
                                //lcd.lcdPrint("Maaling afsluttet");
                                //Thread.Sleep(3000);
                                Måleigen = false;

                                }
                                else
                                {
                                    lcd.lcdClear();
                                    lcd.lcdGotoXY(0, 0);
                                    lcd.lcdPrint("ikke gyldigt CPR-nummer");
                                    Thread.Sleep(3000);
                                    lcd.lcdClear();
                                    lcd.lcdGotoXY(0, 0);
                                    lcd.lcdPrint("Vil du bruge igen");
                                  
                                    Måleigen = padlcd.MaleigenYesNo(); // måske skal jeg bare skrive den ene af dem



                                }
                            }
                         

                        }
                        else
                        {
                        lcd.lcdClear();
                        padlcd.CPRnummber = "9999990000"; //her skal man tilføje til databasen med binde strege måske
                        lcd.lcdPrint("Cpr: " + padlcd.CPRnummber);
                        Thread.Sleep(3000);
                        lcd.lcdClear();
                            lcd.lcdGotoXY(0, 0);
                            lcd.lcdPrint("Maaling paabegyndt");
                            Thread.Sleep(3000);
                            lcd.lcdGotoXY(0, 1);
                            lcd.lcdPrint("Maaling afsluttet");
                        Thread.Sleep(2000);
                    }


                    //}
                    //else
                    //{
                    //    lcd.lcdClear();
                    //    lcd.lcdGotoXY(0, 0);
                    //    lcd.lcdPrint("batteriet er for lav, oplade batteriet");
                    //    Thread.Sleep(3000);


                    //}
                    while (startButton.ButtonIPressed()) ;
                }
               else
                {
                    //if (battery.GetVoltage() > 20)
                    //{
                    //    lcd.lcdClear();
                    //    lcd.lcdGotoXY(0, 0);
                    //    lcd.lcdPrint("Systemet er klare");
                    //    lcd.lcdGotoXY(0, 1);
                    //    lcd.lcdPrint("tryk på start knappen");
                    //    lcd.lcdGotoXY(0, 2);
                    //    lcd.lcdPrint(Convert.ToDouble(battery.GetVoltage()) + "%");


                    //    //Thread.Sleep(200);
                    //}
                    //else
                    //{
                    //    lcd.lcdPrint("batteriet er for");
                    //    lcd.lcdGotoXY(0, 1);
                    //    lcd.lcdPrint("lav, oplade batteriet");
                    //    Thread.Sleep(3000);
                    //}   

                    if (i < 1 && battery.GetVoltage() == batterystaues)
                    {
                        lcd.lcdClear();
                        lcd.lcdGotoXY(0, 0);
                        lcd.lcdPrint("Systemet er klare");
                        lcd.lcdGotoXY(0, 1);
                        lcd.lcdPrint("tryk på start knappen");
                        lcd.lcdGotoXY(0, 2);
                        lcd.lcdPrint(Convert.ToDouble(battery.GetVoltage()) + "%");
                        i++;
                  
                    }

                    if (battery.GetVoltage() < batterystaues/* && battery.GetVoltage() > 20*/)
                    {
                        lcd.lcdClear();
                        lcd.lcdGotoXY(0, 0);
                        lcd.lcdPrint("Systemet er klare");
                        lcd.lcdGotoXY(0, 1);
                        lcd.lcdPrint("tryk på start knappen");
                        lcd.lcdGotoXY(0, 2);
                        lcd.lcdPrint(Convert.ToDouble(battery.GetVoltage()) + "%");
                        batterystaues = battery.GetVoltage();
                     
                    }

                    if (startButton.ButtonIPressed() == true && battery.GetVoltage() <= 20)
                    {
                        lcd.lcdClear();
                        lcd.lcdGotoXY(0, 0);
                        lcd.lcdPrint("batteriet er for");
                        lcd.lcdGotoXY(0, 1);
                        lcd.lcdPrint("lav, oplade batteriet");
                        lcd.lcdGotoXY(0, 2);
                        lcd.lcdPrint("Status" + Convert.ToDouble(battery.GetVoltage()) + "%");
                        Thread.Sleep(3000);
                       
                    }

                }
        }

    }



      

    }
}
