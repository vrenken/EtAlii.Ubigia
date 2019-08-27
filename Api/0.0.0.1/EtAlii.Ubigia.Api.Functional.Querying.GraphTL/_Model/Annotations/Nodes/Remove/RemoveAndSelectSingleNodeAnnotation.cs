namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Functional.Scripting;

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
