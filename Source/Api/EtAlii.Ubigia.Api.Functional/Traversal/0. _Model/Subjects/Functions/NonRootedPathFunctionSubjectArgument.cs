// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
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
