namespace EtAlii.Ubigia.Api.Functional 
{
    public class ValueFragment : Fragment
    {
        public object Mutation { get; }

        internal ValueFragment(
            string name, 
            Annotation annotation, 
            Requirement requirement, 
            FragmentType fragmentType, 
            object mutation)
            : base(name, annotation, requirement, fragmentType)
        {
            Mutation = mutation;
        }
    }
}
