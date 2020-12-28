﻿namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.Threading.Tasks;

    internal interface ISchemaProcessor
    {
        Task<SchemaProcessingResult> Process(Schema schema);
    }
}