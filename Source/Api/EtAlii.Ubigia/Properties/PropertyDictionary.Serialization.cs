// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia;

using System.IO;

public sealed partial class PropertyDictionary
{
    public static void Write(BinaryWriter writer, PropertyDictionary item)
    {
        writer.Write(item.Count);
        foreach (var kvp in item)
        {
            writer.Write(kvp.Key);
            writer.WriteTyped(kvp.Value);
        }
    }

    public static PropertyDictionary Read(BinaryReader reader)
    {
        var result = new PropertyDictionary();
        var count = reader.ReadInt32();
        for (var i = 0; i < count; i++)
        {
            var key = reader.ReadString();
            var value = reader.ReadTyped();
            result.Add(key, value);
        }
        return result;
    }
}
