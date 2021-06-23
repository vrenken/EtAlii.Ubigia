// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    public abstract class RelationComponent : NonCompositeComponent
    {
        public Relation Relation { get; internal set; }
    }
}
