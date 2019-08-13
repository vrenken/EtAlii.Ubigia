namespace EtAlii.Ubigia.Api.Functional
{
    public class RemoveAndSelectSingleNodeAnnotation : NodeAnnotation
    {
        /// <summary>
        /// The name of the node to be removed. 
        /// </summary>
        public string Name { get; }

        public RemoveAndSelectSingleNodeAnnotation(PathSubject target, string name) : base(target)
        {
            Name = name;
        }
    }
}
