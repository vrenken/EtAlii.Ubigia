// ReSharper disable all
#pragma warning disable CA2208 // External file, for now no need to cleanup.

using System;

namespace HashLib
{
    public static class HashFactory
    {
        public static class Hash32
        {
            public static IHash CreateAP()
            {
                return new HashLib.Hash32.AP();
            }

            public static IHash CreateBernstein()
            {
                return new HashLib.Hash32.Bernstein();
            }

            public static IHash CreateBernstein1()
            {
                return new HashLib.Hash32.Bernstein1();
            }

            public static IHash CreateBKDR()
            {
                return new HashLib.Hash32.BKDR();
            }

            public static IHash CreateDEK()
            {
                return new HashLib.Hash32.DEK();
            }

            public static IHash CreateDJB()
            {
                return new HashLib.Hash32.DJB();
            }

            public static IHash CreateDotNet()
            {
                return new HashLib.Hash32.DotNet();
            }

            public static IHash CreateELF()
            {
                return new HashLib.Hash32.ELF();
            }

            public static IHash CreateFNV()
            {
                return new HashLib.Hash32.FNV();
            }

            public static IHash CreateFNV1a()
            {
                return new HashLib.Hash32.FNV1a();
            }

            public static IHash CreateJenkins3()
            {
                return new HashLib.Hash32.Jenkins3();
            }

            public static IHash CreateJS()
            {
                return new HashLib.Hash32.JS();
            }

            public static IHashWithKey CreateMurmur2()
            {
                return new HashLib.Hash32.Murmur2();
            }

            public static IHashWithKey CreateMurmur3()
            {
                return new HashLib.Hash32.Murmur3();
            }

            public static IHash CreateOneAtTime()
            {
                return new HashLib.Hash32.OneAtTime();
            }

            public static IHash CreatePJW()
            {
                return new HashLib.Hash32.PJW();
            }

            public static IHash CreateRotating()
            {
                return new HashLib.Hash32.Rotating();
            }

            public static IHash CreateRS()
            {
                return new HashLib.Hash32.RS();
            }

            public static IHash CreateSDBM()
            {
                return new HashLib.Hash32.SDBM();
            }

            public static IHash CreateShiftAndXor()
            {
                return new HashLib.Hash32.ShiftAndXor();
            }

            public static IHash CreateSuperFast()
            {
                return new HashLib.Hash32.SuperFast();
            }
        }

        public static class Checksum
        {
            /// <summary>
            /// IEEE 802.3, polynomial = 0xEDB88320
            /// </summary>
            /// <returns></returns>
            public static IHash CreateCRC32_IEEE()
            {
                return new CRC32_IEEE();
            }

            /// <summary>
            /// Castagnoli, polynomial = 0x82F63B78
            /// </summary>
            /// <returns></returns>
            public static IHash CreateCRC32_CASTAGNOLI()
            {
                return new CRC32_CASTAGNOLI();
            }

            /// <summary>
            /// Koopman, polynomial = 0xEB31D82E
            /// </summary>
            /// <returns></returns>
            public static IHash CreateCRC32_KOOPMAN()
            {
                return new CRC32_KOOPMAN();
            }

            /// <summary>
            /// Q, polynomial = 0xD5828281
            /// </summary>
            /// <returns></returns>
            public static IHash CreateCRC32_Q()
            {
                return new CRC32_Q();
            }

            public static IHash CreateCRC32(uint a_polynomial, uint a_initial_value = uint.MaxValue, uint a_final_xor = uint.MaxValue)
            {
                return new CRC32(a_polynomial, a_initial_value, a_final_xor);
            }

            public static IHash CreateAdler32()
            {
                return new Adler32();
            }

            /// <summary>
            /// ECMA 182, polynomial = 0xD800000000000000
            /// </summary>
            /// <returns></returns>
            public static IHash CreateCRC64_ISO()
            {
                return new CRC64_ISO();
            }

            /// <summary>
            /// ISO, polynomial = 0xC96C5795D7870F42
            /// </summary>
            /// <returns></returns>
            public static IHash CreateCRC64_ECMA()
            {
                return new CRC64_ECMA();
            }

