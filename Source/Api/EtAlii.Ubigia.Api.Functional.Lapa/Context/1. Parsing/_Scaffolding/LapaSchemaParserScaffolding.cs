// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.xTechnology.MicroContainer;

    internal sealed class LapaSchemaParserScaffolding : IScaffolding
    {
        public void Register(IRegisterOnlyContainer container)
        {
            container.Register<INodeAnnotationsParser, NodeAnnotationsParser>();
            container.Register<IValueAnnotationsParser, ValueAnnotationsParser>();

            container.Register<ISchemaParser, LapaSchemaParser>();
            container.Register<IRequirementParser, RequirementParser>();

            container.Register<IAssignmentParser, AssignmentParser>();
            container.Register<IFragmentKeyValuePairParser, FragmentKeyValuePairParser>();

            container.Register<IStructureFragmentParser, StructureFragmentParser>();
            container.Register<IValueFragmentParser, ValueFragmentParser>();

            container.Register<IAddAndSelectMultipleNodesAnnotationParser, AddAndSelectMultipleNodesAnnotationParser>();
            container.Register<IAddAndSelectSingleNodeAnnotationParser, AddAndSelectSingleNodeAnnotationParser>();

            container.Register<ILinkAndSelectMultipleNodesAnnotationParser, LinkAndSelectMultipleNodesAnnotationParser>();
            container.Register<ILinkAndSelectSingleNodeAnnotationParser, LinkAndSelectSingleNodeAnnotationParser>();

            container.Register<IRemoveAndSelectMultipleNodesAnnotationParser, RemoveAndSelectMultipleNodesAnnotationParser>();
            container.Register<IRemoveAndSelectSingleNodeAnnotationParser, RemoveAndSelectSingleNodeAnnotationParser>();

            container.Register<ISelectMultipleNodesAnnotationParser, SelectMultipleNodesAnnotationParser>();
            container.Register<ISelectSingleNodeAnnotationParser, SelectSingleNodeAnnotationParser>();

            container.Register<IUnlinkAndSelectMultipleNodesAnnotationParser, UnlinkAndSelectMultipleNodesAnnotationParser>();
            container.Register<IUnlinkAndSelectSingleNodeAnnotationParser, UnlinkAndSelectSingleNodeAnnotationParser>();

            container.Register<ISetAndSelectValueAnnotationParser, SetAndSelectValueAnnotationParser>();
            container.Register<IClearAndSelectValueAnnotationParser, ClearAndSelectValueAnnotationParser>();
            container.Register<ISelectValueAnnotationParser, SelectValueAnnotationParser>();
        }
    }
}
