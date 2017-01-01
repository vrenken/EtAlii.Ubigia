namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Threading.Tasks;

    public interface IPathVariableExpander
    {
        PathSubjectPart[] Expand(PathSubjectPart[] path);
    }
}