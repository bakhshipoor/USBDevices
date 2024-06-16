using CopyFilesToFlash.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyFilesToFlash.Events;

public class CopyEventArgs : EventArgs
{
    public CopyEventArgs(Volume volume,FileToCopy fileToCopy, bool copyStatus,[AllowNull] Exception? fileException=null)
    {
        Volume = volume;
        FileToCopy = fileToCopy;
        CopyStatus = copyStatus;
        FileException = fileException;
    }

    public Volume Volume { get; set; }
    public FileToCopy FileToCopy { get; set; }
    public bool CopyStatus { get; set; }
    public Exception? FileException { get; set; }
}
