using System.Runtime.InteropServices;

namespace Hardcodet.Wpf.TaskbarNotification.Interop
{
    /// <summary>
    /// Win API struct providing coordinates for a single point.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Point
    {
        /// <summary>
        /// X Coordinate
        /// </summary>
        public int X;
        /// <summary>
        /// Y Coordinate
        /// </summary>
        public int Y;
    }
}