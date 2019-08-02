namespace EtAlii.Ubigia.Api.Functional 
{
    public abstract class Fragment
    {
        /// <summary>
        /// The Name of the Fragment.
        /// </summary>
        public string Name {get;}
        public Annotation Annotation { get; }
        public FragmentType Type { get; }

        public Requirement Requirement { get; }

        protected Fragment(
            string name, 
            Annotation annotation, 
            Requirement requirement, 
            FragmentType fragmentType)
        {
            Annotation = annotation;
            Name = name;
            Requirement = requirement;
            Type = fragmentType;
        }

        public override string ToString()
        {
            return $"{Name}{(Annotation != null ? " " + Annotation : string.Empty)}";
        }
    }
}
