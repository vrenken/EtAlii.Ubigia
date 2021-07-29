﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    public class TagComponent : NonCompositeComponent
    {
        internal TagComponent()
        {
        }

        public string Tag { get; internal set; }

        /// <inheritdoc />
        protected internal override string Name => "Tag";

        /// <inheritdoc />
        protected internal override void Apply(IComponentEditableEntry entry, bool markAsStored)
        {
            entry.TagComponent = this;
        }
    }
}