﻿using System;
using System.IO;
using Newtonsoft.Json;

namespace EDSCT
{


    public partial class JsonHandler
    {
        static string AppFolder = AppDomain.CurrentDomain.BaseDirectory;
        public static string DataFolder = AppFolder + "Data\\";



        public class ship
        {
            //Core ship info
            public bool Horizons { get; set; }
            public string ShipName { get; set; }
            public string Manufacturer { get; set; }
            public double[] Dimensions { get; set; }
            public string LandingPadSize { get; set; }
            public string Type { get; set; }
            public int Cost { get; set; }
            public int Insurance { get; set; }
            public int TopSpeed { get; set; }
            public int MaxSpeed { get; set; }
            public int BoostSpeed { get; set; }
            public int MaxBoostSpeed { get; set; }
            public int Manoeuvrability { get; set; }
            public int Shields { get; set; }
            public int Armor { get; set; }
            public int HullMass { get; set; }
            public int CargoCapacity { get; set; }
            public int MaxCargo { get; set; }
            public int FuelCapacity { get; set; }
            public double UnladenJump { get; set; }
            public double MaxJump { get; set; }
            public int MassLockFactor { get; set; }
            public int Seats { get; set; }
            public bool FighterBay { get; set; }
            public int FighterCount { get; set; }

            //Hardpoints
            public int Utility { get; set; }
            public int Small { get; set; }
            public int Medium { get; set; }
            public int Large { get; set; }
            public int Huge { get; set; }

            //Internal Sizes
            public int Size1 { get; set; }
            public int Size2 { get; set; }
            public int Size3 { get; set; }
            public int Size4 { get; set; }
            public int Size5 { get; set; }
            public int Size6 { get; set; }
            public int Military { get; set; }
        }



        public static void createExampleJson()
        {

            ship Sidewinder = new ship();

            Sidewinder.Horizons = false;
            Sidewinder.ShipName = "Sidewinder";
            Sidewinder.Manufacturer = "Faulcon DeLacy";
            Sidewinder.Dimensions = new double[] { 14.9, 21.3, 5.4 };
            Sidewinder.LandingPadSize = "Small";
            Sidewinder.Type = "Light Multipurpose";
            Sidewinder.Cost = 48000;
            Sidewinder.Insurance = 1600;
            Sidewinder.TopSpeed = 220;
            Sidewinder.MaxSpeed = 255;
            Sidewinder.BoostSpeed = 320;
            Sidewinder.MaxBoostSpeed = 371;
            Sidewinder.Manoeuvrability = 5;
            Sidewinder.Shields = 40;
            Sidewinder.Armor = 108;
            Sidewinder.HullMass = 25;
            Sidewinder.Seats = 1;
            Sidewinder.FighterBay = false;
            Sidewinder.FighterCount = 0;
            Sidewinder.CargoCapacity = 4;
            Sidewinder.MaxCargo = 12;
            Sidewinder.FuelCapacity = 2;
            Sidewinder.UnladenJump = 7.56;
            Sidewinder.MaxJump = 24.43;
            Sidewinder.MassLockFactor = 6;
            Sidewinder.Utility = 2;
            Sidewinder.Small = 2;
            Sidewinder.Medium = 0;
            Sidewinder.Large = 0;
            Sidewinder.Huge = 0;
            Sidewinder.Size1 = 2;
            Sidewinder.Size2 = 2;
            Sidewinder.Size3 = 0;
            Sidewinder.Size4 = 0;
            Sidewinder.Size5 = 0;
            Sidewinder.Size6 = 0;
            Sidewinder.Military = 0;

            string json = JsonConvert.SerializeObject(Sidewinder, Formatting.Indented);
            File.WriteAllText(DataFolder + "Sidewinder.json", json);
            
        }
 
    }
}
