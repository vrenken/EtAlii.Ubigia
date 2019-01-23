namespace EtAlii.Ubigia.Windows.Shared
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using EtAlii.Ubigia.Windows.Settings;

    public static class Registrations
    {
        public static string[] GetObsolete(IGlobalSettings globalSettings)
        {
            var obsoleteAssemblies = new List<string>();

            var assemblies = Directory.GetFiles(App.ShellExtensionsDirectory, "*.dll");

            foreach (var assembly in assemblies)
            {
                var hasStorageSettings = AssemblyHasStorageSettings(assembly, globalSettings);
                if (!hasStorageSettings)
                {
                    obsoleteAssemblies.Add(assembly);
                }
            }

            return obsoleteAssemblies.ToArray();
        }

        public static Guid[] GetMissing(IGlobalSettings globalSettings)
        {
            var missingAssemblies = new List<Guid>();

            //var assemblies = Directory.GetFiles(App.ShellExtensionsDirectory, "*.dll")

            foreach (var storageSettings in globalSettings.Storage)
            {
                var hasAssembly = StorageSettingsHasAssembly(storageSettings);
                if (!hasAssembly)
                {
                    missingAssemblies.Add(storageSettings.Id);
                }
            }

            return missingAssemblies.ToArray();
        }

        private static bool StorageSettingsHasAssembly(StorageSettings storageSettings)
        {
            var fileName = $"{storageSettings.Id}.dll";
            fileName = Path.Combine(App.ShellExtensionsDirectory, fileName);
            return File.Exists(fileName);
        }

        private static bool AssemblyHasStorageSettings(string fileName, IGlobalSettings globalSettings)
        {
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            var id = new Guid(fileNameWithoutExtension);
            return globalSettings.Storage.Any(storageSettings => storageSettings.Id == id);
        }

    }
}
