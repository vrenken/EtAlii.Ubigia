namespace EtAlii.Servus.PowerShell
{
    using EtAlii.Servus.Client.Model;
    using System;
    using System.Web;

    public class AddressFactory
    {
        public string Create(Storage storage, string path)
        {
            var builder = new UriBuilder(storage.Address);
            builder.Path = path;
            return builder.ToString();
        }

        public string Create(Storage storage, string path, params string[] parameters)
        {
            var httpValueCollection = HttpUtility.ParseQueryString(String.Empty);
            for (int i = 0; i < parameters.Length; i += 2)
            {
                var key = parameters[i];
                var value = parameters[i + 1];
                httpValueCollection[key] = value;
            }

            var builder = new UriBuilder(storage.Address);
            builder.Path = String.Format("{0}?{1}", path, httpValueCollection.ToString());
            return builder.ToString();
        }
    }
}
