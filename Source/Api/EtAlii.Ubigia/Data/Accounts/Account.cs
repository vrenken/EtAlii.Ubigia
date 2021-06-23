// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System;

    public class Account : IIdentifiable
    {
        public Account()
        {
            Roles = Array.Empty<string>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string[] Roles { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}