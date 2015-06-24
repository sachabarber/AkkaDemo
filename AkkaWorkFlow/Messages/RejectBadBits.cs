namespace AkkaWorkFlow
{
    public class RejectBadBits
    {
        public string FilePath { get; private set; }
        public BadBits BadBits { get; private set; }

        public RejectBadBits(string filePath, BadBits badBits)
        {
            FilePath = filePath;
            BadBits = badBits;
        }
    }
}