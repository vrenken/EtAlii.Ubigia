namespace EtAlii.Ubigia.Api.Functional 
{
    internal class QueryExecutionScope
    {
        //public IGraphSLScriptContext ScriptContext { get; }

        public IScriptScope ScriptScope { get; }
        
        public QueryExecutionScope()
        {
            ScriptScope = new ScriptScope();
            
            //ScriptContext = scriptContext;
        }
    }

}
