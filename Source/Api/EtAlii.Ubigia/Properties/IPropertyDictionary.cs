// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia;

using System;
using System.Collections.Generic;

public interface IPropertyDictionary : IDictionary<string, object>, IEquatable<IPropertyDictionary>
{
    bool Stored { get; }
}
