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
using static System.Net.Mime.MediaTypeNames;
using MattNode.Property;
using System.Windows.Threading;

namespace MattNode
{
    /// <summary>
    /// MenuBar.xaml에 대한 상호 작용 논리
    /// </summary>
    public class SaveData
    {
        public List<ExportFile> ExportFiles;
        public List<NodeType> NodeTypes;
        public List<NodeData> NodeDatas;
        public SaveData(List<ExportFile> exportFiles, List<NodeType> nodeTypes, List<NodeData> nodeDatas) 
        {
            ExportFiles = exportFiles;
            NodeTypes = nodeTypes;
            NodeDatas = nodeDatas;
        }
    }

    public class NodeData
    {
        public double Top,Left,Width,Height;
        public string Type, Text;

        public NodeData(double left, double top, double width, double height, string type, string text)
        {
            Left = left;
            Top = top;
            Width = width;
            Height = height;
            Type = type;
            Text = text;
        }
    }
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
            List<NodeData> nodeDatas = new List<NodeData>();

            ProjectSaveLoading saveLoadingWindow = new ProjectSaveLoading(Node.NodeList.Count);
            saveLoadingWindow.HorizontalAlignment = HorizontalAlignment.Left;
            saveLoadingWindow.VerticalAlignment = VerticalAlignment.Top;
            Canvas.SetTop(saveLoadingWindow, 0);
            Canvas.SetBottom(saveLoadingWindow, 0);
            Grid.SetZIndex(saveLoadingWindow, 4000);
            MainWindow._MainWindow.mainGrid.Children.Add(saveLoadingWindow);

            DispatcherTimer _timer = new DispatcherTimer();
            int a = 0;
            _timer.Interval = TimeSpan.FromMilliseconds(1);
            _timer.Tick += (s, args) =>
            {
                for (int i = 0; i < 100; i++)
                {
                    if (a >= Node.NodeList.Count)
                    {
                        _timer.Stop();
                        saveLoadingWindow.Dispose();

                        SaveData saveData = new SaveData(ProjectProperty.ExportFiles, ProjectProperty.NodeTypes, nodeDatas);
                        string json = JsonConvert.SerializeObject(saveData);

                        File.WriteAllText(path, json);

                        break;
                    }
                    nodeDatas.Add(new NodeData(Canvas.GetLeft(Node.NodeList[a]), Canvas.GetTop(Node.NodeList[a]), Node.NodeList[a].Width, Node.NodeList[a].Height, Node.NodeList[a].typeComboBox.SelectedValue.ToString(), Node.NodeList[a].contentTextBox.Text));

                    a++;
                    saveLoadingWindow.SetNodeCount(a);
                }
            };

            _timer.Start();
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
            openFileDialog.Filter = "MattNode file (*.MattNode)|*.MattNode";

            if (openFileDialog.ShowDialog() == true)
            {
                SavePath = openFileDialog.FileName;
                string json = File.ReadAllText(SavePath);
                SaveData saveData = JsonConvert.DeserializeObject<SaveData>(json);

                ProjectProperty.ExportFiles = saveData.ExportFiles;
                ProjectProperty.NodeTypes = saveData.NodeTypes;

                ProjectCleanLoading cleanLoadingWindow = new ProjectCleanLoading(Node.NodeList.Count);
                cleanLoadingWindow.HorizontalAlignment = HorizontalAlignment.Left;
                cleanLoadingWindow.VerticalAlignment = VerticalAlignment.Top;
                Canvas.SetTop(cleanLoadingWindow, 0);
                Canvas.SetBottom(cleanLoadingWindow, 0);
                Grid.SetZIndex(cleanLoadingWindow, 4000);
                MainWindow._MainWindow.mainGrid.Children.Add(cleanLoadingWindow);

                DispatcherTimer _timer = new DispatcherTimer();
                int a = 0;
                _timer.Interval = TimeSpan.FromMilliseconds(1);
                _timer.Tick += (s, args) =>
                {
                    for (int i = 0; i < 100; i++)
                    {
                        if (Node.NodeList.Count <= 0)
                        {
                            ProjectOpenLoading openLoadingWindow = new ProjectOpenLoading(saveData.NodeDatas.Count);
                            openLoadingWindow.HorizontalAlignment = HorizontalAlignment.Left;
                            openLoadingWindow.VerticalAlignment = VerticalAlignment.Top;
                            Canvas.SetTop(openLoadingWindow, 0);
                            Canvas.SetBottom(openLoadingWindow, 0);
                            Grid.SetZIndex(openLoadingWindow, 4000);
                            MainWindow._MainWindow.mainGrid.Children.Add(openLoadingWindow);

                            DispatcherTimer _timer2 = new DispatcherTimer();
                            int b = 0;
                            _timer2.Interval = TimeSpan.FromMilliseconds(1);
                            _timer2.Tick += (s, args) =>
                            {
                                for (int i = 0; i < 100; i++)
                                {
                                    if (b >= saveData.NodeDatas.Count)
                                    {
                                        _timer2.Stop();
                                        openLoadingWindow.Dispose();
                                        break;
                                    }

                                    Node node = new Node(false, new Point(0, 0));
                                    Canvas.SetLeft(node, saveData.NodeDatas[b].Left);
                                    Canvas.SetTop(node, saveData.NodeDatas[b].Top);
                                    node.Width = saveData.NodeDatas[b].Width;
                                    node.Height = saveData.NodeDatas[b].Height;
                                    node.RepositionElements();
                                    Panel.SetZIndex(node, 100);

                                    //MainWindow._MainWindow.mainCanvas.mainCanvas.Children.Add(node);
                                    node.SetTypeItems();
                                    node.typeComboBox.SelectedValue = saveData.NodeDatas[b].Type;
                                    node.SetContent(saveData.NodeDatas[b].Text);
                                    node.ReregisterCollisionTree();

                                    b++;
                                    openLoadingWindow.SetNodeCount(b);
                                }
                            };

                            _timer2.Start();
                            _timer.Stop();
                            cleanLoadingWindow.Dispose();
                            break;
                        }

                        Node.NodeList[0].Dispose();

                        a++;
                        cleanLoadingWindow.SetNodeCount(a);
                    }
                };

                _timer.Start();
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
