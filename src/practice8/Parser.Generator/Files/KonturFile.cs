namespace Parser.Generator.Files
{
    using System.IO;

    public abstract class KonturFile
    {
        public KonturFile()
        {
            FileName = Path.GetRandomFileName();
        }

        public string FileName { get; }

        public abstract Stream Open { get; }
    }
}