﻿namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using EtAlii.Ubigia.Api.Functional.Scripting;

    public class SchemaProcessorConfiguration : Configuration, ISchemaProcessorConfiguration
    {
        public ISchemaScope SchemaScope { get; private set; }
        
        public IGraphSLScriptContext ScriptContext { get; private set; }


        public SchemaProcessorConfiguration Use(ISchemaScope scope)
        {
            SchemaScope = scope ?? throw new ArgumentException(nameof(scope));
            return this;
        }

        public SchemaProcessorConfiguration Use(IGraphSLScriptContext scriptContext)
        {
            ScriptContext = scriptContext ?? throw new ArgumentException(nameof(scriptContext));
            return this;
        }
    }
}
