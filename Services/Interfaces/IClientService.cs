using SalesCrud.ViewModel;

namespace SalesCrud.Services.Interfaces;

public interface IClientService
{
    Task<(List<ClientRespViewModel>, int)> FindAll(int pageNumber, int pageSize);
    ClientRespViewModel FindById(Guid id);
    List<ClientRespViewModel> FindAllByName(string name);
    Task<List<(ClientRespViewModel, int)>> FindBestSeller();
    ClientRespViewModel Created(ClientPostViewModel clientViewModel);
    void Updated(ClientPutViewModel clientViewModel);
    void Delete(Guid id);
}
