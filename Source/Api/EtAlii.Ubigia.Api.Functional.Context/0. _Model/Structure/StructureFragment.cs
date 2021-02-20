namespace EtAlii.Ubigia.Api.Functional.Context
{
    public class StructureFragment : Fragment
    {
        public StructureFragment[] Children { get; }

        public NodeAnnotation Annotation { get; }
        public ValueFragment[] Values { get; }

        public Plurality Plurality { get; }

        public StructureFragment(
            string name,
            Plurality plurality,
            NodeAnnotation annotation,
            ValueFragment[] values,
            StructureFragment[] children,
            FragmentType fragmentType)
            : base(name, fragmentType)
        {
            Plurality = plurality;
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
