
namespace EtAlii.xTechnology.Diagnostics.Serilog
{
    using System;
    using global::Serilog;

    public class SerilogLogFactory : ILogFactory
    {
        static SerilogLogFactory()
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.WithThreadName()
                .Enrich.WithThreadId()
                .Enrich.WithProcessName()
                .Enrich.WithProcessId()
                .Enrich.WithMachineName()
                .Enrich.WithEnvironmentUserName()
                .Enrich.WithAssemblyName()
                .Enrich.WithAssemblyVersion()
                .Enrich.WithMemoryUsage()
                .WriteTo.Async(writeTo =>
                {
                    writeTo.Seq("http://vrenken.duckdns.org:5341");
                    writeTo.Debug();
                })
                .CreateLogger();

            AppDomain.CurrentDomain.UnhandledException += (o, e) =>
            {
                Log.Error((Exception) e.ExceptionObject, "Unhandled exception");
            };
        }
        
        public global::EtAlii.xTechnology.Diagnostics.ILogger Create(string name, string category)
        {
            return new SerilogLogger(name, category);
        }
    }
}
