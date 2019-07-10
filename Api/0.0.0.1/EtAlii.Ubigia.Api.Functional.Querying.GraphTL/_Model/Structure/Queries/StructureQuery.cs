namespace EtAlii.Ubigia.Api.Functional
{
    public class StructureQuery : QueryFragment
    {
        public ValueQuery[] Values { get; }
        public StructureQuery[] Children { get; }
    
        public StructureQuery (string name, Annotation annotation, Requirement requirement, ValueQuery[] values, StructureQuery[] children)
            : base(name, annotation, requirement)
        {
            Values = values;
            Children = children;
        }
    }
}