namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Collections.Generic;

    internal class LessThanPredicateFactory : ILessThanPredicateFactory
    {
        public Predicate<PropertyDictionary> Create(Condition condition)
        {
            return (Predicate<PropertyDictionary>)(p =>
            {
                var result = false;
                object propertyValue = null;
                if (p.TryGetValue(condition.Property, out propertyValue))
                {
                    if (propertyValue != null)
                    {
                        result = Comparer<object>.Default.Compare(propertyValue, condition.Value) < 0;
                    }
                    else if (condition.Value != null)
                    {
                        result = Comparer<object>.Default.Compare(condition.Value, propertyValue) < 0;
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