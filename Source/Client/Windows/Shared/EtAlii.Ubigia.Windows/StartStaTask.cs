namespace EtAlii.Ubigia.Windows
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public static class StaHelper
    {
        public static Task StartStaTask(Action action)
        {
            var tcs = new TaskCompletionSource<object>();
            var thread = new Thread(() =>
            {
                try
                {
                    action();
                    tcs.SetResult(new object());
                }
                catch (Exception e)
                {
                    tcs.SetException(e);
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            return tcs.Task;
        }
    }
}