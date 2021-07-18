// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

#if NETSTANDARD2_0 || NETSTANDARD2_1 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET45 || NET451 || NET452 || NET6 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48

// ReSharper disable once CheckNamespace
namespace System.Runtime.CompilerServices
{
    // See: https://www.mking.net/blog/error-cs0518-isexternalinit-not-defined
    using System.ComponentModel;

    /// <summary>
    /// A workaround to facilitate init properties in non .NET 5 projects.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal static class IsExternalInit { }
}

#endif
