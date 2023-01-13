namespace EtAlii.Ubigia.Tests;

using System;

public class TestPropertiesFactory
{
    private readonly Random _random;

    public TestPropertiesFactory()
    {
        _random = new Random(1234567890);
    }
    public PropertyDictionary Create()
    {
        var size = _random.Next(1, 20);
        return Create(size);
    }

    public PropertyDictionary Create(int size)
    {
        var properties = new PropertyDictionary();


        for (var i = 0; i < size; i++)
        {
            var type = _random.Next(6);
            switch (type)
            {
                case 0:
                    properties[Guid.NewGuid().ToString()] = _random.Next();
                    break;
                case 1:
                    properties[Guid.NewGuid().ToString()] = Guid.NewGuid();
                    break;
                case 2:
                    properties[Guid.NewGuid().ToString()] = Guid.NewGuid().ToString();
                    break;
                case 3:
                    properties[Guid.NewGuid().ToString()] = (uint)_random.Next();
                    break;
                case 4:
                    properties[Guid.NewGuid().ToString()] = _random.Next(1) == 1;
                    break;
                case 5:
                    properties[Guid.NewGuid().ToString()] = (long)_random.Next();
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

    public PropertyDictionary CreateComplete()
    {
        var properties = new PropertyDictionary();
        properties[Guid.NewGuid().ToString()] = _random.Next();
        properties[Guid.NewGuid().ToString()] = Guid.NewGuid();
        properties[Guid.NewGuid().ToString()] = Guid.NewGuid().ToString();
        properties[Guid.NewGuid().ToString()] = (uint)_random.Next();
        properties[Guid.NewGuid().ToString()] = _random.Next(1) == 1;
        properties[Guid.NewGuid().ToString()] = (long)_random.Next();
        properties[Guid.NewGuid().ToString()] = _random.NextDouble();
        properties[Guid.NewGuid().ToString()] = (float)_random.NextDouble();
        return properties;
    }

    public PropertyDictionary CreateSimple()
    {
        var result = new PropertyDictionary();
        result["Name"] = Guid.NewGuid().ToString();
        result["Value"] = Guid.NewGuid();
        return result;
    }


    public PropertyDictionary[] CreateSimple(int count)
    {
        var result = new PropertyDictionary[count];
        for (var i = 0; i < count; i++)
        {
            result[i] = CreateSimple();
        }
        return result;
    }

}
