// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    public class TimeContentType : ContentType
    {
        private const string _timeContentTypeId = "Time";

        public ContentType Year { get; } = new(_timeContentTypeId, "Year");

        public ContentType Month { get; } = new(_timeContentTypeId, "Month");

        public ContentType Day { get; } = new(_timeContentTypeId, "Day");

        public ContentType Hour { get; } = new(_timeContentTypeId, "Hour");

        public ContentType Minute { get; } = new(_timeContentTypeId, "Minute");

        internal TimeContentType()
            : base(_timeContentTypeId)
        {
        }
    }
}