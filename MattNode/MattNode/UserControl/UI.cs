using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MattNode
{
    public partial class UI : UserControl
    {
        public static List<UI> UiList = new List<UI>();
        public UI()
        {
            InitializeComponent();
            UiList.Add(this);
        }

        public static void BringUiToFront()
        {
            for(int i = 0; i < UiList.Count; i++)
            {
                UiList[i].BringToFront();
            }
        }
    }
}
