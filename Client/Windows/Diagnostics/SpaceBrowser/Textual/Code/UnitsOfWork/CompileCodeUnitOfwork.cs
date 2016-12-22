
namespace EtAlii.Servus.Client.Windows.Diagnostics
{
    using EtAlii.xTechnology.Workflow;
    using System;

    public class CompileCodeUnitOfwork : UnitOfWorkBase<CompileCodeUnitOfworkHandler>
    {
        public CodeViewModel CodeViewModel { get; private set; }
        public int Time { get; private set; }

        public CompileCodeUnitOfwork(CodeViewModel codeViewModel)
        {
            CodeViewModel = codeViewModel;
            Time = Environment.TickCount;
        }
    }
}
