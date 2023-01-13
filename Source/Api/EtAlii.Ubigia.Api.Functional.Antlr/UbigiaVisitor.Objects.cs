// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Antlr;

using System.Collections.Generic;
using EtAlii.Ubigia.Api.Functional.Traversal;

public partial class UbigiaVisitor
{
    public override object VisitObject_value(UbigiaParser.Object_valueContext context)
    {
        var properties = new PropertyDictionary();

        var pairs = context.object_kv_pair_with_comma();
        foreach (var pair in pairs)
        {
            var kvp = (KeyValuePair<string, object>)VisitObject_kv_pair_with_comma(pair);
            properties.Add(kvp.Key, kvp.Value);
        }

        var kvpContext = context.object_kv_pair_without_comma();
        if (kvpContext != null)
        {
            var lastKvp = (KeyValuePair<string, object>)VisitObject_kv_pair_without_comma(kvpContext);
            properties.Add(lastKvp.Key, lastKvp.Value);
        }

        return new ObjectConstantSubject(properties);
    }

    public override object VisitObject_kv_pair_with_comma(UbigiaParser.Object_kv_pair_with_commaContext context)
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

    public override object VisitObject_kv_pair_without_comma(UbigiaParser.Object_kv_pair_without_commaContext context)
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
