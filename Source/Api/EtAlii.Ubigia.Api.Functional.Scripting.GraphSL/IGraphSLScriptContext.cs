﻿namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System;

    /// <summary>
    /// A <see cref="GraphSLScriptContext"/> can be used to execute GSL scripts on a Ubigia space. 
    /// </summary>
    public interface IGraphSLScriptContext
    {
        /// <summary>
        /// Parse the specified text into a GSL script. 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        ScriptParseResult Parse(string text);

        // TODO: Add non-async methods.
        //void Process(Script script, IScriptScope scope)
        //void Process(Script script, IScriptScope scope, IProgress<ScriptProcessingProgress> progress)
        //IEnumerable<object> Process(string text, IProgress<ScriptProcessingProgress> progress)
        //IEnumerable<object> Process(string text, params object[] args)
        //IEnumerable<object> Process(string text, IProgress<ScriptProcessingProgress> progress, params object[] args)

        // TODO: Rename to ProcessAsync.
        IObservable<SequenceProcessingResult> Process(Script script, IScriptScope scope);
        //Task Process(Script script, IScriptScope scope, IProgress<ScriptProcessingProgress> progress)
        //Task<IEnumerable<object>> Process(string text, IProgress<ScriptProcessingProgress> progress)
        IObservable<SequenceProcessingResult> Process(string text, params object[] args);
        IObservable<SequenceProcessingResult> Process(string[] text);
        IObservable<SequenceProcessingResult> Process(string[] text, IScriptScope scope);

        IObservable<SequenceProcessingResult> Process(string text);
        //Task<IEnumerable<object>> Process(string text, IProgress<ScriptProcessingProgress> progress, params object[] args)

    }
}