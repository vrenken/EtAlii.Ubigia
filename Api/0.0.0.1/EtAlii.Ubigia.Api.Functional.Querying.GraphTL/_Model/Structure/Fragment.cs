namespace EtAlii.Ubigia.Api.Functional 
{
    public abstract class Fragment
    {
        /// <summary>
        /// The Name of the Fragment.
        /// </summary>
        public string Name {get;}
        public Annotation Annotation { get; }

        internal FragmentMetadata Metadata { get; private set; }
        protected Fragment(string name, Annotation annotation)
        {
            Annotation = annotation;
            Name = name;
        }

        internal void SetMetaData(FragmentMetadata metadata)
        {
            Metadata = metadata;
        }

        public override string ToString()
        {
            return $"{Name}{(Annotation != null ? " " + Annotation : string.Empty)}";
        }
    }
}
