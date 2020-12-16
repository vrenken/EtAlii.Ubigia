namespace EtAlii.xTechnology.Structure.Tests
{
    using EtAlii.xTechnology.Structure;

    public class StringToIntQueryHandler : IQueryHandler<StringToIntQuery, string, int>
    {
        public StringToIntQuery Create(string parameter)
        {
            return new(parameter);
        }

        public int Handle(StringToIntQuery query)
        {
            return query.String.Length;
        }
    }
}
