namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Rest
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class IdentifierBinder : IModelBinder
    {
        private readonly string[] _locationSplitCharacters = { IdentifierSplitter.Location };
        private readonly string[] _timeSplitCharacters = { IdentifierSplitter.Time };
        private readonly string[] _partSplitCharacters = { IdentifierSplitter.Part };

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType != typeof(Identifier))
            {
                return Task.FromException(new InvalidOperationException("Only able to bind to Identifier parameters"));
            }

            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            //string rawValue = value.RawValue as string
            var rawValue = value.FirstValue;
            if (rawValue == null)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Wrong value type");
                return Task.FromException(new InvalidOperationException("Wrong value type"));
            }

            var parts = rawValue.Split(_partSplitCharacters, StringSplitOptions.None);
            var locationPart = parts[0];
            var timePart = parts[1];

            var locations = locationPart.Split(_locationSplitCharacters, StringSplitOptions.None);
            var times = timePart.Split(_timeSplitCharacters, StringSplitOptions.None);

            var model = Identifier.Create
            (
                new Guid(locations[0]),
                new Guid(locations[1]),
                new Guid(locations[2]),
                ulong.Parse(times[0]),
                ulong.Parse(times[1]),
                ulong.Parse(times[2])
            );

            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }
}
