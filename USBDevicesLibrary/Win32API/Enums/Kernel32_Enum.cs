using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// https://learn.microsoft.com/en-us/windows/win32/api/
//https://www.geoffchappell.com/studies/windows/win32/kernel32/api/index.htm

namespace USBDevicesLibrary.Win32API;

public static partial class Kernel32Data
{
    
    public enum ACCESSTYPES : uint
    {
        //  The following are masks for the predefined standard access types
        DELETE = 0x00010000,
        READ_CONTROL = 0x00020000,
        WRITE_DAC = 0x00040000,
        WRITE_OWNER = 0x00080000,
        SYNCHRONIZE = 0x00100000,
        STANDARD_RIGHTS_REQUIRED = 0x000F0000,
        STANDARD_RIGHTS_READ = READ_CONTROL,
        STANDARD_RIGHTS_WRITE = READ_CONTROL,
        STANDARD_RIGHTS_EXECUTE = READ_CONTROL,
        STANDARD_RIGHTS_ALL = 0x001F0000,
        SPECIFIC_RIGHTS_ALL = 0x0000FFFF,
        // AccessSystemAcl access type
        ACCESS_SYSTEM_SECURITY = 0x01000000,
        // MaximumAllowed access type
        MAXIMUM_ALLOWED = 0x02000000,
        //  These are the generic rights.
        GENERIC_READ = 0x80000000,
        GENERIC_WRITE = 0x40000000,
        GENERIC_EXECUTE = 0x20000000,
        GENERIC_ALL = 0x10000000,
    }

