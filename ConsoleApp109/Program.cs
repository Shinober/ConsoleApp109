using System;
using System.Collections.Generic;
using System.Linq;

class Order
{
    public double StartLatitude { get; set; }
    public double StartLongitude { get; set; }
    public double EndLatitude { get; set; }
    public double EndLongitude { get; set; }
    public double Price { get; set; }
}

class Courier
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}

class Program
{
    static void Main()
    {
        List<Order> orders = new List<Order>();
        List<Courier> couriers = new List<Courier>();

        Console.WriteLine("Введите детали заказов (Координаты вводите без точек и ковычек).\n Введите 'done' что бы закончить ввод данных заказа!");
        while (true)
        {
            Console.Write("Широта (A): ");
            if (!double.TryParse(Console.ReadLine(), out double startLat))
                break;

            Console.Write("Долгота (A): ");
            if (!double.TryParse(Console.ReadLine(), out double startLon))
                break;

            Console.Write("Широта (B): ");
            if (!double.TryParse(Console.ReadLine(), out double endLat))
                break;

            Console.Write("Долгота (B): ");
            if (!double.TryParse(Console.ReadLine(), out double endLon))
                break;

            Console.Write("Общая стоимость заказа: ");
            if (!double.TryParse(Console.ReadLine(), out double price))
                break;

            orders.Add(new Order { StartLatitude = startLat, StartLongitude = startLon, EndLatitude = endLat, EndLongitude = endLon, Price = price });
        }

        Console.WriteLine("\nВведите координаты местоположения курьеров.\nВведите 'done' что бы завершить ввод данных");
        while (true)
        {
            Console.Write("Широта: ");
            if (!double.TryParse(Console.ReadLine(), out double courierLat))
                break;

            Console.Write("Долгота: ");
            if (!double.TryParse(Console.ReadLine(), out double courierLon))
                break;

            couriers.Add(new Courier { Latitude = courierLat, Longitude = courierLon });
        }

        foreach (var order in orders.OrderBy(o => CalculateDistance(o.StartLatitude, o.StartLongitude, o.EndLatitude, o.EndLongitude)))
        {
            var courier = couriers.OrderBy(c => CalculateDistance(c.Latitude, c.Longitude, order.StartLatitude, order.StartLongitude)).First();
            Console.WriteLine($"Заказ из ({order.StartLatitude}, {order.StartLongitude}) до ({order.EndLatitude}, {order.EndLongitude}) с ценой заказа {order.Price}\n Назначен курьеру с координатами ({courier.Latitude}, {courier.Longitude})");
        }
    }

    static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        var R = 6371;
        var dLat = ToRadians(lat2 - lat1);
        var dLon = ToRadians(lon2 - lon1);
        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        var distance = R * c;
        return distance;
    }

    static double ToRadians(double angle)
    {
        return Math.PI * angle / 180.0;
    }
}
