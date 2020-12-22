namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal class TimeRootHandlerMapper : IRootHandlerMapper
    {
        public string Name { get; }

        public IRootHandler[] AllowedRootHandlers { get; }

        public TimeRootHandlerMapper()
        {
            Name = "time";

            var timePreparer = new TimePreparer();

           AllowedRootHandlers = new IRootHandler[]
            {
                new TimeRootByPathBasedYyyymmddhhmmssmmmHandler(timePreparer), // 00: YYYY/MM/DD/HH/MM/SS/MMM
                new TimeRootByPathBasedYyyymmddhhmmssHandler(timePreparer), // 01: YYYY/MM/DD/HH/MM/SS
                new TimeRootByPathBasedYyyymmddhhmmHandler(timePreparer), // 02: YYYY/MM/DD/HH/MM
                new TimeRootByPathBasedYyyymmddhhHandler(timePreparer), // 03: YYYY/MM/DD/HH
                new TimeRootByPathBasedYyyymmddHandler(timePreparer), // 04: YYYY/MM/DD
                new TimeRootByPathBasedYyyymmHandler(timePreparer), // 05: YYYY/MM
                new TimeRootByPathBasedYyyyHandler(timePreparer), // 06: YYYY

                new TimeRootByRegexBasedNowHandler(timePreparer), // 07: now, NOW, Now, NoW, noW, nOw

                new TimeRootByRegexBasedYyyymmddhhmmssmmmHandler(timePreparer), // 08: "YYYYMMDDHHMMSSMMM"
                new TimeRootByRegexBasedYyyymmddhhmmssHandler(timePreparer), // 09: "YYYYMMDDHHMMSS"
                new TimeRootByRegexBasedYyyymmddhhmmHandler(timePreparer), // 10: "YYYYMMDDHHMM"
                new TimeRootByRegexBasedYyyymmddhhHandler(timePreparer), // 11: "YYYYMMDDHH"
                new TimeRootByRegexBasedYyyymmddHandler(timePreparer), // 12: "YYYYMMDD"
                new TimeRootByRegexBasedYyyymmHandler(timePreparer), // 13: "YYYYMM"

                new TimeRootByRegexBasedSeparatedYyyymmddhhmmssmmmHandler(timePreparer), // 14: "YYYY-MM-DD HH:MM:SS.MMM"
                new TimeRootByRegexBasedSeparatedYyyymmddhhmmssHandler(timePreparer), // 15: "YYYY-MM-DD HH:MM:SS"
                new TimeRootByRegexBasedSeparatedYyyymmddhhmmHandler(timePreparer), // 16: "YYYY-MM-DD HH:MM"
                new TimeRootByRegexBasedSeparatedYyyymmddhhHandler(timePreparer), // 17: "YYYY-MM-DD HH"
                new TimeRootByRegexBasedSeparatedYyyymmddHandler(timePreparer), // 18: "YYYY-MM-DD"
                new TimeRootByRegexBasedSeparatedYyyymmHandler(timePreparer), // 19: "YYYY-MM"

                new TimeRootByEmptyHandler(), // 20: only root, no arguments, should be at the end.
            };
        }
    }
}
