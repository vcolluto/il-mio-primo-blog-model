using System.Diagnostics;

namespace NetCore_01
{
    public class CustomConsoleLogger: ICustomLogger
    {
        public void WriteLog(string message)
        {
            Debug.WriteLine("LOG " + message);
        }
    }
}
