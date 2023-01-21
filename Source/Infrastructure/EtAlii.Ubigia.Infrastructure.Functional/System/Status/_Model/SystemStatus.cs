// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional;

public enum SystemStatus
{
    /// <summary>
    /// If some prerequisite is missing and the system really needs to be setup accordingly.
    /// </summary>
    SetupIsNeeded = 0,

    /// <summary>
    /// Something is wrong, but the mandatory initial prerequisites are met.
    /// </summary>
    SystemIsNonOperational = 1,

    /// <summary>
    /// If the system can function. That is, setup or maintenance can still be needed,
    /// but the system nevertheless is able to deliver content through its APIs.
    /// </summary>
    SystemIsOperational = 2,
}
