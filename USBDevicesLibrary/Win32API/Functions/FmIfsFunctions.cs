using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static USBDevicesLibrary.Win32API.FmIfsData;

namespace USBDevicesLibrary.Win32API;

public static partial class FmIfsFunctions
{
    // https://github.com/microsoft/winfile/blob/master/src/fmifs.h

    private delegate byte FMIFS_CALLBACK
        (
        FMIFS_PACKET_TYPE PacketType,
        uint PacketLength,
        IntPtr PacketData
        );

    // https://github.com/microsoft/winfile/blob/master/src/fmifs.h#L336
    [DllImport("FMIFS.dll", EntryPoint = "Format", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern void Format
        (
        string DriveName,
        FMIFS_MEDIA_TYPE MediaType,
        string FileSystemName,
        string Label,
        byte Quick,
        FMIFS_CALLBACK callBackDelegate
        );

    public static void FormatFMIFS(string name)
    {
        FMIFS_CALLBACK fcb = new FMIFS_CALLBACK(formatCallBack);
        Format(name, FMIFS_MEDIA_TYPE.FmMediaRemovable, "exFAT", "zzzzz", 1, fcb);
    }

    private static byte formatCallBack(FMIFS_PACKET_TYPE PacketType, uint PacketLength, IntPtr PacketData)
    {
        switch (PacketType)
        {
            case FMIFS_PACKET_TYPE.FmIfsCantLock:
                break;
            case FMIFS_PACKET_TYPE.FmIfsPercentCompleted:
                FMIFS_PERCENT_COMPLETE_INFORMATION percent = Marshal.PtrToStructure<FMIFS_PERCENT_COMPLETE_INFORMATION>(PacketData);
                break;
            case FMIFS_PACKET_TYPE.FmIfsTextMessage:
                FMIFS_TEXT_MESSAGE message = Marshal.PtrToStructure<FMIFS_TEXT_MESSAGE>(PacketData);
                break;
            case FMIFS_PACKET_TYPE.FmIfsFinished:
                FMIFS_FINISHED_INFORMATION success = Marshal.PtrToStructure<FMIFS_FINISHED_INFORMATION>(PacketData);
                break;
            case FMIFS_PACKET_TYPE.FmIfsFormatReport:
                FMIFS_FORMAT_REPORT_INFORMATION formatReportInformation = Marshal.PtrToStructure<FMIFS_FORMAT_REPORT_INFORMATION>(PacketData);
                break;
        }
        return 1;
    }
}
