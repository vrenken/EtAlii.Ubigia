// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
/// A context that wraps all <see cref="Storage"/> specific operations.
/// </summary>
public interface IStorageContext : IStorageClientContext
{
    /// <summary>
    /// Add a <see cref="Storage"/> using the specified name and address.
    /// </summary>
    /// <param name="storageName"></param>
    /// <param name="storageAddress"></param>
    /// <returns></returns>
    Task<Storage> Add(string storageName, string storageAddress);

    /// <summary>
    /// Remove a <see cref="Storage"/> using its Id.
    /// </summary>
    /// <param name="storageId"></param>
    /// <returns></returns>
    Task Remove(Guid storageId);

    /// <summary>
    /// Change the storage specified by the given storageId.
    /// </summary>
    /// <param name="storageId"></param>
    /// <param name="storageName"></param>
    /// <param name="storageAddress"></param>
    /// <returns></returns>
    Task<Storage> Change(Guid storageId, string storageName, string storageAddress);

    /// <summary>
    /// Return the <see cref="Storage"/> identified by the provided name.
    /// </summary>
    /// <param name="storageName"></param>
    /// <returns></returns>
    Task<Storage> Get(string storageName);

    /// <summary>
    /// Return the <see cref="Storage"/> identified by the provided Id.
    /// </summary>
    /// <param name="storageId"></param>
    /// <returns></returns>
    Task<Storage> Get(Guid storageId);

    /// <summary>
    /// Return all <see cref="Storage"/> instances known by the connected backend.
    /// </summary>
    /// <returns></returns>
    IAsyncEnumerable<Storage> GetAll();
}
