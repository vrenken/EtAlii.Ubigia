namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using EtAlii.Ubigia.Api.Functional.Scripting;

    public class SchemaProcessorConfiguration : ConfigurationBase, ISchemaProcessorConfiguration
    {
        public ISchemaScope SchemaScope { get; private set; }
        
        public IGraphSLScriptContext ScriptContext { get; private set; }


        public SchemaProcessorConfiguration Use(ISchemaScope scope)
        {
            SchemaScope = scope ?? throw new ArgumentException("No scope specified", nameof(scope));
            return this;
        }

        public SchemaProcessorConfiguration Use(IGraphSLScriptContext scriptContext)
        {
            ScriptContext = scriptContext ?? throw new ArgumentException("No script context specified", nameof(scriptContext));
            return this;
        }
    }
}
