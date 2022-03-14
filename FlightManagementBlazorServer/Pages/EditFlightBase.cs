using DomainModel.Models;
using FlightManagementBlazorServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net;
using System.Threading.Tasks;

namespace FlightManagementBlazorServer.Pages
{
    public class EditFlightBase:ComponentBase
    {
        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }
        [Inject]
        private NavigationManager _navigationManager { get; set; }
        [Inject]
        private FlightService _flightService { get; set; }
        [Parameter]
        public string FlightId { get; set; }

        public Flight Flight { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authenticationState = await authenticationStateTask;
            if (!authenticationState.User.Identity.IsAuthenticated)
            {
                string returnUrl = WebUtility.UrlEncode($"/EditFlight/{FlightId}");
                _navigationManager.NavigateTo($"/identity/account/login?returnUrl={returnUrl}");
            }
            Flight = await _flightService.GetFlightAsync(int.Parse(FlightId));
        }

        protected void Close()
        {
            _navigationManager.NavigateTo("/");
        }

        protected async Task UpdateFlightAsync()
        {
            await _flightService.UpdateFlight(Flight);
            Close();
        }
    }
}
