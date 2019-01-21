// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
	using System.Collections.Generic;
	using System.Linq;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.xTechnology.Logging;
    using global::Google.Apis.PeopleService.v1.Data;

    public class DebuggingPersonSetter : IPersonSetter
    {
        private readonly IPersonSetter _decoree;
        private readonly ILogger _logger;

        public DebuggingPersonSetter(IPersonSetter decoree, ILogger logger)
        {
			_decoree = decoree;
            _logger = logger;
        }

        public void Set(IGraphSLScriptContext context, Person person)
        {
	        var name = GetName(person.Names) ??
	                   //GetName(person.ContactEntry.Name) ??
	                   GetEmail(person.EmailAddresses);
					   //GetEmail(person.PrimaryEmail) ??
        //               GetEmail(person.ContactEntry.PrimaryEmail) ??
        //               GetEmail(person.Emails) ?? GetEmail(person.ContactEntry.Emails);

            _logger.Info($"Setting contact: {name}");

            _decoree.Set(context, person);
        }

        private string GetName(IList<Name> names)
        {
            var familyName = names.FirstOrDefault()?.FamilyName;
            var givenName = names.FirstOrDefault()?.GivenName;
            if (familyName != null && givenName != null)
            {
                return $"{givenName} {familyName}";
            }
            else return null;
        }

        private string GetEmail(IList<EmailAddress> emails)
        {
            var email = emails.FirstOrDefault();
            return email?.Value;
        }
    }
}