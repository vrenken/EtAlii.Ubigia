// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;

    public class ConfigurationSpaceGetter : IConfigurationSpaceGetter
    {
        private readonly IProviderContext _context;

        public ConfigurationSpaceGetter(IProviderContext context)
        {
            _context = context;
        }

        public ConfigurationSpace[] GetAll()
        {
            var result = new List<ConfigurationSpace>();

            var task = Task.Run(async () =>
            {
                var accounts = await _context.ManagementConnection.Accounts.GetAll();
                foreach (var account in accounts)
                {
                    var spaces = await _context.ManagementConnection.Spaces.GetAll(account.Id);
                    var configurationsToAdd = spaces
                        .Where(s => s.Name == SpaceName.Configuration)
                        .Select(s => new ConfigurationSpace {Account = account, Space = s});
                    result.AddRange(configurationsToAdd);
                }
            });
            task.Wait();
            return result.ToArray();
        }
    }
}