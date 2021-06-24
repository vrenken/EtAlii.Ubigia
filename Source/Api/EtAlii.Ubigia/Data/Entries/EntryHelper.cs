// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System.Collections.Generic;
    using System.Linq;

    public static class EntryHelper
    {
        /// <summary>
        /// Apply the component on the specified entry.
        /// </summary>
        /// <param name="component"></param>
        /// <param name="entry"></param>
        /// <param name="markAsStored"></param>
        private static void Apply(IComponent component, IComponentEditableEntry entry, bool markAsStored)
        {
            ((ComponentBase)component).Apply(entry, markAsStored);
        }

        /// <summary>
        /// Decompose the editable entry in its components.
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public static IEnumerable<IComponent> Decompose(IComponentEditableEntry entry)
        {
            var components = new List<IComponent>
            {
                entry.IdComponent
            };

            // Hierarchical
            Add(components, entry.ParentComponent, entry.ParentComponent.Relation);
            Add(components, entry.Parent2Component, entry.Parent2Component.Relation);
            AddAll(components, entry.ChildrenComponent);
            AddAll(components, entry.Children2Component);

            // Sequential
            Add(components, entry.PreviousComponent, entry.PreviousComponent.Relation);
            Add(components, entry.NextComponent, entry.NextComponent.Relation);

            // Temporal
            Add(components, entry.DowndateComponent, entry.DowndateComponent.Relation);
            AddAll(components, entry.UpdatesComponent);

            // Indexed
            AddAll(components, entry.IndexesComponent);
            Add(components, entry.IndexedComponent, entry.IndexedComponent.Relation);

            if (!string.IsNullOrEmpty(entry.TypeComponent.Type))
            {
                components.Add(entry.TypeComponent);
            }

            if (!string.IsNullOrEmpty(entry.TagComponent.Tag))
            {
                components.Add(entry.TagComponent);
            }

            return components;
        }

        /// <summary>
        /// Compose a entry of the specified components.
        /// </summary>
        /// <param name="components"></param>
        /// <param name="markAsStored"></param>
        /// <returns></returns>
        public static Entry Compose(IEnumerable<IComponent> components, bool markAsStored = false)
        {
            var entry = (IComponentEditableEntry)Entry.NewEntry();

            foreach (var component in components)
            {
                Apply(component, entry, markAsStored);
            }

            return (Entry)entry;
        }

        private static void Add<T>(List<IComponent> components, T component, Relation relation)
            where T : NonCompositeComponent
        {
            if (relation != Relation.None)
            {
                components.Add(component);
            }
        }

        private static void AddAll<T>(List<IComponent> components, IEnumerable<T> componentsCollection)
            where T : IComponent
        {
            components.AddRange(componentsCollection.Select(component => (IComponent) component));
        }
    }
}
