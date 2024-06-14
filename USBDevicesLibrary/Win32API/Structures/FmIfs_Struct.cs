using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace USBDevicesLibrary.Win32API;

public static partial class FmIfsData
{
    // https://github.com/microsoft/winfile/blob/master/src/fmifs.h

    [StructLayout(LayoutKind.Sequential, Size = 4, CharSet = CharSet.Unicode)]
    public struct FMIFS_PERCENT_COMPLETE_INFORMATION
    {
        public uint PercentCompleted;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct FMIFS_FORMAT_REPORT_INFORMATION
    {
        public uint KiloBytesTotalDiskSpace;
        public uint KiloBytesAvailable;
    }

    public struct FMIFS_FINISHED_INFORMATION
    {
        public byte Success;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct FMIFS_TEXT_MESSAGE
    {
        public TEXT_MESSAGE_TYPE    MessageType;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
        public string Message;
    }

    
}