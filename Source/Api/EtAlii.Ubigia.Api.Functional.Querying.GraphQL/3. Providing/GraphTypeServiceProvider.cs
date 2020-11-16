namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using System;
    using System.Collections.Generic;
    using GraphQL.Types;

    public class GraphTypeServiceProvider : IGraphTypeServiceProvider
    {
        private readonly IDictionary<System.Type, GraphType> _types = new Dictionary<System.Type, GraphType>();

        public object GetService(System.Type serviceType)
        {
            if (_types.TryGetValue(serviceType, out var graphType))
            {
                return graphType;
            }

            return Activator.CreateInstance(serviceType);
        }

        public void Register(GraphType graphType)
        {
            _types[graphType.GetType()] = graphType;
        }
    }
}