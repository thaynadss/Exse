using System.Security.Claims;
using Exse.Api.Common.Api;
using Exse.Core.Services;
using Exse.Core.Models;
using Exse.Core.Requests.Transactions;
using Exse.Core.Responses;

namespace Exse.Api.Endpoints.Transactions;

public class GetTransactionByIdEndpoint : IEndpoint
{
  public static void Map(IEndpointRouteBuilder app)
      => app.MapGet("/{id}", HandleAsync)
          .WithName("Transactions: Get By Id")
          .WithSummary("Recupera uma transação")
          .WithDescription("Recupera uma transação")
          .WithOrder(4)
          .Produces<Response<Transaction?>>();

  private static async Task<IResult> HandleAsync(
      ITransactionService service,
      long id)
  {
    var request = new GetTransactionByIdRequest
    {
      UserId = ApiConfiguration.UserId,
      Id = id
    };

    var result = await service.GetByIdAsync(request);
    return result.IsSuccess
        ? TypedResults.Ok(result)
        : TypedResults.BadRequest(result);
  }
}