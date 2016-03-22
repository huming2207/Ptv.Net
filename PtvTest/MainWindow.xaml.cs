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
    }
}
