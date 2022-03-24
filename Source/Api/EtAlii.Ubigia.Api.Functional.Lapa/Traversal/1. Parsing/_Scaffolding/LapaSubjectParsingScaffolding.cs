// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.MicroContainer;

    internal sealed class LapaSubjectParsingScaffolding : IScaffolding
    {
        public void Register(IRegisterOnlyContainer container)
        {
            container.Register<ISubjectsParser, SubjectsParser>();
            container.Register<IVariableSubjectParser, VariableSubjectParser>();

            container.Register<IConstantSubjectsParser, ConstantSubjectsParser>();
            container.Register<IStringConstantSubjectParser, StringConstantSubjectParser>();
            container.Register<IObjectConstantSubjectParser, ObjectConstantSubjectParser>();

            container.Register<IRootSubjectParser, RootSubjectParser>();
            container.Register<IRootDefinitionSubjectParser, RootDefinitionSubjectParser>();

            container.Register<IFunctionSubjectParser, FunctionSubjectParser>();
            container.Register<IConstantFunctionSubjectArgumentParser, ConstantFunctionSubjectArgumentParser>();
            container.Register<IVariableFunctionSubjectArgumentParser, VariableFunctionSubjectArgumentParser>();
            container.Register<INonRootedPathFunctionSubjectArgumentParser, NonRootedPathFunctionSubjectArgumentParser>();
            container.Register<IRootedPathFunctionSubjectArgumentParser, RootedPathFunctionSubjectArgumentParser>();

            container.Register<IFunctionSubjectArgumentsParser, FunctionSubjectArgumentsParser>();
        }
    }
}
