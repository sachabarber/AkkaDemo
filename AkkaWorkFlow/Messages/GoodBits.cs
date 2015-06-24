using System.Collections.Generic;
using System.Xml.Linq;

namespace AkkaWorkFlow
{
    public class GoodBits
    {
        public IEnumerable<XElement> GoodSections { get; private set; }

        public GoodBits(IEnumerable<XElement> goodSections)
        {
            GoodSections = goodSections;
        }
    }
}