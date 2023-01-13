// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using EtAlii.Ubigia.Infrastructure.Fabric;

public class LogicalAccountSet : ILogicalAccountSet
{
    private readonly IFabricContext _fabric;
    private readonly object _lockObject = new();

    private const string Folder = "Accounts";

    private ObservableCollection<Account> Items { get { lock (_lockObject) { return _items ??= InitializeItems(); } } }
    private ObservableCollection<Account> _items; // We don't us a Lazy construction here because the first get of this property is actually cascaded through the logical layer. A Lazy instance results in a deadlock.

    public LogicalAccountSet(IFabricContext fabric)
    {
        _fabric = fabric;
    }

    /// <inheritdoc />
    public IAsyncEnumerable<Account> GetAll()
    {
        return _fabric.Items.GetAll(Items);
    }

    /// <inheritdoc />
    public async Task<Account> Get(Guid id)
    {
        return await _fabric.Items.Get(Items, id).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public Task<Account> Get(string accountName)
    {
        var account = Items.SingleOrDefault(account => account.Name == accountName);
        return Task.FromResult(account);
    }

    /// <inheritdoc />
    public Task<Account> Get(string accountName, string password)
    {
        var account = Items.SingleOrDefault(account => account.Name == accountName && account.Password == password);
        return Task.FromResult(account);
    }

    private bool CanAddFunction(IList<Account> items, Account item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item), "No item specified");
        }

        var canAdd = !string.IsNullOrWhiteSpace(item.Name);
        if (canAdd)
        {
            canAdd = item.Id == Guid.Empty;
        }
        if (canAdd)
        {
            canAdd = !items.Any(i => string.CompareOrdinal(i.Name, item.Name) == 0 || i.Id == item.Id);
        }
        return canAdd;
    }

    private Account UpdateFunction(Account originalItem, Account updatedItem)
    {
        originalItem.Name = updatedItem.Name;
        originalItem.Password = updatedItem.Password;
        originalItem.Roles = updatedItem.Roles;

        // We should not update the created date of the account.
        //originalItem.Created = updatedItem.Created

        // Whenever we touch the account we also register the updated time.
        originalItem.Updated = DateTime.UtcNow;

        return originalItem;
    }

    private ObservableCollection<Account> InitializeItems()
    {
        var task = _fabric.Items.GetItems<Account>(Folder);
        return task.GetAwaiter().GetResult();
    }

    /// <inheritdoc />
    public async Task<(Account, bool)> Add(Account item, AccountTemplate template)
    {
        // We want to make absolutely sure that the account has the roles described by the template.
        item.Roles = item.Roles
            .Concat(template.RolesToAssign)
            .Distinct()
            .ToArray();

        var account = await _fabric.Items.Add(Items, CanAddFunction, item).ConfigureAwait(false);

        var isAdded = account != null;
        return (account, isAdded);
    }

    /// <inheritdoc />
    public Task Remove(Guid itemId)
    {
        return _fabric.Items.Remove(Items, itemId);
    }

    /// <inheritdoc />
    public Task Remove(Account itemToRemove)
    {
        return _fabric.Items.Remove(Items, itemToRemove);
    }

    /// <inheritdoc />
    public Task<Account> Update(Guid itemId, Account updatedItem)
    {
        return _fabric.Items.Update(Items, UpdateFunction, Folder, itemId, updatedItem);
    }
}
