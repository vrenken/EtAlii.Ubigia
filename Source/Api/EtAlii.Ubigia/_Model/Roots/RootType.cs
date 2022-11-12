// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia;

/// <summary>
/// Examples:
/// root:text &lt;= EtAlii.Api.Roots.Text
/// root:location &lt;= EtAlii.Api.Roots.Location
/// root:orders &lt;= EtAlii.Api.Roots.Text
/// root:email &lt;= EtAlii.Api.Roots.Text
/// </summary>
/// <param name="Value"></param>
public readonly record struct RootType(string Value)
{
    // RT2022: We cannot change the root type yet.
    // This one should not be needed.
    public static readonly RootType None = new(null);

    public static readonly RootType Time = new("EtAlii.Api.Roots.Time");
    public static readonly RootType Text = new("EtAlii.Api.Roots.Text");
    public static readonly RootType Location = new("EtAlii.Api.Roots.Location");
    public static readonly RootType Tail = new("EtAlii.Api.Roots.Tail");
    public static readonly RootType Head = new("EtAlii.Api.Roots.Head");
}
