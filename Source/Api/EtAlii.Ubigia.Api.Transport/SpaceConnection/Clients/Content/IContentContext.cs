// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    /// <summary>
    /// A context that wraps <see cref="Content"/> specific space actions and notifications.
    /// </summary>
    public interface IContentContext : ISpaceClientContext
    {
        /// <summary>
        /// A instance that enables work with <see cref="Content"/> specific actions.
        /// </summary>
        IContentDataClient Data { get; }
    }
}
