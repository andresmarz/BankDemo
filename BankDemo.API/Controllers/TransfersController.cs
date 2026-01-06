using BankDemo.API.DTOs.Transfers;
using BankDemo.Application.Transfers.CreateTransfer;
using BankDemo.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace BankDemo.API.Controllers;

[ApiController]
[Route("api/transfers")]
public class TransfersController : ControllerBase
{
    private readonly CreateTransferHandler _handler;

    public TransfersController(CreateTransferHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTransferRequest request)
    {
        try
        {
            var command = new CreateTransferCommand
            {
                CustomerId = request.CustomerId,
                ClientRequestId = request.ClientRequestId,
                FromAccountId = request.FromAccountId,
                BeneficiaryId = request.BeneficiaryId,
                Amount = request.Amount,
                Currency = request.Currency,
                Description = request.Description
            };

            var transfer = await _handler.HandleAsync(command);

            return Ok(new CreateTransferResponse
            {
                TransferId = transfer.Id,
                Status = transfer.Status.ToString(),
                CreatedAt = transfer.CreatedAt
            });
        }
        catch (DomainException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
