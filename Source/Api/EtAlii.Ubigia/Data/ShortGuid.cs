namespace EtAlii.Ubigia
{
    using System;

    /// <summary>
    /// A guid can be made more compact when the full alphabet is used.
    /// </summary>
    public static class ShortGuid
    {

        public static string New()
        {
            return Base36Convert.ToString(Guid.NewGuid()).ToUpper();
        }
    }
}
