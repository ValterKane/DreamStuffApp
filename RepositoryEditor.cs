using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamStuffApp
{
    public static class RepositoryEditor
    {
        public const string DBFILENAME = "DreamStuffDB.db";

        public static string RESOURCESPATH
        {
            get { return System.IO.Path.Combine(@"Data/", DBFILENAME); }
        }

        public static string DBFILEPATH
        {
            get
            {
                return Path.Combine(FileSystem.AppDataDirectory, DBFILENAME);
            }
        }

        public static void CopyIfNew()
        {
            if (File.Exists(DBFILEPATH))
            {
                Task<Stream> task = FileSystem.OpenAppPackageFileAsync(RESOURCESPATH);
                var stream = task.Result;

                long oldDBFileLength = new FileInfo(DBFILEPATH).Length;
                using MemoryStream memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);

                long newDBFileLength = memoryStream.Length;

                if(new FileInfo(DBFILEPATH).LastWriteTime < new FileInfo(RESOURCESPATH).LastWriteTime)
                {
                    File.WriteAllBytes(DBFILEPATH, memoryStream.ToArray());
                }
 
                
            }
            
        }

        public static void CopyAnyWay()
        {
            Task<Stream> task = FileSystem.OpenAppPackageFileAsync(RESOURCESPATH);
            var stream = task.Result;
            using MemoryStream memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            File.WriteAllBytes(DBFILEPATH, memoryStream.ToArray());
        }

    }
}
