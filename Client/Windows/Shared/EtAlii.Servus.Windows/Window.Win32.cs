namespace EtAlii.Servus.Windows
{
    using System;
    using System.Windows;
    using System.Windows.Interop;
    using System.Windows.Media;
    using IWin32Window = System.Windows.Forms.IWin32Window;

    public static class Win32Window
    {
        public static IWin32Window GetWin32Window(this Visual visual)
        {
            var source = PresentationSource.FromVisual(visual) as HwndSource;
            IWin32Window win = new OldWindow(source.Handle);
            return win;
        }

        private class OldWindow : IWin32Window
        {
            IntPtr IWin32Window.Handle { get { return _handle; } }
            private readonly IntPtr _handle;

            public OldWindow(IntPtr handle)
            {
                _handle = handle;
            }
        }
    }
}
