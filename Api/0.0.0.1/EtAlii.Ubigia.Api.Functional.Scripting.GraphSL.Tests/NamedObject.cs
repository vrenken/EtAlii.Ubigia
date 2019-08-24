namespace EtAlii.Ubigia.Api.Functional.Scripting.Tests
{
    using EtAlii.Ubigia.Api.Logical;

    public class NamedObject : Node
    {
        public string Name { get { return GetProperty<string>(); } set { SetProperty(value); } }
        public int Value { get { return GetProperty<int>(); } set { SetProperty(value); } }

        public NamedObject(IReadOnlyEntry entry)
            : base(entry)
        {
        }
    }
}
