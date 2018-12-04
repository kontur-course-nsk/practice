using System.Collections.Generic;

namespace Parser
{
    using Parser.Generator.Files;

    public interface IDirectoryDataParser
    {
        IEnumerable<Person> GetPeople(KonturDirectory directory);
    }
}
