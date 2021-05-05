﻿namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.MicroContainer;

    internal class LapaSequenceParsingScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            // Sequence
            container.Register<ISequenceParser, SequenceParser>();
            container.Register<ISequencePartsParser, SequencePartsParser>();
            container.Register<ICommentParser, CommentParser>();
        }
    }
}