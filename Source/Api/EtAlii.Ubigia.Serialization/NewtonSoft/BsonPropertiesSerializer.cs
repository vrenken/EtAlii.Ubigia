// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Serialization
{
    using System.IO;
    using System.Text;

    public sealed class BsonPropertiesSerializer : IPropertiesSerializer
    {
        public void Serialize(Stream stream, PropertyDictionary item)
        {
            using var writer = new BinaryWriter(stream, Encoding.Default, true);

            writer.Write(item, PropertyDictionary.Write);
        }

        public PropertyDictionary Deserialize(Stream stream)
        {
            using var reader = new BinaryReader(stream, Encoding.Default, true);
            return reader.Read(PropertyDictionary.Read);
        }
    }
}
