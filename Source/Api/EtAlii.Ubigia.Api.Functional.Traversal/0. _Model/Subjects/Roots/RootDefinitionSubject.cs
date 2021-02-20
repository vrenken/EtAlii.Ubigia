namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public class RootDefinitionSubject : Subject
    {
        public readonly string Type;
        //public readonly PathSubject Schema

        public RootDefinitionSubject(string type)//, PathSubject schema)
        {
            Type = type;
            //Schema = schema
        }

        public override string ToString()
        {
            return Type;//Schema == null ? $"[Type]" : $"[Type]:[Schema]"
        }
    }
}
