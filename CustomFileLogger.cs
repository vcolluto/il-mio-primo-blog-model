namespace NetCore_01
{
    public class CustomFileLogger: ICustomLogger
    {
        public void WriteLog(string message)
        {
            File.AppendAllText("ex-log.txt", "LOG " + message + "\n");
        }
    }
}
