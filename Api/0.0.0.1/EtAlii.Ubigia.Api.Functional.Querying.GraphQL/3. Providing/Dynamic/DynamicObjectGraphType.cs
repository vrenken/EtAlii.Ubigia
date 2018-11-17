namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System;
    using global::GraphQL.Resolvers;
    using global::GraphQL.Types;

    public class DynamicObjectGraphType : ObjectGraphType<object>
    {
        public static void Setup(DynamicObjectGraphType instance, string name, string path, PropertyDictionary properties)
        {
            instance.Name = name;
            instance.Description = $"Dynamic {name} type created for the Ubigia path: {path}";

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

        private static Type GetType(object value)
        {
            switch (value)
            {
                case string _: return typeof(StringGraphType);
                case int _: return typeof(IntGraphType);
                case float _: return typeof(FloatGraphType);
                case decimal _: return typeof(DecimalGraphType);
                case DateTime dateTime: return GetDateTimeType(dateTime);
                case TimeSpan _: return typeof(TimeSpanMillisecondsGraphType);
                default: throw new NotSupportedException();
            }
        }

        private static Type GetDateTimeType(DateTime value)
        {
            return value.TimeOfDay == TimeSpan.Zero
                ? typeof(DateGraphType)
                : typeof(DateTimeGraphType);
        }
        
        private static IFieldResolver GetResolver(object value)
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