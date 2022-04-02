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

namespace ka
{
    class Communikation
    {
        private PadLCD padlcd;

        private SerLCD lcd;

        private TWIST tWIST;

        private Battery battery;

        private StartButton startButton;

        public Communikation()
        {
            lcd = new SerLCD();
            tWIST = new TWIST();
            padlcd = new PadLCD(lcd,tWIST);
            battery = new Battery();
            startButton = new StartButton();

            tWIST.setCount(0);


        }


        public void comun()
        {
            bool ja = true;

            while (ja == true)
            {
                if (startButton.ButtonIPressed() == true && battery.GetVoltage() > 20)
                {
                    //if (battery.GetVoltage() > 20)
                    //{
                        lcd.lcdClear();
                        lcd.lcdGotoXY(0, 0);
                        lcd.lcdPrint("ønsker du at oplyse cpr-nummer");

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
                                    padlcd.MaleigenYesNo();
                                    Måleigen = padlcd.MaleigenYesNo();


                                }
                            }
                         

                        }
                        else
                        {
                            padlcd.CPRnummber = "9999990000"; //her skal man tilføje til databasen med binde strege måske
                            lcd.lcdClear();
                            lcd.lcdGotoXY(0, 0);
                            lcd.lcdPrint("Maaling paabegyndt");
                            Thread.Sleep(3000);
                            lcd.lcdGotoXY(0, 1);
                            lcd.lcdPrint("Maaling afsluttet");
                        }
                    

                    //}
                    //else
                    //{
                    //    lcd.lcdClear();
                    //    lcd.lcdGotoXY(0, 0);
                    //    lcd.lcdPrint("batteriet er for lav, oplade batteriet");
                    //    Thread.Sleep(3000);
                 

                    //}
                }
                if (startButton.ButtonIPressed() == false)
                {
                    if (battery.GetVoltage() > 20)
                    {
                        lcd.lcdClear();
                        lcd.lcdGotoXY(0, 0);
                        lcd.lcdPrint("Systemet er klare tryk på start knappen");
                        lcd.lcdGotoXY(0, 1);
                        lcd.lcdPrint(Convert.ToString(battery.GetVoltage()) + "%");
                        Thread.Sleep(3000);
                    }
                    else
                    {
                        lcd.lcdClear();
                        lcd.lcdGotoXY(0, 0);
                        lcd.lcdPrint("batteriet er for lav, oplade batteriet");
                        Thread.Sleep(3000);
                    }

                }
        }

    }

    }
}
