// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.MicroContainer;

using System;

public interface IServiceCollection
{
    /// <summary>
    /// Instantiates and returns an instance that got configured for the specified interface.
    /// If the instance requires any constructor parameters these will also get instantiated and injected.
    ///
    /// The container works in a way that it tries to create the constructor-injected objects dependency tree.
    /// During this creation it will run all requested initializations immediately after the construction of
    /// a single object, and after the root object has been created it will run all lazy registrations.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">In case one of the objects in the DI graph cannot be initialized.
    /// For example due to a missing registration or when a cyclic dependency has been defined.</exception>
    T GetInstance<T>();
}
