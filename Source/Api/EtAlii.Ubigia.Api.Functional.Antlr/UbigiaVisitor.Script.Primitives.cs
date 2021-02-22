// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

// This file is shared by both the traversal and context projects.
// We use CultureInfo.InvariantCulture to ensure the . is always used as separator.

namespace EtAlii.Ubigia.Api.Functional.Antlr
{
    using System;
    using System.Globalization;
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public partial class UbigiaVisitor
    {
        public override object VisitString_quoted(UbigiaParser.String_quotedContext context)
        {
            var text = context?.STRING_QUOTED()?.GetText();
            return text?.Substring(1, text.Length - 2);
        }

        public override object VisitString_quoted_non_empty(UbigiaParser.String_quoted_non_emptyContext context)
        {
            var text = context?.STRING_QUOTED_NON_EMPTY()?.GetText();
            return text?.Substring(1, text.Length - 2);
        }

        public override object VisitIdentifier(UbigiaParser.IdentifierContext context) => context?.GetText();

        public override object VisitBoolean_literal(UbigiaParser.Boolean_literalContext context) => bool.Parse(context.GetText());

        public override object VisitFloat_literal(UbigiaParser.Float_literalContext context) => float.Parse(context.GetText(), CultureInfo.InvariantCulture);

        public override object VisitFloat_literal_unsigned(UbigiaParser.Float_literal_unsignedContext context) => float.Parse(context.GetText(), CultureInfo.InvariantCulture);

        public override object VisitInteger_literal(UbigiaParser.Integer_literalContext context) => int.Parse(context.GetText(), CultureInfo.InvariantCulture);

        public override object VisitInteger_literal_unsigned(UbigiaParser.Integer_literal_unsignedContext context) => int.Parse(context.GetText(), CultureInfo.InvariantCulture);

        public override object VisitTimespan(UbigiaParser.TimespanContext context)
        {
            var days = (int)VisitInteger_literal_unsigned(context.integer_literal_unsigned(0));
            var hours = (int)VisitInteger_literal_unsigned(context.integer_literal_unsigned(1));
            var minutes = (int)VisitInteger_literal_unsigned(context.integer_literal_unsigned(2));
            var seconds = (int)VisitInteger_literal_unsigned(context.integer_literal_unsigned(3));
            var milliseconds = (int)VisitInteger_literal_unsigned(context.integer_literal_unsigned(4));
            return new TimeSpan(days, hours, minutes, seconds, milliseconds);
        }

        public override object VisitDatetime_format_1(UbigiaParser.Datetime_format_1Context context)
        {
            try
            {
                var year = int.Parse(context.datetime_d4().GetText());
                var month = int.Parse(context.datetime_d2(0).GetText());
                var day = int.Parse(context.datetime_d2(1).GetText());
                var hour = int.Parse(context.datetime_d2(2).GetText());
                var minute = int.Parse(context.datetime_d2(3).GetText());
                var second = int.Parse(context.datetime_d2(4).GetText());
                var milliSecond = int.Parse(context.datetime_d3().GetText());

                return new DateTime(year, month, day, hour, minute, second, milliSecond);
            }
            catch (Exception e)
            {
                throw new ScriptParserException("Cannot parse DateTime: " + context.GetText(), e);
            }
        }

        public override object VisitDatetime_format_2(UbigiaParser.Datetime_format_2Context context)
        {
            try
            {
                var year = int.Parse(context.datetime_d4().GetText());
                var month = int.Parse(context.datetime_d2(0).GetText());
                var day = int.Parse(context.datetime_d2(1).GetText());
                var hour = int.Parse(context.datetime_d2(2).GetText());
                var minute = int.Parse(context.datetime_d2(3).GetText());
                var second = int.Parse(context.datetime_d2(4).GetText());

                return new DateTime(year, month, day, hour, minute, second);
            }
            catch (Exception e)
            {
                throw new ScriptParserException("Cannot parse DateTime: " + context.GetText(), e);
            }
        }

        public override object VisitDatetime_format_3(UbigiaParser.Datetime_format_3Context context)
        {
            try
            {
                var year = int.Parse(context.datetime_d4().GetText());
                var month = int.Parse(context.datetime_d2(0).GetText());
                var day = int.Parse(context.datetime_d2(1).GetText());
                var hour = int.Parse(context.datetime_d2(2).GetText());
                var minute = int.Parse(context.datetime_d2(3).GetText());

                return new DateTime(year, month, day, hour, minute, 0);
            }
            catch (Exception e)
            {
                throw new ScriptParserException("Cannot parse DateTime: " + context.GetText(), e);
            }
        }

        public override object VisitDatetime_format_4(UbigiaParser.Datetime_format_4Context context)
        {
            try
            {
                var year = int.Parse(context.datetime_d4().GetText());
                var month = int.Parse(context.datetime_d2(0).GetText());
                var day = int.Parse(context.datetime_d2(1).GetText());

                return new DateTime(year, month, day);
            }
            catch (Exception e)
            {
                throw new ScriptParserException("Cannot parse DateTime: " + context.GetText(), e);
            }
        }

        public override object VisitDatetime_format_5(UbigiaParser.Datetime_format_5Context context)
        {
            try
            {
                var day = int.Parse(context.datetime_d2(0).GetText());
                var month = int.Parse(context.datetime_d2(1).GetText());
                var year = int.Parse(context.datetime_d4().GetText());
                var hour = int.Parse(context.datetime_d2(2).GetText());
                var minute = int.Parse(context.datetime_d2(3).GetText());
                var second = int.Parse(context.datetime_d2(4).GetText());
                var milliSecond = int.Parse(context.datetime_d3().GetText());

                return new DateTime(year, month, day, hour, minute, second, milliSecond);
            }
            catch (Exception e)
            {
                throw new ScriptParserException("Cannot parse DateTime: " + context.GetText(), e);
            }
        }

        public override object VisitDatetime_format_6(UbigiaParser.Datetime_format_6Context context)
        {
            try
            {
                var day = int.Parse(context.datetime_d2(0).GetText());
                var month = int.Parse(context.datetime_d2(1).GetText());
                var year = int.Parse(context.datetime_d4().GetText());
                var hour = int.Parse(context.datetime_d2(2).GetText());
                var minute = int.Parse(context.datetime_d2(3).GetText());
                var second = int.Parse(context.datetime_d2(4).GetText());

                return new DateTime(year, month, day, hour, minute, second);
            }
            catch (Exception e)
            {
                throw new ScriptParserException("Cannot parse DateTime: " + context.GetText(), e);
            }
        }

        public override object VisitDatetime_format_7(UbigiaParser.Datetime_format_7Context context)
        {
            try
            {
                var day = int.Parse(context.datetime_d2(0).GetText());
                var month = int.Parse(context.datetime_d2(1).GetText());
                var year = int.Parse(context.datetime_d4().GetText());
                var hour = int.Parse(context.datetime_d2(2).GetText());
                var minute = int.Parse(context.datetime_d2(3).GetText());

                return new DateTime(year, month, day, hour, minute, 0);
            }
            catch (Exception e)
            {
                throw new ScriptParserException("Cannot parse DateTime: " + context.GetText(), e);
            }
        }

        public override object VisitDatetime_format_8(UbigiaParser.Datetime_format_8Context context)
        {
            try
            {
                var day = int.Parse(context.datetime_d2(0).GetText());
                var month = int.Parse(context.datetime_d2(1).GetText());
                var year = int.Parse(context.datetime_d4().GetText());

                return new DateTime(year, month, day);
            }
            catch (Exception e)
            {
                throw new ScriptParserException("Cannot parse DateTime: " + context.GetText(), e);
            }
        }
    }
}