    public enum FilesAccessRights : uint
    {
        FILE_READ_DATA = 0x0001, // file & pipe
        FILE_LIST_DIRECTORY = 0x0001, // directory
        FILE_WRITE_DATA = 0x0002, // file & pipe
        FILE_ADD_FILE = 0x0002, // directory
        FILE_APPEND_DATA = 0x0004, // file
        FILE_ADD_SUBDIRECTORY = 0x0004, // directory
        FILE_CREATE_PIPE_INSTANCE = 0x0004, // named pipe
        FILE_READ_EA = 0x0008, // file & directory
        FILE_WRITE_EA = 0x0010, // file & directory
        FILE_EXECUTE = 0x0020, // file
        FILE_TRAVERSE = 0x0020, // directory
        FILE_DELETE_CHILD = 0x0040, // directory
        FILE_READ_ATTRIBUTES = 0x0080, // all
        FILE_WRITE_ATTRIBUTES = 0x0100, // all
        FILE_ALL_ACCESS = ACCESSTYPES.STANDARD_RIGHTS_REQUIRED | ACCESSTYPES.SYNCHRONIZE | 0x1FF,
        FILE_GENERIC_READ = ACCESSTYPES.STANDARD_RIGHTS_READ | FILE_READ_DATA | FILE_READ_ATTRIBUTES | FILE_READ_EA | ACCESSTYPES.SYNCHRONIZE,
        FILE_GENERIC_WRITE = ACCESSTYPES.STANDARD_RIGHTS_WRITE | FILE_WRITE_DATA | FILE_WRITE_ATTRIBUTES | FILE_WRITE_EA | FILE_APPEND_DATA | ACCESSTYPES.SYNCHRONIZE,
        FILE_GENERIC_EXECUTE = ACCESSTYPES.STANDARD_RIGHTS_EXECUTE | FILE_READ_ATTRIBUTES | FILE_EXECUTE | ACCESSTYPES.SYNCHRONIZE,
        FILE_SHARE_READ = 0x00000001,
        FILE_SHARE_WRITE = 0x00000002,
        FILE_SHARE_DELETE = 0x00000004,
        FILE_ATTRIBUTE_READONLY = 0x00000001,
        FILE_ATTRIBUTE_HIDDEN = 0x00000002,
        FILE_ATTRIBUTE_SYSTEM = 0x00000004,
        FILE_ATTRIBUTE_DIRECTORY = 0x00000010,
        FILE_ATTRIBUTE_ARCHIVE = 0x00000020,
        FILE_ATTRIBUTE_DEVICE = 0x00000040,
        FILE_ATTRIBUTE_NORMAL = 0x00000080,
        FILE_ATTRIBUTE_TEMPORARY = 0x00000100,
        FILE_ATTRIBUTE_SPARSE_FILE = 0x00000200,
        FILE_ATTRIBUTE_REPARSE_POINT = 0x00000400,
        FILE_ATTRIBUTE_COMPRESSED = 0x00000800,
        FILE_ATTRIBUTE_OFFLINE = 0x00001000,
        FILE_ATTRIBUTE_NOT_CONTENT_INDEXED = 0x00002000,
        FILE_ATTRIBUTE_ENCRYPTED = 0x00004000,
        FILE_ATTRIBUTE_INTEGRITY_STREAM = 0x00008000,
        FILE_ATTRIBUTE_VIRTUAL = 0x00010000,
        FILE_ATTRIBUTE_NO_SCRUB_DATA = 0x00020000,
        FILE_ATTRIBUTE_EA = 0x00040000,
        FILE_ATTRIBUTE_PINNED = 0x00080000,
        FILE_ATTRIBUTE_UNPINNED = 0x00100000,
        FILE_ATTRIBUTE_RECALL_ON_OPEN = 0x00040000,
        FILE_ATTRIBUTE_RECALL_ON_DATA_ACCESS = 0x00400000,
        TREE_CONNECT_ATTRIBUTE_PRIVACY = 0x00004000,
        TREE_CONNECT_ATTRIBUTE_INTEGRITY = 0x00008000,
        TREE_CONNECT_ATTRIBUTE_GLOBAL = 0x00000004,
        TREE_CONNECT_ATTRIBUTE_PINNED = 0x00000002,
        FILE_ATTRIBUTE_STRICTLY_SEQUENTIAL = 0x20000000,
        FILE_NOTIFY_CHANGE_FILE_NAME = 0x00000001,
        FILE_NOTIFY_CHANGE_DIR_NAME = 0x00000002,
        FILE_NOTIFY_CHANGE_ATTRIBUTES = 0x00000004,
        FILE_NOTIFY_CHANGE_SIZE = 0x00000008,
        FILE_NOTIFY_CHANGE_LAST_WRITE = 0x00000010,
        FILE_NOTIFY_CHANGE_LAST_ACCESS = 0x00000020,
        FILE_NOTIFY_CHANGE_CREATION = 0x00000040,
        FILE_NOTIFY_CHANGE_SECURITY = 0x00000100,
        FILE_ACTION_ADDED = 0x00000001,
        FILE_ACTION_REMOVED = 0x00000002,
        FILE_ACTION_MODIFIED = 0x00000003,
        FILE_ACTION_RENAMED_OLD_NAME = 0x00000004,
        FILE_ACTION_RENAMED_NEW_NAME = 0x00000005,
        //MAILSLOT_NO_MESSAGE = -1,
        //MAILSLOT_WAIT_FOREVER = -1,
        FILE_CASE_SENSITIVE_SEARCH = 0x00000001,
        FILE_CASE_PRESERVED_NAMES = 0x00000002,
        FILE_UNICODE_ON_DISK = 0x00000004,
        FILE_PERSISTENT_ACLS = 0x00000008,
        FILE_FILE_COMPRESSION = 0x00000010,
        FILE_VOLUME_QUOTAS = 0x00000020,
        FILE_SUPPORTS_SPARSE_FILES = 0x00000040,
        FILE_SUPPORTS_REPARSE_POINTS = 0x00000080,
        FILE_SUPPORTS_REMOTE_STORAGE = 0x00000100,
        FILE_RETURNS_CLEANUP_RESULT_INFO = 0x00000200,
        FILE_SUPPORTS_POSIX_UNLINK_RENAME = 0x00000400,
        FILE_SUPPORTS_BYPASS_IO = 0x00000800,
        FILE_SUPPORTS_STREAM_SNAPSHOTS = 0x00001000,
        FILE_SUPPORTS_CASE_SENSITIVE_DIRS = 0x00002000,
        FILE_VOLUME_IS_COMPRESSED = 0x00008000,
        FILE_SUPPORTS_OBJECT_IDS = 0x00010000,
        FILE_SUPPORTS_ENCRYPTION = 0x00020000,
        FILE_NAMED_STREAMS = 0x00040000,
        FILE_READ_ONLY_VOLUME = 0x00080000,
        FILE_SEQUENTIAL_WRITE_ONCE = 0x00100000,
        FILE_SUPPORTS_TRANSACTIONS = 0x00200000,
        FILE_SUPPORTS_HARD_LINKS = 0x00400000,
        FILE_SUPPORTS_EXTENDED_ATTRIBUTES = 0x00800000,
        FILE_SUPPORTS_OPEN_BY_FILE_ID = 0x01000000,
        FILE_SUPPORTS_USN_JOURNAL = 0x02000000,
        FILE_SUPPORTS_INTEGRITY_STREAMS = 0x04000000,
        FILE_SUPPORTS_BLOCK_REFCOUNTING = 0x08000000,
        FILE_SUPPORTS_SPARSE_VDL = 0x10000000,
        FILE_DAX_VOLUME = 0x20000000,
        FILE_SUPPORTS_GHOSTING = 0x40000000,
    }

