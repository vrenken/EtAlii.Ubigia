﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.Diagnostics;

    [DebuggerStepThrough]
    [DebuggerDisplay("{" + nameof(Name) + "}")]
    public class GraphNode : GraphPathPart
    {
        public readonly string Name;

        public GraphNode(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}