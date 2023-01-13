// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence;

public interface IPropertiesStorage
{
    void Store(ContainerIdentifier container, PropertyDictionary properties, string name = "_Default");
    PropertyDictionary Retrieve(ContainerIdentifier container, string name = "_Default");
}
