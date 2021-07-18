// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System;

    /// <summary>
    /// This interface represents identifiable, named types.
    /// </summary>
    public interface IIdentifiable
    {
        /// <summary>
        /// The unique Guid with which the item can be represented in the Ubigia systems.
        /// </summary>
        Guid Id { get; set; }
        /// <summary>
        /// The name with which the item can be represented in the Ubigia systems.
        /// </summary>
        string Name { get; }
    }
}
