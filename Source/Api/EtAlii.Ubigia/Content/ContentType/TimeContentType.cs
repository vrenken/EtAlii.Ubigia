namespace EtAlii.Ubigia
{
    public class TimeContentType : ContentType
    {
        private const string TimeContentTypeId = "Time";

        public ContentType Year { get; } = new ContentType(TimeContentTypeId, "Year");

        public ContentType Month { get; } = new ContentType(TimeContentTypeId, "Month");

        public ContentType Day { get; } = new ContentType(TimeContentTypeId, "Day");

        public ContentType Hour { get; } = new ContentType(TimeContentTypeId, "Hour");

        public ContentType Minute { get; } = new ContentType(TimeContentTypeId, "Minute");

        internal TimeContentType()
            : base(TimeContentTypeId)
        {
        }
    }
}