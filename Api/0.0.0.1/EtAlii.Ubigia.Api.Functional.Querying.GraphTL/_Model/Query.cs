﻿namespace EtAlii.Ubigia.Api.Functional
{
    /// <summary>
    /// The Query class contains all information needed to execute graph traversal actions on the current infrastructureClient.
    /// </summary>
    public class Query
    {
        public ObjectDefinition ObjectDefinition { get; }

        public Query(ObjectDefinition objectDefinition)
        {
            ObjectDefinition = objectDefinition;
        }
    }
}
