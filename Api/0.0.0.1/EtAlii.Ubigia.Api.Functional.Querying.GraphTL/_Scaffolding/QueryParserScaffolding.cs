// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.xTechnology.MicroContainer;

    internal class QueryParserScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IAnnotationParser, AnnotationParser>();
            container.Register<ICommentParser, CommentParser>();
            container.Register<IObjectParser, ObjectParser>();
            container.Register<IQueryParser, QueryParser>();

            // Path helpers
            //container.Register<IPathRelationParserBuilder, PathRelationParserBuilder>();
        }
    }
}
