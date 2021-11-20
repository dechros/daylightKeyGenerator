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
using Windows.Devices.Enumeration;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using System.Threading;
using Windows.Storage.Streams;
using DayLightKeyGenerator.Utility;
using System.Text.RegularExpressions;

namespace DayLightKeyGenerator.View
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        private LicenseHandler licenseHandler = new LicenseHandler();
        private string licenseKey;
        private const int machineIdSize = 8;
        public MainWindow()
        {
            InitializeComponent();
            licenseKey = "";
            licenseTextBox.Text = licenseKey;
        }

        private void machineIdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (machineIdTextBox.Text.Length == machineIdSize)
            {
                generateButton.IsEnabled = true;
            }
            else
            {
                generateButton.IsEnabled = false;
                licenseTextBox.Text = "";
            }
        }
        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            licenseKey = licenseHandler.CreateLicense(machineIdTextBox.Text);
            licenseTextBox.Text = licenseKey;
            bool encryptionTest = licenseHandler.DecodeLicense(licenseTextBox.Text, machineIdTextBox.Text);
            if (encryptionTest == true)
            {
                messageLabel.Content = "License Generated Successfuly";
            }
            else if(encryptionTest == false)
            {
                messageLabel.Content = "Error Generating License";
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && generateButton.IsEnabled == true)
            {
                GenerateButton_Click(sender, e);
            }
            else if (e.Key == Key.Escape)
            {
                Close();
            }
        }

        private void machineIdTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void machineIdTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
            base.OnPreviewKeyDown(e);
        }
    }
}
