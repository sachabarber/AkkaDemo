namespace AkkaWorkFlow
{
    public class DataPartiallyValid : ParseResults
    {
        public GoodBits GoodBits { get; private set; }
        public BadBits BadBits { get; private set; }

        public DataPartiallyValid(string fullFilePath, GoodBits goodBits, BadBits badBits) : base(fullFilePath)
        {
            GoodBits = goodBits;
            BadBits = badBits;
        }
    }
}