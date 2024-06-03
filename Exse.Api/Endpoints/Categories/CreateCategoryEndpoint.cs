using System.Security.Claims;
using Exse.Api.Common.Api;
using Exse.Core.Services;
using Exse.Core.Models;
using Exse.Core.Requests.Categories;
using Exse.Core.Responses;

namespace Exse.Api.Endpoints.Categories;

public class CreateCategoryEndpoint : IEndpoint
{
  public static void Map(IEndpointRouteBuilder app)
      => app.MapPost("/", HandleAsync)
          .WithName("Categories: Create")
          .WithSummary("Cria uma nova categoria")
          .WithDescription("Cria uma nova categoria")
          .WithOrder(1)
          .Produces<Response<Category?>>();

  private static async Task<IResult> HandleAsync(
      ICategoryService service,
      CreateCategoryRequest request)
  {
    request.UserId = ApiConfiguration.UserId;
    var response = await service.CreateAsync(request);
    return response.IsSuccess
        ? TypedResults.Created($"v1/categories/{response.Data?.Id}", response)
        : TypedResults.BadRequest(response);
  }
}