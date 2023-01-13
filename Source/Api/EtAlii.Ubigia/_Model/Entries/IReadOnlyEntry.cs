// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia;

public interface IReadOnlyEntry
{
    Identifier Id{ get; }

    Relation Parent { get; }
    Relation[] Children { get; }

    Relation Parent2 { get; }
    Relation[] Children2 { get; }

    Relation Previous { get; }
    Relation Next{ get; }

    Relation Downdate { get; }
    Relation[] Updates { get; }

    Relation Indexed { get; }
    Relation[] Indexes { get; }

    string Type { get; }
    string Tag { get; }
}
