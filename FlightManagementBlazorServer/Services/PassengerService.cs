using DomainModel.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FlightManagementBlazorServer.Services
{
    public class PassengerService
    {
        private readonly HttpClient _httpClient;
        private readonly string BaseApiUrl = "https://localhost:44334/api/Passenger";
        public PassengerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Passenger>> GetUncheckedPassengers(int flightId)
        {
            return await _httpClient.GetFromJsonAsync<List<Passenger>>($"{BaseApiUrl}/passenger/{flightId}");
        }
        public async Task<List<Passenger>> GetAllUncheckedPassengers()
        {
            return await _httpClient.GetFromJsonAsync<List<Passenger>>(BaseApiUrl);
        }
        public async Task AddPassengerAsync(Passenger passenger, int flightId)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{BaseApiUrl}/AddPassenger/{flightId}");
            request.Content = new StringContent(JsonSerializer.Serialize(passenger), Encoding.UTF8, "application/json");
            await _httpClient.SendAsync(request);
        }
        public async Task<Passenger> GetPassengerAsync(int passengerId)
        {
            return await _httpClient.GetFromJsonAsync<Passenger>($"{BaseApiUrl}/{passengerId}");
        }
        public async Task UpdatePassengerAsync(Passenger passenger)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, BaseApiUrl);
            request.Content = new StringContent(JsonSerializer.Serialize(passenger), Encoding.UTF8, "application/json");
            await  _httpClient.SendAsync(request);
        }
        public async Task DeletePassengerAsync(int passengerId)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Delete, $"{BaseApiUrl}/{passengerId}");
            await _httpClient.SendAsync(httpRequest);
        }
        public async Task CheckPassengerAsync(int passengerId, int row, string seat)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Put, $"{BaseApiUrl}/checkPassenger/{passengerId}/{row}/{seat}");
            await _httpClient.SendAsync(httpRequest);
        }
        public async Task<List<Passenger>> GetCheckedPassengersAsync(int flightId)
        {
            return await _httpClient.GetFromJsonAsync<List<Passenger>>($"{BaseApiUrl}/checkedPassengers/{flightId}");
        }
    }
}
