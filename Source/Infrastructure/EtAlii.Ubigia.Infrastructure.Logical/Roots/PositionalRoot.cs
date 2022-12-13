// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical
{
    public static class PositionalRoot
    {
        public static readonly RootTemplate Head = new("Head", new RootType("Head"));
        public static readonly RootTemplate Tail = new("Tail", new RootType("Tail"));
    }
}
