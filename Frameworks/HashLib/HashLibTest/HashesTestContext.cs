using System;
using HashLib;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;
using TomanuExtensions;
using TomanuExtensions.Utils;
using TomanuExtensions.TestUtils;
using System.Text;

namespace HashLibTest
{
    public class HashesTestContext
    {
        public readonly MersenneTwister Random = new MersenneTwister(4563487);
        private readonly ReadOnlyCollection<Func<object>> _creators;

        public HashesTestContext()
        {
            _creators = new List<Func<object>>
            {
                () => { return Random.NextByte(); }, 
                () => { return Random.NextChar(); },
                () => { return Random.NextShort(); },
                () => { return Random.NextUShort(); },
                () => { return Random.NextInt(); },
                () => { return Random.NextUInt(); },
                () => { return Random.NextLong(); },
                () => { return Random.NextULong(); },
                () => { return Random.NextFloatFull(); },
                () => { return Random.NextDoubleFull(); },
                () => { return Random.NextString(Random.Next(0, 200)); }, 
                () => { return Random.NextBytes(Random.Next(0, 200)); }, 
                () => { return Random.NextChars(Random.Next(0, 200)); },
                () => { return Random.NextShorts(Random.Next(0, 200)); },
                () => { return Random.NextUShorts(Random.Next(0, 200)); },
                () => { return Random.NextInts(Random.Next(0, 200)); },
                () => { return Random.NextUInts(Random.Next(0, 200)); },
                () => { return Random.NextLongs(Random.Next(0, 200)); },
                () => { return Random.NextULongs(Random.Next(0, 200)); },
                () => { return Random.NextFloatsFull(Random.Next(0, 200)); },
                () => { return Random.NextDoublesFull(Random.Next(0, 200)); },
            }.AsReadOnly();
        }

        private volatile int transform_rounds = 100;

