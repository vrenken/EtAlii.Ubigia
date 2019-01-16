
namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.xTechnology.Workflow;
    using System;

    public class ProcessGraphScriptLanguageUnitOfwork : UnitOfWorkBase<IProcessGraphScriptLanguageUnitOfworkHandler>
    {
        public IGraphScriptLanguageViewModel ScriptViewModel { get; }
        public int Time { get; }

        public ProcessGraphScriptLanguageUnitOfwork(IGraphScriptLanguageViewModel scriptViewModel)
        {
            ScriptViewModel = scriptViewModel;
            Time = Environment.TickCount;
        }
    }
}
