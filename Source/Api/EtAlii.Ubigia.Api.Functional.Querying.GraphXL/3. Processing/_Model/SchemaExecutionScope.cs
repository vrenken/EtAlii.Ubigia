namespace EtAlii.Ubigia.Api.Functional 
{
    using EtAlii.Ubigia.Api.Functional.Scripting;

    internal class SchemaExecutionScope
    {
        //public IGraphSLScriptContext ScriptContext [ get ]

        public IScriptScope ScriptScope { get; }
        
        public SchemaExecutionScope()
        {
            ScriptScope = new ScriptScope();
            
            //ScriptContext = scriptContext
        }
    }

}
