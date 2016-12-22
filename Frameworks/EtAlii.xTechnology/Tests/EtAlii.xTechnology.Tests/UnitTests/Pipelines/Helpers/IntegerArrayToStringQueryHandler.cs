namespace EtAlii.xTechnology.Tests
{
    using System;
    using System.Linq;
    using EtAlii.xTechnology.Structure;

    public class IntegerArrayToStringQueryHandler : IQueryHandler<IntegerArrayToStringQuery, int[], string>
    {
        public IntegerArrayToStringQuery Create(int[] parameter)
        {
            return new IntegerArrayToStringQuery(parameter);
        }

        public string Handle(IntegerArrayToStringQuery query)
        {
            return String.Join(", ", query.Array.Select(i => i.ToString()));
        }
    }
}