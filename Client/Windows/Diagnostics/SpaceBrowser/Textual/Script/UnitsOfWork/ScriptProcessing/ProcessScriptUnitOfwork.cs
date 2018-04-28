
namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.xTechnology.Workflow;
    using System;

    public class ProcessScriptUnitOfwork : UnitOfWorkBase<IProcessScriptUnitOfworkHandler>
    {
        public IScriptViewModel ScriptViewModel { get; }
        public int Time { get; }

        public ProcessScriptUnitOfwork(IScriptViewModel scriptViewModel)
        {
            ScriptViewModel = scriptViewModel;
            Time = Environment.TickCount;
        }
    }
}
