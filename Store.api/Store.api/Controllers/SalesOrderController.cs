using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SalesOrderController : ControllerBase
    {
        //[HttpGet("cancel-order/{docNum}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> CancelOrderAsync(int docNum)
        //{
        //    try
        //    {
        //        await _salesOrderService.CancelAsync(docNum).ConfigureAwait(false);
        //        return Ok();
        //    }
        //    catch (NullReferenceException ex)
        //    {
        //        return NotFound();
        //    }
        //}
    }
}
