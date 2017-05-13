﻿using System;
using System.IO;
using System.Windows;
using Newtonsoft.Json;

namespace EDSCT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        //variables
        public string LogTime = DateTime.Now.ToString("h:mm:ss tt");
        public string LogTimeNewLine = DateTime.Now.ToString("\nh:mm:ss tt");
        static string AppFolder = AppDomain.CurrentDomain.BaseDirectory;
        public static string DataFolder = AppFolder + "Data\\";
        string LogFile = AppFolder + "EDSCT.log";


        public MainWindow()
        {

            InitializeComponent();

            if (!File.Exists(LogFile))
            {
                File.AppendAllText(LogFile, LogTime + " - Application Started");
            }
            else
            {
                logger(" - Application Started");
            }

            if (!Directory.Exists(DataFolder))
            {
                logger(" - Data Folder not found, creating it now");
                Directory.CreateDirectory("Data");
                logger(" - Creating Sidewinder example JSON");
                JsonHandler.createExampleJson();
            }
            else
            {
                logger(" - Data Folder found");
                if (!File.Exists(DataFolder + "0Sidewinder.json"))
                {
                    logger(" - Sidewinder example JSON missing, creating it now");
                    JsonHandler.createExampleJson();
                }
                logger(" - Reading files");
                JsonHandler.ship sidwinderString = JsonConvert.DeserializeObject<JsonHandler.ship>(File.ReadAllText(DataFolder + @"Sidewinder.json"));
                testBox.Text = sidwinderString.ShipName;
                addBoxItems();
                colorCompare(shipHPValue1.Text, shipHPValue2.Text);

            }

        }



        public void colorCompare(dynamic shipDynData1, dynamic shipDynData2)
        {

            double shipVal1 = Double.Parse(shipDynData1);
            double shipVal2 = Double.Parse(shipDynData2);

            if (shipVal1 == shipVal2)
            {
                
            }
            else if (shipVal1 < shipVal2)
            {

            }
            else if(shipVal1 > shipVal2)
            {

            }
        }

        public void addBoxItems()
        {


            //JsonHandler.ship shipData = JsonConvert.DeserializeObject<JsonHandler.ship>(File.ReadAllText(DataFolder + @"Sidewinder.json")); - Leaving this here for now for reference

            foreach (string file in Directory.EnumerateFiles(DataFolder, "*.json"))
            {
                using (StreamReader r = new StreamReader(file))
                {
                   string json = r.ReadToEnd();
                   JsonHandler.ship shipData = JsonConvert.DeserializeObject<JsonHandler.ship>(json);
                   logger(" - Found: " + shipData.ShipName + ".json, loading it.");


                    string[] boxItems = new string[] { file };
                    for (int i = 0; i < boxItems.Length; i++)
                    {
                        int[] numBoxItems = new int[10];
                        boxItems[i] = shipData.ShipName;
                    }

                    shipBox1.ItemsSource = boxItems;
                    shipBox2.ItemsSource = boxItems;
                }
            }

        }

        public void logger (string Text) //Simple logging method for writting to a "log" to test and ensure things are working how I want them
        {
            File.AppendAllText(LogFile, LogTimeNewLine + Text);
        }

    }
}
