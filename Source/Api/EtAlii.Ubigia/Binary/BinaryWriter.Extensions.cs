// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System;
    using System.IO;

    /// <summary>
    /// Simple extension class to make flat, layman's serialization fun and performant.
    /// </summary>
    public static class BinaryWriterExtensions
    {
        public static void Write(this BinaryWriter writer, Range item)
        {
            writer.Write(item.Start.Value);
            writer.Write(item.End.Value); // We do not support from end ranges.
        }

        public static void WriteOptional<T>(this BinaryWriter writer, T item)
            where T: class
        {
            if (item != null)
            {
                writer.Write(true);
                Write<T>(writer, item);
            }
            else
            {
                writer.Write(false);
            }
        }

        public static void WriteOptional<T>(this BinaryWriter writer, T? item)
            where T: struct
        {
            if (item.HasValue)
            {
                writer.Write(true);
                Write<T>(writer, item.Value);
            }
            else
            {
                writer.Write(false);
            }
        }

        public static void Write(this BinaryWriter writer, IBinarySerializable item)
        {
            item.Write(writer);
        }

        public static void Write<T>(this BinaryWriter writer, object item)
        {
            if (item is IBinarySerializable serializable)
            {
                serializable.Write(writer);
                return;
            }

            switch(typeof(T))
            {
                case { } t when t == typeof(string): writer.Write((string)item); break;
                case { } t when t == typeof(int): writer.Write((int)item); break;
                case { } t when t == typeof(uint): writer.Write((uint)item); break;
                case { } t when t == typeof(short): writer.Write((short)item); break;
                case { } t when t == typeof(ushort): writer.Write((ushort)item); break;
                case { } t when t == typeof(long): writer.Write((long)item); break;
                case { } t when t == typeof(ulong): writer.Write((ulong)item); break;
                case { } t when t == typeof(char): writer.Write((char)item); break;
                case { } t when t == typeof(byte): writer.Write((byte)item); break;
                case { } t when t == typeof(sbyte): writer.Write((sbyte)item); break;
                case { } t when t == typeof(float): writer.Write((float)item); break;
                case { } t when t == typeof(double): writer.Write((double)item); break;
                case { } t when t == typeof(Guid): writer.Write((Guid)item); break;
                case { } t when t == typeof(bool): writer.Write((bool)item); break;
                case { } t when t == typeof(decimal): writer.Write((decimal)item); break;
                case { } t when t == typeof(Range): writer.Write((Range)item); break;
                case { } t when t == typeof(DateTime): writer.Write((DateTime)item); break;
                case { } t when t == typeof(TimeSpan): writer.Write((TimeSpan)item); break;
                case { } t when t == typeof(Version): writer.Write((Version)item); break;
                default: throw new ArgumentOutOfRangeException($"Unable to write {typeof(T)} from BinaryWriter");
            }
        }

        public static void Write(this BinaryWriter writer, Guid item)
        {
            writer.Write(item.ToByteArray());
        }

        public static void Write(this BinaryWriter writer, DateTime item)
        {
            writer.Write((byte)item.Kind);
            writer.Write(item.Ticks);
        }

        public static void Write(this BinaryWriter writer, TimeSpan item)
        {
            writer.Write(item.Ticks);
        }

        public static void Write(this BinaryWriter writer, Version item)
        {
            writer.Write(item.Major);
            writer.Write(item.Minor);
            writer.Write(item.Build);
            writer.Write(item.Revision);
        }

        public static void WriteTyped(this BinaryWriter writer, object item)
        {
            var typeId = TypeIdConverter.ToTypeId(item);
            writer.Write((byte)typeId);

            switch(typeId)
            {
                case TypeId.String: writer.Write((string)item); break;
                case TypeId.Char: writer.Write((char)item); break;
                case TypeId.Boolean: writer.Write((bool)item); break;
                case TypeId.SByte: writer.Write((sbyte)item); break;
                case TypeId.Byte: writer.Write((byte)item); break;
                case TypeId.Int16: writer.Write((short)item); break;
                case TypeId.Int32: writer.Write((int)item); break;
                case TypeId.Int64: writer.Write((long)item); break;
                case TypeId.UInt16: writer.Write((ushort)item); break;
                case TypeId.UInt32: writer.Write((uint)item); break;
                case TypeId.UInt64: writer.Write((ulong)item); break;
                case TypeId.Single: writer.Write((float)item); break;
                case TypeId.Double: writer.Write((double)item); break;
                case TypeId.Decimal: writer.Write((decimal)item); break;
                case TypeId.DateTime: writer.Write((DateTime)item); break;
                case TypeId.TimeSpan: writer.Write((TimeSpan)item); break;
                case TypeId.Guid: writer.Write((Guid)item); break;
                case TypeId.Version: writer.Write((Version)item); break;
                case TypeId.Range: writer.Write((Range)item); break;
                case TypeId.None: break;
                default: throw new NotSupportedException($"TypeId is not supported: {typeId}");
            }
        }

        public static void Write<TSerializable>(this BinaryWriter writer, TSerializable item, Action<BinaryWriter, TSerializable> write)
        {
            write(writer, item);
        }

        public static void WriteMany<T>(this BinaryWriter writer, T[] items)
        {
            writer.Write(items.Length);

            foreach (var item in items)
            {
                Write<T>(writer, item);
            }
        }

        public static void WriteMany<TSerializable>(this BinaryWriter writer, TSerializable[] items, Action<BinaryWriter, TSerializable> write)
        {
            writer.Write(items.Length);

            foreach (var item in items)
            {
                write(writer, item);
            }
        }
    }
}
