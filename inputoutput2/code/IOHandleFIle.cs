using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inputoutput2
{
    internal static class IOHandleFIle
    {
        private static string useFolderUrl;
        private static string parentFolderUrl;


        static IOHandleFIle()
        {
            useFolderUrl = Utility.UseFolderUrl;
            parentFolderUrl = Utility.ParentFolderUrl;
            using var watcher = new FileSystemWatcher(parentFolderUrl);
        }
        public static string HandleFileRead(string fileName, CRUDoperation operationType)
        {
            string returnMessage = "";
            try
            {
                string path = Path.Combine(parentFolderUrl,fileName);
                if (!File.Exists(path))
                    Console.WriteLine("The File doesn't exist.");
                if (File.Exists(path))
                {
                    //Hvis jeg bruger using, behøver jeg ikke at lave en kode for, at den skal lukke
                    //Den kode hedder fs.close i dette tilfælde.
                    using (FileStream fs = File.OpenRead(path))
                    {
                        Console.WriteLine(path);
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error. Something went wrong");
            }
            return returnMessage;
        }

        public static string HandleFileCreate(string? fileName, CRUDoperation operationType)
        {
            string returnMessage = "";
            try
            {
                // ? = if() og : = else
                string? fileUrl = fileName != null ? Path.Combine(parentFolderUrl, fileName) : null;
                if (fileUrl != null)
                {
                    File.Create(fileUrl);
                    Console.WriteLine($"{fileUrl} is created.");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error. Couldn't create the file.");
            }
            return returnMessage;
        }

        public static string HandleFileUpdate(string fileName, CRUDoperation operationType)
        {
            string returnMessage = "";
            try
            {
                string path = fileName != null ? Path.Combine(parentFolderUrl, fileName) : null;
                if (!File.Exists(path))
                    Console.WriteLine($"{path} eksistere ikke.");
                //Hvis jeg bruger using, behøver jeg ikke at lave en kode for, at den skal lukke
                //Den kode hedder sw.close i dette tilfælde.
                using (StreamWriter sw = File.AppendText(path))
                {
                    Console.WriteLine($"{path} is updated!");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error. Couldn't Update the file.");
            }
            return returnMessage;
        }

        public static string HandleFileDelete(string fileName, CRUDoperation operationType)
        {
            string returnMessage = "";
            try
            {
                // ? = if() og : = else
                string? fileUrl = fileName != null ? Path.Combine(parentFolderUrl, fileName) : null;
                //string path = Path.Combine(_parentFolderUrl, "Mathias.txt");
                if (!File.Exists(fileUrl))
                    Console.WriteLine($"{fileUrl} eksistere ikke.");
                if (File.Exists(fileUrl))
                {
                    File.Delete(fileUrl);
                    Console.WriteLine($"{fileUrl} is Deleted.");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error. Couldn't delete file");
            }
            return returnMessage;
        }
    }
}


