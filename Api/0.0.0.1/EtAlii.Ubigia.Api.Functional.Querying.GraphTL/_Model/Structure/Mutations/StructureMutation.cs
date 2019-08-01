namespace EtAlii.Ubigia.Api.Functional
{
    public class StructureMutation : Fragment
    {
        public Fragment[] Values { get; }
    
        public StructureMutation (string name, Annotation annotation, Fragment[] values)
            : base(name, annotation)
        {
            Values = values;
        }
    }
}