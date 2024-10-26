using System.Globalization;

namespace DeliveryServiceApp.Models
{
    public class Order
    {
        public required string OrderNumber { get; set; }
        public double WeightKg { get; set; }
        public required string District { get; set; }
        public DateTime DeliveryTime { get; set; }

        public static bool TryParse(string line, out Order order, out string error)
        {
            order = null!;
            error = null!;
            var parts = line.Split(',');

            if (parts.Length != 4)
            {
                error = $"Неверное количество полей: {line}";
                return false;
            }

            var orderNumber = parts[0].Trim();
            if (string.IsNullOrEmpty(orderNumber))
            {
                error = $"Номер заказа пуст: {line}";
                return false;
            }

            if (!double.TryParse(parts[1].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out double weightKg) || weightKg <= 0)
            {
                error = $"Неверный вес заказа: {line}";
                return false;
            }

            var district = parts[2];
            if (string.IsNullOrEmpty(district))
            {
                error = $"Район заказа пуст: {line}";
                return false;
            }

            if (!DateTime.TryParseExact(parts[3], "yyyy-MM-dd HH:mm:ss", null, DateTimeStyles.None, out DateTime deliveryTime))
            {
                error = $"Неверный формат времени доставки: {line}";
                return false;
            }

            order = new Order
            {
                OrderNumber = orderNumber,
                WeightKg = weightKg,
                District = district,
                DeliveryTime = deliveryTime
            };

            return true;
        }
    }
}
