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

        private void LED_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Settings));
        }

    }
}
