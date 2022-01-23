using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inputoutput2
{
    internal static class Utility
    {
        //hvis man har noget static data som alle klasser skal kunne se.
        //static dvs. noget som ikke skal ændres
        public static string UseFolderUrl
        {
            get
            {
                //Environment er min egen pc.
                return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            }
        }

        public static string ParentFolderUrl
        {
            get
            {
                //Path.Combine samler en du laver nu og en forgående. userFolderUrl\H1GP
                return Path.Combine(UseFolderUrl, "H1GP");
            }
        }
    }
    internal static class IOHandler
    {
        private static string _userFolderUrl;

        private static string _parentFolderUrl;
        private static FileSystemWatcher Watcher { get; set; }

        static IOHandler()
        {
            _userFolderUrl = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            _parentFolderUrl = Path.Combine(_userFolderUrl, "H1GP");

            Watcher = new FileSystemWatcher(_parentFolderUrl);
            Watcher.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime
                                    | NotifyFilters.DirectoryName
                                    | NotifyFilters.FileName
                                    | NotifyFilters.LastAccess
                                    | NotifyFilters.LastWrite;
            Watcher.Changed += Watcher_Changed;
            Watcher.Filter = "*.txt";
            Watcher.IncludeSubdirectories = true;
            Watcher.EnableRaisingEvents = true;
        }
        //den sender til using var Watcher
        private static void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }
            Console.WriteLine($"Changed: {e.FullPath}");
        }  
    }
}
