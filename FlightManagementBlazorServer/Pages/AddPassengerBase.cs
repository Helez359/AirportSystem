using DomainModel.Models;
using FlightManagementBlazorServer.Services;
using FlightManagementBlazorServer.ValidationModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementBlazorServer.Pages
{
    public class AddPassengerBase:ComponentBase
    {
        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }
        [Inject]
        private NavigationManager _navigationManager { get; set; }
        [Inject]
        private PassengerService _passengerService { get; set; }
        [Parameter]
        public string flightId { get; set; }

        public Passenger Passenger { get; set; }
        public NotificationDialog NotificationDialog { get; set; }
        public List<ValidationError> ValidationErrors { get; set; }
        public string ConcatenatedValidationErrors { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authenticationState = await authenticationStateTask;
            if (!authenticationState.User.Identity.IsAuthenticated)
            {
                string returnUrl = WebUtility.UrlEncode($"/AddPassenger");
                _navigationManager.NavigateTo($"/identity/account/login?returnUrl={returnUrl}");
            }
            Passenger = new Passenger();
        }
        protected void Close()
        {
            _navigationManager.NavigateTo("/");
        }
        public async Task AddPassengerAsync()
        {
            ValidationErrors = ValidatePassenger();
            if (ValidationErrors.Any())
            {
                ConcatenatedValidationErrors = GetConcatenatedValidationErrors(ValidationErrors);
                NotificationDialog.Show();
            }
            else
            {
                Passenger.FlightId = int.Parse(flightId);
                await _passengerService.AddPassengerAsync(Passenger, int.Parse(flightId));
                Close();
            }
        }
        protected List<ValidationError> ValidatePassenger()
        {
            var validationErrors = new List<ValidationError>();
            if (String.IsNullOrWhiteSpace(Passenger.Name))
                validationErrors.Add(new ValidationError { Description = "Please Insert Passenger Name!" });
            if (String.IsNullOrWhiteSpace(Passenger.LastName))
                validationErrors.Add(new ValidationError { Description = "Please Insert Passenger Last Name!" });
            if (String.IsNullOrWhiteSpace(Passenger.Gender))
                validationErrors.Add(new ValidationError { Description = "Please Insert Passenger Gender!" });
            return validationErrors;
        }
        protected string GetConcatenatedValidationErrors(List<ValidationError> validationErrors)
        {
            StringBuilder message = new StringBuilder();
            foreach (var error in validationErrors)
            {
                if (message.Length == 0)
                {
                    message.Append(error.Description);
                }
                else
                {
                    message.Append($"{Environment.NewLine}{error.Description}");
                }
            }

            return message.ToString();
        }
    }
}
