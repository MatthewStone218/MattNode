using Microsoft.Win32;
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

using Newtonsoft.Json;
using System.IO;

namespace MattNode
{
    /// <summary>
    /// MenuBar.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MenuBar : UserControl
    {
        public static string ?SavePath = null;
        public MenuBar()
        {
            InitializeComponent();
        }

        private void CopyDiscordName(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText("매튜돌#0269");
        }
        private void CopyGmailAddress(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText("matthewstone218@gmail.com");
        }
        private void CopyKakaoTalkLink(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText("https://open.kakao.com/o/s57uv0jg");
        }

        private void OpenPropertyMenu(object sender, RoutedEventArgs e)
        {
            PropertyMenu propertyMenu = new PropertyMenu();
            propertyMenu.Margin = new Thickness(0, 0, 0, 0); // 위치 설정
            propertyMenu.HorizontalAlignment = HorizontalAlignment.Left;
            propertyMenu.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetZIndex(propertyMenu, 1000);
            MainWindow._MainWindow.mainGrid.Children.Add(propertyMenu);
        }

        public static void SaveProject()
        {
            if(SavePath == null)
            {
                SaveProjectAs();
            }
            else
            {
                SaveProject(SavePath);
            }
        }
        public static void SaveProject(string path)
        {
            string text = "";

            text += "\n[ExportFiles]";

            for (int i = 0; i < ProjectProperty.ExportFiles.Count; i++)
            {
                text += $"\nName:{ProjectProperty.ExportFiles[i].Name}" +
                        $"\nExtension:{ProjectProperty.ExportFiles[i].Extension}" +
                        $"\n***";
            }

            text += "\n[/ExportFiles]" +
                    "\n[NodeTypes]";

            for (int i = 0; i < ProjectProperty.NodeTypes.Count; i++)
            {
                text += $"\nName:{ProjectProperty.NodeTypes[i].Name}" +
                        $"\nColor:{ProjectProperty.NodeTypes[i].Color.Color.R} {ProjectProperty.NodeTypes[i].Color.Color.G} {ProjectProperty.NodeTypes[i].Color.Color.B}";

                for (int ii = 0; ii < ProjectProperty.NodeTypes[i].ExportOption.Count; ii++)
                {
                    text += $"\n>>ExportOption{ii}:" +
                            $"\n>>>>WriteType:{ProjectProperty.NodeTypes[i].ExportOption[ii].WriteType}" +
                            $"\n>>>>WriteText:{ProjectProperty.NodeTypes[i].ExportOption[ii].WriteText}" +
                            $"\n>>>>WritePrevNodes:{ProjectProperty.NodeTypes[i].ExportOption[ii].WritePrevNodes}" +
                            $"\n>>>>WriteNextNodes:{ProjectProperty.NodeTypes[i].ExportOption[ii].WriteNextNodes}";
                }
                text += $"\n***";
            }

            text += $"\n[/NodeTypes]" +
                    $"\n[Nodes]";
                    for (int i = 0; i < Node.NodeList.Count; i++)
                    {
                        text += $"\nNodePos:{Node.NodeList[i].Margin.Left} {Node.NodeList[i].Margin.Top}" +
                                $"\nNodeSize:{Node.NodeList[i].Width} {Node.NodeList[i].Height}" +
                                $"\nNodeType:{Node.NodeList[i].typeComboBox.SelectedValue}" +
                                $"\nNodeText:{Node.NodeList[i].contentTextBox.Text}" +
                                $"\n***";
                    }

            text += $"\n[/Nodes]";


            File.WriteAllText(path, text);
        }

        public static void SaveProjectAs()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            // 저장할 파일 형식 필터 설정 (예: 텍스트 파일)
            saveFileDialog.Filter = "MattNode file (*.MattNode)|*.MattNode";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (saveFileDialog.ShowDialog() == true)
            {
                SaveProject(saveFileDialog.FileName);
                SavePath = saveFileDialog.FileName;
            }
        }

        private void SaveProjectAs(object sender, RoutedEventArgs e)
        {
            SaveProjectAs();
        }

        public static void OpenProject()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MattNode Properties file (*.MattNodeProperties)|*.MattNodeProperties";

            if (openFileDialog.ShowDialog() == true)
            {
                using (StreamReader reader = new StreamReader(openFileDialog.FileName))
                {
                    if (!File.Exists(openFileDialog.FileName))
                    {
                        MessageBox.Show("Failed to load property file.");
                        return;
                    }

                    string line;

                    ProjectProperty.ExportFiles = new List<ExportFile>();
                    ProjectProperty.NodeTypes = new List<NodeType>();
                    
                    for(int i = 0; i < Node.NodeList.Count; i++)
                    {
                        Node.NodeList[i].Dispose();
                        i--;
                    }
                    //
                }

                SavePath = openFileDialog.FileName;
            }
        }

        private static string GetStringAfter(string text, string substring)
        {
            return text.Substring(text.IndexOf(substring) + substring.Length, text.Length - (text.IndexOf(substring) + substring.Length));
        }

        private void SaveProject(object sender, RoutedEventArgs e)
        {
            SaveProject();
        }

        private void OpenProject(object sender, RoutedEventArgs e)
        {
            SaveAsk saveAsk = new SaveAsk();
            saveAsk.HorizontalAlignment = HorizontalAlignment.Left;
            saveAsk.VerticalAlignment = VerticalAlignment.Top;
            Canvas.SetTop(saveAsk, 0);
            Canvas.SetBottom(saveAsk, 0);
            Grid.SetZIndex(saveAsk, 4000);
            MainWindow._MainWindow.mainGrid.Children.Add(saveAsk);
        }
    }
}
