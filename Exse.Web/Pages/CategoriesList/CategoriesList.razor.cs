using Exse.Core.Services;
using Exse.Core.Models;
using Exse.Core.Requests.Categories;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Exse.Web.Pages.CategoriesList;

public partial class CategoriesListPage : ComponentBase
{
  #region Properties

  public bool IsBusy { get; set; } = false;
  public List<Category> Categories { get; set; } = [];

  #endregion

  #region Services

  [Inject]
  public ISnackbar Snackbar { get; set; } = null!;

  [Inject]
  public IDialogService Dialog { get; set; } = null!;

  [Inject]
  public ICategoryService Service { get; set; } = null!;

  #endregion

  #region Overrides

  protected override async Task OnInitializedAsync()
  {
    IsBusy = true;
    try
    {
      var request = new GetAllCategoriesRequest();
      var result = await Service.GetAllAsync(request);
      if (result.IsSuccess)
        Categories = result.Data ?? [];
    }
    catch (Exception ex)
    {
      Snackbar.Add(ex.Message, Severity.Error);
    }
    finally
    {
      IsBusy = false;
    }
  }

  #endregion

  public async void OnDeleteButtonClickedAsync(long id, string title)
  {
    var result = await Dialog.ShowMessageBox(
        "ATENÇÃO",
        $"Tem certeza que deseja excluir a categoria {title}?",
        yesText: "Sim, continuar",
        cancelText: "Cancelar");

    if (result is true)
      await OnDeleteAsync(id, title);

    StateHasChanged();
  }

  public async Task OnDeleteAsync(long id, string title)
  {
    try
    {
      var request = new DeleteCategoryRequest
      {
        Id = id
      };
      await Service.DeleteAsync(request);
      Categories.RemoveAll(x => x.Id == id);
      Snackbar.Add($"Categoria {title} excluída", Severity.Info);
    }
    catch (Exception ex)
    {
      Snackbar.Add(ex.Message, Severity.Error);
    }
  }
}