namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using global::GraphQL.Resolvers;
    using global::GraphQL.Types;
    using System.Reflection;
    using System.Reflection.Emit;
    using EtAlii.Ubigia.Api.Logical;

    public class DynamicObjectGraphType : ObjectGraphType<object>
    {
        private static TypeInfo BuildInstanceType()
        {
            var assemblyName = new AssemblyName($"DynamicAssembly_{Guid.NewGuid():N}");
            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            var moduleBuilder = assemblyBuilder.DefineDynamicModule("DynamicModule");
            var typeName = $"{typeof(DynamicObjectGraphType).Name}_{Guid.NewGuid():N}";
            var typeBuilder = moduleBuilder.DefineType(typeName, TypeAttributes.Public, typeof(DynamicObjectGraphType));
            return typeBuilder.CreateTypeInfo();
        }
        
 
        public static DynamicObjectGraphType[] Create(string path, string name, PropertyDictionary[] propertiesCollection)
        {
            return propertiesCollection
                .Select(properties => Create(path, name, properties))
                .ToArray();
        }

        public static DynamicObjectGraphType Create(string path, string name, PropertyDictionary properties)
        {
            var fieldTypeInstanceType = BuildInstanceType();
            
            var result = (DynamicObjectGraphType)Activator.CreateInstance(fieldTypeInstanceType);

            result.Name = name;
            result.Description = $"Dynamic {name} type created for the Ubigia path: {path}";
            AddFields(result, properties);
            
            return result;
        }

        public static ObjectGraphType CreateShallow(string path, string name, PropertyDictionary properties)
        {
            var result = new ObjectGraphType
            {
                Name = name, 
                Description = $"Shallow dynamic {name} type created for the Ubigia path: {path}"
            };

            foreach (var kvp in properties)
            {
                result.Field(kvp.Key, GetScalarGraphType(kvp.Value));
            }

            return result;
        }

        public static void AddFields(ComplexGraphType<object> instance, PropertyDictionary properties)
        {
            foreach (var kvp in properties)
            {
                var propertyName = kvp.Key.ToLower();
                var value = kvp.Value;
                var fieldType = new FieldType()
                {
                    Name = propertyName,
                    Description = $"Dynamic field {propertyName}",
                    DeprecationReason = null,
                    Type = GetType(value),
                    Arguments = null,
                    Resolver = GetResolver(value)
                };
                    
                instance.AddField(fieldType);
            }
        }

        

        public static Type GetType(object value)
        {
            switch (value)
            {
                case string _: return typeof(StringGraphType);
                case int _: return typeof(IntGraphType);
                case float _: return typeof(FloatGraphType);
                case decimal _: return typeof(DecimalGraphType);
                case DateTime dateTime: return dateTime.TimeOfDay == TimeSpan.Zero
                    ? typeof(DateGraphType)
                    : typeof(DateTimeGraphType);
                case TimeSpan _: return typeof(TimeSpanMillisecondsGraphType);
                default: throw new NotSupportedException();
            }
        }
        
        public static ScalarGraphType GetScalarGraphType(object value)
        {
            switch (value)
            {
                case string _: return new StringGraphType();
                case int _: return new IntGraphType();
                case float _: return new FloatGraphType();
                case decimal _: return new DecimalGraphType();
                case DateTime dateTime: return dateTime.TimeOfDay == TimeSpan.Zero
                        ? (ScalarGraphType)new DateGraphType()
                        : new DateTimeGraphType();
                case TimeSpan _: return new TimeSpanMillisecondsGraphType();
                default: throw new NotSupportedException();
            }
        }

        public static IFieldResolver GetResolver(object value)
        {
            switch (value)
            {
                case string v: return new FuncFieldResolver<object, string>(context => v);
                case int v: return new FuncFieldResolver<object, int>(context => v);
                case float v: return new FuncFieldResolver<object, float>(context => v);
                case decimal v: return new FuncFieldResolver<object, decimal>(context => v);
                case DateTime v: return new FuncFieldResolver<object, DateTime>(context => v);
                case TimeSpan v: return new FuncFieldResolver<object, TimeSpan>(context => v);
                case DynamicNode v: return new FuncFieldResolver<object, DynamicNode>(context => v);
                default: throw new NotSupportedException();
            }
        }
    }
}