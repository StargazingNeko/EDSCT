using System;
using System.IO;
using System.Windows;
using Newtonsoft.Json;
using System.Collections.Generic;
using static EDSCT.JsonHandler;
using Newtonsoft.Json.Linq;
using System.Linq;

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
            defaults();
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

        public void defaults()
        {
            //Box 1 default values
            shipArmorValue1.Text = "0";
            shipArmorValue1.Foreground = System.Windows.Media.Brushes.Blue;
            shipShieldsValue1.Text = "0";
            shipShieldsValue1.Foreground = System.Windows.Media.Brushes.Blue;

            //Box 2 default values
            shipArmorValue2.Text = "0";
            shipArmorValue2.Foreground = System.Windows.Media.Brushes.Blue;
            shipShieldsValue2.Text = "0";
            shipShieldsValue2.Foreground = System.Windows.Media.Brushes.Blue;
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
                string item = (sender as System.Windows.Controls.ComboBox).SelectedItem as string;
                logger(" - Ship 1: " + (string)JShip["ShipName"]);
                Console.WriteLine("Ship 1: " + (string)JShip["ShipName"]);

                string[] dimensions = JShip.OfType<object>().Select(o => o.ToString()).ToArray();
                logger("Getting type of dimensions: " + dimensions.GetType());
                //Grab data and color
                shipArmorValue1.Text = (string)JShip["Armor"];
                shipArmorValue1.Foreground = System.Windows.Media.Brushes.Lime;
                shipShieldsValue1.Text = (string)JShip["Shields"];
                shipShieldsValue1.Foreground = System.Windows.Media.Brushes.Lime;
                shipManufacturer1.Text = (string)JShip["Manufacturer"];
                shipManufacturer1.Foreground = System.Windows.Media.Brushes.Lime;
                shipDimensions1.Text = JShip["Dimensions"].ToString();
                shipDimensions1.Foreground = System.Windows.Media.Brushes.Lime;
                shipLandingPadSize1.Text = (string)JShip["LandingPadSize"];
                shipLandingPadSize1.Foreground = System.Windows.Media.Brushes.Lime;
                shipCost1.Text = string.Format("{0:n0} CR", JShip["Cost"]);
                shipCost1.Foreground = System.Windows.Media.Brushes.Lime;
                shipInsurance1.Text = string.Format("{0:n0} CR", JShip["Insurance"]);
                shipInsurance1.Foreground = System.Windows.Media.Brushes.Lime;
                shipTopSpeed1.Text = (string)JShip["TopSpeed"];
                shipTopSpeed1.Foreground = System.Windows.Media.Brushes.Lime;
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
                string item = (sender as System.Windows.Controls.ComboBox).SelectedItem as string;
                logger("Ship 2: " + (string)JShip["ShipName"], true);
                Console.WriteLine("Ship 2: " + (string)JShip["ShipName"]);

                //Grab data and color
                shipArmorValue2.Text = (string)JShip["Armor"];
                shipArmorValue2.Foreground = System.Windows.Media.Brushes.Lime;
                shipShieldsValue2.Text = (string)JShip["Shields"];
                shipShieldsValue2.Foreground = System.Windows.Media.Brushes.Lime;
                shipManufacturer2.Text = (string)JShip["Manufacturer"];
                shipManufacturer2.Foreground = System.Windows.Media.Brushes.Lime;
                shipDimensions2.Text = JShip["Dimensions"].ToString();
                shipDimensions2.Foreground = System.Windows.Media.Brushes.Lime;
                shipLandingPadSize2.Text = (string)JShip["LandingPadSize"];
                shipLandingPadSize2.Foreground = System.Windows.Media.Brushes.Lime;
                shipCost2.Text = string.Format("{0:n0} CR", JShip["Cost"]);
                shipCost2.Foreground = System.Windows.Media.Brushes.Lime;
                shipInsurance2.Text = string.Format("{0:n0} CR", JShip["Insurance"]);
                shipInsurance2.Foreground = System.Windows.Media.Brushes.Lime;
                shipTopSpeed2.Text = (string)JShip["TopSpeed"];
                shipTopSpeed2.Foreground = System.Windows.Media.Brushes.Lime;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Ship 2: File Not Found, defaulting to Sidewinder.json");
                logger(" - Ship 2: File Not Found, defaulting to Sidewinder.json");
                File.ReadAllText(DataFolder + "Sidewinder.json");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Hide();
            CreateYourOwn form2 = new CreateYourOwn();
            form2.ShowDialog();
            form2 = null;
            Show();
        }
    }
}
