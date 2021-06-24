// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Collections.Generic;

    internal class LessThanPredicateFactory : ILessThanPredicateFactory
    {
        public Predicate<PropertyDictionary> Create(Condition condition)
        {
            return p =>
            {
                var result = false;
                if (p.TryGetValue(condition.Property, out var propertyValue))
                {
                    if (propertyValue != null)
                    {
                        result = Comparer<object>.Default.Compare(propertyValue, condition.Value) < 0;
                    }
                    else if (condition.Value != null)
                    {
                        result = Comparer<object>.Default.Compare(condition.Value, null) < 0;
                    }
                    else if (condition.Value == null)
                    {
                        result = false;
                    }
                }
                return result;
            };
        }
    }
}
