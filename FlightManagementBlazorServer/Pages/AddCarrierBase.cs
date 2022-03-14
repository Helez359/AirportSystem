using DomainModel.Models;
using FlightManagementBlazorServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net;
using System.Threading.Tasks;

namespace FlightManagementBlazorServer.Pages
{
    public class AddCarrierBase:ComponentBase
    {
        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }
        [Inject]
        private NavigationManager _navigationManager { get; set; }
        [Inject]
        private CarrierService _carrierService { get; set; }
        public Carrier Carrier { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authenticationState = await authenticationStateTask;
            if (!authenticationState.User.Identity.IsAuthenticated)
            {
                string returnUrl = WebUtility.UrlEncode($"/AddCarrier");
                _navigationManager.NavigateTo($"/identity/account/login?returnUrl={returnUrl}");
            }
            Carrier = new Carrier();
        }

        protected async Task AddCarrierAsync()
        {
            await _carrierService.AddCarrierAsync(Carrier);
            Close();
        }

        protected void Close()
        {
            _navigationManager.NavigateTo("/CarrierList");
        }
    }
}
