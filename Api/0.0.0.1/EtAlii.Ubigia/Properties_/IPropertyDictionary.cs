namespace EtAlii.Ubigia
{
    using System;
    using System.Collections.Generic;

    public interface IPropertyDictionary : IDictionary<string, object>, IEquatable<IPropertyDictionary>, IComparable<IPropertyDictionary>
    {
        bool Stored { get; }
    }
}