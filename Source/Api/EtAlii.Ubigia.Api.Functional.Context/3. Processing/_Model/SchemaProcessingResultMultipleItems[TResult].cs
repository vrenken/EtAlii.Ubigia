namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Reactive.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public class SchemaProcessingResultMultipleItems<TResult>  : INotifyPropertyChanged
    {
        public Schema Schema { get; }

        /// <inheritdoc />
        public event PropertyChangedEventHandler PropertyChanged;

        public FragmentExecutionPlan CurrentExecutionPlan { get => _currentExecutionPlan; set => SetProperty(ref _currentExecutionPlan, value); }
        private FragmentExecutionPlan _currentExecutionPlan;

        public int Step { get => _step; set => SetProperty(ref _step, value); }
        private int _step;

        public int Total { get; }

        public IObservable<TResult> Output { get; private set; }
        public ReadOnlyObservableCollection<TResult> Items { get; }

        public SchemaProcessingResultMultipleItems(Schema schema, int total, ReadOnlyObservableCollection<TResult> items)
        {
            Schema = schema;
            Total = total;
            Items = items;
        }

        internal void Update(IObservable<TResult> schemaOutput)
        {
            Output = schemaOutput;
        }
        internal void Update(int step, FragmentExecutionPlan currentExecutionPlan)
        {
            CurrentExecutionPlan = currentExecutionPlan;
            Step = step;
        }

        private void SetProperty<T>(ref T storage, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, newValue)) return;
            storage = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Awaiting this method ensures GCL schema processing has finished.
        /// </summary>
        /// <returns></returns>
        public async Task Completed()
        {
            await Output.LastOrDefaultAsync();
        }
    }
}
