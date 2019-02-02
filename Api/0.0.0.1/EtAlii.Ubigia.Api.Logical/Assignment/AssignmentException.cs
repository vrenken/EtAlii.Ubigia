namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class AssignmentException : Exception
    {
        private AssignmentException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public AssignmentException(string message)
            : base(message)
        {
        }
    }
}