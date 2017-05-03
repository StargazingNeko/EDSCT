using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;



namespace EDSCT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

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
            shipBox1.Text = "Select Ship";


            /*string[] boxItems;
            for (int i = 0; i<boxItems.Length; i++)
            {
                int[] numBoxItems = new int[10];
                boxItems[i] = JsonHandler.ship.;
            }


            shipBox1.ItemsSource = boxItems;*/
            
        }

        public void logger (string Text)
        {
            File.AppendAllText(LogFile, LogTimeNewLine + Text);
        }


    }
}