        public void TestExtremelyLong()
        {
            const int SLEEP_TIME_MS = 10;
            int THREADS = System.Environment.ProcessorCount;
            const float TARGET_CPU_LOAD = 0.4f;

            Assert.True(THREADS <= System.Environment.ProcessorCount);
 
            ProgressIndicator pi = new ProgressIndicator("Crypto +4GB Test");

            pi.AddLine(String.Format("Configuration: {0} threads, CPU Load: {1}%", THREADS, TARGET_CPU_LOAD * 100));

            CancellationTokenSource src = new CancellationTokenSource();
            
            Task regulator = Task.Factory.StartNew(token => 
            {
                PerformanceCounter pc = new PerformanceCounter("Processor", "% Processor Time", "_Total");

                List<float> cpu_load = new List<float>();

                for (;;)
                {
                    const int PROBE_DELTA_MS = 200;
                    const int PROBE_COUNT = 30;
                    const int REGULATE_COUNT = 10;

                    for (int i = 0; i < REGULATE_COUNT; i++)
                    {
                        System.Threading.Thread.Sleep(PROBE_DELTA_MS);

                        cpu_load.Add(pc.NextValue() / 100);

                        if (src.IsCancellationRequested)
                            break;
                    }

                    while (cpu_load.Count > PROBE_COUNT)
                        cpu_load.RemoveFirst();

                    int old_transform_rounds = transform_rounds;

                    float avg_cpu_load = cpu_load.Average();

                    if (avg_cpu_load >= TARGET_CPU_LOAD)
                    {
                        transform_rounds = (int)Math.Round(transform_rounds * 0.9);

                        if (old_transform_rounds == transform_rounds)
                            transform_rounds--;
                    }
                    else
                    {
                        transform_rounds = (int)Math.Round(transform_rounds * 1.1);

                        if (old_transform_rounds == transform_rounds)
                            transform_rounds++;
                    }

                    if (transform_rounds == 0)
                        transform_rounds = 1;

                    if (src.IsCancellationRequested)
                        break;
                }
            }, src.Token);

            var partitioner = Partitioner.Create(Hashes.CryptoAll, EnumerablePartitionerOptions.None);

            Parallel.ForEach(partitioner, new ParallelOptions() { MaxDegreeOfParallelism = THREADS }, ht =>
            {
                IHash hash = (IHash)Activator.CreateInstance(ht);

                pi.AddLine(String.Format("{0} / {1} - {2} - {3}%", Hashes.CryptoAll.IndexOf(ht) + 1,
                    Hashes.CryptoAll.Count, hash.Name, 0));

                TestData test_data = TestData.Load(hash);

                int test_data_index = 0;

                for (int i = 0; i < test_data.Count; i++)
                {
                    if (test_data.GetRepeat(i) == 1)
                        continue;

                    test_data_index++;

                    ulong repeats = (ulong)test_data.GetRepeat(i);
                    byte[] data = test_data.GetData(i);
                    string expected_result = Converters.ConvertBytesToHexString(test_data.GetHash(i));

                    hash.Initialize();

                    int transform_counter = transform_rounds;
                    DateTime progress = DateTime.Now;

                    for (ulong j = 0; j < repeats; j++)
                    {
                        hash.TransformBytes(data);

                        transform_counter--;
                        if (transform_counter == 0)
                        {
                            System.Threading.Thread.Sleep(SLEEP_TIME_MS);
                            transform_counter = transform_rounds;
                        }

                        if (DateTime.Now - progress > TimeSpan.FromSeconds(5))
                        {
                            pi.AddLine(String.Format("{0} / {1} / {2} - {3} - {4}%", Hashes.CryptoAll.IndexOf(ht) + 1,
                                test_data_index, Hashes.CryptoAll.Count, hash.Name, j * 100 / repeats));
                            progress = DateTime.Now;
                        }
                    }

                    HashResult result = hash.TransformFinal();

                    Assert.Equal(expected_result, Converters.ConvertBytesToHexString(result.GetBytes()));//, hash.ToString());

                    pi.AddLine(String.Format("{0} / {1} / {2} - {3} - {4}", Hashes.CryptoAll.IndexOf(ht) + 1,
                        test_data_index, Hashes.CryptoAll.Count, hash.Name, "OK"));
                }
            });

            src.Cancel();
            regulator.Wait();
        }

        public void Test(IHash a_hash)
        {
            TestHashSize(a_hash);
            TestInitialization(a_hash);
            TestComputeTransforms(a_hash);
            TestMultipleTransforms(a_hash);
            TestHashResult(a_hash);
            TestHashStream(a_hash, 26);
            TestHashStream(a_hash, Hash.BUFFER_SIZE);
            TestAgainstTestFile(a_hash);
            TestFastHash32(a_hash);
            TestKey(a_hash);
        }

        protected void TestHashResult(IHash a_hash)
        {
            a_hash.Initialize();
            a_hash.TransformBytes(Random.NextBytes(64));
            HashResult r1 = a_hash.TransformFinal();
            byte[] r2 = (byte[])r1.GetBytes().Clone();
            HashResult r3 = a_hash.ComputeBytes(Random.NextBytes(64));
            byte[] r4 = (byte[])r3.GetBytes().Clone();

            Assert.NotSame(r1, r2);
            Assert.NotSame(r1.GetBytes(), r3.GetBytes());
            Assert.Equal(r1.GetBytes(), r2);
            Assert.Equal(r3.GetBytes(), r4);

            Assert.NotEqual(r1, r3);
            Assert.NotEqual(r2, r4);
            Assert.NotEqual(r1.GetBytes(), r3.GetBytes());

            Assert.Equal(Converters.ConvertBytesToHexString(Converters.ConvertHexStringToBytes("A1B1C2D34567890F")), 
                "A1B1C2D3-4567890F");
        }

