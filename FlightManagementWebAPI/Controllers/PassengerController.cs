using DomainModel.Models;
using FlightManagementWebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        private readonly PassengerRepository _passengerRepository;
        public PassengerController(PassengerRepository passengerRepository)
        {
            _passengerRepository = passengerRepository;
        }
        [HttpGet]
        public IActionResult GetAllPassengers()
        {
            try
            {
                var passengers = _passengerRepository.GetAllPassengers(false);
                return Ok(passengers);
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("passenger/{flightId:int}")]
        public IActionResult GetPassengers(int flightId)
        {
            try
            {
                var passengers = _passengerRepository.GetPassengers(false, flightId);
                return Ok(passengers);
            }
            catch(System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost("AddPassenger/{flightId}")]
        public IActionResult AddPassenger([FromBody] Passenger passenger, int flightId)
        {
            if(passenger == null)
            {
                return BadRequest();
            }
            try
            {
                passenger.FlightId = flightId;
                _passengerRepository.AddPassenger(passenger);
                return Ok();
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut]
        public IActionResult UpdatePassenger([FromBody] Passenger passenger)
        {
            if(passenger == null)
            {
                return BadRequest();
            }

            try
            {
                _passengerRepository.UpdatePassenger(passenger);
                return Ok();
            }
            catch(System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("{passengerId:int}")]
        public IActionResult GetPassenger(int passengerId)
        {
            try
            {
                return Ok(_passengerRepository.GetPassenger(passengerId));
            }
            catch(System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete("{passengerId:int}")]
        public IActionResult DeletePassenger(int passengerId)
        {
            try
            {
                _passengerRepository.DeletePassenger(passengerId);
                return Ok();
            }
            catch(System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut("checkPassenger/{passengerId:int}/{row:int}/{seat}")]
        public IActionResult CheckPassenger(int passengerId, int row, string seat)
        {
            try
            {
                _passengerRepository.CheckPassenger(passengerId, row, seat);
                return Ok();
            }
            catch(System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("checkedPassengers/{flightId:int}")]
        public IActionResult GetCheckedPassengers(int flightId)
        {
            try
            {
                return Ok(_passengerRepository.GetPassengers(true, flightId));
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
