// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    internal partial class GraphContext
    {
        /// <inheritdoc />
        public async Task<TResult> ProcessSingle<TResult>(string text, IResultMapper<TResult> resultMapper, ISchemaScope scope)
        {
            return await ProcessMultiple(text, resultMapper, scope)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async IAsyncEnumerable<TResult> ProcessMultiple<TResult>(string text, IResultMapper<TResult> resultMapper, ISchemaScope scope)
        {
            var items = Process(text, scope).ConfigureAwait(false);

            await foreach (var item in items)
            {
                yield return resultMapper.Map(item);
            }
        }
    }
}
