namespace EtAlii.Servus.Storage
{
    using EtAlii.Servus.Client.Model;
    using Microsoft.Experimental.IO;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;

    public partial class NtfsContainerStorage : NtfsStorageBase, IContainerStorage
    {
        public IEnumerable<T> RetrieveAll<T>(ContainerIdentifier container)
            where T : CompositeComponent
        {
            try
            {
                var componentName = ContentComponent.GetName<T>();

                container = ContainerIdentifier.Combine(container, componentName);

                var folder = GetFolder(container);

                var components = new List<T>();

                ulong count = 0;
                if (LongPathDirectory.Exists(folder))
                {
                    foreach (var fullFileName in LongPathDirectory.EnumerateFiles(folder))
                    {
                        count += 1;
                        if (count > 100)
                        {
                            Debugger.Break();
                        }
                        var component = LoadFromFile<T>(fullFileName);
                        var fileName = Path.GetFileNameWithoutExtension(fullFileName);
                        var id = UInt64.Parse(fileName);
                        CompositeComponent.SetId(component, id);
                        EntryComponentHelper.SetStored(component, true);
                        components.Add(component);
                    }
                }

                return components;
            }
            catch (Exception e)
            {
                throw new StorageException("Unable to retrieve components from the specified container", e);
            }
        }

        public T Retrieve<T>(ContainerIdentifier container)
            where T : NonCompositeComponent
        {
            try
            {
                var componentName = ContentComponent.GetName<T>();
                var folder = GetFolder(container);

                var component = LoadFromFolder<T>(folder, componentName);

                // Why is this if statement here? I don't like it.
                if (component != null)
                {
                    EntryComponentHelper.SetStored(component, true);
                }
                return component;
            }
            catch (Exception e)
            {
                throw new StorageException("Unable to retrieve components from the specified container", e);
            }
        }
    }
}
