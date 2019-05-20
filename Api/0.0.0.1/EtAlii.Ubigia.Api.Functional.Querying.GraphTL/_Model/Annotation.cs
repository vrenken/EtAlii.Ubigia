namespace EtAlii.Ubigia.Api.Functional
{
    /// <summary>
    /// The Query class contains all information needed to execute graph traversal actions on the current infrastructureClient.
    /// </summary>
    public class Annotation
    {
        public static Annotation None { get; } = new Annotation(AnnotationType.Select, null);
        
        public AnnotationType Type { get; }
        public PathSubject Path { get; }

        public Annotation(AnnotationType type, PathSubject path)
        {
            Type = type;
            Path = path;
        }
    }
}
