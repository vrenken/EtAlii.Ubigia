namespace EtAlii.Ubigia.Api.Functional
{
    public class ValueMutation : Fragment
    {
        public object Value { get; }
    
        public ValueMutation(string name, Annotation annotation, object value)
            : base(name, annotation)
        {
            Value = value;
        }
    }
}