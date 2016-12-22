namespace EtAlii.xTechnology.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;

    public static class BinarySerializerExtensions
    {
        public static void WriteTypedList<T>(this BinaryWriter writer, ICollection<T> list) where T : IBinarySerializable
        {
            if (writer == null) { throw new ArgumentNullException(@"writer"); }

            if (list != null)
            {
                writer.Write(list.Count);
                foreach (T item in list)
                {
                    Type type = item.GetType();
                    writer.Write(type.AssemblyQualifiedName);
                    item.Write(writer);
                }
            }
            else
            {
                writer.Write(0);
            }
        }

        public static void WriteList<T>(this BinaryWriter writer, ICollection<T> list) where T : IBinarySerializable
        {
            if (writer == null) { throw new ArgumentNullException(@"writer"); }

            if (list != null)
            {
                writer.Write(list.Count);
                foreach (T item in list)
                {
                    item.Write(writer);
                }
            }
            else
            {
                writer.Write(0);
            }
        }

        public static void WriteList(this BinaryWriter writer, ICollection<string> list)
        {
            if (writer == null) { throw new ArgumentNullException(@"writer"); }

            if (list != null)
            {
                writer.Write(list.Count);
                foreach (string item in list)
                {
                    writer.Write(item);
                }
            }
            else
            {
                writer.Write(0);
            }
        }

        public static void Write<T>(this BinaryWriter writer, T value) where T : IBinarySerializable
        {
            if (writer == null) { throw new ArgumentNullException(@"writer"); }

            if (value != null)
            {
                writer.Write(true);
                value.Write(writer);
            }
            else
            {
                writer.Write(false);
            }
        }

        public static void Write(this BinaryWriter writer, Guid value)
        {
            if (writer == null) { throw new ArgumentNullException(@"writer"); }

            byte[] bytes = value.ToByteArray();
            writer.Write(bytes);
        }

        public static void Write(this BinaryWriter writer, DateTime value)
        {
            if (writer == null) { throw new ArgumentNullException(@"writer"); }

            writer.Write(value.Ticks);
        }

        public static void Write(this BinaryWriter writer, TimeSpan value)
        {
            if (writer == null) { throw new ArgumentNullException(@"writer"); }

            writer.Write(value.Ticks);
        }

        public static void WriteEnum(this BinaryWriter writer, System.Enum value)
        {
            if (writer == null) { throw new ArgumentNullException(@"writer"); }
            if (value == null) { throw new ArgumentNullException(@"value"); }

            writer.Write(value.ToString());
        }

        public static void WriteString(this BinaryWriter writer, string value)
        {
            if (writer == null) { throw new ArgumentNullException(@"writer"); }

            writer.Write(value ?? string.Empty);
        }

        public static T ReadGeneric<T>(this BinaryReader reader) where T : IBinarySerializable, new()
        {
            if (reader == null) { throw new ArgumentNullException(@"reader"); }

            if (reader.ReadBoolean())
            {
                T result = new T();
                result.Read(reader);
                return result;
            }
            return default(T);
        }

        public static Collection<string> ReadList(this BinaryReader reader)
        {
            if (reader == null) { throw new ArgumentNullException(@"reader"); }

            Collection<string> list = new Collection<string>();

            int count = reader.ReadInt32(); //9
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    list.Add(reader.ReadString());
                }
            }

            return list;
        }

        public static Collection<T> ReadTypedList<T>(this BinaryReader reader) where T : IBinarySerializable, new()
        {
            if (reader == null) { throw new ArgumentNullException(@"reader"); }

            Collection<T> list = new Collection<T>();

            int count = reader.ReadInt32();
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    string fullQualifiedTypeName = reader.ReadString();
                    Type type = Type.GetType(fullQualifiedTypeName);
                    T item = (T)Activator.CreateInstance(type);
                    item.Read(reader);
                    list.Add(item);
                }
            }

            return list;
        }


        public static Collection<T> ReadList<T>(this BinaryReader reader) where T : IBinarySerializable, new()
        {
            if (reader == null) { throw new ArgumentNullException(@"reader"); }

            Collection<T> list = new Collection<T>();

            int count = reader.ReadInt32(); 
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    T item = new T();
                    item.Read(reader);
                    list.Add(item);
                }
            }

            return list;
        }

        public static Guid ReadGuid(this BinaryReader reader)
        {
            if (reader == null) { throw new ArgumentNullException(@"reader"); }

            byte[] bytes = reader.ReadBytes(16);
            return new Guid(bytes);
        }

        public static DateTime ReadDateTime(this BinaryReader reader)
        {
            if (reader == null) { throw new ArgumentNullException(@"reader"); }

            var int64 = reader.ReadInt64();
            return new DateTime(int64);
        }

        public static TimeSpan ReadTimeSpan(this BinaryReader reader)
        {
            if (reader == null) { throw new ArgumentNullException(@"reader"); }

            var int64 = reader.ReadInt64();
            return new TimeSpan(int64);
        }

        public static T ReadEnum<T>(this BinaryReader reader)
        {
            if (reader == null) { throw new ArgumentNullException(@"reader"); }

            string type = reader.ReadString();
            return (T)Enum.Parse(typeof(T), type, true);
        }
    }
}
