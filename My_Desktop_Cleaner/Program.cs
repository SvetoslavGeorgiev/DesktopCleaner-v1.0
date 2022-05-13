using System;
using System.IO;
using System.Linq;
using System.Text;

namespace My_Desktop_Cleaner
{
    class Program
    {
        static void Main(string[] args)
        {

            var rootFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //var rootFolderPath = @"D:\download";
            var desktop = Directory.GetFiles(rootFolderPath);


            foreach (var file in desktop)
            {

                var fileInfo = new FileInfo(file);

                var extention = Path.GetExtension(fileInfo.Name);
                if (fileInfo.Name.Contains("~$"))
                {
                    continue;
                }
                var fileToMove = file;
                var destinationPath = string.Empty;
                switch (extention)
                {
                    case ".txt":
                    case ".pdf":
                    case ".docx":
                    case ".doc":
                    case ".xlsx":
                        destinationPath = @"D:/documents/documents From Desctop/" + fileInfo.Name;
                        break;
                    case ".png":
                    case ".PNG":
                    case ".jpg":
                    case ".jpeg":
                        destinationPath = @"D:/pictures/pictures from Desktop/" + fileInfo.Name;
                        break;

                    case ".mp4":
                    case ".mkv":
                    case ".mwv":
                    case ".avi":
                        destinationPath = @"D:/Videos/videos from desktop/" + fileInfo.Name;
                        break; 

                    default:
                        break;
                }
                if (destinationPath != string.Empty)
                {
                    if (File.Exists(destinationPath))
                    {
                        var counter = 0;
                        var sb = new StringBuilder();
                        while (File.Exists(destinationPath))
                        {
                            sb = sb.Clear();
                            counter++;
                            var splitPath = destinationPath.Split("/").ToArray();
                            var name = splitPath[splitPath.Length - 1].Split(".").ToArray();
                            var newName = name[0] + counter.ToString();
                            

                            for (int i = 0; i < splitPath.Length - 1; i++)
                            {
                                sb.Append($"{splitPath[i]}/");
                            }
                            sb.Append($"{newName}.{name[1]}");

                            destinationPath = string.Join("", sb);
                        }

                    }
                    File.Move(fileToMove, destinationPath);
                }
            }
        }
    }
}
