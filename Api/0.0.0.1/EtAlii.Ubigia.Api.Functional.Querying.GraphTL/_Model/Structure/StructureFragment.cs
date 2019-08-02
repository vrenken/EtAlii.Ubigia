namespace EtAlii.Ubigia.Api.Functional 
{
    public abstract class StructureFragment : Fragment
    {
        public StructureFragment[] Children { get; }

        public ValueFragment[] Values { get; }

        protected StructureFragment(
            string name, 
            Annotation annotation, 
            ValueFragment[] values, 
            StructureFragment[] children)
            : base(name, annotation)
        {
            Values = values;
            Children = children;
        }
    }
}
