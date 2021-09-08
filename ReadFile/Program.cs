using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadFile
{
    class Program
    {
        static void Main(string[] args)
        {

            //var searchTerm = "NameMap";
            //var searchDirectory = new System.IO.DirectoryInfo(@"C:\wamp\www\Eskimo Back Office\Export\");

            //var queryMatchingFiles =
            //        from file in searchDirectory.GetFiles()
            //        where file.Extension == ".txt"
            //        let fileContent = System.IO.File.ReadAllText(file.FullName)
            //        where fileContent.Contains(searchTerm)
            //        select file.FullName;

            //foreach (var fileName in queryMatchingFiles)
            //{
            //    // Do something
            //    Console.WriteLine(fileName);
            //}

            //string path = @"C:\Temp\Test Export\After Blobs removed\";

            string path;

            path = Environment.GetCommandLineArgs()[1];

            if (!path.EndsWith("\\")) { path += "\\"; }

            

            Console.WriteLine("Path: " + path);
           
            bool booSkipping = false;
            string end_tag = string.Empty;
            StringBuilder sb;

            foreach (var file in Directory.GetFiles(path, "*.txt", SearchOption.AllDirectories))
            {
                Console.WriteLine(file);

                if (file == $"{path}ExportLog.txt") { continue; }
                if (file.Contains("\\Queries\\")) 
                { 
                    continue; 
                }

                var contents = File.ReadAllText(file);

                sb = new StringBuilder();

                foreach (string line in contents.Split(new[] { Environment.NewLine },    StringSplitOptions.None))
                {
                    if (line.Trim() == string.Empty)
                    {

                    }
                    else if (line.IndexOf("LayoutCachedHeight =") >= 0)
                    {

                    }
                    else if (line.IndexOf(" Filter =") >= 0)
                    {

                    }
                    //else if (line.IndexOf("TabIndex =") >= 0)
                    //{

                    //}
                    else if (line.IndexOf("Checksum =") >= 0)
                    {

                    }
                    else if (line.IndexOf("ItemSuffix =") >= 0)
                    {

                    }
                    //else if (line.IndexOf("Left =") >= 0)
                    //{

                    //}
                    //else if (line.IndexOf("Right =") >= 0)
                    //{

                    //}
                    //else if (line.IndexOf("Top =") >= 0)
                    //{

                    //}
                    //else if(line.IndexOf("Bottom =") >= 0)
                    //{

                    //}
                    else if (booSkipping ) 
                    {
                        if (line.IndexOf(end_tag) >= 0)
                        {
                            booSkipping = false;
                        }
                    }
                    else if (line.Contains("NameMap = Begin"))
                    {
                        end_tag = "End";
                        booSkipping = true;
                    }
                    else if (line.Contains("RecSrcDt = Begin"))
                    {
                        end_tag = "End";
                        booSkipping = true;
                    }
                    else if (line.Contains("GUID = Begin"))
                    {
                        end_tag = "End";
                        booSkipping = true;
                    }
                    else if (line.Contains("PrtMip = Begin"))
                    {
                        end_tag = "End";
                        booSkipping = true;
                    }
                    else if (line.Contains("PrtDevMode = Begin"))
                    {
                        end_tag = "End";
                        booSkipping = true;
                    }
                    else if (line.Contains("PrtDevModeW = Begin"))
                    {
                        end_tag = "End";
                        booSkipping = true;
                    }
                    else if (line.Contains("PrtDevNames = Begin"))
                    {
                        end_tag = "End";
                        booSkipping = true;
                    }
                    else if (line.Contains("PrtDevNamesW = Begin"))
                    {
                        end_tag = "End";
                        booSkipping = true;
                    }
                    else
                    {
                        sb.AppendLine(line);
                    }

                }

                using (StreamWriter sw = File.CreateText(file))
                {
                    sw.Write( sb.ToString());
                }

            }
        }
    }
}
