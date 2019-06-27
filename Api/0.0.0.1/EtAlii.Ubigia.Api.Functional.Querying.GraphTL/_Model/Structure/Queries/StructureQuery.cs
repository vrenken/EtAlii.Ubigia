namespace EtAlii.Ubigia.Api.Functional
{
    public class StructureQuery : QueryFragment
    {
        public QueryFragment[] Values { get; }
    
        public StructureQuery (string name, Annotation annotation, Requirement requirement, QueryFragment[] values)
            : base(name, annotation, requirement)
        {
            Values = values;
        }
    }
}