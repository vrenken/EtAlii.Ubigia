namespace EtAlii.Servus.Api.Data
{
    internal static class ScriptProcessorFactory
    {
        public static IScriptProcessor Create()
        {
            return new ScriptProcessor();
        }
    }
}
