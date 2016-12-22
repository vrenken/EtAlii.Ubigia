using LogicNP.EZNamespaceExtensions;
using System;
using System.Drawing;
using System.IO;

namespace EtAlii.Servus.Client.Windows
{
    public class FileItem : NSEItem
    {
		// name of the file
        internal string name;
		// Full path of the file
        internal string fullPath;

        public FileItem(string s)
        {
            fullPath = s;
            name = Path.GetFileName(s);
        }

        public FileItem(string parentFolder, string name)
        {
            fullPath = Path.Combine(parentFolder, name);
            this.name = name;
        }

		// The GetIconFileAndIndex method is called to retrieve information about the icon 
		// for the item.
		public override void GetIconFileAndIndex(GetIconFileAndIndexEventArgs e)
        {
			// Use the same icon as that shown for the file
            e.IconExtractMode = IconExtractMode.SystemImageListIndexFromPath;
            e.IconFile = fullPath;

        }

		public override int[] GetXPTaskPaneColumnIndices()
		{
			// Use all column values
			return new int[] { 0,1,2,3 };
		}

        public override int[] GetTileViewColumnIndices()
        {
			// Use all column values
			return new int[] { 1, 2, 3 };
        }
        public override int[] GetPreviewDetailsColumnIndices()
        {
			// Use all column values
			return new int[] { 0, 1, 2, 3 };
        }	

		// Called to get the thumbnail for the item
		public override void GetThumbnail(GetThumbnailEventArgs e)
		{
			if(fullPath.EndsWith(".jpg") || fullPath.EndsWith(".bmp") || fullPath.EndsWith(".gif"))
			{
				Bitmap temp = new Bitmap(fullPath);
				if(temp!=null)
				{
					e.Thumbnail = new Bitmap(temp,e.Width,e.Height);
					temp.Dispose();
				}
			}
		}


		// The IsValid is called to verify whether the item is valid or not. Typically, 
		// your implementation should check with the actual underlying data that this 
		// item represents and return true or false based on the status of the data.
		public override bool IsValid()
        {
            return File.Exists(fullPath);
        }


        public override string GetInfoTip()
        {
            return fullPath;
        }

        public override void GetOverlayIcon(GetOverlayIconEventArgs e)
        {
			// Use shortcut overlay icon the file is a link
            if (fullPath.EndsWith(".lnk"))
            {
                e.OverlayIconType = OverlayIconType.Shortcut;
                return;
            }
        }

		// The Serialize method is called to persist immutable data about the item. 
		// A typical implementation should first store the type of this item and 
		// then information specific to this type of item. 
        public override void Serialize(BinaryWriter writer)
        {
			// Write version number of the serialized data. This is necessary as Windows may cache 
			// old data and present it back to us resulting in mismatch.
			writer.Write((byte)1); // Version=1, change if data format changes

			// Store type = 0 (file)
			writer.Write((byte)0);

			// Store name of the file
            writer.Write(name);
        }

		// The GetDisplayName method is called to retrieve the display name of the item.
		public override string GetDisplayName()
        {
            return name;
        }

		// The GetAttributes method is called to retrieve the attributes of the item 
		// in the namespace extension. Folder items should include 
		// NSEItemAttributes.Folder in the return value. Other attributes should be 
		// returned if the item supports the corresponding functionality. 
		public override NSEItemAttributes GetAttributes(NSEItemAttributes attributes)
        {
            return NSEItemAttributes.CanDelete
                | NSEItemAttributes.CanRename
                | NSEItemAttributes.CanLink
                | NSEItemAttributes.CanCopy
                | NSEItemAttributes.CanMove
                | NSEItemAttributes.CanLink
                | NSEItemAttributes.FileSystem
                ;
        }

		// The OnChangeName method is called when the item has been renamed in Windows Explorer.
		// This method should return true of the renaming was successfully applied.
		public override bool OnChangeName(ChangeNameEventArgs e)
        {
            try
            {
                string parentFolder = Path.GetDirectoryName(fullPath);
                string newName = Path.Combine(parentFolder, e.NewName);
                File.Move(fullPath, newName);
                fullPath = newName;
                name = e.NewName; // store new name
                return true; // success!
            }
            catch
            {
                return false;
            }
        }

        string SizeStrFromLength(long length)
        {
            if (length <= 0)
                return "0 bytes";

            length = length / 1024;
            if (length < 1024)
            {
                return length.ToString() + " KB";
            }

            length = length / 1024;
            if (length < 1024)
            {
                return length.ToString() + " MB";
            }

            length = length / 1024;
            return length.ToString() + " GB";

        }

