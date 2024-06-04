using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USBDevicesLibrary.USBDevices;

public class USBPipePolicies
{
    public USBPipePolicies()
    {
        
    }
    // https://learn.microsoft.com/en-us/windows-hardware/drivers/usbcon/winusb-functions-for-pipe-policy-modification
    // 0x01 SHORT_PACKET_TERMINATE
    public bool ShortPacketTerminate {  get; set; }
    // 0x02 AUTO_CLEAR_STALL
    public bool AutoClearStall { get; set; }
    // 0x03 PIPE_TRANSFER_TIMEOUT
    public uint PipeTransferTimeout { get; set; }
    // 0x04 IGNORE_SHORT_PACKETS
    public bool IgnoreShortPckets { get; set; }
    // 0x05 ALLOW_PARTIAL_READS
    public bool AllowPrtialReads { get; set; }
    // 0x06 AUTO_FLUSH
    public bool AutoFlush { get; set; }
    // 0x07 RAW_IO
    public bool RawIO { get; set; }
    // 0x08 MAXIMUM_TRANSFER_SIZE
    public uint MaximumTransferSize { get; set; }
    // 0x09 RESET_PIPE_ON_RESUME
    public bool ResetPipeOnRsume { get; set; }
}
