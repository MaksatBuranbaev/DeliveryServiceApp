namespace DeliveryServiceApp.Services
{
    public interface ILoggerService
    {
        void LogInfo(string message);
        void LogError(string message);
    }

    public class LoggerService : ILoggerService
    {
        private readonly string _logFilePath;
        private readonly object _lock = new object();

        public LoggerService(string logFilePath)
        {
            _logFilePath = logFilePath;
            if (!File.Exists(_logFilePath))
            {
                using (File.Create(_logFilePath)) { }
            }
        }

        public void LogInfo(string message)
        {
            WriteLog("INFO", message);
        }

        public void LogError(string message)
        {
            WriteLog("ERROR", message);
        }

        private void WriteLog(string level, string message)
        {
            var logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] {message}";
            lock (_lock)
            {
                using (StreamWriter sw = File.AppendText(_logFilePath))
                {
                    sw.WriteLine(logEntry);
                }
            }
        }
    }
}
