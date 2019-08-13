namespace EtAlii.Ubigia.Api.Functional
{
    public class AddAndSelectSingleNodeAnnotation : NodeAnnotation
    {
        /// <summary>
        /// The name of the node to be added. 
        /// </summary>
        public string Name { get; }

        public AddAndSelectSingleNodeAnnotation(PathSubject target, string name) : base(target)
        {
            Name = name;
        }
    }
}
