namespace EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.Infrastructure;

    public interface IUbigiaDbSet<TEntity> : IQueryable<TEntity>, IAsyncEnumerable<TEntity>, IInfrastructure<IServiceProvider>, IListSource
        where TEntity: class
    {
        EntityEntry<TEntity> Add([NotNull] TEntity entity);
    }
}