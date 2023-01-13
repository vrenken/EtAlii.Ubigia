namespace HashLib.Tests;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using HashLib;
using HashLibTest;
using TomanuExtensions.Utils;
using Xunit;

public class HashesTestContext
{
    public readonly MersenneTwister Random = new(4563487);
    private readonly ReadOnlyCollection<Func<object>> _creators;

    public HashesTestContext()
    {
        _creators = new List<Func<object>>
        {
            () => Random.NextByte(),
            () => Random.NextChar(),
            () => Random.NextShort(),
            () => Random.NextUShort(),
            () => Random.NextInt(),
            () => Random.NextUInt(),
            () => Random.NextLong(),
            () => Random.NextULong(),
            () => Random.NextFloatFull(),
            () => Random.NextDoubleFull(),
            () => Random.NextString(Random.Next(0, 200)),
            () => Random.NextBytes(Random.Next(0, 200)),
            () => Random.NextChars(Random.Next(0, 200)),
            () => Random.NextShorts(Random.Next(0, 200)),
            () => Random.NextUShorts(Random.Next(0, 200)),
            () => Random.NextInts(Random.Next(0, 200)),
            () => Random.NextUInts(Random.Next(0, 200)),
            () => Random.NextLongs(Random.Next(0, 200)),
            () => Random.NextULongs(Random.Next(0, 200)),
            () => Random.NextFloatsFull(Random.Next(0, 200)),
            () => Random.NextDoublesFull(Random.Next(0, 200)),
        }.AsReadOnly();
    }

    public void Test(IHash aHash)
    {
        TestHashSize(aHash);
        TestInitialization(aHash);
        TestComputeTransforms(aHash);
        TestMultipleTransforms(aHash);
        TestHashResult(aHash);
        TestHashStream(aHash, 26);
        TestHashStream(aHash, Hash.BUFFER_SIZE);
        TestAgainstTestFile(aHash);
        TestFastHash32(aHash);
        TestKey(aHash);
    }

    private void TestHashResult(IHash aHash)
    {
        aHash.Initialize();
        aHash.TransformBytes(Random.NextBytes(64));
        var r1 = aHash.TransformFinal();
        var r2 = (byte[])r1.GetBytes().Clone();
        var r3 = aHash.ComputeBytes(Random.NextBytes(64));
        var r4 = (byte[])r3.GetBytes().Clone();

        Assert.NotSame(r1, r2);
        Assert.NotSame(r1.GetBytes(), r3.GetBytes());
        Assert.Equal(r1.GetBytes(), r2);
        Assert.Equal(r3.GetBytes(), r4);

        Assert.NotEqual(r1, r3);
        Assert.NotEqual(r2, r4);
        Assert.NotEqual(r1.GetBytes(), r3.GetBytes());

        var actual = Converters.ConvertBytesToHexString(Converters.ConvertHexStringToBytes("A1B1C2D34567890F"));
        Assert.Equal("A1B1C2D3-4567890F", actual);
    }

