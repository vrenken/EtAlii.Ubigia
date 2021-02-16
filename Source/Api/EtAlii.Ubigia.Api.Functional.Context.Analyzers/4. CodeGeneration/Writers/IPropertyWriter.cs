// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context.Analyzers
{
    using System.CodeDom.Compiler;
    using Serilog;

    public interface IPropertyWriter
    {
        void Write(ILogger logger, IndentedTextWriter writer, ValueFragment valueFragment);
    }
}
