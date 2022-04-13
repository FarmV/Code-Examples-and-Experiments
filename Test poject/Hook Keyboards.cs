using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Win32.SafeHandles;

using System.Windows.Forms;

using static System.Net.Mime.MediaTypeNames;
using static Test_poject.Hook_Keyboards;
using static Test_poject.Hook_Keyboards.KeyboardHook;
using System.Diagnostics.Metrics;

namespace Test_poject
{
    internal class Hook_Keyboards
    {
        public class KeyboardHook
        {
            /// <summary>
            /// Virtual Keys
            /// </summary>
            public enum VKeys
            {
                // Losely based on http://www.pinvoke.net/default.aspx/Enums/VK.html

                LBUTTON = 0x01,     // Left mouse button
                RBUTTON = 0x02,     // Right mouse button
                CANCEL = 0x03,      // Control-break processing
                MBUTTON = 0x04,     // Middle mouse button (three-button mouse)
                XBUTTON1 = 0x05,    // Windows 2000/XP: X1 mouse button
                XBUTTON2 = 0x06,    // Windows 2000/XP: X2 mouse button
                                    //                  0x07   // Undefined
                BACK = 0x08,        // BACKSPACE key
                TAB = 0x09,         // TAB key
                                    //                  0x0A-0x0B,  // Reserved
                CLEAR = 0x0C,       // CLEAR key
                RETURN = 0x0D,      // ENTER key
                                    //                  0x0E-0x0F, // Undefined
                SHIFT = 0x10,       // SHIFT key
                CONTROL = 0x11,     // CTRL key
                MENU = 0x12,        // ALT key
                PAUSE = 0x13,       // PAUSE key
                CAPITAL = 0x14,     // CAPS LOCK key
                KANA = 0x15,        // Input Method Editor (IME) Kana mode
                HANGUL = 0x15,      // IME Hangul mode
                                    //                  0x16,  // Undefined
                JUNJA = 0x17,       // IME Junja mode
                FINAL = 0x18,       // IME final mode
                HANJA = 0x19,       // IME Hanja mode
                KANJI = 0x19,       // IME Kanji mode
                                    //                  0x1A,  // Undefined
                ESCAPE = 0x1B,      // ESC key
                CONVERT = 0x1C,     // IME convert
                NONCONVERT = 0x1D,  // IME nonconvert
                ACCEPT = 0x1E,      // IME accept
                MODECHANGE = 0x1F,  // IME mode change request
                SPACE = 0x20,       // SPACEBAR
                PRIOR = 0x21,       // PAGE UP key
                NEXT = 0x22,        // PAGE DOWN key
                END = 0x23,         // END key
                HOME = 0x24,        // HOME key
                LEFT = 0x25,        // LEFT ARROW key
                UP = 0x26,          // UP ARROW key
                RIGHT = 0x27,       // RIGHT ARROW key
                DOWN = 0x28,        // DOWN ARROW key
                SELECT = 0x29,      // SELECT key
                PRINT = 0x2A,       // PRINT key
                EXECUTE = 0x2B,     // EXECUTE key
                SNAPSHOT = 0x2C,    // PRINT SCREEN key
                INSERT = 0x2D,      // INS key
                DELETE = 0x2E,      // DEL key
                HELP = 0x2F,        // HELP key
                KEY_0 = 0x30,       // 0 key
                KEY_1 = 0x31,       // 1 key
                KEY_2 = 0x32,       // 2 key
                KEY_3 = 0x33,       // 3 key
                KEY_4 = 0x34,       // 4 key
                KEY_5 = 0x35,       // 5 key
                KEY_6 = 0x36,       // 6 key
                KEY_7 = 0x37,       // 7 key
                KEY_8 = 0x38,       // 8 key
                KEY_9 = 0x39,       // 9 key
                                    //                  0x3A-0x40, // Undefined
                KEY_A = 0x41,       // A key
                KEY_B = 0x42,       // B key
                KEY_C = 0x43,       // C key
                KEY_D = 0x44,       // D key
                KEY_E = 0x45,       // E key
                KEY_F = 0x46,       // F key
                KEY_G = 0x47,       // G key
                KEY_H = 0x48,       // H key
                KEY_I = 0x49,       // I key
                KEY_J = 0x4A,       // J key
                KEY_K = 0x4B,       // K key
                KEY_L = 0x4C,       // L key
                KEY_M = 0x4D,       // M key
                KEY_N = 0x4E,       // N key
                KEY_O = 0x4F,       // O key
                KEY_P = 0x50,       // P key
                KEY_Q = 0x51,       // Q key
                KEY_R = 0x52,       // R key
                KEY_S = 0x53,       // S key
                KEY_T = 0x54,       // T key
                KEY_U = 0x55,       // U key
                KEY_V = 0x56,       // V key
                KEY_W = 0x57,       // W key
                KEY_X = 0x58,       // X key
                KEY_Y = 0x59,       // Y key
                KEY_Z = 0x5A,       // Z key
                LWIN = 0x5B,        // Left Windows key (Microsoft Natural keyboard)
                RWIN = 0x5C,        // Right Windows key (Natural keyboard)
                APPS = 0x5D,        // Applications key (Natural keyboard)
                                    //                  0x5E, // Reserved
                SLEEP = 0x5F,       // Computer Sleep key
                NUMPAD0 = 0x60,     // Numeric keypad 0 key
                NUMPAD1 = 0x61,     // Numeric keypad 1 key
                NUMPAD2 = 0x62,     // Numeric keypad 2 key
                NUMPAD3 = 0x63,     // Numeric keypad 3 key
                NUMPAD4 = 0x64,     // Numeric keypad 4 key
                NUMPAD5 = 0x65,     // Numeric keypad 5 key
                NUMPAD6 = 0x66,     // Numeric keypad 6 key
                NUMPAD7 = 0x67,     // Numeric keypad 7 key
                NUMPAD8 = 0x68,     // Numeric keypad 8 key
                NUMPAD9 = 0x69,     // Numeric keypad 9 key
                MULTIPLY = 0x6A,    // Multiply key
                ADD = 0x6B,         // Add key
                SEPARATOR = 0x6C,   // Separator key
                SUBTRACT = 0x6D,    // Subtract key
                DECIMAL = 0x6E,     // Decimal key
                DIVIDE = 0x6F,      // Divide key
                F1 = 0x70,          // F1 key
                F2 = 0x71,          // F2 key
                F3 = 0x72,          // F3 key
                F4 = 0x73,          // F4 key
                F5 = 0x74,          // F5 key
                F6 = 0x75,          // F6 key
                F7 = 0x76,          // F7 key
                F8 = 0x77,          // F8 key
                F9 = 0x78,          // F9 key
                F10 = 0x79,         // F10 key
                F11 = 0x7A,         // F11 key
                F12 = 0x7B,         // F12 key
                F13 = 0x7C,         // F13 key
                F14 = 0x7D,         // F14 key
                F15 = 0x7E,         // F15 key
                F16 = 0x7F,         // F16 key
                F17 = 0x80,         // F17 key  
                F18 = 0x81,         // F18 key  
                F19 = 0x82,         // F19 key  
                F20 = 0x83,         // F20 key  
                F21 = 0x84,         // F21 key  
                F22 = 0x85,         // F22 key, (PPC only) Key used to lock device.
                F23 = 0x86,         // F23 key  
                F24 = 0x87,         // F24 key  
                                    //                  0x88-0X8F,  // Unassigned
                NUMLOCK = 0x90,     // NUM LOCK key
                SCROLL = 0x91,      // SCROLL LOCK key
                                    //                  0x92-0x96,  // OEM specific
                                    //                  0x97-0x9F,  // Unassigned
                LSHIFT = 0xA0,      // Left SHIFT key
                RSHIFT = 0xA1,      // Right SHIFT key
                LCONTROL = 0xA2,    // Left CONTROL key
                RCONTROL = 0xA3,    // Right CONTROL key
                LMENU = 0xA4,       // Left MENU key
                RMENU = 0xA5,       // Right MENU key
                BROWSER_BACK = 0xA6,    // Windows 2000/XP: Browser Back key
                BROWSER_FORWARD = 0xA7, // Windows 2000/XP: Browser Forward key
                BROWSER_REFRESH = 0xA8, // Windows 2000/XP: Browser Refresh key
                BROWSER_STOP = 0xA9,    // Windows 2000/XP: Browser Stop key
                BROWSER_SEARCH = 0xAA,  // Windows 2000/XP: Browser Search key
                BROWSER_FAVORITES = 0xAB,  // Windows 2000/XP: Browser Favorites key
                BROWSER_HOME = 0xAC,    // Windows 2000/XP: Browser Start and Home key
                VOLUME_MUTE = 0xAD,     // Windows 2000/XP: Volume Mute key
                VOLUME_DOWN = 0xAE,     // Windows 2000/XP: Volume Down key
                VOLUME_UP = 0xAF,  // Windows 2000/XP: Volume Up key
                MEDIA_NEXT_TRACK = 0xB0,// Windows 2000/XP: Next Track key
                MEDIA_PREV_TRACK = 0xB1,// Windows 2000/XP: Previous Track key
                MEDIA_STOP = 0xB2, // Windows 2000/XP: Stop Media key
                MEDIA_PLAY_PAUSE = 0xB3,// Windows 2000/XP: Play/Pause Media key
                LAUNCH_MAIL = 0xB4,     // Windows 2000/XP: Start Mail key
                LAUNCH_MEDIA_SELECT = 0xB5,  // Windows 2000/XP: Select Media key
                LAUNCH_APP1 = 0xB6,     // Windows 2000/XP: Start Application 1 key
                LAUNCH_APP2 = 0xB7,     // Windows 2000/XP: Start Application 2 key
                                        //                  0xB8-0xB9,  // Reserved
                OEM_1 = 0xBA,       // Used for miscellaneous characters; it can vary by keyboard.
                                    // Windows 2000/XP: For the US standard keyboard, the ';:' key
                OEM_PLUS = 0xBB,    // Windows 2000/XP: For any country/region, the '+' key
                OEM_COMMA = 0xBC,   // Windows 2000/XP: For any country/region, the ',' key
                OEM_MINUS = 0xBD,   // Windows 2000/XP: For any country/region, the '-' key
                OEM_PERIOD = 0xBE,  // Windows 2000/XP: For any country/region, the '.' key
                OEM_2 = 0xBF,       // Used for miscellaneous characters; it can vary by keyboard.
                                    // Windows 2000/XP: For the US standard keyboard, the '/?' key
                OEM_3 = 0xC0,       // Used for miscellaneous characters; it can vary by keyboard.
                                    // Windows 2000/XP: For the US standard keyboard, the '`~' key
                                    //                  0xC1-0xD7,  // Reserved
                                    //                  0xD8-0xDA,  // Unassigned
                OEM_4 = 0xDB,       // Used for miscellaneous characters; it can vary by keyboard.
                                    // Windows 2000/XP: For the US standard keyboard, the '[{' key
                OEM_5 = 0xDC,       // Used for miscellaneous characters; it can vary by keyboard.
                                    // Windows 2000/XP: For the US standard keyboard, the '\|' key
                OEM_6 = 0xDD,       // Used for miscellaneous characters; it can vary by keyboard.
                                    // Windows 2000/XP: For the US standard keyboard, the ']}' key
                OEM_7 = 0xDE,       // Used for miscellaneous characters; it can vary by keyboard.
                                    // Windows 2000/XP: For the US standard keyboard, the 'single-quote/double-quote' key
                OEM_8 = 0xDF,       // Used for miscellaneous characters; it can vary by keyboard.
                                    //                  0xE0,  // Reserved
                                    //                  0xE1,  // OEM specific
                OEM_102 = 0xE2,     // Windows 2000/XP: Either the angle bracket key or the backslash key on the RT 102-key keyboard
                                    //                  0xE3-E4,  // OEM specific
                PROCESSKEY = 0xE5,  // Windows 95/98/Me, Windows NT 4.0, Windows 2000/XP: IME PROCESS key
                                    //                  0xE6,  // OEM specific
                PACKET = 0xE7,      // Windows 2000/XP: Used to pass Unicode characters as if they were keystrokes. The VK_PACKET key is the low word of a 32-bit Virtual Key value used for non-keyboard input methods. For more information, see Remark in KEYBDINPUT, SendInput, WM_KEYDOWN, and WM_KEYUP
                                    //                  0xE8,  // Unassigned
                                    //                  0xE9-F5,  // OEM specific
                ATTN = 0xF6,        // Attn key
                CRSEL = 0xF7,       // CrSel key
                EXSEL = 0xF8,       // ExSel key
                EREOF = 0xF9,       // Erase EOF key
                PLAY = 0xFA,        // Play key
                ZOOM = 0xFB,        // Zoom key
                NONAME = 0xFC,      // Reserved
                PA1 = 0xFD,         // PA1 key
                OEM_CLEAR = 0xFE    // Clear key
            }

