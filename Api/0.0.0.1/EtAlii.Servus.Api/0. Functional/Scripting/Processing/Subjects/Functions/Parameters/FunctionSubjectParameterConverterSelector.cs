namespace EtAlii.Servus.Api.Functional
{
    using EtAlii.xTechnology.Structure;

    internal class FunctionSubjectParameterConverterSelector : Selector<FunctionSubjectArgument, IFunctionSubjectParameterConverter>, IFunctionSubjectParameterConverterSelector
    {
        public FunctionSubjectParameterConverterSelector(
            IPathSubjectFunctionParameterConverter pathSubjectFunctionParameterConverter,
            IConstantSubjectFunctionParameterConverter constantSubjectFunctionParameterConverter,
            IVariableSubjectFunctionParameterConverter variableSubjectFunctionParameterConverter)
        {

            Register(item => item is PathFunctionSubjectArgument, pathSubjectFunctionParameterConverter);
            Register(item => item is ConstantFunctionSubjectArgument, constantSubjectFunctionParameterConverter);
            Register(item => item is VariableFunctionSubjectArgument, variableSubjectFunctionParameterConverter);
        }
    }
}
