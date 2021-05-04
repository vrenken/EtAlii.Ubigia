namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using System;
    using GraphQL.Types;

    public static class GraphTypeConverter
    {
        public static System.Type ToGraphType(object value)
        {
            return value switch
            {
                string _ => typeof(StringGraphType),
                int _ => typeof(IntGraphType),
                float _ => typeof(FloatGraphType),
                decimal _ => typeof(DecimalGraphType),
                DateTime dateTime => dateTime.TimeOfDay == TimeSpan.Zero
                    ? typeof(DateGraphType)
                    : typeof(DateTimeGraphType),
                TimeSpan _ => typeof(TimeSpanMillisecondsGraphType),
                _ => throw new NotSupportedException($"Unable to convert object to GraphType: {value?.ToString() ?? "NULL"}")
            };
        }

        public static ScalarGraphType ToScalarGraphType(object value)
        {
            return value switch
            {
                string _ => new StringGraphType(),
                int _ => new IntGraphType(),
                float _ => new FloatGraphType(),
                decimal _ => new DecimalGraphType(),
                DateTime dateTime => dateTime.TimeOfDay == TimeSpan.Zero
                    ? new DateGraphType()
                    : new DateTimeGraphType(),
                TimeSpan _ => new TimeSpanMillisecondsGraphType(),
                _ => throw new NotSupportedException($"Unable to convert object to scalar GraphType: {value?.ToString() ?? "NULL"}")
            };
        }
   }
}
