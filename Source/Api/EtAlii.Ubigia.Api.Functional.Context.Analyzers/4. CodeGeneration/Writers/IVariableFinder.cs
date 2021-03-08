namespace EtAlii.Ubigia.Api.Functional.Context.Analyzers
{
    public interface IVariableFinder
    {
        string[] FindVariables(Schema schema);
    }
}
