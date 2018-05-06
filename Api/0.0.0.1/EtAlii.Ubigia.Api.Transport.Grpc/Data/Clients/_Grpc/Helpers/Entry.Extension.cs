﻿namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Collections.Generic;
    using System.Linq;

    public static class EntryExtension 
    {
        public static Entry ToLocal(this WireProtocol.Entry entry)
        {
            IComponentEditableEntry result = Entry.NewEntry();
            result.IdComponent = entry.Id.ToLocal();
            
            result.ParentComponent = entry.Parent.ToLocal<ParentComponent>();
            foreach (var child in entry.Children)
            {
                result.ChildrenComponent.Add(child.Relations.ToLocal(), child.Stored);
            }
            
            result.Parent2Component = entry.Parent2.ToLocal<Parent2Component>();
            foreach (var child in entry.Children2)
            {
                result.Children2Component.Add(child.Relations.ToLocal(), child.Stored);
            }

            result.DowndateComponent = entry.Downdate.ToLocal<DowndateComponent>();
            foreach (var update in entry.Updates)
            {
                result.UpdatesComponent.Add(update.Relations.ToLocal(), update.Stored);
            }

            result.PreviousComponent = entry.Previous.ToLocal<PreviousComponent>();
            result.NextComponent = entry.Next.ToLocal<NextComponent>();

            result.IndexedComponent = entry.Indexed.ToLocal<IndexedComponent>();
            foreach (var index in entry.Indexes)
            {
                result.IndexesComponent.Add(index.Relations.ToLocal(), index.Stored);
            }

            return (Entry)result;
        }
        
        public static WireProtocol.Entry ToWire(this IComponentEditableEntry entry)
        {
            var result = new WireProtocol.Entry();
            result.Id = entry.IdComponent.ToWire();
            
            result.Parent = entry.ParentComponent.ToWire();
            result.Children.AddRange(entry.ChildrenComponent.ToWire());
            
            result.Parent2 = entry.Parent2Component.ToWire();
            result.Children2.AddRange(entry.Children2Component.ToWire());

            result.Downdate = entry.DowndateComponent.ToWire();
            result.Updates.AddRange(entry.UpdatesComponent.ToWire());

            result.Previous = entry.PreviousComponent.ToWire();
            result.Next = entry.NextComponent.ToWire();

            result.Indexed = entry.IndexedComponent.ToWire();
            result.Indexes.AddRange(entry.IndexesComponent.ToWire());

            return result;
        }
        
                
        public static IEnumerable<WireProtocol.Entry> ToWire(this IEnumerable<Entry> entries)
        {
            return entries.Select(id => id.ToWire());
        }
        
        public static IEnumerable<Entry> ToLocal(this IEnumerable<WireProtocol.Entry> entries)
        {
            return entries.Select(id => id.ToLocal());
        }
    }
}
