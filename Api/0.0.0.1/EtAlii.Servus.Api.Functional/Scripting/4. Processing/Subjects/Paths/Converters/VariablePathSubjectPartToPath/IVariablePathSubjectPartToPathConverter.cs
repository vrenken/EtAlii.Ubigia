namespace EtAlii.Servus.Api.Functional
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IVariablePathSubjectPartToPathConverter
    {
        PathSubjectPart[] Convert(ScopeVariable variable);
    }
}