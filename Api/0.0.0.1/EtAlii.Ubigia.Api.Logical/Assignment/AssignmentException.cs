namespace EtAlii.Ubigia.Api.Logical
{
    using System;

    [Serializable]
    public class AssignmentException : Exception
    {
        public AssignmentException()
        {
        }
        public AssignmentException(string message)
            : base(message)
        {
        }
    }
}