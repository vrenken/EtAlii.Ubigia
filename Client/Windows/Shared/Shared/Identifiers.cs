using System;

namespace EtAlii.Ubigia.Windows.Shared
{
    public static class Identifiers 
    {
        // HKEY_CLASSES_ROOT\CLSID\{D90A1734-1783-461B-A0D5-9187EDFE36B3}
        // HKEY_CLASSES_ROOT\EtAlii.Ubigia.Client.Windows.ShellExtension\CLSID
        // HKEY_LOCAL_MACHINE\SOFTWARE\Classes\CLSID\{D90A1734-1783-461B-A0D5-9187EDFE36B3}
        // HKEY_LOCAL_MACHINE\SOFTWARE\Classes\EtAlii.Ubigia.Client.Windows.ShellExtension\CLSID

        public static readonly Guid ShellExtensionRegistration = new Guid(ShellExtensionRegistrationString);
        public const string ShellExtensionRegistrationString = "D90A1734-1783-461B-A0D5-9187EDFE36B3";
        public const string ShellExtensionExperimentRegistrationString = "086397C5-F726-4557-8A14-2C48F7D1B659";

        public const string ProgramRegistrationString = "EtAlii.Ubigia.Client.Windows.Registration";


        public const string RootFolderItemString = "CAF99A98-DE68-4B03-A0D8-0C1974CDCF23";
        public const string FolderItemString = "9A807612-6AFC-4254-8B75-816BA411D4A7";
        public const string FileItemString = "8F89A372-C11F-477F-826E-F39B2E9E48E3";
    }
}
