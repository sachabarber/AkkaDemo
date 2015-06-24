namespace AkkaWorkFlow
{
    public class FileSchemaInvalid : ParseResults
    {
        public string[] ErrorMessages { get; private set; }

        public FileSchemaInvalid(string fullFilePath, string[] errorMessages) : base(fullFilePath)
        {
            ErrorMessages = errorMessages;
        }
    }
}