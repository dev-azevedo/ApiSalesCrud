using SalesCrud.ViewModel;

namespace SalesCrud.Services.Interfaces;

public interface IClientService
{
    List<ClientRespViewModel> FindAll();
    ClientRespViewModel FindById(Guid id);
    List<ClientRespViewModel> FindAllByName(string name);
    ClientRespViewModel Created(ClientPostViewModel clientViewModel);
    void Updated(ClientPutViewModel clientViewModel);
    void Delete(Guid id);
}
