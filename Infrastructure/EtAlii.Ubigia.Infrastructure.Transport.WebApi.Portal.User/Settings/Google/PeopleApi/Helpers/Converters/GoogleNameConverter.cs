namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi.Portal.Admin
{
    using System.Collections.Generic;

    public class GoogleNameConverter : IGoogleNameConverter
    {
        public GoogleName Convert(dynamic googleName)
        {
            var isPrimary = false;
            object isPrimaryValue;
            if (((IDictionary<string, object>)googleName.metadata).TryGetValue("primary", out isPrimaryValue))
            {
                isPrimary = (bool)isPrimaryValue;
            }
;
            return new GoogleName
            {
                IsPrimary = isPrimary,
                DisplayName = googleName.displayName,
                DisplayNameLastFirst = googleName.displayNameLastFirst,
                FamilyName = googleName.familyName,
                GivenName = googleName.givenName,
            };
        }
    }
}