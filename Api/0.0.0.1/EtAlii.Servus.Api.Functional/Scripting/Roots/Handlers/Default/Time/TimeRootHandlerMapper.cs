namespace EtAlii.Servus.Api.Functional
{
    using System;

    internal class TimeRootHandlerMapper : IRootHandlerMapper
    {
        public string Name { get { return _name; } }
        private readonly string _name;

        public IRootHandler[] AllowedRootHandlers { get { return _allowedRootHandlers; } }
        private readonly IRootHandler[] _allowedRootHandlers;

        public TimeRootHandlerMapper()
        {
            _name = "time";

            _allowedRootHandlers = new IRootHandler[]
            {
                new TimeRootByPathBasedYyyymmddhhmmssHandler(), // 00: YYYY/MM/DD/HH/MM/SS
                new TimeRootByPathBasedYyyymmddhhmmHandler(), // 01: YYYY/MM/DD/HH/MM
                new TimeRootByPathBasedYyyymmddhhHandler(), // 02: YYYY/MM/DD/HH
                new TimeRootByPathBasedYyyymmddHandler(), // 03: YYYY/MM/DD
                new TimeRootByPathBasedYyyymmHandler(), // 04: YYYY/MM
                new TimeRootByPathBasedYyyyHandler(), // 05: YYYY

                new TimeRootByRegexBasedNowHandler(), // 06: now, NOW, Now, NoW, noW, nOw

                new TimeRootByRegexBasedYyyymmddhhmmssHandler(), // 09: "YYYYMMDDHHMMSS"
                new TimeRootByRegexBasedYyyymmddhhmmHandler(), // 10: "YYYYMMDDHHMM"
                new TimeRootByRegexBasedYyyymmddhhHandler(), // 11: "YYYYMMDDHH"
                new TimeRootByRegexBasedYyyymmddHandler(), // 12: "YYYYMMDD"
                new TimeRootByRegexBasedYyyymmHandler(), // 13: "YYYYMM"

                new TimeRootByRegexBasedSeparatedYyyymmddhhmmssHandler(), // 14: "YYYY-MM-DD HH:MM:SS" 
                new TimeRootByRegexBasedSeparatedYyyymmddhhmmHandler(), // 15: "YYYY-MM-DD HH:MM"
                new TimeRootByRegexBasedSeparatedYyyymmddhhHandler(), // 16: "YYYY-MM-DD HH" 
                new TimeRootByRegexBasedSeparatedYyyymmddHandler(), // 17: "YYYY-MM-DD"
                new TimeRootByRegexBasedSeparatedYyyymmHandler(), // 18: "YYYY-MM" 

                new TimeRootByEmptyHandler(), // 05: only root, no arguments, should be at the end.
            };
        }
    }
}