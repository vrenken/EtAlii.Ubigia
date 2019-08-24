namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System.Threading.Tasks;

    public interface IVariablePathSubjectPartToPathConverter
    {
        Task<PathSubjectPart[]> Convert(ScopeVariable variable);
    }
}