        /*
		// The GetColumnValue method is deprecated. Use the GetColumnValueEx instead (see below).
		public override object GetColumnValue(ShellColumn column)
        {
            if (column.Index == 0) // Name
            {
                return name;
            }

            if (column.Index == 1) // Size
            {
                FileInfo fi = new FileInfo(fullPath);
                return SizeStrFromLength(fi.Length);
            }

			if(column.Index==2) // Type
			{
				return Utils.GetFileType(fullPath);
			}


            if (column.Index == 3) // Date Modified
            {
                DateTime dt = File.GetLastWriteTime(fullPath);
                return dt;
            }
            return null;
        }
		*/

        // The GetColumnValueEx method is called to retrieve the value to be displayed for the
        // item in Details/Report mode for the specified column.
        public override bool GetColumnValueEx(ref VARIANT value,ShellColumn column)
        {
            if (column.Index == 0) // Name
            {
                value.SetValue(name);
                return true;
            }

            if (column.Index == 1) // Size
            {
                FileInfo fi = new FileInfo(fullPath);

                if (value.vt == VarType.VT_BSTR) // Value is asked in string format(on XP and before)
                    value.SetValue(SizeStrFromLength(fi.Length));
                else // Value is being asked as VARIANT 
                    value.SetValue(fi.Length);
                return true;
            }

            if (column.Index == 2) // Type
            {
				value.SetValue(Utils.GetFileType(fullPath));
                return true;
            }

            if (column.Index == 3) // Date Modified
            {
                DateTime dt = File.GetLastWriteTime(fullPath);
                if (value.vt == VarType.VT_BSTR)  // Value is asked in string format(on XP and before)
                    value.SetValue(dt.ToLocalTime().ToString());
                else // Value is being asked as VARIANT 
                    value.SetValue(dt,true); 
                return true; 
            } 
            return false;
        }

		// The CompareTo method is called to perform a comparison of the item with the
		// specified item with respect to the specified column. Return a number less 
		// than zero if this item should come before otherItem, greater than zero if 
		// this item should come after otherItem, and zero if the two items are equal.
		public override int CompareTo(NSEItem item2, ShellColumn column)
        {
			// Folders should come before files
            if (item2 is FolderItem)
                return 1;

            FileItem file2 = item2 as FileItem;

            if (column.Index == 0) // name
            {
                return name.CompareTo(file2.name);
            }

            if (column.Index == 1) // size
            {
                FileInfo fi1 = new FileInfo(fullPath);
                FileInfo fi2 = new FileInfo(file2.fullPath);

                return fi1.Length.CompareTo(fi2.Length);

            }

             if (column.Index == 1) // size
            {
                FileInfo fi1 = new FileInfo(fullPath);
                FileInfo fi2 = new FileInfo(file2.fullPath);

                return fi1.Length.CompareTo(fi2.Length);

            }

             if (column.Index == 2) // type
            {
                string type1 = Utils.GetFileType(fullPath);
				 string type2 = Utils.GetFileType(file2.fullPath);

                return type1.CompareTo(type2);

            }

           if (column.Index == 3) // date modified
            {
                DateTime dt1 = File.GetLastWriteTime(fullPath);
                DateTime dt2 = File.GetLastWriteTime(file2.fullPath);

                return dt1.CompareTo(dt2);
            }

            return 0;
        }

        public override string ToString()
        {
            return fullPath;
        }

		// The GetStream method is called when the file is dropped on external folders
        public override Stream GetStream()
        {
            return File.OpenRead(fullPath);
        }

		// The GetFileDescriptor method is called to retrieve file-related information
		// about the item.
        public override FileDescriptor GetFileDescriptor()
        {
            FileInfo fi = new FileInfo(fullPath);
            FileDescriptor fd = new FileDescriptor();
            fd.FileName = name;
            fd.Attributes = fi.Attributes;
            fd.CreationTime = fi.CreationTime;
            fd.FileSize = fi.Length;
            fd.LastAccessTime = fi.LastAccessTime;
            fd.LastWriteTime = fi.LastWriteTime;
            return fd;
        }

		// The GetDisplayNameEx method is called to retrieve different types of 
		// display names for the item. 
		public override string GetDisplayNameEx(DisplayNameFlags flags)
        {
			// If a fully qualified parsing name is requested, return the full path
            if ((flags & DisplayNameFlags.InFolder) == 0)
            {
                if ((flags & DisplayNameFlags.ForParsing) != 0)
                    return fullPath;
            }

            return GetDisplayName();
        }
    }
}
