//namespace EtAlii.Ubigia.Api.Functional
//{
//    using EtAlii.Ubigia.Api.Functional.Scripting;
//
//    /// <summary>
//    /// The Query class contains all information needed to execute graph traversal actions on the current infrastructureClient.
//    /// </summary>
//    public class Annotation
//    {
//        /// <summary>
//        /// The type of the annotation.
//        /// </summary>
//        public AnnotationType Type { get; }
//        
//        /// <summary>
//        /// The optional subject (in case a operator is given).
//        /// </summary>
//        public Subject Subject { get; }
//        
//        /// <summary>
//        /// The optional operator.
//        /// </summary>
//        public Operator Operator { get; }
//        
//        /// <summary>
//        /// The target path subject, i.e. absolute, relative or rooted path towards one or multiple nodes.
//        /// </summary>
//        public PathSubject Path { get; }
//
//
//        public Annotation(AnnotationType type, PathSubject path, Operator @operator, Subject subject)
//        {
//            Type = type;
//            Path = path;
//            Operator = @operator;
//            Subject = subject;
//        }
//
//        public override string ToString()
//        {
//            return $"@{Type}({Path?.ToString() ?? string.Empty})";
//        }
//    }
//}
