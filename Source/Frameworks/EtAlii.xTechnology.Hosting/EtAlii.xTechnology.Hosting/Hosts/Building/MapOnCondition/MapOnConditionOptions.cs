// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting;

using System;
using Microsoft.AspNetCore.Http;

/// <summary>
/// Options for the <see cref="MapOnConditionMiddleware"/>.
/// </summary>
public class MapOnConditionOptions
{
    private readonly Func<HttpContext, bool> _predicate;

    /// <summary>
    /// The user callback that determines if the branch should be taken.
    /// </summary>
    public Func<HttpContext, bool> Predicate
    {
        get => _predicate;
        init => _predicate = value ?? throw new ArgumentNullException(nameof(value));
    }

    /// <summary>
    /// The branch taken for a positive match.
    /// </summary>
    public RequestDelegate Branch { get; init; }

    /// <summary>
    /// The path to match.
    /// </summary>
    public PathString PathMatch { get; init; }
}
