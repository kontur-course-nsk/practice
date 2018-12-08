namespace Parser.Generator.Files
{
    using System.IO;

    public sealed class BigDataFile : KonturFile
    {
        public override Stream Open => DataGenerator.Generate(1000000);
    }
}
