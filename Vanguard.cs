using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Vanguard
{
    class VLAuthentication
    {
        public static string PUBLIC_KEY = "eyJpdiI6InZ2bWw5TjE1aEdGWWJtN0VCUUFSZVE9PSIsInZhbHVlIjoiN3hQMXlPUlBCcjBKOUxaaEZZb1k4MUNreG54QitxbFk5dFN3YjY0UXRicz0iLCJtYWMiOiIyOGZlMTA4ZmU5YjU2ZDcwZjE4Mzk0ZDkwYTM2Y2MwNzVkZTg5Mzk3YzFlODA0NGExMjc0ZmFiMmE2ZGIyNDFlIn0=";
    }
    class Functions
    {
            public delegate void ResponseDelegate(string s);

            [DllImport(@"vanguard.dll", EntryPoint = "InitializeApplication", CallingConvention = CallingConvention.StdCall)]
            public static extern void InitializeApplication([MarshalAs(UnmanagedType.LPStr)] string apikey);

            [DllImport(@"vanguard.dll", EntryPoint = "Login", CallingConvention = CallingConvention.StdCall)]
            public static extern void Login([MarshalAs(UnmanagedType.LPStr)] string username, string password, ResponseDelegate response);
            
            [DllImport(@"vanguard.dll", EntryPoint = "LoginWithLicense", CallingConvention = CallingConvention.StdCall)]
            public static extern void LoginWithLicense([MarshalAs(UnmanagedType.LPStr)] string licensekey, ResponseDelegate response);

            [DllImport(@"vanguard.dll", EntryPoint = "GetModules", CallingConvention = CallingConvention.StdCall)]
            public static extern void GetModules(ResponseDelegate response);

            [DllImport(@"vanguard.dll", EntryPoint = "UsernameRecovery", CallingConvention = CallingConvention.StdCall)]
            public static extern void UsernameRecovery(ResponseDelegate response);
        
            [DllImport(@"vanguard.dll", EntryPoint = "EmailRecovery", CallingConvention = CallingConvention.StdCall)]
            public static extern void EmailRecovery(ResponseDelegate response);

            [DllImport(@"vanguard.dll", EntryPoint = "InjectModule", CallingConvention = CallingConvention.StdCall)]
            public static extern void InjectModule([MarshalAs(UnmanagedType.LPStr)] string ModuleName);

            [DllImport(@"vanguard.dll", EntryPoint = "Status", CallingConvention = CallingConvention.StdCall)]
            public static extern void Status(ResponseDelegate response);

    }
}
