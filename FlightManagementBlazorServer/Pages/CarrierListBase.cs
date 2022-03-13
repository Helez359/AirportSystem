using DomainModel.Models;
using FlightManagementBlazorServer.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace FlightManagementBlazorServer.Pages
{
    public class CarrierListBase : ComponentBase
    {
        [Inject]
        private NavigationManager _navigationManager { get; set; }
        [Inject]
        private CarrierService _carrierService { get; set; }
        public List<Carrier> Carriers { get; set; }

        protected override async Task OnInitializedAsync()
        {
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
