using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Gpio;
using Windows.UI.Xaml.Automation.Provider;
using System.Threading.Tasks;
using Windows.UI.Xaml.Automation.Peers;
using Windows.UI;
using TTPackStartup;
using System.Reflection;
using Windows.ApplicationModel.Core;

namespace TTPackStartup
{
    /// <summary>
    /// Pagina vuota che può essere usata autonomamente oppure per l'esplorazione all'interno di un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const int LED_PIN = 6;
        private GpioPin Led;

        public MainPage()
        {
            this.InitializeComponent();
            //InitGPIO();

            var controls = grid.Children.ToList();
            foreach (var ctrl in controls)
            {
                if (ctrl.GetType().Name == "Button")
                {
                    var button = (Button)ctrl;
                }
            }
        }

        private void InitGPIO()
        {
            var gpio = GpioController.GetDefault();

            if (gpio == null)
            {
                return;
            }

            Led = gpio.OpenPin(LED_PIN);

            Led.Write(GpioPinValue.High);
            Led.SetDriveMode(GpioPinDriveMode.Output);

        }

        private void Q1_ON_Click(object sender, RoutedEventArgs e)
        {
            if (!App.ModbusClient.Connected)
                return;
            Console.WriteLine("writeCoil");
            App.ModbusClient.WriteSingleCoil(8192, true);
            Console.WriteLine("writeCoil fatto");
        }

        private void Q1_OFF_Click(object sender, RoutedEventArgs e)
        {
            if (!App.ModbusClient.Connected) 
                return;
            Console.WriteLine("writeCoil");
            App.ModbusClient.WriteSingleCoil(8192, false);
            Console.WriteLine("writeCoil fatto");
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Settings));
        }

    }
}
