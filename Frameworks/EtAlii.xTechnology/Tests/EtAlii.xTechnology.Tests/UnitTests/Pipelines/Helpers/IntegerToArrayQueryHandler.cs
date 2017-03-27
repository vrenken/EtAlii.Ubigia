namespace EtAlii.xTechnology.UnitTests
{
    using EtAlii.xTechnology.Structure;

    internal class IntegerToArrayQueryHandler : IQueryHandler<IntegerToArrayQuery, int, int[]>
    {
        public IntegerToArrayQuery Create(int parameter)
        {
            return new IntegerToArrayQuery(parameter);
        }

        public int[] Handle(IntegerToArrayQuery query)
        {
            var result = new int[query.Integer];
            for (int i = 0; i < query.Integer; i++)
            {
                result[i] = i;
            }
            return result;
        }
    }
}