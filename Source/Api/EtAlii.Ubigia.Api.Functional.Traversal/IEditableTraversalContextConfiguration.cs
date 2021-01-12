namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;

    public interface IEditableTraversalContextConfiguration
    {
        IFunctionHandlersProvider FunctionHandlersProvider { get; set; }

        IRootHandlerMappersProvider RootHandlerMappersProvider { get; set; }

        Func<IScriptParserFactory> ScriptParserFactory { get; set; }
        Func<IScriptProcessorFactory> ScriptProcessorFactory { get; set; }
    }
}
