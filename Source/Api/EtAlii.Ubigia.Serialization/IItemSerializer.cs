// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Serialization
{
    using System.IO;
    using System.Threading.Tasks;

    public interface IItemSerializer
    {
        void Serialize<T>(Stream stream, T item)
            where T : class;
        Task<T> Deserialize<T>(Stream stream)
            where T : class;
    }
}