            /// <summary>
            /// Internal callback processing function
            /// </summary>
            private delegate IntPtr KeyboardHookHandler(int nCode, IntPtr wParam, IntPtr lParam);
            private KeyboardHookHandler hookHandler;

            /// <summary>
            /// Function that will be called when defined events occur
            /// </summary>
            /// <param name="key">VKeys</param>
            public delegate void KeyboardHookCallback(VKeys key);

            #region Events
            public event KeyboardHookCallback KeyDown;
            public event KeyboardHookCallback KeyUp;
            #endregion

            /// <summary>
            /// Hook ID
            /// </summary>
            private IntPtr hookID = IntPtr.Zero;

            /// <summary>
            /// Install low level keyboard hook
            /// </summary>
            public void Install()
            {
                hookHandler = HookFunc;
                hookID = SetHook(hookHandler);
            }

            /// <summary>
            /// Remove low level keyboard hook
            /// </summary>
            public void Uninstall()
            {
                UnhookWindowsHookEx(hookID);
            }

            /// <summary>
            /// Registers hook with Windows API
            /// </summary>
            /// <param name="proc">Callback function</param>
            /// <returns>Hook ID</returns>
            private IntPtr SetHook(KeyboardHookHandler proc)
            {
                using (ProcessModule module = Process.GetCurrentProcess().MainModule)
                    return SetWindowsHookEx(13, proc, GetModuleHandle(module.ModuleName), 0);
            }

