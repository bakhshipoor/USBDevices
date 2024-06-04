using System.ComponentModel;

namespace USBDevicesLibrary.Win32API;


public class Win32ResponseDataStruct
{
    public Win32ResponseDataStruct()
    {
        Data = new();
        ErrorDescription = string.Empty;
        ErrorFunctionName = string.Empty;
        DataType = typeof(object);
    }
    public bool Status { get; set; }
    public object Data { get; set; }
    public Type DataType { get; set; }
    public uint LengthTransferred { get; set; }
    public int ErrorCode { get; set; }
    public string ErrorDescription { get; set; }
    public string ErrorFunctionName { get; set; }

    public override string ToString()
    {
        string status = string.Empty;
        if (Status)
        {
            return "Every Things Is Well.";
        }
        else
        {
            return $"Function Name: {ErrorFunctionName}\r\nError Code: {ErrorCode}\r\nError Description: {new Win32Exception(ErrorCode).Message}";
        }
    }
}

