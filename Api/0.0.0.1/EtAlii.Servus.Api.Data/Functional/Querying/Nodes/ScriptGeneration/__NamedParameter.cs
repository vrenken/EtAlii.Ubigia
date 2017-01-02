namespace EtAlii.Servus.Api.Data
{
    public class __NamedParameter
    {
        public __NamedParameter(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public object Value { get; set; }
    }
}