    protected void TestInitialization(IHash aHash)
    {
        for (var i = 0; i <= (aHash.BlockSize * 3 + 1); i++)
        {
            var hash2 = ((IHash)Activator.CreateInstance(aHash.GetType()));
            var v = Random.NextBytes(i);

            var h1 = aHash.ComputeBytes(v);
            var h2 = hash2!.ComputeBytes(v);

            Assert.Equal(h1, h2);//, String.Format("{0}, {1}", a_hash.Name, i));
        }

        aHash.TransformByte(0x55);
        var r1 = aHash.ComputeBytes(new byte[] { 0x55, 0x55 });

        aHash.TransformByte(0x55);
        aHash.Initialize();
        var r2 = aHash.ComputeBytes(new byte[] { 0x55, 0x55 });

        aHash.ComputeBytes(new byte[] { 0x55, 0x55 });
        aHash.TransformByte(0x55);
        aHash.TransformByte(0x55);
        var r3 = aHash.TransformFinal();

        aHash.ComputeBytes(new byte[] { 0x55, 0x55 });
        aHash.Initialize();
        aHash.TransformByte(0x55);
        aHash.TransformByte(0x55);
        var r4 = aHash.TransformFinal();

        aHash.TransformByte(0x55);
        aHash.Initialize();
        aHash.TransformByte(0x55);
        aHash.TransformByte(0x55);
        var r5 = aHash.TransformFinal();

        Assert.Equal(r1, r2);
        Assert.Equal(r2, r3);
        Assert.Equal(r3, r4);
        Assert.Equal(r4, r5);

        aHash.Initialize();
        var r6 = aHash.TransformFinal();
        var r7 = aHash.TransformFinal();
        aHash.ComputeBytes(new byte[] { 0x55, 0x55 });
        var r8 = aHash.TransformFinal();

        aHash = (IHash)Activator.CreateInstance(aHash.GetType());
        var r9 = aHash!.TransformFinal();

        Assert.Equal(r6, r7);
        Assert.Equal(r7, r8);
        Assert.Equal(r9, r9);

        {
            aHash.Initialize();
            aHash.TransformByte(0x55);
            var h1 = aHash.TransformFinal();
            aHash.Initialize();
            aHash.TransformByte(0x55);
            aHash.ComputeBytes(new byte[] { 0x55, 0x55 });
            aHash.TransformByte(0x55);
            var h2 = aHash.TransformFinal();

            Assert.Equal(h1, h2);
        }

        {
            aHash.Initialize();
            aHash.TransformByte(0x55);
            var h1 = aHash.TransformFinal();
            aHash.Initialize();
            aHash.TransformByte(0x55);
            aHash.ComputeBytes(new byte[] { 0x55, 0x55 });
            aHash.TransformByte(0xA3);
            var h2 = aHash.TransformFinal();

            Assert.NotEqual(h1, h2);
        }

        if (aHash is IFastHash32)
        {
            var fastHash = aHash as IFastHash32;
            var fastList = new List<Action>
            {
                () => fastHash.ComputeByteFast(55),
                () => fastHash.ComputeBytesFast(new byte[] { 0x55, 0x55 }),
                () => fastHash.ComputeCharFast('c'),
                () => fastHash.ComputeCharsFast(new[] { 'c', 'c' }),
                () => fastHash.ComputeDoubleFast(3.456489566e156),
                () => fastHash.ComputeDoublesFast(new[] { 3.456489566e156, 3.456489566e156 }),
                () => fastHash.ComputeFloatFast(3.45698986e16f),
                () => fastHash.ComputeFloatsFast(new[] { 3.45698986e16f, 3.45698986e16f }),
                () => fastHash.ComputeIntFast(1234567456),
                () => fastHash.ComputeIntsFast(new[] { 1234567456, 1234567456 }),
                () => fastHash.ComputeLongFast(7632345678765765765),
                () => fastHash.ComputeLongsFast(new[] { 7632345678765765765, 7632345678765765765 }),
                () => fastHash.ComputeShortFast(22345),
                () => fastHash.ComputeShortsFast(new short[] { 22345, 22345 }),
                () => fastHash.ComputeStringFast("test"),
                () => fastHash.ComputeUIntFast(3234567456),
                () => fastHash.ComputeUIntsFast(new[] { 3234567456, 3234567456 }),
                () => fastHash.ComputeULongFast(9632345678765765765),
                () => fastHash.ComputeULongsFast(new[] { 9632345678765765765, 9632345678765765765 }),
                () => fastHash.ComputeUShortFast(42345),
                () => fastHash.ComputeUShortsFast(new ushort[] { 42345, 42345 }),
            };

            foreach (var fast in fastList)
            {
                {
                    aHash.Initialize();
                    aHash.TransformByte(0x55);
                    var h1 = aHash.TransformFinal();
                    aHash.Initialize();
                    aHash.TransformByte(0x55);
                    fast();
                    aHash.TransformByte(0x55);
                    var h2 = aHash.TransformFinal();

                    Assert.Equal(h1, h2);
                }

                {
                    aHash.Initialize();
                    aHash.TransformByte(0x55);
                    var h1 = aHash.TransformFinal();
                    aHash.Initialize();
                    aHash.TransformByte(0x55);
                    fast();
                    aHash.TransformByte(0xA3);
                    var h2 = aHash.TransformFinal();

                    Assert.NotEqual(h1, h2);
                }
            }
        }
    }

