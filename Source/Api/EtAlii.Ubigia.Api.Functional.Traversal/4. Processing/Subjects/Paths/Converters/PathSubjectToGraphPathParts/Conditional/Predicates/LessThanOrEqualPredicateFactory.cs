namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Collections.Generic;

    internal class LessThanOrEqualPredicateFactory : ILessThanOrEqualPredicateFactory
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
                        result = Comparer<object>.Default.Compare(propertyValue, condition.Value) <= 0;
                    }
                    else if (condition.Value != null)
                    {
                        result = Comparer<object>.Default.Compare(condition.Value, null) <= 0;
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
