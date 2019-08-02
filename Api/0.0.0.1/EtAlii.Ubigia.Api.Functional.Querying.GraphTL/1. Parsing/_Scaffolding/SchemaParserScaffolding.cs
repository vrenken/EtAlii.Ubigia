// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.xTechnology.MicroContainer;

    internal class SchemaParserScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IAnnotationParser, AnnotationParser>();

            container.Register<ISchemaParser, SchemaParser>();
            container.Register<IRequirementParser, RequirementParser>();
            
            container.Register<IAssignmentParser, AssignmentParser>();
            container.RegisterInitializer<IKeyValuePairParser>(keyValuePairParser => ((KeyValuePairParser)keyValuePairParser).Initialize(container.GetInstance<IAssignmentParser>().Parser));
            
            container.Register<IStructureQueryParser, StructureQueryParser>();
            container.Register<IValueQueryParser, ValueQueryParser>();

            container.Register<IStructureMutationParser, StructureMutationParser>();
            container.Register<IValueMutationParser, ValueMutationParser>();

            // Path helpers
            //container.Register<IPathRelationParserBuilder, PathRelationParserBuilder>();
        }
    }
}
