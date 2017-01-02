namespace EtAlii.Servus.Api.Data
{
    using System;
    using Moppet.Lapa;
    using System.Collections.Generic;

    internal class ProcessingContext
    {
        public ScriptScope Scope { get; private set; }
        public IDataConnection Connection { get; private set; }

        public void Setup(ScriptScope scope, IDataConnection connection)
        {
            Scope = scope;
            Connection = connection;
        }
        
    }
}
