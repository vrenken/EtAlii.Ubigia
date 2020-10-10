namespace EtAlii.xTechnology.Hosting.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using Microsoft.Extensions.Configuration;

    public class LoggingInstanceCreator : IInstanceCreator
    {
        private readonly IInstanceCreator _decoree;
        private readonly ILogger _logger;

        public LoggingInstanceCreator(IInstanceCreator decoree, ILogger logger)
        {
            _decoree = decoree;
            _logger = logger;
        }

        public bool TryCreate<TInstance>(
            IConfigurationSection configuration, 
            IConfigurationDetails configurationDetails, 
            string name, out TInstance instance)
        {
            var factoryTypeName = configuration.GetValue<string>("Factory");

            _logger.Info($"Creating instance for {name ?? "NULL"} using factory: {factoryTypeName ?? "NULL"}");

            var result = _decoree.TryCreate(configuration, configurationDetails, name, out instance);
            
            _logger.Info($"Created instance: {(result ? "TRUE" : "FALSE")}");

            return result;
        }

        public bool TryCreate<TInstance>(
            IConfigurationSection configuration, 
            IConfigurationDetails configurationDetails, string name,
            out TInstance instance, bool throwOnNoFactory)
        {
            var factoryTypeName = configuration.GetValue<string>("Factory");

            _logger.Info($"Creating instance for {name ?? "NULL"} using factory: {factoryTypeName ?? "NULL"}");

            var result = _decoree.TryCreate(configuration, configurationDetails, name, out instance, throwOnNoFactory);
            
            _logger.Info($"Created instance: {(result ? "TRUE" : "FALSE")}");

            return result;
        }
    }
}