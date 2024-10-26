using DeliveryServiceApp.Utils;

namespace DeliveryServiceApp.Tests
{
    public class CommandLineOptionsTests
    {
        [Fact]
        public void CommandLineOptionsTryParseValidArgs()
        {
            var args = new string[]
            {
                "_inputFile", "orders.txt",
                "_deliveryLog", "delivery.log",
                "_deliveryOrder", "filtered_orders.txt",
                "_cityDistrict", "DistrictA",
                "_firstDeliveryDateTime", "2024-10-25 10:00:00"
            };

            var result = CommandLineOptions.TryParse(args, out CommandLineOptions options, out string? error);

            Assert.True(result);
            Assert.Null(error);
            Assert.Equal("orders.txt", options.InputFilePath);
            Assert.Equal("delivery.log", options.DeliveryLogPath);
            Assert.Equal("filtered_orders.txt", options.DeliveryOrderPath);
            Assert.Equal("DistrictA", options.CityDistrict);
            Assert.Equal(new DateTime(2024, 10, 25, 10, 0, 0), options.FirstDeliveryDateTime);
        }

        [Fact]
        public void CommandLineOptionsTryParseMissingParameters()
        {
            var args = new string[]
            {
                "_inputFile", "orders.txt",
                "_deliveryLog", "delivery.log"
            };

            var result = CommandLineOptions.TryParse(args, out CommandLineOptions options, out string? error);

            Assert.False(result);
            Assert.Equal("Недостаточно параметров. Необходимо указать все параметры: _inputFile, _deliveryLog, _deliveryOrder, _cityDistrict, _firstDeliveryDateTime.", error);
        }

    }
}