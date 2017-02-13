using EtAlii.Ubigia.Client.Windows.Shared;
using LogicNP.EZNamespaceExtensions;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace EtAlii.Ubigia.Client.Windows.ShellExtension
{
    [Guid(Identifiers.RootFolderItemString)]
    public class RootFolderItem : FolderItem 
	{
        protected StorageSettings StorageSettings { get { return _storageSettings.Value; } }
        private Lazy<StorageSettings> _storageSettings;

        public RootFolderItem()
            : base()
		{
            _storageSettings = new Lazy<StorageSettings>(GetStorageSetting);

            if (StorageSettings != null)
            {
                FullPath = StorageSettings.StorageLocation;
                Name = Path.GetFileName(FullPath);
            }
        }

        private StorageSettings GetStorageSetting()
        {
            var type = this.GetType();
            var guidAttribute = type.GetCustomAttribute<GuidAttribute>();
            var id = new Guid(guidAttribute.Value);

            var globalSettings = App.Current.Container.GetInstance<IGlobalSettings>();
            return globalSettings.Storage.Where(storageSettings => storageSettings.Id == id)
                                         .SingleOrDefault();
        }

        // The root folder class of your namespace extension should override the
        // GetNSETargetInfo method and return a NSETargetInfo object which 
        // contains information such as the location of the namespace extension
        // To register the namespace extension under multiple locations,
        // use the GetNSETargetInfoEx method.
        public override NSETargetInfo GetNSETargetInfo()
        {
            var name = StorageSettings != null ? StorageSettings.Name : "Unknown Space";
            return CreateTargetInfo(name, NSETarget.MyComputer);
        }

        public override IEnumerable GetChildren(GetChildrenEventArgs e)
        {
            var children = new ArrayList(20);

            // Add folders if asked for
            if ((e.ChildrenType & ChildrenType.Folders) != 0)
            {
                string[] dirs = Directory.GetDirectories(FullPath);
                foreach (string s in dirs)
                {
                    children.Add(new FolderItem(s));
                }
            }

            // Add files if asked for
            if ((e.ChildrenType & ChildrenType.NonFolders) != 0)
            {
                string[] files = Directory.GetFiles(FullPath);
                foreach (string s in files)
                {
                    children.Add(new FileItem(s));
                }
            }
            return children;
        }
	}
}
