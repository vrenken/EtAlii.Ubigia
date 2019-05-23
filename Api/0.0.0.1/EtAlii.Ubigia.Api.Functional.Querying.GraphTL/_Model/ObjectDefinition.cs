namespace EtAlii.Ubigia.Api.Functional
{
    public class ObjectDefinition
    {
        public Annotation Annotation {get;}
        public IPropertyDictionary Properties { get; }

        public ObjectDefinition(Annotation annotation, IPropertyDictionary properties)
        {
            Annotation = annotation;
            Properties = properties;
        }

    }
}
