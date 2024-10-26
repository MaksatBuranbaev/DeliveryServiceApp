using DeliveryServiceApp.Services;
using DeliveryServiceApp.Utils;

namespace DeliveryServiceApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!CommandLineOptions.TryParse(args, out CommandLineOptions options, out string? parseError))
            {
                Console.WriteLine($"Ошибка: {parseError}");
                ShowUsage();
                return;
            }

            ILoggerService logger = new LoggerService(options.DeliveryLogPath!);
            var orderService = new OrderService(logger);

            try
            {
                var orders = orderService.LoadOrders(options.InputFilePath!);
                var filteredOrders = orderService.FilterOrders(orders, options.CityDistrict!, options.FirstDeliveryDateTime);
                orderService.SaveFilteredOrders(filteredOrders, options.DeliveryOrderPath!);
                logger.LogInfo("Программа завершена успешно.");
                Console.WriteLine("Фильтрация заказов выполнена успешно.");
            }
            catch (Exception ex)
            {
                logger.LogError($"Необработанное исключение: {ex.Message}");
                Console.WriteLine($"Произошла ошибка: {ex.Message}. Подробности в логах.");
            }
        }

        static void ShowUsage()
        {
            Console.WriteLine("Использование:");
            Console.WriteLine("dotnet run _inputFile <путь_к_входному_файлу> " +
                              "_deliveryLog <путь_к_файлу_логов> " +
                              "_deliveryOrder <путь_к_результату> " +
                              "_cityDistrict <район> " +
                              "_firstDeliveryDateTime <yyyy-MM-dd HH:mm:ss>");
        }
    }
}
