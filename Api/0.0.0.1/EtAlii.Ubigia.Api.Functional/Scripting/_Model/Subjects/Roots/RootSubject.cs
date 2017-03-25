namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    public class RootSubject : Subject
    {
        public readonly string Name;

        public RootSubject(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"root:{Name}";
        }
    }
}
