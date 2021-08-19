// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.MicroContainer;

    public class ProfilingPathParserExtension : IExtension
    {
        public void Initialize(IRegisterOnlyContainer container)
        {
            container.RegisterDecorator<IPathParser, ProfilingPathParser>();
            container.RegisterDecorator<INonRootedPathSubjectParser, ProfilingNonRootedPathSubjectParser>();
        }
    }
}
