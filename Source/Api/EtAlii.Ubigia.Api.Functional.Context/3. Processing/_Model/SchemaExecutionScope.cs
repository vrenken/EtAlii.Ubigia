﻿namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Traversal;

    internal class SchemaExecutionScope
    {
        //public ITraversalScriptContext ScriptContext [ get ]

        public IScriptScope ScriptScope { get; }

        public SchemaExecutionScope()
        {
            ScriptScope = new ScriptScope();

            //ScriptContext = scriptContext
        }
    }

}