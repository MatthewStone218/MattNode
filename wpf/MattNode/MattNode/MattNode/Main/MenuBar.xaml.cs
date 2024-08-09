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
using System.IO.Compression;
using static System.Net.Mime.MediaTypeNames;
using MattNode.Property;
using System.Windows.Threading;
using static MattNode.MenuBar;


namespace MattNode
{
    /// <summary>
    /// MenuBar.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 
    public class SaveData
    {
        public List<ExportFile> ExportFiles;
        public List<NodeType> NodeTypes;
        public List<NodeData> NodeDatas;
        public int NodeCount;
        public SaveData(List<ExportFile> exportFiles, List<NodeType> nodeTypes, int nodeCount, List<NodeData> nodeDatas) 
        {
            ExportFiles = exportFiles;
            NodeTypes = nodeTypes;
            NodeCount = nodeCount;
            NodeDatas = nodeDatas;
        }
    }

    public class NodeData
    {
        public int Num;
        public double Top,Left,Width,Height;
        public string Type, Text;
        public List<int> NextNodes;

        public NodeData(int num, double left, double top, double width, double height, string type, string text, List<int> nextNodes)
        {
            Num = num;
            Left = left;
            Top = top;
            Width = width;
            Height = height;
            Type = type;
            Text = text;
            NextNodes = nextNodes;
        }
    }
    public partial class MenuBar : UserControl
    {
        public enum STATE
        {
            NORMAL,
            PROPERTY,
            CLEAN,
            SAVE,
            OPEN,
            EXPORT
        }
        public static STATE State = STATE.NORMAL;
        public static string ?SavePath = null;
        public delegate void ActionAfterSave();
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
            State = STATE.PROPERTY;
            PropertyMenu propertyMenu = new PropertyMenu();
            propertyMenu.Margin = new Thickness(0, 0, 0, 0); // 위치 설정
            propertyMenu.HorizontalAlignment = HorizontalAlignment.Left;
            propertyMenu.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetZIndex(propertyMenu, 1000);
            MainWindow._MainWindow.mainGrid.Children.Add(propertyMenu);
        }

        public static void SaveProject(ActionAfterSave actionAfterSave = null)
        {
            if(SavePath == null)
            {
                SaveProjectAs(actionAfterSave);
            }
            else
            {
                SaveProject(SavePath, actionAfterSave);
            }
        }

        public static void SaveAndCloseProject()
        {
            SaveProject(() => {
                MainWindow.CloseByCode = true;
                MainWindow._MainWindow.Close();
            });
        }

        public static void SaveProject(string path, ActionAfterSave actionAfterSave = null)
        {
            State = STATE.SAVE;

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
                        State = STATE.NORMAL;
                        _timer.Stop();
                        saveLoadingWindow.Dispose();

                        SaveData saveData = new SaveData(ProjectProperty.ExportFiles, ProjectProperty.NodeTypes, Node.NodeCount, nodeDatas);
                        string json = JsonConvert.SerializeObject(saveData);

                        File.WriteAllText(path, json);

                        if(actionAfterSave != null)
                        {
                            actionAfterSave();
                        }

                        break;
                    }

                    List<int> nextNodes = new List<int>();

                    for (int ii = 0; ii < Node.NodeList[a].ArrowsFromMe.Count; ii++)
                    {
                        nextNodes.Add(Node.NodeList[a].ArrowsFromMe[ii].EndNode.Num);
                    }

                    nodeDatas.Add(new NodeData(
                        Node.NodeList[a].Num,
                        Canvas.GetLeft(Node.NodeList[a]), 
                        Canvas.GetTop(Node.NodeList[a]), 
                        Node.NodeList[a].Width, 
                        Node.NodeList[a].Height, 
                        Node.NodeList[a].typeComboBox.SelectedValue.ToString(), 
                        Node.NodeList[a].contentTextBox.Text,
                        nextNodes
                        ));

