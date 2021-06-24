// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.MicroContainer
{
    /// <summary>
    /// This interface is useful to make consistent groups of container registrations,
    /// which essentially are the 'scaffolding' on which an application gets instantiated.
    /// </summary>

    public interface IScaffolding
    {
        /// <summary>
        /// Run the scaffolding and corresponding container register calls it contains.
        /// </summary>
        /// <param name="container"></param>
        void Register(Container container);
    }
}
