namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Functional.Scripting;

    public class AddAndSelectSingleNodeAnnotation : NodeAnnotation
    {
        /// <summary>
        /// The name of the node to be added. 
        /// </summary>
        public string Name { get; }

        public AddAndSelectSingleNodeAnnotation(PathSubject source, string name) : base(source) 
        {
            Name = name;
        }
        
        public override string ToString()
        {
            return $"@{AnnotationPrefix.NodeAdd}({Source?.ToString() ?? string.Empty}, {Name})";
        }
    }
}
