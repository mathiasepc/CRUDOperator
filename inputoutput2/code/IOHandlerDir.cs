using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inputoutput2
{
    internal class IOHandlerDir
    {
        private static string _userFolderUrl;
        private static string _parentFolderUrl;
        static IOHandlerDir()
        {
            _userFolderUrl = Utility.UseFolderUrl;
            _parentFolderUrl = Utility.ParentFolderUrl;
            using var Watcher = new FileSystemWatcher(_parentFolderUrl);
        }

        static void ReadDir(string url)
        {
            try
            { 
                string[] directories = Directory.GetDirectories(url);
                foreach (string directory in directories)
                {
                    Console.WriteLine($"{directory}");
                    string[] subDirectories = Directory.GetDirectories(directory);
                    foreach (string subDirectory in subDirectories)
                    {
                        Console.WriteLine($"{subDirectory}");
                        ReadDir(subDirectory);  
                    }
                    string[] filesNameUrl = Directory.GetFiles(directory);
                    foreach (string file in filesNameUrl)
                    {
                        Console.WriteLine($"{file}");
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error. Couldn't Read the file.");
            }
        }
        public static string HandleDirRead(string? directoryName, CRUDoperation operationType)
        {
            string returnMessage = "";
            string directoryNameUrl = directoryName != null ? Path.Combine(_parentFolderUrl, directoryName) : _parentFolderUrl;
            ReadDir(directoryNameUrl);
            return returnMessage;
        }

        public static string HandleDirCreate(string? directoryName, CRUDoperation operationType)
        {
            string returnMessage = "";
            try
            {
                // ? = if() og : = else
                string directoryNameUrl = directoryName != null ? Path.Combine(_parentFolderUrl, directoryName) : _parentFolderUrl;
                Directory.CreateDirectory(directoryNameUrl);
                if (Directory.Exists(directoryNameUrl)) 
                    Console.WriteLine("Couldn't create the directory. Already exist.");
                Console.WriteLine($"{directoryNameUrl} is created");
            }
            catch (Exception)
            {
                Console.WriteLine("Error. Something went wrong.");
            }
            return returnMessage;
        }

        public static string HandleDirUpdate(string? directoryName, string? newDirectoryPath, CRUDoperation operationType)
        {
            string returnMessage = "";
            try
            {
                // ? = if() og : = else
                string directoryNameUrl = directoryName != null ? Path.Combine(_parentFolderUrl, directoryName) : _parentFolderUrl;
                string? newDirectoryUrl = newDirectoryPath != null ? Path.Combine(_parentFolderUrl, newDirectoryPath) : null;
                if (newDirectoryUrl != null)
                {
                    Directory.Move(directoryNameUrl,newDirectoryUrl);
                    Console.WriteLine("The directory is updated.");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error. Something went wrong.");
            }
            return returnMessage;
        }

        public static string HandleDirDelete(string? directoryName, CRUDoperation operationType)
        {
            string returnMessage = "";
            try
            {
                string directoryNameUrl = directoryName != null ? Path.Combine(_parentFolderUrl, directoryName) : null;
                if (!Directory.Exists(directoryNameUrl))
                    Console.WriteLine("The file doesn't exist");
                Directory.Delete(directoryNameUrl, true);
                Console.WriteLine($"{directoryNameUrl} is deleted.");
            }
            catch (Exception)
            {
                Console.WriteLine("Error. Couldn't find the file.");
            }
            return returnMessage;
        }
    }
}
