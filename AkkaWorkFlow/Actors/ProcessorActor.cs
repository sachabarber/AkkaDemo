using System;
using System.Collections.Generic;
using Akka.Actor;
using AkkaWorkFlow.Config;

namespace AkkaWorkFlow
{
    //TODO 
    public class ProcessorActor : ReceiveActor
    {

        public ProcessorActor(ProcessGoodBits processGoodBits)
        {
            Console.WriteLine("Processed  {0}. GoodBits: {1}.", processGoodBits.FilePath,
                    string.Join(" | ", processGoodBits.GoodBits.GoodSections.ToString()));
            Sender.Tell(new Processed());
        }

        public static Props Create(ProcessGoodBits processGoodBits)
        {
            return Props.Create(() => new ProcessorActor(processGoodBits));
        }

        
    }
}
