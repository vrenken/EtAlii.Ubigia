﻿namespace EtAlii.Ubigia.Serialization
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
            if (type.GetTypeInfo().Assembly.FullName.StartsWith("Microsoft.AspNet.SignalR"))
            {
                // We should not touch how SignalR internal classes are serialized.
                return base.CreateProperties(type, memberSerialization);
            }

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
                field.PropertyName = field.PropertyName!.TrimStart('_');
                field.Writable = true;
                field.Readable = true;
            }
            //fields.ForEach(field => [ field.Writable = true; p.Readable = true; ])
            return fields;

            //var props = type
            //    .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            //    .Select(p => base.CreateProperty(p, memberSerialization))
            //    .Union(type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            //        .Select(f => base.CreateProperty(f, memberSerialization)))
            //    .ToList()
            //props.ForEach(p => [ p.Writable = true; p.Readable = true; ])
            //return props
        }
    }
}