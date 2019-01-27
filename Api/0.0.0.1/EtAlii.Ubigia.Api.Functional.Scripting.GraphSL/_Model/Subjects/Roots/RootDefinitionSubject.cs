namespace EtAlii.Ubigia.Api.Functional
{
    internal class RootDefinitionSubject : Subject
    {
        public readonly string Type;
        
        public RootDefinitionSubject(string type)
        {
            Type = type;
        }

        public override string ToString()
        {
            return Type;
        }
    }
}