                    a++;
                    saveLoadingWindow.SetNodeCount(a);
                }
            };

            _timer.Start();
        }

        public static void SaveProjectAs(ActionAfterSave actionAfterSave = null)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            // 저장할 파일 형식 필터 설정 (예: 텍스트 파일)
            saveFileDialog.Filter = "MattNode file (*.MattNode)|*.MattNode";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (saveFileDialog.ShowDialog() == true)
            {
                SaveProject(saveFileDialog.FileName, actionAfterSave);
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
                State = STATE.CLEAN;
                SavePath = openFileDialog.FileName;
                string json = File.ReadAllText(SavePath);
                SaveData saveData = JsonConvert.DeserializeObject<SaveData>(json);

                ProjectProperty.ExportFiles = saveData.ExportFiles;
                ProjectProperty.NodeTypes = saveData.NodeTypes;
                Node.NodeCount = saveData.NodeCount;

                MainCanvas.Canvas.X = 0;
                MainCanvas.Canvas.Y = 0;

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
                            State = STATE.OPEN;

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
                                for (int ii = 0; ii < 100; ii++)
                                {
                                    if (b >= saveData.NodeDatas.Count)
                                    {
                                        ProjectOpenArrowsLoading openArrowLoadingWindow = new ProjectOpenArrowsLoading(saveData.NodeDatas.Count);
                                        openArrowLoadingWindow.HorizontalAlignment = HorizontalAlignment.Left;
                                        openArrowLoadingWindow.VerticalAlignment = VerticalAlignment.Top;
                                        Canvas.SetTop(openArrowLoadingWindow, 0);
                                        Canvas.SetBottom(openArrowLoadingWindow, 0);
                                        Grid.SetZIndex(openArrowLoadingWindow, 4000);
                                        MainWindow._MainWindow.mainGrid.Children.Add(openArrowLoadingWindow);

                                        DispatcherTimer _timer3 = new DispatcherTimer();
                                        int c = 0;
                                        _timer3.Interval = TimeSpan.FromMilliseconds(1);
                                        _timer3.Tick += (s, args) =>
                                        {
                                            for (int iii = 0; iii < 100; iii++)
                                            {
                                                if (c >= saveData.NodeDatas.Count)
                                                {
                                                    State = STATE.NORMAL;
                                                    _timer3.Stop();
                                                    openArrowLoadingWindow.Dispose();
                                                    break;
                                                }

                                                for (int k = 0; k < saveData.NodeDatas[c].NextNodes.Count; k++)
                                                {
                                                    Node node1 = null;
                                                    for(int j = 0; j < Node.NodeList.Count; j++)
                                                    {
                                                        if (Node.NodeList[j].Num == saveData.NodeDatas[c].Num)
                                                        {
                                                            node1 = Node.NodeList[j];
                                                            break;
                                                        }
                                                    }

                                                    Node node2 = null;
                                                    for (int j = 0; j < Node.NodeList.Count; j++)
                                                    {
                                                        if (Node.NodeList[j].Num == saveData.NodeDatas[c].NextNodes[k])
                                                        {
                                                            node2 = Node.NodeList[j];
                                                            break;
                                                        }
                                                    }

                                                    NodeArrow nodeArrow = new NodeArrow(node1, node2);
                                                    Panel.SetZIndex(nodeArrow, 90);

                                                    MainWindow._MainWindow.mainCanvas.mainCanvas.Children.Add(nodeArrow);
                                                    node2.ArrowsFromOther.Add(nodeArrow);
                                                    node1.ArrowsFromMe.Add(nodeArrow);
                                                }

                                                c++;
                                                openArrowLoadingWindow.SetNodeCount(c);
                                            }
                                        };

                                        _timer3.Start();

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
                                    node.Num = saveData.NodeDatas[b].Num;
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

        private void NewProject(object sender, RoutedEventArgs e)
        {
            SaveAskNewFile saveAsk = new SaveAskNewFile();
            saveAsk.HorizontalAlignment = HorizontalAlignment.Left;
            saveAsk.VerticalAlignment = VerticalAlignment.Top;
            Canvas.SetTop(saveAsk, 0);
            Canvas.SetBottom(saveAsk, 0);
            Grid.SetZIndex(saveAsk, 4000);
            MainWindow._MainWindow.mainGrid.Children.Add(saveAsk);
        }

        public static void NewProject()
        {
            State = STATE.CLEAN;

            SavePath = null;
            ProjectProperty.ApplyDefaultProperty();

            MainCanvas.Canvas.X = 0;
            MainCanvas.Canvas.Y = 0;

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
                        State = STATE.NORMAL;
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

        private void ExportFile(object sender, RoutedEventArgs e)
        {
            State = STATE.EXPORT;

            if (SavePath != null)
            {
                SaveProject(SavePath);
            }

            List<NodeData> nodeDatas = new List<NodeData>();

            ProjectExportLoading exportLoadingWindow = new ProjectExportLoading();
            exportLoadingWindow.HorizontalAlignment = HorizontalAlignment.Left;
            exportLoadingWindow.VerticalAlignment = VerticalAlignment.Top;
            Canvas.SetTop(exportLoadingWindow, 0);
            Canvas.SetBottom(exportLoadingWindow, 0);
            Grid.SetZIndex(exportLoadingWindow, 4000);
            MainWindow._MainWindow.mainGrid.Children.Add(exportLoadingWindow);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            // 저장할 파일 형식 필터 설정 (예: 텍스트 파일)
            saveFileDialog.Filter = "zip file (*.zip)|*.zip";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (saveFileDialog.ShowDialog() == true)
            {
                string zipPath = saveFileDialog.FileName;
                string folderPath = System.IO.Path.GetDirectoryName(zipPath)+"\\temp-amtt-node-delete-this-folder";
                Directory.CreateDirectory(folderPath);

                string text = "";

                for (int b = 0; b < ProjectProperty.ExportFiles.Count; b++)
                {
                    text = "";

                    if (ProjectProperty.ExportFiles[b].Extension == ".csv")
                    {
                        text += "index,type,text,next_node,prev_node";
                    }
                    else if (ProjectProperty.ExportFiles[b].Extension == ".txt(Structure containing functions)")
                    {
                        text += "{";
                    }
                    else if (ProjectProperty.ExportFiles[b].Extension == ".txt(Structure)")
                    {
                        text += "{";
                    }
                    else if (ProjectProperty.ExportFiles[b].Extension == ".txt(Structure without indexing)")
                    {
                        text += "{";
                    }
                    else if (ProjectProperty.ExportFiles[b].Extension == ".txt(Script containing functions)")
                    {
                    }
                    else if (ProjectProperty.ExportFiles[b].Extension == ".txt(Script without indexing)")
                    {
                    }

                    for (int i = 0; i < Node.NodeList.Count; i++)
                    {
                        string text2 = "";
                        int _index = Node.NodeList[i].Num;
                        string _text = Node.NodeList[i].GetNodeText();
                        string _type = Node.NodeList[i].GetNodeType();

                        bool write = false;

                        int ?type_num = null;

                        for(int a = 0; a < ProjectProperty.NodeTypes.Count; a++)
                        {
                            if(Node.NodeList[i].GetNodeType() == ProjectProperty.NodeTypes[a].Name)
                            {
                                type_num = a;
                                break;
                            }
                        }

                        List<int> nextNodes = new List<int>();

                        for (int ii = 0; ii < Node.NodeList[i].ArrowsFromMe.Count; ii++)
                        {
                            nextNodes.Add(Node.NodeList[i].ArrowsFromMe[ii].EndNode.Num);
                        }

                        List<int> prevNodes = new List<int>();

                        for (int ii = 0; ii < Node.NodeList[i].ArrowsFromOther.Count; ii++)
                        {
                            prevNodes.Add(Node.NodeList[i].ArrowsFromOther[ii].StartNode.Num);
                        }

                        if (ProjectProperty.ExportFiles[b].Extension == ".csv")
                        {
                            text += $"\n{_index},";
                            if (ProjectProperty.NodeTypes[(int)type_num].ExportOption[b].WriteType)
                            {
                                write = true;
                                text2 += $"\"{_type}\",";
                            }
                            else
                            {
                                text2 += $"\"\",";
                            }

                            if (ProjectProperty.NodeTypes[(int)type_num].ExportOption[b].WriteText)
                            {
                                write = true;
                                text2 += $"\"{_text.Replace("\"","\"\"")}\",";
                            }
                            else
                            {
                                text2 += $"\"\",";
                            }

                            if (ProjectProperty.NodeTypes[(int)type_num].ExportOption[b].WriteNextNodes)
                            {
                                write = true;
                                text2 += $"\"[";
                                for (int ii = 0; ii < nextNodes.Count; ii++)
                                {
                                    text2 += $"{nextNodes[ii]},";
                                }
                                text2 += $"]\",";
                            }
                            else
                            {
                                text2 += $"\"[]\"";
                            }

                            if (ProjectProperty.NodeTypes[(int)type_num].ExportOption[b].WritePrevNodes)
                            {
                                write = true;
                                text2 += $"\"[";
                                for (int ii = 0; ii < prevNodes.Count; ii++)
                                {
                                    text2 += $"{prevNodes[ii]},";
                                }
                                text2 += $"]\",";
                            }
                        }
                        else if (ProjectProperty.ExportFiles[b].Extension == ".txt(Structure containing functions)")
                        {
                            text2 += $"\n\t{Node.NodeList[i].Num}: {{";

                            if (ProjectProperty.NodeTypes[(int)type_num].ExportOption[b].WriteType)
                            {
                                write = true;
                                text2 += $"\n\t\ttype: \"{_type}\"";
                            }

                            if (ProjectProperty.NodeTypes[(int)type_num].ExportOption[b].WriteText)
                            {
                                write = true;
                                text2 += $"\n\t\tfunc: function(){{\n\t\t\t{_text.Replace(System.Environment.NewLine, "\n\t\t\t")}\n\t\t}}";
                            }

                            if (ProjectProperty.NodeTypes[(int)type_num].ExportOption[b].WriteNextNodes)
                            {
                                write = true;
                                text2 += $"\n\t\tnext_nodes: [";
                                for (int ii = 0; ii < nextNodes.Count; ii++)
                                {
                                    text2 += $"{nextNodes[ii]},";
                                }
                                text2 += $"]";
                            }

                            if (ProjectProperty.NodeTypes[(int)type_num].ExportOption[b].WritePrevNodes)
                            {
                                write = true;
                                text2 += $"\n\t\tprev_nodes: [";
                                for (int ii = 0; ii < prevNodes.Count; ii++)
                                {
                                    text2 += $"{prevNodes[ii]},";
                                }
                                text2 += $"]";
                            }

                            text2 += $"\n\t}}\n,";
                        }
                        else if (ProjectProperty.ExportFiles[b].Extension == ".txt(Structure)")
                        {
                            text2 += $"\n\t{Node.NodeList[i].Num}: {{";

                            if (ProjectProperty.NodeTypes[(int)type_num].ExportOption[b].WriteType)
                            {
                                write = true;
                                text2 += $"\n\t\ttype: \"{_type}\"";
                            }

                            if (ProjectProperty.NodeTypes[(int)type_num].ExportOption[b].WriteText)
                            {
                                write = true;
                                text2 += $"\n\t\ttext: \"{_text.Replace(System.Environment.NewLine, "\\n")}\"";
                            }

                            if (ProjectProperty.NodeTypes[(int)type_num].ExportOption[b].WriteNextNodes)
                            {
                                write = true;
                                text2 += $"\n\t\tnext_nodes: [";
                                for (int ii = 0; ii < nextNodes.Count; ii++)
                                {
                                    text2 += $"{nextNodes[ii]},";
                                }
                                text2 += $"]";
                            }

                            if (ProjectProperty.NodeTypes[(int)type_num].ExportOption[b].WritePrevNodes)
                            {
                                write = true;
                                text2 += $"\n\t\tprev_nodes: [";
                                for (int ii = 0; ii < prevNodes.Count; ii++)
                                {
                                    text2 += $"{prevNodes[ii]},";
                                }
                                text2 += $"]";
                            }

                            text2 += $"\n\t}}\n,";
                        }
                        else if (ProjectProperty.ExportFiles[b].Extension == ".txt(Structure without indexing)")
                        {
                            if (ProjectProperty.NodeTypes[(int)type_num].ExportOption[b].WriteText)
                            {
                                write = true;
                                text2 += $"\n\t{_text.Replace(System.Environment.NewLine, "\n\t")}\n,";
                            }
                        }
                        else if (ProjectProperty.ExportFiles[b].Extension == ".txt(Script containing functions)")
                        {
                            text2 += $"\nfunction func_{Node.NodeList[i].Num}(){{";

                            if (ProjectProperty.NodeTypes[(int)type_num].ExportOption[b].WriteType)
                            {
                                write = true;
                                text2 += $"\n\tvar _type = \"{_type}\";";
                            }

                            if (ProjectProperty.NodeTypes[(int)type_num].ExportOption[b].WriteNextNodes)
                            {
                                write = true;
                                text2 += $"\n\tvar _next_nodes = [";
                                for (int ii = 0; ii < nextNodes.Count; ii++)
                                {
                                    text2 += $"{nextNodes[ii]},";
                                }
                                text2 += $"];";
                            }

                            if (ProjectProperty.NodeTypes[(int)type_num].ExportOption[b].WritePrevNodes)
                            {
                                write = true;
                                text2 += $"\n\tvar prev_nodes = [";
                                for (int ii = 0; ii < prevNodes.Count; ii++)
                                {
                                    text2 += $"{prevNodes[ii]},";
                                }
                                text2 += $"];";
                            }

                            if (ProjectProperty.NodeTypes[(int)type_num].ExportOption[b].WriteText)
                            {
                                write = true;
                                text2 += $"\n\t{_text.Replace(System.Environment.NewLine, "\n\t")}";
                            }

                            text2 += "\n}\n";
                        }
                        else if (ProjectProperty.ExportFiles[b].Extension == ".txt(Script without indexing)")
                        {
                            if (ProjectProperty.NodeTypes[(int)type_num].ExportOption[b].WriteText)
                            {
                                write = true;
                                text2 += $"\n{_text}\n";
                            }
                        }
                        
                        if (write)
                        {
                            text += text2;
                        }
                    }

                    if (ProjectProperty.ExportFiles[b].Extension == ".csv")
                    {
                        File.WriteAllText(System.IO.Path.Combine(folderPath, ProjectProperty.ExportFiles[b].Name) + ".csv", text);
                    }
                    else if (ProjectProperty.ExportFiles[b].Extension == ".txt(Structure containing functions)")
                    {
                        text += "}";
                        File.WriteAllText(System.IO.Path.Combine(folderPath, ProjectProperty.ExportFiles[b].Name) + ".txt", text);
                    }
                    else if (ProjectProperty.ExportFiles[b].Extension == ".txt(Structure)")
                    {
                        text += "}";
                        File.WriteAllText(System.IO.Path.Combine(folderPath, ProjectProperty.ExportFiles[b].Name) + ".txt", text);
                    }
                    else if (ProjectProperty.ExportFiles[b].Extension == ".txt(Structure without indexing)")
                    {
                        text += "\n}";
                        File.WriteAllText(System.IO.Path.Combine(folderPath, ProjectProperty.ExportFiles[b].Name) + ".txt", text);
                    }
                    else if (ProjectProperty.ExportFiles[b].Extension == ".txt(Script containing functions)")
                    {
                        File.WriteAllText(System.IO.Path.Combine(folderPath, ProjectProperty.ExportFiles[b].Name) + ".txt", text);
                    }
                    else if (ProjectProperty.ExportFiles[b].Extension == ".txt(Script without indexing)")
                    {
                        File.WriteAllText(System.IO.Path.Combine(folderPath, ProjectProperty.ExportFiles[b].Name) + ".txt", text);
                    }
                }

                if (File.Exists(zipPath)) { File.Delete(zipPath); }

                ZipFile.CreateFromDirectory(folderPath, zipPath);

                Directory.Delete(folderPath, true);
            }

            State = STATE.NORMAL;
            exportLoadingWindow.Dispose();
        }
    }
}
