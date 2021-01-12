﻿namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    /// <summary>
    /// The interface that abstracts away any GTL specific parser implementation.
    /// </summary>
    public interface IScriptParser
    {
        /// <summary>
        /// Parse the script specified in the provided text.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        ScriptParseResult Parse(string text);

        /// <summary>
        /// Parse the script specified in the provided lines.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        ScriptParseResult Parse(string[] text);
    }
}
