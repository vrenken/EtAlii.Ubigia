// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;
    using global::Google.Apis.PeopleService.v1.Data;

    public class PersonSetter : IPersonSetter
    {
        public Task Set(IGraphSLScriptContext context, Person person)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }
            return SetInternal(context, person);
        }

        private async Task SetInternal(IGraphSLScriptContext context, Person person)
        {
            var familyName = person.Names.FirstOrDefault()?.FamilyName ?? person.Names.FirstOrDefault()?.FamilyName;
            var givenName = person.Names.FirstOrDefault()?.GivenName ?? person.Names.FirstOrDefault()?.GivenName;

            if (givenName != null && familyName != null)
            {
                var script = new[]
                {
                    $"/Person += \"{familyName}\"/\"{givenName}\"",
                    $"<= /Person/\"{familyName}\"/\"{givenName}\" <= $details"
                };

                dynamic details = new
                {
                    Birthday = person.Birthdays.FirstOrDefault(),
                    //Birthday = context.Indexes.GetTime(contact.ContactEntry.Birthday),
                    NickName = person.Nicknames.FirstOrDefault(),
                    //Initials = person..ContactEntry.Initials,
                    PrimaryEmail = person.EmailAddresses.FirstOrDefault()?.Value,
                    PrimaryPhonenumber = person.PhoneNumbers.FirstOrDefault()?.Value,
                };

                var scope = new ScriptScope();
                scope.Variables.Add("details", new ScopeVariable(details, "Value"));
                var lastSequence = await context.Process(script, scope);
                await lastSequence.Output.SingleOrDefaultAsync();

                //var lastSequence2 = await context.Scripts.Process($"<= /Person/\"[familyName]\"/\"[givenName]\"")
                //var result2 = await lastSequence2.Output.LastOrDefaultAsync()
            }
        }
    }
}
