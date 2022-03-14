using DomainModel.Models;
using FlightManagementBlazorServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;


namespace FlightManagementBlazorServer.Pages
{
    public class CarrierListBase : ComponentBase
    {
        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }
        [Inject]
        private NavigationManager _navigationManager { get; set; }
        [Inject]
        private CarrierService _carrierService { get; set; }
        public List<Carrier> Carriers { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authenticationState = await authenticationStateTask;
            if (!authenticationState.User.Identity.IsAuthenticated)
            {
                string returnUrl = WebUtility.UrlEncode("/CarrierList");
                _navigationManager.NavigateTo($"/identity/account/login?returnUrl={returnUrl}");
            }
            Carriers = await GetCarriersAsync();
        }
        protected void ShowAddCarrierPage()
        {
            _navigationManager.NavigateTo("/AddCarrier");
        }

        protected void OpenEditCarrierPage(int carrierId)
        {
            _navigationManager.NavigateTo($"/EditCarrier/{carrierId}");
        }

        protected async Task DeleteCarrierAsync(int carrierId)
        {
            await _carrierService.DeleteCarrierAsync(carrierId);
            Carriers = await GetCarriersAsync();
        }

        protected async Task<List<Carrier>> GetCarriersAsync()
        {
            return await _carrierService.GetCarriersAsync();
        }
    }
}
