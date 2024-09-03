using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Security.Policy;
using Microsoft.Win32;

namespace MattNode
{
    /// <summary>
    /// FirstWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class FirstWindow : Window
    {
        public static string Version = "1.0.12";
        public FirstWindow()
        {
            AssociateFileExtension();
            if (App.OpenFilePath == null)
            {
                InitializeComponent();
                RequestVersion();
            }
            else
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();

                MenuBar.OpenProject(App.OpenFilePath);

                this.Close();
            }
        }
        public static void AssociateFileExtension()
        {
            string progID = "MattNode";
            string extension = ".mattnode";
            string applicationPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MattNode.exe");
            string iconPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"spr_logo.ico");

            // 확장자와 ProgID 연결
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Classes\" + extension, "", progID);

            // ProgID와 프로그램 실행 경로 연결
            using (var key = Registry.CurrentUser.CreateSubKey(@"Software\Classes\" + progID + @"\shell\open\command"))
            {
                key.SetValue("", "\"" + applicationPath + "\" \"%1\"");
            }

            // 아이콘 설정
            using (var iconKey = Registry.CurrentUser.CreateSubKey(@"Software\Classes\" + progID + @"\DefaultIcon"))
            {
                iconKey.SetValue("", iconPath);
            }
        }
        private async void RequestVersion()
        {
            string url = "http://matthewstone218.dothome.co.kr/mattnode/version.php";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // GET 요청을 보냅니다.
                    HttpResponseMessage response = await client.GetAsync(url);

                    // 응답이 성공적인지 확인합니다.
                    if (response.IsSuccessStatusCode)
                    {
                        // 응답 본문을 문자열로 읽어옵니다.
                        string responseBody = await response.Content.ReadAsStringAsync();
                        if(responseBody != Version)
                        {
                            updateButton.IsEnabled = true;
                            updateButton.Content = "Update";
                            updateLabel.Visibility = Visibility.Visible;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void creditButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This product includes CefSharp. CefSharp and the Chromium Embedded Framework are owned by their respective copyright holders. All elements related to CefSharp included in this product are used in accordance with the original copyright owner's license terms.");
        }

        private void tutorialButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://www.notion.so/MattNode-English-Tutorial-958fdf74e0404d73a54edf12dc0ada78",
                UseShellExecute = true // 필수: UseShellExecute를 true로 설정하여 OS가 기본 브라우저를 사용하도록 합니다.
            });
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://github.com/MatthewStone218/MattNode",
                UseShellExecute = true // 필수: UseShellExecute를 true로 설정하여 OS가 기본 브라우저를 사용하도록 합니다.
            });
        }
    }
}
