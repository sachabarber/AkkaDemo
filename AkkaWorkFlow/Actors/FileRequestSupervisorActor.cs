using Akka.Actor;
using AkkaWorkFlow.Config;

namespace AkkaWorkFlow
{
    public class FileRequestSupervisorActor : ReceiveActor
    {
        private int failures;
        private bool pendingRejection;
        private bool pendingProcessing;

        public FileRequestSupervisorActor(ProcessFile file, AceFxUploadSettings settings)
        {
            Context.ActorOf(FileParserActor.Create(new FileIO(file.FilePath), new ParseFile(file.FilePath, settings)));


            //deal wtih Invalid against Schema
            Receive<FileSchemaInvalid>(x =>
            {
                pendingRejection = true;
                var rejector = Context.ActorOf(RejectorActor.Create(), "rejector");
                rejector.Tell(new RejectEntireFile(x.FullFilePath, x.ErrorMessages));
            });
 

            //deal wtih Invalid data
            Receive<DataPartiallyValid>(x =>
            {
                pendingRejection = true;
                pendingProcessing = true;

                var rejector = Context.ActorOf(RejectorActor.Create(), "rejector");
                rejector.Tell(new RejectBadBits(x.FullFilePath, x.BadBits));

                Context.ActorOf(ProcessorActor.Create(new ProcessGoodBits(x.FullFilePath, x.GoodBits)), "processor");
            });


            //deal wtih Valid file
            Receive<ParseSuccessful>(x =>
            {
                pendingProcessing = true;
                Context.ActorOf(ProcessorActor.Create(new ProcessGoodBits(x.FullFilePath, x.GoodBits)), "processor");
            });




            Receive<EntirelyRejected>(x =>
            {
                pendingRejection = false;
                Check();
            });

            Receive<PartiallyRejected>(x =>
            {
                pendingRejection = false;
                Check();
            });
            
            Receive<Processed>(x =>
            {
                pendingProcessing = false;
                Check();
            });
        }

        private void Check()
        {
            if(!pendingRejection && !pendingProcessing)
                Self.Tell(PoisonPill.Instance);
        }

        public static Props Create(ProcessFile file, AceFxUploadSettings settings)
        {
            return Props.Create(() => new FileRequestSupervisorActor(file, settings));
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(x =>
            {
                failures++;

                if (failures > 3)
                {
                    Self.Tell(PoisonPill.Instance);
                    return Directive.Stop;
                }

                return Directive.Restart;
            });
        }
    }
}