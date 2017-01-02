namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;

    public interface IScriptParser
    {
        Script Parse(string text);
    }
}
