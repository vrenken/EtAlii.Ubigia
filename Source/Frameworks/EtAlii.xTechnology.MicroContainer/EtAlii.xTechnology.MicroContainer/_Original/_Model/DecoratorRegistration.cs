// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

#if USE_ORIGINAL_CONTAINER

namespace EtAlii.xTechnology.MicroContainer
{
    using System;

    /// <summary>
    /// This internal class contains the per-instance know-how needed to decorate the specific object once.
    /// Multiple decorates are possible and will be used in a first-come-first-serve order.
    /// This means the first decorator will be the first to wrap the object, the second will wrap the first decorator and so on.
    /// </summary>
    /// <remarks>Reason for the usage of members instead of properties is speed - Especially for bigger, more complex container usage.</remarks>

    internal class DecoratorRegistration
    {
        public Type? DecoratorType;
        public Type ServiceType = null!;
    }
}
#endif
