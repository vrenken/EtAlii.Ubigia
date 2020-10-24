// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using global::Google.Apis.PeopleService.v1.Data;
    using Serilog;

    public class DebuggingPersonSetter : IPersonSetter
    {
        private readonly IPersonSetter _decoree;
        private readonly ILogger _logger = Log.ForContext<IPersonSetter>();

        public DebuggingPersonSetter(IPersonSetter decoree)
        {
			_decoree = decoree;
        }

        public Task Set(IGraphSLScriptContext context, Person person)
        {
	        var name = GetName(person.Names) ??
	                   //GetName(person.ContactEntry.Name) ??
	                   GetEmail(person.EmailAddresses);
					   //GetEmail(person.PrimaryEmail) ??
        //               GetEmail(person.ContactEntry.PrimaryEmail) ??
        //               GetEmail(person.Emails) ?? GetEmail(person.ContactEntry.Emails)

            _logger.Information("Setting contact: {ContactName}", name);

            return _decoree.Set(context, person);
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

//        private string GetEmail(EmailAddress email)
//        [
//            return email?.Value
//        ]
        private string GetEmail(IList<EmailAddress> emails)
        {
            var email = emails.FirstOrDefault();
            return email?.Value;
        }
    }
}