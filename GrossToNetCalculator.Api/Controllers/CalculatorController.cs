using GrossToNetCalculator.Model.Dtos;
using GrossToNetCalculator.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;

namespace GrossToNetCalculator.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly ICalculatorService _calculatorService;
        private IMemoryCache _cache;

        public CalculatorController(ICalculatorService calculatorService, IMemoryCache cache)
        {
            _calculatorService = calculatorService;
            _cache = cache;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Calculate(ContactDto model)
        {
            if (ModelState.IsValid)
            {
                ContactTaxesDto result;
                string modelHash = model.GetSha256Hash<ContactDto>();
                if (_cache.TryGetValue(modelHash, out result))
                {
                    return Ok(result);
                }
                else
                {
                    result = _calculatorService.CalculateContactTaxes(model);
                    if (result == null)
                    {
                        return NotFound();
                    }
                    _cache.Set(modelHash, result);
                    return Ok(result);
                }
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
