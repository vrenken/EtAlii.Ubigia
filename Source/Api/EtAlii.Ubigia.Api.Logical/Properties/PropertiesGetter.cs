// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.xTechnology.Collections;

    public class PropertiesGetter : IPropertiesGetter
    {
        private readonly IFabricContext _fabric;

        public PropertiesGetter(IFabricContext fabric)
        {
            _fabric = fabric;
        }

        public async Task<PropertyDictionary> Get(Identifier identifier, ExecutionScope scope)
        {
            PropertyDictionary result = null;
            do
            {
                if (identifier != Identifier.Empty)
                {
                    result = await _fabric.Properties.Retrieve(identifier, scope).ConfigureAwait(false);
                    if (result == null)
                    {
                        var entries = await _fabric.Entries
                            .GetRelated(identifier, EntryRelation.Downdate, scope)
                            .ToArrayAsync()
                            .ConfigureAwait(false);
                        if (entries.Multiple())
                        {
                            throw new NotSupportedException("The PropertiesGetter cannot handle multiple downdates.");
                        }

                        if (!entries.Any())
                        {
                            break;
                        }

                        identifier = entries.Select(e => e.Id).Single();
                    }
                }
            } while (result == null);

            return result;
        }
    }
}