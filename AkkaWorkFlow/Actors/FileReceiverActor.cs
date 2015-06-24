using System;
using System.Collections.Generic;
using Akka.Actor;
using AkkaWorkFlow.Config;

namespace AkkaWorkFlow
{
    public class FileReceiverActor : ReceiveActor
    {
        private FileReceiverActor(AceFxUploadSettings settings)
        {
            Receive<ProcessFile>(x => Context.ActorOf(FileRequestSupervisorActor.Create(x, settings)));
        }

        public static Props Create(AceFxUploadSettings settings)
        {
            return Props.Create(() => new FileReceiverActor(settings));
        }
    }
}
