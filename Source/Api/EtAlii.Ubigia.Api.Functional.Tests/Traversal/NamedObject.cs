// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using EtAlii.Ubigia.Api.Logical;

    public class NamedObject : Node
    {
        public string Name { get => GetProperty<string>(); set => SetProperty(value); }
        public int Value { get => GetProperty<int>(); set => SetProperty(value); }

        public NamedObject(IReadOnlyEntry entry)
            : base(entry)
        {
        }
    }
}
