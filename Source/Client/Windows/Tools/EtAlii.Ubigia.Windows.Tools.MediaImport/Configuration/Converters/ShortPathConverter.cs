namespace EtAlii.Ubigia.Windows.Tools.MediaImport
{
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Data;

    internal class ShortPathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string valueString)) return value;

            var length = 0;
            switch (parameter)
            {
                case string parameterString: length = int.Parse(parameterString); break;
                case int parameterInt: length = parameterInt; break;
            }
            value = CompactPath(valueString, length);
            
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
        private static extern bool PathCompactPathEx([Out] StringBuilder pszOut, string szPath, int cchMax, int dwFlags);

        public static string CompactPath(string longPathName, int wantedLength)
        {
            // NOTE: You need to create the builder with the 
            //       required capacity before calling function.
            // See http://msdn.microsoft.com/en-us/library/aa446536.aspx
            var sb = new StringBuilder(wantedLength + 1);
            PathCompactPathEx(sb, longPathName, wantedLength + 1, 0);
            return sb.ToString();
        }

    }
}