        protected void TestInitialization(IHash a_hash)
        {
            for (int i = 0; i <= (a_hash.BlockSize * 3 + 1); i++)
            {
                IHash hash2 = ((IHash)Activator.CreateInstance(a_hash.GetType()));
                byte[] v = Random.NextBytes(i);

                HashResult h1 = a_hash.ComputeBytes(v);
                HashResult h2 = hash2.ComputeBytes(v);

                Assert.Equal(h1, h2);//, String.Format("{0}, {1}", a_hash.Name, i));
            }

            a_hash.TransformByte(0x55);
            HashResult r1 = a_hash.ComputeBytes(new byte[] { 0x55, 0x55 });

            a_hash.TransformByte(0x55);
            a_hash.Initialize();
            HashResult r2 = a_hash.ComputeBytes(new byte[] { 0x55, 0x55 });

            a_hash.ComputeBytes(new byte[] { 0x55, 0x55 });
            a_hash.TransformByte(0x55);
            a_hash.TransformByte(0x55);
            HashResult r3 = a_hash.TransformFinal();

            a_hash.ComputeBytes(new byte[] { 0x55, 0x55 });
            a_hash.Initialize();
            a_hash.TransformByte(0x55);
            a_hash.TransformByte(0x55);
            HashResult r4 = a_hash.TransformFinal();

            a_hash.TransformByte(0x55);
            a_hash.Initialize();
            a_hash.TransformByte(0x55);
            a_hash.TransformByte(0x55);
            HashResult r5 = a_hash.TransformFinal();

            Assert.Equal(r1, r2);
            Assert.Equal(r2, r3);
            Assert.Equal(r3, r4);
            Assert.Equal(r4, r5);

            a_hash.Initialize();
            HashResult r6 = a_hash.TransformFinal();
            HashResult r7 = a_hash.TransformFinal();
            a_hash.ComputeBytes(new byte[] { 0x55, 0x55 });
            HashResult r8 = a_hash.TransformFinal();

            a_hash = (IHash)Activator.CreateInstance(a_hash.GetType());
            HashResult r9 = a_hash.TransformFinal();

            Assert.Equal(r6, r7);
            Assert.Equal(r7, r8);
            Assert.Equal(r9, r9);

            {
                a_hash.Initialize();
                a_hash.TransformByte(0x55);
                HashResult h1 = a_hash.TransformFinal();
                a_hash.Initialize();
                a_hash.TransformByte(0x55);
                a_hash.ComputeBytes(new byte[] { 0x55, 0x55 });
                a_hash.TransformByte(0x55);
                HashResult h2 = a_hash.TransformFinal();

                Assert.Equal(h1, h2);
            }

            {
                a_hash.Initialize();
                a_hash.TransformByte(0x55);
                HashResult h1 = a_hash.TransformFinal();
                a_hash.Initialize();
                a_hash.TransformByte(0x55);
                a_hash.ComputeBytes(new byte[] { 0x55, 0x55 });
                a_hash.TransformByte(0xA3);
                HashResult h2 = a_hash.TransformFinal();

                Assert.NotEqual(h1, h2);
            }

            if (a_hash is IFastHash32)
            {
                IFastHash32 fast_hash = a_hash as IFastHash32;
                List<Action> fast_list = new List<Action>()
                {
                    () => fast_hash.ComputeByteFast(55), 
                    () => fast_hash.ComputeBytesFast(new byte[] { 0x55, 0x55 }), 
                    () => fast_hash.ComputeCharFast('c'), 
                    () => fast_hash.ComputeCharsFast(new char[] { 'c', 'c' }), 
                    () => fast_hash.ComputeDoubleFast(3.456489566e156), 
                    () => fast_hash.ComputeDoublesFast(new double[] { 3.456489566e156, 3.456489566e156 }), 
                    () => fast_hash.ComputeFloatFast(3.45698986e16f), 
                    () => fast_hash.ComputeFloatsFast(new float[] { 3.45698986e16f, 3.45698986e16f }), 
                    () => fast_hash.ComputeIntFast(1234567456), 
                    () => fast_hash.ComputeIntsFast(new int[] { 1234567456, 1234567456 }), 
                    () => fast_hash.ComputeLongFast(7632345678765765765), 
                    () => fast_hash.ComputeLongsFast(new long[] { 7632345678765765765, 7632345678765765765 }), 
                    () => fast_hash.ComputeShortFast(22345), 
                    () => fast_hash.ComputeShortsFast(new short[] { 22345, 22345 }), 
                    () => fast_hash.ComputeStringFast("test"), 
                    () => fast_hash.ComputeUIntFast(3234567456), 
                    () => fast_hash.ComputeUIntsFast(new uint[] { 3234567456, 3234567456 }), 
                    () => fast_hash.ComputeULongFast(9632345678765765765), 
                    () => fast_hash.ComputeULongsFast(new ulong[] { 9632345678765765765, 9632345678765765765 }), 
                    () => fast_hash.ComputeUShortFast(42345), 
                    () => fast_hash.ComputeUShortsFast(new ushort[] { 42345, 42345 }),   
                };

                foreach (var fast in fast_list)
                {
                    {
                        a_hash.Initialize();
                        a_hash.TransformByte(0x55);
                        HashResult h1 = a_hash.TransformFinal();
                        a_hash.Initialize();
                        a_hash.TransformByte(0x55);
                        fast();
                        a_hash.TransformByte(0x55);
                        HashResult h2 = a_hash.TransformFinal();

                        Assert.Equal(h1, h2);
                    }

                    {
                        a_hash.Initialize();
                        a_hash.TransformByte(0x55);
                        HashResult h1 = a_hash.TransformFinal();
                        a_hash.Initialize();
                        a_hash.TransformByte(0x55);
                        fast();
                        a_hash.TransformByte(0xA3);
                        HashResult h2 = a_hash.TransformFinal();

                        Assert.NotEqual(h1, h2);
                    }
                }
            }
        }

