// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    /// <summary>
    /// The interface that abstracts away any GTL specific path parser implementation.
    /// This specific parser solely parses paths, and nothing else.
    /// </summary>
    public interface IPathParser
    {
        Subject ParseRootedPath(string text);
        Subject ParseNonRootedPath(string text);

        Subject ParsePath(string text);
    }
}
