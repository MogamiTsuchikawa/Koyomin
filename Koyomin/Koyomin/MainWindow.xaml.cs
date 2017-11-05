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
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using ICSharpCode.AvalonEdit.Highlighting;

namespace Koyomin
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public ICSharpCode.AvalonEdit.TextEditor[] avalons;
        public TabItem[] TabPages;
        public string[] TabString;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            while (Hensu.ProjectName == null && Hensu.Exit == false)
            {
                Project ProjectWindow = new Project();
                ProjectWindow.ShowDialog();
            }
            if (Hensu.Exit) Environment.Exit(0);

            this.Title = "Koyomin " + Hensu.ver + " [" + Hensu.ProjectName + "]";
            //TreeViewにプロジェクトファイル一覧を表示
            var files = System.IO.Directory.GetFiles(Hensu.ProjectPath + @"\source", "*", System.IO.SearchOption.AllDirectories);
            if (files.Length != 0)
            {
                for (int i = 0; i < files.Length; ++i)
                {
                    TreeViewItem FileNode = new TreeViewItem();
                    FileNode.Header = System.IO.Path.GetFileName(files[i]);
                    ProjectItemView.Items.Add(FileNode);
                }
            }

            //Tab表示開始 ここから言語別に別れる
            //テキストエディタにて表示可能なファイルを検索
            int countFileTypeText = 0;
            if (files.Length != 0) for (int i = 0; i < files.Length; ++i) if (-1 != Array.IndexOf(Hensu.TextType, System.IO.Path.GetExtension(files[i]))) ++countFileTypeText;

            avalons = new ICSharpCode.AvalonEdit.TextEditor[countFileTypeText];
            TabString = new string[countFileTypeText];
            TabPages = new TabItem[countFileTypeText];
            if (files.Length != 0)
            {
                int TabCount = 0;
                for (int i = 0; i < files.Length; ++i)
                {
                    if (-1 != Array.IndexOf(Hensu.TextType, System.IO.Path.GetExtension(files[i])))
                    {
                        TabPages[TabCount] = new TabItem();
                        avalons[TabCount] = new ICSharpCode.AvalonEdit.TextEditor();
                        TabPages[TabCount].Header = System.IO.Path.GetFileName(files[i]);
                        TabString[TabCount] = System.IO.Path.GetFileName(files[i]);
                        TabPages[TabCount].Content = avalons[TabCount];
                        Tab1.Items.Add(TabPages[TabCount]);
                        System.IO.StreamReader rf = new System.IO.StreamReader(
                            Hensu.ProjectPath + @"\source\" + System.IO.Path.GetFileName(files[i]));
                        avalons[TabCount].Text = rf.ReadToEnd();
                        rf.Close();
                        avalons[TabCount].ShowLineNumbers = true;
                        //ここからファイル種類別
                        string xshdPath = "";
                        switch (System.IO.Path.GetExtension(files[i]))
                        {
                            case ".txt":

                                break;
                            case ".cs":
                                xshdPath = @"AppData\csharp.xshd";
                                break;
                            case ".xaml":
                                xshdPath = @"AppData\XML-Mode.xshd";
                                break;
                            case ".js":
                                xshdPath = @"AppData\JavaScript-Mode.xshd";
                                break;
                            case ".py":
                                xshdPath = @"AppData\Python-Mode.xshd";
                                break;
                            case ".xml":
                                xshdPath = @"AppData\XML-Mode.xshd";
                                break;
                            case ".java":
                                xshdPath = @"AppData\Java-Mode.xshd";
                                break;
                            case ".vb":
                                xshdPath = @"AppData\VB-Mode.xshd";
                                break;
                            case ".html":
                                if(Hensu.Language == "JavaScript")
                                {
                                    xshdPath = @"AppData\JavaScript-Mode.xshd";
                                }
                                else
                                {
                                    xshdPath = @"AppData\XML-Mode.xshd";
                                }
                                break;

                        }
                        if (System.IO.Path.GetExtension(files[i]) != ".txt")
                        {
                            var reader = new System.Xml.XmlTextReader(xshdPath);
                            var definition = HighlightingLoader.Load(reader, HighlightingManager.Instance);
                            avalons[TabCount].SyntaxHighlighting = definition;
                            reader.Close();
                        }
                        //Tab及びavalonテキストエディタ関連処理終わり
                        ++TabCount;
                    }
                }
            }
            Tab1.SelectedIndex = 0;
        }

        private void ProjectItemView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            string selectF = ProjectItemView.SelectedItem.ToString();
            selectF = selectF.Replace("System.Windows.Controls.TreeViewItem Header:", "");
            selectF = selectF.Replace(" Items.Count:0", "");
            //MessageBox.Show(selectF);
            for (int i = 0; TabPages.Length > i; ++i)
            {
                if (TabString[i] == selectF)
                {
                    Tab1.SelectedIndex = i;
                }
            }
        }

        //****************************************************************
        //   メニュー関連処理
        //****************************************************************

        private void SaveProject_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; TabPages.Length > i; ++i)
            {
                System.IO.StreamWriter SaveF = new System.IO.StreamWriter(Hensu.ProjectPath + @"\source\" + TabString[i], false);
                SaveF.Write(avalons[i].Text);
                SaveF.Close();
            }
        }

        private void CutBtn_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetData(DataFormats.Text, avalons[Tab1.SelectedIndex].SelectedText);
            avalons[Tab1.SelectedIndex].SelectedText = "";
        }

        private void CopyBtn_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetData(DataFormats.Text, avalons[Tab1.SelectedIndex].SelectedText);
        }

        private void PasteBtn_Click(object sender, RoutedEventArgs e)
        {
            int index = avalons[Tab1.SelectedIndex].SelectionStart;
            avalons[Tab1.SelectedIndex].Text = avalons[Tab1.SelectedIndex].Text.Insert(index, Clipboard.GetText());
        }

        private void NewItem_Click(object sender, RoutedEventArgs e)
        {
            AddItem Window = new AddItem();
            Window.ShowDialog();
            MessageBox.Show("ソフト再起動後に編集が可能です。");
        }

        private void AppClose_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        //プログラム実行
        private void RunBtn_Click(object sender, RoutedEventArgs e)
        {
            switch (Hensu.Language)
            {
                case "C#":
                    for(int i = 0; i < TabString.Length; ++i)
                    {
                        string Fpath = Hensu.ProjectPath + @"\source\" + TabString[i];
                        System.IO.StreamWriter SaveFcs = new System.IO.StreamWriter(Fpath);
                        SaveFcs.Write(avalons[i].Text);
                        SaveFcs.Close();
                    }
                    ErAndMsgBox.Text=Run.RunCsharp(Hensu.ProjectPath);

                    break;
                case "JavaScript":
                    string path = Hensu.ProjectPath + @"\source\" + TabString[Tab1.SelectedIndex];
                    System.IO.StreamWriter SaveF = new System.IO.StreamWriter(path);
                    SaveF.Write(avalons[Tab1.SelectedIndex].Text);
                    SaveF.Close();
                    Run.RunJavaScript(Hensu.ProjectPath + @"\source\", TabString[Tab1.SelectedIndex]);
                    break;
                case "Python":
                    string pathPY = Hensu.ProjectPath + @"\source\" + TabString[Tab1.SelectedIndex];
                    System.IO.StreamWriter SaveFpy = new System.IO.StreamWriter(pathPY);
                    SaveFpy.Write(avalons[Tab1.SelectedIndex].Text);
                    SaveFpy.Close();
                    Run.RunPython(Hensu.ProjectPath + @"\source\", TabString[Tab1.SelectedIndex]);
                    break;
            }
        }

        private void WebBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process p = System.Diagnostics.Process.Start("http://mogami-soft.floppy.jp/koyominhome.html");
        }
    }
}