namespace EtAlii.xTechnology.Hosting.Diagnostics
{
    using Microsoft.Extensions.Configuration;
    using Serilog;

    public class LoggingInstanceCreator : IInstanceCreator
    {
        private readonly IInstanceCreator _decoree;
        private readonly ILogger _logger = Log.ForContext<IInstanceCreator>();

        public LoggingInstanceCreator(IInstanceCreator decoree)
        {
            _decoree = decoree;
        }

        public bool TryCreate<TInstance>(
            IConfigurationSection configuration, 
            IConfigurationDetails configurationDetails, 
            string name, out TInstance instance)
        {
            var factoryTypeName = configuration?.GetValue<string>("Factory");

            _logger.Information("Creating instance for {Name} using factory: {FactoryTypeName}", name, factoryTypeName);

            var result = _decoree.TryCreate(configuration, configurationDetails, name, out instance);
            
            _logger.Information("Created instance: {Success}", result);

            return result;
        }

        public bool TryCreate<TInstance>(
            IConfigurationSection configuration, 
            IConfigurationDetails configurationDetails, string name,
            out TInstance instance, bool throwOnNoFactory)
        {
            var factoryTypeName = configuration.GetValue<string>("Factory");

            _logger.Information("Creating instance for {Name} using factory: {FactoryTypeName}", name, factoryTypeName);

            var result = _decoree.TryCreate(configuration, configurationDetails, name, out instance, throwOnNoFactory);
            
            _logger.Information("Created instance: {Success}", result);

            return result;
        }
    }
}