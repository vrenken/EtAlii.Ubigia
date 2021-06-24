// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.CodeDom.Compiler;

    public interface IHeaderWriter
    {
        void Write(IndentedTextWriter writer, string fileName);
    }
}
