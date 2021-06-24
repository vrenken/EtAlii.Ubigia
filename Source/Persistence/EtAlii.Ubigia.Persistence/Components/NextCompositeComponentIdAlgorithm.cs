// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class NextCompositeComponentIdAlgorithm : INextCompositeComponentIdAlgorithm
    {
        private readonly Dictionary<ContainerIdentifier, ulong> _cached = new();

        public ulong Create(ContainerIdentifier containerIdentifier)
        {
            // We use tickcounts to start with.
            var proposed = (ulong) DateTime.UtcNow.Ticks;

            // However, on speedy machines these are not unique enough. so let's cache them for each container and increment them on each next request.
            var actual = proposed;
            if(_cached.TryGetValue(containerIdentifier, out var previous))
            {
                previous += 1;
                // if a container is idle for a while the proposed value will be bigger then the incremented cached one and we are back on track to use that one.
                actual = proposed > previous ? proposed : previous;
            }

            // Everything that is older then the proposed value is no longer needed.
            var toRemove = _cached
                .Where(kvp => kvp.Value < proposed)
                .ToArray();
            foreach (var kvp in toRemove)
            {
                _cached.Remove(kvp.Key);
            }

            _cached[containerIdentifier] = actual;

            return actual;
        }
    }
}
