namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal class ConstantSubjectFunctionParameterConverter : IConstantSubjectFunctionParameterConverter
    {
        public object Convert(FunctionSubjectArgument argument)
        {
            var constantArgument = (ConstantFunctionSubjectArgument)argument;
            return constantArgument.Value;
        }
    }
}
