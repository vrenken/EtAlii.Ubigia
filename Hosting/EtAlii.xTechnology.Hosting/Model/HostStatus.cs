namespace EtAlii.xTechnology.Hosting
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class HostStatus : INotifyPropertyChanged
    {
        public string Id { get; }
        public string Title { get => _title; set => SetProperty(ref _title, value); }
        private string _title;
        public string Summary { get => _summary; set => SetProperty(ref _summary, value); }
        private string _summary;
        public string Description { get => _description; set => SetProperty(ref _description, value); }
        private string _description;

        public event PropertyChangedEventHandler PropertyChanged;

        public HostStatus(string id)
        {
            Id = id;
        }


        private void SetProperty<T>(ref T storage, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!object.Equals((object)storage, (object)newValue))
            {
                storage = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
