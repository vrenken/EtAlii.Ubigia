// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.MicroContainer;

    internal class PathBuildingScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IPathVariableExpander, PathVariableExpander>();

            container.Register<IConstantPathSubjectPartToGraphPathPartsConverter, ConstantPathSubjectPartToGraphPathPartsConverter>();
            container.Register<IWildcardPathSubjectPartToGraphPathPartsConverter, WildcardPathSubjectPartToGraphPathPartsConverter>();
            container.Register<ITaggedPathSubjectPartToGraphPathPartsConverter, TaggedPathSubjectPartToGraphPathPartsConverter>();
            container.Register<ITraversingWildcardPathSubjectPartToGraphPathPartsConverter, TraversingWildcardPathSubjectPartToGraphPathPartsConverter>();

            container.Register<IConditionalPathSubjectPartToGraphPathPartsConverter, ConditionalPathSubjectPartToGraphPathPartsConverter>();
            container.Register<IConditionalPredicateFactorySelector, ConditionalPredicateFactorySelector>();
            container.Register<IEqualPredicateFactory, EqualPredicateFactory>();
            container.Register<INotEqualPredicateFactory, NotEqualPredicateFactory>();
            container.Register<ILessThanPredicateFactory, LessThanPredicateFactory>();
            container.Register<ILessThanOrEqualPredicateFactory, LessThanOrEqualPredicateFactory>();
            container.Register<IMoreThanPredicateFactory, MoreThanPredicateFactory>();
            container.Register<IMoreThanOrEqualPredicateFactory, MoreThanOrEqualPredicateFactory>();

            container.Register<IIdentifierPathSubjectPartToGraphPathPartsConverter, IdentifierPathSubjectPartToGraphPathPartsConverter>();
            container.Register<IAllParentsPathSubjectPartToGraphPathPartsConverter, AllParentsPathSubjectPartToGraphPathPartsConverter>();
            container.Register<IParentPathSubjectPartToGraphPathPartsConverter, ParentPathSubjectPartToGraphPathPartsConverter>();
            container.Register<IAllChildrenPathSubjectPartToGraphPathPartsConverter, AllChildrenPathSubjectPartToGraphPathPartsConverter>();
            container.Register<IChildrenPathSubjectPartToGraphPathPartsConverter, ChildrenPathSubjectPartToGraphPathPartsConverter>();
            container.Register<IAllDowndatesPathSubjectPartToGraphPathPartsConverter, AllDowndatesPathSubjectPartToGraphPathPartsConverter>();
            container.Register<IDowndatePathSubjectPartToGraphPathPartsConverter, DowndatePathSubjectPartToGraphPathPartsConverter>();
            container.Register<IAllUpdatesPathSubjectPartToGraphPathPartsConverter, AllUpdatesPathSubjectPartToGraphPathPartsConverter>();
            container.Register<IUpdatesPathSubjectPartToGraphPathPartsConverter, UpdatesPathSubjectPartToGraphPathPartsConverter>();
            container.Register<IPathSubjectToGraphPathConverter, PathSubjectToGraphPathConverter>();
            container.Register<IPathProcessor, PathProcessor>();

            container.Register<IVariablePathSubjectPartToPathConverter, VariablePathSubjectPartToPathConverter>();
            container.Register<IVariablePathSubjectPartToGraphPathPartsConverter, VariablePathSubjectPartToGraphPathPartsConverter>();
        }
    }
}
