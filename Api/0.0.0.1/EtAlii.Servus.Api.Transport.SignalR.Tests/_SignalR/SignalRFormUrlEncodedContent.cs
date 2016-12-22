namespace EtAlii.Servus.Api.Transport.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using FormUrlEncodedContent = SignalRFormUrlEncodedContent;

    /// <summary>
    /// A container for name/value tuples encoded using application/x-www-form-urlencoded MIME type.
    /// </summary>
    internal class SignalRFormUrlEncodedContent : ByteArrayContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Net.Http.FormUrlEncodedContent"/> class with a specific collection of name/value pairs.
        /// </summary>
        /// <param name="nameValueCollection">A collection of name/value pairs.</param>
        public SignalRFormUrlEncodedContent(IEnumerable<KeyValuePair<string, string>> nameValueCollection)
            : base(FormUrlEncodedContent.GetContentByteArray(nameValueCollection))
        {
            this.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
        }

        private static byte[] GetContentByteArray(IEnumerable<KeyValuePair<string, string>> nameValueCollection)
        {
            if (nameValueCollection == null)
            {
                throw new ArgumentNullException("nameValueCollection");
            }
            var stringBuilder = new StringBuilder();
            foreach (var keyValuePair in nameValueCollection)
            {
                if (stringBuilder.Length > 0)
                {
                    stringBuilder.Append('&');
                }
                stringBuilder.Append(FormUrlEncodedContent.Encode(keyValuePair.Key));
                stringBuilder.Append('=');
                stringBuilder.Append(FormUrlEncodedContent.Encode(keyValuePair.Value));
            }
            return SignalRHttpRuleParser.DefaultHttpEncoding.GetBytes(stringBuilder.ToString());
        }

        private static string Encode(string data)
        {
            if (string.IsNullOrEmpty(data))
                return string.Empty;
            return EscapeDataString(data).Replace("%20", "+");
        }

        private static string EscapeDataString(string data)
        {
            const int limit = 32760;

            var sb = new StringBuilder();
            int loops = data.Length / limit;

            for (int i = 0; i <= loops; i++)
            {
                if (i < loops)
                {
                    sb.Append(Uri.EscapeDataString(data.Substring(limit * i, limit)));
                }
                else
                {
                    sb.Append(Uri.EscapeDataString(data.Substring(limit * i)));
                }
            }
            return sb.ToString();
        }
        //private static string EscapeDataString(string data)
        //{
        //    return Uri.EscapeDataString(data);
        //}
    }
}
