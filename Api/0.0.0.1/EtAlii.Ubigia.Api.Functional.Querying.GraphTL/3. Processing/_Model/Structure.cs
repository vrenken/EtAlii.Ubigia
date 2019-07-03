namespace EtAlii.Ubigia.Api.Functional 
{
    using System.Collections.ObjectModel;

    public sealed class Structure
    {
        //public Identifier Id { get; }
        public string Name { get; }

        public ReadOnlyObservableCollection<Structure> Children { get; }

        public ReadOnlyObservableCollection<Value> Values { get; }

        public Structure(string name)
        {
            Name = name;
            
            var children = new ObservableCollection<Structure>();
            var values = new ObservableCollection<Value>();

            Children = new ReadOnlyObservableCollection<Structure>(children);
            Values  = new ReadOnlyObservableCollection<Value>(values);

        }
        
        public Structure(string name, ReadOnlyObservableCollection<Structure> children, ReadOnlyObservableCollection<Value> values)
        {
            Name = name;
            Children = children;
            Values  = values;
        }

    }
}
