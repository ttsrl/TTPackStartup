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

// Il modello di elemento Pagina vuota è documentato all'indirizzo https://go.microsoft.com/fwlink/?LinkId=234238

namespace TTPackStartup
{
    /// <summary>
    /// Pagina vuota che può essere usata autonomamente oppure per l'esplorazione all'interno di un frame.
    /// </summary>
    public sealed partial class NumericKeyboard : Page
    {
        public NumericKeyboard()
        {
            this.InitializeComponent();
        }

        private void n9_Click(object sender, RoutedEventArgs e)
        {
            textbox.Text += "9";
        }

        private void n8_Click(object sender, RoutedEventArgs e)
        {
            textbox.Text += "8";
        }

        private void n7_Click(object sender, RoutedEventArgs e)
        {
            textbox.Text += "7";
        }

        private void n6_Click(object sender, RoutedEventArgs e)
        {
            textbox.Text += "6";
        }

        private void n5_Click(object sender, RoutedEventArgs e)
        {
            textbox.Text += "5";
        }

        private void n4_Click(object sender, RoutedEventArgs e)
        {
            textbox.Text += "4";
        }

        private void n3_Click(object sender, RoutedEventArgs e)
        {
            textbox.Text += "3";
        }

        private void n2_Click(object sender, RoutedEventArgs e)
        {
            textbox.Text += "2";
        }

        private void n1_Click(object sender, RoutedEventArgs e)
        {
            textbox.Text += "1";
        }

        private void n0_Click(object sender, RoutedEventArgs e)
        {
            textbox.Text += "0";
        }

        private void point_Click(object sender, RoutedEventArgs e)
        {
            if(!textbox.Text.Contains("."))
                textbox.Text += ".";
        }

        private void sign_Click(object sender, RoutedEventArgs e)
        {
            if (textbox.Text.Contains("-"))
                textbox.Text = textbox.Text.Substring(1);
            else
                textbox.Text = "-" + textbox.Text;
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            textbox.Text = "";
        }
    }
}
