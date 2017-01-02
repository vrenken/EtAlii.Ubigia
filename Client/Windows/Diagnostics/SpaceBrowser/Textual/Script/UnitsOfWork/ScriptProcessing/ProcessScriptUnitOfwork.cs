
namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.xTechnology.Workflow;
    using System;

    public class ProcessScriptUnitOfwork : UnitOfWorkBase<ProcessScriptUnitOfworkHandler>
    {
        public ScriptViewModel ScriptViewModel { get; private set; }
        public int Time { get; private set; }

        public ProcessScriptUnitOfwork(ScriptViewModel scriptViewModel)
        {
            ScriptViewModel = scriptViewModel;
            Time = Environment.TickCount;
        }
    }
}
