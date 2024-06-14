using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USBDevicesLibrary.Win32API;

public static partial class FmIfsData
{
    // https://github.com/microsoft/winfile/blob/master/src/fmifs.h

    public enum FMIFS_MEDIA_TYPE : uint
    {
        FmMediaUnknown,
        FmMediaF5_160_512,      // 5.25", 160KB,  512 bytes/sector
        FmMediaF5_180_512,      // 5.25", 180KB,  512 bytes/sector
        FmMediaF5_320_512,      // 5.25", 320KB,  512 bytes/sector
        FmMediaF5_320_1024,     // 5.25", 320KB,  1024 bytes/sector
        FmMediaF5_360_512,      // 5.25", 360KB,  512 bytes/sector
        FmMediaF3_720_512,      // 3.5",  720KB,  512 bytes/sector
        FmMediaF5_1Pt2_512,     // 5.25", 1.2MB,  512 bytes/sector
        FmMediaF3_1Pt44_512,    // 3.5",  1.44MB, 512 bytes/sector
        FmMediaF3_2Pt88_512,    // 3.5",  2.88MB, 512 bytes/sector
        FmMediaF3_20Pt8_512,    // 3.5",  20.8MB, 512 bytes/sector
        FmMediaRemovable,       // Removable media other than floppy
        FmMediaFixed,
        FmMediaF3_120M_512      // 3.5", 120M Floppy
    }

    public enum FMIFS_PACKET_TYPE
    {
        FmIfsPercentCompleted,
        FmIfsFormatReport,
        FmIfsInsertDisk,
        FmIfsIncompatibleFileSystem,
        FmIfsFormattingDestination,
        FmIfsIncompatibleMedia,
        FmIfsAccessDenied,
        FmIfsMediaWriteProtected,
        FmIfsCantLock,
        FmIfsCantQuickFormat,
        FmIfsIoError,
        FmIfsFinished,
        FmIfsBadLabel,
        // DBLSPACE_ENABLED
        /*
        FmIfsDblspaceCreateFailed,
        FmIfsDblspaceMountFailed,
        FmIfsDblspaceDriveLetterFailed,
        FmIfsDblspaceCreated,
        FmIfsDblspaceMounted,
        */
        // DBLSPACE_ENABLED
        FmIfsCheckOnReboot,
        FmIfsTextMessage,
        FmIfsHiddenStatus
    }

    public enum TEXT_MESSAGE_TYPE : uint
    {
        MESSAGE_TYPE_PROGRESS,
        MESSAGE_TYPE_RESULTS,
        MESSAGE_TYPE_FINAL
    }
}
