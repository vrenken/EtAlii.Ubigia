// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System.IO;

    public interface IBinarySerializable
    {
        void Write(BinaryWriter writer);

        void Read(BinaryReader reader);
    }
}
