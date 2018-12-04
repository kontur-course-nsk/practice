namespace Parser.Generator.Files
{
    using System.IO;

    public class RemoteDataFile : KonturFile
    {
        public override Stream Open => DataGenerator.GenerateSlow(2000);
    }
}
