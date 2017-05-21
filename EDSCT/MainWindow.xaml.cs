using System;
using System.IO;
using System.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using static EDSCT.JsonHandler;

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

                //JArray sizes = (JArray)JShip["Dimensions"];
                //string dim = (string)sizes[0] + "L, " + (string)sizes[1] + "W, " + (string)sizes[2] + "H";

                testBox.Text = (string)JShip["ShipName"]; // check if values work in real time

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

                    this.Dispatcher.Invoke((Action)(() => {  //Dispatcher to remove items from the combobox
                        shipBox1.Items.Remove(Path.GetFileNameWithoutExtension(changeEvent.Name));
                        shipBox2.Items.Remove(Path.GetFileNameWithoutExtension(changeEvent.Name));

                    }));
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

    }
}