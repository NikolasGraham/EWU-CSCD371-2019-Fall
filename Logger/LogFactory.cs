namespace Logger
{
    public class LogFactory
    {
        public BaseLogger CreateLogger(string className)
        {
            BaseLogger returnLogger = new FileLogger();
            return returnLogger;
        }
    }
}
