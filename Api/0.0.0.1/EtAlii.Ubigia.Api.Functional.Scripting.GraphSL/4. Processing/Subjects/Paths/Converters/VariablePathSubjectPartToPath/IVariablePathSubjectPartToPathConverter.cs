namespace EtAlii.Ubigia.Api.Functional
{
    using System.Threading.Tasks;

    public interface IVariablePathSubjectPartToPathConverter
    {
        Task<PathSubjectPart[]> Convert(ScopeVariable variable);
    }
}