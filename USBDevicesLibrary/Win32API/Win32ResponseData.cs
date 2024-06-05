using System.ComponentModel;

namespace USBDevicesLibrary.Win32API;


public class Win32ResponseDataStruct
{
    public Win32ResponseDataStruct()
    {
        Data = new();
        Exception = new();
        ErrorFunctionName = string.Empty;
        DataType = typeof(object);
    }
    public bool Status { get; set; }
    public object Data { get; set; }
    public Type DataType { get; set; }
    public uint LengthTransferred { get; set; }
    public Win32Exception Exception { get; set; }
    public string ErrorFunctionName { get; set; }

    public override string ToString()
    {
        if (Status)
        {
            return Exception.ToString();
        }
        else
        {
            return $"Function Name: {ErrorFunctionName}\r\n{Exception}";
        }
    }
}

