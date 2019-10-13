namespace Logger
{
    public class LogFactory
    {
        private string filePath;
        public BaseLogger CreateLogger(string className)
        {
            switch (filePath)
            {
                case null: return null;
                default: BaseLogger logger = new FileLogger(filePath) { className = className };
                    return logger;

            }
        }

        public void ConfigureFileLogger(string filePath)
        {
            this.filePath = filePath;
        }
    }
}
