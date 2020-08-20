using System;
using System.Collections.Generic;
using System.IO.Ports;
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

namespace GroundStation
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        SerialPort SP = new SerialPort("COM67", 57600);

        private float _speed = 00;
        public float Speed
        {
            get { return _speed; }
            set
            {
                if (value != _speed)
                {
                    _speed = value;
                    Update();
                }
            }
        }//Speed Value

        private float _altitude = 000;
        public float Altitude
        {
            get { return _altitude; }
            set
            {
                if (value != _altitude)
                {
                    _altitude = value;
                    Update();
                }
            }
        } // Altitude Value

        private float _pitch = 00;
        public float Pitch
        {
            get { return _pitch; }
            set
            {
                if (value != _pitch)
                {
                    _pitch = value;
                    Update();
                }
            }
        }// Pitch Value
        private float _roll = 00;
        public float Roll
        {
            get { return _roll; }
            set
            {
                if (value != _roll)
                {
                    _roll = value;
                    Update();
                }
            }
        }//Roll value
        private float _heading = 360;
        public float Heading
        {
            get { return _heading; }
            set
            {
                if (value != _heading)
                {
                    _heading = value;
                    Update();
                }
            }
        }//Roll value
        private float _fd_P = 1500;
        public float FD_P
        {
            get { return _fd_P; }
            set
            {
                    _fd_P = value;
                    Update();
            }
        } // flight director pitch commanded
        private float _fd_R = 1500;
        public float FD_R
        {
            get { return _fd_R; }
            set
            {
                    _fd_R = value;
                    Update();
            }
        } // flight director Roll commanded
        private bool _fd_Enabled = true;
        public bool FD_Enabled
        {
            get { return _fd_Enabled; }
            set {
                if(value != _fd_Enabled)
                {
                    _fd_Enabled = value;
                    Update();
                }
            }
        }// is the flight director enabled?

        public UserControl1()
        {
            InitializeComponent();
            Update();
        }
        public void Update()
        {
            this.Dispatcher.Invoke(() =>
            {
                TranslateTransform TT = new TranslateTransform(0, Pitch * 7.2);
                TranslateTransform HT = new TranslateTransform(-Heading * 6, 0);

                RotateTransform RT = new RotateTransform(-Roll);
                RT.CenterX = Scale.Width / 2;
                RT.CenterY = Scale.Height / 2;

                TransformGroup TG = new TransformGroup();

                TG.Children.Add(TT);
                TG.Children.Add(RT);
        
                Scale.RenderTransform = TG;

                // the sky ground
                TranslateTransform TT2 = new TranslateTransform(0, Pitch * 7.2);

                RotateTransform RT2 = new RotateTransform(-Roll);
                RT2.CenterX = Image1.Width / 2;
                RT2.CenterY = Image1.Height / 2;

                TransformGroup TG2 = new TransformGroup();

                TG2.Children.Add(TT2);
                TG2.Children.Add(RT2);

                Image1.RenderTransform = TG2;

                RollIND.RenderTransform = RT;

                HeadingTape.RenderTransform = HT;


                if(FD_Enabled == false)
                {
                    FDI_P.Opacity = 0;
                    FDI_R.Opacity = 0;
                }
                else if (FD_Enabled)
                {
                    FDI_P.Opacity = 100;
                    FDI_R.Opacity = 100;
                    TranslateTransform FDHT = new TranslateTransform(0, ((FD_P - 1500) / 5) * -0.75);
                    TranslateTransform FDVT = new TranslateTransform(((FD_R - 1500) /5) * -0.75, 0);
                    FDI_P.RenderTransform = FDHT;
                    FDI_R.RenderTransform = FDVT;
                }

                TranslateTransform ST = new TranslateTransform(0, Speed * 19.5);
                SpeedTape.RenderTransform = ST;


                if (Speed < 100)
                {
                    SpeedActual.Text = Speed.ToString("00");
                }
                else
                {
                    SpeedActual.Text = Speed.ToString("Mx");
                }


                if (-20 < Altitude && Altitude < 300)
                {
                    TranslateTransform AT = new TranslateTransform(0, Altitude * 20);
                    AltitudeTape.RenderTransform = AT;
                }

                AltitudeActual.Text = Altitude.ToString("000");
            });

            
        }
        

    }
}
