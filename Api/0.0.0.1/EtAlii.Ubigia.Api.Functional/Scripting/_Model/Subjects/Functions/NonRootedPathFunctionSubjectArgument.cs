﻿namespace EtAlii.Ubigia.Api.Functional
{
    public class NonRootedPathFunctionSubjectArgument : FunctionSubjectArgument
    {
        public NonRootedPathSubject Subject { get; }

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
