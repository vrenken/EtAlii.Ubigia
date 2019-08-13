namespace EtAlii.Ubigia.Api.Functional
{
    public class AddAndSelectMultipleNodesAnnotation : NodeAnnotation
    {
        /// <summary>
        /// The name of the node to be added. 
        /// </summary>
        public string Name { get; }
        
        public AddAndSelectMultipleNodesAnnotation(PathSubject target, string name) : base(target)
        {
            Name = name;
        }
    }
}
