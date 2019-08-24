namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    internal class VariableSubject : Subject
    {
        public readonly string Name;

        public VariableSubject(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"${Name}";
        }
    }
}
