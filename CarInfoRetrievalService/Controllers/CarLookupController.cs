using CarInfoRetrievalService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarInfoRetrievalService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarLookupController : ControllerBase
    {
        private readonly CarMakeIdentifierService _carMakeIdentifierService;

        public CarLookupController(CarMakeIdentifierService carMakeServiCarMakeIdentifierService)
        {
            _carMakeIdentifierService = carMakeServiCarMakeIdentifierService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCarModelsByMakeAndYear([FromQuery] string make,
                                                                   [FromQuery] int modelYear)
        {
            var makeId = _carMakeIdentifierService.FindMakeIdByMakeName(make);
            return Ok(makeId);
        }
    }
}
