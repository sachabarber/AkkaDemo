namespace AkkaWorkFlow
{
    public class ProcessGoodBits
    {
        public string FilePath { get; private set; }
        public GoodBits GoodBits { get; private set; }

        public ProcessGoodBits(string filePath, GoodBits goodBits)
        {
            FilePath = filePath;
            GoodBits = goodBits;
        }
    }
}