    public enum FileConsatnts : uint
    {
        CREATE_NEW = 1,
        CREATE_ALWAYS = 2,
        OPEN_EXISTING = 3,
        OPEN_ALWAYS = 4,
        TRUNCATE_EXISTING = 5,
    }

    public enum FileFlags : uint
    {
        FILE_FLAG_WRITE_THROUGH = 0x80000000,
        FILE_FLAG_OVERLAPPED = 0x40000000,
        FILE_FLAG_NO_BUFFERING = 0x20000000,
        FILE_FLAG_RANDOM_ACCESS = 0x10000000,
        FILE_FLAG_SEQUENTIAL_SCAN = 0x08000000,
        FILE_FLAG_DELETE_ON_CLOSE = 0x04000000,
        FILE_FLAG_BACKUP_SEMANTICS = 0x02000000,
        FILE_FLAG_POSIX_SEMANTICS = 0x01000000,
        FILE_FLAG_SESSION_AWARE = 0x00800000,
        FILE_FLAG_OPEN_REPARSE_POINT = 0x00200000,
        FILE_FLAG_OPEN_NO_RECALL = 0x00100000,
        FILE_FLAG_FIRST_PIPE_INSTANCE = 0x00080000,
        FILE_FLAG_OPEN_REQUIRING_OPLOCK = 0x00040000,
        FILE_FLAG_IGNORE_IMPERSONATED_DEVICEMAP = 0x00020000,
    }

