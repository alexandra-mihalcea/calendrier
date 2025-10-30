using Calendrier.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Calendrier.Services
{
    public class OrderService(AppSettingsService appSettingsService)
    {
        public AppSettings Settings { get; } = appSettingsService.Settings;

        private readonly JsonSerializerOptions options = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
        };

        public async Task<OrderData?> LoadOrderDataAsync()
        {
            // load existing data or create new
            var path = Settings.OrderDataFilePath;
            if (File.Exists(path))
            {
                var json = await File.ReadAllTextAsync(path);
                var loaded = JsonSerializer.Deserialize<OrderData>(json, options);
                if (loaded is not null)
                {
                    return loaded;
                }
            }
            else
            {
                var orderData = new OrderData
                {
                    Orders = [],
                    PaymentMethods = [],
                    ProductTypes = []
                };

                await SaveOrderDataAsync(orderData);

                return orderData;
            }

            return null;
        }



        public async Task SaveOrderDataAsync(OrderData orderData)
        {
            var path = Settings.OrderDataFilePath;

            string json = JsonSerializer.Serialize(orderData, options);
            await File.WriteAllTextAsync(path, json);
        }

        public async Task SaveOrderDataAsync(List<OrderItem> orderItems, List<PaymentMethodItem> paymentMethodItems, List<ProductTypeItem> productTypeItems)
        {
            var orderData = new OrderData
            {
                Orders = orderItems,
                PaymentMethods = paymentMethodItems,
                ProductTypes = productTypeItems
            };

            await SaveOrderDataAsync(orderData);
        }

    }
}