            public static IHash CreateCRC64(ulong a_polynomial, ulong a_initial_value = ulong.MaxValue, ulong a_final_xor = ulong.MaxValue)
            {
                return new CRC64(a_polynomial, a_initial_value, a_final_xor);
            }
        }

        public static class Hash64
        {
            public static IHash CreateFNV1a()
            {
                return new HashLib.Hash64.FNV1a64();
            }

            public static IHash CreateFNV()
            {
                return new HashLib.Hash64.FNV64();
            }

            public static IHashWithKey CreateMurmur2()
            {
                return new HashLib.Hash64.Murmur2_64();
            }

            public static IHashWithKey CreateSipHash()
            {
                return new HashLib.Hash64.SipHash();
            }
        }

        public static class Hash128
        {
            public static IHashWithKey CreateMurmur3_128()
            {
                return new HashLib.Hash128.Murmur3_128();
            }
        }

        public static class Crypto
        {
            //public static class BuildIn
            //{
            //    public static IHash CreateMD5CryptoServiceProvider()
            //    {
            //        return new HashLib.Crypto.BuildIn.MD5CryptoServiceProvider();
            //    }

            //    public static IHash CreateRIPEMD160Managed()
            //    {
            //        return new HashLib.Crypto.BuildIn.RIPEMD160Managed();
            //    }

            //    public static IHash CreateSHA1Cng()
            //    {
            //        return new HashLib.Crypto.BuildIn.SHA1Cng();
            //    }

            //    public static IHash CreateSHA1CryptoServiceProvider()
            //    {
            //        return new HashLib.Crypto.BuildIn.SHA1CryptoServiceProvider();
            //    }

            //    public static IHash CreateSHA1Managed()
            //    {
            //        return new HashLib.Crypto.BuildIn.SHA1Managed();
            //    }

            //    public static IHash CreateSHA256Cng()
            //    {
            //        return new HashLib.Crypto.BuildIn.SHA256Cng();
            //    }

            //    public static IHash CreateSHA256CryptoServiceProvider()
            //    {
            //        return new HashLib.Crypto.BuildIn.SHA256CryptoServiceProvider();
            //    }

            //    public static IHash CreateSHA256Managed()
            //    {
            //        return new HashLib.Crypto.BuildIn.SHA256Managed();
            //    }

            //    public static IHash CreateSHA384Cng()
            //    {
            //        return new HashLib.Crypto.BuildIn.SHA384Cng();
            //    }

            //    public static IHash CreateSHA384CryptoServiceProvider()
            //    {
            //        return new HashLib.Crypto.BuildIn.SHA384CryptoServiceProvider();
            //    }

            //    public static IHash CreateSHA384Managed()
            //    {
            //        return new HashLib.Crypto.BuildIn.SHA384Managed();
            //    }

            //    public static IHash CreateSHA512Cng()
            //    {
            //        return new HashLib.Crypto.BuildIn.SHA512Cng();
            //    }

            //    public static IHash CreateSHA512CryptoServiceProvider()
            //    {
            //        return new HashLib.Crypto.BuildIn.SHA512CryptoServiceProvider();
            //    }

            //    public static IHash CreateSHA512Managed()
            //    {
            //        return new HashLib.Crypto.BuildIn.SHA512Managed();
            //    }
            //}

            public static IHash CreateGost()
            {
                return new HashLib.Crypto.Gost();
            }

            public static IHash CreateGrindahl256()
            {
                return new HashLib.Crypto.Grindahl256();
            }

            public static IHash CreateGrindahl512()
            {
                return new HashLib.Crypto.Grindahl512();
            }

            public static IHash CreateHAS160()
            {
                return new HashLib.Crypto.HAS160();
            }

            public static IHash CreateHaval_3_128()
            {
                return new HashLib.Crypto.Haval_3_128();
            }

            public static IHash CreateHaval_4_128()
            {
                return new HashLib.Crypto.Haval_4_128();
            }

            public static IHash CreateHaval_5_128()
            {
                return new HashLib.Crypto.Haval_5_128();
            }

            public static IHash CreateHaval_3_160()
            {
                return new HashLib.Crypto.Haval_3_160();
            }

