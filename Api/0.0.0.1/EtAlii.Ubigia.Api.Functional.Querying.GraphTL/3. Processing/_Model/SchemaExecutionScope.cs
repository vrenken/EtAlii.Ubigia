namespace EtAlii.Ubigia.Api.Functional 
{
    internal class SchemaExecutionScope
    {
        //public IGraphSLScriptContext ScriptContext { get; }

        public IScriptScope ScriptScope { get; }
        
        public SchemaExecutionScope()
        {
            ScriptScope = new ScriptScope();
            
            //ScriptContext = scriptContext;
        }
    }

}