            /// <summary>
            /// Default hook call, which analyses pressed keys
            /// </summary>
            private IntPtr HookFunc(int nCode, IntPtr wParam, IntPtr lParam)
            {

                if (nCode >= 0)
                {
                    int iwParam = wParam.ToInt32();

                    if (( iwParam == WM_KEYDOWN || iwParam == WM_SYSKEYDOWN ))
                        if (KeyDown != null)
                            KeyDown((VKeys)Marshal.ReadInt32(lParam));
                    if (( iwParam == WM_KEYUP || iwParam == WM_SYSKEYUP ))
                        if (KeyUp != null)
                            KeyUp((VKeys)Marshal.ReadInt32(lParam));
                }

                return CallNextHookEx(hookID, nCode, wParam, lParam);
            }

            /// <summary>
            /// Destructor. Unhook current hook
            /// </summary>
            ~KeyboardHook()
            {
                Uninstall();
            }

            /// <summary>
            /// Low-Level function declarations
            /// </summary>
            #region WinAPI
            private const int WM_KEYDOWN = 0x100;
            private const int WM_SYSKEYDOWN = 0x104;
            private const int WM_KEYUP = 0x101;
            private const int WM_SYSKEYUP = 0x105;

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            private static extern IntPtr SetWindowsHookEx(int idHook, KeyboardHookHandler lpfn, IntPtr hMod, uint dwThreadId);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool UnhookWindowsHookEx(IntPtr hhk);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

