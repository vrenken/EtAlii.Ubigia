// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System.Collections.Generic;

    public interface IReadOnlyRelationsComponentCollection<out TRelationsComponent> : IReadOnlyCollection<TRelationsComponent>
        where TRelationsComponent : RelationsComponent, new()
    {
        bool Contains(Identifier id);
    }
}
