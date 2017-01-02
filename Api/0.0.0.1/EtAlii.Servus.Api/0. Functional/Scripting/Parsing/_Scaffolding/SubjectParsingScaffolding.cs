namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Linq;
    using EtAlii.xTechnology.MicroContainer;

    internal class SubjectParsingScaffolding : IScaffolding
    {
        private readonly IFunctionHandlersProvider _functionHandlersProvider;

        public SubjectParsingScaffolding(IFunctionHandlersProvider functionHandlersProvider)
        {
            _functionHandlersProvider = functionHandlersProvider;
        }

        public void Register(Container container)
        {
            container.Register<ISubjectsParser, SubjectsParser>();
            container.Register<IVariableSubjectParser, VariableSubjectParser>();

            container.Register<IConstantSubjectsParser, ConstantSubjectsParser>();
            container.Register<IStringConstantSubjectParser, StringConstantSubjectParser>();
            container.Register<IObjectConstantSubjectParser, ObjectConstantSubjectParser>();

            RegisterPathSubjectParsing(container);

            container.Register<IFunctionSubjectParser, FunctionSubjectParser>();
            container.Register<IConstantFunctionSubjectArgumentParser, ConstantFunctionSubjectArgumentParser>();
            container.Register<IVariableFunctionSubjectArgumentParser, VariableFunctionSubjectArgumentParser>();
            container.Register<IPathFunctionSubjectArgumentParser, PathFunctionSubjectArgumentParser>();

            container.Register<IFunctionHandlerFactory, FunctionHandlerFactory>();
            container.Register<IFunctionHandlersProvider>(() => GetFunctionHandlersProvider(container));
            container.Register<IFunctionSubjectArgumentsParser, FunctionSubjectArgumentsParser>();
        }

        private IFunctionHandlersProvider GetFunctionHandlersProvider(Container container)
        {

            var defaultFunctionHandlers = container.GetInstance<IFunctionHandlerFactory>().CreateDefaults();

            var functionHandlers = defaultFunctionHandlers
                .Concat(_functionHandlersProvider.FunctionHandlers)
                .ToArray();

            var doubles = functionHandlers
                .GroupBy(fh => fh.Name)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToArray();
            if (doubles.Any())
            {
                var message = String.Format("Double registered function handlers detected: {0}", String.Join(", ", doubles));
                throw new ScriptParserException(message);   
            }

            return new FunctionHandlersProvider(functionHandlers); 
        }

        internal static void RegisterPathSubjectParsing(Container container)
        {
            container.Register<IPathSubjectParser, PathSubjectParser>();
            container.Register<IPathSubjectPartsParser, PathSubjectPartsParser>();
            container.Register<IWildcardPathSubjectPartParser, WildcardPathSubjectPartParser>();
            container.Register<ITraversingWildcardPathSubjectPartParser, TraversingWildcardPathSubjectPartParser>();

            container.Register<IConditionalPathSubjectPartParser, ConditionalPathSubjectPartParser>();
            container.Register<IConditionParser, ConditionParser>();
            
            container.Register<IConstantPathSubjectPartParser, ConstantPathSubjectPartParser>();
            container.Register<IVariablePathSubjectPartParser, VariablePathSubjectPartParser>();
            container.Register<IIdentifierPathSubjectPartParser, IdentifierPathSubjectPartParser>();
            container.Register<IIsParentOfPathSubjectPartParser, IsParentOfPathSubjectPartParser>();
            container.Register<IIsChildOfPathSubjectPartParser, IsChildOfPathSubjectPartParser>();
            container.Register<IDowndateOfPathSubjectPartParser, DowndateOfPathSubjectPartParser>();
            container.Register<IUpdatesOfPathSubjectPartParser, UpdatesOfPathSubjectPartParser>();
        }
    }
}