    protected void TestHashSize(IHash aHash)
    {
        Assert.Equal(aHash.HashSize, aHash.ComputeBytes(Array.Empty<byte>()).GetBytes().Length);
    }

    protected void TestAgainstTestFile(IHash aHash, TestData aTestData = null)
    {
        if (aTestData == null)
            aTestData = TestData.Load(aHash);

        for (var i = 0; i < aTestData.Count; i++)
        {
            var outputArray = Converters.ConvertBytesToHexString(aTestData.GetHash(i));

            if (aTestData.GetRepeat(i) != 1)
                continue;

            if (aHash is IWithKey key)
            {
                key.Key = aTestData.GetKey(i);
            }

            Assert.Equal(outputArray, aHash.ComputeBytes(aTestData.GetData(i)).ToString());//, String.Format("{0}, {1}", a_hash.Name, i));
        }
    }

    protected void TestMultipleTransforms(IHash aMulti, IHash aHash, IList<int> aList)
    {
        var v1 = new List<byte[]>(aList.Count);

        foreach (var length in aList)
        {
            var ar = new byte[length];
            for (var i = 0; i < ar.Length; i++)
                ar[i] = (byte)Random.Next(byte.MaxValue);
            v1.Add(ar);
        }

        var len = 0;
        foreach (var ar in v1)
        {
            len += ar.Length;
        }

        var v2 = new byte[len];

        var index = 0;
        foreach (var ar in v1)
        {
            Array.Copy(ar, 0, v2, index, ar.Length);
            index += ar.Length;
        }

        aMulti.Initialize();

        foreach (var t in v1)
        {
            aMulti.TransformBytes(t);
        }

        var h1 = aMulti.TransformFinal();
        var h2 = aHash.ComputeBytes(v2);

        Assert.Equal(h2, h1);//, a_hash.Name);
    }

    protected void TestMultipleTransforms(IHash aHash)
    {
        TestMultipleTransforms(aHash, aHash, new List<int> { 0, 0 });
        TestMultipleTransforms(aHash, aHash, new List<int> { 1, 0 });
        TestMultipleTransforms(aHash, aHash, new List<int> { 0, 1 });

        for (var tries = 0; tries < 10; tries++)
        {
            var parts = Random.Next(20) + 1;

            var list = new List<int>(parts);

            for (var i = 0; i < parts; i++)
                list.Add(Random.Next(aHash.BlockSize * 3 + 1));

            TestMultipleTransforms(aHash, aHash, list);
        }

        byte[] bytes;

        for (var i = 0; i < 10; i++)
        {
            CreateListForDataTest(out var objects, out bytes);

            var h1 = aHash.ComputeBytes(bytes);

            aHash.Initialize();

            foreach (var o in objects)
                aHash.TransformObject(o);

            var h2 = aHash.TransformFinal();

            Assert.Equal(h1, h2);//, String.Format("{0}, {1}", a_hash.Name, i));
        }

        {
            aHash.Initialize();
            aHash.TransformString("rwffasfger4536552▰Ḑ");
            var h3 = aHash.TransformFinal();

            aHash.Initialize();
            aHash.TransformString("rwffasfger4536552▰Ḑ", Encoding.Unicode);
            var h4 = aHash.TransformFinal();

            Assert.Equal(h3, h4);
        }
    }

    protected void TestComputeTransforms(IHash aHash)
    {
        foreach (var creator in _creators)
        {
            for (var i = 0; i < 10; i++)
            {
                var v = creator();
                var bytes = Converters.ConvertToBytes(v);

                var h1 = aHash.ComputeObject(v);
                var h2 = aHash.ComputeBytes(bytes);

                Assert.Equal(h1, h2);//, String.Format("{0}, {1}", a_hash.Name, i));
            }
        }

        {
            var h3 = aHash.ComputeString("rwffasfger4536552▰Ḑ");
            var h4 = aHash.ComputeString("rwffasfger4536552▰Ḑ", Encoding.Unicode);

            Assert.Equal(h3, h4);
        }
    }

