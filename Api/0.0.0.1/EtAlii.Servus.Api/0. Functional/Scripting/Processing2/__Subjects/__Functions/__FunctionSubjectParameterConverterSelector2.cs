//namespace EtAlii.Servus.Api.Functional
//{
//    using EtAlii.xTechnology.Structure;

//    internal class FunctionSubjectParameterConverterSelector2 : Selector<FunctionSubjectArgument, IFunctionSubjectParameterConverter2>, IFunctionSubjectParameterConverterSelector2
//    {
//        public FunctionSubjectParameterConverterSelector2(
//            IPathSubjectFunctionParameterConverter2 pathSubjectFunctionParameterConverter,
//            IConstantSubjectFunctionParameterConverter2 constantSubjectFunctionParameterConverter,
//            IVariableSubjectFunctionParameterConverter2 variableSubjectFunctionParameterConverter)
//        {

//            Register(item => item is PathFunctionSubjectArgument, pathSubjectFunctionParameterConverter);
//            Register(item => item is ConstantFunctionSubjectArgument, constantSubjectFunctionParameterConverter);
//            Register(item => item is VariableFunctionSubjectArgument, variableSubjectFunctionParameterConverter);
//        }
//    }
//}
