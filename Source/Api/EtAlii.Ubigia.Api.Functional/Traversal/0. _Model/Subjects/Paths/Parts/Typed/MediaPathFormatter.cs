// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

public static class MediaPathFormatter
{
    public static readonly TypedPathFormatter CompanyNameFormatter = new("MEDIA_COMPANY", @"^[a-zA-Z0-9\-_]+$");
    public static readonly TypedPathFormatter ProductFamilyNameFormatter = new("MEDIA_FAMILY", @"^[a-zA-Z0-9\-_]+$");
    public static readonly TypedPathFormatter ProductModelNameFormatter = new("MEDIA_MODEL", @"^[a-zA-Z0-9\-_]+$");
    public static readonly TypedPathFormatter ProductNumberFormatter = new("MEDIA_NUMBER", @"^[0123456789]+$");
}
