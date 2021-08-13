// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.MicroContainer;

    internal class LapaSequenceParsingScaffolding : IScaffolding
    {
        public void Register(IRegisterOnlyContainer container)
        {
            // Sequence
            container.Register<ISequenceParser, SequenceParser>();
            container.Register<ISequencePartsParser, SequencePartsParser>();
            container.Register<ICommentParser, CommentParser>();
        }
    }
}
