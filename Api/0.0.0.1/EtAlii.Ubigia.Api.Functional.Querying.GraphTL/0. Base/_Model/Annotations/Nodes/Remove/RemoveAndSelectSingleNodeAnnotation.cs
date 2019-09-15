namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Functional.Scripting;

    public class RemoveAndSelectSingleNodeAnnotation : NodeAnnotation
    {
        /// <summary>
        /// The name of the node to be removed. 
        /// </summary>
        public string Name { get; }

        public RemoveAndSelectSingleNodeAnnotation(PathSubject source, string name) : base(source) 
        {
            Name = name;
        }
                        
        public override string ToString()
        {
            return $"@{AnnotationPrefix.NodeRemove}({Source?.ToString() ?? string.Empty}, {Name})";
        }
    }
}
