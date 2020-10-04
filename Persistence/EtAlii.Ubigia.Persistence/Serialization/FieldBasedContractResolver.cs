﻿namespace EtAlii.Ubigia.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Newtonsoft.Json.Serialization;

    public class FieldBasedContractResolver : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, Newtonsoft.Json.MemberSerialization memberSerialization)
        {
            var fields = new List<JsonProperty>();
            do
            {
                var currentFields = type.GetRuntimeFields()
                    .Where(field => !field.IsStatic)
                    .Select(field => base.CreateProperty(field, memberSerialization));
                fields.AddRange(currentFields);
                type = type.GetTypeInfo().BaseType;
            } while (type != null);

            foreach (var field in fields)
            {
                field.PropertyName = field.PropertyName.TrimStart('_');
                field.Writable = true;
                field.Readable = true;
            }
            return fields;
        }
    }
}
