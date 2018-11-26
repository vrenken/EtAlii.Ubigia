namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System;
    using global::GraphQL.Resolvers;
    using global::GraphQL.Types;
    using System.Reflection;
    using System.Reflection.Emit;

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
        
        public static DynamicObjectGraphType Create(string path, string name, PropertyDictionary properties)
        {
            var fieldTypeInstanceType = BuildInstanceType();
            
            var instance = (DynamicObjectGraphType)Activator.CreateInstance(fieldTypeInstanceType);

            instance.Name = name;
            instance.Description = $"Dynamic {instance.Name} type created for the Ubigia path: {path}";
            
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

            return instance;
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
                case string _: return new FuncFieldResolver<object, string>(context => value as string);
                case int _: return new FuncFieldResolver<object, int>(context => (int) (int?) value);
                case float _: return new FuncFieldResolver<object, float>(context => (float) (float?) value);
                case decimal _: return new FuncFieldResolver<object, decimal>(context => (decimal) (decimal?) value);
                case DateTime _: return new FuncFieldResolver<object, DateTime>(context => (DateTime) (DateTime?) value);
                case TimeSpan _: return new FuncFieldResolver<object, TimeSpan>(context => (TimeSpan) (TimeSpan?) value);
                default: throw new NotSupportedException();
            }
        }
    }
}