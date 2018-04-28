
namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.xTechnology.Workflow;
    using System;

    public class CompileCodeUnitOfwork : UnitOfWorkBase<ICompileCodeUnitOfworkHandler>
    {
        public ICodeViewModel CodeViewModel { get; }
        public int Time { get; }

        public CompileCodeUnitOfwork(ICodeViewModel codeViewModel)
        {
            CodeViewModel = codeViewModel;
            Time = Environment.TickCount;
        }
    }
}
