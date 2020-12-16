namespace EtAlii.xTechnology.Structure.Tests
{
    using System.Linq;
    using EtAlii.xTechnology.Structure;

    public class IntegerArrayToIntQueryHandler : IQueryHandler<IntegerArrayToIntQuery, int[], int>
    {
        public IntegerArrayToIntQuery Create(int[] parameter)
        {
            return new(parameter);
        }

        public int Handle(IntegerArrayToIntQuery query)
        {
            return query.Array.Sum();
        }
    }
}
