
namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.xTechnology.Workflow;
    using System;

    public class ParseScriptUnitOfwork : UnitOfWorkBase<IParseScriptUnitOfworkHandler>
    {
        public IScriptViewModel ScriptViewModel { get; }
        public int Time { get; }

        public ParseScriptUnitOfwork(IScriptViewModel scriptViewModel)
        {
            ScriptViewModel = scriptViewModel;
            Time = Environment.TickCount;
        }
    }
}
