namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    internal class NotEqualPredicateFactory : INotEqualPredicateFactory
    {
        public Predicate<PropertyDictionary> Create(Condition condition)
        {
            return (Predicate<PropertyDictionary>)(p =>
            {
                var result = true;
                object propertyValue = null;
                if (p.TryGetValue(condition.Property, out propertyValue))
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
            });
        }
    }
}