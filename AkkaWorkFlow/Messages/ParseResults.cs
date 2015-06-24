namespace AkkaWorkFlow
{
    public class ParseResults
    {
        public string FullFilePath { get; private set; }
        
        public ParseResults(string fullFilePath)
        {
            FullFilePath = fullFilePath;
        }
    }
}