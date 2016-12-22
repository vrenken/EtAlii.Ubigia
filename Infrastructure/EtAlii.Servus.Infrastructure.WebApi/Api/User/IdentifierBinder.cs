namespace EtAlii.Servus.Infrastructure.WebApi
{
    using System;
    using System.Web.Http.Controllers;
    using System.Web.Http.ModelBinding;
    using EtAlii.Servus.Api;

    internal class IdentifierBinder : IModelBinder
    {
        private readonly string[] _locationSplitCharacters = new string[] { IdentifierSplitter.Location };
        private readonly string[] _timeSplitCharacters = new string[] { IdentifierSplitter.Time };
        private readonly string[] _partSplitCharacters = new string[] { IdentifierSplitter.Part };

        public bool BindModel(HttpActionContext actionContext,
                              ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType != typeof(Identifier))
            {
                return false;
            }

            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (value == null)
            {
                return false;
            }

            string rawValue = value.RawValue as string;
            if (rawValue == null)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Wrong value type");
                return false;
            }

            var parts = rawValue.Split(_partSplitCharacters, StringSplitOptions.None);
            var locationPart = parts[0];
            var timePart = parts[1];

            var locations = locationPart.Split(_locationSplitCharacters, StringSplitOptions.None);
            var times = timePart.Split(_timeSplitCharacters, StringSplitOptions.None);

            bindingContext.Model = Identifier.Create
            (
                new Guid(locations[0]),
                new Guid(locations[1]),
                new Guid(locations[2]),
                UInt64.Parse(times[0]),
                UInt64.Parse(times[1]),
                UInt64.Parse(times[2])
            );
            return true;
        }
    }
}
