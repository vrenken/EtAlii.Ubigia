namespace EtAlii.Ubigia.Api.Functional 
{
    public abstract class QueryFragment : Fragment
    {
        public Requirement Requirement { get; }
        
//        public static readonly QueryFragment None = new EmptyQueryFragment();

        public QueryFragment(string name, Annotation annotation, Requirement requirement) : base(name, annotation)
        {
            Requirement = requirement;
        }
//        
//        private class EmptyQueryFragment : QueryFragment
//        {
//            public EmptyQueryFragment() : base(nameof(None), null, Requirement.None)
//            {
//            }
//        }
    }
}
