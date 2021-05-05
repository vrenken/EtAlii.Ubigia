namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Threading.Tasks;

    public interface IVariablePathSubjectPartToPathConverter
    {
        Task<PathSubjectPart[]> Convert(ScopeVariable variable);
    }
}