    // https://learn.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-getvolumeinformationw
    [Flags]
    public enum FILE_SYSTEM_FLAGS : uint
    {
        // The specified volume supports case-sensitive file names.
        FILE_CASE_SENSITIVE_SEARCH = 0x00000001,
        // The specified volume supports preserved case of file names when it places a name on disk.
        FILE_CASE_PRESERVED_NAMES = 0x00000002,
        // The specified volume supports Unicode in file names as they appear on disk.
        FILE_UNICODE_ON_DISK = 0x00000004,
        // The specified volume preserves and enforces access control lists (ACL). For example, the NTFS file system preserves and enforces ACLs, and the FAT file system does not.
        FILE_PERSISTENT_ACLS = 0x00000008,
        // The specified volume supports file-based compression.
        FILE_FILE_COMPRESSION = 0x00000010,
        // The specified volume supports disk quotas.
        FILE_VOLUME_QUOTAS = 0x00000020,
        // The specified volume supports sparse files.
        FILE_SUPPORTS_SPARSE_FILES = 0x00000040,
        // The specified volume supports reparse points.
        // ReFS: ReFS supports reparse points but does not index them so FindFirstVolumeMountPoint and FindNextVolumeMountPoint will not function as expected.
        FILE_SUPPORTS_REPARSE_POINTS = 0x00000080,
        // The file system supports remote storage.
        FILE_SUPPORTS_REMOTE_STORAGE = 0x00000100,
        // On a successful cleanup operation, the file system returns information that describes additional actions taken during cleanup, such as deleting the file.
        // File system filters can examine this information in their post-cleanup callback.
        FILE_RETURNS_CLEANUP_RESULT_INFO = 0x00000200,
        // The file system supports POSIX-style delete and rename operations.
        FILE_SUPPORTS_POSIX_UNLINK_RENAME = 0x00000400,
        // 
        FILE_SUPPORTS_BYPASS_IO = 0x00000800,
        // 
        FILE_SUPPORTS_STREAM_SNAPSHOTS = 0x00001000,
        // 
        FILE_SUPPORTS_CASE_SENSITIVE_DIRS = 0x00002000,
        // The specified volume is a compressed volume, for example, a DoubleSpace volume.
        FILE_VOLUME_IS_COMPRESSED = 0x00008000,
        // The specified volume supports object identifiers.
        FILE_SUPPORTS_OBJECT_IDS = 0x00010000,
        // The specified volume supports the Encrypted File System (EFS). For more information, see File Encryption.
        FILE_SUPPORTS_ENCRYPTION = 0x00020000,
        // The specified volume supports named streams.
        FILE_NAMED_STREAMS = 0x00040000,
        // The specified volume is read-only.
        FILE_READ_ONLY_VOLUME = 0x00080000,
        // The specified volume supports a single sequential write.
        FILE_SEQUENTIAL_WRITE_ONCE = 0x00100000,
        // The specified volume supports transactions. For more information, see About KTM.
        FILE_SUPPORTS_TRANSACTIONS = 0x00200000,
        // The specified volume supports hard links. For more information, see Hard Links and Junctions.
        // Windows Server 2008, Windows Vista, Windows Server 2003 and Windows XP: This value is not supported until Windows Server 2008 R2 and Windows 7.
        FILE_SUPPORTS_HARD_LINKS = 0x00400000,
        // The specified volume supports extended attributes. An extended attribute is a piece of application-specific metadata that an application can
        // associate with a file and is not part of the file's data.
        // Windows Server 2008, Windows Vista, Windows Server 2003 and Windows XP: This value is not supported until Windows Server 2008 R2 and Windows 7.
        FILE_SUPPORTS_EXTENDED_ATTRIBUTES = 0x00800000,
        // The file system supports open by FileID. For more information, see FILE_ID_BOTH_DIR_INFO.
        // Windows Server 2008, Windows Vista, Windows Server 2003 and Windows XP: This value is not supported until Windows Server 2008 R2 and Windows 7.
        FILE_SUPPORTS_OPEN_BY_FILE_ID = 0x01000000,
        // The specified volume supports update sequence number (USN) journals. For more information, see Change Journal Records.
        // Windows Server 2008, Windows Vista, Windows Server 2003 and Windows XP: This value is not supported until Windows Server 2008 R2 and Windows 7.
        FILE_SUPPORTS_USN_JOURNAL = 0x02000000,
        // The file system supports integrity streams.
        FILE_SUPPORTS_INTEGRITY_STREAMS = 0x04000000,
        // The specified volume supports sharing logical clusters between files on the same volume. The file system reallocates on writes to shared clusters.
        // Indicates that FSCTL_DUPLICATE_EXTENTS_TO_FILE is a supported operation.
        FILE_SUPPORTS_BLOCK_REFCOUNTING = 0x08000000,
        // The file system tracks whether each cluster of a file contains valid data (either from explicit file writes or automatic zeros) or
        // invalid data (has not yet been written to or zeroed).
        // File systems that use sparse valid data length (VDL) do not store a valid data length and do not require that valid data be contiguous within a file.
        FILE_SUPPORTS_SPARSE_VDL = 0x10000000,
        // The specified volume is a direct access (DAX) volume.
        // Note: This flag was introduced in Windows 10, version 1607.
        FILE_DAX_VOLUME = 0x20000000,
        // The file system supports ghosting.
        FILE_SUPPORTS_GHOSTING = 0x40000000,
    }

    public enum DRIVE_TYPE : uint
    {
        // The drive type cannot be determined.
        DRIVE_UNKNOWN = 0,
        // The root path is invalid; for example, there is no volume mounted at the specified path.
        DRIVE_NO_ROOT_DIR = 1,
        // The drive has removable media; for example, a floppy drive, thumb drive, or flash card reader.
        DRIVE_REMOVABLE = 2,
        // The drive has fixed media; for example, a hard disk drive or flash drive.
        DRIVE_FIXED = 3,
        // The drive is a remote (network) drive.
        DRIVE_REMOTE = 4,
        // The drive is a CD-ROM drive.
        DRIVE_CDROM = 5,
        // The drive is a RAM disk.
        DRIVE_RAMDISK = 6,
    }
}
