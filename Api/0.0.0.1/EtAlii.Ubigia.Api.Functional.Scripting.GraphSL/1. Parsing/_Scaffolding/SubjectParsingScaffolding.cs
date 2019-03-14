namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.xTechnology.MicroContainer;

    internal class SubjectParsingScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<ISubjectsParser, SubjectsParser>();
            container.Register<IVariableSubjectParser, VariableSubjectParser>();

            container.Register<IConstantSubjectsParser, ConstantSubjectsParser>();
            container.Register<IStringConstantSubjectParser, StringConstantSubjectParser>();
            container.Register<IObjectConstantSubjectParser, ObjectConstantSubjectParser>();

            RegisterPathSubjectParsing(container);

            container.Register<IRootSubjectParser, RootSubjectParser>();
            container.Register<IRootDefinitionSubjectParser, RootDefinitionSubjectParser>();

            container.Register<IFunctionSubjectParser, FunctionSubjectParser>();
            container.Register<IConstantFunctionSubjectArgumentParser, ConstantFunctionSubjectArgumentParser>();
            container.Register<IVariableFunctionSubjectArgumentParser, VariableFunctionSubjectArgumentParser>();
            container.Register<INonRootedPathFunctionSubjectArgumentParser, NonRootedPathFunctionSubjectArgumentParser>();
            container.Register<IRootedPathFunctionSubjectArgumentParser, RootedPathFunctionSubjectArgumentParser>();

            container.Register<IFunctionSubjectArgumentsParser, FunctionSubjectArgumentsParser>();
        }

        internal static void RegisterPathSubjectParsing(Container container)
        {
            container.Register<INonRootedPathSubjectParser, NonRootedPathSubjectParser>();
            container.Register<IRootedPathSubjectParser, RootedPathSubjectParser>();
            container.Register<IPathSubjectPartsParser, PathSubjectPartsParser>();
            container.Register<ITaggedPathSubjectPartParser, TaggedPathSubjectPartParser>();
            container.Register<IWildcardPathSubjectPartParser, WildcardPathSubjectPartParser>();
            container.Register<ITraversingWildcardPathSubjectPartParser, TraversingWildcardPathSubjectPartParser>();

            container.Register<IConditionalPathSubjectPartParser, ConditionalPathSubjectPartParser>();
            container.Register<IConditionParser, ConditionParser>();
            
            container.Register<IConstantPathSubjectPartParser, ConstantPathSubjectPartParser>();
            container.Register<IVariablePathSubjectPartParser, VariablePathSubjectPartParser>();
            container.Register<IIdentifierPathSubjectPartParser, IdentifierPathSubjectPartParser>();
            
            container.Register<IAllParentsPathSubjectPartParser, AllParentsPathSubjectPartParser>();
            container.Register<IParentPathSubjectPartParser, ParentPathSubjectPartParser>();
            container.Register<IAllChildrenPathSubjectPartParser, AllChildrenPathSubjectPartParser>();
            container.Register<IChildrenPathSubjectPartParser, ChildrenPathSubjectPartParser>();
            
            container.Register<IAllDowndatesPathSubjectPartParser, AllDowndatesPathSubjectPartParser>();
            container.Register<IDowndatePathSubjectPartParser, DowndatePathSubjectPartParser>();
            container.Register<IAllUpdatesPathSubjectPartParser, AllUpdatesPathSubjectPartParser>();
            container.Register<IUpdatesPathSubjectPartParser, UpdatesPathSubjectPartParser>();
            
            container.Register<IAllPreviousPathSubjectPartParser, AllPreviousPathSubjectPartParser>();
            container.Register<IPreviousPathSubjectPartParser, PreviousPathSubjectPartParser>();
            container.Register<IAllNextPathSubjectPartParser, AllNextPathSubjectPartParser>();
            container.Register<INextPathSubjectPartParser, NextPathSubjectPartParser>();
            
            container.Register<ITypedPathSubjectPartParser, TypedPathSubjectPartParser>();
            container.Register<IRegexPathSubjectPartParser, RegexPathSubjectPartParser>();
        }
    }
}
