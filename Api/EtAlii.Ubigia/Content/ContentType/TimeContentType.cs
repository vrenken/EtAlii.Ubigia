namespace EtAlii.Ubigia
{
    public class TimeContentType : ContentType
    {
        private const string _timeContentTypeId = "Time";

        public ContentType Year { get; } = new ContentType(_timeContentTypeId, "Year");

        public ContentType Month { get; } = new ContentType(_timeContentTypeId, "Month");

        public ContentType Day { get; } = new ContentType(_timeContentTypeId, "Day");

        public ContentType Hour { get; } = new ContentType(_timeContentTypeId, "Hour");

        public ContentType Minute { get; } = new ContentType(_timeContentTypeId, "Minute");

        internal TimeContentType()
            : base(_timeContentTypeId)
        {
        }
    }
}