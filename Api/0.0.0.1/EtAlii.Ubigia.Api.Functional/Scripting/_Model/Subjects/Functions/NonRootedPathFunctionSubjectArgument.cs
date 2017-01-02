namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;

    public class NonRootedPathFunctionSubjectArgument : FunctionSubjectArgument
    {
        public NonRootedPathSubject Subject { get; private set; }

        public NonRootedPathFunctionSubjectArgument(NonRootedPathSubject subject)
        {
            Subject = subject;
        }

        public override string ToString()
        {
            return Subject.ToString();
        }
    }
}
