﻿namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Rest.AspNetCore
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class IdentifiersBinder : IModelBinder
    {
        private readonly string[] _locationSplitCharacters = new[] { IdentifierSplitter.Location };
        private readonly string[] _timeSplitCharacters = new[] { IdentifierSplitter.Time };
        private readonly string[] _partSplitCharacters = new[] { IdentifierSplitter.Part };
//        private readonly string[] _identifierSplitCharacters = new[] { IdentifierSplitter.Separator };

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType != typeof(Identifier))
            {
                return Task.FromException(new InvalidOperationException("Only able to bind to Identifier parameters"));
            }

            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            //string rawValue = value.RawValue as string;
            var rawValue = value.FirstValue;
            if (rawValue == null)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Wrong value type");
                return Task.FromException(new InvalidOperationException("Wrong value type"));
            }

            if (rawValue.IndexOf(IdentifierSplitter.Separator) == -1)
            {
                return Task.FromException(new InvalidOperationException("No separator found"));
            }

            var identifiersAsStrings = rawValue.Split(_partSplitCharacters, StringSplitOptions.None);

            bindingContext.Model = identifiersAsStrings.Select(ToIdentifier);
            return Task.CompletedTask;
        }

        private Identifier ToIdentifier(string identifierAsString)
        {
            var parts = identifierAsString.Split(_partSplitCharacters, StringSplitOptions.None);
            var locationPart = parts[0];
            var timePart = parts[1];

            var locations = locationPart.Split(_locationSplitCharacters, StringSplitOptions.None);
            var times = timePart.Split(_timeSplitCharacters, StringSplitOptions.None);

            return Identifier.Create
            (
                new Guid(locations[0]),
                new Guid(locations[1]),
                new Guid(locations[2]),
                UInt64.Parse(times[0]),
                UInt64.Parse(times[1]),
                UInt64.Parse(times[2])
            );
        }
    }
}
