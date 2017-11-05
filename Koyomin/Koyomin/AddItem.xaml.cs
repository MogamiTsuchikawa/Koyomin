using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Koyomin
{
    /// <summary>
    /// AddItem.xaml の相互作用ロジック
    /// </summary>
    public partial class AddItem : Window
    {
        string[] FT = { "C#", "XAML", "Java","JavaScript","TEXT","Python","VB.NET","XML" };
        public AddItem()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            for(int i=0;FT.Length > i; ++i)
            {
                fileType.Items.Add(FT[i]+"ファイル");
            }
            
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            string fType = ".txt";
            switch (fileType.Text)
            {
                case "C#":fType = ".cs";break;
                case "XAML": fType = ".xaml"; break;
                case "Java": fType = ".java"; break;
                case "JavaScript": fType = ".js"; break;
                case "TEXT": fType = ".txt"; break;
                case "Python": fType = ".py"; break;
                case "VB.NET": fType = ".vb"; break;
                case "XML": fType = ".xml"; break;
            }
            System.IO.StreamWriter SF = new System.IO.StreamWriter(Hensu.ProjectPath + @"\source\" + Fname.Text + fType);
            this.Close();
        }
    }
}
