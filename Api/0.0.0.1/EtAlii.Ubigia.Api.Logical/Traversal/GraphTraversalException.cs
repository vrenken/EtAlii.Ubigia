namespace EtAlii.Ubigia.Api.Logical
{
    using System;

    [Serializable]
    public class GraphTraversalException : Exception
    {
        public GraphTraversalException()
        {
        }

        public GraphTraversalException(string message) 
            : base(message)
        {
        }
        public GraphTraversalException(string message, Exception exception) 
            : base(message, exception)
        {
        }
    }
}