# DeliveryServiceApp

**DeliveryServiceApp** — консольное приложение для службы доставки, которое фильтрует заказы по району и времени доставки.

## Требования

- **.NET SDK**: Версия 7.0 или выше

## Установка

1. **Клонирование репозитория:**

   ```bash
   git clone https://github.com/MaksatBuranbaev/DeliveryServiceApp

2. **Переход в директорию проекта:**

   ```bash
   cd DeliveryServiceApp

3. **Восстановление зависимостей:**

   ```bash
   dotnet restore

4. **Сборка проекта:**

   ```bash
   dotnet build

## Использование

1. **Пример запуска:**

   ```bash
   dotnet run -- -inputFile orders.txt -deliveryLog delivery.log -deliveryOrder filtered_orders.txt -cityDistrict DistrictA -firstDeliveryDateTime "2024-10-25 10:00:00"

2. **Запуск тестов:**

   ```bash
   dotnet test
