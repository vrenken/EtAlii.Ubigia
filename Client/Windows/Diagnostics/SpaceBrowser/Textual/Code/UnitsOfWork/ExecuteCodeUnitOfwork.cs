
namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.xTechnology.Workflow;
    using System;

    public class ExecuteCodeUnitOfwork : UnitOfWorkBase<IExecuteCodeUnitOfworkHandler>
    {
        public ICodeViewModel CodeViewModel { get; private set; }
        public int Time { get; private set; }

        public ExecuteCodeUnitOfwork(ICodeViewModel codeViewModel)
        {
            CodeViewModel = codeViewModel;
            Time = Environment.TickCount;
        }
    }
}
