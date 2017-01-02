// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.xTechnology.Logging;
    using global::Google.Contacts;
    using global::Google.GData.Extensions;

    public class DebuggingPersonSetter : IPersonSetter
    {
        private readonly IPersonSetter _decoree;
        private readonly ILogger _logger;

        public DebuggingPersonSetter(IPersonSetter decoree, ILogger logger)
        {
            _decoree = decoree;
            _logger = logger;
        }

        public void Set(IDataContext context, Contact person)
        {
            var name = GetName(person.Name) ??
                       GetName(person.ContactEntry.Name) ??
                       GetEmail(person.PrimaryEmail) ??
                       GetEmail(person.ContactEntry.PrimaryEmail) ??
                       GetEmail(person.Emails) ?? GetEmail(person.ContactEntry.Emails);

            _logger.Info($"Setting contact: {name}");

            _decoree.Set(context, person);
        }

        private string GetName(Name name)
        {
            var familyName = name?.FamilyName;
            var givenName = name?.GivenName;
            if (familyName != null && givenName != null)
            {
                return $"{givenName} {familyName}";
            }
            else return null;
        }

        private string GetEmail(EMail email)
        {
            return email?.Address;
        }

        private string GetEmail(ExtensionCollection<EMail> emails)
        {
            var email = emails.FirstOrDefault();
            return email?.Address;
        }
    }
}