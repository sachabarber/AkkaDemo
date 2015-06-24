using Akka.Actor;

namespace AkkaWorkFlow
{
    public class FileParserActor : ReceiveActor
    {
        private FileParserActor(FileIO io, ParseFile file)
        {
            Context.Parent.Tell(file.ParseUsing(io));
        }

        public static Props Create(FileIO io, ParseFile file)
        {
            return Props.Create(() => new FileParserActor(io, file));
        }
    }
}