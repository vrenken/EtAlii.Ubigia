namespace EtAlii.Ubigia.Api.Functional 
{
    public abstract class QueryFragment : Fragment
    {
        public Requirement Requirement { get; }
        
        public QueryFragment(string name, Annotation annotation, Requirement requirement) : base(name, annotation)
        {
            Requirement = requirement;
        }
    }
}
