using System.Xml.Linq;

namespace AkkaWorkFlow
{
    public class BadBit
    {
        public XElement XElement { get; private set; }
        public string[] ErrorMessages { get; private set; }

        public BadBit(XElement xElement, string[] errorMessages)
        {
            XElement = xElement;
            ErrorMessages = errorMessages;
        }
    }
}