        protected void TestHashSize(IHash a_hash)
        {
            Assert.Equal(a_hash.HashSize, a_hash.ComputeBytes(new byte[] { }).GetBytes().Length);
        }

        protected void TestAgainstTestFile(IHash a_hash, TestData a_test_data = null)
        {
            if (a_test_data == null)
                a_test_data = TestData.Load(a_hash);

            for (int i = 0; i < a_test_data.Count; i++)
            {
                string output_array = Converters.ConvertBytesToHexString(a_test_data.GetHash(i));

                if (a_test_data.GetRepeat(i) != 1)
                    continue;

                if (a_hash is IWithKey)
                    (a_hash as IWithKey).Key = a_test_data.GetKey(i);

                Assert.Equal(output_array, a_hash.ComputeBytes(a_test_data.GetData(i)).ToString());//, String.Format("{0}, {1}", a_hash.Name, i));
            }
        }

        protected void TestMultipleTransforms(IHash a_multi, IHash a_hash, IList<int> a_list)
        {
            List<byte[]> v1 = new List<byte[]>(a_list.Count);

            foreach (int length in a_list)
            {
                byte[] ar = new byte[length];
                for (int i = 0; i < ar.Length; i++)
                    ar[i] = (byte)Random.Next(Byte.MaxValue);
                v1.Add(ar);
            }

            int len = 0;
            foreach (byte[] ar in v1)
                len += ar.Length;

            byte[] v2 = new byte[len];

            int index = 0;
            foreach (byte[] ar in v1)
            {
                Array.Copy(ar, 0, v2, index, ar.Length);
                index += ar.Length;
            }

            a_multi.Initialize();

            for (int i = 0; i < v1.Count; i++)
                a_multi.TransformBytes(v1[i]);

            HashResult h1 = a_multi.TransformFinal();
            HashResult h2 = a_hash.ComputeBytes(v2);

            Assert.Equal(h2, h1);//, a_hash.Name);
        }

