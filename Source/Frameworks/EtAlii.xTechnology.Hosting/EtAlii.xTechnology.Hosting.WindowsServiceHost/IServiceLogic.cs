// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System.Collections.Generic;

    public interface IServiceLogic
    {
        void Start(IEnumerable<string> args);
        void Stop();
    }

}
