// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using Moppet.Lapa;

    /// <summary>
    /// This is the basic interface of all Moppet.Lapa parsers.
    /// </summary>
    internal interface IParser
    {
        /// <summary>
        /// The Id that can be used to find LpNode instances for the given LpsParser.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// The LpsParser used to parse text for instances.
        /// </summary>
        LpsParser Parser { get; }
    }

    /// <summary>
    /// This is the basic interface of all Moppet.Lapa parsers.
    /// </summary>
    internal interface IParser<out TItem> : IParser
    {
        /// <summary>
        /// Parse the given node and return an item instance that matches it.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        TItem Parse(LpNode node);

        /// <summary>
        /// Return true when the node can be parsed by the parser.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        bool CanParse(LpNode node);
    }
}
