namespace EtAlii.xTechnology.Structure.Tests
{
    using System.Linq;
    using EtAlii.xTechnology.Structure;

    public class IntegerArrayToStringQueryHandler : IQueryHandler<IntegerArrayToStringQuery, int[], string>
    {
        public IntegerArrayToStringQuery Create(int[] parameter)
        {
            return new(parameter);
        }

        public string Handle(IntegerArrayToStringQuery query)
        {
            return string.Join(", ", query.Array.Select(i => i.ToString()));
        }
    }
}
