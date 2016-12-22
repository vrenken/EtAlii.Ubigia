using System;
using System.Runtime.InteropServices;

namespace Hardcodet.Wpf.TaskbarNotification.Interop
{
    /// <summary>
    /// Callback delegate which is used by the Windows API to
    /// submit window messages.
    /// </summary>
    public delegate long WindowProcedureHandler(IntPtr hwnd, uint uMsg, uint wparam, uint lparam);


    /// <summary>
    /// Win API WNDCLASS struct - represents a single window.
    /// Used to receive window messages.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct WindowClass
    {
        /// <summary>
        /// style
        /// </summary>
        public uint style;
        /// <summary>
        /// lpfnWndProc
        /// </summary>
        public WindowProcedureHandler lpfnWndProc;
        /// <summary>
        /// cbClsExtra
        /// </summary>
        public int cbClsExtra;
        /// <summary>
        /// cbWndExtra
        /// </summary>
        public int cbWndExtra;
        /// <summary>
        /// hInstance
        /// </summary>
        public IntPtr hInstance;
        /// <summary>
        /// hIcon
        /// </summary>
        public IntPtr hIcon;
        /// <summary>
        /// hCursor
        /// </summary>
        public IntPtr hCursor;
        /// <summary>
        /// hbrBackground
        /// </summary>
        public IntPtr hbrBackground;
        /// <summary>
        /// lpszMenuName
        /// </summary>
        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpszMenuName;
        /// <summary>
        /// lpszClassName
        /// </summary>
        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpszClassName;
    }
}