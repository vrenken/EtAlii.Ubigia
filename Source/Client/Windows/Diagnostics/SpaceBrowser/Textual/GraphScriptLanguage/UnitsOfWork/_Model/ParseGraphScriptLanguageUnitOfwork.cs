
namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.xTechnology.Structure.Workflow;
    using System;

    public class ParseGraphScriptLanguageUnitOfwork : UnitOfWorkBase<IParseGraphScriptLanguageUnitOfworkHandler>
    {
        public IGraphScriptLanguageViewModel ScriptViewModel { get; }
        public int Time { get; }

        public ParseGraphScriptLanguageUnitOfwork(IGraphScriptLanguageViewModel scriptViewModel)
        {
            ScriptViewModel = scriptViewModel;
            Time = Environment.TickCount;
        }
    }
}
