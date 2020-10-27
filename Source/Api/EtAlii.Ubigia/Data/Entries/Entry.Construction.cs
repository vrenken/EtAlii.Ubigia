﻿namespace EtAlii.Ubigia
{
    using System;

    public sealed partial class Entry
    {
        public static Entry NewEntry(Guid storage, Guid account, Guid space)
        {
            var identifier = Identifier.NewIdentifier(storage, account, space);
            return new Entry(identifier);
        }

        public static Entry NewEntry(Identifier id, Relation previous)
        {
            return new Entry(id, previous);
        }

        public static Entry NewEntry(Identifier id)
        {
            return new Entry(id);
        }

        /// <summary>
        /// Instantiate a new entry. 
        /// </summary>
        /// <returns></returns>
        public static Entry NewEntry()
        {
            return new Entry();
        }

    }
}