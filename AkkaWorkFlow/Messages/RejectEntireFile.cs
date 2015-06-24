namespace AkkaWorkFlow
{
    public class RejectEntireFile
    {
        public string FilePath { get; private set; }
        public string[] ErrorMessages { get; private set; }

        public RejectEntireFile(string filePath, string[] errorMessages)
        {
            FilePath = filePath;
            ErrorMessages = errorMessages;
        }
    }
}