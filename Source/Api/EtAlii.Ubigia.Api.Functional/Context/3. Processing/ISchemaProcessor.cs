// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.Collections.Generic;

    public interface ISchemaProcessor
    {
        IAsyncEnumerable<Structure> Process(Schema schema);
    }
}