        protected void TestMultipleTransforms(IHash a_hash)
        {
            TestMultipleTransforms(a_hash, a_hash, new List<int>() { 0, 0 });
            TestMultipleTransforms(a_hash, a_hash, new List<int>() { 1, 0 });
            TestMultipleTransforms(a_hash, a_hash, new List<int>() { 0, 1 });

            for (int tries = 0; tries < 10; tries++)
            {
                int parts = Random.Next(20) + 1;

                List<int> list = new List<int>(parts);

                for (int i = 0; i < parts; i++)
                    list.Add(Random.Next(a_hash.BlockSize * 3 + 1));

                TestMultipleTransforms(a_hash, a_hash, list);
            }

            List<object> objects;
            byte[] bytes;

            for (int i = 0; i < 10; i++)
            {
                CreateListForDataTest(out objects, out bytes);

                HashResult h1 = a_hash.ComputeBytes(bytes);

                a_hash.Initialize();

                foreach (object o in objects)
                    a_hash.TransformObject(o);

                HashResult h2 = a_hash.TransformFinal();

                Assert.Equal(h1, h2);//, String.Format("{0}, {1}", a_hash.Name, i));
            }

            {
                a_hash.Initialize();
                a_hash.TransformString("rwffasfger4536552▰Ḑ");
                var h3 = a_hash.TransformFinal();

                a_hash.Initialize();
                a_hash.TransformString("rwffasfger4536552▰Ḑ", Encoding.Unicode);
                var h4 = a_hash.TransformFinal();

                Assert.Equal(h3, h4);
            }
        }

        protected void TestComputeTransforms(IHash a_hash)
        {
            foreach (var creator in _creators)
            {
                for (int i = 0; i < 10; i++)
                {
                    object v = creator();
                    byte[] bytes = Converters.ConvertToBytes(v);

                    var h1 = a_hash.ComputeObject(v);
                    var h2 = a_hash.ComputeBytes(bytes);

                    Assert.Equal(h1, h2);//, String.Format("{0}, {1}", a_hash.Name, i));
                }
            }

            {
                var h3 = a_hash.ComputeString("rwffasfger4536552▰Ḑ");
                var h4 = a_hash.ComputeString("rwffasfger4536552▰Ḑ", Encoding.Unicode);

                Assert.Equal(h3, h4);
            }
        }

        private void CreateListForDataTest(out List<object> a_objects, out byte[] a_bytes)
        {
            a_objects = new List<object>();

            foreach (var creator in _creators)
                a_objects.Add(creator());

            List<byte[]> bytes = new List<byte[]>();

            foreach (object o in a_objects)
                bytes.Add(Converters.ConvertToBytes(o));

            int count = 0;
            foreach (byte[] b in bytes)
                count += b.Length;

            a_bytes = new byte[count];

            int index = 0;
            foreach (byte[] b in bytes)
            {
                Array.Copy(b, 0, a_bytes, index, b.Length);
                index += b.Length;
            }
        }

