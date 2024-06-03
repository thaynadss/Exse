using System.Security.Claims;
using Exse.Api.Common.Api;
using Exse.Core.Services;
using Exse.Core.Models;
using Exse.Core.Requests.Categories;
using Exse.Core.Responses;

namespace Exse.Api.Endpoints.Categories;

public class GetCategoryByIdEndpoint : IEndpoint
{
  public static void Map(IEndpointRouteBuilder app)
      => app.MapGet("/{id}", HandleAsync)
          .WithName("Categories: Get By Id")
          .WithSummary("Recupera uma categoria")
          .WithDescription("Recupera uma categoria")
          .WithOrder(4)
          .Produces<Response<Category?>>();

  private static async Task<IResult> HandleAsync(
      ICategoryService service,
      long id)
  {
    var request = new GetCategoryByIdRequest
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