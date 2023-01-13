// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport;

using System.Threading.Tasks;

/// <summary>
/// An interface that defines a client able to work with <see cref="Content"/> specific actions.
/// </summary>
public interface IContentDataClient : ISpaceTransportClient
{
    /// <summary>
    /// Store the <see cref="ContentDefinition"/> using the specified identifier.
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="contentDefinition"></param>
    /// <returns></returns>
    Task StoreDefinition(Identifier identifier, ContentDefinition contentDefinition);

    /// <summary>
    /// Store the <see cref="ContentDefinitionPart"/> using the specified identifier.
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="contentDefinitionPart"></param>
    /// <returns></returns>
    Task StoreDefinition(Identifier identifier, ContentDefinitionPart contentDefinitionPart);

    /// <summary>
    /// Retrieve the <see cref="ContentDefinition"/> stored in the specified identifier.
    /// </summary>
    /// <param name="identifier"></param>
    /// <returns></returns>
    Task<ContentDefinition> RetrieveDefinition(Identifier identifier);

    /// <summary>
    /// Store the specified <see cref="Content"/> in the given <see cref="Identifier"/>.
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    Task Store(Identifier identifier, Content content);

    /// <summary>
    /// Store the specified <see cref="ContentPart"/> in the given <see cref="Identifier"/>.
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="contentPart"></param>
    /// <returns></returns>
    Task Store(Identifier identifier, ContentPart contentPart);

    /// <summary>
    /// Retrieve the <see cref="Content"/> stored in the specified identifier.
    /// </summary>
    /// <param name="identifier"></param>
    /// <returns></returns>
    Task<Content> Retrieve(Identifier identifier);

    /// <summary>
    /// Retrieve the <see cref="Content"/> as registered under <see cref="contentPartId"/> in the specified identifier.
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="contentPartId"></param>
    /// <returns></returns>
    Task<ContentPart> Retrieve(Identifier identifier, ulong contentPartId);
}

/// <summary>
/// An interface that defines a  client able to work with <see cref="Content"/> specific actions.
/// </summary>
public interface IContentDataClient<in TTransport> : IContentDataClient, ISpaceTransportClient<TTransport>
    where TTransport: ISpaceTransport
{

}