        protected void TestHashStream(IHash a_hash, int a_block_size)
        {
            int old = Hash.BUFFER_SIZE;
            Hash.BUFFER_SIZE = a_block_size;

            try
            {
                for (int i = 1; i < 10; i++)
                {
                    byte[] data = new byte[13 * i];
                    new Random().NextBytes(data);

                    using (MemoryStream ms = new MemoryStream(data))
                    {
                        HashResult h1 = a_hash.ComputeBytes(data);
                        HashResult h2 = a_hash.ComputeStream(ms);

                        Assert.Equal(h1, h2);

                        h1 = a_hash.ComputeBytes(HashLib.ArrayExtensions.SubArray(data, i, i * 7));
                        ms.Seek(i, SeekOrigin.Begin);
                        h2 = a_hash.ComputeStream(ms, i * 7);

                        Assert.Equal(h1, h2);

                        h1 = a_hash.ComputeBytes(data);
                        a_hash.Initialize();
                        a_hash.TransformBytes(data, 0, i * 3);
                        ms.Seek(i * 3, SeekOrigin.Begin);
                        a_hash.TransformStream(ms, i * 2);
                        a_hash.TransformBytes(data, i * 5);
                        h2 = a_hash.TransformFinal();

                        Assert.Equal(h1, h2);
                    }
                }

                int[] sizes = { 1, 11, 63, 64, 65, 127, 128, 129, 255, 256, 257, 511, 512, 513, 
                              1023, 1024, 1025, 4011, 64000, 66000, 250000, Hash.BUFFER_SIZE * 20 + 511};

                foreach (int size in sizes)
                {
                    byte[] data = new byte[size];
                    new Random().NextBytes(data);

                    using (MemoryStream ms = new MemoryStream(data))
                    {
                        HashResult h1 = a_hash.ComputeBytes(data);
                        HashResult h2 = a_hash.ComputeStream(ms);

                        Assert.Equal(h1, h2);
                    }
                }

                {
                    byte[] data = new byte[1011];
                    new Random().NextBytes(data);

                    using (MemoryStream ms = new MemoryStream(data))
                    {
                        bool ex = false;

                        try
                        {
                            ms.Position = 1011;
                            a_hash.ComputeStream(ms, -1);
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
                            a_hash.ComputeStream(ms, 2);
                        }
                        catch
                        {
                            ex = true;
                        }

                        Assert.True(ex);

                        try
                        {
                            ms.Position = 0;
                            a_hash.ComputeStream(ms, 1012);
                        }
                        catch
                        {
                            ex = true;
                        }

                        Assert.True(ex);
                    }

                }
            }
            finally
            {
                Hash.BUFFER_SIZE = old;
            }
        }

        protected void TestHMAC(IHMAC a_build_in_hmac, IHMAC a_not_build_in_hmac)
        {
            Assert.Equal(a_not_build_in_hmac.HashSize, a_build_in_hmac.HashSize);
            Assert.Equal(a_not_build_in_hmac.BlockSize, a_build_in_hmac.BlockSize);

            Assert.True(a_not_build_in_hmac is HMACNotBuildInAdapter);

            List<int> keys_length = new List<int>() { 0, 1, 7, 51, 121, 512, 1023 };
            keys_length.Add(a_build_in_hmac.BlockSize - 1);
            keys_length.Add(a_build_in_hmac.BlockSize);
            keys_length.Add(a_build_in_hmac.BlockSize + 1);

            List<int> msgs_length = new List<int>();
            msgs_length.AddRange(keys_length);
            msgs_length.Add(a_build_in_hmac.BlockSize * 4 - 1);
            msgs_length.Add(a_build_in_hmac.BlockSize * 4);
            msgs_length.Add(a_build_in_hmac.BlockSize * 4 + 1);

            foreach (int key_length in keys_length)
            {
                byte[] key = Random.NextBytes(key_length);

                a_not_build_in_hmac.Key = key;
                a_build_in_hmac.Key = key;

                foreach (int msg_length in msgs_length)
                {
                    byte[] msg = Random.NextBytes(msg_length);

                    a_not_build_in_hmac.Initialize();
                    a_not_build_in_hmac.TransformBytes(msg);
                    HashResult h1 = a_not_build_in_hmac.TransformFinal();

                    a_build_in_hmac.Initialize();
                    a_build_in_hmac.TransformBytes(msg);
                    HashResult h2 = a_build_in_hmac.TransformFinal();

                    Assert.Equal(h1, h2);//, a_build_in_hmac.Name);

                    h1 = a_not_build_in_hmac.ComputeString(BitConverter.ToString(msg));
                    h2 = a_build_in_hmac.ComputeString(BitConverter.ToString(msg));

                    Assert.Equal(h1, h2);//, a_build_in_hmac.Name);
                }
            }
        }

