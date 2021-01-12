// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Collections.Generic;
    using EtAlii.Ubigia.Api.Functional.Traversal.Antlr;

    public partial class TraversalVisitor
    {
        public override object VisitObject(TraversalScriptParser.ObjectContext context)
        {
            var properties = new PropertyDictionary();

            var pairs = context.object_kv_pair_with_comma();
            foreach (var pair in pairs)
            {
                var (key, value) = (KeyValuePair<string, object>)VisitObject_kv_pair_with_comma(pair);
                properties.Add(key, value);
            }

            var kvpContext = context.object_kv_pair_without_comma();
            if (kvpContext != null)
            {
                var (lastKey, lastValue) = (KeyValuePair<string, object>)VisitObject_kv_pair_without_comma(kvpContext);
                properties.Add(lastKey, lastValue);
            }

            return new ObjectConstantSubject(properties);
        }

        public override object VisitObject_kv_pair_with_comma(TraversalScriptParser.Object_kv_pair_with_commaContext context)
        {
            var key = (string)VisitObject_kv_key(context.object_kv_key());
            object value = null;
            var valueContext = context.object_kv_value();
            if (valueContext != null)
            {
                value = VisitObject_kv_value(context.object_kv_value());
            }
            return new KeyValuePair<string, object>(key, value);
        }

        public override object VisitObject_kv_pair_without_comma(TraversalScriptParser.Object_kv_pair_without_commaContext context)
        {
            var key = (string)VisitObject_kv_key(context.object_kv_key());
            object value = null;
            var valueContext = context.object_kv_value();
            if (valueContext != null)
            {
                value = VisitObject_kv_value(context.object_kv_value());
            }
            return new KeyValuePair<string, object>(key, value);
        }
    }
}
