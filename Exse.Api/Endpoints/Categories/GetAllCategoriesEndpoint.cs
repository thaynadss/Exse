using System.Security.Claims;
using Exse.Api.Common.Api;
using Exse.Core;
using Exse.Core.Services;
using Exse.Core.Models;
using Exse.Core.Requests.Categories;
using Exse.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Exse.Api.Endpoints.Categories;

public class GetAllCategoriesEndpoint : IEndpoint
{
  public static void Map(IEndpointRouteBuilder app)
      => app.MapGet("/", HandleAsync)
          .WithName("Categories: Get All")
          .WithSummary("Recupera todas as categorias")
          .WithDescription("Recupera todas as categorias")
          .WithOrder(5)
          .Produces<PagedResponse<List<Category>?>>();

  private static async Task<IResult> HandleAsync(
      ICategoryService service,
      [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
      [FromQuery] int pageSize = Configuration.DefaultPageSize)
  {
    var request = new GetAllCategoriesRequest
    {
      UserId = ApiConfiguration.UserId,
      PageNumber = pageNumber,
      PageSize = pageSize,
    };

    var result = await service.GetAllAsync(request);
    return result.IsSuccess
        ? TypedResults.Ok(result)
        : TypedResults.BadRequest(result);
  }
}