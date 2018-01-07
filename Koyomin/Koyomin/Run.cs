using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Koyomin
{
    class Run
    {
        public static string RunJavaScript(string path ,string filename)
        {
            switch (Hensu.ProjectKind)
            {
                case "BasicApp":
                    System.IO.StreamWriter MakeBat = new System.IO.StreamWriter(path + @"\run.bat");
                    MakeBat.WriteLine("@ECHO OFF");
                    MakeBat.WriteLine("CScript " + path + filename);
                    MakeBat.WriteLine("PAUSE");
                    MakeBat.Close();
                    System.Diagnostics.Process p = System.Diagnostics.Process.Start(path + @"run.bat");
                    break;
                case "WebPage":
                    System.Diagnostics.Process.Start(path + filename);
                    break;
            }
            

            return "";
        }
        public static string RunPython(string path,string filename)
        {
            System.IO.StreamWriter MakeBat = new System.IO.StreamWriter(path + @"\run.bat");
            MakeBat.WriteLine("@ECHO OFF");
            MakeBat.WriteLine("python " + path + filename);
            MakeBat.WriteLine("PAUSE");
            MakeBat.Close();
            System.Diagnostics.Process p = System.Diagnostics.Process.Start(path +@"run.bat");

            return "";
        }
        public static string RunCsharp(string path)
        {
            //path は他とは違い\sourceを含まない
            string Msg = "";
            switch (Hensu.ProjectKind)
            {
                case "WindowsConsole":
                    Build.consoleBuild(checkUsing.loadUsing());
                    Msg = Build.MSbuild();
                    System.IO.StreamWriter MakeBat = new System.IO.StreamWriter(path + @"\Build\run.bat");
                    MakeBat.WriteLine("@ECHO OFF");
                    MakeBat.WriteLine(path + @"\Build\main.exe");
                    MakeBat.WriteLine("PAUSE");
                    MakeBat.Close();
                    System.Diagnostics.Process p = System.Diagnostics.Process.Start(path + @"\Build\run.bat");
                    break;
                case "WPF":
                    Build.wpfbuild(checkUsing.loadUsing());
                    Msg = Build.MSbuild();
                    if (System.IO.File.Exists(path + @"\Build\main.exe"))
                    {
                        System.Diagnostics.Process p1 = System.Diagnostics.Process.Start(path + @"\Build\main.exe");
                    }
                    break;
            }
            return Msg;
        }
    }
}
