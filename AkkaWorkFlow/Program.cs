using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using AkkaWorkFlow.Config;
using SimpleConfig;

namespace AkkaWorkFlow
{
    class Program
    {

        public static AceFxUploadSettings AceFxUploadSettings;
        static void Main(string[] args)
        {
            AceFxUploadSettings = Configuration.Load<AceFxUploadSettings>();

            Console.ReadLine();
        }
    }
}
