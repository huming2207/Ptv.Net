using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ptv.Timetable;
using Ptv;
using System.Security.Cryptography;

namespace PtvTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string ApiKey = string.Empty;
        public static string ApiId = string.Empty;
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void loginButton_Click(object sender, RoutedEventArgs e)
        {
            ApiKey = apiKeyTextBox.Text;
            ApiId = apiIdTextBox.Text;

            var client = new TimetableClient(
                ApiId,
                ApiKey,
                (input, key) =>
                {
                    // Unfortunately the APIs exposed to .NET PCLs does not include an implementation
                    // of the HMACSHA1 algorithm which the PTV API requires to generate signatures, so
                    // rather than take a dependency on another library, for now the API defines a
                    // delegate (TimetableClientHasher) which takes the key, and a sequence of bytes
                    // to be hashed which can then be passed into the underlying platforms APIs.
                    var provider = new HMACSHA1(key);
                    var hash = provider.ComputeHash(input);
                    return hash;
                }
            );

            var checkHealthResult = await client.GetHealthAsync();

            serverDatabaseCheckResultTextBlock.Text = "Server Database Check: " + checkHealthResult.IsClientClockOK.ToString();
            timeSyncCheckResultTextBlock.Text = "Time Sync Check: " + checkHealthResult.IsClientClockOK.ToString();
            memcacheCheckResultTextBlock.Text = "Server Memcache Check: " + checkHealthResult.IsMemcacheOK.ToString();
            securityChecksumResultTextBlock.Text = "Security checksum Check: " + checkHealthResult.IsSecurityTokenOK.ToString();

            if(checkHealthResult.IsOK)
            {
                conclusionTextBlock.Text = "Conclusion: All Good!";
            }
            else
            {
                conclusionTextBlock.Text = "Conclusion: Something wrong!";
            }
        }

        private async void getDisruptionButton_Click(object sender, RoutedEventArgs e)
        {
            var client = new TimetableClient(
                 ApiId,
                 ApiKey,
                (input, key) =>
                {
                    var provider = new HMACSHA1(key);
                    var hash = provider.ComputeHash(input);
                    return hash;
                }
            );

            // ComboBox in WPF is different from WinForm, so we need to use it in this way,
            // NOT simply using "ComboBox.SelectedItem.ToString()"
            ComboBoxItem selectedItem = (ComboBoxItem)disruptionModeSelectionComboBox.SelectedItem;
            var disruptionResult = await client.GetDisruptionAsync(selectedItem.Content.ToString());
            for(int i=0; i<disruptionResult.Length; i++)
            {
                disruptionResultTextBox.AppendText("\r\n\r\nTitle: " + disruptionResult[i].Title);
                disruptionResultTextBox.AppendText("\r\nDisruption ID: " + disruptionResult[i].DisruptionId.ToString());
                disruptionResultTextBox.AppendText("\r\nDescription: " + disruptionResult[i].Description);
                disruptionResultTextBox.AppendText("\r\nLink: " + disruptionResult[i].MessageURL);
                disruptionResultTextBox.AppendText("\r\nPublish time: " + disruptionResult[i].PublishTime.ToString());
            }
            
        }

        private async void searchButton_Click(object sender, RoutedEventArgs e)
        {
 
            var client = new TimetableClient(
                ApiId,
                ApiKey,
                (input, key) =>
            {
                var provider = new HMACSHA1(key);
                var hash = provider.ComputeHash(input);
                return hash;
            }
            );

            var itemResult = await client.SearchAsync(searchTextBox.Text);

            for (int i = 0; i < itemResult.Length; i++)
            {
                if(itemResult[i].GetType() == typeof(Ptv.Timetable.Line))
                {
                    Ptv.Timetable.Line result = new Ptv.Timetable.Line();
                    result = (Ptv.Timetable.Line)itemResult[i];
                    searchResultTextBox.AppendText("\r\n\r\nName: " + result.LineName);
                    searchResultTextBox.AppendText("\r\nLine ID: " + result.LineID);
                    searchResultTextBox.AppendText("\r\nLine Number: " + result.LineNumber);
                    searchResultTextBox.AppendText("\r\nType: " + result.TransportType);
                }
                else
                {
                    Stop result = new Stop();
                    result = (Stop)itemResult[i];
                    searchResultTextBox.AppendText("\r\n\r\nStop Location Name: " + result.LocationName);
                    searchResultTextBox.AppendText("\r\nStop ID: " + result.StopID);
                    searchResultTextBox.AppendText("\r\nStop Surburb: " + result.Suburb);
                    searchResultTextBox.AppendText("\r\nType: " + result.TransportType);
                }
            }
            
        }

        private async void searchLineByModeButton_Click(object sender, RoutedEventArgs e)
        {
            var client = new TimetableClient(
                ApiId,
                ApiKey,
                (input, key) =>
                {
                    var provider = new HMACSHA1(key);
                    var hash = provider.ComputeHash(input);
                    return hash;
                }
            );

            var itemResult = await client.SearchLineByModeAsync(searchLineByModeTextBox.Text, (TransportType)searchLineByModeResultComboBox.SelectedIndex);

            for (int i = 0; i < itemResult.Length; i++)
            {
                searchLineByModeResultTextBox.AppendText("\r\n\r\nName: " + itemResult[i].LineName);
                searchLineByModeResultTextBox.AppendText("\r\nLine ID: " + itemResult[i].LineID);
                searchLineByModeResultTextBox.AppendText("\r\nLine Number: " + itemResult[i].LineNumber);
                searchLineByModeResultTextBox.AppendText("\r\nType: " + itemResult[i].TransportType);
            }
        }
    }
}
