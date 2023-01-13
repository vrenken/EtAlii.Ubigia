// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional;

using System;
using System.Linq;
using System.Threading.Tasks;
using EtAlii.Ubigia.Infrastructure.Logical;

internal class LocalStorageGetter : ILocalStorageGetter
{
    private Storage _localStorage;

    private readonly ILogicalContext _logicalContext;
    private readonly ILocalStorageInitializer _localStorageInitializer;

    public LocalStorageGetter(
        FunctionalContextOptions options,
        ILogicalContext logicalContext,
        ILocalStorageInitializer localStorageInitializer)
    {
        _logicalContext = logicalContext;
        _localStorageInitializer = localStorageInitializer;
        _localStorage = new Storage
        {
            Id = Guid.NewGuid(), // TODO: This should be a system-specific Guid, persisted on disk somehow.
            Address = options.StorageAddress.ToString(),
            Name = options.Name,
        };
    }

    public Storage GetLocal() =>  _localStorage;

    public async Task Initialize()
    {
        // Improve this method and use a better way to decide if the local Storage needs to be added.
        // This current test to see if the local storage has already been added is not very stable/scalable.
        // Please find another way to determine that the local storage needs initialization.
        // More details can be found in the Github issue below:
        // https://github.com/vrenken/EtAlii.Ubigia/issues/94


        var items = await _logicalContext.Storages
            .GetAll()
            .ToArrayAsync()
            .ConfigureAwait(false);

        var alreadyRegisteredLocalStorage = items.SingleOrDefault(s => s.Name == _localStorage.Name);
        if (alreadyRegisteredLocalStorage == null)
        {
            await _logicalContext.Storages
                .AddLocalStorage(_localStorage)
                .ConfigureAwait(false);

            await _localStorageInitializer
                .Initialize(_localStorage)
                .ConfigureAwait(false);
        }
        else
        {
            _localStorage = alreadyRegisteredLocalStorage;
        }
    }
}
