namespace EtAlii.Ubigia.Api.Functional.Scripting
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
