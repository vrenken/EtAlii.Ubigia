// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.CodeDom.Compiler;

    public class HeaderWriter : IHeaderWriter
    {
        public void Write(IndentedTextWriter writer, string fileName)
        {
            writer.WriteLine($"// Remark: this file was auto-generated based on '{fileName}'.");
            writer.WriteLine("// Any changes will be overwritten the next time the file is generated.");
        }
    }
}
