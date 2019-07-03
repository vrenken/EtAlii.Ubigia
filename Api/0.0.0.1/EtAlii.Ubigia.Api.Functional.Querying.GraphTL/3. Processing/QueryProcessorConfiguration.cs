namespace EtAlii.Ubigia.Api.Functional 
{
    using System;

    public class QueryProcessorConfiguration : Configuration, IQueryProcessorConfiguration
    {
        public IQueryScope QueryScope { get; private set; }
        
        public IGraphSLScriptContext ScriptContext { get; private set; }

        public QueryProcessorConfiguration()
        {
        }


        public QueryProcessorConfiguration Use(IQueryScope scope)
        {
            QueryScope = scope ?? throw new ArgumentException(nameof(scope));
            return this;
        }

        public QueryProcessorConfiguration Use(IGraphSLScriptContext scriptContext)
        {
            ScriptContext = scriptContext ?? throw new ArgumentException(nameof(scriptContext));
            return this;
        }
    }
}
