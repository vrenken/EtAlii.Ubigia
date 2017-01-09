
namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.xTechnology.Workflow;
    using System;

    public class ParseScriptUnitOfwork : UnitOfWorkBase<IParseScriptUnitOfworkHandler>
    {
        public IScriptViewModel ScriptViewModel { get; private set; }
        public int Time { get; private set; }

        public ParseScriptUnitOfwork(IScriptViewModel scriptViewModel)
        {
            ScriptViewModel = scriptViewModel;
            Time = Environment.TickCount;
        }
    }
}
