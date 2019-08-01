namespace EtAlii.Ubigia.Api.Functional
{
    public class StructureQuery : Fragment
    {
        public ValueQuery[] Values { get; }
        public StructureQuery[] Children { get; }
    
        public Requirement Requirement { get; }
        
        public StructureQuery(string name, Annotation annotation, Requirement requirement, ValueQuery[] values, StructureQuery[] children)
            : base(name, annotation)
        {
            Requirement = requirement;
            Values = values;
            Children = children;
        }
    }
}