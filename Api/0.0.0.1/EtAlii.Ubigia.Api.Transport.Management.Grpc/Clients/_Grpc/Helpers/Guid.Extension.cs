namespace EtAlii.Ubigia.Api.Transport.Management.Grpc
{
    using System;

    public static class GuidExtension 
    {
        public static System.Guid ToLocal(this WireProtocol.Guid id)
        {
            var bytes = new byte[16]; 
            
            BitConverter.GetBytes(id.Data1).CopyTo(bytes, 0);
            BitConverter.GetBytes(id.Data2).CopyTo(bytes, 8);

            return new System.Guid(bytes);
        }
        
        public static WireProtocol.Guid ToWire(this System.Guid id)
        {
            var bytes = id.ToByteArray();
            return new WireProtocol.Guid()
            {
                Data1 = BitConverter.ToUInt64(bytes, 0),
                Data2 = BitConverter.ToUInt64(bytes, 8),
            };
        }
    }
}
