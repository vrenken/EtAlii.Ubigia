using System;
using HashLib;
using System.Linq;
using Xunit;
using System.Text;

namespace HashLibTest
{
    
    public class HashesTest : IClassFixture<HashesTestContext>
    {
        private readonly HashesTestContext _testContext;

        public HashesTest(HashesTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public void HashLib_Crypto_MD5()
        {
            _testContext.Test(HashFactory.Crypto.CreateMD5());
        }

        [Fact]
        public void HashLib_Crypto_Snefru()
        {
            _testContext.Test(HashFactory.Crypto.CreateSnefru_4_128());
            _testContext.Test(HashFactory.Crypto.CreateSnefru_4_256());
            _testContext.Test(HashFactory.Crypto.CreateSnefru_8_128());
            _testContext.Test(HashFactory.Crypto.CreateSnefru_8_256());
        }

        [Fact]
        public void HashLib_Crypto_HAS160()
        {
            _testContext.Test(HashFactory.Crypto.CreateHAS160());
        }

        [Fact]
        public void HashLib_Crypto_MD2()
        {
            _testContext.Test(HashFactory.Crypto.CreateMD2());
        }

        [Fact]
        public void HashLib_Crypto_MD4()
        {
            _testContext.Test(HashFactory.Crypto.CreateMD4());
        }

        [Fact]
        public void HashLib_Crypto_SHA224()
        {
            _testContext.Test(HashFactory.Crypto.CreateSHA224());
        }

        [Fact]
        public void HashLib_Crypto_RIPEMD128()
        {
            _testContext.Test(HashFactory.Crypto.CreateRIPEMD128());
        }

        [Fact]
        public void HashLib_Crypto_RIPEMD160()
        {
            _testContext.Test(HashFactory.Crypto.CreateRIPEMD160());
        }

        [Fact]
        public void HashLib_Crypto_Haval()
        {
            _testContext.Test(HashFactory.Crypto.CreateHaval_3_128());
            _testContext.Test(HashFactory.Crypto.CreateHaval_4_128());
            _testContext.Test(HashFactory.Crypto.CreateHaval_5_128());

            _testContext.Test(HashFactory.Crypto.CreateHaval_3_160());
            _testContext.Test(HashFactory.Crypto.CreateHaval_4_160());
            _testContext.Test(HashFactory.Crypto.CreateHaval_5_160());

            _testContext.Test(HashFactory.Crypto.CreateHaval_3_192());
            _testContext.Test(HashFactory.Crypto.CreateHaval_4_192());
            _testContext.Test(HashFactory.Crypto.CreateHaval_5_192());

            _testContext.Test(HashFactory.Crypto.CreateHaval_3_224());
            _testContext.Test(HashFactory.Crypto.CreateHaval_4_224());
            _testContext.Test(HashFactory.Crypto.CreateHaval_5_224());

            _testContext.Test(HashFactory.Crypto.CreateHaval_3_256());
            _testContext.Test(HashFactory.Crypto.CreateHaval_4_256());
            _testContext.Test(HashFactory.Crypto.CreateHaval_5_256());
        }

        [Fact]
        public void HashLib_Crypto_SHA3_JH()
        {
            _testContext.Test(HashFactory.Crypto.SHA3.CreateJH224());
            _testContext.Test(HashFactory.Crypto.SHA3.CreateJH256());
            _testContext.Test(HashFactory.Crypto.SHA3.CreateJH384());
            _testContext.Test(HashFactory.Crypto.SHA3.CreateJH512());
        }

        [Fact]
        public void HashLib_Crypto_SHA3_Echo()
        {
            _testContext.Test(HashFactory.Crypto.SHA3.CreateEcho224());
            _testContext.Test(HashFactory.Crypto.SHA3.CreateEcho256());
            _testContext.Test(HashFactory.Crypto.SHA3.CreateEcho384());
            _testContext.Test(HashFactory.Crypto.SHA3.CreateEcho512());
        }

        [Fact]
        public void HashLib_Crypto_SHA3_Fugue()
        {
            _testContext.Test(HashFactory.Crypto.SHA3.CreateFugue224());
            _testContext.Test(HashFactory.Crypto.SHA3.CreateFugue256());
            _testContext.Test(HashFactory.Crypto.SHA3.CreateFugue384());
            _testContext.Test(HashFactory.Crypto.SHA3.CreateFugue512());
        }

        [Fact]
        public void HashLib_Crypto_SHA3_Groestl()
        {
            _testContext.Test(HashFactory.Crypto.SHA3.CreateGroestl224());
            _testContext.Test(HashFactory.Crypto.SHA3.CreateGroestl256());
            _testContext.Test(HashFactory.Crypto.SHA3.CreateGroestl384());
            _testContext.Test(HashFactory.Crypto.SHA3.CreateGroestl512());
        }

        [Fact]
        public void HashLib_Crypto_SHA3_Hamsi()
        {
            _testContext.Test(HashFactory.Crypto.SHA3.CreateHamsi224());
            _testContext.Test(HashFactory.Crypto.SHA3.CreateHamsi256());
            _testContext.Test(HashFactory.Crypto.SHA3.CreateHamsi384());
            _testContext.Test(HashFactory.Crypto.SHA3.CreateHamsi512());
        }

        [Fact]
        public void HashLib_Crypto_SHA3_Keccak()
        {
            _testContext.Test(HashFactory.Crypto.SHA3.CreateKeccak224());
            _testContext.Test(HashFactory.Crypto.SHA3.CreateKeccak256());
            _testContext.Test(HashFactory.Crypto.SHA3.CreateKeccak384());
            _testContext.Test(HashFactory.Crypto.SHA3.CreateKeccak512());
        }

        [Fact]
        public void HashLib_Crypto_SHA3_Luffa()
        {
            _testContext.Test(HashFactory.Crypto.SHA3.CreateLuffa224());
            _testContext.Test(HashFactory.Crypto.SHA3.CreateLuffa256());
            _testContext.Test(HashFactory.Crypto.SHA3.CreateLuffa384());
            _testContext.Test(HashFactory.Crypto.SHA3.CreateLuffa512());
        }

        [Fact]
        public void HashLib_Crypto_SHA3_Shabal()
        {
            _testContext.Test(HashFactory.Crypto.SHA3.CreateShabal224());
            _testContext.Test(HashFactory.Crypto.SHA3.CreateShabal256());
            _testContext.Test(HashFactory.Crypto.SHA3.CreateShabal384());
            _testContext.Test(HashFactory.Crypto.SHA3.CreateShabal512());
        }

        [Fact]
        public void HashLib_Crypto_SHA3_SHAvite3()
        {
            _testContext.Test(HashFactory.Crypto.SHA3.CreateSHAvite3_224());
            _testContext.Test(HashFactory.Crypto.SHA3.CreateSHAvite3_256());
            _testContext.Test(HashFactory.Crypto.SHA3.CreateSHAvite3_384());
            _testContext.Test(HashFactory.Crypto.SHA3.CreateSHAvite3_512());
        }

        [Fact]
        public void HashLib_Crypto_SHA3_SIMD()
        {
            _testContext.Test(HashFactory.Crypto.SHA3.CreateSIMD224());
            _testContext.Test(HashFactory.Crypto.SHA3.CreateSIMD256());
            _testContext.Test(HashFactory.Crypto.SHA3.CreateSIMD384());
            _testContext.Test(HashFactory.Crypto.SHA3.CreateSIMD512());
        }

        [Fact]
        public void HashLib_Crypto_SHA3_Skein()
        {
            _testContext.Test(HashFactory.Crypto.SHA3.CreateSkein224());
            _testContext.Test(HashFactory.Crypto.SHA3.CreateSkein256());
            _testContext.Test(HashFactory.Crypto.SHA3.CreateSkein384());
            _testContext.Test(HashFactory.Crypto.SHA3.CreateSkein512());
        }

        [Fact]
        public void HashLib_Crypto_SHA3_CubeHash()
        {
            _testContext.Test(HashFactory.Crypto.SHA3.CreateCubeHash224());
            _testContext.Test(HashFactory.Crypto.SHA3.CreateCubeHash256());
            _testContext.Test(HashFactory.Crypto.SHA3.CreateCubeHash384());
            _testContext.Test(HashFactory.Crypto.SHA3.CreateCubeHash512());
        }

        [Fact]
        public void HashLib_Crypto_SHA3_Blake()
        {
            _testContext.Test(HashFactory.Crypto.SHA3.CreateBlake224());
            _testContext.Test(HashFactory.Crypto.SHA3.CreateBlake256());
            _testContext.Test(HashFactory.Crypto.SHA3.CreateBlake384());
            _testContext.Test(HashFactory.Crypto.SHA3.CreateBlake512());
        }

        [Fact]
        public void HashLib_Crypto_SHA3_BlueMidnightWish()
        {
            _testContext.Test(HashFactory.Crypto.SHA3.CreateBlueMidnightWish224());
            _testContext.Test(HashFactory.Crypto.SHA3.CreateBlueMidnightWish256());
            _testContext.Test(HashFactory.Crypto.SHA3.CreateBlueMidnightWish384());
            _testContext.Test(HashFactory.Crypto.SHA3.CreateBlueMidnightWish512());
        }

        [Fact]
        public void HashLib_Crypto_RIPEMD256()
        {
            _testContext.Test(HashFactory.Crypto.CreateRIPEMD256());
        }

        [Fact]
        public void HashLib_Crypto_RIPEMD320()
        {
            _testContext.Test(HashFactory.Crypto.CreateRIPEMD320());
        }

        [Fact]
        public void HashLib_Crypto_SHA1()
        {
            _testContext.Test(HashFactory.Crypto.CreateSHA1());
        }

        [Fact]
        public void HashLib_Crypto_SHA512()
        {
            _testContext.Test(HashFactory.Crypto.CreateSHA512());
        }

        [Fact]
        public void HashLib_Crypto_SHA384()
        {
            _testContext.Test(HashFactory.Crypto.CreateSHA384());
        }

        [Fact]
        public void HashLib_Crypto_SHA256()
        {
            _testContext.Test(HashFactory.Crypto.CreateSHA256());
        }

        [Fact]
        public void HashLib_Crypto_Whirlpool()
        {
            _testContext.Test(HashFactory.Crypto.CreateWhirlpool());
        }

        //[Fact(Skip = "Not working (yet)")]
        public void Crypto_ExtremelyLong()
        {
            _testContext.TestExtremelyLong();
        }

        [Fact]
        public void HashLib_Crypto_Gost()
        {
            _testContext.Test(HashFactory.Crypto.CreateGost());
        }

        [Fact]
        public void HashLib_Crypto_Tiger()
        {
            _testContext.Test(HashFactory.Crypto.CreateTiger_3_192());
            _testContext.Test(HashFactory.Crypto.CreateTiger_4_192());
        }

        [Fact]
        public void HashLib_Crypto_Tiger2()
        {
            _testContext.Test(HashFactory.Crypto.CreateTiger2());
        }

        [Fact]
        public void HashLib_32_AP()
        {
            _testContext.Test(HashFactory.Hash32.CreateAP());
        }

        [Fact]
        public void HashLib_Checksum_Adler32()
        {
            _testContext.Test(HashFactory.Checksum.CreateAdler32());
        }

        [Fact]
        public void HashLib_32_Bernstein()
        {
            _testContext.Test(HashFactory.Hash32.CreateBernstein());
        }

        [Fact]
        public void HashLib_32_Bernstein1()
        {
            _testContext.Test(HashFactory.Hash32.CreateBernstein1());
        }

        [Fact]
        public void HashLib_32_BKDR()
        {
            _testContext.Test(HashFactory.Hash32.CreateBKDR());
        }

        [Fact]
        public void HashLib_Checksum_CRC32()
        {
            _testContext.Test(HashFactory.Checksum.CreateCRC32_IEEE());
            _testContext.Test(HashFactory.Checksum.CreateCRC32_CASTAGNOLI());
            _testContext.Test(HashFactory.Checksum.CreateCRC32_KOOPMAN());
            _testContext.Test(HashFactory.Checksum.CreateCRC32_Q());
        }

        [Fact]
        public void HashLib_32_DEK()
        {
            _testContext.Test(HashFactory.Hash32.CreateDEK());
        }

        [Fact]
        public void HashLib_32_DJB()
        {
            _testContext.Test(HashFactory.Hash32.CreateDJB());
        }

        [Fact]
        public void HashLib_32_ELF()
        {
            _testContext.Test(HashFactory.Hash32.CreateELF());
        }

        [Fact]
        public void HashLib_Checksum_FNV()
        {
            _testContext.Test(HashFactory.Hash32.CreateFNV());
        }

        [Fact]
        public void HashLib_32_FNV1a()
        {
            _testContext.Test(HashFactory.Hash32.CreateFNV1a());
        }

        [Fact]
        public void HashLib_32_Jenkins3()
        {
            _testContext.Test(HashFactory.Hash32.CreateJenkins3());
        }

        [Fact]
        public void HashLib_32_JS()
        {
            _testContext.Test(HashFactory.Hash32.CreateJS());
        }

        [Fact]
        public void HashLib_32_Murmur2()
        {
            _testContext.Test(HashFactory.Hash32.CreateMurmur2());
        }

        [Fact]
        public void HashLib_32_Murmur3()
        {
            _testContext.Test(HashFactory.Hash32.CreateMurmur3());
        }

        [Fact]
        public void HashLib_32_OneAtTime()
        {
            _testContext.Test(HashFactory.Hash32.CreateOneAtTime());
        }

        [Fact]
        public void HashLib_32_PJW()
        {
            _testContext.Test(HashFactory.Hash32.CreatePJW());
        }

        [Fact]
        public void HashLib_32_Rotating()
        {
            _testContext.Test(HashFactory.Hash32.CreateRotating());
        }

        [Fact]
        public void HashLib_32_RS()
        {
            _testContext.Test(HashFactory.Hash32.CreateRS());
        }

        [Fact]
        public void HashLib_32_SDBM()
        {
            _testContext.Test(HashFactory.Hash32.CreateSDBM());
        }

        [Fact]
        public void HashLib_32_ShiftAndXor()
        {
            _testContext.Test(HashFactory.Hash32.CreateShiftAndXor());
        }

        [Fact]
        public void HashLib_32_SuperFast()
        {
            _testContext.Test(HashFactory.Hash32.CreateSuperFast());
        }

        [Fact]
        public void HashLib_64_FNV()
        {
            _testContext.Test(HashFactory.Hash64.CreateFNV());
        }

        [Fact]
        public void HashLib_64_FNV1a()
        {
            _testContext.Test(HashFactory.Hash64.CreateFNV1a());
        }

        [Fact]
        public void HashLib_Checksum_CRC64()
        {
            _testContext.Test(HashFactory.Checksum.CreateCRC64_ISO());
            _testContext.Test(HashFactory.Checksum.CreateCRC64_ECMA());
        }

        [Fact]
        public void HashLib_64_Murmur2()
        {
            _testContext.Test(HashFactory.Hash64.CreateMurmur2());
        }

        [Fact]
        public void HashLib_128_Murmur3()
        {
            _testContext.Test(HashFactory.Hash128.CreateMurmur3_128());
        }

        [Fact]
        public void HashLib_64_SipHash()
        {
            _testContext.Test(HashFactory.Hash64.CreateSipHash());
        }

        [Fact]
        public void HashLib_Crypto_Grindahl256()
        {
            _testContext.Test(HashFactory.Crypto.CreateGrindahl256());
        }

        [Fact]
        public void HashLib_Crypto_Grindahl512()
        {
            _testContext.Test(HashFactory.Crypto.CreateGrindahl512());
        }

        [Fact]
        public void HashLib_Crypto_Panama()
        {
            _testContext.Test(HashFactory.Crypto.CreatePanama());
        }

        [Fact]
        public void HashLib_Crypto_RadioGatun32()
        {
            _testContext.Test(HashFactory.Crypto.CreateRadioGatun32());
        }

        [Fact]
        public void HashLib_Crypto_RadioGatun64()
        {
            _testContext.Test(HashFactory.Crypto.CreateRadioGatun64());
        }

        [Fact]
        public void HashLib_Crypto_RIPEMD()
        {
            _testContext.Test(HashFactory.Crypto.CreateRIPEMD());
        }

        [Fact]
        public void HashLib_Crypto_SHA0()
        {
            _testContext.Test(HashFactory.Crypto.CreateSHA0());
        }

        [Fact]
        public void TestConverters()
        {
            {
                var chars = new[] { '\x1234', '\xABCD' };
                var bytes = Converters.ConvertCharsToBytes(chars);
                Assert.Equal(bytes.ToList(), 
                    Converters.ConvertHexStringToBytes("3412CDAB").ToList());
            }

            {
                var str = "\x1234\xABCD";
                var bytes = Converters.ConvertStringToBytes(str);
                Assert.Equal(bytes.ToList(),
                    Converters.ConvertHexStringToBytes("3412CDAB").ToList());
            }

            {
                var str = "\x1234\xABCD";
                var bytes = Converters.ConvertStringToBytes(str, Encoding.Unicode);
                Assert.Equal(bytes.ToList(),
                    Converters.ConvertHexStringToBytes("3412CDAB").ToList());
            }

            {
                var shorts = new short[] { 0x1234, 0x7BCD };
                var bytes = Converters.ConvertShortsToBytes(shorts);
                Assert.Equal(bytes.ToList(),
                    Converters.ConvertHexStringToBytes("3412CD7B").ToList());
            }

            {
                var ushorts = new ushort[] { 0x1234, 0xABCD };
                var bytes = Converters.ConvertUShortsToBytes(ushorts);
                Assert.Equal(bytes.ToList(),
                    Converters.ConvertHexStringToBytes("3412CDAB").ToList());
            }

            {
                var ints = new[] { 0x12345678, 0x7BCDEF45 };
                var bytes = Converters.ConvertIntsToBytes(ints);
                Assert.Equal(bytes.ToList(), 
                    Converters.ConvertHexStringToBytes("7856341245EFCD7B").ToList());
            }

            {
                var uints = new uint[] { 0x12345678, 0xABCDEF45 };
                var bytes = Converters.ConvertUIntsToBytes(uints);
                Assert.Equal(bytes.ToList(),
                    Converters.ConvertHexStringToBytes("7856341245EFCDAB").ToList());
            }

            {
                var longs = new[] { 0x12345678ABCDEF45, 0x6756EEFFBC456783 };
                var bytes = Converters.ConvertLongsToBytes(longs);
                Assert.Equal(bytes.ToList(),
                    Converters.ConvertHexStringToBytes("12345678ABCDEF45").Reverse().Concat(
                        Converters.ConvertHexStringToBytes("6756EEFFBC456783").Reverse()).ToList());
            }

            {
                var ulongs = new ulong[] { 0x12345678ABCDEF45, 0xF756EEFFBC456783 };
                var bytes = Converters.ConvertULongsToBytes(ulongs);
                Assert.Equal(bytes.ToList(),
                    Converters.ConvertHexStringToBytes("12345678ABCDEF45").Reverse().Concat(
                        Converters.ConvertHexStringToBytes("F756EEFFBC456783").Reverse()).ToList());
            }

            {
                var doubles = new[] { 56.678768, -34.4568768, 10e34, 10e-20, Double.NaN };
                var bytes = Converters.ConvertDoublesToBytes(doubles);

                var b0 = BitConverter.GetBytes(doubles[0]);
                var b1 = BitConverter.GetBytes(doubles[1]);
                var b2 = BitConverter.GetBytes(doubles[2]);
                var b3 = BitConverter.GetBytes(doubles[3]);
                var b4 = BitConverter.GetBytes(doubles[4]);

                Assert.Equal(bytes.ToList(),
                    b0.Concat(b1).Concat(b2).Concat(b3).Concat(b4).ToList());
            }

            {
                var floats = new[] { 56.678768f, -34.4568768f, 10e34f, 10e-20f, Single.NaN };
                var bytes = Converters.ConvertFloatsToBytes(floats);

                var b0 = BitConverter.GetBytes(floats[0]);
                var b1 = BitConverter.GetBytes(floats[1]);
                var b2 = BitConverter.GetBytes(floats[2]);
                var b3 = BitConverter.GetBytes(floats[3]);
                var b4 = BitConverter.GetBytes(floats[4]);

                Assert.Equal(bytes.ToList(),
                    b0.Concat(b1).Concat(b2).Concat(b3).Concat(b4).ToList());
            }
        }

        [Fact]
        public void HashResult()
        {
            for (int i = 0; i < 14; i++)
            {
                HashResult h1 = new HashResult(_testContext.Random.NextBytes(i));

                try
                {
                    uint h2 = h1.GetUInt();

                    Assert.False(i != 4, i.ToString());

                    Assert.True(Converters.ConvertBytesToUInts(h1.GetBytes())[0] == h2, i.ToString());
                }
                catch
                {
                    Assert.False(i == 4, i.ToString());
                }

                try
                {
                    ulong h3 = h1.GetULong();

                    Assert.False(i != 8, i.ToString());

                    Assert.True(Converters.ConvertBytesToULongs(h1.GetBytes())[0] == h3, i.ToString());
                }
                catch
                {
                    Assert.False(i == 8, i.ToString());
                }
            }
        }

        [Fact]
        public void HMAC_All()
        {
            _testContext.TestKey(HashFactory.HMAC.CreateHMAC(HashFactory.Crypto.CreateMD5()));
        }
    }
}
