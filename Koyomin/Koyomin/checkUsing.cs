using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Koyomin
{
    class checkUsing
    {
        public static string loadUsing()
        {
            string Usings ="";
            var projpath = Hensu.ProjectPath;
            var files = Directory.GetFiles(projpath + @"\build");
            int i = 0;
            while (files.Length != i)
            {
                if (System.IO.Path.GetExtension(files[i])==".cs")
                {
                    System.IO.StreamReader rf = new System.IO.StreamReader(
                        files[i], System.Text.Encoding.GetEncoding("shift_jis"));
                    while(rf.Peek() > -1)
                    {
                        string temp = "";
                        temp = rf.ReadLine();
                        if(temp.Length > 5)
                        {
                            if (temp.Substring(0, 5) == "using")
                            {
                                temp = temp.Replace("using", "");
                                temp = temp.Replace(";", "");
                                temp = temp.Trim();
                                if (Usings == "")
                                {
                                    Usings = temp;
                                }
                                else
                                {
                                    string[] tempAr = Usings.Split('*');
                                    if (Array.IndexOf(tempAr, temp) == -1)
                                    {
                                        Usings = Usings + "*" + temp;
                                    }
                                }
                            }
                        }
                    }
                    rf.Close();
                }
                ++i;
            }
            return Usings;
        }
    }
}
