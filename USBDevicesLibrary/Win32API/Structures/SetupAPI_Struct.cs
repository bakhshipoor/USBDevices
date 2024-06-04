using System.Collections;
using System.Runtime.InteropServices;

// https://learn.microsoft.com/en-us/windows/win32/api/setupapi/

namespace USBDevicesLibrary.Win32API;

public static partial class SetupAPIData
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SpDeviceInterfaceData
    {
        public uint CbSize;
        public Guid InterfaceClassGuid;
        public uint Flags;
        public IntPtr Reserved;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SP_DEVINFO_DATA
    {
        public uint CbSize;
        public Guid ClassGuid;
        public uint DevInst;    // DEVINST handle
        public IntPtr Reserved;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct SpDeviceInterfaceDetailData
    {
        public SpDeviceInterfaceDetailData()
        {
            if (IntPtr.Size == 8)
            {
                CbSize = 8;
            }
            else
            {
                CbSize = 4 + (uint)Marshal.SystemDefaultCharSize;
            }
            //CbSize = 8;
            DevicePath = string.Empty;
        }
        public uint CbSize;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string DevicePath;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SpDeviceInfoData
    {
        public uint CbSize;
        public Guid ClassGuid;
        public uint DevInst;
        public IntPtr Reserved;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DEVPROPKEY : IEqualityComparer
    {
        public DEVPROPKEY()
        {
            
        }

        public DEVPROPKEY(Guid f, int p) : this()
        {
            fmtid = f;
            pid = (uint)p;
        }

        public Guid fmtid;
        public uint pid;

        public new bool Equals(object? x, object? y)
        {
            if (ReferenceEquals(x, y)) return true;
            return false;
        }

        public override bool Equals(object? o)
        {
            if (o is null) return false;
            if (GetType() != o.GetType()) return false;
            return (fmtid == ((DEVPROPKEY)o).fmtid) && (pid == ((DEVPROPKEY)o).pid);
        }

        public override int GetHashCode() => (fmtid, pid).GetHashCode();

        public int GetHashCode(object obj) => GetHashCode();

        public static bool operator ==(DEVPROPKEY lhs, DEVPROPKEY rhs) => (lhs.fmtid == rhs.fmtid) && (lhs.pid == rhs.pid);

        public static bool operator !=(DEVPROPKEY lhs, DEVPROPKEY rhs) => !(lhs == rhs);

        public override string ToString()
        {
            return string.Format("{{{0}}}[{1}]", fmtid.ToString(), pid);
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct PSP_DRVINFO_DATA_W // _SP_DRVINFO_DATA_V2_W
    {
        public uint cbSize;
        public SPDIT DriverType;
        public ulong Reserved;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string Description;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string MfgName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string ProviderName;
        public long DriverDate;
        public ulong DriverVersion;
    }
}
