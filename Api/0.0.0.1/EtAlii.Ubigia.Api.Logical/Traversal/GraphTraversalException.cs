namespace EtAlii.Ubigia.Api.Logical
{
    using System;

    public class GraphTraversalException : System.Exception
    {
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