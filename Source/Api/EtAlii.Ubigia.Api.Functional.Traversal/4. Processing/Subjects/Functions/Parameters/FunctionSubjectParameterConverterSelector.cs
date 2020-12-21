namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.Structure;

    internal class FunctionSubjectParameterConverterSelector : Selector<FunctionSubjectArgument, IFunctionSubjectParameterConverter>, IFunctionSubjectParameterConverterSelector
    {
        public FunctionSubjectParameterConverterSelector(
            INonRootedPathSubjectFunctionParameterConverter nonRootedPathSubjectFunctionParameterConverter,
            IRootedPathSubjectFunctionParameterConverter rootedPathSubjectFunctionParameterConverter,
            IConstantSubjectFunctionParameterConverter constantSubjectFunctionParameterConverter,
            IVariableSubjectFunctionParameterConverter variableSubjectFunctionParameterConverter)
        {

            Register(item => item is NonRootedPathFunctionSubjectArgument, nonRootedPathSubjectFunctionParameterConverter)
            .Register(item => item is RootedPathFunctionSubjectArgument, rootedPathSubjectFunctionParameterConverter)
            .Register(item => item is ConstantFunctionSubjectArgument, constantSubjectFunctionParameterConverter)
            .Register(item => item is VariableFunctionSubjectArgument, variableSubjectFunctionParameterConverter);
        }
    }
}
