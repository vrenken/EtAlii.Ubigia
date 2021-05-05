namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;

    internal class EqualPredicateFactory : IEqualPredicateFactory
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
                        result = propertyValue.Equals(condition.Value);
                    }
                    else if (condition.Value != null)
                    {
                        result = condition.Value.Equals(null);
                    }
                    else if (condition.Value == null)
                    {
                        result = true;
                    }
                }
                return result;
            };
        }
    }
}
