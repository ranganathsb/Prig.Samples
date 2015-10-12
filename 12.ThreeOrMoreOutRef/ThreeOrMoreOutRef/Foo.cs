using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace ThreeOrMoreOutRef
{
    public class Foo
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetCurrentThread();

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool GetThreadTimes(IntPtr hThread, out long lpCreationTime, out long lpExitTime, out long lpKernelTime, out long lpUserTime);

        public string FormatCurrentThreadTimes()
        {
            var creationTime = default(long);
            var exitTime = default(long);
            var kernelTime = default(long);
            var userTime = default(long);
            if (!GetThreadTimes(GetCurrentThread(), out creationTime, out exitTime, out kernelTime, out userTime))
                throw new Win32Exception(Marshal.GetLastWin32Error());

            return string.Format("Creation Time: {0}, Exit Time: {1}, Kernel Time: {2}, User Time: {3}", creationTime, exitTime, kernelTime, userTime);
        }
    }
}