            public static IHash CreateHaval_4_160()
            {
                return new HashLib.Crypto.Haval_4_160();
            }

            public static IHash CreateHaval_5_160()
            {
                return new HashLib.Crypto.Haval_5_160();
            }

            public static IHash CreateHaval_3_192()
            {
                return new HashLib.Crypto.Haval_3_192();
            }

            public static IHash CreateHaval_4_192()
            {
                return new HashLib.Crypto.Haval_4_192();
            }

            public static IHash CreateHaval_5_192()
            {
                return new HashLib.Crypto.Haval_5_192();
            }

            public static IHash CreateHaval_3_224()
            {
                return new HashLib.Crypto.Haval_3_224();
            }

            public static IHash CreateHaval_4_224()
            {
                return new HashLib.Crypto.Haval_4_224();
            }

            public static IHash CreateHaval_5_224()
            {
                return new HashLib.Crypto.Haval_5_224();
            }

            public static IHash CreateHaval_3_256()
            {
                return new HashLib.Crypto.Haval_3_256();
            }

            public static IHash CreateHaval_4_256()
            {
                return new HashLib.Crypto.Haval_4_256();
            }

            public static IHash CreateHaval_5_256()
            {
                return new HashLib.Crypto.Haval_5_256();
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="a_rounds">3, 4, 5</param>
            /// <param name="a_hash_size">128, 160, 192, 224, 256</param>
            /// <returns></returns>
            public static IHash CreateHaval(HashRounds a_rounds, HashLib.HashSize a_hash_size)
            {
                switch (a_rounds)
                {
                    case HashRounds.Rounds3:

                        switch (a_hash_size)
                        {
                            case HashLib.HashSize.HashSize128: return CreateHaval_3_128();
                            case HashLib.HashSize.HashSize160: return CreateHaval_3_160();
                            case HashLib.HashSize.HashSize192: return CreateHaval_3_192();
                            case HashLib.HashSize.HashSize224: return CreateHaval_3_224();
                            case HashLib.HashSize.HashSize256: return CreateHaval_3_256();
                            default: throw new ArgumentException();
                        }

                    case HashRounds.Rounds4:

                        switch (a_hash_size)
                        {
                            case HashLib.HashSize.HashSize128: return CreateHaval_4_128();
                            case HashLib.HashSize.HashSize160: return CreateHaval_4_160();
                            case HashLib.HashSize.HashSize192: return CreateHaval_4_192();
                            case HashLib.HashSize.HashSize224: return CreateHaval_4_224();
                            case HashLib.HashSize.HashSize256: return CreateHaval_4_256();
                            default: throw new ArgumentException();
                        }

                    case HashRounds.Rounds5:

                        switch (a_hash_size)
                        {
                            case HashLib.HashSize.HashSize128: return CreateHaval_5_128();
                            case HashLib.HashSize.HashSize160: return CreateHaval_5_160();
                            case HashLib.HashSize.HashSize192: return CreateHaval_5_192();
                            case HashLib.HashSize.HashSize224: return CreateHaval_5_224();
                            case HashLib.HashSize.HashSize256: return CreateHaval_5_256();
                            default: throw new ArgumentException();
                        }

                    default: throw new ArgumentException();
                }
            }

            public static IHash CreateMD2()
            {
                return new HashLib.Crypto.MD2();
            }

            public static IHash CreateMD4()
            {
                return new HashLib.Crypto.MD4();
            }

            public static IHash CreateMD5()
            {
                return new HashLib.Crypto.MD5();
            }

            public static IHash CreatePanama()
            {
                return new HashLib.Crypto.Panama();
            }

            public static IHash CreateRadioGatun32()
            {
                return new HashLib.Crypto.RadioGatun32();
            }

            public static IHash CreateRadioGatun64()
            {
                return new HashLib.Crypto.RadioGatun64();
            }

            public static IHash CreateRIPEMD()
            {
                return new HashLib.Crypto.RIPEMD();
            }

            public static IHash CreateRIPEMD128()
            {
                return new HashLib.Crypto.RIPEMD128();
            }

            public static IHash CreateRIPEMD160()
            {
                return new HashLib.Crypto.RIPEMD160();
            }

            public static IHash CreateRIPEMD256()
            {
                return new HashLib.Crypto.RIPEMD256();
            }

            public static IHash CreateRIPEMD320()
            {
                return new HashLib.Crypto.RIPEMD320();
            }

            public static IHash CreateSHA0()
            {
                return new HashLib.Crypto.SHA0();
            }

            public static IHash CreateSHA1()
            {
                return new HashLib.Crypto.SHA1();
            }

            public static IHash CreateSHA224()
            {
                return new HashLib.Crypto.SHA224();
            }

            public static IHash CreateSHA256()
            {
                return new HashLib.Crypto.SHA256();
            }

            public static IHash CreateSHA384()
            {
                return new HashLib.Crypto.SHA384();
            }

            public static IHash CreateSHA512()
            {
                return new HashLib.Crypto.SHA512();
            }

            public static IHash CreateSnefru_4_128()
            {
                return new HashLib.Crypto.Snefru_4_128();
            }

            public static IHash CreateSnefru_4_256()
            {
                return new HashLib.Crypto.Snefru_4_256();
            }

            public static IHash CreateSnefru_8_128()
            {
                return new HashLib.Crypto.Snefru_8_128();
            }

            public static IHash CreateSnefru_8_256()
            {
                return new HashLib.Crypto.Snefru_8_256();
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="a_rounds">4, 8</param>
            /// <param name="a_hash_size">128, 256</param>
            /// <returns></returns>
            public static IHash CreateSnefru(HashRounds a_rounds, HashLib.HashSize a_hash_size)
            {
                switch (a_rounds)
                {
                    case HashRounds.Rounds4:

                        switch (a_hash_size)
                        {
                            case HashLib.HashSize.HashSize128: return CreateSnefru_4_128();
                            case HashLib.HashSize.HashSize256: return CreateSnefru_4_256();
                            default: throw new ArgumentException();
                        }

                    case HashRounds.Rounds8:

                        switch (a_hash_size)
                        {
                            case HashLib.HashSize.HashSize128: return CreateSnefru_8_128();
                            case HashLib.HashSize.HashSize256: return CreateSnefru_8_256();
                            default: throw new ArgumentException();
                        }

                    default: throw new ArgumentException();
                }
            }

            public static IHash CreateTiger_3_192()
            {
                return new HashLib.Crypto.Tiger_3_192();
            }

            public static IHash CreateTiger_4_192()
            {
                return new HashLib.Crypto.Tiger_4_192();
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="a_rounds">3, 4</param>
            /// <returns></returns>
            public static IHash CreateTiger(HashRounds a_rounds)
            {
                switch (a_rounds)
                {
                    case HashRounds.Rounds3: return CreateTiger_3_192();
                    case HashRounds.Rounds4: return CreateTiger_4_192();
                    default: throw new ArgumentException();
                }
            }

            public static IHash CreateTiger2()
            {
                return new HashLib.Crypto.Tiger2();
            }

            public static IHash CreateWhirlpool()
            {
                return new HashLib.Crypto.Whirlpool();
            }
        }

        public static class HMAC
        {
            public static IHMAC CreateHMAC(IHash a_hash)
            {
                if (a_hash is IHMAC)
                {
                    return (IHMAC)a_hash;
                }
                //else if (a_hash is IHasHMACBuildIn)
                //{
                //    IHasHMACBuildIn h = (IHasHMACBuildIn)a_hash;
                //    return new HMACBuildInAdapter(h.GetBuildHMAC(), h.BlockSize);
                //}
                else
                {
                    return new HMACNotBuildInAdapter(a_hash);
                }
            }
        }

        public static class Wrappers
        {
            //public static System.Security.Cryptography.HashAlgorithm HashToHashAlgorithm(IHash a_hash)
            //{
            //    return new HashAlgorithmWrapper(a_hash);
            //}

            //public static IHash HashAlgorithmToHash(System.Security.Cryptography.HashAlgorithm a_hash,
            //    int a_block_size = -1)
            //{
            //    return new HashCryptoBuildIn(a_hash, a_block_size);
            //}
        }
    }
}
