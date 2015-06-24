using System.Linq;
using Akka.Actor;

namespace AkkaWorkFlow
{
    public class DirectoryScannerActor : ReceiveActor
    {
        public DirectoryScannerActor(DirectoryIO io, IActorRef receiver)
        {
            foreach( var m in io.GetExistingFiles().Select(x => new ProcessFile(x)))
                receiver.Tell(m);
        }

        public static Props Create(DirectoryIO io, IActorRef receiver)
        {

            return Props.Create(() => new DirectoryScannerActor(io, receiver));
        }
    }
}