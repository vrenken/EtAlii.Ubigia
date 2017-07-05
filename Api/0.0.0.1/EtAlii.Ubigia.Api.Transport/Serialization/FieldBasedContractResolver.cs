namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Newtonsoft.Json;
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
            //fields.ForEach(field => { field.Writable = true; p.Readable = true; });
            return fields;

            //var props = type
            //    .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            //    .Select(p => base.CreateProperty(p, memberSerialization))
            //    .Union(type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            //        .Select(f => base.CreateProperty(f, memberSerialization)))
            //    .ToList();
            //props.ForEach(p => { p.Writable = true; p.Readable = true; });
            //return props;
        }
    }
}
