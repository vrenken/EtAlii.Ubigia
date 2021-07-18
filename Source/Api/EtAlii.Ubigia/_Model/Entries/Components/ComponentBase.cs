// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    public abstract class ComponentBase : IComponent
    {
        /// <summary>
        /// Returns true when the component has been stored.
        /// </summary>
        public bool Stored { get; internal set; }

        /// <summary>
        /// Returns the type name of the component.
        /// </summary>
        protected internal abstract string Name { get; }

        /// <summary>
        /// Apply this component to the provided entry.
        /// Set markAsStored to true to indicate that the component has been stored on the server.
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="markAsStored"></param>
        protected internal abstract void Apply(IComponentEditableEntry entry, bool markAsStored);
    }
}
