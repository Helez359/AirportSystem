using DomainModel.Models;
using FlightManagementBlazorServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightManagementBlazorServer.Pages
{
    public class FlightListBase:ComponentBase
    {
        
        [Inject]
        private NavigationManager _navigationManager { get; set; }
        [Inject]
        private FlightService _flightService { get; set; }
        protected List<Flight> Flights;
        public ConfirmationDialog DeleteConfirmationDialog { get; set; }
        public ConfirmationDialog ArchiveConfirmationDialog { get; set; }
        public PassengerList PassengerList { get; set; }
        public int SelectedFlightId { get; set; }
        protected override async Task OnInitializedAsync()
        {
            
            Flights = await _flightService.GetFlights();

        }

        protected void OpenAddFlightPage()
        {
            _navigationManager.NavigateTo("/AddFlight");
        }

        protected async Task DeleteFlight(int flightId)
        {
            SelectedFlightId = flightId;
            DeleteConfirmationDialog.Show();
        }

        protected async Task OnDeleteConfirmationSelected(bool isDeleteConfirmed)
        {
            if (isDeleteConfirmed)
            {
                await _flightService.DeleteFlight(SelectedFlightId);
                Flights = await _flightService.GetFlights();
            }
        }

        protected async Task OnArchiveConfirmationSelected(bool isArchiveConfirmed)
        {
            if (isArchiveConfirmed)
            {
                await _flightService.ArchiveFlight(SelectedFlightId);
                Flights = await _flightService.GetFlights();
            }
        }

        protected async Task ArchiveFlight(int flightId)
        {
            SelectedFlightId = flightId;
            ArchiveConfirmationDialog.Show();
        }
        protected void GoToAddPassenger(int flightId)
        {
            _navigationManager.NavigateTo($"AddPassenger/{flightId}");
        }
        protected void GoToPassengerList(int flightId)
        {
            _navigationManager.NavigateTo($"/Passengers/{flightId}");
        }
        protected void GoToCheckedPassengerList(int flightId)
        {
            _navigationManager.NavigateTo($"/checkedPassengers/{flightId}");
        }
        protected void GoToCheckIn(int flightId)
        {
            _navigationManager.NavigateTo($"/CheckIn/{flightId}");
        }
    }
}
