
namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.xTechnology.Structure.Workflow;
    using System;

    public class ParseGraphQueryLanguageUnitOfwork : UnitOfWorkBase<IParseGraphQueryLanguageUnitOfworkHandler>
    {
        public IGraphQueryLanguageViewModel ScriptViewModel { get; }
        public int Time { get; }

        public ParseGraphQueryLanguageUnitOfwork(IGraphQueryLanguageViewModel scriptViewModel)
        {
            ScriptViewModel = scriptViewModel;
            Time = Environment.TickCount;
        }
    }
}
