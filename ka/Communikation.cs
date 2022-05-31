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
using LogicLayer;

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


        private EkgRecored ekgRecored;

        private ADC1015 adc;

        private double batteriStatusVoltage;
        public Communikation()
        {
            controller = new GpioController(PinNumberingScheme.Board);
            lcd = new SerLCD();
            tWIST = new TWIST();
            adc = new ADC1015(72, 512); //gain er 512 fordi vi måler over 4 V og ikke 6

            lcd.lcdSetBackLight(50, 50, 0);

            padlcd = new PadLCD(lcd,tWIST, controller);
            battery = new Battery(adc);
            startButton = new StartButton(controller);

            ekgRecored = new EkgRecored(adc);
            tWIST.setCount(0);


        }


        public void RUN()
        {
            int i = 0;
            int j = 0;
            double batterystaues = battery.GetVoltage();

            while (true)
            {
                Thread.Sleep(50);
                batteriStatusVoltage = battery.GetVoltage(); //det er så den måler batterie statues hvert gange vi looper

                if (startButton.ButtonIPressed() == true && batteriStatusVoltage > 20)
                {
                    i--;
                 
                    lcd.lcdClear();
                        lcd.lcdGotoXY(0, 0);
                        padlcd.CPRnummber = null;
                        lcd.lcdPrint("Oplys CPR?");

                        if(padlcd.OplyseYesNo() == true)
                    {
                       
                        bool Måleigen = true;
                            while (Måleigen == true)
                            {
                                padlcd.WriteCpr();
                                if (/*padlcd.*/VertifayCPR() == true)
                                {
                                    lcd.lcdClear();
                                    lcd.lcdGotoXY(0, 0);
                                    lcd.lcdPrint("Maaling paabegyndt");
                                //Thread.Sleep(3000);

                                while (ekgRecored.Startmaling() == false) { } //den ny
                                ekgRecored.CreateEkgDto(padlcd.CPRnummber);
                                //while ( == false) { }

                                lcd.lcdClear();
                                    lcd.lcdPrint("Maaling afsluttet");

                                lcd.lcdGotoXY(0, 1);
                                lcd.lcdPrint("Data er sendt");
                                lcd.lcdGotoXY(0, 2);
                                lcd.lcdPrint("mID = " + ekgRecored.CountId());

                                Thread.Sleep(3000);
                          

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
                                    lcd.lcdPrint("Ikke gyldigt CPR-nummer");
                                    Thread.Sleep(3000);
                                    lcd.lcdClear();
                                    lcd.lcdGotoXY(0, 0);
                                    lcd.lcdPrint("Vil du prove igen?");
                                  
                                    Måleigen = padlcd.MaleigenYesNo(); // måske skal jeg bare skrive den ene af dem



                                }
                            }
                         

                        }
                        else
                        {
                        lcd.lcdClear();
                        padlcd.CPRnummber = "9999990000"; //her skal man tilføje til databasen med binde strege måske
                        lcd.lcdPrint("CPR: " + padlcd.CPRnummber);
                        Thread.Sleep(3000);
                        lcd.lcdClear();
                            lcd.lcdGotoXY(0, 0);
                            lcd.lcdPrint("Maaling paabegyndt");

                        //Thread.Sleep(3000);
                        while (ekgRecored.Startmaling() == false) { } 
                        ekgRecored.CreateEkgDto(padlcd.CPRnummber);

                        lcd.lcdClear();
                            lcd.lcdPrint("Maaling afsluttet");
                        lcd.lcdGotoXY(0, 1);
                        lcd.lcdPrint("Data er sendt");
                        lcd.lcdGotoXY(0, 2);
                        lcd.lcdPrint("mID = " + ekgRecored.CountId());
                        Thread.Sleep(3000);
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
                 
                    if (i < 1 && batteriStatusVoltage == batterystaues && batteriStatusVoltage > 20)
                    {
                        lcd.lcdClear();
                        lcd.lcdGotoXY(0, 0);
                        lcd.lcdPrint("Systemet er klare");
                        lcd.lcdGotoXY(0, 1);
                        lcd.lcdPrint("tryk på start knappen");
                        lcd.lcdGotoXY(0, 2);
                        lcd.lcdPrint(Convert.ToDouble(batteriStatusVoltage) + "%");
                        i++;
                      
                  
                    } // denne kører første gang når batteristatus er over 20%
                   
                    if (j < 1 && batteriStatusVoltage == batterystaues && batteriStatusVoltage < 20) // denne kommer kun første gange hvis batterie statues er under 20
                    {
                        lcd.lcdClear();
                        lcd.lcdGotoXY(0, 0);
                        lcd.lcdPrint("batteriet er for");
                        lcd.lcdGotoXY(0, 1);
                        lcd.lcdPrint("lav, oplade batteriet");
                        lcd.lcdGotoXY(0, 2);
                        lcd.lcdPrint("Status" + Convert.ToDecimal(batteriStatusVoltage) + "%");
                        j++;

                    }// denne kører første gang når batteristatus er under 20%

                    //Thread.Sleep(50);
                    if (batteriStatusVoltage < batterystaues && batteriStatusVoltage > 20) // denne kommer hvert gange batterie statues ænder sig.
                    {
                        lcd.lcdClear();
                        lcd.lcdGotoXY(0, 0);
                        lcd.lcdPrint("Systemet er klare");
                        lcd.lcdGotoXY(0, 1);
                        lcd.lcdPrint("tryk på start knappen");
                        lcd.lcdGotoXY(0, 2);
                        lcd.lcdPrint(Convert.ToDouble(batteriStatusVoltage) + "%");
                        batterystaues = batteriStatusVoltage;
                     
                    } //hvis batteri´staus ændre sig og bliver større end før imens man og bliver mindre men større end 20%
                    if (batteriStatusVoltage < batterystaues && batteriStatusVoltage < 20) // denne kommer hvert gange batterie statues ænder sig.
                    {
                        lcd.lcdClear();
                        lcd.lcdGotoXY(0, 0);
                        lcd.lcdPrint("batteriet er for");
                        lcd.lcdGotoXY(0, 1);
                        lcd.lcdPrint("lav, oplade batteriet");
                        lcd.lcdGotoXY(0, 2);
                        lcd.lcdPrint("Status" + Convert.ToDecimal(batteriStatusVoltage) + "%");
                        batterystaues = batteriStatusVoltage;

                    } //omvendt
                    if (batteriStatusVoltage > batterystaues && batteriStatusVoltage > 20) // denne kommer hvert gange batterie statues ænder sig.
                    {
                        lcd.lcdClear();
                        lcd.lcdGotoXY(0, 0);
                        lcd.lcdPrint("Systemet er klare");
                        lcd.lcdGotoXY(0, 1);
                        lcd.lcdPrint("tryk på start knappen");
                        lcd.lcdGotoXY(0, 2);
                        lcd.lcdPrint(Convert.ToDouble(batteriStatusVoltage) + "%");
                        batterystaues = batteriStatusVoltage;

                    } //hvis batteri´staus ændre sig og bliver større  imens man og bliver mindre men større end 20%
                    if (batteriStatusVoltage > batterystaues && batteriStatusVoltage < 20) // denne kommer hvert gange batterie statues ænder sig.
                    {
                        lcd.lcdClear();
                        lcd.lcdGotoXY(0, 0);
                        lcd.lcdPrint("batteriet er for");
                        lcd.lcdGotoXY(0, 1);
                        lcd.lcdPrint("lav, oplade batteriet");
                        lcd.lcdGotoXY(0, 2);
                        lcd.lcdPrint("Status" + Convert.ToDecimal(batteriStatusVoltage) + "%");
                        batterystaues = batteriStatusVoltage;

                    }

                    if (startButton.ButtonIPressed() == true && batteriStatusVoltage <= 20) // denne kommer hvis man prøver at starte måling men batterie statues er under 20
                    {
                        lcd.lcdClear();
                        lcd.lcdGotoXY(0, 0);
                        lcd.lcdPrint("batteriet er for");
                        lcd.lcdGotoXY(0, 1);
                        lcd.lcdPrint("lav, oplade batteriet");
                        lcd.lcdGotoXY(0, 2);
                        lcd.lcdPrint("Status" + Convert.ToDecimal(batteriStatusVoltage) + "%");


                    } //hvis man trykker på start knappen imens batteri statues er under 20%

                }
        }

    }


        /// <summary>
        ///Tjekker og CPR-nummer er gyldig. 
        /// </summary>
        /// <returns>"true" hvis det er gyldig cpr nummer og "false" hvis den er ikke gyldig </returns>
        public bool VertifayCPR()
        {
            int[] integer = new int[padlcd.CPRnummber.Length]; // her laver en int array og sætter antallet til at være lige med bogstaverne i CPR-nr
            for (int i = 0; i < padlcd.CPRnummber.Length; i++)
            {
                for (int j = 0; j < integer.Length; j++)
                {
                    if (j == i)
                    {
                        integer[j] = padlcd.CPRnummber[i] - 48; //man er nøde til at trække 48 fra fordi 0 har værdien 48 når man laver det om
                    }
                }
            }
            //// Algoritme der kotrollerer om cifrene danner et gyldigt personnummer
            if ((4 * integer[0] + 3 * integer[1] + 2 * integer[2] + 7 * integer[3] + 6 * integer[4] + 5 * integer[5] + 4 * integer[6] + 3 * integer[7] + 2 * integer[8] + integer[9]) % 11 != 0)
                return false;
            else
                return true;
        }

    }
}
