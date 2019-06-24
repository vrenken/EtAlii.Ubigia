namespace EtAlii.Ubigia.Api.Functional
{
    public class StructureQuery : Fragment
    {
        public Fragment[] Values { get; }
    
        public StructureQuery (string name, Annotation annotation, Fragment[] values)
            : base(name, annotation)
        {
            Values = values;
        }
    }
}