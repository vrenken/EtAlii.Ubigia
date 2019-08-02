namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class SchemaProcessingResult : INotifyPropertyChanged
    {
        public Schema Schema { get; }
        
        public FragmentExecutionPlan CurrentExecutionPlan { get => _currentExecutionPlan; set => SetProperty(ref _currentExecutionPlan, value); }
        private FragmentExecutionPlan _currentExecutionPlan;

        public int Step { get => _step; set => SetProperty(ref _step, value); }
        private int _step;

        public int Total { get; }

        public IObservable<Structure> Output { get; private set; }
        public ReadOnlyObservableCollection<Structure> Structure {get; }

        internal SchemaProcessingResult(Schema schema,
            int total,
            ReadOnlyObservableCollection<Structure> structure)
        {
            Schema = schema;
            Total = total;
            Structure = structure;
        }

        internal void Update(IObservable<Structure> output)
        {
            Output = output;
        }
        internal void Update(int step, FragmentExecutionPlan currentExecutionPlan)
        {
            CurrentExecutionPlan = currentExecutionPlan;
            Step = step;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        private void SetProperty<T>(ref T storage, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, newValue)) return;
            storage = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
