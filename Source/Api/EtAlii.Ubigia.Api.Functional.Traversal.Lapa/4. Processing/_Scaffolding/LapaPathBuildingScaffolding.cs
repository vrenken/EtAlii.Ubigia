// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.MicroContainer;

    internal class LapaPathBuildingScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IVariablePathSubjectPartToPathConverter, VariablePathSubjectPartToPathConverter>();
            container.Register<IVariablePathSubjectPartToGraphPathPartsConverter, VariablePathSubjectPartToGraphPathPartsConverter>();

            container.Register<IConstantSubjectsParser, ConstantSubjectsParser>();
            container.Register<IStringConstantSubjectParser, StringConstantSubjectParser>();
            container.Register<IObjectConstantSubjectParser, ObjectConstantSubjectParser>();
        }
    }
}
