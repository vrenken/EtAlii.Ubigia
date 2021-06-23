// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    public class TimeContentType : ContentType
    {
        private const string TimeContentTypeId = "Time";

        public ContentType Year { get; } = new(TimeContentTypeId, "Year");

        public ContentType Month { get; } = new(TimeContentTypeId, "Month");

        public ContentType Day { get; } = new(TimeContentTypeId, "Day");

        public ContentType Hour { get; } = new(TimeContentTypeId, "Hour");

        public ContentType Minute { get; } = new(TimeContentTypeId, "Minute");

        internal TimeContentType()
            : base(TimeContentTypeId)
        {
        }
    }
}
