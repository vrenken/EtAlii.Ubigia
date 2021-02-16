namespace EtAlii.Ubigia.Api.Functional.Context.Analyzers
{
    using System.CodeDom.Compiler;
    using Serilog;

    public interface IClassWriter
    {
        void Write(ILogger logger, IndentedTextWriter writer, StructureFragment structureFragment);
    }
}
