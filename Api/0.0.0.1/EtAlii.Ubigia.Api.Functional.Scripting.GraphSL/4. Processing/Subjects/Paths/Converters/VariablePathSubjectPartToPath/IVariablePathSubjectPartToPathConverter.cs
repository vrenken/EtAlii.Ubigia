namespace EtAlii.Ubigia.Api.Functional
{
    public interface IVariablePathSubjectPartToPathConverter
    {
        PathSubjectPart[] Convert(ScopeVariable variable);
    }
}