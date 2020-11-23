// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using System.Collections.Generic;
    using System.Linq;

    public class ConfigurationSpaceGetter : IConfigurationSpaceGetter
    {
        private readonly IProviderContext _context;

        public ConfigurationSpaceGetter(IProviderContext context)
        {
            _context = context;
        }

        public async IAsyncEnumerable<ConfigurationSpace> GetAll()
        {
            var accounts = _context.ManagementConnection.Accounts.GetAll();
            await foreach (var account in accounts)
            {
                var result = _context.ManagementConnection.Spaces
                    .GetAll(account.Id)
                    .Where(s => s.Name == SpaceName.Configuration)
                    .Select(s => new ConfigurationSpace {Account = account, Space = s});
                
                await foreach (var item in result.ConfigureAwait(false))
                {
                    yield return item;
                }
            }
        }
    }
}