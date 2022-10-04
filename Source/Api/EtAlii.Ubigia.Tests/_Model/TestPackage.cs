namespace EtAlii.Ubigia.Tests
{
    using System.IO;

    public class TestPackage<T> : IBinarySerializable
    {
        public T Value { get; set; }

        public void Write(BinaryWriter writer)
        {
            writer.Write<T>(Value);
        }
        public void Read(BinaryReader reader)
        {
            Value = reader.Read<T>();
        }
    }
}
