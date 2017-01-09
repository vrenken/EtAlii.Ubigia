
namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.xTechnology.Workflow;
    using System;

    public class ProcessScriptUnitOfwork : UnitOfWorkBase<IProcessScriptUnitOfworkHandler>
    {
        public IScriptViewModel ScriptViewModel { get; private set; }
        public int Time { get; private set; }

        public ProcessScriptUnitOfwork(IScriptViewModel scriptViewModel)
        {
            ScriptViewModel = scriptViewModel;
            Time = Environment.TickCount;
        }
    }
}
