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
    public partial class MainWindow : Window
    {

        public Dictionary<string, JObject> _shipDict = new Dictionary<string, JObject>();

        //variables
        public string LogTime = DateTime.Now.ToString("h:mm:ss tt");
        public string LogTimeNewLine = DateTime.Now.ToString("\nh:mm:ss tt");
        static string AppFolder = AppDomain.CurrentDomain.BaseDirectory;
        public static string DataFolder = AppFolder + "Data\\";
        string LogFile = AppFolder + "EDSCT.log";


        public MainWindow()
        {

            InitializeComponent();
            debug("Debug Enabled");
            if (!File.Exists(LogFile))
            {
                File.AppendAllText(LogFile, LogTime + "- Application Started");
            }
            else
            {
                logger("- Application Started");
            }

            if (!Directory.Exists(DataFolder))
            {
                logger("- Data Folder not found, creating it now");
                Directory.CreateDirectory("Data");
                logger("- Creating Sidewinder example JSON");
                createExampleJson();
            }
            else
            {
                logger("- Data Folder found");
                if (!File.Exists(DataFolder + "Sidewinder.json"))
                {
                    logger(" - Sidewinder example JSON missing, creating it now");
                    createExampleJson();
                }
                logger("- Reading files");
                addBoxItems();
            }

        }

        public void addBoxItems()
        {

            string[] shipPaths = Directory.GetFiles(DataFolder, "*.json");

            foreach (string ship in shipPaths)
            {
                JObject JShip = JObject.Parse(File.ReadAllText(ship));
                _shipDict.Add(Path.GetFileNameWithoutExtension(ship), JShip);
                logger("- Found: " + JShip["ShipName"] + ".json, loading it.");

                string[] boxItems = new string[] { ship };

                foreach (var shipName in boxItems)
                {
                    shipBox1.Items.Add(Path.GetFileNameWithoutExtension(shipName));
                    shipBox2.Items.Add(Path.GetFileNameWithoutExtension(shipName));
                }
            }
        }

        private void debug(string text = null)
        {

            bool isDebug;
            string debugFile = AppFolder + "debug";

            if (File.Exists(debugFile))
            {
                isDebug = true;
                try
                {
                    ship debugData = JsonConvert.DeserializeObject<ship>(File.ReadAllText(DataFolder + @"Sidewinder.json"));
                    testBox.Text = debugData.ShipName;
                }
                catch (DirectoryNotFoundException)
                {
                    Directory.CreateDirectory("Data");
                    createExampleJson();
                    ship debugData = JsonConvert.DeserializeObject<ship>(File.ReadAllText(DataFolder + @"Sidewinder.json"));
                    testBox.Text = debugData.ShipName;
                }
                catch (FileNotFoundException)
                {
                    if (!Directory.Exists(DataFolder))
                    {
                        Directory.CreateDirectory("Data");
                    }
                    createExampleJson();
                    ship debugData = JsonConvert.DeserializeObject<ship>(File.ReadAllText(DataFolder + @"Sidewinder.json"));
                    testBox.Text = debugData.ShipName;
                }
            }
            else
            {
                isDebug = false;
            }

            if (!isDebug)
            {
                testBox.Visibility = Visibility.Hidden;
            }
            else
            {
                testBox.Visibility = Visibility.Visible;
            }

            if (text != null)
            {
                if (isDebug)
                {
                    logger(text, isDebug);
                }
            }
        }

        public void logger(string Text, bool debug = false)
        {
            //Simple logging method for writting to a "log" to test and ensure things are working how I want them
            if (!debug)
            {
                File.AppendAllText(LogFile, LogTimeNewLine + " " + Text);
            }
            else
            {
                File.AppendAllText(LogFile, LogTimeNewLine + " - Debug: " + Text);
            }

        }

        private void shipBox1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                JObject JShip = JObject.Parse(File.ReadAllText(DataFolder + shipBox1.SelectedItem.ToString() + ".json"));
                logger(" - Ship 1: " + (string)JShip["ShipName"]);
                Console.WriteLine("Ship 1: " + (string)JShip["ShipName"]);

                JArray sizes = (JArray)JShip["Dimensions"];
                string dim = (string)sizes[0] + "L, " + (string)sizes[1] + "W, " + (string)sizes[2] + "H";

                //Grab data and color
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine(" Ship 1: File Not Found, defaulting to Sidewinder.json");
                logger("- Ship 1: File Not Found, defaulting to Sidewinder.json");
                File.ReadAllText(DataFolder + "Sidewinder.json");
            }
        }

        private void shipBox2_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                JObject JShip = JObject.Parse(File.ReadAllText(DataFolder + shipBox2.SelectedItem.ToString() + ".json"));
                logger("Ship 2: " + (string)JShip["ShipName"], true);
                Console.WriteLine("Ship 2: " + (string)JShip["ShipName"]);

                JArray sizes = (JArray)JShip["Dimensions"];
                string dim = (string)sizes[0] + "L, " + (string)sizes[1] + "W, " + (string)sizes[2] + "H";

                //Grab data and color
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Ship 2: File Not Found, defaulting to Sidewinder.json");
                logger(" - Ship 2: File Not Found, defaulting to Sidewinder.json");
                File.ReadAllText(DataFolder + "Sidewinder.json");
            }
        }
    }
}
