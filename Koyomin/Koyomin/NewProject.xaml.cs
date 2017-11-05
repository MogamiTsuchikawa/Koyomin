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
    /// NewProject.xaml の相互作用ロジック
    /// </summary>
    public partial class NewProject : Window
    {
        public NewProject()
        {
            InitializeComponent();
        }

        private void LanguageValue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            KindValue.Items.Clear();
            switch (LanguageValue.SelectedValue.ToString())
            {
                case "C#":
                    KindValue.Items.Add("WindowsConsole");
                    KindValue.Items.Add("WPF");
                    break;
                case "Python":
                    KindValue.Items.Add("BasicApp");
                    break;
                case "JavaScript":
                    KindValue.Items.Add("BasicApp");
                    KindValue.Items.Add("WebPage");
                    break;

            }
        }

        private void KindValue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LanguageValue.Items.Add("C#");
            LanguageValue.Items.Add("Python");
            LanguageValue.Items.Add("JavaScript");
        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            if(ProjectName.Text != ""&&LanguageValue.Text != "" & KindValue.Text != "")
            {
                Hensu.ProjectName = ProjectName.Text;
                Hensu.Language = LanguageValue.Text;
                Hensu.ProjectKind = KindValue.Text;
                switch (LanguageValue.Text)
                {
                    case "C#":
                        if(CsharpProject())this.Close();
                        break;
                    case "Python":
                        if(PythonProject())this.Close();
                        break;
                    case "JavaScript":
                        if(JavaScriptProject())this.Close();
                        break;
                }
                
            }
            else
            {
                MessageBox.Show("設定していない箇所があります");
            }
        }

        bool CsharpProject()
        {
            bool rt=false;
            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\Koyomin\" + ProjectName.Text;
            if (System.IO.Directory.Exists(path))
            {
                MessageBox.Show("その名前のプロジェクトはすでに存在します。");
            }
            else
            {
                System.IO.Directory.CreateDirectory(path + @"\source");
                System.IO.Directory.CreateDirectory(path + @"\build");
                System.IO.StreamWriter kymF = new System.IO.StreamWriter(path+@"\"+Hensu.ProjectName+@".kym",false);
                kymF.WriteLine("ProjectName:"+Hensu.ProjectName);
                kymF.WriteLine("Kind:"+Hensu.ProjectKind);
                kymF.WriteLine("Language:"+Hensu.Language);
                kymF.WriteLine("Mode:Simple");
                kymF.Close();
                switch (Hensu.ProjectKind)
                {
                    case "WindowsConsole":
                        System.IO.StreamWriter MainFcsC = new System.IO.StreamWriter(path + @"\source\main.cs", false);
                        MainFcsC.WriteLine("using System;");
                        MainFcsC.WriteLine("namespace " + Hensu.ProjectName);
                        MainFcsC.WriteLine("{");
                        MainFcsC.WriteLine("\tstatic class main");
                        MainFcsC.WriteLine("\t{");
                        MainFcsC.WriteLine("\t\t[STAThread]");
                        MainFcsC.WriteLine("\t\tstatic void Main()");
                        MainFcsC.WriteLine("\t\t{");
                        MainFcsC.WriteLine("\t\t\t");
                        MainFcsC.WriteLine("\t\t}");
                        MainFcsC.WriteLine("\t}");
                        MainFcsC.WriteLine("}");
                        MainFcsC.Close();
                        break;
                    case "WPF":
                        System.IO.StreamWriter appF = new System.IO.StreamWriter(
                        path+ @"\source\app.xaml");
                        appF.WriteLine(@"<Application x:Class=""" + Hensu.ProjectName + @".App""");
                        appF.WriteLine("\t\t" + @"xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""");
                        appF.WriteLine("\t\t" + @"xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""");
                        appF.WriteLine("\t\t" + @"xmlns:local=""clr -namespace:" + Hensu.ProjectName + @"""");
                        appF.WriteLine("\t\t" + @"StartupUri=""MainWindow.xaml"">");
                        appF.WriteLine(@"</Application>");
                        appF.Close();
                        System.IO.StreamWriter MainW = new System.IO.StreamWriter(
                            path + @"\source\MainWindow.xaml");
                        MainW.WriteLine(@"<Window x:Class=""" + Hensu.ProjectName + @".MainWindow""");
                        MainW.WriteLine("\t\t" + @"xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""");
                        MainW.WriteLine("\t\t" + @"xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""");
                        MainW.WriteLine("\t\t" + @"xmlns:d=""http://schemas.microsoft.com/expression/blend/2008""");
                        MainW.WriteLine("\t\t" + @"xmlns:mc=""http://schemas.openxmlformats.org/markup-compatibility/2006""");
                        MainW.WriteLine("\t\t" + @"xmlns:local=""clr-namespace:""" + Hensu.ProjectName + @"""");
                        MainW.WriteLine("\t\t" + @"mc:Ignorable=""d""");
                        MainW.WriteLine("\t\t" + @"Title=""MainWindow"" Height=""350"" Width=""525"">");
                        MainW.WriteLine("\t" + @"<Grid>");
                        MainW.WriteLine("\t\t");
                        MainW.WriteLine("\t" + @"</Grid>");
                        MainW.WriteLine(@"</Window>");
                        MainW.WriteLine(@"");
                        MainW.Close();
                        System.IO.StreamWriter mainCS = new System.IO.StreamWriter(
                            path+ @"\source\main.cs");
                        mainCS.WriteLine("using System;");
                        mainCS.WriteLine("using System.Windows;");
                        mainCS.WriteLine("");
                        mainCS.WriteLine("namespace " + Hensu.ProjectName);
                        mainCS.WriteLine("{");
                        mainCS.WriteLine("\tclass main");
                        mainCS.WriteLine("\t{");
                        mainCS.WriteLine("\t\tstatic class void Main()");
                        mainCS.WriteLine("\t\t{");
                        mainCS.WriteLine("\t\t\t");
                        mainCS.WriteLine("\t\t}");
                        mainCS.WriteLine("\t}");
                        mainCS.WriteLine("}");
                        mainCS.Close();
                        break;
                }
                
                Hensu.ProjectPath = path;
                rt = true;
            }
            return rt;
        }
        bool PythonProject()
        {
            bool rt = false;
            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\Koyomin\" + ProjectName.Text;
            if (System.IO.Directory.Exists(path))
            {
                MessageBox.Show("その名前のプロジェクトはすでに存在します。");
            }
            else
            {
                System.IO.Directory.CreateDirectory(path + @"\source");
                System.IO.Directory.CreateDirectory(path + @"\build");
                System.IO.StreamWriter kymF = new System.IO.StreamWriter(path + @"\" + Hensu.ProjectName + @".kym", false);
                kymF.WriteLine("ProjectName:" + Hensu.ProjectName);
                kymF.WriteLine("Kind:BasicApp");
                kymF.WriteLine("Language:" + Hensu.Language);
                kymF.WriteLine("Mode:Simple");
                kymF.Close();
                System.IO.StreamWriter MainF = new System.IO.StreamWriter(path + @"\source\main.py", false);
                MainF.Write("");
                MainF.Close();
                Hensu.ProjectPath = path;
                rt = true;
            }
            return rt;
        }
        bool JavaScriptProject()
        {
            bool rt = false;
            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\Koyomin\" + ProjectName.Text;
            if (System.IO.Directory.Exists(path))
            {
                MessageBox.Show("その名前のプロジェクトはすでに存在します。");
            }
            else
            {
                System.IO.Directory.CreateDirectory(path + @"\source");
                System.IO.Directory.CreateDirectory(path + @"\build");
                System.IO.StreamWriter kymF = new System.IO.StreamWriter(path + @"\" + Hensu.ProjectName + @".kym", false);
                kymF.WriteLine("ProjectName:" + Hensu.ProjectName);
                switch (KindValue.Text)
                {
                    case "BasicApp":
                        kymF.WriteLine("Kind:BasicApp");
                        System.IO.StreamWriter MainJS = new System.IO.StreamWriter(path + @"\source\main.js", false);
                        MainJS.Write("");
                        MainJS.Close();
                        break;
                    case "WebPage":
                        kymF.WriteLine("Kind:WebPage");
                        System.IO.StreamWriter MainHtml = new System.IO.StreamWriter(path + @"\source\index.html", false);
                        MainHtml.Write("");
                        MainHtml.Close();
                        break;
                }
                
                
                kymF.WriteLine("Language:" + Hensu.Language);
                kymF.WriteLine("Mode:Simple");
                kymF.Close();
                
                Hensu.ProjectPath = path;
                rt = true;
            }
            return rt;
        }
    }
}
