// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public static class NamePathFormatter
    {
        public static readonly TypedPathFormatter FirstNameFormatter = new("FIRSTNAME", @"^\p{L}+$");
        public static readonly TypedPathFormatter LastNameFormatter = new("LASTNAME", @"^\p{L}+$");
    }
}
