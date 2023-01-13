// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Serialization;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Serialization;

public sealed class FieldBasedContractResolver : DefaultContractResolver
{
    protected override IList<JsonProperty> CreateProperties(Type type, Newtonsoft.Json.MemberSerialization memberSerialization)
    {
        var fields = new List<JsonProperty>();
        do
        {
            var currentFields = type.GetRuntimeFields()
                .Where(field => !field.IsStatic)
                .Select(field => CreateProperty(field, memberSerialization));
            fields.AddRange(currentFields);
            type = type.GetTypeInfo().BaseType;
        } while (type != null);

        foreach (var field in fields)
        {
            field.PropertyName = field.PropertyName!.TrimStart('_');
            field.Writable = true;
            field.Readable = true;
        }
        return fields;
    }
}
