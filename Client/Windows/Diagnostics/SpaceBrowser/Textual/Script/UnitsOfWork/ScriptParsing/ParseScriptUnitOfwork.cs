
namespace EtAlii.Ubigia.Client.Windows.Diagnostics
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
