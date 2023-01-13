// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context;

using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
/// A <see cref="GraphContext"/> can be used to execute GCL queries on a Ubigia space.
/// </summary>
public interface IGraphContext
{
    /// <summary>
    /// Parse the specified text into a GCL query.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="scope"></param>
    /// <returns></returns>
    SchemaParseResult Parse(string text, ExecutionScope scope);

    IAsyncEnumerable<Structure> Process(Schema schema, ExecutionScope scope);
    IAsyncEnumerable<Structure> Process(string text, ExecutionScope scope, params object[] args);
    IAsyncEnumerable<Structure> Process(string[] text, ExecutionScope scope);
    IAsyncEnumerable<Structure> Process(string text, ExecutionScope scope);

    Task<TResult> ProcessSingle<TResult>(string text, IResultMapper<TResult> resultMapper, ExecutionScope scope);
    IAsyncEnumerable<TResult> ProcessMultiple<TResult>(string text, IResultMapper<TResult> resultMapper, ExecutionScope scope);
}
