using System;
using System.IO;
using System.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using static EDSCT.JsonHandler;
using System.Collections.ObjectModel;

namespace EDSCT {

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window {

        #region Variables and Objects

        public Dictionary<string, JObject> _shipDict = new Dictionary<string, JObject>();
        FileSystemWatcher fs;
        DateTime fsLastRaised;

        public string watchingFolder;
        public string LogTime = DateTime.Now.ToString("h:mm:ss tt");
        public string LogTimeNewLine = DateTime.Now.ToString("\nh:mm:ss tt");
        static string AppFolder = AppDomain.CurrentDomain.BaseDirectory;
        public static string DataFolder = AppFolder + "Data\\";
        string LogFile = AppFolder + "EDSCT.log";


        #endregion

        public MainWindow() {

            InitializeComponent();
            debug("Debug Enabled");
            if (!File.Exists(LogFile)) {
                File.AppendAllText(LogFile, LogTime + "- Application Started");
            } else {
                logger("- Application Started");
            }

            if (!Directory.Exists(DataFolder)) {
                logger("- Data Folder not found, creating it now");
                Directory.CreateDirectory("Data");
                logger("- Creating Sidewinder example JSON");
                createExampleJson();
            } else {
                logger("- Data Folder found");
                if (!File.Exists(DataFolder + "Sidewinder.json")) {
                    logger(" - Sidewinder example JSON missing, creating it now");
                    createExampleJson();
                }
                logger("- Reading files");
                addBoxItems();
            }

            watchingFolder = DataFolder; //the folder to be watched
            fs = new FileSystemWatcher(watchingFolder, "*.json*"); //initialize the filesystem watcher

            fs.EnableRaisingEvents = true;
            fs.IncludeSubdirectories = true;

            fs.Created += new FileSystemEventHandler(newfile); //This event will check for  new files added to the watching folder
            fs.Deleted += new FileSystemEventHandler(fs_Deleted); //this event will check for any deletion of file in the watching folder

        }

        public void addBoxItems() {

            string[] shipPaths = Directory.GetFiles(DataFolder, "*.json");

            foreach (string ship in shipPaths) {
                JObject JShip = JObject.Parse(File.ReadAllText(ship));
                _shipDict.Add(Path.GetFileNameWithoutExtension(ship), JShip);
                logger("- Found: " + JShip["ShipName"] + ".json, loading it.");

                string[] boxItems = new string[] { ship };

                foreach (var shipName in boxItems) {
                    shipBox1.Items.Add(Path.GetFileNameWithoutExtension(shipName));
                    shipBox2.Items.Add(Path.GetFileNameWithoutExtension(shipName));
                }
            }
        }

        private void debug(string text = null) {

            bool isDebug;
            string debugFile = AppFolder + "debug";

            if (File.Exists(debugFile)) {
                isDebug = true;
                try {
                    ship debugData = JsonConvert.DeserializeObject<ship>(File.ReadAllText(DataFolder + @"Sidewinder.json"));
                    testBox.Text = debugData.ShipName;
                } catch (DirectoryNotFoundException) {
                    Directory.CreateDirectory("Data");
                    createExampleJson();
                    ship debugData = JsonConvert.DeserializeObject<ship>(File.ReadAllText(DataFolder + @"Sidewinder.json"));
                    testBox.Text = debugData.ShipName;
                } catch (FileNotFoundException) {
                    if (!Directory.Exists(DataFolder)) {
                        Directory.CreateDirectory("Data");
                    }
                    createExampleJson();
                    ship debugData = JsonConvert.DeserializeObject<ship>(File.ReadAllText(DataFolder + @"Sidewinder.json"));
                    testBox.Text = debugData.ShipName;
                }
            } else {
                isDebug = false;
            }

            if (!isDebug) {
                testBox.Visibility = Visibility.Hidden;
            } else {
                testBox.Visibility = Visibility.Visible;
            }

            if (text != null) {
                if (isDebug) {
                    logger(text, isDebug);
                }
            }
        }

        public void logger(string Text, bool debug = false) {
            //Simple logging method for writting to a "log" to test and ensure things are working how I want them
            if (!debug) {
                File.AppendAllText(LogFile, LogTimeNewLine + " " + Text);
            } else {
                File.AppendAllText(LogFile, LogTimeNewLine + " - Debug: " + Text);
            }

        }

        private void shipBox1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) {

            try {
                JObject JShip = JObject.Parse(File.ReadAllText(DataFolder + shipBox1.SelectedItem.ToString() + ".json"));
                logger(" - Ship 1: " + (string)JShip["ShipName"]);
                Console.WriteLine("Ship 1: " + (string)JShip["ShipName"]);

                JArray sizes = (JArray)JShip["Dimensions"];
                string dimensions = (string)sizes[0] + "L, " + (string)sizes[1] + "W, " + (string)sizes[2] + "H";

                #region Add to DataGrid
                var list = new ObservableCollection<ship>();
                list.Add(new ship() { A = "Ship Name", B = (string)JShip["ShipName"] });
                list.Add(new ship() { A = "Manufacturer", B = (string)JShip["Manufacturer"] });
                list.Add(new ship() { A = "Dimensions", B = dimensions });
                list.Add(new ship() { A = "Landing Pad Size", B = (string)JShip["LandingPadSize"] });
                list.Add(new ship() { A = "Type", B = (string)JShip["Type"] });
                list.Add(new ship() { A = "Cost", B = (string)JShip["Cost"] });
                list.Add(new ship() { A = "Insurance", B = (string)JShip["Insurance"] });
                list.Add(new ship() { A = "Top Speed", B = (string)JShip["TopSpeed"] });
                list.Add(new ship() { A = "Max Speed", B = (string)JShip["MaxSpeed"] });
                list.Add(new ship() { A = "Boost Speed", B = (string)JShip["BoostSpeed"] });
                list.Add(new ship() { A = "Max Boost Speed", B = (string)JShip["MaxBoostSpeed"] });
                list.Add(new ship() { A = "Manoeuvrability", B = (string)JShip["Manoeuvrability"] });
                list.Add(new ship() { A = "Shields", B = (string)JShip["Shields"] });
                list.Add(new ship() { A = "Armor", B = (string)JShip["Armor"] });
                list.Add(new ship() { A = "Hull Mass", B = (string)JShip["HullMass"] });
                list.Add(new ship() { A = "Cargo Capacity", B = (string)JShip["CargoCapacity"] });
                list.Add(new ship() { A = "Max Cargo", B = (string)JShip["MaxCargo"] });
                list.Add(new ship() { A = "Fuel Capacity", B = (string)JShip["FuelCapacity"] });
                list.Add(new ship() { A = "Unladen Jump", B = (string)JShip["UnladenJump"] });
                list.Add(new ship() { A = "Max Jump", B = (string)JShip["MaxJump"] });
                list.Add(new ship() { A = "Mass Lock Factor", B = (string)JShip["MassLockFactor"] });
                list.Add(new ship() { A = "Seats", B = (string)JShip["Seats"] });
                list.Add(new ship() { A = "FighterBay", B = (string)JShip["FighterBay"] });
                list.Add(new ship() { A = "FighterCount", B = (string)JShip["FighterCount"] });
                list.Add(new ship() { A = "Utility", B = (string)JShip["Utility"] });
                list.Add(new ship() { A = "Small", B = (string)JShip["Small"] });
                list.Add(new ship() { A = "Medium", B = (string)JShip["Medium"] });
                list.Add(new ship() { A = "Large", B = (string)JShip["Large"] });
                list.Add(new ship() { A = "Huge", B = (string)JShip["Huge"] });
                list.Add(new ship() { A = "Size 1", B = (string)JShip["Size1"] });
                list.Add(new ship() { A = "Size 2", B = (string)JShip["Size2"] });
                list.Add(new ship() { A = "Size 3", B = (string)JShip["Size3"] });
                list.Add(new ship() { A = "Size 4", B = (string)JShip["Size4"] });
                list.Add(new ship() { A = "Size 5", B = (string)JShip["Size5"] });
                list.Add(new ship() { A = "Size 6", B = (string)JShip["Size6"] });
                list.Add(new ship() { A = "Size 7", B = (string)JShip["Size7"] });
                list.Add(new ship() { A = "Size 8", B = (string)JShip["Size8"] });
                list.Add(new ship() { A = "Military Slot 1", B = (string)JShip["Military_Slot1"] });
                list.Add(new ship() { A = "Military Slot 2", B = (string)JShip["Military_Slot2"] });
                list.Add(new ship() { A = "Military Slot 3", B = (string)JShip["Military_Slot3"] });
                this.dataGrid1.ItemsSource = list;
                #endregion

            } catch (FileNotFoundException) {
                Console.WriteLine(" Ship 1: File Not Found, defaulting to Sidewinder.json");
                logger("- Ship 1: File Not Found, defaulting to Sidewinder.json");
                File.ReadAllText(DataFolder + "Sidewinder.json");
            }
        }

