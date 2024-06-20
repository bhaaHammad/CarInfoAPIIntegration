using CarInfoRetrievalService.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CarInfoRetrievalService.Controllers
{
    [Route("api/models")]
    [ApiController]
    public class CarLookupController : ControllerBase
    {
        private readonly CarMakeIdentifierService _carMakeIdentifierService;
        private readonly CarDataApiService _carDataApiService;
        private int currentYear = DateTime.Now.Year;

        public CarLookupController(CarMakeIdentifierService carMakeServiCarMakeIdentifierService,
                                   CarDataApiService carDataApiService)
        {
            _carMakeIdentifierService = carMakeServiCarMakeIdentifierService;
            _carDataApiService = carDataApiService;
        }
        [HttpGet]
        public async Task<IActionResult> GetCarModelsByMakeAndYear([FromQuery][Required][Range(1, int.MaxValue, ErrorMessage = "The value must be greater than zero.")] int modelYear,
                                                                   [FromQuery][Required] string make)
        {
            if(modelYear > currentYear) 
            {
                ModelState.AddModelError("modelYear", "The model year must be less than or equal to the current year.");
                return BadRequest(ModelState);
            }
            var makeId = _carMakeIdentifierService.FindMakeIdByMakeName(make);
            if (makeId != null)
            {
                var carsInformation = await _carDataApiService.GetCarModelsByMakeAndYear(makeId, modelYear);
                return Ok(carsInformation);
            }
            return NotFound("Car make not found.");
        }
    }
}