        public void TestKey(IHash a_hash)
        {
            var hash_with_key = a_hash as IHashWithKey;

            if (hash_with_key == null)
                return;

            int key_length = hash_with_key.KeyLength ?? 251;
            byte[] key = Random.NextBytes(key_length);
            key[0] = 11;

            hash_with_key.Key = key;
            Assert.Equal(key, hash_with_key.Key);
            hash_with_key.Key[0] = 12;
            Assert.Equal(key, hash_with_key.Key);
            key[0] = 12;
            Assert.NotEqual(key, hash_with_key.Key);
            key[0] = 11;
            Assert.Equal(key, hash_with_key.Key);

            hash_with_key.Initialize();
            Assert.Equal(key, hash_with_key.Key);

            hash_with_key.ComputeByte(56);
            Assert.Equal(key, hash_with_key.Key);

            hash_with_key.ComputeBytes(key);
            Assert.Equal(key, hash_with_key.Key);

            {
                hash_with_key.Initialize();
                hash_with_key.TransformBytes(key);
                hash_with_key.TransformFinal();
                Assert.Equal(key, hash_with_key.Key);

                hash_with_key.Initialize();
                hash_with_key.TransformBytes(key);
                hash_with_key.TransformBytes(key);
                var h1 = hash_with_key.TransformFinal();

                hash_with_key.Initialize();
                key[0] = 12;
                hash_with_key.Key = key;
                key[0] = 11;
                hash_with_key.TransformBytes(key);
                hash_with_key.TransformBytes(key);
                var h2 = hash_with_key.TransformFinal();

                Assert.Equal(h1, h2);
            }

            {
                hash_with_key.Initialize();
                hash_with_key.TransformBytes(key);
                hash_with_key.TransformBytes(key);
                var h1 = hash_with_key.TransformFinal();

                hash_with_key.Initialize();
                hash_with_key.TransformBytes(key);
                key[0] = 12;
                hash_with_key.Key = key;
                key[0] = 11;
                hash_with_key.TransformBytes(key);
                var h2 = hash_with_key.TransformFinal();

                Assert.Equal(h1, h2);
            }

            {
                hash_with_key.Key = null;
                var key1 = hash_with_key.Key; 
                hash_with_key.Key = key;
                Assert.Equal(hash_with_key.Key, key);
                hash_with_key.Key = null;
                Assert.Equal(hash_with_key.Key, key1);
                Assert.NotEqual(key, key1);
            }
        }

