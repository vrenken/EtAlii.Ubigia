namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.CodeDom.Compiler;

    public interface IHeaderWriter
    {
        void Write(IndentedTextWriter writer, string fileName);
    }
}
