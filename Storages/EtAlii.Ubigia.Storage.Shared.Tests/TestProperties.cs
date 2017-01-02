namespace EtAlii.Ubigia.Storage.Tests
{
    using System;
    using EtAlii.Ubigia.Api;

    public static class TestProperties
    {
        public static PropertyDictionary Create()
        {
            var rnd = new Random(1234567890);
            var size = rnd.Next(1, 20);
            return Create(size);
        }

        public static PropertyDictionary Create(int size)
        {
            var rnd = new Random(1234567890);
            var properties = new PropertyDictionary();


            for (int i = 0; i < size; i++)
            {
                int type = rnd.Next(6);
                switch (type)
                {
                    case 0:
                        properties[Guid.NewGuid().ToString()] = rnd.Next();
                        break;
                    case 1:
                        properties[Guid.NewGuid().ToString()] = Guid.NewGuid();
                        break;
                    case 2:
                        properties[Guid.NewGuid().ToString()] = Guid.NewGuid().ToString();
                        break;
                    case 3:
                        properties[Guid.NewGuid().ToString()] = (UInt32)rnd.Next();
                        break;
                    case 4:
                        properties[Guid.NewGuid().ToString()] = rnd.Next(1) == 1;
                        break;
                    case 5:
                        properties[Guid.NewGuid().ToString()] = (Int64)rnd.Next();
                        break;
                    //case 6:
                    //    properties[Guid.NewGuid().ToString()] = rnd.NextDouble();
                    //    break;
                    //case 7:
                    //    properties[Guid.NewGuid().ToString()] = (float)rnd.NextDouble();
                    //    break;
                }
            }

            return properties;
        }

        public static PropertyDictionary CreateComplete()
        {
            var rnd = new Random(1234567890);
            var properties = new PropertyDictionary();
            properties[Guid.NewGuid().ToString()] = rnd.Next();
            properties[Guid.NewGuid().ToString()] = Guid.NewGuid();
            properties[Guid.NewGuid().ToString()] = Guid.NewGuid().ToString();
            properties[Guid.NewGuid().ToString()] = (UInt32)rnd.Next();
            properties[Guid.NewGuid().ToString()] = rnd.Next(1) == 1;
            properties[Guid.NewGuid().ToString()] = (Int64)rnd.Next();
            properties[Guid.NewGuid().ToString()] = rnd.NextDouble();
            properties[Guid.NewGuid().ToString()] = (float)rnd.NextDouble();
            return properties;
        }

        public static PropertyDictionary CreateSimple()
        {
            var result = new PropertyDictionary();
            result["Name"] = Guid.NewGuid().ToString();
            result["Value"] = Guid.NewGuid();
            return result;
        }


        public static PropertyDictionary[] CreateSimple(int count)
        {
            var result = new PropertyDictionary[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = CreateSimple();
            }
            return result;
        }

    }
}
