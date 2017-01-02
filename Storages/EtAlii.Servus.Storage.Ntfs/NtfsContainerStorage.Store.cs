namespace EtAlii.Servus.Storage
{
    using EtAlii.Servus.Client.Model;
    using System;
    using System.Collections.Generic;

    public partial class NtfsContainerStorage : NtfsStorageBase, IContainerStorage
    {
        public void StoreAll<T>(ContainerIdentifier container, IEnumerable<T> components)
            where T : EntryComponent
        {
            foreach (var component in components)
            {
                Store(container, component);
            }
        }

        public void Store<T>(ContainerIdentifier container, T component)
            where T : EntryComponent
        {
            try
            {
                var componentName = EntryComponent.GetName(component);
                var compositeComponentId = (ulong)0;

                if (component is CompositeComponent)
                {
                    container = ContainerIdentifier.Combine(container, componentName);

                    var compositeComponent = component as CompositeComponent;
                    if (compositeComponent.Id != 0)
                    {
                        throw new InvalidOperationException("Unable to store composite component: Id already assigned");
                    }
                    //CompositeComponent.SetId(compositeComponent);
                    compositeComponentId = (ulong)System.DateTime.UtcNow.Ticks;
                    componentName = compositeComponentId.ToString();
                }

                var folder = GetFolder(container);

                LongPathHelper.Create(folder);

                EntryComponentHelper.SetStored(component, false);
                SaveToFolder(component, componentName, folder);
                EntryComponentHelper.SetStored(component, true);

                if (component is CompositeComponent)
                {
                    var compositeComponent = component as CompositeComponent;
                    CompositeComponent.SetId(compositeComponent, compositeComponentId);
                }
            }
            catch (Exception e)
            {
                throw new StorageException("Unable to store components in the specified container", e);
            }
        }
    }
}
