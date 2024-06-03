using Exse.Core.Models;
using Exse.Core.Requests.Transactions;
using Exse.Core.Responses;

namespace Exse.Core.Services;

public interface ITransactionService
{
  Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request);
  Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request);
  Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request);
  Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request);
  Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionsByPeriodRequest request);
}