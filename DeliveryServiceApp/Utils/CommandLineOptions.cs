namespace DeliveryServiceApp.Utils
{
    public class CommandLineOptions
    {
        public string? InputFilePath { get; set; }
        public string? DeliveryLogPath { get; set; }
        public string? DeliveryOrderPath { get; set; }
        public string? CityDistrict { get; set; }
        public DateTime FirstDeliveryDateTime { get; set; }

        public static bool TryParse(string[] args, out CommandLineOptions options, out string error)
        {
            options = new CommandLineOptions();
            error = null!;

            if (args.Length < 8)
            {
                error = "Недостаточно параметров. Необходимо указать все параметры: " +
                        "_inputFile, _deliveryLog, _deliveryOrder, _cityDistrict, _firstDeliveryDateTime.";
                return false;
            }

            try
            {
                for (int i = 0; i < args.Length; i += 2)
                {
                    var key = args[i].ToLower();
                    var value = args[i + 1];
                    switch (key)
                    {
                        case "_inputfile":
                            options.InputFilePath = value;
                            break;
                        case "_deliverylog":
                            options.DeliveryLogPath = value;
                            break;
                        case "_deliveryorder":
                            options.DeliveryOrderPath = value;
                            break;
                        case "_citydistrict":
                            options.CityDistrict = value;
                            break;
                        case "_firstdeliverydatetime":
                            if (!DateTime.TryParseExact(value, "yyyy-MM-dd HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out DateTime dt))
                            {
                                error = $"Неверный формат даты и времени: {value}. Ожидается формат yyyy-MM-dd HH:mm:ss";
                                return false;
                            }
                            options.FirstDeliveryDateTime = dt;
                            break;
                        default:
                            error = $"Неизвестный параметр: {key}";
                            return false;
                    }
                }

                if (string.IsNullOrEmpty(options.InputFilePath) ||
                    string.IsNullOrEmpty(options.DeliveryLogPath) ||
                    string.IsNullOrEmpty(options.DeliveryOrderPath) ||
                    string.IsNullOrEmpty(options.CityDistrict))
                {
                    error = "Одно или несколько обязательных параметров отсутствуют.";
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                error = $"Ошибка при разборе параметров: {ex.Message}";
                return false;
            }
        }
    }
}
