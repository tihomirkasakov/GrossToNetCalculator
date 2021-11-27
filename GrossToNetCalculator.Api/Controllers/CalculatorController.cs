using GrossToNetCalculator.Model.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrossToNetCalculator.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        private IMemoryCache _cache;

        public CalculatorController(IMemoryCache cache)
        {
            _cache = cache;
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Calculate(ContactDto model)
        {
            if (ModelState.IsValid)
            {
                ContactTaxesDto result;
                //string modelHash = model.GetSha256Hash<TaxPayerDto>();
                //if (_cache.TryGetValue(modelHash, out result))
                //{

                //}
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
