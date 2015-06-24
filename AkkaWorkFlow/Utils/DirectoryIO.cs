using System.IO;
using System.Linq;

namespace AkkaWorkFlow
{
    public class DirectoryIO
    {
        private readonly string directoryPath;

        public DirectoryIO(string directoryPath)
        {
            this.directoryPath = directoryPath;
        }

        public virtual string[] GetExistingFiles()
        {
            return Directory.GetFiles(directoryPath).ToArray();
        }
    }
}