﻿namespace EtAlii.Servus.Api.Functional.Tests
{
    using EtAlii.Servus.Api.Logical;

    public class NamedObject : Node
    {
        public string Name { get { return GetProperty<string>(); } set { SetProperty<string>(value); } }
        public int Value { get { return GetProperty<int>(); } set { SetProperty<int>(value); } }

        public NamedObject(IReadOnlyEntry entry)
            : base(entry)
        {
        }
    }
}
