// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// Simple extension class to make flat, layman's serialization fun and performant.
    /// </summary>
    public static class BinaryReaderExtensions
    {
        public static object ReadTyped(this BinaryReader reader)
        {
            var typeId = (TypeId)reader.ReadByte();

            return typeId switch
            {
                TypeId.String => reader.ReadString(),
                TypeId.Char => reader.ReadChar(),
                TypeId.Boolean => reader.ReadBoolean(),
                TypeId.SByte => reader.ReadSByte(),
                TypeId.Byte => reader.ReadByte(),
                TypeId.Int16 => reader.ReadInt16(),
                TypeId.Int32 => reader.ReadInt32(),
                TypeId.Int64 => reader.ReadInt64(),
                TypeId.UInt16 => reader.ReadUInt16(),
                TypeId.UInt32 => reader.ReadUInt32(),
                TypeId.UInt64 => reader.ReadUInt64(),
                TypeId.Single => reader.ReadSingle(),
                TypeId.Double => reader.ReadDouble(),
                TypeId.Decimal => reader.ReadDecimal(),
                TypeId.DateTime => reader.ReadDateTime(),
                TypeId.TimeSpan => reader.ReadTimeSpan(),
                TypeId.Guid => reader.ReadGuid(),
                TypeId.Version => reader.ReadVersion(),
                TypeId.Range => reader.ReadRange(),

                TypeId.None => null,
                _ => throw new NotSupportedException($"TypeId is not supported: {typeId}")
            };

        }

        public static T ReadOptional<T>(this BinaryReader reader)
            where T : new()
        {
            var hasValue = reader.ReadBoolean();
            if (hasValue)
            {
                return Read<T>(reader);
            }

            return default;
        }

        public static T Read<T>(this BinaryReader reader)
        {
            var type = typeof(T);
            if (typeof(IBinarySerializable).IsAssignableFrom(type))
            {
                IBinarySerializable serializable;
                if (type.IsClass)
                {
                    #pragma warning disable S3011
                    // reasoning We explicitly want to get access to any constructor, either a public or a non public one.
                    var constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, null, Array.Empty<Type>(), Array.Empty<ParameterModifier>());
                    #pragma warning restore S3011

                    serializable = (IBinarySerializable)constructor!.Invoke(Array.Empty<object>());
                }
                else
                {
                    serializable = (IBinarySerializable)Activator.CreateInstance<T>();
                }
                serializable.Read(reader);
                return (T)serializable;
            }
            if (type == typeof(Identifier))
            {
                // We want to handle Identifiers somewhat differently
                // as they have a very strong and tightly optimized (immutable) structure.
                return (T)(object)Identifier.Read(reader);
            }

            object result = typeof(T) switch
            {
                _ when typeof(T) == typeof(string) => reader.ReadString(),
                _ when typeof(T) == typeof(int) => reader.ReadInt32(),
                _ when typeof(T) == typeof(uint) => reader.ReadUInt32(),
                _ when typeof(T) == typeof(short) => reader.ReadInt16(),
                _ when typeof(T) == typeof(ushort) => reader.ReadUInt16(),
                _ when typeof(T) == typeof(long) => reader.ReadInt64(),
                _ when typeof(T) == typeof(ulong) => reader.ReadUInt64(),
                _ when typeof(T) == typeof(char) => reader.ReadChar(),
                _ when typeof(T) == typeof(byte) => reader.ReadByte(),
                _ when typeof(T) == typeof(sbyte) => reader.ReadSByte(),
                _ when typeof(T) == typeof(float) => reader.ReadSingle(),
                _ when typeof(T) == typeof(double) => reader.ReadDouble(),
                _ when typeof(T) == typeof(Guid) => reader.ReadGuid(),
                _ when typeof(T) == typeof(bool) => reader.ReadBoolean(),
                _ when typeof(T) == typeof(decimal) => reader.ReadDecimal(),
                _ when typeof(T) == typeof(Range) => reader.ReadRange(),
                _ when typeof(T) == typeof(DateTime) => reader.ReadDateTime(),
                _ when typeof(T) == typeof(TimeSpan) => reader.ReadTimeSpan(),
                _ when typeof(T) == typeof(Version) => reader.ReadVersion(),
                _ => throw new ArgumentOutOfRangeException($"Unable to read {typeof(T)} from BinaryReader")
            };
            return (T)result;
        }

        public static TSerializable Read<TSerializable>(this BinaryReader reader, Func<BinaryReader, TSerializable> read)
        {
            return read(reader);
        }

        public static T[] ReadMany<T>(this BinaryReader reader)
        {
            var count = reader.ReadInt32();
            var result = new T[count];
            for (var i = 0; i < count; i++)
            {
                result[i] = Read<T>(reader);
            }
            return result;
        }

        private static Range ReadRange(this BinaryReader reader)
        {
            return new Range(reader.ReadInt32(), reader.ReadInt32());
        }

        private static Guid ReadGuid(this BinaryReader reader)
        {
            var bytes = reader.ReadBytes(16);
            return new Guid(bytes);
        }

        private static DateTime ReadDateTime(this BinaryReader reader)
        {
            var kind = (DateTimeKind)reader.ReadByte();
            var ticks = reader.ReadInt64();
            return new DateTime(ticks, kind);
        }
        private static TimeSpan ReadTimeSpan(this BinaryReader reader)
        {
            var ticks = reader.ReadInt64();
            return new TimeSpan(ticks);
        }

        private static Version ReadVersion(this BinaryReader reader)
        {
            var major = reader.ReadInt32();
            var minor = reader.ReadInt32();
            var build = reader.ReadInt32();
            var revision = reader.ReadInt32();
            return new Version(major, minor, build, revision);
        }
    }
}
