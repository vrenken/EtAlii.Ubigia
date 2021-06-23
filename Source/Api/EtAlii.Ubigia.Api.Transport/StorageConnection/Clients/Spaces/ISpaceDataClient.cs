// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// An interface that defines a client able to work with <see cref="Account"/> specific actions.
    /// </summary>
    public interface ISpaceDataClient : IStorageTransportClient
    {
        /// <summary>
        /// Add a <see cref="Account"/> using the specified id, name and <see cref="SpaceTemplate"/>. 
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="spaceName"></param>
        /// <param name="template"></param>
        /// <returns></returns>
        Task<Space> Add(Guid accountId, string spaceName, SpaceTemplate template);
        
        /// <summary>
        /// Remove a <see cref="Account"/> using its Id.
        /// </summary>
        /// <param name="spaceId"></param>
        /// <returns></returns>
        Task Remove(Guid spaceId);
        
        /// <summary>
        /// Change the name of the <see cref="Account"/> specified by the given Id. 
        /// </summary>
        /// <param name="spaceId"></param>
        /// <param name="spaceName"></param>
        /// <returns></returns>
        Task<Space> Change(Guid spaceId, string spaceName);
        
        /// <summary>
        /// Return the <see cref="Account"/> identified by the provided name.
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="spaceName"></param>
        /// <returns></returns>
        Task<Space> Get(Guid accountId, string spaceName);
        
        /// <summary>
        /// Return the <see cref="Account"/> identified by the provided Id.
        /// </summary>
        /// <param name="spaceId"></param>
        /// <returns></returns>
        Task<Space> Get(Guid spaceId);

        /// <summary>
        /// Return all <see cref="Account"/> instances known by the connected backend.
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        IAsyncEnumerable<Space> GetAll(Guid accountId);
    }

    /// <summary>
    /// An interface that defines a client able to work with <see cref="Account"/> specific actions.
    /// </summary>
    public interface ISpaceDataClient<in TTransport> : ISpaceDataClient, IStorageTransportClient<TTransport>
        where TTransport : IStorageTransport
    {
    }
}
