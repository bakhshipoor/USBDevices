using CopyFilesToFlash.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyFilesToFlash.Events;

public class FormatEventArgs : EventArgs
{
    public FormatEventArgs(Volume volume,bool formatIsWorking, bool formatIsSuccess, uint formatProgress)
    {
        Volume = volume;
        FormatIsWorking = formatIsWorking;
        FormatIsSuccess = formatIsSuccess;
        FormatProgress = formatProgress;
    }

    public Volume Volume { get; set; }
    public bool FormatIsWorking { get; set; }
    public bool FormatIsSuccess { get; set;}
    public uint FormatProgress { get; set;}
}
