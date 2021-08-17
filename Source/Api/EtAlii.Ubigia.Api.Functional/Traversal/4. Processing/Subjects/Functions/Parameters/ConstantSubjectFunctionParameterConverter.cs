// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal class ConstantSubjectFunctionParameterConverter : IConstantSubjectFunctionParameterConverter
    {
        public object Convert(FunctionSubjectArgument argument, ExecutionScope scope)
        {
            var constantArgument = (ConstantFunctionSubjectArgument)argument;
            return constantArgument.Value;
        }
    }
}
