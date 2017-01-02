using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EtAlii.Ubigia.Client.Windows.Shared
{
    public static class Registrations
    {
        public static string[] GetObsolete(GlobalSettings globalSettings)
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

        public static Guid[] GetMissing(GlobalSettings globalSettings)
        {
            var missingAssemblies = new List<Guid>();

            var assemblies = Directory.GetFiles(App.ShellExtensionsDirectory, "*.dll");

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
            var fileName = String.Format("{0}.dll", storageSettings.Id);
            fileName = Path.Combine(App.ShellExtensionsDirectory, fileName);
            return File.Exists(fileName);
        }

        private static bool AssemblyHasStorageSettings(string fileName, GlobalSettings globalSettings)
        {
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            var id = new Guid(fileNameWithoutExtension);
            return globalSettings.Storage.Any(storageSettings => storageSettings.Id == id);
        }

    }
}
