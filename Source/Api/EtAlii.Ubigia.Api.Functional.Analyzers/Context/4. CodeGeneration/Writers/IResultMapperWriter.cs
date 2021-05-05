namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.CodeDom.Compiler;
    using Serilog;

    public interface IResultMapperWriter
    {
        void Write(ILogger logger, IndentedTextWriter writer, StructureFragment structureFragment, bool isRoot);
    }
}
