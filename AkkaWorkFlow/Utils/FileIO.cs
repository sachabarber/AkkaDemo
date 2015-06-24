using System.IO;

namespace AkkaWorkFlow
{
    public class FileIO
    {
        private readonly string filePath;

        public FileIO(string filePath)
        {
            this.filePath = filePath;
        }

        public virtual string ReadFile()
        {
            return File.ReadAllText(filePath);
        }
    }
}