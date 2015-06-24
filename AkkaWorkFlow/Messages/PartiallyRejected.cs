namespace AkkaWorkFlow
{

    public class PartiallyRejected : ParseResults
    {
        public BadBits BadBits { get; private set; }

        public PartiallyRejected(string fullFilePath, BadBits badBits)
            : base(fullFilePath)
        {
            BadBits = badBits;
        }
    }
}