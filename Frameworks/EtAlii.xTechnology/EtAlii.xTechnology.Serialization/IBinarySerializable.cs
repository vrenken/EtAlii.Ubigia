namespace EtAlii.xTechnology.Serialization
{
    using System.IO;

    public interface IBinarySerializable
    {
        void Write(BinaryWriter writer);
        void Read(BinaryReader reader);
    }
}
