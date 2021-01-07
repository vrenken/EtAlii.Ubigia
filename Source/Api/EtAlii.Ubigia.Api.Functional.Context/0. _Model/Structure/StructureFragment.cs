namespace EtAlii.Ubigia.Api.Functional.Context
{
    public class StructureFragment : Fragment
    {
        public StructureFragment[] Children { get; }

        public NodeAnnotation Annotation { get; }
        public ValueFragment[] Values { get; }

        internal StructureFragment(
            string name,
            NodeAnnotation annotation,
            Requirement requirement,
            ValueFragment[] values,
            StructureFragment[] children,
            FragmentType fragmentType)
            : base(name, requirement, fragmentType)
        {
            Values = values;
            Children = children;
            Annotation = annotation;
        }

        public override string ToString()
        {
            return $"{Name}{(Annotation != null ? " " + Annotation : string.Empty)}";
        }
    }
}
