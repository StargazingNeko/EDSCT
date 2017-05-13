using System;
using System.IO;
using System.Windows;
using Newtonsoft.Json;
using System.Collections.Generic;
using static EDSCT.JsonHandler;
using Newtonsoft.Json.Linq;
using System.Windows.Media;

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
            debug();
            defaults();
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
                if (!File.Exists(DataFolder + "Sidewinder.json"))
                {
                    logger(" - Sidewinder example JSON missing, creating it now");
                    createExampleJson();
                }
                logger(" - Reading files");
                addBoxItems();
            }

        }

        public void addBoxItems() {
            
            string[] shipPaths = Directory.GetFiles(DataFolder, "*.json");

            foreach (string ship in shipPaths)
            {
                JObject JShip = JObject.Parse(File.ReadAllText(ship));
                _shipDict.Add(Path.GetFileNameWithoutExtension(ship), JShip);
                logger(" - Found: " + JShip["ShipName"] + ".json, loading it.");

                string[] boxItems = new string[] { ship };

                foreach (var shipName in boxItems)
                {
                    shipBox1.Items.Add(Path.GetFileNameWithoutExtension(shipName));
                    shipBox2.Items.Add(Path.GetFileNameWithoutExtension(shipName));
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

        public void defaults()
        {
            //Box 1 default values
            HorizonsBool1.Text = "No";
            HorizonsBool1.Foreground = Brushes.Blue;
            shipHullValue1.Text = "0";
            shipHullValue1.Foreground = Brushes.Blue;
            shipShieldsValue1.Text = "0";
            shipShieldsValue1.Foreground = Brushes.Blue;

            //Box 2 default values
            HorizonsBool2.Text = "No";
            HorizonsBool2.Foreground = Brushes.Blue;
            shipHullValue2.Text = "0";
            shipHullValue2.Foreground = Brushes.Blue;
            shipShieldsValue2.Text = "0";
            shipShieldsValue2.Foreground = Brushes.Blue;
        }

        public void logger(string Text)
        {
            //Simple logging method for writting to a "log" to test and ensure things are working how I want them
            File.AppendAllText(LogFile, LogTimeNewLine + Text);
        }

        private void shipBox1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string[] shipPaths = Directory.GetFiles(DataFolder, "*.json");
            foreach (string ship in shipPaths)
            {
                JObject JShip = JObject.Parse(File.ReadAllText(ship));
                if (shipBox1.Text == (string)JShip["ShipName"])
                {
                    bool horizons = (bool)JShip["Horizons"]; //This is how you call data.

                    if (!horizons)
                    {
                        HorizonsBool1.Text = "Yes";
                        HorizonsBool1.Foreground = Brushes.Red;
                    }
                    else
                    {
                        HorizonsBool1.Text = "No";
                        HorizonsBool1.Foreground = Brushes.Lime;
                    }
                }
            }
        }

        private void shipBox2_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
