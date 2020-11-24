﻿namespace EtAlii.Ubigia
{
    using System;
    using System.Collections.Generic;

    public abstract class RelationsComponent : CompositeComponent
    {
        internal RelationsComponent()
        { 
        }

        public IEnumerable<Relation> Relations { get; internal set; } = Array.Empty<Relation>();
    }
}
