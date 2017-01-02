namespace EtAlii.Servus.Api.Functional
{
    using EtAlii.xTechnology.MicroContainer;

    internal class FunctionSubjectProcessingScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IFunctionSubjectProcessor, FunctionSubjectProcessor>();
            container.Register<IFunctionContext, FunctionContext>();
            container.Register<IFunctionSubjectParameterConverterSelector, FunctionSubjectParameterConverterSelector>();

            container.Register<IPathSubjectFunctionParameterConverter, PathSubjectFunctionParameterConverter>();
            container.Register<IConstantSubjectFunctionParameterConverter, ConstantSubjectFunctionParameterConverter>();
            container.Register<IVariableSubjectFunctionParameterConverter, VariableSubjectFunctionParameterConverter>();
        }
    }
}
