namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;

    public interface IScriptProcessor
    {
        void Process(Script script, ScriptScope scope, IDataConnection connection);
    }
}
