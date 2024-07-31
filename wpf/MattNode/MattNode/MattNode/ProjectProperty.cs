using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MattNode
{
    public struct FileExportOption
    {
        public bool WriteType;
        public bool WriteText;
        public bool WritePrevNodes;
        public bool WriteNextNodes;

        public FileExportOption(bool writeType, bool writeText, bool writePrevNodes, bool writeNextNodes)
        {
            WriteType = writeType;
            WriteText = writeText;
            WritePrevNodes = writePrevNodes;
            WriteNextNodes = writeNextNodes;
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

        public NodeType(string name, SolidColorBrush color, List<FileExportOption> exportOptions)
        {
            Name = name;
            Color = color;
            if (exportOptions == null)
            {
                ExportOption = new List<FileExportOption>();
                for (int i = 0; i < ProjectProperty.ExportFiles.Count; i++)
                {
                    ExportOption.Add(new FileExportOption(false, false, false, false));
                }
            }
            else
            {
                ExportOption = exportOptions;
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

            AddExportFile(name,".csv");
        }

        public static void AddExportFile(string name, string extension)
        {
            ExportFiles.Add(new ExportFile(name, extension));

            for (int i = 0; i < NodeTypes.Count; i++)
            {
                NodeTypes[i].ExportOption.Add(new FileExportOption(false, false, false, false));
            }
        }

        public static void ModifyExportFile(int num, string newName, string newExtension)
        {
            ExportFiles[num] = new ExportFile(newName, newExtension);
        }

        public static void RemoveExportFile(int num)
        {
            for (int i = 0; i < NodeTypes.Count; i++)
            {
                NodeTypes[i].ExportOption.RemoveAt(num);
            }
            ExportFiles.RemoveAt(num);
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
            NodeTypes.Add(new NodeType(name,color,null));
        }

        public static void ModifyNodeType(int num, string newName, SolidColorBrush newColor, List<FileExportOption> exportOptions)
        {
            NodeTypes[num] = new NodeType(newName, newColor, exportOptions);
        }

        public static void RemoveNodeType(int num)
        {
            NodeTypes.RemoveAt(num);
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

        public static void CleanProperty()
        {
            ExportFiles = new List<ExportFile>();
            NodeTypes = new List<NodeType>();
        }

        public static void ApplyDefaultProperty()
        {
            CleanProperty();
            
            AddExportFile("TextTable", ".csv");
            AddExportFile("Script", ".yymps(Script)");
            
            AddNodeType("Text", new SolidColorBrush(Color.FromRgb(255,255,255)));
            AddNodeType("Script", new SolidColorBrush(Color.FromRgb(193, 102, 107)));

            NodeTypes[0].ExportOption[0] = new FileExportOption(true, true, true, true);
            NodeTypes[0].ExportOption[1] = new FileExportOption(false, false, false, false);

            NodeTypes[1].ExportOption[0] = new FileExportOption(true, false, true, true);
            NodeTypes[1].ExportOption[1] = new FileExportOption(false, true, false, false);
        }
    }
}
