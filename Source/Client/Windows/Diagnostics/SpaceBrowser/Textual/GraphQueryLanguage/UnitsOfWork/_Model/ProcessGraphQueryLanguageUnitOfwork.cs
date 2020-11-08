
namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.xTechnology.Structure.Workflow;
    using System;

    public class ProcessGraphQueryLanguageUnitOfwork : UnitOfWorkBase<IProcessGraphQueryLanguageUnitOfworkHandler>
    {
        public IGraphQueryLanguageViewModel QueryViewModel { get; }
        public int Time { get; }

        public ProcessGraphQueryLanguageUnitOfwork(IGraphQueryLanguageViewModel queryViewModel)
        {
            QueryViewModel = queryViewModel;
            Time = Environment.TickCount;
        }
    }
}
