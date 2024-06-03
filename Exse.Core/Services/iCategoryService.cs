using Exse.Core.Models;
using Exse.Core.Requests.Categories;
using Exse.Core.Responses;

namespace Exse.Core.Services;

public interface ICategoryService
{
  Task<Response<Category?>> CreateAsync(CreateCategoryRequest request);
  Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request);
  Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request);
  Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request);
  Task<PagedResponse<List<Category>?>> GetAllAsync(GetAllCategoriesRequest request);
}