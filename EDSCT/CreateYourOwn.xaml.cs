using System.Windows;
using static EDSCT.JsonHandler;
using Newtonsoft.Json.Linq;
using System.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Controls;

namespace EDSCT {
    /// <summary>
    /// Interaction logic for CreateYourOwn.xaml
    /// </summary>
    public partial class CreateYourOwn : Window {

        //Variables
        public Dictionary<string, JObject> _shipDict = new Dictionary<string, JObject>();

        public string LogTime = DateTime.Now.ToString("h:mm:ss tt");
        public string LogTimeNewLine = DateTime.Now.ToString("\nh:mm:ss tt");
        static string AppFolder = AppDomain.CurrentDomain.BaseDirectory;
        public static string DataFolder = AppFolder + "Data\\";
        string LogFile = AppFolder + "EDSCT.log";
        
        #region JSON Variables
        static bool Horizons = false;
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
        static string military = "";
        #endregion

        public CreateYourOwn() {
            InitializeComponent();
        }

        #region Bunch of shitty on change events.

        private void name_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            ShipName = name.Text;
        }

        private void Manufacturer1_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            manufacturer = Manufacturer1.Text;
        }

        private void TextBox_TextChanged_1(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            //landing pad size idk why it called it this...
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

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            //This is MassLockFactor idk why it's using this name
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

        private void military_input_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            military = military_input.Text;
        }

        #endregion

        public static void createJson() {

            ship customShipCreation = new ship();

            customShipCreation.ShipName = ShipName;
            customShipCreation.Manufacturer = manufacturer;
            customShipCreation.Dimensions = new double[] { 14.9, 21.3, 5.4 };
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
            customShipCreation.FighterBay = false;
            customShipCreation.FighterCount = Int32.Parse(fighterCount);
            customShipCreation.CargoCapacity = Int32.Parse(cargoCapacity);
            customShipCreation.MaxCargo = Int32.Parse(maxCargo);
            customShipCreation.FuelCapacity = Int32.Parse(fuelCapacity);
            customShipCreation.UnladenJump = Int32.Parse(unladenJump);
            customShipCreation.MaxJump = Int32.Parse(maxJump);
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
            customShipCreation.Military = Int32.Parse(military);

            string json = JsonConvert.SerializeObject(customShipCreation, Formatting.Indented);
            File.WriteAllText(DataFolder + ShipName + ".json", json);

        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            createJson();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            this.Close();

        }

    }
}
