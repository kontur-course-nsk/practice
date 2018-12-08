namespace Parser.Generator.Files
{
    using System.Collections.Generic;
    using System.Linq;

    public class KonturDirectory
    {
        private KonturDirectory()
        {

        }

        public IEnumerable<RemoteDataFile> Files => Enumerable.Range(0, 100).Select(o => new RemoteDataFile());

        public static KonturDirectory Dicrectory => new KonturDirectory();
    }
}