            [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            private static extern IntPtr GetModuleHandle(string lpModuleName);
            #endregion
        }


        static async Task Main()
        {


            MyLowlevlhook test1 = new MyLowlevlhook();
            test1.InstallHook();
            int Count = default;
            test1.KeyDown1 += (a) =>
            {
                Count++; Console.WriteLine(a);
            };
            //  await Task.Delay(10000);



            System.Windows.Forms.Application.Run();
        }
    }







    enum ViryalKey : byte
    {
        // Constant                Value               Description
        VK_LBUTTON = 0x01,         /* Left mouse button*/
        VK_RBUTTON = 0x02,         /* Right mouse button*/
        VK_CANCEL = 0x03,          /* Control-break processing*/
        VK_MBUTTON = 0x04,         /* Middle mouse button (three-button mouse)*/
        VK_XBUTTON1 = 0x05,        /* X1 mouse button*/
        VK_XBUTTON2 = 0x06,           /* X2 mouse button*/
       // 0x07,                     /* Undefined*/
        VK_BACK = 0x08,            /* BACKSPACE key*/
        VK_TAB = 0x09,             /* TAB key*/
        /* 0x0A-0B                 Reserved*/     
        VK_CLEAR = 0x0C,           /* CLEAR key*/
        VK_RETURN = 0x0D,          /* ENTER key*/
       //0x0E,                     /* = 0x0E-0F--Undefined*/
        VK_SHIFT = 0x10,           /* SHIFT key*/
        VK_CONTROL = 0x11,         /* CTRL key*/
        VK_MENU = 0x12,            /* ALT key*/
        VK_PAUSE = 0x13,           /* PAUSE key*/
        VK_CAPITAL = 0x14,         /* CAPS LOCK key*/
        VK_KANA = 0x15,            /* IME Kana mode*/
        VK_HANGUEL = 0x15,         /* IME Hanguel mode (maintained for compatibility; use VK_HANGUL)*/
        VK_HANGUL = 0x15,          /* IME Hangul mode*/
        VK_IME_ON = 0x16,          /* IME On*/
        VK_JUNJA = 0x17,           /* IME Junja mode*/
        VK_FINAL = 0x18,           /* IME final mode*/
        VK_HANJA = 0x19,           /* IME Hanja mode*/
        VK_KANJI = 0x19,           /* IME Kanji mode*/
        VK_IME_OFF = 0x1A,         /* IME Off*/
        VK_ESCAPE = 0x1B,          /* ESC key*/
        VK_CONVERT = 0x1C,         /* IME convert*/
        VK_NONCONVERT = 0x1D,          /* IME nonconvert*/
        VK_ACCEPT = 0x1E,          /* IME accept*/
        VK_MODECHANGE = 0x1F,          /* IME mode change request*/
        VK_SPACE = 0x20,           /* SPACEBAR*/
        VK_PRIOR = 0x21,           /* PAGE UP key*/
        VK_NEXT = 0x22,            /* PAGE DOWN key*/
        VK_END = 0x23,         /* END key*/
        VK_HOME = 0x24,            /* HOME key*/
        VK_LEFT = 0x25,            /* LEFT ARROW key*/
        VK_UP = 0x26,          /* UP ARROW key*/
        VK_RIGHT = 0x27,           /* RIGHT ARROW key*/
        VK_DOWN = 0x28,            /* DOWN ARROW key*/
        VK_SELECT = 0x29,          /* SELECT key*/
        VK_PRINT = 0x2A,           /* PRINT key*/
        VK_EXECUTE = 0x2B,         /* EXECUTE key*/
        VK_SNAPSHOT = 0x2C,            /* PRINT SCREEN key*/
        VK_INSERT = 0x2D,          /* INS key*/
        VK_DELETE = 0x2E,          /* DEL key*/
        VK_HELP = 0x2F,            /* HELP key*/
        KEY_0 = 0x30,          // 0 key
        KEY_1 = 0x31,          // 1 key
        KEY_2 = 0x32,          // 2 key
        KEY_3 = 0x33,          // 3 key
        KEY_4 = 0x34,          // 4 key
        KEY_5 = 0x35,          // 5 key
        KEY_6 = 0x36,          // 6 key
        KEY_7 = 0x37,          // 7 key
        KEY_8 = 0x38,          // 8 key
        KEY_9 = 0x39,          // 9 key
        //- 	                   = 0x3A-40 	       //Undefined
        KEY_A = 0x41,          /* A key*/
        KEY_B = 0x42,          /* B key*/
        KEY_C = 0x43,          /* C key*/
        KEY_D = 0x44,          /* D key*/
        KEY_E = 0x45,          /* E key*/
        KEY_F = 0x46,          /* F key*/
        KEY_G = 0x47,          /* G key*/
        KEY_H = 0x48,          /* H key*/
        KEY_I = 0x49,          /* I key*/
        KEY_J = 0x4A,          /* J key*/
        KEY_K = 0x4B,          /* K key*/
        KEY_L = 0x4C,          /* L key*/
        KEY_M = 0x4D,          /* M key*/
        KEY_N = 0x4E,          /* N key*/
        KEY_O = 0x4F,          /* O key*/
        KEY_P = 0x50,          /* P key*/
        KEY_Q = 0x51,          /* Q key*/
        KEY_R = 0x52,          /* R key*/
        KEY_S = 0x53,          /* S key*/
        KEY_T = 0x54,          /* T key*/
        KEY_U = 0x55,          /* U key*/
        KEY_V = 0x56,          /* V key*/
        KEY_W = 0x57,          /* W key*/
        KEY_X = 0x58,          /* X key*/
        KEY_Y = 0x59,          /* Y key*/
        KEY_Z = 0x5A,          /* Z key*/
        VK_LWIN = 0x5B,         /*Left Windows key (Natural keyboard)*/
        VK_RWIN = 0x5C,         /*Right Windows key (Natural keyboard)*/
        VK_APPS = 0x5D,         /*Applications key (Natural keyboard)*/
        /*-*/                 /*= 0x5E*/     /*Reserved*/
        VK_SLEEP = 0x5F,        /*Computer Sleep key*/
        VK_NUMPAD0 = 0x60,      /*Numeric keypad 0 key*/
        VK_NUMPAD1 = 0x61,      /*Numeric keypad 1 key*/
        VK_NUMPAD2 = 0x62,      /*Numeric keypad 2 key*/
        VK_NUMPAD3 = 0x63,      /*Numeric keypad 3 key*/
        VK_NUMPAD4 = 0x64,      /*Numeric keypad 4 key*/
        VK_NUMPAD5 = 0x65,      /*Numeric keypad 5 key*/
        VK_NUMPAD6 = 0x66,      /*Numeric keypad 6 key*/
        VK_NUMPAD7 = 0x67,      /*Numeric keypad 7 key*/
        VK_NUMPAD8 = 0x68,      /*Numeric keypad 8 key*/
        VK_NUMPAD9 = 0x69,      /*Numeric keypad 9 key*/
        VK_MULTIPLY = 0x6A,         /*Multiply key*/
        VK_ADD = 0x6B,      /*Add key*/
        VK_SEPARATOR = 0x6C,        /*Separator key*/
        VK_SUBTRACT = 0x6D,         /*Subtract key*/
        VK_DECIMAL = 0x6E,      /*Decimal key*/
        VK_DIVIDE = 0x6F,       /*Divide key*/
        VK_F1 = 0x70,       /*F1 key*/
        VK_F2 = 0x71,       /*F2 key*/
        VK_F3 = 0x72,       /*F3 key*/
        VK_F4 = 0x73,       /*F4 key*/
        VK_F5 = 0x74,       /*F5 key*/
        VK_F6 = 0x75,       /*F6 key*/
        VK_F7 = 0x76,       /*F7 key*/
        VK_F8 = 0x77,       /*F8 key*/
        VK_F9 = 0x78,       /*F9 key*/
        VK_F10 = 0x79,      /*F10 key*/
        VK_F11 = 0x7A,      /*F11 key*/
        VK_F12 = 0x7B,      /*F12 key*/
        VK_F13 = 0x7C,      /*F13 key*/
        VK_F14 = 0x7D,      /*F14 key*/
        VK_F15 = 0x7E,      /*F15 key*/
        VK_F16 = 0x7F,      /*F16 key*/
        VK_F17 = 0x80,      /*F17 key*/
        VK_F18 = 0x81,      /*F18 key*/
        VK_F19 = 0x82,      /*F19 key*/
        VK_F20 = 0x83,      /*F20 key*/
        VK_F21 = 0x84,      /*F21 key*/
        VK_F22 = 0x85,      /*F22 key*/
        VK_F23 = 0x86,      /*F23 key*/
        VK_F24 = 0x87,      /*F24 key*/
        /*-*/                 /*= 0x88-8F*/  /*Unassigned*/
        VK_NUMLOCK = 0x90,      /*NUM LOCK key*/
        VK_SCROLL = 0x91,   /*SCROLL LOCK key*/
        /* = 0x92-96*/    /*OEM specific*/
        /* = 0x97-9F*/    /*Unassigned*/
        VK_LSHIFT = 0xA0,       /*Left SHIFT key*/
        VK_RSHIFT = 0xA1,       /*Right SHIFT key*/
        VK_LCONTROL = 0xA2,     /*Left CONTROL key*/
        VK_RCONTROL = 0xA3,     /*Right CONTROL key*/
        VK_LMENU = 0xA4,    /*Left MENU key*/
        VK_RMENU = 0xA5,    /*Right MENU key*/
        VK_BROWSER_BACK = 0xA6,     /*Browser Back key*/
        VK_BROWSER_FORWARD = 0xA7,      /*Browser Forward key*/
        VK_BROWSER_REFRESH = 0xA8,      /*Browser Refresh key*/
        VK_BROWSER_STOP = 0xA9,     /*Browser Stop key*/
        VK_BROWSER_SEARCH = 0xAA,       /*Browser Search key*/
        VK_BROWSER_FAVORITES = 0xAB,    /*Browser Favorites key*/
        VK_BROWSER_HOME = 0xAC,     /*Browser Start and Home key*/
        VK_VOLUME_MUTE = 0xAD,      /*Volume Mute key*/
        VK_VOLUME_DOWN = 0xAE,      /*Volume Down key*/
        VK_VOLUME_UP = 0xAF,    /*Volume Up key*/
        VK_MEDIA_NEXT_TRACK = 0xB0,         /*Next Track key*/
        VK_MEDIA_PREV_TRACK = 0xB1,         /*Previous Track key*/
        VK_MEDIA_STOP = 0xB2,       /*Stop Media key*/
        VK_MEDIA_PLAY_PAUSE = 0xB3,         /*Play/Pause Media key*/
        VK_LAUNCH_MAIL = 0xB4,      /*Start Mail key*/
        VK_LAUNCH_MEDIA_SELECT = 0xB5,      /*Select Media key*/
        VK_LAUNCH_APP1 = 0xB6,      /*Start Application 1 key*/
        VK_LAUNCH_APP2 = 0xB7,       /*Start Application 2 key*/
        /*- */                              /* = 0xB8-B9*/    /*Reserved*/
        VK_OEM_1 = 0xBA,        /*Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the ';:' key*/
        VK_OEM_PLUS = 0xBB,         /*For any country/region, the '+' key*/
        VK_OEM_COMMA = 0xBC,        /*For any country/region, the ',' key*/
        VK_OEM_MINUS = 0xBD,        /*For any country/region, the '-' key*/
        VK_OEM_PERIOD = 0xBE,       /*For any country/region, the '.' key*/
        VK_OEM_2 = 0xBF,        /*Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the '/?' key*/
        VK_OEM_3 = 0xC0,        /*Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the '`~' key*/
        /* - */                                /* = 0xC1-D7 */   /*Reserved*/
        /* - */                             /* = 0xD8-DA */    /*Unassigned*/
        VK_OEM_4 = 0xDB,        /*Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the '[{' key*/
        VK_OEM_5 = 0xDC,        /*Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the '\|' key*/
        VK_OEM_6 = 0xDD,        /*Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the ']}' key*/
        VK_OEM_7 = 0xDE,        /*Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the 'single-quote/double-quote' key*/
        VK_OEM_8 = 0xDF,        /*Used for miscellaneous characters; it can vary by keyboard.*/
        /* = 0xE0 */        /*Reserved*/
        /* = 0xE1 */        /*OEM specific*/
        VK_OEM_102 = 0xE2,          /*The <> keys on the US standard keyboard, or the \\| key on the non-US 102-key keyboard*/
        /* = 0xE3-E4 */  /* OEM specific*/
        VK_PROCESSKEY = 0xE5,       /*IME PROCESS key*/
        /*= 0xE6,*/        /*OEM specific*/
        VK_PACKET = 0xE7,       /*Used to pass Unicode characters as if they were keystrokes. The VK_PACKET key is the low word of a 32-bit Virtual Key value used for non-keyboard input methods. For more information, see Remark in KEYBDINPUT, SendInput, WM_KEYDOWN, and WM_KEYUP*/
        VK_Unassigned0xE8 = 0xE8,       /*Unassigned*/
        /* = 0xE9-F5 */  /*  OEM specific*/
        VK_ATTN = 0xF6,         /*Attn key*/
        VK_CRSEL = 0xF7,        /*CrSel key*/
        VK_EXSEL = 0xF8,        /*ExSel key*/
        VK_EREOF = 0xF9,        /*Erase EOF key*/
        VK_PLAY = 0xFA,         /*Play key*/
        VK_ZOOM = 0xFB,         /*Zoom key*/
        VK_NONAME = 0xFC,       /*Reserved*/
        VK_PA1 = 0xFD,      /*PA1 key*/
        VK_OEM_CLEAR = 0xFE, 	    /*Clear key*/
    }

