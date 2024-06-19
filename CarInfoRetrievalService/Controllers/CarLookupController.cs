using CarInfoRetrievalService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarInfoRetrievalService.Controllers
{
    [Route("api/models")]
    [ApiController]
    public class CarLookupController : ControllerBase
    {
        private readonly CarMakeIdentifierService _carMakeIdentifierService;
        private readonly CarDataApiService _carDataApiService;

        public CarLookupController(CarMakeIdentifierService carMakeServiCarMakeIdentifierService,
                                   CarDataApiService carDataApiService)
        {
            _carMakeIdentifierService = carMakeServiCarMakeIdentifierService;
            _carDataApiService = carDataApiService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCarModelsByMakeAndYear([FromQuery] int modelYear,
                                                                   [FromQuery] string make)
        {
            if (string.IsNullOrEmpty(make) || modelYear <= 0)
            {
                return BadRequest(string.IsNullOrEmpty(make) ? "Missing required parameter: make" : "Invalid model year. Please enter a positive value.");
            }
            try
            {
                var makeId = _carMakeIdentifierService.FindMakeIdByMakeName(make);
                if (makeId != null)
                {
                    var carsInformation = await _carDataApiService.GetCarModelsByMakeAndYear(makeId, modelYear);
                    return Ok(carsInformation);
                }
                return NotFound("Car make not found.");
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Error retrieving car models: {ex.Message}");
            }
        }
    }
}
