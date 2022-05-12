// ReSharper disable All

using System;
using System.Linq;
using HashLib;
using Xunit;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using TomanuExtensions.Utils;
using System.Text;

namespace HashLibTest
{
    public class TestData
    {
        private readonly List<byte[]> m_hashes = new List<byte[]>();
        private readonly List<byte[]> m_datas = new List<byte[]>();
        private readonly List<int> m_repeats = new List<int>();
        private readonly IHash m_hash;
        private readonly List<byte[]> m_keys = new List<byte[]>();
        private readonly MersenneTwister m_random = new MersenneTwister(4563487);

        protected TestData(IHash a_hash, List<byte[]> a_keys)
        {
            m_hash = a_hash;

            if (a_keys != null)
                m_keys = a_keys;

            if (m_keys.Count != 0)
                Debug.Assert(a_hash is IWithKey);
        }

        public string GetFileName()
        {
            var dir = "TestData";//new DirectoryInfo(
                //System.Reflection.Assembly.GetAssembly(typeof(IHash)).Location).Parent.Parent.Parent.Parent.FullName +
                //Path.DirectorySeparatorChar + "HashLibTest" + Path.DirectorySeparatorChar + "TestData";

            //if (!Directory.Exists(dir))
            //{
            //    dir = new DirectoryInfo(
            //        System.Reflection.Assembly.GetAssembly(typeof(IHash)).Location).Parent.Parent.Parent.Parent.Parent.FullName +
            //    Path.DirectorySeparatorChar + "HashLibTest" + Path.DirectorySeparatorChar + "TestData";
            //}

            return dir + Path.DirectorySeparatorChar + m_hash.GetType().Name + "Test.txt";
        }

        public static TestData Load(IHash a_hash)
        {
            var td = new TestData(a_hash, new List<byte[]>());
            td.Load();
            return td;
        }

        private void Load()
        {
            var lines = File.ReadAllLines(GetFileName());
            lines = (from line in lines
                     let l = line.Trim()
                     where !String.IsNullOrEmpty(l)
                     select l).ToArray();

            for (var i=0; i<lines.Length; i++)
            {
                var line = lines[i];

                if (line.StartsWith("Repeat: "))
                {
                    m_repeats.Add(Int32.Parse(line.Replace("Repeat: ", "")));
                }
                else if (line.StartsWith("Messsage: "))
                {
                    m_datas.Add(Converters.ConvertHexStringToBytes(line.Replace("Messsage: ", "")));
                }
                else if (line == "Messsage:")
                {
                    m_datas.Add(Array.Empty<byte>());
                }
                else if (line.StartsWith("Text: "))
                {
                    m_datas.Add(Converters.ConvertStringToBytes(line.Replace("Text: ", ""), Encoding.ASCII));
                }
                else if (line.StartsWith("Hash: "))
                {
                    m_hashes.Add(Converters.ConvertHexStringToBytes(line.Replace("Hash: ", "")));
                }
                else if (line.StartsWith("Key: "))
                {
                    m_keys.Add(Converters.ConvertHexStringToBytes(line.Replace("Key: ", "")));
                }
            }

            Assert.True(Count >= 1);

            Assert.True(m_datas.Count == m_hashes.Count);
            Assert.True(m_hashes.Count == m_repeats.Count);

            if (m_hash is IWithKey)
                Assert.True(m_repeats.Count == m_keys.Count);
        }

        public static void Save(IHash a_hash, List<byte[]> a_keys = null)
        {
            var td = new TestData(a_hash, a_keys);
            td.Save();
        }

        private void Save()
        {
            if (m_hash is IWithKey)
                Debug.Assert(m_keys.Count >= 2);

            var temp_file = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            var fs = new FileStream(temp_file, FileMode.Create);

            using var sw = new StreamWriter(fs);

            for (var i = 0; i <= m_hash.BlockSize * 3 + 1; i++)
            {
                if (m_keys.Any())
                {
                    foreach (var key in m_keys)
                    {
                        (m_hash as IWithKey).Key = key;
                        SaveLine(sw, i);
                    }
                }
                else
                {
                    SaveLine(sw, i);
                }
            }

            if (m_hash is ICrypto)
            {
                var repeats = new int[] { 16777216, 67108865 };

                foreach (var repeat in repeats)
                {
                    var str = "abcdefghbcdefghicdefghijdefghijkefghijklfghijklmghijklmnhijklmno";
                    var data = System.Text.Encoding.ASCII.GetBytes(str);

                    for (var i = 0; i < repeat; i++)
                    {
                        m_hash.TransformBytes(data);
                    }

                    var hash = m_hash.TransformFinal().GetBytes();

                    sw.WriteLine("Repeat: {0}", repeat);
                    sw.WriteLine("Text: {0}", str);
                    sw.WriteLine("Hash: {0}", Converters.ConvertBytesToHexString(hash, false));
                    sw.WriteLine("");
                }
            }

            System.IO.File.Delete(GetFileName());
            System.IO.File.Move(temp_file, GetFileName());
        }

        private void SaveLine(StreamWriter a_sw, int a_data_length)
        {
            var data = m_random.NextBytes(a_data_length);
            var hash = m_hash.ComputeBytes(data).GetBytes();

            if (m_hash is IWithKey)
            {
                a_sw.WriteLine("Key: {0}", Converters.ConvertBytesToHexString((m_hash as IWithKey).Key, false));
            }

            a_sw.WriteLine("Repeat: {0}", 1);
            a_sw.WriteLine("Messsage: {0}", Converters.ConvertBytesToHexString(data, false));
            a_sw.WriteLine("Hash: {0}", Converters.ConvertBytesToHexString(hash, false));
            a_sw.WriteLine("");
        }

        public byte[] GetHash(int a_index)
        {
            return (byte[])m_hashes[a_index].Clone();
        }

        public byte[] GetData(int a_index)
        {
            return (byte[])m_datas[a_index].Clone();
        }

        public byte[] GetKey(int a_index)
        {
            return (byte[])m_keys[a_index].Clone();
        }

        public int Count => m_hashes.Count;

        public int GetRepeat(int a_index)
        {
            return m_repeats[a_index];
        }
    }
}
