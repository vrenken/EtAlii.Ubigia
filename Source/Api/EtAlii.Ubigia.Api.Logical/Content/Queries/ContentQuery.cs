// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.Diagnostics;

    [DebuggerStepThrough]
    public class ContentQuery
    {
        public readonly Identifier Identifier;

        public ContentQuery(in Identifier identifier)
        {
            Identifier = identifier;
        }
    }
}
