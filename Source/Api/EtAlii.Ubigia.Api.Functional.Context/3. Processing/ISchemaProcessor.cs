namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISchemaProcessor
    {
        IAsyncEnumerable<Structure> Process(Schema schema);

        IAsyncEnumerable<TResult> ProcessMultiple<TResult>(Schema schema);
        Task<TResult> ProcessSingle<TResult>(Schema schema);
    }
}
