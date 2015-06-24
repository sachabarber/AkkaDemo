namespace AkkaWorkFlow
{
    public class ProcessFile
    {
        public string FilePath { get; private set; }

        public ProcessFile(string filePath)
        {
            FilePath = filePath;
        }
    }
}