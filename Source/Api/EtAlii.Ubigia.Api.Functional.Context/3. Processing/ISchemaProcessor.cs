namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.Collections.Generic;

    public interface ISchemaProcessor
    {
        IAsyncEnumerable<Structure> Process(Schema schema);
    }
}
