﻿// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using EtAlii.xTechnology.MicroContainer;

    internal class SchemaParserScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<INodeAnnotationsParser, NodeAnnotationsParser>();
            container.Register<INodeValueAnnotationsParser, NodeValueAnnotationsParser>();

            container.Register<ISchemaParser, SchemaParser>();
            container.Register<IRequirementParser, RequirementParser>();
            
            container.Register<IAssignmentParser, AssignmentParser>();
            container.RegisterInitializer<IKeyValuePairParser>(keyValuePairParser => ((KeyValuePairParser)keyValuePairParser).Initialize(container.GetInstance<IAssignmentParser>().Parser));
            
            container.Register<IStructureFragmentParser, StructureFragmentParser>();
            container.Register<INodeValueFragmentParser, NodeValueFragmentParser>();

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

            container.Register<ISetAndSelectNodeValueAnnotationParser, SetAndSelectNodeValueAnnotationParser>();
            container.Register<IClearAndSelectNodeValueAnnotationParser, ClearAndSelectNodeValueAnnotationParser>();
            container.Register<ISelectNodeValueAnnotationParser, SelectNodeValueAnnotationParser>();

            //container.Register<IStructureMutationParser, StructureMutationParser>() 
            //container.Register<IValueMutationParser, ValueMutationParser>()

            // Path helpers
            //container.Register<IPathRelationParserBuilder, PathRelationParserBuilder>()
        }
    }
}