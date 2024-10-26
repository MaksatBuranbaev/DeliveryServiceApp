using DeliveryServiceApp.Models;
using DeliveryServiceApp.Services;
using Moq;

namespace DeliveryServiceApp.Tests;

public class OrderServiceTests
{
    [Fact]
    public void LoadOrdersTest()
    {
        var mockLogger = new Mock<ILoggerService>();
        var orderService = new OrderService(mockLogger.Object);
        var input = "ORD001,2.5,DistrictA,2024-10-25 10:00:00\n" +
                        "INVALID_LINE\n" +
                        "ORD002,abc,DistrictB,2024-10-25 10:15:00\n" +
                        "ORD003,3.0,DistrictA,2024-10-25 10:25:00";
        var inputFile = "test_orders.txt";

        File.WriteAllText(inputFile, input);

        var orders = orderService.LoadOrders(inputFile);

        Assert.Equal(2, orders.Count);
        Assert.Equal("ORD001", orders[0].OrderNumber);
        Assert.Equal("ORD003", orders[1].OrderNumber);

        File.Delete(inputFile);
    }

    [Fact]
    public void FilterOrdersTest()
    {
        var mockLogger = new Mock<ILoggerService>();
        var orderService = new OrderService(mockLogger.Object);

        var orders = new List<Order>
        {
            new Order { OrderNumber = "ORD001", WeightKg = 2.5, District = "DistrictA", DeliveryTime = new DateTime(2024, 10, 25, 10, 0, 0) },
            new Order { OrderNumber = "ORD002", WeightKg = 1.0, District = "DistrictA", DeliveryTime = new DateTime(2024, 10, 25, 10, 15, 0) },
            new Order { OrderNumber = "ORD003", WeightKg = 3.0, District = "DistrictA", DeliveryTime = new DateTime(2024, 10, 25, 10, 25, 0) },
            new Order { OrderNumber = "ORD004", WeightKg = 2.2, District = "DistrictA", DeliveryTime = new DateTime(2024, 10, 25, 10, 45, 0) }
        };
        var district = "DistrictA";
        var firstDeliveryTime = new DateTime(2024, 10, 25, 10, 0, 0);

        var filtered = orderService.FilterOrders(orders, district, firstDeliveryTime);

        Assert.Equal(3, filtered.Count);
        Assert.False(filtered.Exists(o => o.OrderNumber == "ORD004"));
    }

    [Fact]
    public void SaveFilteredOrdersTest()
    {
        var orders = new List<Order>
        {
            new Order { OrderNumber = "ORD001", WeightKg = 1.0, District = "DistrictA", DeliveryTime = new DateTime(2024, 10, 25, 10, 0, 0) },
            new Order { OrderNumber = "ORD002", WeightKg = 2.0, District = "DistrictB", DeliveryTime = new DateTime(2024, 10, 25, 10, 10, 0) },
            new Order { OrderNumber = "ORD003", WeightKg = 3.0, District = "DistrictC", DeliveryTime = new DateTime(2024, 10, 25, 10, 30, 0) }
        };

        var mockLogger = new Mock<ILoggerService>();
        var orderService = new OrderService(mockLogger.Object);
        var lines = new string[] { "ORD001,1,DistrictA,2024-10-25 10:00:00", "ORD002,2,DistrictB,2024-10-25 10:10:00", "ORD003,3,DistrictC,2024-10-25 10:30:00" };

        var outputFile = "test_orders.txt";

        orderService.SaveFilteredOrders(orders, outputFile);

        var output = File.ReadAllLines(outputFile);

        Assert.Equal(orders.Count, lines.Length);
        Assert.Equal(lines[0], output[0]);
        Assert.Equal(lines[1], output[1]);
        Assert.Equal(lines[2], output[2]);

        File.Delete(outputFile);
    }
}