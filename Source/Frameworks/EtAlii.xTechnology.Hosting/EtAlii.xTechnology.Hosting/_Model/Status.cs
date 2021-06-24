// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System.ComponentModel;

    public class Status : INotifyPropertyChanged
    {
        public string Id { get; }
        public string Title { get => _title; set => PropertyChanged.SetAndRaise(this, ref _title, value); }
        private string _title;
        public string Summary { get => _summary; set => PropertyChanged.SetAndRaise(this, ref _summary, value); }
        private string _summary;
        public string Description { get => _description; set => PropertyChanged.SetAndRaise(this, ref _description, value); }
        private string _description;

        public event PropertyChangedEventHandler PropertyChanged;

        public Status(string id)
        {
            Id = id;
        }
    }
}
