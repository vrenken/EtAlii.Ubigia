namespace EtAlii.xTechnology.Hosting
{
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    public interface IHost : INotifyPropertyChanged
    {
        bool ShouldOutputLog { get; set; }
        LogLevel LogLevel { get; set; }

        IHostConfiguration Configuration { get; }
        Task Start();
        Task Stop();

        Task Shutdown();

        State State { get; }

        Status[] Status { get; }

        ICommand[] Commands { get; }

		ISystem[] Systems { get; }

        void Setup(ICommand[] commands, Status[] status);

        void Initialize();
    }
}
