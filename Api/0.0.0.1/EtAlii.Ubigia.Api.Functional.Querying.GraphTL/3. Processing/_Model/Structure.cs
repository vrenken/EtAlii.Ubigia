namespace EtAlii.Ubigia.Api.Functional
{
    using System.Collections.ObjectModel;

    public sealed class Structure
    {
        //public Identifier Id { get; }

        public ReadOnlyObservableCollection<Structure> Children { get; }

        public ReadOnlyObservableCollection<Value> Values { get; }

        public Structure()
        {
            var children = new ObservableCollection<Structure>();
            var values = new ObservableCollection<Value>();

            Children = new ReadOnlyObservableCollection<Structure>(children);
            Values  = new ReadOnlyObservableCollection<Value>(values);

        }
        
        public Structure(ObservableCollection<Structure> children, ObservableCollection<Value> values)
        {
            Children = new ReadOnlyObservableCollection<Structure>(children);
            Values  = new ReadOnlyObservableCollection<Value>(values);
        }

    }
}