    internal class MyLowlevlhook : IAsyncDisposable
    {

        public async ValueTask DisposeAsync() => await Task.Run(() => { UninstallHook(); GC.SuppressFinalize(this); });

        private delegate IntPtr KeyboardHookHandler(int nCode, WMEvent wParam, TagKBDLLHOOKSTRUCT lParam);
        private KeyboardHookHandler? hookHandler;

        private IntPtr hookID = IntPtr.Zero;

        public void InstallHook()
        {
            hookHandler = HookFunc;
            hookID = SetHook(hookHandler);
        }

        ~MyLowlevlhook() => UninstallHook();

        public void UninstallHook() => UnhookWindowsHookEx(hookID);


        private readonly int WH_KEYBOARD_LL = 13;
        private readonly int WH_KEYBOARD = 2;

        private IntPtr SetHook(KeyboardHookHandler proc) => SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                 GetModuleHandleW(Process.GetCurrentProcess().MainModule is not ProcessModule module2 ? throw new NullReferenceException() : module2.ModuleName ?? throw new NullReferenceException()), 0);


        public delegate void KeyboardHookCallback(ViryalKey key);
        public event KeyboardHookCallback? KeyDown1;
        public event KeyboardHookCallback? KeyUp1;


        private List<(WMEvent,int)> test = new List<(WMEvent, int)>();
        private IntPtr HookFunc(int nCode, WMEvent wParam, TagKBDLLHOOKSTRUCT lParam)
        {

            //   test.Add((wParam, lParam.Flags));


            Console.WriteLine($"Эвент_{wParam}_ - клавиша _{lParam.Vkcode}_ - ScanCode _{lParam.ScanCode}_ Time _{lParam.Time}_{Environment.NewLine}" +
            $"Альт нажата? - {( (KeyStats)lParam.Flags ).ALTkeyIsPpressed}, Клавиша расширенная? - {( (KeyStats)lParam.Flags ).Extendedkey}, " +
            $"Cобытие инжектировано из низкого уровня? - {( (KeyStats)lParam.Flags ).EventjectedisLow}, Cобытие инжектировано? - {( (KeyStats)lParam.Flags ).EventIsInjected}, Отпущена ли клавиша? - {( (KeyStats)lParam.Flags ).KeyNotIsPressed} {Environment.NewLine} ");
 

            if (nCode >= 0)
            {
                //if (wParam is WM_KEYDOWN || wParam is WM_SYSKEYDOWN)

                //    KeyDown1?.Invoke((Viryalkey)Marshal.ReadInt32(lParam));
                //if (wParam is WM_KEYUP || wParam is WM_SYSKEYUP)

                //    KeyUp1?.Invoke((Viryalkey)Marshal.ReadInt32(lParam));
            }

            return CallNextHookEx(hookID, nCode, (int)wParam, lParam);
        }
        enum WMEvent
        {
            WM_KEYDOWN = 0x100,
            WM_SYSKEYDOWN = 0x104,
            WM_KEYUP = 0x101,
            WM_SYSKEYUP = 0x105
        }
        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-kbdllhookstruct?redirectedfrom=MSDN
        [StructLayout(LayoutKind.Sequential)]
        private struct TagKBDLLHOOKSTRUCT
        {
            internal readonly ViryalKey Vkcode;
            internal readonly int ScanCode;
            internal readonly int Flags;
            internal readonly int Time; // Милисикунды между сообщениями. Обнуляются при переполнинии.
            internal readonly UIntPtr DwExtraInfo;   //??
        }
        internal struct KeyStats
        {
            internal bool Extendedkey;
            internal bool EventjectedisLow;
            internal bool EventIsInjected;
            internal bool ALTkeyIsPpressed;
            internal bool KeyNotIsPressed;
            internal int CountRepeat;

            public static implicit operator KeyStats(int flags) => new KeyStats
            {
                Extendedkey = Convert.ToBoolean(flags >> 0),
                EventjectedisLow = Convert.ToBoolean(flags >> 1),
                EventIsInjected = Convert.ToBoolean(flags >> 4),
                ALTkeyIsPpressed = Convert.ToBoolean(flags >> 5),
                KeyNotIsPressed = Convert.ToBoolean(flags >> 7),
                CountRepeat = Convert.ToInt32(flags >> 15),
            };


           
        }




        #region WinAPI
        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int idHook, KeyboardHookHandler lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll")]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, int wParam, TagKBDLLHOOKSTRUCT lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr GetModuleHandleW([MarshalAs(UnmanagedType.LPWStr)] string lpModuleName);
        #endregion
    }
}
