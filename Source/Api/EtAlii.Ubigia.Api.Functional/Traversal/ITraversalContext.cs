// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System;

/// <summary>
/// A <see cref="TraversalContext"/> can be used to execute GTL scripts on a Ubigia space.
/// </summary>
public interface ITraversalContext
{
    /// <summary>
    /// Parse the specified text into a GTL script.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="scope"></param>
    /// <returns></returns>
    ScriptParseResult Parse(string text, ExecutionScope scope);

    IObservable<SequenceProcessingResult> Process(Script script, ExecutionScope scope);
    IObservable<SequenceProcessingResult> Process(string text, ExecutionScope scope, params object[] args);
    IObservable<SequenceProcessingResult> Process(string[] text, ExecutionScope scope);

    IObservable<SequenceProcessingResult> Process(string text, ExecutionScope scope);
}
