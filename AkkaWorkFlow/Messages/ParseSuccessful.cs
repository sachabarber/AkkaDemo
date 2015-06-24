namespace AkkaWorkFlow
{
    public class ParseSuccessful : ParseResults
    {
        public GoodBits GoodBits { get; private set; }

        public ParseSuccessful(string fullFilePath, GoodBits goodBits) : base(fullFilePath)
        {
            GoodBits = goodBits;
        }
    }
}