namespace EtAlii.xTechnology.Tests
{
    using System;
    using EtAlii.xTechnology.Structure;

    public class IntegersToStringQueryHandler : IQueryHandler<IntegersToStringQuery, int, int, string>
    {
        public IntegersToStringQuery Create(int parameter1, int parameter2)
        {
            return new IntegersToStringQuery(parameter1, parameter2);
        }

        public string Handle(IntegersToStringQuery query)
        {
            return $"{query.Integer1}-{query.Integer2}";
        }
    }
}