// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Serialization
{
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    public sealed class BsonItemSerializer : IItemSerializer
    {
        public void Serialize<T>(Stream stream, T item)
            where T : class
        {
            using var writer = new BinaryWriter(stream, Encoding.Default, true);

            writer.Write<T>(item);
        }

        public Task<T> Deserialize<T>(Stream stream)
            where T : class
        {
            using var reader = new BinaryReader(stream, Encoding.Default, true);

            var result = reader.Read<T>();
            return Task.FromResult(result);
        }
    }
}
