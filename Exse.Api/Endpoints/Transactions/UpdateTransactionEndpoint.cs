using System.Security.Claims;
using Exse.Api.Common.Api;
using Exse.Core.Services;
using Exse.Core.Models;
using Exse.Core.Requests.Transactions;
using Exse.Core.Responses;

namespace Exse.Api.Endpoints.Transactions;

public class UpdateTransactionEndpoint : IEndpoint
{
  public static void Map(IEndpointRouteBuilder app)
      => app.MapPut("/{id}", HandleAsync)
          .WithName("Transactions: Update")
          .WithSummary("Atualiza uma transação")
          .WithDescription("Atualiza uma transação")
          .WithOrder(2)
          .Produces<Response<Transaction?>>();

  private static async Task<IResult> HandleAsync(
      ITransactionService service,
      UpdateTransactionRequest request,
      long id)
  {
    request.UserId = ApiConfiguration.UserId;
    request.Id = id;

    var result = await service.UpdateAsync(request);
    return result.IsSuccess
        ? TypedResults.Ok(result)
        : TypedResults.BadRequest(result);
  }
}