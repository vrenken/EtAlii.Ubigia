
namespace EtAlii.Ubigia.Client.Windows.Diagnostics
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
