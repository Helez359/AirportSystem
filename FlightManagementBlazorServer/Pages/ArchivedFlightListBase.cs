using DomainModel.Models;
using FlightManagementBlazorServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace FlightManagementBlazorServer.Pages
{
    public class ArchivedFlightListBase:ComponentBase
    {
        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }
        [Inject]
        private NavigationManager _navigationManager { get; set; }
        [Inject]
        private FlightService _flightService { get; set; }
        public List<Flight> Flights { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authenticationState = await authenticationStateTask;
            if (!authenticationState.User.Identity.IsAuthenticated)
            {
                string returnUrl = WebUtility.UrlEncode("/archivedFlightList");
                _navigationManager.NavigateTo($"/identity/account/login?returnUrl={returnUrl}");
            }
            Flights = await _flightService.GetArchivedFlights();
        }
    }
}
