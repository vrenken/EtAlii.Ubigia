// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;
    using global::Google.Contacts;

    public class PersonSetter : IPersonSetter
    {
        public void Set(IDataContext context, Contact person)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }

            var familyName = person.Name?.FamilyName ?? person.ContactEntry.Name?.FamilyName;
            var givenName = person.Name?.GivenName ?? person.ContactEntry.Name?.GivenName;

            if (givenName != null && familyName != null)
            {
                var script = new[]
                {
                    $"/Person += \"{familyName}\"/\"{givenName}\"",
                    $"<= /Person/\"{familyName}\"/\"{givenName}\" <= $details"
                };

                dynamic details = new
                {
                    Birthday = person.ContactEntry.Birthday,
                    //Birthday = context.Indexes.GetTime(contact.ContactEntry.Birthday),
                    NickName = person.ContactEntry.Nickname,
                    Initials = person.ContactEntry.Initials,
                    PrimaryEmail = person.PrimaryEmail?.Address ?? person.ContactEntry.PrimaryEmail?.Address,
                    PrimaryPhonenumber = person.PrimaryPhonenumber?.Uri ?? person.ContactEntry.PrimaryPhonenumber?.Uri,
                };

                var task = Task.Run(async () =>
                {
                    var scope = new ScriptScope();
                    scope.Variables.Add("details", new ScopeVariable(details, "Value"));
                    var lastSequence = await context.Scripts.Process(script, scope);
                    await lastSequence.Output.SingleOrDefaultAsync();

                    //var lastSequence2 = await context.Scripts.Process($"<= /Person/\"{familyName}\"/\"{givenName}\"");
                    //var result2 = await lastSequence2.Output.LastOrDefaultAsync();
                });
                task.Wait();
            }
        }
    }
}
