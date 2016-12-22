namespace EtAlii.Servus.Storage.Tests
{
    using System;
    using System.Collections.Generic;
    using EtAlii.Servus.Api;

    public static class TestContent
    {
        public static Content Create()
        {
            var rnd = new Random();
            var content = new Content();

            var parts = (UInt64)rnd.Next(6, 16);
            content.TotalParts = parts;

            return content;
        }

        public static Content Create(UInt64 parts)
        {
            var content = new Content
            {
                TotalParts = parts
            };
            return content;
        }

        public static List<ContentPart> CreateParts(UInt64 totalParts)
        {
            var contentParts = new List<ContentPart>();

            for (UInt32 partId = 0; partId < totalParts; partId++)
            {
                contentParts.Add(CreatePart(partId));
            }

            return contentParts;
        }

        public static List<ContentPart> CreateParts(byte[][] datas)
        {
            var contentParts = new List<ContentPart>();

            var partId = (ulong)0;
            foreach (var data in datas)
            {
                var contentPart = CreatePart(data, partId++);
                contentParts.Add(contentPart);
            }

            return contentParts;
        }

        public static ContentPart CreatePart()
        {
            var rnd = new Random();
            var partId = (UInt32)rnd.Next(0, int.MaxValue);
            return CreatePart(partId);
        }

        public static ContentPart CreatePart(UInt32 partId)
        {
            var data = CreateData(5, 30);
            return CreatePart(data, partId);
        }
        public static ContentPart CreatePart(byte[] data, UInt64 partId = 0)
        {
            var contentPart = new ContentPart
            {
                Id = partId,
                Data = data,
            };
            return contentPart;
        }

        public static byte[] CreateData(int min, int max)
        {
            var rnd = new Random();
            var size = (UInt64)rnd.Next(min, max);
            var data = new byte[size];
            rnd.NextBytes(data);
            return data;
        }

        public static byte[][] CreateData(int min, int max, int count)
        {
            var rnd = new Random();
            var result = new List<byte[]>();
            for (int i = 0; i < count; i++)
            {
                var size = (UInt64)rnd.Next(min, max);
                var data = new byte[size];
                rnd.NextBytes(data);
                result.Add(data);
            }
            return result.ToArray();
        }

    }
}
