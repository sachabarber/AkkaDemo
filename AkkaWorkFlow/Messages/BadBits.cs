using System.Collections.Generic;

namespace AkkaWorkFlow
{
    public class BadBits
    {
        public IEnumerable<BadBit> BadSections { get; private set; }

        public BadBits(IEnumerable<BadBit> badSections)
        {
            BadSections = badSections;
        }
    }
}