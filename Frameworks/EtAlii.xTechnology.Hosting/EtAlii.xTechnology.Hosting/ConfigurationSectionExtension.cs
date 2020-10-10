namespace EtAlii.xTechnology.Hosting
{
    using System.Collections.Generic;
    using Microsoft.Extensions.Configuration;

    public static class ConfigurationSectionExtension
    {
        public static IConfigurationSection[] GetAllSections(this IConfigurationSection section, string key)
        {
            var result = new List<IConfigurationSection>();

            int index = 0;
            bool exists;
            do
            {
                var childSection = section.GetSection($"{key}:{index++}");

                exists = childSection.Exists();
                if (exists)
                {
                    result.Add(childSection);
                }
            } while (exists);

            return result.ToArray();
        }
    }
}
