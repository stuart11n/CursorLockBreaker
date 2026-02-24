using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

class Program
{
    [DllImport("user32.dll")]
    static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

    [DllImport("user32.dll")]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll")]
    static extern bool SetForegroundWindow(IntPtr hWnd);

    static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
    const uint SWP_NOSIZE = 0x0001;
    const uint SWP_NOMOVE = 0x0002;
    const uint TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;

    static void Main()
    {
        uint myId = (uint)Process.GetCurrentProcess().Id;

        Process[] processes = Process.GetProcessesByName("FlightSimulator2024");

        if (processes.Length == 0) return;

        Process fsProc = processes[0];
        IntPtr fsHwnd = fsProc.MainWindowHandle;

        IntPtr activeHwnd = GetForegroundWindow();
        GetWindowThreadProcessId(activeHwnd, out uint activeProcId);

        if (activeProcId == myId)
        {
            return;
        }

        if (activeProcId == fsProc.Id)
        {
            SetWindowPos(fsHwnd, HWND_BOTTOM, 0, 0, 0, 0, TOPMOST_FLAGS);
        }
        else
        {
            SetForegroundWindow(fsHwnd);
        }
    }
}