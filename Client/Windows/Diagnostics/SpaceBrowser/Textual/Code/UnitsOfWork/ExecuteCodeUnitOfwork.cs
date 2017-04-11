
namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.xTechnology.Workflow;
    using System;

    public class ExecuteCodeUnitOfwork : UnitOfWorkBase<IExecuteCodeUnitOfworkHandler>
    {
        public ICodeViewModel CodeViewModel { get; }
        public int Time { get; }

        public ExecuteCodeUnitOfwork(ICodeViewModel codeViewModel)
        {
            CodeViewModel = codeViewModel;
            Time = Environment.TickCount;
        }
    }
}
