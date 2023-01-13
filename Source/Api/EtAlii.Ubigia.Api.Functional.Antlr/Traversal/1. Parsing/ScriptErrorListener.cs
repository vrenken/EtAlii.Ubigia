// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Antlr.Traversal;

using System.IO;
using System.Text;
using Antlr4.Runtime;

internal sealed class ScriptErrorListener : IAntlrErrorListener<object>
{
    private readonly StringBuilder _stringBuilder = new();
    public void SyntaxError(TextWriter output, IRecognizer recognizer, object offendingSymbol, int line, int charPositionInLine,
        string msg, RecognitionException e)

    {
        output.WriteLine($"line {line}:{charPositionInLine} {msg}");
        _stringBuilder.AppendLine($"line {line}:{charPositionInLine} {msg}");
    }

    public string ToErrorString() => _stringBuilder.ToString();
}
