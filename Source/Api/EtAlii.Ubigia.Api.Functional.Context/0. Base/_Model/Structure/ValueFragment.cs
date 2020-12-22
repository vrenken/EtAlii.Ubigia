namespace EtAlii.Ubigia.Api.Functional.Context
{
    public class ValueFragment : Fragment
    {
        public object Mutation { get; }

        public NodeValueAnnotation Annotation { get; }

        internal ValueFragment(
            string name,
            NodeValueAnnotation annotation,
            Requirement requirement,
            FragmentType fragmentType,
            object mutation)
            : base(name, requirement, fragmentType)
        {
            Annotation = annotation;
            Mutation = mutation;
        }

        public override string ToString()
        {
            return $"{Name}{(Annotation != null ? " " + Annotation : string.Empty)}";
        }
    }
}
