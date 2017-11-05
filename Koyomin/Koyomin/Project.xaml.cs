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
using System.IO;
using Microsoft.Win32;

namespace Koyomin
{
    /// <summary>
    /// Project.xaml の相互作用ロジック
    /// </summary>
    public partial class Project : Window
    {
        public Project()
        {
            InitializeComponent();
        }

        private void NewProjectBtn_Click(object sender, RoutedEventArgs e)
        {
            //Project新規作成
            //CreateProject Windowに移動
            NewProject Window = new NewProject();
            Window.ShowDialog();
            this.Close();
        }

        private void OpenProjectBtn_Click(object sender, RoutedEventArgs e)
        {
            //Projectを開く
            OpenFileDialog openProject = new OpenFileDialog();
            openProject.FilterIndex = 1;
            openProject.Filter = "Koyominプロジェクト ファイル(.kym)|*.kym";
            bool? result = openProject.ShowDialog();
            if (result == true)
            {
                //KYMファイルを読み込みプロジェクト開始を準備
                System.IO.StreamReader openPrjReader = new System.IO.StreamReader(openProject.FileName);
                //内容を一行ずつ読み込む
                while (openPrjReader.Peek() > -1)
                {
                    string[] temp = openPrjReader.ReadLine().Split(':');
                    switch (temp[0])
                    {
                        case "ProjectName":
                            Hensu.ProjectName = temp[1];
                            break;
                        case "Language":
                            Hensu.Language = temp[1];
                            break;
                        case "Mode":
                            Hensu.Mode = temp[1];
                            break;
                        case "Kind":
                            Hensu.ProjectKind = temp[1];
                            break;
                    }
                }
                openPrjReader.Close();
                //Projectのカレントディレクトリの特定 (Pathには最後に\はつかない)
                Hensu.ProjectPath = System.IO.Path.GetDirectoryName(openProject.FileName);
                this.Close();
            }
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            //Exit
            Hensu.Exit = true;
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
