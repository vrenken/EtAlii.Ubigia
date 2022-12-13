// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

internal class TextRootHandlerMapper : IRootHandlerMapper
{
    public RootType Type => RootType.Text;

    public IRootHandler[] AllowedRootHandlers { get; }

    public TextRootHandlerMapper()
    {
        AllowedRootHandlers = new IRootHandler[]
        {
            new TextRootByEmptyHandler(), // only root, no arguments, should be at the end.
        };
    }
}