    private void CreateListForDataTest(out List<object> aObjects, out byte[] aBytes)
    {
        aObjects = new List<object>();

        foreach (var creator in _creators)
            aObjects.Add(creator());

        var bytes = new List<byte[]>();

        foreach (var o in aObjects)
            bytes.Add(Converters.ConvertToBytes(o));

        var count = 0;
        foreach (var b in bytes)
            count += b.Length;

        aBytes = new byte[count];

        var index = 0;
        foreach (var b in bytes)
        {
            Array.Copy(b, 0, aBytes, index, b.Length);
            index += b.Length;
        }
    }

    protected void TestHashStream(IHash aHash, int aBlockSize)
    {
        var old = Hash.BUFFER_SIZE;
        Hash.BUFFER_SIZE = aBlockSize;

        try
        {
            for (var i = 1; i < 10; i++)
            {
                var data = new byte[13 * i];
                new Random().NextBytes(data);

                using var ms = new MemoryStream(data);
                var h1 = aHash.ComputeBytes(data);
                var h2 = aHash.ComputeStream(ms);

                Assert.Equal(h1, h2);

                h1 = aHash.ComputeBytes(data.SubArray(i, i * 7));
                ms.Seek(i, SeekOrigin.Begin);
                h2 = aHash.ComputeStream(ms, i * 7);

                Assert.Equal(h1, h2);

                h1 = aHash.ComputeBytes(data);
                aHash.Initialize();
                aHash.TransformBytes(data, 0, i * 3);
                ms.Seek(i * 3, SeekOrigin.Begin);
                aHash.TransformStream(ms, i * 2);
                aHash.TransformBytes(data, i * 5);
                h2 = aHash.TransformFinal();

                Assert.Equal(h1, h2);
            }

            int[] sizes = { 1, 11, 63, 64, 65, 127, 128, 129, 255, 256, 257, 511, 512, 513,
                1023, 1024, 1025, 4011, 64000, 66000, 250000, Hash.BUFFER_SIZE * 20 + 511};

            foreach (var size in sizes)
            {
                var data = new byte[size];
                new Random().NextBytes(data);

                using var ms = new MemoryStream(data);
                var h1 = aHash.ComputeBytes(data);
                var h2 = aHash.ComputeStream(ms);

                Assert.Equal(h1, h2);
            }

            {
                var data = new byte[1011];
                new Random().NextBytes(data);

                using var ms = new MemoryStream(data);

                var ex = false;

                try
                {
                    ms.Position = 1011;
                    aHash.ComputeStream(ms);
                }
                catch
                {
                    ex = true;
                }

                Assert.False(ex);

                ex = false;

                try
                {
                    ms.Position = 1010;
                    aHash.ComputeStream(ms, 2);
                }
                catch
                {
                    ex = true;
                }

                Assert.True(ex);

                try
                {
                    ms.Position = 0;
                    aHash.ComputeStream(ms, 1012);
                }
                catch
                {
                    ex = true;
                }

                Assert.True(ex);
            }
        }
        finally
        {
            Hash.BUFFER_SIZE = old;
        }
    }

    protected void TestHmac(IHMAC aBuildInHmac, IHMAC aNotBuildInHmac)
    {
        Assert.Equal(aNotBuildInHmac.HashSize, aBuildInHmac.HashSize);
        Assert.Equal(aNotBuildInHmac.BlockSize, aBuildInHmac.BlockSize);

        Assert.True(aNotBuildInHmac is HMACNotBuildInAdapter);

        var keysLength = new List<int> {
            0,
            1,
            7,
            51,
            121,
            512,
            1023,
            aBuildInHmac.BlockSize - 1,
            aBuildInHmac.BlockSize,
            aBuildInHmac.BlockSize + 1
        };

        var msgsLength = new List<int>();
        msgsLength.AddRange(keysLength);
        msgsLength.Add(aBuildInHmac.BlockSize * 4 - 1);
        msgsLength.Add(aBuildInHmac.BlockSize * 4);
        msgsLength.Add(aBuildInHmac.BlockSize * 4 + 1);

        foreach (var keyLength in keysLength)
        {
            var key = Random.NextBytes(keyLength);

            aNotBuildInHmac.Key = key;
            aBuildInHmac.Key = key;

            foreach (var msgLength in msgsLength)
            {
                var msg = Random.NextBytes(msgLength);

                aNotBuildInHmac.Initialize();
                aNotBuildInHmac.TransformBytes(msg);
                var h1 = aNotBuildInHmac.TransformFinal();

                aBuildInHmac.Initialize();
                aBuildInHmac.TransformBytes(msg);
                var h2 = aBuildInHmac.TransformFinal();

                Assert.Equal(h1, h2);//, a_build_in_hmac.Name);

                h1 = aNotBuildInHmac.ComputeString(BitConverter.ToString(msg));
                h2 = aBuildInHmac.ComputeString(BitConverter.ToString(msg));

                Assert.Equal(h1, h2);//, a_build_in_hmac.Name);
            }
        }
    }

