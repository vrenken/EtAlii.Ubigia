// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System.ComponentModel;

    public class Status : INotifyPropertyChanged
    {
        public string Id { get; init; }
        public string Summary { get => _summary; set => PropertyChanged.SetAndRaise(this, ref _summary, value); }
        private string _summary;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
