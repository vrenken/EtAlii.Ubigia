namespace EtAlii.Ubigia.Tests
{
    using System;
    using System.Collections.Generic;

    public class TestContentFactory
    {
        private readonly Random _random;

        public TestContentFactory()
        {
            _random = new Random(1234567890);
        }

        public Content Create()
        {
            var content = new Content();

            var parts = (ulong)_random.Next(6, 16);
            Blob.SetTotalParts(content, parts);

            return content;
        }

        public Content Create(ulong parts)
        {
            var content = new Content();
            Blob.SetTotalParts(content, parts);

            return content;
        }

        public List<ContentPart> CreateParts(ulong totalParts)
        {
            var contentParts = new List<ContentPart>();

            for (uint partId = 0; partId < totalParts; partId++)
            {
                contentParts.Add(CreatePart(partId));
            }

            return contentParts;
        }

        public List<ContentPart> CreateParts(byte[][] datas)
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

        public ContentPart CreatePart()
        {
            var partId = (uint)_random.Next(0, int.MaxValue);
            return CreatePart(partId);
        }

        public ContentPart CreatePart(ulong partId)
        {
            var data = CreateData(5, 30);
            return CreatePart(data, partId);
        }
        public ContentPart CreatePart(byte[] data, ulong partId = 0)
        {
            var contentPart = ContentPart.Create(partId, data);
            return contentPart;
        }

        public byte[] CreateData(int min, int max)
        {
            var size = (ulong)_random.Next(min, max);
            var data = new byte[size];
            _random.NextBytes(data);
            return data;
        }

        public byte[][] CreateData(int min, int max, int count)
        {
            var result = new List<byte[]>();
            for (var i = 0; i < count; i++)
            {
                var size = (ulong)_random.Next(min, max);
                var data = new byte[size];
                _random.NextBytes(data);
                result.Add(data);
            }
            return result.ToArray();
        }

    }
}
