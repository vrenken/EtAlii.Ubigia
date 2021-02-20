// // Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia
//
// namespace EtAlii.Ubigia.Api.Functional.Context
// {
//     using System;
//     using System.Globalization;
//     using EtAlii.Ubigia.Api.Functional.Context.Antlr;
//     using EtAlii.Ubigia.Api.Functional.Traversal;
//
//     // The method in this class should be kept in sync with the TraversalVisitor.Primitives.cs.
//     // Just copy/paste them and rename their context instances accordingly.
//     // There is no easy way to keep only one instance as polymorphism isn't supported in C# using classes.
//     public partial class ContextVisitor
//     {
//        public override object VisitString_quoted(ContextSchemaParser.String_quotedContext context)
//         {
//             var text = context?.STRING_QUOTED()?.GetText();
//             return text?.Substring(1, text.Length - 2);
//         }
//
//         public override object VisitString_quoted_non_empty(ContextSchemaParser.String_quoted_non_emptyContext context)
//         {
//             var text = context?.STRING_QUOTED_NON_EMPTY()?.GetText();
//             return text?.Substring(1, text.Length - 2);
//         }
//
//         public override object VisitIdentifier(ContextSchemaParser.IdentifierContext context) => context?.GetText();
//
//         public override object VisitBoolean_literal(ContextSchemaParser.Boolean_literalContext context) => bool.Parse(context.GetText());
//
//         public override object VisitFloat_literal(ContextSchemaParser.Float_literalContext context) => float.Parse(context.GetText(), CultureInfo.InvariantCulture);
//
//         public override object VisitFloat_literal_unsigned(ContextSchemaParser.Float_literal_unsignedContext context) => float.Parse(context.GetText(), CultureInfo.InvariantCulture);
//
//         public override object VisitInteger_literal(ContextSchemaParser.Integer_literalContext context) => int.Parse(context.GetText(), CultureInfo.InvariantCulture);
//
//         public override object VisitInteger_literal_unsigned(ContextSchemaParser.Integer_literal_unsignedContext context) => int.Parse(context.GetText(), CultureInfo.InvariantCulture);
//
//         public override object VisitTimespan(ContextSchemaParser.TimespanContext context)
//         {
//             var days = (int)VisitInteger_literal_unsigned(context.integer_literal_unsigned(0));
//             var hours = (int)VisitInteger_literal_unsigned(context.integer_literal_unsigned(1));
//             var minutes = (int)VisitInteger_literal_unsigned(context.integer_literal_unsigned(2));
//             var seconds = (int)VisitInteger_literal_unsigned(context.integer_literal_unsigned(3));
//             var milliseconds = (int)VisitInteger_literal_unsigned(context.integer_literal_unsigned(4));
//             return new TimeSpan(days, hours, minutes, seconds, milliseconds);
//         }
//
//         public override object VisitDatetime_format_1(ContextSchemaParser.Datetime_format_1Context context)
//         {
//             try
//             {
//                 var year = context.datetime_date_yyyy().GetText();
//                 var month = context.datetime_date_mm().GetText();
//                 var day = context.datetime_date_dd().GetText();
//                 var hour = context.datetime_time_hh().GetText();
//                 var minute = context.datetime_time_mm().GetText();
//                 var second = context.datetime_time_ss().GetText();
//                 var milliSecond = context.datetime_ms().GetText();
//
//                 return DateTime.ParseExact($"{year}-{month}-{day}T{hour}:{minute}:{second}.{milliSecond}",
//                     "yyyy-MM-ddTHH:mm:ss.fff", CultureInfo.InvariantCulture);
//             }
//             catch (Exception e)
//             {
//                 throw new ScriptParserException("Cannot parse DateTime: " + context.GetText(), e);
//             }
//         }
//
//         public override object VisitDatetime_format_2(ContextSchemaParser.Datetime_format_2Context context)
//         {
//             try
//             {
//                 var year = context.datetime_date_yyyy().GetText();
//                 var month = context.datetime_date_mm().GetText();
//                 var day = context.datetime_date_dd().GetText();
//                 var hour = context.datetime_time_hh().GetText();
//                 var minute = context.datetime_time_mm().GetText();
//                 var second = context.datetime_time_ss().GetText();
//
//                 return DateTime.ParseExact($"{year}-{month}-{day}T{hour}:{minute}:{second}", "yyyy-MM-ddTHH:mm:ss",
//                     CultureInfo.InvariantCulture);
//             }
//             catch (Exception e)
//             {
//                 throw new ScriptParserException("Cannot parse DateTime: " + context.GetText(), e);
//             }
//         }
//
//         public override object VisitDatetime_format_3(ContextSchemaParser.Datetime_format_3Context context)
//         {
//             try
//             {
//                 var year = context.datetime_date_yyyy().GetText();
//                 var month = context.datetime_date_mm().GetText();
//                 var day = context.datetime_date_dd().GetText();
//                 var hour = context.datetime_time_hh().GetText();
//                 var minute = context.datetime_time_mm().GetText();
//
//                 return DateTime.ParseExact($"{year}-{month}-{day}T{hour}:{minute}", "yyyy-MM-ddTHH:mm",
//                     CultureInfo.InvariantCulture);
//             }
//             catch (Exception e)
//             {
//                 throw new ScriptParserException("Cannot parse DateTime: " + context.GetText(), e);
//             }
//         }
//
//         public override object VisitDatetime_format_4(ContextSchemaParser.Datetime_format_4Context context)
//         {
//             try
//             {
//                 var year = context.datetime_date_yyyy().GetText();
//                 var month = context.datetime_date_mm().GetText();
//                 var day = context.datetime_date_dd().GetText();
//
//                 return DateTime.ParseExact($"{year}-{month}-{day}", "yyyy-MM-dd", CultureInfo.InvariantCulture);
//             }
//             catch (Exception e)
//             {
//                 throw new ScriptParserException("Cannot parse DateTime: " + context.GetText(), e);
//             }
//         }
//
//         public override object VisitDatetime_format_5(ContextSchemaParser.Datetime_format_5Context context)
//         {
//             try
//             {
//                 var year = context.datetime_date_yyyy().GetText();
//                 var month = context.datetime_date_mm().GetText();
//                 var day = context.datetime_date_dd().GetText();
//                 var hour = context.datetime_time_hh().GetText();
//                 var minute = context.datetime_time_mm().GetText();
//                 var second = context.datetime_time_ss().GetText();
//                 var milliSecond = context.datetime_ms().GetText();
//
//                 return DateTime.ParseExact($"{year}-{month}-{day}T{hour}:{minute}:{second}.{milliSecond}",
//                     "yyyy-MM-ddTHH:mm:ss.fff", CultureInfo.InvariantCulture);
//             }
//             catch (Exception e)
//             {
//                 throw new ScriptParserException("Cannot parse DateTime: " + context.GetText(), e);
//             }
//         }
//
//         public override object VisitDatetime_format_6(ContextSchemaParser.Datetime_format_6Context context)
//         {
//             try
//             {
//                 var year = context.datetime_date_yyyy().GetText();
//                 var month = context.datetime_date_mm().GetText();
//                 var day = context.datetime_date_dd().GetText();
//                 var hour = context.datetime_time_hh().GetText();
//                 var minute = context.datetime_time_mm().GetText();
//                 var second = context.datetime_time_ss().GetText();
//
//                 return DateTime.ParseExact($"{year}-{month}-{day}T{hour}:{minute}:{second}", "yyyy-MM-ddTHH:mm:ss",
//                     CultureInfo.InvariantCulture);
//             }
//             catch (Exception e)
//             {
//                 throw new ScriptParserException("Cannot parse DateTime: " + context.GetText(), e);
//             }
//         }
//
//         public override object VisitDatetime_format_7(ContextSchemaParser.Datetime_format_7Context context)
//         {
//             try
//             {
//                 var year = context.datetime_date_yyyy().GetText();
//                 var month = context.datetime_date_mm().GetText();
//                 var day = context.datetime_date_dd().GetText();
//                 var hour = context.datetime_time_hh().GetText();
//                 var minute = context.datetime_time_mm().GetText();
//
//                 return DateTime.ParseExact($"{year}-{month}-{day}T{hour}:{minute}", "yyyy-MM-ddTHH:mm",
//                     CultureInfo.InvariantCulture);
//             }
//             catch (Exception e)
//             {
//                 throw new ScriptParserException("Cannot parse DateTime: " + context.GetText(), e);
//             }
//         }
//
//         public override object VisitDatetime_format_8(ContextSchemaParser.Datetime_format_8Context context)
//         {
//             try
//             {
//                 var year = context.datetime_date_yyyy().GetText();
//                 var month = context.datetime_date_mm().GetText();
//                 var day = context.datetime_date_dd().GetText();
//
//                 return DateTime.ParseExact($"{year}-{month}-{day}", "yyyy-MM-dd", CultureInfo.InvariantCulture);
//             }
//             catch (Exception e)
//             {
//                 throw new ScriptParserException("Cannot parse DateTime: " + context.GetText(), e);
//             }
//         }
//     }
// }
