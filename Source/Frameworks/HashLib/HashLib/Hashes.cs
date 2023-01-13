using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace HashLib;

public static class Hashes
{
    public static readonly ReadOnlyCollection<Type> All;
    public static readonly ReadOnlyCollection<Type> AllUnique;
    public static readonly ReadOnlyCollection<Type> Hash32;
    public static readonly ReadOnlyCollection<Type> Hash64;
    public static readonly ReadOnlyCollection<Type> Hash128;
    public static readonly ReadOnlyCollection<Type> CryptoAll;
    public static readonly ReadOnlyCollection<Type> CryptoNotBuildIn;
    //public readonly static ReadOnlyCollection<Type> CryptoBuildIn;
    //public readonly static ReadOnlyCollection<Type> HasHMACBuildIn;

    public static readonly ReadOnlyCollection<Type> NonBlock;
    public static readonly ReadOnlyCollection<Type> FastComputes;
    public static readonly ReadOnlyCollection<Type> Checksums;
    public static readonly ReadOnlyCollection<Type> WithKey;

    static Hashes()
    {
        All = new ReadOnlyCollection<Type>((from hf in typeof(IHash).GetTypeInfo().Assembly.ExportedTypes
            where hf.GetTypeInfo().IsClass
            where !hf.GetTypeInfo().IsAbstract
            where hf != typeof(HMACNotBuildInAdapter)
            //where hf != typeof(HashCryptoBuildIn)
            //where hf != typeof(HMACBuildInAdapter)
            where hf.IsImplementInterface(typeof(IHash))
            where !hf.GetTypeInfo().IsNested
            select hf).ToList());

        All = new ReadOnlyCollection<Type>((from hf in All
            orderby hf.Name
            select hf).ToList());

        var x2 = new[]
        {
            //typeof(HashLib.Crypto.BuildIn.SHA1Cng),
            //typeof(HashLib.Crypto.BuildIn.SHA1Managed),
            //typeof(HashLib.Crypto.BuildIn.SHA256Cng),
            //typeof(HashLib.Crypto.BuildIn.SHA256Managed),
            //typeof(HashLib.Crypto.BuildIn.SHA384Cng),
            //typeof(HashLib.Crypto.BuildIn.SHA384Managed),
            //typeof(HashLib.Crypto.BuildIn.SHA512Cng),
            //typeof(HashLib.Crypto.BuildIn.SHA512Managed),
            typeof(Crypto.MD5),
            typeof(Crypto.RIPEMD160),
            typeof(Crypto.SHA1),
            typeof(Crypto.SHA256),
            typeof(Crypto.SHA384),
            typeof(Crypto.SHA512),
        };

        AllUnique = new ReadOnlyCollection<Type>((from hf in All
            where !(hf.IsDerivedFrom(typeof(Hash32.DotNet)))
            where !x2.Contains(hf)
            where !hf.IsNested
            select hf).ToList());

        Hash32 = new ReadOnlyCollection<Type>((from hf in All
            where hf.IsImplementInterface(typeof(IHash32))
            where !hf.IsImplementInterface(typeof(IChecksum))
            select hf).ToList());

        Hash64 = new ReadOnlyCollection<Type>((from hf in All
            where hf.IsImplementInterface(typeof(IHash64))
            where !hf.IsImplementInterface(typeof(IChecksum))
            select hf).ToList());

        Hash128 = new ReadOnlyCollection<Type>((from hf in All
            where hf.IsImplementInterface(typeof(IHash128))
            where !hf.IsImplementInterface(typeof(IChecksum))
            select hf).ToList());

        Checksums = new ReadOnlyCollection<Type>((from hf in All
            where hf.IsImplementInterface(typeof(IChecksum))
            select hf).ToList());

        FastComputes = new ReadOnlyCollection<Type>((from hf in All
            where hf.IsImplementInterface(typeof(IFastHash32))
            select hf).ToList());

        NonBlock = new ReadOnlyCollection<Type>((from hf in All
            where hf.IsImplementInterface(typeof(INonBlockHash))
            select hf).ToList());

        WithKey = new ReadOnlyCollection<Type>((from hf in All
            where hf.IsImplementInterface(typeof(IWithKey))
            select hf).ToList());

        CryptoAll = new ReadOnlyCollection<Type>((from hf in All
            where hf.IsImplementInterface(typeof(ICrypto))
            select hf).ToList());

        CryptoNotBuildIn = new ReadOnlyCollection<Type>((from hf in CryptoAll
            where hf.IsImplementInterface(typeof(ICryptoNotBuildIn))
            select hf).ToList());

        //CryptoBuildIn = (from hf in CryptoAll
        //                 where hf.IsImplementInterface(typeof(ICryptoBuildIn))
        //                 select hf).ToList().AsReadOnly();

        //HasHMACBuildIn = (from hf in CryptoBuildIn
        //                  where hf.IsImplementInterface(typeof(IHasHMACBuildIn))
        //                  select hf).ToList().AsReadOnly();
    }
}
