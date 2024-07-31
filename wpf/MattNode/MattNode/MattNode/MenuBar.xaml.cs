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

using System.Text.Json;
using System.IO;

namespace MattNode
{
    /// <summary>
    /// MenuBar.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MenuBar : UserControl
    {
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

        public void ExportProperties(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            // 저장할 파일 형식 필터 설정 (예: 텍스트 파일)
            saveFileDialog.Filter = "MattNode Properties file (*.MattNodeProperties)|*.MattNodeProperties";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); // 초기 디렉토리 설정

            if (saveFileDialog.ShowDialog() == true) // 대화 상자를 표시하고 사용자 입력을 받음
            {
                string text = "";
                
                text += "\n[ExportFiles]";

                for (int i = 0; i < ProjectProperty.ExportFiles.Count; i++)
                {
                    text += $"\n***" +
                            $"\nName:{ProjectProperty.ExportFiles[i].Name}" +
                            $"\nExtension:{ProjectProperty.ExportFiles[i].Extension}";
                }

                text += "\n***" +
                        "\n[NodeTypes]";

                for (int i = 0; i < ProjectProperty.NodeTypes.Count; i++)
                {
                    text += $"\n***" +
                            $"\nName:{ProjectProperty.NodeTypes[i].Name}" +
                            $"\nColor:{ProjectProperty.NodeTypes[i].Color.Color.R} {ProjectProperty.NodeTypes[i].Color.Color.G} {ProjectProperty.NodeTypes[i].Color.Color.B}";

                    for(int ii = 0; ii <  ProjectProperty.NodeTypes[i].ExportOption.Count; ii++)
                    {
                        text += $"\n>>ExportOption{ii}:" +
                                $"\n>>>>WriteType:{ProjectProperty.NodeTypes[i].ExportOption[ii].WriteType}" +
                                $"\n>>>>WriteText:{ProjectProperty.NodeTypes[i].ExportOption[ii].WriteText}" +
                                $"\n>>>>WritePrevNodes:{ProjectProperty.NodeTypes[i].ExportOption[ii].WritePrevNodes}" +
                                $"\n>>>>WriteNextNodes:{ProjectProperty.NodeTypes[i].ExportOption[ii].WriteNextNodes}";
                    }
                }

                text += $"\n***\n[EOF]";


                File.WriteAllText(saveFileDialog.FileName, text);
            }
        }
    }
}
