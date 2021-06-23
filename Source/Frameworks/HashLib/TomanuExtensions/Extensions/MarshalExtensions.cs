// ReSharper disable all

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace TomanuExtensions
{
    [DebuggerStepThrough]
    public static class MarshalExtensions
    {
        public static byte[] StructureToArray<T>(T a_struct) where T : struct
        {
            var len = Marshal.SizeOf(typeof(T));
            var arr = new byte[len];
            var ptr = Marshal.AllocHGlobal(len);
            Marshal.StructureToPtr(a_struct, ptr, true);
            Marshal.Copy(ptr, arr, 0, len);
            Marshal.FreeHGlobal(ptr);
            return arr;
        }

        public static void ArrayToStruct<T>(byte[] a_bytes, T a_struct) where T : struct
        {
            var size = Marshal.SizeOf(typeof(T));
            var ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(a_struct, ptr, true);
            Marshal.Copy(a_bytes, 0, ptr, size);
            var _ = Marshal.PtrToStructure(ptr, typeof(T));
            Marshal.FreeHGlobal(ptr);
        }

        public static byte[] StructurePtrToArray<T>(IntPtr a_struct) where T : struct
        {
            var len = Marshal.SizeOf(typeof(T));
            var arr = new byte[len];
            Marshal.Copy(a_struct, arr, 0, len);
            return arr;
        }
    }
}
