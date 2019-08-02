namespace EtAlii.Ubigia.Api.Functional 
{
    public class StructureFragment : Fragment
    {
        public StructureFragment[] Children { get; }

        public ValueFragment[] Values { get; }

        internal StructureFragment(
            string name, 
            Annotation annotation, 
            Requirement requirement, 
            ValueFragment[] values, 
            StructureFragment[] children, 
            FragmentType fragmentType)
            : base(name, annotation, requirement, fragmentType)
        {
            Values = values;
            Children = children;
        }
    }
}
