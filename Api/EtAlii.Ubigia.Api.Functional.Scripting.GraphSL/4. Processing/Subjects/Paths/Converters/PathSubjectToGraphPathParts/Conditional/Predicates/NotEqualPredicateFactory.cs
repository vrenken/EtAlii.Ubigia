namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System;

    internal class NotEqualPredicateFactory : INotEqualPredicateFactory
    {
        public Predicate<PropertyDictionary> Create(Condition condition)
        {
            return p =>
            {
                var result = true;
                if (p.TryGetValue(condition.Property, out var propertyValue))
                {
                    if (propertyValue != null)
                    {
                        result = !propertyValue.Equals(condition.Value);
                    }
                    else if (condition.Value != null)
                    {
                        result = !condition.Value.Equals(propertyValue);
                    }
                    else if (propertyValue == null && condition.Value == null)
                    {
                        result = false;
                    }
                }
                return result;
            };
        }
    }
}