        private void TestFastHash32(IHash a_hash)
        {
            IFastHash32 fh = a_hash as IFastHash32;

            if (fh == null)
                return;

            for (int i=0; i<10; i++)
            {
                {
                    var data = Random.NextBytes((i == 0) ? 0 : (int)(Random.NextUInt() % 200));
                    var h1 = fh.ComputeBytesFast(data);
                    var h2 = a_hash.ComputeBytes(data);
                    Assert.Equal(h1, h2.GetInt());
                }

                {
                    var data = Random.NextInt();
                    var h1 = fh.ComputeIntFast(data);
                    var h2 = a_hash.ComputeInt(data);
                    Assert.Equal(h1, h2.GetInt());
                }

                {
                    var data = Random.NextUInt();
                    var h1 = fh.ComputeUIntFast(data);
                    var h2 = a_hash.ComputeUInt(data);
                    Assert.Equal(h1, h2.GetInt());
                }

                {
                    var data = Random.NextByte();
                    var h1 = fh.ComputeByteFast(data);
                    var h2 = a_hash.ComputeByte(data);
                    Assert.Equal(h1, h2.GetInt());
                }

                {
                    var data = Random.NextChar();
                    var h1 = fh.ComputeCharFast(data);
                    var h2 = a_hash.ComputeChar(data);
                    Assert.Equal(h1, h2.GetInt());
                }

                {
                    var data = Random.NextShort();
                    var h1 = fh.ComputeShortFast(data);
                    var h2 = a_hash.ComputeShort(data);
                    Assert.Equal(h1, h2.GetInt());
                }

                {
                    var data = Random.NextUShort();
                    var h1 = fh.ComputeUShortFast(data);
                    var h2 = a_hash.ComputeUShort(data);
                    Assert.Equal(h1, h2.GetInt());
                }

                {
                    var data = Random.NextDoubleFull();
                    var h1 = fh.ComputeDoubleFast(data);
                    var h2 = a_hash.ComputeDouble(data);
                    Assert.Equal(h1, h2.GetInt());
                }

                {
                    var data = Random.NextFloatFull();
                    var h1 = fh.ComputeFloatFast(data);
                    var h2 = a_hash.ComputeFloat(data);
                    Assert.Equal(h1, h2.GetInt());
                }

                {
                    var data = Random.NextLong();
                    var h1 = fh.ComputeLongFast(data);
                    var h2 = a_hash.ComputeLong(data);
                    Assert.Equal(h1, h2.GetInt());
                }

                {
                    var data = Random.NextULong();
                    var h1 = fh.ComputeULongFast(data);
                    var h2 = a_hash.ComputeULong(data);
                    Assert.Equal(h1, h2.GetInt());
                }

                {
                    var data = Random.NextUInts((i == 0) ? 0 : (int)(Random.NextUInt() % 200));
                    var h1 = fh.ComputeUIntsFast(data);
                    var h2 = a_hash.ComputeUInts(data);
                    Assert.Equal(h1, h2.GetInt());
                }

                {
                    var data = Random.NextInts((i == 0) ? 0 : (int)(Random.NextUInt() % 200));
                    var h1 = fh.ComputeIntsFast(data);
                    var h2 = a_hash.ComputeInts(data);
                    Assert.Equal(h1, h2.GetInt());
                }

                {
                    var data = Random.NextLongs((i == 0) ? 0 : (int)(Random.NextUInt() % 200));
                    var h1 = fh.ComputeLongsFast(data);
                    var h2 = a_hash.ComputeLongs(data);
                    Assert.Equal(h1, h2.GetInt());
                }

                {
                    var data = Random.NextULongs((i == 0) ? 0 : (int)(Random.NextUInt() % 200));
                    var h1 = fh.ComputeULongsFast(data);
                    var h2 = a_hash.ComputeULongs(data);
                    Assert.Equal(h1, h2.GetInt());
                }

                {
                    var data = Random.NextDoublesFull((i == 0) ? 0 : (int)(Random.NextUInt() % 200));
                    var h1 = fh.ComputeDoublesFast(data);
                    var h2 = a_hash.ComputeDoubles(data);
                    Assert.Equal(h1, h2.GetInt());
                }

                {
                    var data = Random.NextFloatsFull((i == 0) ? 0 : (int)(Random.NextUInt() % 200));
                    var h1 = fh.ComputeFloatsFast(data);
                    var h2 = a_hash.ComputeFloats(data);
                    Assert.Equal(h1, h2.GetInt());
                }

                {
                    var data = Random.NextChars((i == 0) ? 0 : (int)(Random.NextUInt() % 200));
                    var h1 = fh.ComputeCharsFast(data);
                    var h2 = a_hash.ComputeChars(data);
                    Assert.Equal(h1, h2.GetInt());
                }

                {
                    var data = Random.NextUShorts((i == 0) ? 0 : (int)(Random.NextUInt() % 200));
                    var h1 = fh.ComputeUShortsFast(data);
                    var h2 = a_hash.ComputeUShorts(data);
                    Assert.Equal(h1, h2.GetInt());
                }

                {
                    var data = Random.NextString((i == 0) ? 0 : (int)(Random.NextUInt() % 200));
                    var h1 = fh.ComputeStringFast(data);
                    var h2 = a_hash.ComputeString(data);
                    Assert.Equal(h1, h2.GetInt());
                }

                {
                    var data = Random.NextShorts((i == 0) ? 0 : (int)(Random.NextUInt() % 200));
                    var h1 = fh.ComputeShortsFast(data);
                    var h2 = a_hash.ComputeShorts(data);
                    Assert.Equal(h1, h2.GetInt());
                }
            }
        }
    }
}