    public void TestKey(IHash aHash)
    {
        if (!(aHash is IHashWithKey hashWithKey))
            return;

        var keyLength = hashWithKey.KeyLength ?? 251;
        var key = Random.NextBytes(keyLength);
        key[0] = 11;

        hashWithKey.Key = key;
        Assert.Equal(key, hashWithKey.Key);
        hashWithKey.Key[0] = 12;
        Assert.Equal(key, hashWithKey.Key);
        key[0] = 12;
        Assert.NotEqual(key, hashWithKey.Key);
        key[0] = 11;
        Assert.Equal(key, hashWithKey.Key);

        hashWithKey.Initialize();
        Assert.Equal(key, hashWithKey.Key);

        hashWithKey.ComputeByte(56);
        Assert.Equal(key, hashWithKey.Key);

        hashWithKey.ComputeBytes(key);
        Assert.Equal(key, hashWithKey.Key);

        {
            hashWithKey.Initialize();
            hashWithKey.TransformBytes(key);
            hashWithKey.TransformFinal();
            Assert.Equal(key, hashWithKey.Key);

            hashWithKey.Initialize();
            hashWithKey.TransformBytes(key);
            hashWithKey.TransformBytes(key);
            var h1 = hashWithKey.TransformFinal();

            hashWithKey.Initialize();
            key[0] = 12;
            hashWithKey.Key = key;
            key[0] = 11;
            hashWithKey.TransformBytes(key);
            hashWithKey.TransformBytes(key);
            var h2 = hashWithKey.TransformFinal();

            Assert.Equal(h1, h2);
        }

        {
            hashWithKey.Initialize();
            hashWithKey.TransformBytes(key);
            hashWithKey.TransformBytes(key);
            var h1 = hashWithKey.TransformFinal();

            hashWithKey.Initialize();
            hashWithKey.TransformBytes(key);
            key[0] = 12;
            hashWithKey.Key = key;
            key[0] = 11;
            hashWithKey.TransformBytes(key);
            var h2 = hashWithKey.TransformFinal();

            Assert.Equal(h1, h2);
        }

        {
            hashWithKey.Key = null;
            var key1 = hashWithKey.Key;
            hashWithKey.Key = key;
            Assert.Equal(hashWithKey.Key, key);
            hashWithKey.Key = null;
            Assert.Equal(hashWithKey.Key, key1);
            Assert.NotEqual(key, key1);
        }
    }

