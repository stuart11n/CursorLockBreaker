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
        // 1. Get our own Process ID to make sure we don't target ourselves
        uint myId = (uint)Process.GetCurrentProcess().Id;

        // 2. Find the Flight Sim process
        Process[] processes = Process.GetProcessesByName("FlightSimulator2024");
        if (processes.Length == 0) return;

        Process fsProc = processes[0];
        IntPtr fsHwnd = fsProc.MainWindowHandle;

        // 3. Get the ID of the currently focused window
        IntPtr activeHwnd = GetForegroundWindow();
        GetWindowThreadProcessId(activeHwnd, out uint activeProcId);

        // --- Logic Fix: If WE are the focused window, the sim can't be. ---
        // This handles the split-second where the .exe starts up.
        if (activeProcId == myId)
        {
            // Optional: You could implement a "Find Previous Window" logic here, 
            // but usually, just exiting or a tiny delay is enough.
            return;
        }

        if (activeProcId == fsProc.Id)
        {
            // Sim is in focus -> Send to back
            SetWindowPos(fsHwnd, HWND_BOTTOM, 0, 0, 0, 0, TOPMOST_FLAGS);
        }
        else
        {
            // Sim is not in focus -> Bring to front
            SetForegroundWindow(fsHwnd);
        }
    }
}