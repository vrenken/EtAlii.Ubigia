namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.CodeDom.Compiler;
    using Serilog;

    public interface INamespaceWriter
    {
        void Write(ILogger logger, IndentedTextWriter writer, Schema schema);
    }
}
