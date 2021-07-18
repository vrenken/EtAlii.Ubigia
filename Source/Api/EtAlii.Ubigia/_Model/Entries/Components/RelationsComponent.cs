// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System;
    using System.Collections.Generic;

    public abstract class RelationsComponent : CompositeComponent
    {
        public IEnumerable<Relation> Relations { get; internal set; } = Array.Empty<Relation>();
    }
}
