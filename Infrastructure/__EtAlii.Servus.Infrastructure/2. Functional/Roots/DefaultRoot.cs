using EtAlii.Servus.Client.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EtAlii.Servus.Infrastructure.Model
{
    public static class DefaultRoot
    {
        public const string Head = "Head";
        public const string Tail = "Tail";
        public const string Hierarchy = "Hierarchy";
        public const string Sequences = "Sequences";
        public const string Tags = "Tags";
        public const string Time = "Time";
        public const string Communications = "Communications";
        public const string Contacts = "Contacts";
        public const string Locations = "Locations";
        public const string Subscriptions = "Subscriptions";

        public static readonly string[] All = new string[]
        {
            Head,
            Tail,
            Hierarchy,
            Sequences,
            Tags,
            Time,
            Communications,
            Contacts,
            Locations,
            Subscriptions,
        };
    }
}