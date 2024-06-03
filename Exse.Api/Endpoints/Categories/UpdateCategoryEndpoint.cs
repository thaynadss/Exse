using System.Security.Claims;
using Exse.Api.Common.Api;
using Exse.Core.Services;
using Exse.Core.Models;
using Exse.Core.Requests.Categories;
using Exse.Core.Responses;

namespace Exse.Api.Endpoints.Categories;

public class UpdateCategoryEndpoint : IEndpoint
{
  public static void Map(IEndpointRouteBuilder app)
      => app.MapPut("/{id}", HandleAsync)
          .WithName("Categories: Update")
          .WithSummary("Atualiza uma categoria")
          .WithDescription("Atualiza uma categoria")
          .WithOrder(2)
          .Produces<Response<Category?>>();

  private static async Task<IResult> HandleAsync(
      ICategoryService service,
      UpdateCategoryRequest request,
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