﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence;

using System.IO;

public interface IPropertiesSerializer
{
    void Serialize(Stream stream, PropertyDictionary item);
    PropertyDictionary Deserialize(Stream stream);
}
