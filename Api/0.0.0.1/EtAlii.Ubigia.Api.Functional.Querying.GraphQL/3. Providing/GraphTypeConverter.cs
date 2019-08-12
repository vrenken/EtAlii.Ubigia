namespace EtAlii.Ubigia.Api.Functional.Querying.GraphQL
{
    using System;
    using global::GraphQL.Types;

    public static class GraphTypeConverter
    {
        

        public static System.Type ToGraphType(object value)
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
                default: throw new NotSupportedException($"Unable to convert object to GraphType: {value?.ToString() ?? "NULL"}");
            }
        }
        
        public static ScalarGraphType ToScalarGraphType(object value)
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
                default: throw new NotSupportedException($"Unable to convert object to scalar GraphType: {value?.ToString() ?? "NULL"}");
            }
        }
   }
}