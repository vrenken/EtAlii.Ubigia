namespace EtAlii.Servus.Api.Logical
{
    using System;

    public class AssignmentException : Exception
    {
        public AssignmentException(string message)
            : base(message)
        {
        }
    }
}