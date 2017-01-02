namespace EtAlii.Servus.Infrastructure.Model
{
    using EtAlii.Servus.Client.Model;
    using EtAlii.Servus.Storage;
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class EntryHelper
    {
        public static IEnumerable<ContainerComponent> Decompose(Entry entry)
        {
            var components = new List<ContainerComponent>();
            
            components.Add(new EntryComponent(entry.Id)); 

            Add(entry.Parent, components, CreateParentComponent);
            AddAll(entry.Children, components, CreateChildrenComponent);
            Add(entry.Previous, components, CreatePreviousComponent);
            Add(entry.Next, components, CreateNextComponent);
            Add(entry.Downdate, components, CreateDowndateComponent);
            AddAll(entry.Updates, components, CreateUpdatesComponent);

            if (!String.IsNullOrEmpty(entry.Label))
            {
                components.Add(new LabelComponent(entry.Label));
            }

            return components;
        }

        public static Entry Compose(IEnumerable<ContainerComponent> components)
        {
            var entry = (IEditableEntry)Entry.NewEntry();

            foreach (var component in components)
            {
                component.Apply(entry);
            }

            return (Entry)entry;
        }


        private static void Add<T>(Relation relation, List<ContainerComponent> components, Func<Relation, T> createComponent)
            where T : ContainerComponent
        {
            if (relation != Relation.None)
            {
                var component = createComponent(relation);
                components.Add(component);
            }
        }

        private static void AddAll<T>(IEnumerable<Relation> relations, List<ContainerComponent> components, Func<IEnumerable<Relation>, T> createComponent)
            where T : ContainerComponent
        {
            if (relations != null && relations.Any())
            {
                var component = createComponent(relations);
                components.Add(component);
            }
        }

        private static ContainerComponent CreateParentComponent(Relation parent)
        {
            return new ParentComponent(parent);
        }

        private static ContainerComponent CreateChildrenComponent(IEnumerable<Relation> children)
        {
            return new ChildrenComponent(children);
        }
        
        private static ContainerComponent CreatePreviousComponent(Relation parent)
        {
            return new PreviousComponent(parent);
        }
        private static ContainerComponent CreateNextComponent(Relation parent)
        {
            return new NextComponent(parent);
        }

        private static ContainerComponent CreateUpdatesComponent(IEnumerable<Relation> updates)
        {
            return new UpdatesComponent(updates);
        }

        private static ContainerComponent CreateDowndateComponent(Relation parent)
        {
            return new DowndateComponent(parent);
        }
    }
}