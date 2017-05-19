using System.Windows;
using static EDSCT.JsonHandler;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace EDSCT {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CreateYourOwn : Window {

        //Variables
        public Dictionary<string, JObject> _shipDict = new Dictionary<string, JObject>();

        static string AppFolder = AppDomain.CurrentDomain.BaseDirectory;
        public static string DataFolder = AppFolder + "Data\\";
        
        #region JSON Variables
        static string ShipName = "";
        static string manufacturer = "";
        static string landingPadSize = "";
        static string type_of_ship = "";
        static string cost = "";
        static string Insurance = "";
        static string TopSpeed = "";
        static string MaxSpeed = "";
        static string BoostSpeed = "";
        static string MaxBoostSpeed = "";
        static string manoeuvrability_ = "";
        static string Shields = "";
        static string armor_ = "";
        static string hullMass = "";
        static string seats_ = "";
        static string fighterBay = "";
        static string fighterCount = "";
        static string cargoCapacity = "";
        static string maxCargo = "";
        static string fuelCapacity = "";
        static string unladenJump = "";
        static string maxJump = "";
        static string massLockFactor = "";
        static string utility_ = "";
        static string small_ = "";
        static string medium_ = "";
        static string large_ = "";
        static string huge_ = "";
        static string size1_ = "";
        static string size2_ = "";
        static string size3_ = "";
        static string size4_ = "";
        static string size5_ = "";
        static string size6_ = "";
        static string size7_ = "";
        static string size8_ = "";
        static string military_slot1 = "";
        static string military_slot2 = "";

        //Dimensions hotfix
        static string L = "";
        static string W = "";
        static string H = "";
        #endregion

        public CreateYourOwn() {
            InitializeComponent();
        }

       #region On change events.

        private void name_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            ShipName = name.Text;
        }

        private void Manufacturer1_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            manufacturer = Manufacturer1.Text;
        }

        private void TextBox_TextChanged_1(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            landingPadSize = Landing_Pad_Size.Text;
        }

        private void type_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            type_of_ship = type.Text;
        }

        private void ship_cost_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            cost = ship_cost.Text;
        }

        private void insurance_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            Insurance = insurance.Text;
        }

        private void top_speed_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            TopSpeed = top_speed.Text;
        }

        private void max_speed_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            MaxSpeed = max_speed.Text;
        }

        private void boost_speed_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            BoostSpeed = boost_speed.Text;
        }

        private void max_boost_speed_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            MaxBoostSpeed = max_boost_speed.Text;
        }

        private void manoeuvrability_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            manoeuvrability_ = manoeuvrability.Text;
        }

        private void shields_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            Shields = shields.Text;
        }

        private void armor_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            armor_ = armor.Text;
        }

        private void hull_mass_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            hullMass = hull_mass.Text;
        }

        private void cargo_capacity_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            cargoCapacity = cargo_capacity.Text;
        }

        private void max_cargo_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            maxCargo = max_cargo.Text;
        }

        private void fuel_capacity_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            fuelCapacity = fuel_capacity.Text;
        }

        private void uladenjump_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            unladenJump = uladenjump.Text;
        }

        private void max_jump_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            maxJump = max_jump.Text;
        }

        private void MassLockFactor_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            massLockFactor = mass_lock_factor.Text;
        }

        private void seats_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            seats_ = seats.Text;
        }

        private void fighter_bay_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            fighterBay = fighter_bay.Text;
        }

        private void fighter_count_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            fighterCount = fighter_count.Text;
        }

        private void utility_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            utility_ = utility.Text;
        }

        private void small_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            small_ = small.Text;
        }

        private void medium_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            medium_ = medium.Text;
        }

        private void large_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            large_ = large.Text;
        }

        private void huge_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            huge_ = huge.Text;
        }

        private void size1_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            size1_ = size1.Text;
        }

        private void size2_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            size2_ = size2.Text;
        }

        private void size3_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            size3_ = size3.Text;
        }

        private void size4_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            size4_ = size4.Text;
        }

        private void size5_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            size5_ = size5.Text;
        }

        private void size6_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            size6_ = size6.Text;
        }

        private void size7_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            size7_ = size7.Text;
        }

        private void size8_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            size8_ = size8.Text;
        }

        private void DimensionsLength_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            L = DimensionsLength.Text;
        }

        private void DimensionsHeight_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            H = DimensionsHeight.Text;
        }

        private void DimensionsWidth_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            W = DimensionsWidth.Text;
        }

        private void Military_slot1_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            military_slot1 = Military_slot1.Text;
        }

        private void Military_slot2_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            military_slot2 = Military_slot2.Text;
        }

        #endregion

        public static void createJson() {

            ship customShipCreation = new ship();

            customShipCreation.ShipName = ShipName;
            customShipCreation.Manufacturer = manufacturer;
            customShipCreation.Dimensions = new double[] { Convert.ToDouble(L), Convert.ToDouble(W), Convert.ToDouble(H) };
            customShipCreation.LandingPadSize = landingPadSize;
            customShipCreation.Type = type_of_ship;
            customShipCreation.Cost = Int32.Parse(cost);
            customShipCreation.Insurance = Int32.Parse(Insurance);
            customShipCreation.TopSpeed = Int32.Parse(TopSpeed);
            customShipCreation.MaxSpeed = Int32.Parse(MaxSpeed);
            customShipCreation.BoostSpeed = Int32.Parse(BoostSpeed);
            customShipCreation.MaxBoostSpeed = Int32.Parse(MaxBoostSpeed);
            customShipCreation.Manoeuvrability = Int32.Parse(manoeuvrability_);
            customShipCreation.Shields = Int32.Parse(Shields);
            customShipCreation.Armor = Int32.Parse(armor_);
            customShipCreation.HullMass = Int32.Parse(hullMass);
            customShipCreation.Seats = Int32.Parse(seats_);
            customShipCreation.FighterBay = bool.Parse(fighterBay);
            customShipCreation.FighterCount = Int32.Parse(fighterCount);
            customShipCreation.CargoCapacity = Int32.Parse(cargoCapacity);
            customShipCreation.MaxCargo = Int32.Parse(maxCargo);
            customShipCreation.FuelCapacity = Int32.Parse(fuelCapacity);
            customShipCreation.UnladenJump = Double.Parse(unladenJump);
            customShipCreation.MaxJump = Double.Parse(maxJump);
            customShipCreation.MassLockFactor = Int32.Parse(massLockFactor);
            customShipCreation.Utility = Int32.Parse(utility_);
            customShipCreation.Small = Int32.Parse(small_);
            customShipCreation.Medium = Int32.Parse(medium_);
            customShipCreation.Large = Int32.Parse(large_);
            customShipCreation.Huge = Int32.Parse(huge_);
            customShipCreation.Size1 = Int32.Parse(size1_);
            customShipCreation.Size2 = Int32.Parse(size2_);
            customShipCreation.Size3 = Int32.Parse(size3_);
            customShipCreation.Size4 = Int32.Parse(size4_);
            customShipCreation.Size5 = Int32.Parse(size5_);
            customShipCreation.Size6 = Int32.Parse(size6_);
            customShipCreation.Size7 = Int32.Parse(size7_);
            customShipCreation.Size8 = Int32.Parse(size8_);
            customShipCreation.Military_Slot1 = Int32.Parse(military_slot1);
            customShipCreation.Military_Slot2 = Int32.Parse(military_slot2);

            string json = JsonConvert.SerializeObject(customShipCreation, Formatting.Indented);
            try
            {
                File.WriteAllText(DataFolder + ShipName + ".json", json);
            }
            catch(DirectoryNotFoundException)
            {
                Directory.CreateDirectory(DataFolder);
                File.WriteAllText(DataFolder + ShipName + ".json", json);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            createJson();
        }

        private void LoadFile_Click(object sender, RoutedEventArgs e) {

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".json";
            dlg.Filter = "*.json|*.JSON";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true) { // Open document 

                string filename = dlg.FileName; //Grabs the path + file name

                JObject JShip = JObject.Parse(File.ReadAllText(filename)); //Loads and deserializes JSON from filename variable.
                JArray sizes = (JArray)JShip["Dimensions"]; //turns dimensions into an array that can be read from.

                #region Grab Data From Deserialized / Parsed JSON
                name.Text = (string)JShip["ShipName"];
                Manufacturer1.Text = (string)JShip["Manufacturer"];
                DimensionsLength.Text = (string)sizes[0];
                DimensionsWidth.Text = (string)sizes[1];
                DimensionsHeight.Text = (string)sizes[2];
                Landing_Pad_Size.Text = (string)JShip["LandingPadSize"];
                type.Text = (string)JShip["Type"]; //type of ship
                ship_cost.Text = (string)JShip["Cost"];
                insurance.Text = (string)JShip["Insurance"];
                top_speed.Text = (string)JShip["TopSpeed"];
                max_speed.Text = (string)JShip["MaxSpeed"];
                boost_speed.Text = (string)JShip["BoostSpeed"];
                max_boost_speed.Text = (string)JShip["MaxBoostSpeed"];
                manoeuvrability.Text = (string)JShip["Manoeuvrability"];
                shields.Text = (string)JShip["Shields"];
                armor.Text = (string)JShip["Armor"];
                hull_mass.Text = (string)JShip["HullMass"];
                cargo_capacity.Text = (string)JShip["CargoCapacity"];
                max_cargo.Text = (string)JShip["MaxCargo"];
                fuel_capacity.Text = (string)JShip["FuelCapacity"];
                uladenjump.Text = (string)JShip["UnladenJump"];
                max_jump.Text = (string)JShip["MaxJump"];
                mass_lock_factor.Text = (string)JShip["MassLockFactor"];
                seats.Text = (string)JShip["Seats"];
                fighter_bay.Text = (string)JShip["FighterBay"];
                fighter_count.Text = (string)JShip["FighterCount"];
                utility.Text = (string)JShip["Utility"];
                small.Text = (string)JShip["Small"];
                medium.Text = (string)JShip["Medium"];
                large.Text = (string)JShip["Large"];
                huge.Text = (string)JShip["Huge"];
                size1.Text = (string)JShip["Size1"];
                size2.Text = (string)JShip["Size2"];
                size3.Text = (string)JShip["Size3"];
                size4.Text = (string)JShip["Size4"];
                size5.Text = (string)JShip["Size5"];
                size6.Text = (string)JShip["Size6"];
                size7.Text = (string)JShip["Size7"];
                size8.Text = (string)JShip["Size8"];
                Military_slot1.Text = (string)JShip["Military_Slot1"];
                Military_slot2.Text = (string)JShip["Military_Slot2"];

                #endregion

            }
        }
    }
}
