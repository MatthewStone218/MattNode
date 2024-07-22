using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Windows.Forms;

public class GlobalHooks
{
    public static bool MouseWheelUp = false;
    public static bool MouseWheelDown = false;
    public static bool MouseLeftDown = false;
    public static bool KeyboardSpaceDown = false;
    public static bool KeyboardCtrlDown = false;

    private const int WH_KEYBOARD_LL = 13;
    private const int WH_MOUSE_LL = 14;
    private const int WM_MOUSEWHEEL = 0x020A;
    private const int WM_LBUTTONDOWN = 0x0201;
    private const int WM_LBUTTONUP = 0x0202;
    private const int WM_KEYDOWN = 0x0100;
    private const int WM_KEYUP = 0x0101;
    private const int VK_SPACE = 0x20;
    private const int VK_CONTROL = 0xA2;

    private static IntPtr mouseHookID = IntPtr.Zero;
    private static IntPtr keyboardHookID = IntPtr.Zero;
    private static LowLevelMouseProc mouseProc;
    private static LowLevelKeyboardProc keyboardProc;

    private static List<Action<int>> actions = new List<Action<int>>();

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx(int idHook, Delegate lpfn, IntPtr hMod, uint dwThreadId);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool UnhookWindowsHookEx(IntPtr hhk);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr GetModuleHandle(string lpModuleName);

    public static void Start()
    {
        mouseProc = MouseHookCallback;
        keyboardProc = KeyboardHookCallback;

        using (Process process = Process.GetCurrentProcess())
        using (ProcessModule module = process.MainModule)
        {
            IntPtr moduleHandle = GetModuleHandle(module.ModuleName);
            mouseHookID = SetWindowsHookEx(WH_MOUSE_LL, mouseProc, moduleHandle, 0);
            keyboardHookID = SetWindowsHookEx(WH_KEYBOARD_LL, keyboardProc, moduleHandle, 0);
        }
    }

    public static void Stop()
    {
        UnhookWindowsHookEx(mouseHookID);
        UnhookWindowsHookEx(keyboardHookID);
    }

    public static void AddCallbackMouseWheel(Action<int> action)
    {
        actions.Add(action);
    }

    private static IntPtr MouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode >= 0)
        {
            if (wParam == (IntPtr)WM_MOUSEWHEEL)
            {
                MSLLHOOKSTRUCT hookStruct = Marshal.PtrToStructure<MSLLHOOKSTRUCT>(lParam);
                for(int i = 0; i < actions.Count; i++)
                {
                    actions[i](hookStruct.mouseData);
                }
            }
            else if((int)wParam == WM_LBUTTONDOWN)
            {
                MouseLeftDown = true;
            }
            else if ((int)wParam == WM_LBUTTONUP)
            {
                MouseLeftDown = false;
            }
        }

        return CallNextHookEx(mouseHookID, nCode, wParam, lParam);
    }

    private static IntPtr KeyboardHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode >= 0)
        {
            int vkCode = Marshal.ReadInt32(lParam);
            if (wParam == (IntPtr)WM_KEYDOWN && vkCode == VK_SPACE)
            {
                KeyboardSpaceDown = true;
            }
            else if (wParam == (IntPtr)WM_KEYUP && vkCode == VK_SPACE)
            {
                KeyboardSpaceDown = false;
            }
            else if (wParam == (IntPtr)WM_KEYDOWN && vkCode == VK_CONTROL)
            {
                KeyboardCtrlDown = true;
            }
            else if (wParam == (IntPtr)WM_KEYUP && vkCode == VK_CONTROL)
            {
                KeyboardCtrlDown = false;
            }
        }
        return CallNextHookEx(keyboardHookID, nCode, wParam, lParam);
    }

    private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);
    private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

    [StructLayout(LayoutKind.Sequential)]
    private struct MSLLHOOKSTRUCT
    {
        public POINT pt;
        public int mouseData;
        public int flags;
        public int time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct POINT
    {
        public int x;
        public int y;
    }
}