    private void TestFastHash32(IHash aHash)
    {
        var fh = aHash as IFastHash32;

        if (fh == null)
            return;

        for (var i=0; i<10; i++)
        {
            {
                var data = Random.NextBytes((i == 0) ? 0 : (int)(Random.NextUInt() % 200));
                var h1 = fh.ComputeBytesFast(data);
                var h2 = aHash.ComputeBytes(data);
                Assert.Equal(h1, h2.GetInt());
            }

            {
                var data = Random.NextInt();
                var h1 = fh.ComputeIntFast(data);
                var h2 = aHash.ComputeInt(data);
                Assert.Equal(h1, h2.GetInt());
            }

            {
                var data = Random.NextUInt();
                var h1 = fh.ComputeUIntFast(data);
                var h2 = aHash.ComputeUInt(data);
                Assert.Equal(h1, h2.GetInt());
            }

            {
                var data = Random.NextByte();
                var h1 = fh.ComputeByteFast(data);
                var h2 = aHash.ComputeByte(data);
                Assert.Equal(h1, h2.GetInt());
            }

            {
                var data = Random.NextChar();
                var h1 = fh.ComputeCharFast(data);
                var h2 = aHash.ComputeChar(data);
                Assert.Equal(h1, h2.GetInt());
            }

            {
                var data = Random.NextShort();
                var h1 = fh.ComputeShortFast(data);
                var h2 = aHash.ComputeShort(data);
                Assert.Equal(h1, h2.GetInt());
            }

            {
                var data = Random.NextUShort();
                var h1 = fh.ComputeUShortFast(data);
                var h2 = aHash.ComputeUShort(data);
                Assert.Equal(h1, h2.GetInt());
            }

            {
                var data = Random.NextDoubleFull();
                var h1 = fh.ComputeDoubleFast(data);
                var h2 = aHash.ComputeDouble(data);
                Assert.Equal(h1, h2.GetInt());
            }

            {
                var data = Random.NextFloatFull();
                var h1 = fh.ComputeFloatFast(data);
                var h2 = aHash.ComputeFloat(data);
                Assert.Equal(h1, h2.GetInt());
            }

            {
                var data = Random.NextLong();
                var h1 = fh.ComputeLongFast(data);
                var h2 = aHash.ComputeLong(data);
                Assert.Equal(h1, h2.GetInt());
            }

            {
                var data = Random.NextULong();
                var h1 = fh.ComputeULongFast(data);
                var h2 = aHash.ComputeULong(data);
                Assert.Equal(h1, h2.GetInt());
            }

            {
                var data = Random.NextUInts((i == 0) ? 0 : (int)(Random.NextUInt() % 200));
                var h1 = fh.ComputeUIntsFast(data);
                var h2 = aHash.ComputeUInts(data);
                Assert.Equal(h1, h2.GetInt());
            }

            {
                var data = Random.NextInts((i == 0) ? 0 : (int)(Random.NextUInt() % 200));
                var h1 = fh.ComputeIntsFast(data);
                var h2 = aHash.ComputeInts(data);
                Assert.Equal(h1, h2.GetInt());
            }

            {
                var data = Random.NextLongs((i == 0) ? 0 : (int)(Random.NextUInt() % 200));
                var h1 = fh.ComputeLongsFast(data);
                var h2 = aHash.ComputeLongs(data);
                Assert.Equal(h1, h2.GetInt());
            }

            {
                var data = Random.NextULongs((i == 0) ? 0 : (int)(Random.NextUInt() % 200));
                var h1 = fh.ComputeULongsFast(data);
                var h2 = aHash.ComputeULongs(data);
                Assert.Equal(h1, h2.GetInt());
            }

            {
                var data = Random.NextDoublesFull((i == 0) ? 0 : (int)(Random.NextUInt() % 200));
                var h1 = fh.ComputeDoublesFast(data);
                var h2 = aHash.ComputeDoubles(data);
                Assert.Equal(h1, h2.GetInt());
            }

            {
                var data = Random.NextFloatsFull((i == 0) ? 0 : (int)(Random.NextUInt() % 200));
                var h1 = fh.ComputeFloatsFast(data);
                var h2 = aHash.ComputeFloats(data);
                Assert.Equal(h1, h2.GetInt());
            }

            {
                var data = Random.NextChars((i == 0) ? 0 : (int)(Random.NextUInt() % 200));
                var h1 = fh.ComputeCharsFast(data);
                var h2 = aHash.ComputeChars(data);
                Assert.Equal(h1, h2.GetInt());
            }

            {
                var data = Random.NextUShorts((i == 0) ? 0 : (int)(Random.NextUInt() % 200));
                var h1 = fh.ComputeUShortsFast(data);
                var h2 = aHash.ComputeUShorts(data);
                Assert.Equal(h1, h2.GetInt());
            }

            {
                var data = Random.NextString((i == 0) ? 0 : (int)(Random.NextUInt() % 200));
                var h1 = fh.ComputeStringFast(data);
                var h2 = aHash.ComputeString(data);
                Assert.Equal(h1, h2.GetInt());
            }

            {
                var data = Random.NextShorts((i == 0) ? 0 : (int)(Random.NextUInt() % 200));
                var h1 = fh.ComputeShortsFast(data);
                var h2 = aHash.ComputeShorts(data);
                Assert.Equal(h1, h2.GetInt());
            }
        }
    }
}
