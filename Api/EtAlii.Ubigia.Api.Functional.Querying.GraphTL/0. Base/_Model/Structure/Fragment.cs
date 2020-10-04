namespace EtAlii.Ubigia.Api.Functional 
{
    public abstract class Fragment
    {
        /// <summary>
        /// The Name of the Fragment.
        /// </summary>
        public string Name {get;}
        public FragmentType Type { get; }

        public Requirement Requirement { get; }

        protected Fragment(
            string name, 
            Requirement requirement, 
            FragmentType fragmentType)
        {
            Name = name;
            Requirement = requirement;
            Type = fragmentType;
        }
    }
}
