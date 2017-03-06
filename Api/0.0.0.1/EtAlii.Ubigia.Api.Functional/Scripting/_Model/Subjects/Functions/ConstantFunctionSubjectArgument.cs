﻿namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    public class ConstantFunctionSubjectArgument : FunctionSubjectArgument
    {
        public string Value { get; }

        public ConstantFunctionSubjectArgument(string value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return String.Format("\"{0}\"", Value);
        }
    }
}
