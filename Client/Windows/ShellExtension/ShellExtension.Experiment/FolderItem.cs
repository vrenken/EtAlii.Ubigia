using EtAlii.Servus.Client.Windows.Shared;
using LogicNP.EZNamespaceExtensions;
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace EtAlii.Servus.Client.Windows
{

	// Represents the root folder of the namespace extension.
	// The GUID of the class representing the root folder is used as the 
	// GUID for the namespace extension
	[Guid("6EC97137-BE18-44b9-BB5B-92240A8D3481")]
	public class FolderItem : NSEFolder
	{
        public string FullPath;
        public string Name;

		internal string fullPath;
		string name;

		// This namespace extension browses the  folder specified below; change if required
		internal static string rootPath = @"c:\";

		public FolderItem()
		{
			fullPath = rootPath;
			name = string.Empty;
		}

		public FolderItem(string s)
		{
			fullPath = s;
			name = Path.GetFileName(s);

		}

		public FolderItem(string parentFolder, string name)
		{
			fullPath = Path.Combine(parentFolder, name);
			this.name = name;
		}

        // The root folder class of your namespace extension should override the
		// GetNSETargetInfo method and return a NSETargetInfo object which 
		// contains information such as the location of the namespace extension
		// To register the namespace extension under multiple locations,
		// use the GetNSETargetInfoEx method.
		public override NSETargetInfo GetNSETargetInfo()
		{
            return CreateTargetInfo("Storage experiment", NSETarget.MyComputer);
		}

        protected NSETargetInfo CreateTargetInfo(string name, NSETarget target)
        {
            // Located under 'My Computer'
            var targetInfo = new NSETargetInfo(name, target,
                NSEItemAttributes.Folder
                | NSEItemAttributes.HasSubFolder
                | NSEItemAttributes.Browsable
                | NSEItemAttributes.FileSysAncestor
                | NSEItemAttributes.FileSystem
                );

            targetInfo.IconFile = Path.Combine(App.CurrentDirectory, "Client.exe");
            targetInfo.IconIndex = 0;
            targetInfo.NSEVisibility = NSEVisibility.CurrentUser;
            targetInfo.InfoTip = String.Format("Browse {0}", name);
            return targetInfo;
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


		// The GetDataObject method is called to fill the data object with data representing 
		// the child items.
		public override void GetDataObject(GetDataObjectEventArgs e)
		{
			if (e.Children.Length <= 0)
				return;

			// Use the FileDrop data format
			
			string[] files = new string[e.Children.Length];
			for (int i = 0; i < e.Children.Length; i++)
			{
				if (e.Children[i] is FolderItem)
				{
					FolderItem m = e.Children[i] as FolderItem;
					files[i] = m.fullPath;
				}
				else
				{
					FileItem c = e.Children[i] as FileItem;
					files[i] = c.fullPath;
				}
			}
			e.DataObject.SetData(DataFormats.FileDrop, files);

			// Alternate way : Use streams to transfer namespace extension items.
			// Comment all of the above and uncomment following line
			//e.DataObject.SetHasFileData();

		}

		// The GetIconFileAndIndex method is called to retrieve information about the icon 
		// for the item.
		public override void GetIconFileAndIndex(GetIconFileAndIndexEventArgs e)
		{
			// Use the same icon as that shown for the folder
			e.IconExtractMode = IconExtractMode.SystemImageListIndexFromPath;
			e.IconFile = fullPath;
		}

		public override string ToString()
		{
			return fullPath;
		}

		// The OnChangeName method is called when the item has been renamed in Windows Explorer.
		// This method should return true of the renaming was successfully applied.
		public override bool OnChangeName(ChangeNameEventArgs e)
		{
			try
			{
				string parentFolder = Path.GetDirectoryName(fullPath);
				string newName = Path.Combine(parentFolder, e.NewName);
				Directory.Move(fullPath, newName);
				fullPath = newName;
				name = e.NewName; // store new name
				return true; // success!
			}
			catch
			{
				return false; // failure
			}
		}


		// The OnDelete method is called when the 'Delete' key is pressed 
		public override bool OnDelete(ExecuteMenuitemsEventArgs e)
		{
			// Delete every file/folder for which the delete command was executed.
			foreach (NSEItem item in e.Children)
			{
				if (item is FileItem)
				{
					FileItem c = item as FileItem;
					if(MessageBox.Show("Delete the file : '" + c.fullPath + "'?","Confirm Delete",MessageBoxButtons.YesNo)==DialogResult.Yes)
					{
						File.Delete(c.fullPath);
						// Remove from the Windows Explorer view
						c.Delete();
					}

				}
				else if (item is FolderItem)
				{
					FolderItem c = item as FolderItem;
					if(MessageBox.Show("Delete the folder : '" + c.fullPath + "'?","Confirm Delete",MessageBoxButtons.YesNo)==DialogResult.Yes)
					{
						Directory.Delete(c.fullPath, true);
						// Remove from the Windows Explorer view
						c.Delete();
					}
				}
			}

			return true;
		}

		// The IsValid is called to verify whether the item is valid or not. Typically, 
		// your implementation should check with the actual underlying data that this 
		// item represents and return true or false based on the status of the data.
		public override bool IsValid()
		{
			return Directory.Exists(fullPath);
		}

		static ShellColumn[] columns;

		static FolderItem()
		{
			ShellColumn colName = new ShellColumn("Name");
			colName.FormatIdentifier = new Guid("b725f130-47ef-101a-a5f1-02608c9eebac");
			colName.PropertyIdentifier = 10;

			ShellColumn colSize = new ShellColumn("Size");
			colSize.FormatIdentifier = new Guid("b725f130-47ef-101a-a5f1-02608c9eebac");
			colSize.PropertyIdentifier = 12;

			ShellColumn colType = new ShellColumn("Type");
			colType.FormatIdentifier = new Guid("b725f130-47ef-101a-a5f1-02608c9eebac");
			colType.PropertyIdentifier = 4;

			ShellColumn colDateModifed = new ShellColumn("Date Modified");
			colDateModifed.FormatIdentifier = new Guid("b725f130-47ef-101a-a5f1-02608c9eebac");
			colDateModifed.PropertyIdentifier = 14;

			columns = new ShellColumn[] { colName, colSize, colType, colDateModifed };

		}

		public override int[] GetXPTaskPaneColumnIndices()
		{
			// Use all column values except 'Size'
			return new int[] { 0,2,3 };
		}
        public override int[] GetTileViewColumnIndices()
        {
			// Use all column values except 'Size'
			return new int[] { 2, 3 };
        }

        public override int[] GetPreviewDetailsColumnIndices()
        {
			// Use all column values except 'Size'
			return new int[] { 0, 2, 3 };
        }

		// The GetAttributes method is called to retrieve the attributes of the item 
		// in the namespace extension. Folder items should include 
		// NSEItemAttributes.Folder in the return value. Other attributes should be 
		// returned if the item supports the corresponding functionality. 
		public override NSEItemAttributes GetAttributes(NSEItemAttributes attributes)
		{

			NSEItemAttributes att = NSEItemAttributes.Folder
				| NSEItemAttributes.FileSystem
				| NSEItemAttributes.FileSysAncestor
				| NSEItemAttributes.Browsable;

			// Only determine if the folder has sub-folders if asked for this attribute
			if ((attributes & NSEItemAttributes.HasSubFolder) != 0)
			{
				if (Directory.GetDirectories(fullPath).Length > 0)
					att |= NSEItemAttributes.HasSubFolder;
			}

			att = att
				| NSEItemAttributes.CanRename
				| NSEItemAttributes.CanDelete
				| NSEItemAttributes.CanLink
				| NSEItemAttributes.CanCopy
				| NSEItemAttributes.CanMove
				| NSEItemAttributes.DropTarget
				;

			return att;
		}

		public override ShellColumn[] GetColumns()
		{
			return columns;
		}

		public override string GetInfoTip()
		{
			return fullPath;
		}

        /*
		// The GetColumnValue method is deprecated. Use the GetColumnValueEx instead (see below).
		public override object GetColumnValue(ShellColumn column)
		{
			if (column.Index == 0) // name
			{
				return name;
			}

			if(column.Index==2) // type
			{
				return Utils.GetFileType(fullPath);
			}

			if (column.Index == 3) // Date Modified
			{
				DateTime dt = Directory.GetLastWriteTime(fullPath);
				return dt;
			}
			return null;
		}
        */

        // The GetColumnValueEx method is called to retrieve the value to be displayed for the
        // item in Details/Report mode for the specified column.
        public override bool GetColumnValueEx(ref VARIANT value, ShellColumn column)
        {
            if (column.Index == 0) // Name
            {
                value.SetValue(name);
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
				if (value.vt == VarType.VT_BSTR) // Value is asked in string format(on XP and before)
					value.SetValue(dt.ToLocalTime().ToString());
				else // Value is being asked as VARIANT 
					value.SetValue(dt,true); 
				return true; 
			}
            return false;
        }

		// The GetChildFromDisplayName method is called to retrieve the item from 
		// its display name. If the e.AssumeChildExists parameter is true, the child with the 
		// specified display name should be assumed to in existence and an item representing 
		// that display name should be returned.
		public override NSEItem GetChildFromDisplayName(GetChildFromDisplayNameEventArgs e)
		{
			if(e.AssumeChildExists)
			{
				return new FileItem(this.fullPath, e.DisplayName);
			}
			else
			{
				// Return only if file truly exists
				if(File.Exists(Path.Combine(this.fullPath,e.DisplayName)))
				{
					return new FileItem(this.fullPath, e.DisplayName);
				}
				else if (Directory.Exists(Path.Combine(this.fullPath, e.DisplayName)))
				{
					return new FolderItem(this.fullPath, e.DisplayName);
				}
			}
			return null;
		}

		// The Serialize method is called to persist immutable data about the item. 
		// A typical implementation should first store the type of this item and 
		// then information specific to this type of item. 
		public override void Serialize(BinaryWriter writer)
		{
			// Write version number of the serialized data. This is necessary as Windows may cache 
			// old data and present it back to us resulting in mismatch.
			writer.Write((byte)1); // Version=1, change if data format changes

			writer.Write((byte)1); // store type of item = 1 (folder)
			// Store name of the folder
			writer.Write(name);
		}

		// Every folder class should override the GetChildren method and return its children.
		// Children should first be collected in an array/collection/arraylist or any 
		// object which implements IEnumerable and then returned as the return value
		public override IEnumerable GetChildren(GetChildrenEventArgs e)
		{
			ArrayList arr = new ArrayList(20);

			// Add folders if asked for
			if ((e.ChildrenType & ChildrenType.Folders) != 0)
			{
				string[] dirs = Directory.GetDirectories(fullPath);
				foreach (string s in dirs)
				{
					arr.Add(new FolderItem(s));
				}
			}

			// Add files if asked for
			if ((e.ChildrenType & ChildrenType.NonFolders) != 0)
			{
				string[] files = Directory.GetFiles(fullPath);
				foreach (string s in files)
				{
					arr.Add(new FileItem(s));
				}
			}
			return arr;
		}

		// Every folder class is responsible for creating its child item from data saved 
		// by the child about itself. A typical implementation first loads a numeric 
		//identifier which identifies which type of child item is described by the remaining data. 
		public override NSEItem DeserializeChild(BinaryReader reader)
		{
			// Read version number
			byte version = reader.ReadByte();
			if(version!=1) // Return null if mismatch(old data)
				return null;

			// Read item type
			byte folder = reader.ReadByte();
			// read the name
			string s = reader.ReadString();
			// make full path
			string temp = Path.Combine(fullPath, s);

			// if the corresponding file exists, return the item representing it
			if (folder==0)
			{
				return new FileItem(temp);
			}
				// else return the folder item if the folder exists
			else if (folder==1)
			{
				return new FolderItem(temp);
			}

			return null;
		}

		// The GetDisplayName method is called to retrieve the display name of the item.
		public override string GetDisplayName()
		{
			return name;
		}

		// The CompareTo method is called to perform a comparison of the item with the
		// specified item with respect to the specified column. Return a number less 
		// than zero if this item should come before otherItem, greater than zero if 
		// this item should come after otherItem, and zero if the two items are equal.
		public override int CompareTo(NSEItem item2, ShellColumn column)
		{
			// folders should come before files
			if (item2 is FileItem)
				return -1;

			FolderItem dir2 = item2 as FolderItem;

			if (column.Index == 0) // name
			{
				return name.CompareTo(dir2.name);
			}

			if (column.Index == 3) // date modified
			{
				DateTime dt1 = Directory.GetLastWriteTime(fullPath);
				DateTime dt2 = Directory.GetLastWriteTime(dir2.fullPath);

				return dt1.CompareTo(dt2);
			}

			return 0;
		}

		// The GetFileDescriptor method is called to retrieve file-related information
		// about the item.
		public override FileDescriptor GetFileDescriptor()
		{
			DirectoryInfo fi = new DirectoryInfo(fullPath);
			FileDescriptor fd = new FileDescriptor();
			fd.FileName = name;
			fd.Attributes = fi.Attributes;
			fd.CreationTime = fi.CreationTime;
			fd.LastAccessTime = fi.LastAccessTime;
			fd.LastWriteTime = fi.LastWriteTime;
			return fd;
		}

		public override void OnExternalDrop( NSEDragEventArgs e)
		{
			if(e.Data.ShouldDeleteSource()&& e.Data.DidValidDrop())
			{
				// Delete every file/folder for which the delete command was executed.
				foreach (NSEItem item in e.Data.Children)
				{
					if (item is FileItem)
					{
						FileItem c = item as FileItem;
						File.Delete(c.fullPath);
						// Remove from the Windows Explorer view
						c.Delete();						
					}
					else if (item is FolderItem)
					{
						FolderItem c = item as FolderItem;
						Directory.Delete(c.fullPath, true);
						// Remove from the Windows Explorer view
						c.Delete();						
					}
				}
			}
			this.RefreshView();
		}


		// Called when a drag-drop operation moves over the item.
		public override void DragOver(NSEDragEventArgs e)
		{
			if ((e.KeyState & KeyStates.CtrlKeyDown) != 0 && (e.KeyState & KeyStates.ShiftKeyDown) != 0)
				e.Effect = DragDropEffects.Link;
			else if ((e.KeyState & KeyStates.ShiftKeyDown) != 0)
				e.Effect = DragDropEffects.Move;
			else
				e.Effect = DragDropEffects.Copy;
		}

		// Called when a drag-drop operation moves over the item for the first time.
		public override void DragEnter(NSEDragEventArgs e)
		{
			if ((e.KeyState & KeyStates.CtrlKeyDown) != 0 && (e.KeyState & KeyStates.ShiftKeyDown) != 0)
				e.Effect = DragDropEffects.Link;
			else if ((e.KeyState & KeyStates.ShiftKeyDown) != 0)
				e.Effect = DragDropEffects.Move;
			else
				e.Effect = DragDropEffects.Copy;
		}

		// Copies a directory recirsively
		public static void DirectoryCopy(String src, String dest)
		{
			Directory.CreateDirectory(dest);
			DirectoryInfo di = new DirectoryInfo(src);
			foreach (FileSystemInfo fsi in di.GetFileSystemInfos())
			{
				String destName = Path.Combine(dest, fsi.Name);

				if (fsi is FileInfo)
					File.Copy(fsi.FullName, destName);
				else
				{
					Directory.CreateDirectory(destName);
					DirectoryCopy(fsi.FullName, destName);
				}
			}
		}

		// Called when a non-folder item is double-clicked
		public override bool OnOpen(ExecuteMenuitemsEventArgs e)
		{
			if (e.Children.Length == 1)
			{
				FileItem fi = e.Children[0] as FileItem;
				if (fi != null)
				{
					ProcessStartInfo pi = new ProcessStartInfo(fi.fullPath);
					pi.UseShellExecute = true;
					pi.Verb = "open";
					System.Diagnostics.Process.Start(pi);
				}
			}

			return true;

		}


		// Called when a drop occurs over the item.
		public override void DragDrop(NSEDragEventArgs e)
		{
			// If file drop data is present, do the copy/move
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
					if (files != null)
					{
						foreach (string file in files)
						{
							string fileName = Path.GetFileName(file);
							string destPath = Path.Combine(fullPath, fileName);
							if((File.GetAttributes(file) & FileAttributes.Directory)!=0)
							{
								try
								{
									DirectoryCopy(file, destPath);
								}
								catch
								{
								}
							}
							else
							{
								try
								{
									File.Copy(file,destPath);
								}
								catch
								{
								}
							}
							e.Data.PerformedDropEffect= e.Effect;
							if(e.Data.PreferredDropEffect==DragDropEffects.Move)
								e.Data.PasteSucceded = e.Effect;
							this.RefreshView();
						}
					}
			}
		}
	}
}
