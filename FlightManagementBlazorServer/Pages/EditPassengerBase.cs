using DomainModel.Models;
using FlightManagementBlazorServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net;
using System.Threading.Tasks;

namespace FlightManagementBlazorServer.Pages
{
    public class EditPassengerBase:ComponentBase
    {
        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }
        [Inject]
        private NavigationManager _navigationManager { get; set; }
        [Inject]
        private PassengerService _passengerService { get; set; }
        [Parameter]
        public string PassengerId { get; set; }
        public Passenger Passenger { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authenticationState = await authenticationStateTask;
            if (!authenticationState.User.Identity.IsAuthenticated)
            {
                string returnUrl = WebUtility.UrlEncode($"/EditPassenger/{PassengerId}");
                _navigationManager.NavigateTo($"/identity/account/login?returnUrl={returnUrl}");
            }
            Passenger = await _passengerService.GetPassengerAsync(int.Parse(PassengerId));
        }
        protected void Close()
        {
            _navigationManager.NavigateTo("/Passengers");
        }
        protected async Task UpdatePassengerAsync()
        {
            await _passengerService.UpdatePassengerAsync(Passenger);
            Close();
        }
    }
}
