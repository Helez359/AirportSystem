using DomainModel.Models;
using FlightManagementBlazorServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace FlightManagementBlazorServer.Pages
{
    public class PassengerListBase:ComponentBase
    {
        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }
        [Inject]
        private NavigationManager _navigationManager { get; set; }  
        [Inject]
        private PassengerService _passengerService { get; set; }
        [Parameter]
        public string flightId { get; set; }
        protected List<Passenger> Passengers { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authenticationState = await authenticationStateTask;
            if (!authenticationState.User.Identity.IsAuthenticated)
            {
                string returnUrl = WebUtility.UrlEncode($"/Passengers/{flightId}");
                _navigationManager.NavigateTo($"/identity/account/login?returnUrl={returnUrl}");
            }
            Passengers = await _passengerService.GetUncheckedPassengers(int.Parse(flightId));
        }
        protected async Task DeletePassengerAsync(int passengerId)
        {
            await _passengerService.DeletePassengerAsync(passengerId);
            Passengers = await _passengerService.GetUncheckedPassengers(int.Parse(flightId));
        }
    }
}
