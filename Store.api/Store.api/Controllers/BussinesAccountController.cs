using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Store.Models.Models;
using Store.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BussinesAccountController : ControllerBase
    {
        private readonly ILogger<BussinesAccountController> _logger;
        private readonly IBussinesAccountService _bussinesAccountService;

        public BussinesAccountController(ILogger<BussinesAccountController> logger, IServiceProvider serviceProvider)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _bussinesAccountService = serviceProvider.GetService<IBussinesAccountService>();

            if (_bussinesAccountService == null)
            {
                throw new ArgumentNullException(nameof(_bussinesAccountService));
            }
        }

        [HttpGet("list-accounts")]
        [ProducesResponseType(typeof(List<BussinesAccountDetailsModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListAccountsAsync()
        {
            var listAccounts = await _bussinesAccountService.ListAsync().ConfigureAwait(false);

            return Ok(listAccounts);
        }

        [HttpGet("details-account/{idBussinesAccount}")]
        [ProducesResponseType(typeof(BussinesAccountDetailsModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DetailsAccountAsync(int idBussinesAccount)
        {
            var accountDetails = await _bussinesAccountService.DetailsAsync(idBussinesAccount).ConfigureAwait(false);

            if (accountDetails == null)
            {
                return NotFound();
            }

            return Ok(accountDetails);
        }

        [HttpGet("history-account/{idBussinesAccount}")]
        [ProducesResponseType(typeof(List<BussinesAccountHistoryDetailsModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> HistoryAccountAsync(int idBussinesAccount)
        {
            var historyAccount = await _bussinesAccountService.GetHistory(idBussinesAccount).ConfigureAwait(false);

            return Ok(historyAccount);
        }
        [HttpPost("register-new-account")]
        [ProducesResponseType(typeof(BussinesAccountDetailsModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAsync([FromBody] BussinesAccountCreateModel bussinesAccountCreate)
        {
            var idAccountCreated = await _bussinesAccountService.CreateAsync(bussinesAccountCreate).ConfigureAwait(false);

            var accountDetails = await _bussinesAccountService.DetailsAsync(idAccountCreated).ConfigureAwait(false);

            return Ok(accountDetails);
        }

        [HttpPut("edit-account/{idBussinesAccount}")]
        [ProducesResponseType(typeof(BussinesAccountDetailsModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditAccountAsync(int idBussinesAccount, [FromBody] BussinesAccountUpdateModel bussinesAccountUpdate)
        {
            if (idBussinesAccount == 0 || bussinesAccountUpdate == null || bussinesAccountUpdate != null && idBussinesAccount != bussinesAccountUpdate.Id)
            {
                return BadRequest();
            }

            try
            {
                await _bussinesAccountService.UpdateAsync(bussinesAccountUpdate).ConfigureAwait(false);
                var accountDetails = await _bussinesAccountService.DetailsAsync(idBussinesAccount).ConfigureAwait(false);

                return Ok(accountDetails);

            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }
    }
}
