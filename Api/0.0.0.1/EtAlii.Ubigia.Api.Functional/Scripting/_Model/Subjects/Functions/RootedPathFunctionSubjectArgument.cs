namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;

    public class RootedPathFunctionSubjectArgument : FunctionSubjectArgument
    {
        public readonly RootedPathSubject Subject;

        public RootedPathFunctionSubjectArgument(RootedPathSubject subject)
        {
            Subject = subject;
        }

        public override string ToString()
        {
            return Subject.ToString();
        }
    }
}
