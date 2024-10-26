using DeliveryServiceApp.Models;

namespace DeliveryServiceApp.Tests
{
    public class OrderTests
    {
        [Fact]
        public void OrderTryParseValidInput()
        {
            var line = "ORD001,2.5,DistrictA,2024-10-25 10:00:00";
            var result = Order.TryParse(line, out Order order, out string error);

            Assert.True(result);
            Assert.Null(error);
            Assert.Equal("ORD001", order.OrderNumber);
            Assert.Equal(2.5, order.WeightKg);
            Assert.Equal("DistrictA", order.District);
            Assert.Equal(new DateTime(2024, 10, 25, 10, 0, 0), order.DeliveryTime);
        }

        [Fact]
        public void OrderTryParseInvalidInput()
        {
            var line = "ORD002,abc,DistrictB,2024-10-25 10:15:00";
            var result = Order.TryParse(line, out Order order, out string error);

            Assert.False(result);
            Assert.Equal("Неверный вес заказа: ORD002,abc,DistrictB,2024-10-25 10:15:00", error);
            Assert.Null(order);
        }

    }
}