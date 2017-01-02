namespace EtAlii.Servus.Api.Data
{
    public interface IScriptProcessor
    {
        void Process(Script script, ScriptScope scope, IDataConnection connection);
    }
}
