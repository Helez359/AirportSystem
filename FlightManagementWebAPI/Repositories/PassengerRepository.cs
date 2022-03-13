using DomainModel.Models;
using FlightManagementWebAPI.DatabaseContext;
using System.Collections.Generic;
using System.Linq;

namespace FlightManagementWebAPI.Repositories
{
    public class PassengerRepository
    {
        private readonly AirportSystemContext _airportSystemContext;
        public PassengerRepository(AirportSystemContext airportSystemContext)
        {
            _airportSystemContext = airportSystemContext;
        }
        public List<Passenger> GetPassengers(bool isChecked, int flightId)
        {
            return _airportSystemContext.Passengers.Where(passenger => passenger.IsChecked.Equals(isChecked)).Where(passenger => passenger.FlightId.Equals(flightId)).ToList();
        }
        public List<Passenger> GetAllPassengers(bool isChecked)
        {
            return _airportSystemContext.Passengers.Where(passenger => passenger.IsChecked.Equals(isChecked)).ToList();
        }

        public void AddPassenger(Passenger passenger)
        {
            _airportSystemContext.Passengers.Add(passenger);
            _airportSystemContext.SaveChanges();
        }
        public void UpdatePassenger(Passenger passenger)
        {
            var passengerToUpdate = GetPassenger(passenger.Id);
            if (passengerToUpdate != null)
            {
                passengerToUpdate.Id = passenger.Id;
                passengerToUpdate.Name = passenger.Name;
                passengerToUpdate.LastName = passenger.LastName;
                passengerToUpdate.Gender = passenger.Gender;

                _airportSystemContext.SaveChanges();
            }
        }
        public Passenger GetPassenger(int passengerId)
        {
            return _airportSystemContext.Passengers.FirstOrDefault(passenger => passenger.Id.Equals(passengerId));
        }
        public void DeletePassenger(int passengerId)
        {
            var passengerToDelete = GetPassenger(passengerId);
            if(passengerToDelete != null)
            {
                _airportSystemContext.Passengers.Remove(passengerToDelete);
                _airportSystemContext.SaveChanges();
            }
        }
        public void CheckPassenger(int passengerId, int row, string seat)
        {
            var passengerToCheck = GetPassenger(passengerId);
            if(passengerToCheck!=null)
            {
                passengerToCheck.IsChecked = true;
                passengerToCheck.Row = row;
                passengerToCheck.Seat = seat;
                _airportSystemContext.SaveChanges();
            }
        }

    }
}
