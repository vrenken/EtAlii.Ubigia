// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public sealed class RootedPathFunctionSubjectArgument : FunctionSubjectArgument
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
