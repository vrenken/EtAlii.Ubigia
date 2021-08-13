// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.MicroContainer;

    internal class LapaOperatorParsingScaffolding : IScaffolding
    {
        public void Register(IRegisterOnlyContainer container)
        {
            // Operators
            container.Register<IOperatorsParser, OperatorsParser>();
            container.Register<IAssignOperatorParser, AssignOperatorParser>();
            container.Register<IAddOperatorParser, AddOperatorParser>();
            container.Register<IRemoveOperatorParser, RemoveOperatorParser>();
        }
    }
}
