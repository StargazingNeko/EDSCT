using System;
using System.IO;
using System.Windows;
using Newtonsoft.Json;
using System.Collections.Generic;
using static EDSCT.JsonHandler;
using Newtonsoft.Json.Linq;

namespace EDSCT {

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        public Dictionary<string, JObject> _shipDict = new Dictionary<string, JObject>();

        //variables
        public string LogTime = DateTime.Now.ToString("h:mm:ss tt");
        public string LogTimeNewLine = DateTime.Now.ToString("\nh:mm:ss tt");
        static string AppFolder = AppDomain.CurrentDomain.BaseDirectory;
        public static string DataFolder = AppFolder + "Data\\";
        string LogFile = AppFolder + "EDSCT.log";


        public MainWindow() {

            InitializeComponent();

            if (!File.Exists(LogFile)) {
                File.AppendAllText(LogFile, LogTime + " - Application Started");
            } else {
                logger(" - Application Started");
            }

            if (!Directory.Exists(DataFolder)) {
                logger(" - Data Folder not found, creating it now");
                Directory.CreateDirectory("Data");
                logger(" - Creating Sidewinder example JSON");
                createExampleJson();
            } else {
                logger(" - Data Folder found");
                if (!File.Exists(DataFolder + "0Sidewinder.json"))
                {
                    logger(" - Sidewinder example JSON missing, creating it now");
                    createExampleJson();
                }
                logger(" - Reading files");
                addBoxItems();
                colorCompare(shipHullValue1.Text, shipHullValue2.Text);

            }

        }



        public void colorCompare(dynamic shipDynData1, dynamic shipDynData2) {

            double shipVal1 = Double.Parse(shipDynData1);
            double shipVal2 = Double.Parse(shipDynData2);

            if (shipVal1 == shipVal2)  {
                
            } else if (shipVal1 < shipVal2) {

            } else if(shipVal1 > shipVal2){

            }
        }

        public void addBoxItems() {
            
            string[] shipPaths = Directory.GetFiles(DataFolder, "*.json");

            foreach (string ship in shipPaths) {

                JObject JShip = JObject.Parse(File.ReadAllText(ship));
                _shipDict.Add(Path.GetFileNameWithoutExtension(ship), JShip);
                logger(" - Found: " + _shipDict + ".json, loading it.");

                string[] boxItems = new string[] { ship };

                foreach (var shipName in boxItems) {
                    shipBox1.Items.Add(Path.GetFileNameWithoutExtension(shipName));
                    shipBox2.Items.Add(Path.GetFileNameWithoutExtension(shipName));
                }

                bool horizons = (bool)JShip["Horizons"]; //This is how you call data.

                if (horizons == false) {
                    HorizonsBool1.Text = "No";
                } else {
                    HorizonsBool1.Text = "Yes";
                    HorizonsBool1.Foreground = System.Windows.Media.Brushes.Red;
                }
            }
        }

        private void debug() {

            bool isDebug;
            string debugFile = AppFolder + "debug";

            if (File.Exists(debugFile)) {
                isDebug = true;
                ship debugData = JsonConvert.DeserializeObject<ship>(File.ReadAllText(DataFolder + @"Sidewinder.json"));
                testBox.Text = debugData.ShipName;
            } else {
                isDebug = false;
            }

            if (!isDebug) {
                testBox.Visibility = Visibility.Hidden;
            } else {
                testBox.Visibility = Visibility.Visible;
            }
        }
         
        public void logger (string Text) {
            //Simple logging method for writting to a "log" to test and ensure things are working how I want them
            File.AppendAllText(LogFile, LogTimeNewLine + Text);
        }
    }
}
