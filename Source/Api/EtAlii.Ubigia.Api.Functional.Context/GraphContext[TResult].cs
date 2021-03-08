namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    internal partial class GraphContext
    {
        public async Task<TResult> ProcessSingle<TResult>(string text, IResultMapper<TResult> resultMapper, ISchemaScope scope)
        {
            return await ProcessMultiple(text, resultMapper, scope)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
        }

        public async IAsyncEnumerable<TResult> ProcessMultiple<TResult>(string text, IResultMapper<TResult> resultMapper, ISchemaScope scope)
        {
            var items = Process(text).ConfigureAwait(false);

            await foreach (var item in items)
            {
                yield return resultMapper.Map(item);
            }
        }
    }
}
