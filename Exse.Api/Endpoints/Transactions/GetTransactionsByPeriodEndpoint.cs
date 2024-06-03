using System.Security.Claims;
using Exse.Api.Common.Api;
using Exse.Core;
using Exse.Core.Services;
using Exse.Core.Models;
using Exse.Core.Requests.Transactions;
using Exse.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Exse.Api.Endpoints.Transactions;

public class GetTransactionsByPeriodEndpoint : IEndpoint
{
  public static void Map(IEndpointRouteBuilder app)
      => app.MapGet("/", HandleAsync)
          .WithName("Transactions: Get All")
          .WithSummary("Recupera todas as transações")
          .WithDescription("Recupera todas as transações")
          .WithOrder(5)
          .Produces<PagedResponse<List<Transaction>?>>();

  private static async Task<IResult> HandleAsync(
      ITransactionService service,
      [FromQuery] DateTime? startDate = null,
      [FromQuery] DateTime? endDate = null,
      [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
      [FromQuery] int pageSize = Configuration.DefaultPageSize)
  {
    var request = new GetTransactionsByPeriodRequest
    {
      UserId = ApiConfiguration.UserId,
      PageNumber = pageNumber,
      PageSize = pageSize,
      StartDate = startDate,
      EndDate = endDate
    };

    var result = await service.GetByPeriodAsync(request);
    return result.IsSuccess
        ? TypedResults.Ok(result)
        : TypedResults.BadRequest(result);
  }
}