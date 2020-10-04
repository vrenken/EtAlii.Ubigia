namespace EtAlii.Ubigia.Diagnostics.Profiling
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Dynamic;

    public class ProfilingResult : DynamicObject, INotifyPropertyChanged
    {
        public ProfilingResult Parent { get; }

        private readonly bool _showInResults;

        public string Action => Get<string>(ProfilingProperty.Action);
        public string ProfilerName => Get<string>(ProfilingProperty.ProfilerName);
        public ProfilingLayer Layer => Get<ProfilingLayer>(ProfilingProperty.Layer);

        public DateTime Started => Get<DateTime>(ProfilingProperty.Started);
        public DateTime Stopped => Get<DateTime>(ProfilingProperty.Stopped);
        public double DurationTotal => Get<double>(ProfilingProperty.DurationTotal);
        public double DurationOfSelf => Get<double>(ProfilingProperty.DurationOfSelf);
        public double DurationOfChildren => Get<double>(ProfilingProperty.DurationOfChildren);

        private readonly PropertyDictionary _properties = new PropertyDictionary();

        public ReadOnlyObservableCollection<ProfilingResult> Children { get; }

        private readonly ObservableCollection<ProfilingResult> _items; 

        public object this[string propertyName]
        {
            get
            {
                _properties.TryGetValue(propertyName, out var value);
                return value;
            }
            set { _properties[propertyName] = value; }
        }

        public ProfilingResult(
            ProfilingResult parent, 
            string profilerName, 
            ProfilingLayer layer, 
            string action, bool showInResults = true)
        {
            _showInResults = showInResults;

            while (parent != null)
            {
                if (parent._showInResults)
                {
                    break;
                }
                parent = parent.Parent;
            }

            Parent = parent;

            if(showInResults)
            { 
                Parent?._items.Add(this);
            }

            _items = new ObservableCollection<ProfilingResult>();
            Children = new ReadOnlyObservableCollection<ProfilingResult>(_items);


            Set(ProfilingProperty.ProfilerName, profilerName);
            Set(ProfilingProperty.Layer, layer);
            Set(ProfilingProperty.Action, action);
        }

        public void Start()
        {
            if (Get<DateTime>(ProfilingProperty.Started) != DateTime.MinValue)
            {
                throw new InvalidProfilingOperationException("The profiling result has already been started");
            }
            Set(ProfilingProperty.Started, DateTime.UtcNow);
        }

        public void Stop()
        {
            var started = Get<DateTime>(ProfilingProperty.Started);
            if (started == DateTime.MinValue)
            {
                throw new InvalidProfilingOperationException("The profiling result has not yet been been started");
            }
            var stopped = Get<DateTime>(ProfilingProperty.Stopped);
            if (stopped != DateTime.MinValue)
            {
                throw new InvalidProfilingOperationException("The profiling result has already been stopped");
            }
            stopped = DateTime.UtcNow;
            Set(ProfilingProperty.Stopped, stopped);

            double durationTotal = (stopped - started).TotalMilliseconds;
            Set(ProfilingProperty.DurationTotal, durationTotal);

            var durationOfChildren = Get<double>(ProfilingProperty.DurationOfChildren);
            double percentage = durationTotal / 1;
            double percentageOfChildren = durationOfChildren / percentage;
            percentageOfChildren = double.IsNaN(percentageOfChildren) ? 0f : percentageOfChildren;
            double percentageOfSelf = 1 - percentageOfChildren;
            Set(ProfilingProperty.DurationOfChildren, percentageOfChildren);
            Set(ProfilingProperty.DurationOfSelf, percentageOfSelf);
            if (Parent != null)
            {
                var durationOfParentChildren = Parent.Get<double>(ProfilingProperty.DurationOfChildren);
                durationOfParentChildren += durationTotal;
                Parent.Set(ProfilingProperty.DurationOfChildren, durationOfParentChildren);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));   
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return _properties.Keys;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            // Locate property by name
            return _properties.TryGetValue(binder.Name, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            var raisePropertyChanged = true;
            if(_properties.TryGetValue(binder.Name, out var currentValue))
            {
                raisePropertyChanged = currentValue != value;
            }
            _properties[binder.Name] = value;
            if (raisePropertyChanged)
            {
                OnPropertyChanged(binder.Name);
            }
            return true;
        }

        private T Get<T>(string name)
        {
            if (!_properties.TryGetValue(name, out var value))
            {
                value = default(T);
            }
            return (T)value;
        }
        private void Set(string name, object value)
        {
            _properties[name] = value;
        }

        public override string ToString()
        {
            return $"{Get<string>(ProfilingProperty.ProfilerName)} - {Get<string>(ProfilingProperty.Action)}";
        }

        public void Add(ProfilingResult result)
        {
            _items.Add(result);
        }
    }
}
