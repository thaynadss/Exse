using System.Security.Claims;
using Exse.Api.Common.Api;
using Exse.Core.Services;
using Exse.Core.Models;
using Exse.Core.Requests.Transactions;
using Exse.Core.Responses;

namespace Exse.Api.Endpoints.Transactions;

public class DeleteTransactionEndpoint : IEndpoint
{
  public static void Map(IEndpointRouteBuilder app)
      => app.MapDelete("/{id}", HandleAsync)
          .WithName("Transactions: Delete")
          .WithSummary("Exclui uma transação")
          .WithDescription("Exclui uma transação")
          .WithOrder(3)
          .Produces<Response<Transaction?>>();

  private static async Task<IResult> HandleAsync(
      ITransactionService service,
      long id)
  {
    var request = new DeleteTransactionRequest
    {
      UserId = ApiConfiguration.UserId,
      Id = id
    };

    var result = await service.DeleteAsync(request);
    return result.IsSuccess
        ? TypedResults.Ok(result)
        : TypedResults.BadRequest(result);
  }
}