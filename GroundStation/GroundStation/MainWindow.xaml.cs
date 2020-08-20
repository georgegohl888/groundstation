using System;
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
//using System.Drawing;
using System.IO.Ports;
using Microsoft.Win32;
using System.IO;
using GMap.NET;
using GMap.NET.WindowsPresentation;
//using GMap.NET.WindowsPresentation.Markers;
using GMap.NET.MapProviders;
using System.ComponentModel;

namespace GroundStation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
       

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));   
        }

        StreamWriter Writer;
        long timeoffset; //what will be used as the difference between actual time and mission time
        SerialPort SP = new SerialPort("COM67", 57600);//com port conection

        private string _flightStatus = "Not Connected";
        public string FlightStatus
        {
            get { return _flightStatus; }
            set { _flightStatus = value; OnPropertyChanged("FlightStatus"); }
        }
        private float _battVolt;
        public float BattVolt
        {
            get { return _battVolt; }
            set { _battVolt = value; OnPropertyChanged("BattVolt"); }
        }

        public MainWindow()
        {
            InitializeComponent();
            SP.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
            this.DataContext = this;
            string[] AvailiblePorts = SerialPort.GetPortNames();
            PortsList.ItemsSource = AvailiblePorts;
            string reg = @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Avalon.Graphics\DisableHWAcceleration";
            Microsoft.Win32.Registry.SetValue(reg, "", 0);
            //FlightStatus = "Not Connected";

        }

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e) //We've recieve some new data
        {

            // get the data from the port
            string data = SP.ReadLine();
            //decode the incoming data
            try
            {
                switch (data.Substring(0, 1))
                {
                    case "R":
                        PFD.Roll = float.Parse(data.Substring(1)) / 10;
                        break;
                    case "P":
                        PFD.Pitch = float.Parse(data.Substring(1)) / 10;
                        break;
                    case "H":
                        PFD.Heading = float.Parse(data.Substring(1)) / 10;
                        break;
                    case "S":
                        PFD.Speed = float.Parse(data.Substring(1));
                        break;
                    case "A":
                        PFD.Altitude = float.Parse(data.Substring(1));
                        break;
                    case "r":
                        PFD.FD_R = float.Parse(data.Substring(1));
                        break;
                    case "p":
                        PFD.FD_P = float.Parse(data.Substring(1));
                        break;
                    case "M":
                        if(int.Parse(data.Substring(1)) == 1) 
                        { Dispatcher.Invoke(() => { StatusBox.Foreground = System.Windows.Media.Brushes.Black; FlightStatus = "Autolevel"; }); }
                        else if (int.Parse(data.Substring(1)) == 0) { this.Dispatcher.Invoke(() => { StatusBox.Foreground = System.Windows.Media.Brushes.Black; FlightStatus = "ManualFlight"; }); }
                        break;
                    case "V":
                        BattVolt = float.Parse(data.Substring(1));
                        break;

                }

            }
            catch { }
            if (Writer != null)
            {
                Writer.WriteLine((DateTime.Now.Ticks - timeoffset).ToString() + "|" + data);
            }
        }

        private void ConnectDisconnect_Click(object sender, RoutedEventArgs e) // connect button has been pressed
        {
            if (PortsList.SelectedValue != null && SP.IsOpen == false) // connect the port
            {
                SP.PortName = PortsList.SelectedValue.ToString();
                SP.Open();
                ConnectDisconnect.Content = "Disconnect";
                DebugLog.Inlines.Add("Connected to " + SP.PortName + "\n");
                SP.WriteLine("Hello");
            }
            else if (SP.IsOpen)//disconnect the port
            {
                SP.WriteLine("Bye");
                SP.Close();
                if (Writer != null)
                {
                    Writer.Close();
                }
                Writer = null;
                ConnectDisconnect.Content = "Connect";
                DebugLog.Inlines.Add("Diconnected " + SP.PortName + "\n");
                FlightStatus = "Not connected";
                StatusBox.Foreground = System.Windows.Media.Brushes.Red;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e) // setup log file
        {
            SaveFileDialog FileDialog = new SaveFileDialog();
            FileDialog.Filter = "BEANS Flight Log File (.txt) | *.txt";
            FileDialog.ShowDialog();
            if (FileDialog.FileName != null)
            {
                Writer = new StreamWriter(FileDialog.FileName);
                DebugLog.Inlines.Add("Created Log at " + FileDialog.FileName + "\n");
                timeoffset = DateTime.Now.Ticks;

            }
        }

        private void FD_Enable_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            PFD.FD_Enabled = true;
            
        }

        private void FD_Enable_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            PFD.FD_Enabled = false;
        }

        private void PFD_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void mapView_Loaded(object sender, RoutedEventArgs e)
        {
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;

            // choose your provider here
            mapView.MapProvider = GMap.NET.MapProviders.GoogleSatelliteMapProvider.Instance;
            mapView.MinZoom = 2;
            mapView.MaxZoom = 20;
            // whole world zoom
            mapView.Zoom = 2;
            // lets the map use the mousewheel to zoom
            mapView.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            // lets the user drag the map
            mapView.CanDragMap = true;
            // lets the user drag the map with the left mouse button
            mapView.DragButton = MouseButton.Left;

            mapView.Position = new GMap.NET.PointLatLng(53.7233995, -0.5737999);
            mapView.Zoom = 17;



            GMap.NET.WindowsPresentation.GMapMarker marker2 = new GMap.NET.WindowsPresentation.GMapMarker(new GMap.NET.PointLatLng(53.7233995, -0.5737999));

            String fp = AppDomain.CurrentDomain.BaseDirectory;
            fp = System.IO.Path.Combine(fp, @"..\..\Images\Aeroplane icon 1.png");

            BitmapImage b = new BitmapImage(new System.Uri(fp));



        marker2.Shape = new Image
            {
                Width = 50,
                Height = 50,
                Source = b
            };

               
            marker2.Offset = new System.Windows.Point(-25, -25);
            mapView.Markers.Add(marker2);

            // new System.Drawing.Bitmap(@"F:\documents\BEANS 2\BEANS-DEV\GroundStation\GroundStation\GroundStation\Images\Aeroplane icon 1.png")
        }

    }
    

}
