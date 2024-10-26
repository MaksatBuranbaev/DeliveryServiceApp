using DeliveryServiceApp.Models;

namespace DeliveryServiceApp.Services
{
    public class OrderService
    {
        private readonly ILoggerService _logger;

        public OrderService(ILoggerService logger)
        {
            _logger = logger;
        }

        public List<Order> LoadOrders(string inputFilePath)
        {
            var orders = new List<Order>();
            try
            {
                _logger.LogInfo($"Чтение входного файла: {inputFilePath}");
                var lines = File.ReadAllLines(inputFilePath);
                int lineNumber = 0;
                foreach (var line in lines)
                {
                    lineNumber++;
                    if (Order.TryParse(line, out Order order, out string error))
                    {
                        orders.Add(order);
                    }
                    else
                    {
                        _logger.LogError($"Строка {lineNumber}: {error}");
                    }
                }
                _logger.LogInfo($"Загружено {orders.Count} заказов из {lines.Length} строк.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка при чтении файла: {ex.Message}");
                throw;
            }
            return orders;
        }

        public List<Order> FilterOrders(List<Order> orders, string district, DateTime firstDeliveryTime)
        {
            _logger.LogInfo($"Фильтрация заказов для района '{district}' в течение 30 минут после {firstDeliveryTime}");
            var filtered = orders
                .Where(o => o.District.Equals(district, StringComparison.OrdinalIgnoreCase) &&
                            o.DeliveryTime >= firstDeliveryTime &&
                            o.DeliveryTime <= firstDeliveryTime.AddMinutes(30))
                .OrderBy(o => o.DeliveryTime)
                .ToList();
            _logger.LogInfo($"Отфильтровано {filtered.Count} заказов.");
            return filtered;
        }

        public void SaveFilteredOrders(List<Order> orders, string outputFilePath)
        {
            try
            {
                _logger.LogInfo($"Запись отфильтрованных заказов в файл: {outputFilePath}");
                var lines = orders.Select(o => $"{o.OrderNumber},{o.WeightKg},{o.District},{o.DeliveryTime:yyyy-MM-dd HH:mm:ss}");
                File.WriteAllLines(outputFilePath, lines);
                _logger.LogInfo("Запись завершена успешно.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка при записи файла: {ex.Message}");
                throw;
            }
        }
    }
}
