namespace Parser
{
    using Parser.Generator.Files;
    using System.Collections.Generic;

    public interface IFileDataParser
    {
        IEnumerable<Person> GetPeople(KonturFile file);
    }
}
