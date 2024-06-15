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

    public delegate byte FMIFS_CALLBACK
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

    // Use this code behind of this method when you call it
    // FMIFS_CALLBACK formatCallBack = new FMIFS_CALLBACK(FormatCallBack); // see below to find FormatCallBack method
    public static async Task FormatVolume(string driveName, FMIFS_MEDIA_TYPE driveType, string fileSystem, string volumeLabel, bool quickFormat, FMIFS_CALLBACK formatCallBack)
    {
        byte qf = Convert.ToByte(quickFormat);
        await Task.Run(()=> Format(driveName, driveType, fileSystem, volumeLabel, qf, formatCallBack));
        
    }

    // use this method in your class to mange call back such as a trigger events.
    /* 
    public static byte FormatCallBack(FMIFS_PACKET_TYPE PacketType, uint PacketLength, IntPtr PacketData)
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
    */
}
