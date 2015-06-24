using AkkaWorkFlow.Config;

namespace AkkaWorkFlow
{
    public class ParseFile
    {
        public string FullFilePath { get; private set; }
        public AceFxUploadSettings Settings { get; private set; }

        public ParseFile(string fullFilePath, AceFxUploadSettings settings)
        {
            FullFilePath = fullFilePath;
            Settings = settings;
        }

        public virtual ParseResults ParseUsing(FileIO file)
        {

            //TODO : We should do the actual parsing here where we return one of the following
            //1. FileSchemaInvalid
            //2. DataPartiallyValid
            //3. ParseSuccessful


            return new ParseResults(FullFilePath);
        }
    }
}