namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Functional.Scripting;

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
