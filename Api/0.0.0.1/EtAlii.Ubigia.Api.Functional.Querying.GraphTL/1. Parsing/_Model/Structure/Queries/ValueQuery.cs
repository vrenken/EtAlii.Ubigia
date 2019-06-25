namespace EtAlii.Ubigia.Api.Functional
{
    public class ValueQuery : QueryFragment
    {
    
        public ValueQuery(string name, Annotation annotation, Requirement requirement)
            : base(name, annotation, requirement)
        {
        }
    }
}