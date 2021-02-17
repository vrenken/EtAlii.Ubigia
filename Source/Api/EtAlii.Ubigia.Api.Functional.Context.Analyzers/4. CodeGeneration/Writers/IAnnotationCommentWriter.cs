namespace EtAlii.Ubigia.Api.Functional.Context.Analyzers
{
    using System.CodeDom.Compiler;
    using Serilog;

    public interface IAnnotationCommentWriter
    {
        void Write(ILogger logger, IndentedTextWriter writer, Annotation annotation);
    }
}
