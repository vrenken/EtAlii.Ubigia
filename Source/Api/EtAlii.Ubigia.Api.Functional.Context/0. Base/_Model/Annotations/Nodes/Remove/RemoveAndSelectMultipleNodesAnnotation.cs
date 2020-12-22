namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public class RemoveAndSelectMultipleNodesAnnotation : NodeAnnotation
    {
        /// <summary>
        /// The name of the node to be removed.
        /// </summary>
        public string Name { get; }

        public RemoveAndSelectMultipleNodesAnnotation(PathSubject source, string name) : base(source)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"@{AnnotationPrefix.NodesRemove}({Source?.ToString() ?? string.Empty}, {Name})";
        }
    }
}
