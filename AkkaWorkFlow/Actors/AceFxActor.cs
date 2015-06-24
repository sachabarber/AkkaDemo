using System;
using System.Linq.Expressions;
using Akka.Actor;
using AkkaWorkFlow.Config;
using Bender.Collections;

namespace AkkaWorkFlow
{
    public class AceFxActor : ReceiveActor
    {
        private AceFxActor(AceFxUploadSettings settings)
        {
            var fileReceiver = Context.ActorOf(FileReceiverActor.Create(settings), "File Receiver");
            Context.ActorOf(DirectoryScannerActor.Create(new DirectoryIO(settings.FtpPath), fileReceiver), "Directory Scanner");
        }

        public static Props Create(AceFxUploadSettings settings)
        {
            return Props.Create(() => new AceFxActor(settings));
        }
    }
}