﻿namespace EtAlii.Ubigia.Api.Functional.Querying
{
    internal class RootQueryExecutorFactory : IRootQueryExecutorFactory
    {
        //private readonly IScriptProcessorFactory _scriptProcessorFactory

        //public RootQueryExecutorFactory()//IScriptProcessorFactory scriptProcessorFactory)
        //[
            //_scriptProcessorFactory = scriptProcessorFactory
        //]

        public IRootQueryExecutor Create()
        {
            return new RootQueryExecutor();//_scriptProcessorFactory)
        }
    }
}