namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public class AddAndSelectMultipleNodesAnnotation : NodeAnnotation
    {
        /// <summary>
        /// The name of the node to be added.
        /// </summary>
        public string Name { get; }

        public AddAndSelectMultipleNodesAnnotation(PathSubject source, string name) : base(source)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"@{AnnotationPrefix.NodesAdd}({Source?.ToString() ?? string.Empty}, {Name})";
        }
    }
}
