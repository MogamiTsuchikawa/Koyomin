using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Koyomin
{
    class Build
    {
        public static void wpfbuild(string Usings)
        {
            var projpath = Hensu.ProjectPath;
            var dfiles = Directory.GetFiles(projpath + @"\build");
            int i = 0;
            while (dfiles.Length != i)
            {
                System.IO.File.Delete(dfiles[i]);
                ++i;
            }
            System.IO.StreamReader fsr = new System.IO.StreamReader(
                @"AppData\res\wpf\proj.txt");
            string pp = fsr.ReadToEnd();
            fsr.Close();
            
            System.IO.StreamWriter proj = new System.IO.StreamWriter(
                Hensu.ProjectPath+@"\build\project.csproj",
                false);
            proj.WriteLine(pp);
            //ここにアイコンやdllの追加動作を記述
            //ico file 
            if (System.IO.File.Exists(Hensu.ProjectPath + @"\source\AppIco.ico"))
            {
                System.IO.File.Copy(Hensu.ProjectPath+@"\source\AppIco.ico", Hensu.ProjectPath+@"\build\AppIco.ico", true);
                proj.WriteLine("\t<PropertyGroup>");
                proj.WriteLine("\t\t<ApplicationIcon>AppIco.ico</ApplicationIcon>");
                proj.WriteLine("\t</PropertyGroup>");
                proj.WriteLine("\t<ItemGroup>");
                proj.WriteLine("\t\t<Resource Include=\"AppIco.ico\" />");
                proj.WriteLine("\t</ItemGroup>");
            }

            proj.WriteLine("\t<ItemGroup>");
            proj.WriteLine("\t\t<ApplicationDefinition Include=\"app.xaml\" />");
            var files = Directory.GetFiles(projpath + @"\source");

            System.IO.File.Copy(Hensu.ProjectPath + @"\source\app.xaml"
                            , Hensu.ProjectPath + @"\build\app.xaml");
            i = 0;
            while(i != files.Length)
            {
                switch (System.IO.Path.GetExtension(files[i]))
                {
                    case ".cs":
                        proj.WriteLine("\t\t"+@"<Compile Include="""+ System.IO.Path.GetFileName(files[i])+@""" />");
                        System.IO.File.Copy(Hensu.ProjectPath+@"\source\" + System.IO.Path.GetFileName(files[i])
                            , Hensu.ProjectPath+ @"\build\" + System.IO.Path.GetFileName(files[i]));
                        break;
                    case ".xaml":
                        if (System.IO.Path.GetFileName(files[i])!= "app.xaml")
                        {
                            proj.WriteLine("\t\t"+@"<Page Include=""" + System.IO.Path.GetFileName(files[i]) + @""" />");
                            System.IO.File.Copy(Hensu.ProjectPath + @"\source\" + System.IO.Path.GetFileName(files[i])
                                , Hensu.ProjectPath + @"\build\" + System.IO.Path.GetFileName(files[i]));
                        }
                        break;
                }
                ++i;
            }
            //proj.WriteLine("\t\t" + @"<Reference Include=""System"" />");
            proj.WriteLine("\t\t" + @"<Reference Include=""System.Xaml"" />");
            proj.WriteLine("\t\t" + @"<Reference Include=""WindowsBase"" />");
            proj.WriteLine("\t\t" + @"<Reference Include=""PresentationCore"" />");
            proj.WriteLine("\t\t" + @"<Reference Include=""PresentationFramework"" />");
            string[] UsingsAr = Usings.Split('*');
            i = 0;
            while(UsingsAr.Length != i)
            {
                proj.WriteLine("\t\t" + @"<Reference Include="""+UsingsAr[i]+ @""" /> ");
                ++i;
            }
            proj.WriteLine("\t" + @"</ItemGroup>");
            proj.WriteLine("\t" + @"<Import Project=""$(MSBuildBinPath)\Microsoft.CSharp.targets"" />");
            proj.WriteLine(@"</Project>");
            proj.WriteLine(@"");
            proj.WriteLine(@"");
            proj.Close();
        }
        //***********************************************************************************************************************************************
        public static void consoleBuild(string Usings)
        {
            var projpath = Hensu.ProjectPath;
            var dfiles = Directory.GetFiles(projpath + @"\build");
            int i = 0;
            while (dfiles.Length != i)
            {
                System.IO.File.Delete(dfiles[i]);
                ++i;
            }
            System.IO.StreamReader fsr = new System.IO.StreamReader(
                @"AppData\res\console\proj.txt");
            string pp = fsr.ReadToEnd();
            fsr.Close();
            System.IO.StreamWriter proj = new System.IO.StreamWriter(
                Hensu.ProjectPath + @"\build\project.csproj",
                false);
            proj.WriteLine(pp);
            //ここにアイコンやdllの追加動作を記述
            //ico file 
            if (System.IO.File.Exists(Hensu.ProjectPath + @"\source\AppIco.ico"))
            {
                System.IO.File.Copy(Hensu.ProjectPath + @"\source\AppIco.ico", Hensu.ProjectPath + @"\build\AppIco.ico", true);
                proj.WriteLine("\t<PropertyGroup>");
                proj.WriteLine("\t\t<ApplicationIcon>AppIco.ico</ApplicationIcon>");
                proj.WriteLine("\t</PropertyGroup>");
                proj.WriteLine("\t<ItemGroup>");
                proj.WriteLine("\t\t<Resource Include=\"AppIco.ico\" />");
                proj.WriteLine("\t</ItemGroup>");
            }

            proj.WriteLine("\t<ItemGroup>");
            var files = Directory.GetFiles(projpath + @"\source");
            i = 0;
            while (i != files.Length)
            {
                switch (System.IO.Path.GetExtension(files[i]))
                {
                    case ".cs":
                        proj.WriteLine("\t\t" + @"<Compile Include=""" + System.IO.Path.GetFileName(files[i]) + @""" />");
                        System.IO.File.Copy(Hensu.ProjectPath + @"\source\" + System.IO.Path.GetFileName(files[i])
                            , Hensu.ProjectPath + @"\build\" + System.IO.Path.GetFileName(files[i]));
                        break;
                }
                ++i;
            }
            string[] UsingsAr = Usings.Split('*');
            i = 0;
            while (UsingsAr.Length != i)
            {
                proj.WriteLine("\t\t" + @"<Reference Include=""" + UsingsAr[i] + @""" /> ");
                ++i;
            }
            proj.WriteLine("\t" + @"</ItemGroup>");
            proj.WriteLine("\t" + @"<Import Project=""$(MSBuildBinPath)\Microsoft.CSharp.targets"" />");
            proj.WriteLine(@"</Project>");
            proj.WriteLine(@"");
            proj.WriteLine(@"");
            proj.Close();
        }
        //***********************************************************************************************************************************************
        public static void formsBuild(string Usings)
        {
            var projpath = Hensu.ProjectPath;
            System.IO.StreamReader fsr = new System.IO.StreamReader(
                @"AppData\res\form\proj.txt");
            string pp = fsr.ReadToEnd();
            fsr.Close();
            System.IO.StreamWriter proj = new System.IO.StreamWriter(
                Hensu.ProjectPath + @"\build\project.csproj",
                false);
            proj.WriteLine(pp);
            //ここにアイコンやdllの追加動作を記述
            //ico file 
            if (System.IO.File.Exists(Hensu.ProjectPath + @"\source\AppIco.ico"))
            {
                System.IO.File.Copy(Hensu.ProjectPath + @"\source\AppIco.ico", Hensu.ProjectPath + @"\build\AppIco.ico", true);
                proj.WriteLine("\t<PropertyGroup>");
                proj.WriteLine("\t\t<ApplicationIcon>AppIco.ico</ApplicationIcon>");
                proj.WriteLine("\t</PropertyGroup>");
                proj.WriteLine("\t<ItemGroup>");
                proj.WriteLine("\t\t<Resource Include=\"AppIco.ico\" />");
                proj.WriteLine("\t</ItemGroup>");
            }

            proj.WriteLine("\t<ItemGroup>");
            var files = Directory.GetFiles(projpath + @"\build");
            int i = 0;
            while (i != files.Length)
            {
                switch (System.IO.Path.GetExtension(files[i]))
                {
                    case ".cs":
                        proj.WriteLine("\t\t" + @"<Compile Include=""" + System.IO.Path.GetFileName(files[i]) + @""" />");
                        break;
                }
                ++i;
            }
            string[] UsingsAr = Usings.Split('*');
            i = 0;
            while (UsingsAr.Length != i)
            {
                proj.WriteLine("\t\t" + @"<Reference Include=""" + UsingsAr[i] + @""" /> ");
                ++i;
            }
            proj.WriteLine("\t" + @"</ItemGroup>");
            proj.WriteLine("\t" + @"<Import Project=""$(MSBuildBinPath)\Microsoft.CSharp.targets"" />");
            proj.WriteLine(@"</Project>");
            proj.WriteLine(@"");
            proj.WriteLine(@"");
            proj.Close();
        }
        //***********************************************************************************************************************************************
        public static string MSbuild()
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(
                @"now.txt",
                false);
            //TextBox1.Textの内容を書き込む
            sw.Write(Hensu.ProjectPath);
            //閉じる
            sw.Close();
            //Processオブジェクトを作成
            System.Diagnostics.Process p = new System.Diagnostics.Process();

            //ComSpec(cmd.exe)のパスを取得して、FileNameプロパティに指定
            p.StartInfo.FileName = System.Environment.GetEnvironmentVariable("ComSpec");
            //出力を読み取れるようにする
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = false;
            //ウィンドウを表示しないようにする
            p.StartInfo.CreateNoWindow = true;
            //コマンドラインを指定（"/c"は実行後閉じるために必要）
            p.StartInfo.Arguments = @"/c cd /d" + Hensu.ProjectPath + @"\build" + @"& & C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe /clp:ErrorsOnly";

            //起動
            p.Start();

            //出力を読み取る
            string results = p.StandardOutput.ReadToEnd();

            //プロセス終了まで待機する
            //WaitForExitはReadToEndの後である必要がある
            //(親プロセス、子プロセスでブロック防止のため)
            p.WaitForExit();
            p.Close();

            //出力された結果を表示
            return results;
        }
    }
}
