namespace EtAlii.Ubigia.Api
{
    using System;
    using System.Collections.Generic;

    public interface IPropertyDictionary : IDictionary<string, object>, IEquatable<IPropertyDictionary>, IComparable<IPropertyDictionary>
    {
        bool Stored { get; }
    }
}