namespace EtAlii.Servus.Infrastructure.WebApi.Portal.Admin
{
    using System.Collections.Generic;

    public class GoogleMailAddressConverter : IGoogleMailAddressConverter
    {
        public GoogleMailAddress Convert(dynamic googleMailAddress)
        {
            var isPrimary = false;
            object isPrimaryValue;
            if (((IDictionary<string, object>)googleMailAddress.metadata).TryGetValue("primary", out isPrimaryValue))
            {
                isPrimary = (bool)isPrimaryValue;
            }

            return new GoogleMailAddress
            {
                IsPrimary = isPrimary,
                Address = googleMailAddress.value,
            };
        }
    }
}