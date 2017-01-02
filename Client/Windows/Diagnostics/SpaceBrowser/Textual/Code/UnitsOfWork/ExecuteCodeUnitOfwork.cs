
namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using EtAlii.xTechnology.Workflow;
    using System;

    public class ExecuteCodeUnitOfwork : UnitOfWorkBase<ExecuteCodeUnitOfworkHandler>
    {
        public CodeViewModel CodeViewModel { get; private set; }
        public int Time { get; private set; }

        public ExecuteCodeUnitOfwork(CodeViewModel codeViewModel)
        {
            CodeViewModel = codeViewModel;
            Time = Environment.TickCount;
        }
    }
}
