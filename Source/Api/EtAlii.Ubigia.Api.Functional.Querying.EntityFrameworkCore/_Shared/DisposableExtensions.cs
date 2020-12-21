// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Utilities
{
    internal static class DisposableExtensions
    {
        public static ValueTask DisposeAsyncIfAvailable([CanBeNull] this IDisposable disposable)
        {
            if (disposable != null)
            {
                if (disposable is IAsyncDisposable asyncDisposable)
                {
                    return asyncDisposable.DisposeAsync();
                }

                disposable.Dispose();
            }

            return default;
        }
    }
}
