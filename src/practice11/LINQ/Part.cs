using System;

namespace LINQ
{
    using System.Collections.Generic;

    public class Part
    {
        public Part(Guid id, string code, string name, Guid modelId, List<string> crossCodes)
        {
            Id = id;
            Code = code;
            Name = name;
            ModelId = modelId;
            CrossCodes = crossCodes;
        }

        public Guid Id { get; }

        public string Code { get; }

        public string Name { get; }

        public Guid ModelId { get; }

        public List<string> CrossCodes { get; }
    }
}
