namespace EtAlii.Ubigia.Api.Functional
{
    public class RemoveAndSelectMultipleNodesAnnotation : NodeAnnotation
    {
        /// <summary>
        /// The name of the node to be removed. 
        /// </summary>
        public string Name { get; }

        public RemoveAndSelectMultipleNodesAnnotation(PathSubject target, string name) : base(target)
        {
            Name = name;
        }
    }
}
