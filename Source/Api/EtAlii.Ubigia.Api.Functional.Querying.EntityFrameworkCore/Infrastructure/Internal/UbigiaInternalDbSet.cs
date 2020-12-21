// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore.Infrastructure.Internal
{
    using JetBrains.Annotations;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Internal;

    #pragma warning disable EF1001
    public class UbigiaInternalDbSet<TEntity> : InternalDbSet<TEntity>, IUbigiaDbSet<TEntity>
    #pragma warning restore EF1001
        where TEntity : class
    {
        public UbigiaInternalDbSet([NotNull] DbContext context, [CanBeNull] string entityTypeName) 
#pragma warning disable EF1001
            : base(context, entityTypeName)
#pragma warning restore EF1001
        {
        }
    }
}