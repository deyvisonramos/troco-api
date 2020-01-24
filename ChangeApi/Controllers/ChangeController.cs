using ChangeApi.Data;
using ChangeApi.Domain.Entities;
using ChangeApi.Domain.Enums;
using ChangeApi.Domain.Services;
using ChangeApi.Dto;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChangeApi.Controllers
{
    /// <summary>
    /// Cálculos de trocos
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ChangeController : ControllerBase
    {
        private readonly ChangeContext _context;
        private readonly Change _changeService;
        private readonly IConfiguration _config;
        public ChangeController(ChangeContext context, Change changeService, IConfiguration configuration)
        {
            _context = context;
            _changeService = changeService;
            _config = configuration;
        }

        /// <summary>
        /// Calcula as cédulas e moedas para o troco dado um valor de conta e o valor pago
        /// </summary>
        /// <param name="calcDto">Objeto com as propriedades de total a pagar e total recebido</param>
        /// <returns>retorna mensagem de sucesso ou falha</returns>
        [HttpPost]
        [Route("calculate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Calculate([FromBody] CalculateDto calcDto)
        {
            var message = _changeService.Calc(calcDto.AmountPaid, calcDto.TotalAmountToPay);

            if (!_changeService.IsValid)
                return BadRequest(_changeService.GetErrors);

            var historyEntry = new ChangeHistory(_changeService.TotalChanged());

            foreach (var (value, quantity) in _changeService.Notes)
            {
                var historyEntryItem = new ChangeHistoryItem(ECurrencyType.Note, quantity, value);
                historyEntry.AddItem(historyEntryItem);
            }

            foreach (var (value, quantity) in _changeService.Coins)
            {
                var historyEntryItem = new ChangeHistoryItem(ECurrencyType.Coin, quantity, value);
                historyEntry.AddItem(historyEntryItem);
            }

            await _context.ChangeHistories.AddAsync(historyEntry);
            await _context.SaveChangesAsync();

            return Ok(message);
        }

        /// <summary>
        /// Obtem todo histórico de trocos realizados desde sempre
        /// </summary>
        /// <returns>retorna objeto de histórico com a lista das cédulas e moedas utilizadas</returns>
        [HttpGet]
        [Route("history")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWholeHistory()
        {
            IEnumerable<ChangeHistory> histories;

            using (SqlConnection conn = new SqlConnection(_config.GetConnectionString("LocalDb")))
            {
                histories = await conn.QueryAsync<ChangeHistory>("select * from ChangeHistories");

                foreach (var history in histories)
                {
                    var historyItens = await conn.QueryAsync<ChangeHistoryItem>("select * from ChangeHistoryItens where ChangeHistoryId = @historyId", new { historyId = history.Id });
                    history.AddItems(historyItens);
                }
            }

            return Ok(histories);
        }
    }
}