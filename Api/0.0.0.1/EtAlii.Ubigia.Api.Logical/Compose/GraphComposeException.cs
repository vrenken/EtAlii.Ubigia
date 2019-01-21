namespace EtAlii.Ubigia.Api.Logical
{
    using System;

    [Serializable]
    public class GraphComposeException : Exception
    {
        public GraphComposeException(string message)
            : base(message)
        {
        }
    }
}