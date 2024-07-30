using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MattNode
{
    public struct FileExportOption
    {
        public string Name;
        public bool WriteIndex;
        public bool WriteType;
        public bool WriteText;

        public FileExportOption(string name)
        {
            Name = name;
            WriteIndex = false;
            WriteType = false;
            WriteText = false;
        }

        public FileExportOption(string name, FileExportOption originalOption)
        {
            Name = name;
            WriteIndex = originalOption.WriteIndex;
            WriteType = originalOption.WriteType;
            WriteText = originalOption.WriteText;
        }
    }
    public struct ExportFile
    {
        public string Name;
        public string Extension;

        public ExportFile(string name, string extension) 
        {
            Name = name;
            Extension = extension;
        }
    }
    public struct NodeType
    {
        public string Name;
        public SolidColorBrush Color;
        public List<FileExportOption> ExportOption;

        public NodeType(string name, SolidColorBrush color)
        {
            Name = name;
            Color = color;
            ExportOption = new List<FileExportOption>();

            for(int i = 0; i < ProjectProperty.ExportFiles.Count; i++) 
            {
                ExportOption.Add(new FileExportOption(ProjectProperty.ExportFiles[i].Name));
            }
        }
    }
    public partial class ProjectProperty
    {
        public static List<ExportFile> ExportFiles = new List<ExportFile>();
        public static List<NodeType> NodeTypes = new List<NodeType>();

        public static void AddExportFile()
        {
            string name;
            int i = 1;

            do
            {
                name = $"ExportFile{i}";
                i++;
            }
            while (ExportFileExists(name));

            AddExportFile(name,"csv");
        }

        public static void AddExportFile(string name, string extension)
        {
            ExportFiles.Add(new ExportFile(name, extension));

            for (int i = 0; i < NodeTypes.Count; i++)
            {
                NodeTypes[i].ExportOption.Add(new FileExportOption(name));
            }
        }

        public static void ModifyExportFile(string name, string newName, string newExtension)
        {
            for (int i = 0; i < ExportFiles.Count; i++)
            {
                if (ExportFiles[i].Name == name)
                {
                    for (int ii = 0; ii < NodeTypes.Count; ii++)
                    {
                        NodeTypes[ii].ExportOption[i] = new FileExportOption(newName, NodeTypes[ii].ExportOption[i]);
                    }
                    ExportFiles[i] = new ExportFile(newName, newExtension);
                    break;
                }
            }
        }

        public static void RemoveExportFile(string name)
        {
            for (int i = 0; i < ExportFiles.Count; i++)
            {
                if (ExportFiles[i].Name == name)
                {
                    for (int ii = 0; ii < NodeTypes.Count; ii++)
                    {
                        NodeTypes[ii].ExportOption.RemoveAt(i);
                    }
                    ExportFiles.RemoveAt(i);
                    break;
                }
            }
        }

        public static bool ExportFileExists(string name)
        {
            for(int i = 0;i < ExportFiles.Count;i++)
            {
                if (ExportFiles[i].Name == name) { return true; }
            }

            return false;
        }

        public static void AddNodeType()
        {
            string name;
            int i = 1;

            do
            {
                name = $"NodeType{i}";
                i++;
            }
            while (NopeTypeExists(name));

            Random random = new Random();

            byte r = (byte)random.Next(256);
            byte g = (byte)random.Next(256);
            byte b = (byte)random.Next(256);

            SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(r, g, b));

            AddNodeType(name, brush);
        }
        public static void AddNodeType(string name, SolidColorBrush color)
        {
            NodeTypes.Add(new NodeType(name,color));
        }

        public static void ModifyNodeType(string name, string newName, SolidColorBrush newColor)
        {
            for (int i = 0; i < NodeTypes.Count; i++)
            {
                if (NodeTypes[i].Name == name)
                {
                    NodeTypes[i] = new NodeType(newName, newColor);
                    break;
                }
            }
        }

        public static void RemoveNodeType(string name)
        {
            for (int i = 0; i < NodeTypes.Count; i++)
            {
                if (NodeTypes[i].Name == name)
                {
                    NodeTypes.RemoveAt(i);
                    break;
                }
            }
        }

        public static void SwapNodeType(int index, int newIndex)
        {
            NodeType temp = NodeTypes[index];
            NodeTypes[index] = NodeTypes[newIndex];
            NodeTypes[newIndex] = temp;
        }

        public static bool NopeTypeExists(string name)
        {
            for (int i = 0; i < NodeTypes.Count; i++)
            {
                if (NodeTypes[i].Name == name) { return true; }
            }

            return false;
        }
    }
}
