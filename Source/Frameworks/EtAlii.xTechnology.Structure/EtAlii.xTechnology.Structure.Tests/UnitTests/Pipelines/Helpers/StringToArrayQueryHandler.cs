namespace EtAlii.xTechnology.Structure.Tests
{
    using EtAlii.xTechnology.Structure;

    internal class StringToArrayQueryHandler : IQueryHandler<StringToArrayQuery, string, string[]>
    {
        public StringToArrayQuery Create(string parameter)
        {
            return new StringToArrayQuery(parameter);
        }

        public string[] Handle(StringToArrayQuery query)
        {
            var length = query.String.Length;
            var result = new string[length];
            for (var i = 0; i < length; i++)
            {
                result[i] = query.String.Substring(0, i);
            }
            return result;
        }
    }
}