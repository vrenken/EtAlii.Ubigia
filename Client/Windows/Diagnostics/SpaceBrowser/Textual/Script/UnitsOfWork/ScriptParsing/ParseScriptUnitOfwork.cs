
namespace EtAlii.Servus.Client.Windows.Diagnostics
{
    using EtAlii.xTechnology.Workflow;
    using System;

    public class ParseScriptUnitOfwork : UnitOfWorkBase<ParseScriptUnitOfworkHandler>
    {
        public ScriptViewModel ScriptViewModel { get; private set; }
        public int Time { get; private set; }

        public ParseScriptUnitOfwork(ScriptViewModel scriptViewModel)
        {
            ScriptViewModel = scriptViewModel;
            Time = Environment.TickCount;
        }
    }
}
