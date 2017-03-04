namespace EtAlii.Ubigia.Api
{
    public class TimeContentType : ContentType
    {
        private const string _timeContentTypeId = "Time";

        public ContentType Year => _year;
        private readonly ContentType _year = new ContentType(_timeContentTypeId, "Year");

        public ContentType Month => _month;
        private readonly ContentType _month = new ContentType(_timeContentTypeId, "Month");

        public ContentType Day => _day;
        private readonly ContentType _day = new ContentType(_timeContentTypeId, "Day");

        public ContentType Hour => _hour;
        private readonly ContentType _hour = new ContentType(_timeContentTypeId, "Hour");

        public ContentType Minute => _minute;
        private readonly ContentType _minute = new ContentType(_timeContentTypeId, "Minute");

        internal TimeContentType()
            : base(_timeContentTypeId)
        {
        }
    }
}