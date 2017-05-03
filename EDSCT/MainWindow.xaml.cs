using System;
using System.IO;
using System.Collections.Generic;
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
                if (!File.Exists(DataFolder + "Sidewinder.json"))
                {
                    logger(" - Sidewinder example JSON missing, creating it now");
                    JsonHandler.createExampleJson();
                }
                logger(" - Reading files");
                addBoxItems();

            }

        }

        public void addBoxItems()
        {

            
            var shipData = JsonConvert.DeserializeObject<dynamic>(JsonHandler.json);
            var shipName = shipData.ShipName;
            logger(shipName);
            //Supposed to go through each JSON located inside /Data/
            string[] boxItems = null; //Nulled while figuring out how to work with JSON
             for (int i = 0; i < boxItems.Length; i++)
             {
                 int[] numBoxItems = new int[10];
                 boxItems[i] = null; //Here it is supposed to pull the "ShipName" from the JSONs for adding to the Ship selection boxes, for now nulled out just to remove error while I figure it out
             }

            //shipBox1.ItemsSource = boxItems; //Not Ready for this yet

        }

        public void logger (string Text) //Simple logging method for writting to a "log" to test and ensure things are working how I want them
        {
            File.AppendAllText(LogFile, LogTimeNewLine + Text);
        }


    }
}