        private void shipBox2_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) {
            try {
                JObject JShip = JObject.Parse(File.ReadAllText(DataFolder + shipBox2.SelectedItem.ToString() + ".json"));
                logger("Ship 2: " + (string)JShip["ShipName"], true);
                Console.WriteLine("Ship 2: " + (string)JShip["ShipName"]);

                JArray sizes = (JArray)JShip["Dimensions"];
                string dimensions = (string)sizes[0] + "L, " + (string)sizes[1] + "W, " + (string)sizes[2] + "H";

                #region Add to DataGrid
                var list = new ObservableCollection<ship>();
                list.Add(new ship() { A = "Ship Name", B = (string)JShip["ShipName"]});
                list.Add(new ship() { A = "Manufacturer", B = (string)JShip["Manufacturer"]});
                list.Add(new ship() { A = "Dimensions", B = dimensions });
                list.Add(new ship() { A = "Landing Pad Size", B = (string)JShip["LandingPadSize"]});
                list.Add(new ship() { A = "Type", B = (string)JShip["Type"]});
                list.Add(new ship() { A = "Cost", B = (string)JShip["Cost"]});
                list.Add(new ship() { A = "Insurance", B = (string)JShip["Insurance"]});
                list.Add(new ship() { A = "Top Speed", B = (string)JShip["TopSpeed"]});
                list.Add(new ship() { A = "Max Speed", B = (string)JShip["MaxSpeed"]});
                list.Add(new ship() { A = "Boost Speed", B = (string)JShip["BoostSpeed"]});
                list.Add(new ship() { A = "Max Boost Speed", B = (string)JShip["MaxBoostSpeed"]});
                list.Add(new ship() { A = "Manoeuvrability", B = (string)JShip["Manoeuvrability"]});
                list.Add(new ship() { A = "Shields", B = (string)JShip["Shields"]});
                list.Add(new ship() { A = "Armor", B = (string)JShip["Armor"]});
                list.Add(new ship() { A = "Hull Mass", B = (string)JShip["HullMass"]});
                list.Add(new ship() { A = "Cargo Capacity", B = (string)JShip["CargoCapacity"]});
                list.Add(new ship() { A = "Max Cargo", B = (string)JShip["MaxCargo"] });
                list.Add(new ship() { A = "Fuel Capacity", B = (string)JShip["FuelCapacity"]});
                list.Add(new ship() { A = "Unladen Jump", B = (string)JShip["UnladenJump"]});
                list.Add(new ship() { A = "Max Jump", B = (string)JShip["MaxJump"]});
                list.Add(new ship() { A = "Mass Lock Factor", B = (string)JShip["MassLockFactor"]});
                list.Add(new ship() { A = "Seats", B = (string)JShip["Seats"]});
                list.Add(new ship() { A = "FighterBay", B = (string)JShip["FighterBay"]});
                list.Add(new ship() { A = "FighterCount", B = (string)JShip["FighterCount"]});
                list.Add(new ship() { A = "Utility", B = (string)JShip["Utility"]});
                list.Add(new ship() { A = "Small", B = (string)JShip["Small"]});
                list.Add(new ship() { A = "Medium", B = (string)JShip["Medium"]});
                list.Add(new ship() { A = "Large", B = (string)JShip["Large"]});
                list.Add(new ship() { A = "Huge", B = (string)JShip["Huge"]});
                list.Add(new ship() { A = "Size 1", B = (string)JShip["Size1"]});
                list.Add(new ship() { A = "Size 2", B = (string)JShip["Size2"]});
                list.Add(new ship() { A = "Size 3", B = (string)JShip["Size3"]});
                list.Add(new ship() { A = "Size 4", B = (string)JShip["Size4"]});
                list.Add(new ship() { A = "Size 5", B = (string)JShip["Size5"]});
                list.Add(new ship() { A = "Size 6", B = (string)JShip["Size6"]});
                list.Add(new ship() { A = "Size 7", B = (string)JShip["Size7"]});
                list.Add(new ship() { A = "Size 8", B = (string)JShip["Size8"]});
                list.Add(new ship() { A = "Military Slot 1", B = (string)JShip["Military_Slot1"]});
                list.Add(new ship() { A = "Military Slot 2", B = (string)JShip["Military_Slot2"]});
                list.Add(new ship() { A = "Military Slot 3", B = (string)JShip["Military_Slot3"]});
                this.dataGrid2.ItemsSource = list;
                #endregion

            } catch (FileNotFoundException) {
                Console.WriteLine("Ship 2: File Not Found, defaulting to Sidewinder.json");
                logger(" - Ship 2: File Not Found, defaulting to Sidewinder.json");
                File.ReadAllText(DataFolder + "Sidewinder.json");
            }
        }

        #region File Added
        protected void newfile(object fscreated, FileSystemEventArgs Eventocc) {
            try {
                //to avoid same process to be repeated ,if the time between two events is more   than 1000 milli seconds only the second process will be considered 
                if (DateTime.Now.Subtract(fsLastRaised).TotalMilliseconds > 1000) { //Greater than 1 second

                    //to get the newly created file name and extension and also the name of the event occured in the watching folder
                    string CreatedFileName = Eventocc.Name;
                    FileInfo createdFile = new FileInfo(CreatedFileName);
                    string extension = createdFile.Extension;
                    string eventoccured = Eventocc.ChangeType.ToString();

                    fsLastRaised = DateTime.Now; //to note the time of event occured
                    System.Threading.Thread.Sleep(100); //Delay is given to the thread for avoiding same process to be repeated

                    this.Dispatcher.Invoke((Action)(() => { //dispatcher to add items to combobox
                        if (!shipBox1.Items.Contains(CreatedFileName)) {
                            shipBox1.Items.Add(Path.GetFileNameWithoutExtension(CreatedFileName));
                        }

                        if (!shipBox2.Items.Contains(CreatedFileName)) {
                            shipBox2.Items.Add(Path.GetFileNameWithoutExtension(CreatedFileName));
                        }

                    }));

                }
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString()); //Prints the error message in a box.
            } finally {
            }
        }
        #endregion

        #region File Deleted
        protected void fs_Deleted(object fschanged, FileSystemEventArgs changeEvent) {
            try {

                if (DateTime.Now.Subtract(fsLastRaised).TotalMilliseconds > 1000) {
                    fsLastRaised = DateTime.Now;
                    System.Threading.Thread.Sleep(100);

                    //Dispatcher to remove items from the combobox
                    this.Dispatcher.Invoke((Action)(() => {
                        shipBox1.Items.Remove(Path.GetFileNameWithoutExtension(changeEvent.Name));
                        shipBox2.Items.Remove(Path.GetFileNameWithoutExtension(changeEvent.Name));
                    }));
                }
            } catch (NullReferenceException) {
                try {
                    this.Dispatcher.Invoke((Action)(() => {
                        shipBox1.Items.Remove(Path.GetFileNameWithoutExtension(changeEvent.Name));
                        shipBox2.Items.Remove(Path.GetFileNameWithoutExtension(changeEvent.Name));
                    }));
                } catch (Exception ex) {
                    Console.WriteLine(ex.ToString());
                }

                if (!File.Exists(DataFolder + "Sidewinder.json")) {
                    this.Dispatcher.Invoke((Action)(() => {
                        if (shipBox1.Items.Contains("Sidewinder")) {
                            shipBox1.Items.Remove("Sidewinder");
                        }

                        if (shipBox2.Items.Contains("Sidewinder")) {
                            shipBox2.Items.Remove("Sidewinder");
                        }
                    }));

                    MessageBox.Show("Current file in use has been deleted and Sidewinder is missing, creating Sidewinder.json and selecting it. \nYou will most likely only see this error once!");
                    createExampleJson();
                } else {
                    MessageBox.Show("File in use has been deleted, selecting Sidewinder.");

                    this.Dispatcher.Invoke((Action)(() => {
                        if (!shipBox1.Items.Contains("Sidewinder")) {
                            shipBox1.Items.Add("Sidewinder");
                        }

                        if (!shipBox2.Items.Contains("Sidewinder")) {
                            shipBox2.Items.Add("Sidewinder");
                        }

                    }));
                }
            }

            this.Dispatcher.Invoke((Action)(() => {
                shipBox1.SelectedItem = "Sidewinder";
                shipBox2.SelectedItem = "Sidewinder";
            }));
        }

        #endregion

    }
}