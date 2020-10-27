﻿namespace EtAlii.Ubigia
{
    using System;

    /// <summary>
    /// A Ubigia storage, including it's address where it can be found. 
    /// </summary>
    public class Storage : IIdentifiable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}