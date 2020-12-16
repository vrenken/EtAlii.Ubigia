namespace EtAlii.xTechnology.Structure.Tests
{
    using System.Linq;
    using EtAlii.xTechnology.Structure;

    public class StringArrayToIntQueryHandler : IQueryHandler<StringArrayToIntQuery, string[], int>
    {
        public StringArrayToIntQuery Create(string[] parameter)
        {
            return new(parameter);
        }

        public int Handle(StringArrayToIntQuery query)
        {
            return query.Array.Select(s => s.Length).Sum();
        }
    }
}
