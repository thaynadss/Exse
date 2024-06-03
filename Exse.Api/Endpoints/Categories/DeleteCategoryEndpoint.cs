using System.Security.Claims;
using Exse.Api.Common.Api;
using Exse.Core.Services;
using Exse.Core.Models;
using Exse.Core.Requests.Categories;
using Exse.Core.Responses;

namespace Exse.Api.Endpoints.Categories;

public class DeleteCategoryEndpoint : IEndpoint
{
  public static void Map(IEndpointRouteBuilder app)
      => app.MapDelete("/{id}", HandleAsync)
          .WithName("Categories: Delete")
          .WithSummary("Exclui uma categoria")
          .WithDescription("Exclui uma categoria")
          .WithOrder(3)
          .Produces<Response<Category?>>();

  private static async Task<IResult> HandleAsync(
      ICategoryService service,
      long id)
  {
    var request = new DeleteCategoryRequest
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