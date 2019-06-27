namespace EtAlii.Ubigia.Api.Functional
{
    public abstract class Fragment
    {
        /// <summary>
        /// The Name of the Fragment.
        /// </summary>
        public string Name {get;}
        public Annotation Annotation { get; }        
        
        public Fragment(string name, Annotation annotation)
        {
            Annotation = annotation;
            Name = name;
        }
    }
}
