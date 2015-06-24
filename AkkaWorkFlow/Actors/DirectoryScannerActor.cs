using System;
using System.Linq;
using System.Threading;
using Akka.Actor;
using Akka.Event;


namespace AkkWorkflow
{
    public class DirectoryScannerActor : ReceiveActor
    {

        private FileObserver fileObserver = null;
        private readonly ILoggingAdapter log = Context.GetLogger();

        public DirectoryScannerActor(DirectoryIO io, IActorRef fileReceiverActor)
        {
            foreach( var m in io.GetExistingFiles().Select(x => new ProcessFile(x)))
                fileReceiverActor.Tell(m);


            fileObserver = new FileObserver(Self,fileReceiverActor);

            //throw new Exception("dfasbasdbjkdasj");


            Receive<Exception>(x => throwUp(x));


            

        }


        private void throwUp(Exception e)
        {
            Console.WriteLine("throwUp");
            throw e;
        }


        public static Props Create(DirectoryIO io, IActorRef fileReceiverActor)
        {

            return Props.Create(() => new DirectoryScannerActor(io, fileReceiverActor));
        }

        protected override void PreRestart(Exception reason, object message)
        {
            base.PreRestart(reason, message);
            if (fileObserver != null)
            {
                fileObserver.Dispose();
            }
        }
    }
}