using CarInfoRetrievalService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarInfoRetrievalService.Controllers
{
    [Route("api/[controller]")]
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
        public IActionResult GetCarModelsByMakeAndYear([FromQuery] string make,
                                                       [FromQuery] int modelYear)
        {
            var makeId = _carMakeIdentifierService.FindMakeIdByMakeName(make);
            if (makeId != null)
            {
                _carDataApiService.GetModelsForMakeIdWithYear(makeId, modelYear);
                return Ok("Success");
            }
            return BadRequest("makeId is null");
        }
    }
}
