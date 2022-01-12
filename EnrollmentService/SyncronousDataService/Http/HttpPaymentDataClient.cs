using EnrollmentService.Data.DTO;
using Microsoft.Extensions.Configuration;
using PlatformService.SyncDataServices.Http;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EnrollmentService.SyncronousDataService.Http
{
    public class HttpPaymentDataClient : IPaymentDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpPaymentDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task SendPlatformToCommand(CreateEnrollDto enroll)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(enroll),
                Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_configuration["PaymentService"],
                httpContent);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync POST to CommandService was OK !");
            }
            else
            {
                Console.WriteLine("--> Sync POST to CommandService failed");
            }
        }
    }
}
