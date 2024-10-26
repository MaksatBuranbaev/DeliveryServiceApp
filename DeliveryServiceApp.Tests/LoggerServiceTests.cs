using DeliveryServiceApp.Services;

namespace DeliveryServiceApp.Tests
{
    public class LoggerServiceTests
    {
        [Fact]
        public void LoggerServiceLogInfo()
        {
            var logFilePath = "test_log.log";
            if (File.Exists(logFilePath))
                File.Delete(logFilePath);

            var logger = new LoggerService(logFilePath);
            logger.LogInfo("Test info message.");

            var logEntries = File.ReadAllLines(logFilePath);
            Assert.Single(logEntries);
            Assert.Contains("[INFO] Test info message.", logEntries[0]);
        }

        [Fact]
        public void LoggerServiceLogError()
        {
            var logFilePath = "test_log.log";
            if (File.Exists(logFilePath))
                File.Delete(logFilePath);

            var logger = new LoggerService(logFilePath);
            logger.LogError("Test error message.");

            var logEntries = File.ReadAllLines(logFilePath);
            Assert.Single(logEntries);
            Assert.Contains("[ERROR] Test error message.", logEntries[0]);
        }
    }
}