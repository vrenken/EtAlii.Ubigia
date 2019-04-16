namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Logical;

    internal class AccountInitializer : IAccountInitializer
    {
        private readonly ILogicalContext _context;

        public AccountInitializer(ILogicalContext context)
        {
            _context = context;
        }

        public void Initialize(Account account, AccountTemplate template)
        {
            var accountId = account.Id;
            var spaces = _context.Spaces.GetAll();

            if (spaces.Any(space => space.AccountId == accountId))
            {
                throw new InvalidOperationException("The account already contains a space");
            }

            var duplicates = template.SpacesToCreate
                .GroupBy(s => s.Name)
                .Where(g => g.Count() > 1)
                .Select(y => y.Key);

            if (duplicates.Any())
            {
                throw new InvalidOperationException("The account template contains duplicate space definitions");
            }

            // And finally let's add the spaces that 
            foreach (var spaceToCreate in template.SpacesToCreate)
            {
                // ReSharper disable once UnusedVariable
                var space = _context.Spaces.Add(new Space { AccountId = accountId, Name = spaceToCreate.Name }, spaceToCreate, out bool _);
            }
        }
    }
}