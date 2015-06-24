using System;
using System.Linq;
using Akka.Actor;

namespace AkkaWorkFlow
{
    public class RejectorActor : ReceiveActor
    {
        public RejectorActor()
        {
            Receive<RejectEntireFile>(x =>
            {
                Console.WriteLine("Rejecting {0}. Errors: {1}.", x.FilePath, string.Join(" | ", x.ErrorMessages));
                Sender.Tell(new EntirelyRejected());
            });

            Receive<RejectBadBits>(x =>
            {
                Console.WriteLine("Partially rejecting {0}. Errors: {1}.", x.FilePath, string.Join(" | ", 
                    x.BadBits.BadSections.SelectMany(y => y.ErrorMessages)));
                Sender.Tell(new PartiallyRejected(x.FilePath,x.BadBits));
            });
        }

        public static Props Create()
        {
            return Props.Create<RejectorActor>();
        }

        protected override void PreRestart(Exception reason, object message)
        {
            base.PreRestart(reason, message);
            Self.Tell(message);
        }
    }
}