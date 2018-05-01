﻿namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System;

    public static class GuidExtension 
    {
        public static System.Guid ToGuid(this WireProtocol.Guid id)
        {
            var bytes = new byte[16]; 
            
            BitConverter.GetBytes(id.Data1).CopyTo(bytes, 0);
            BitConverter.GetBytes(id.Data2).CopyTo(bytes, 8);
//            BitConverter.GetBytes(id.Data3).CopyTo(bytes, 7);
//            BitConverter.GetBytes(id.Data4).CopyTo(bytes, 11);

            return new System.Guid(bytes);
        }
        
        public static WireProtocol.Guid ToGuid(this System.Guid id)
        {
            var bytes = id.ToByteArray();
            return new WireProtocol.Guid()
            {
                Data1 = BitConverter.ToUInt64(bytes, 0),
                Data2 = BitConverter.ToUInt64(bytes, 8),
//                Data3 = BitConverter.ToUInt64(bytes, 7),
//                Data4 = BitConverter.ToUInt64(bytes, 11),
            };
        }
    }
}
