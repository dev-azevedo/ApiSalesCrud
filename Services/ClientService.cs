using AutoMapper;
using SalesCrud.Exceptions;
using SalesCrud.Model;
using SalesCrud.Repository.Interfaces;
using SalesCrud.Services.Interfaces;
using SalesCrud.ViewModel;

namespace SalesCrud.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;

    public ClientService(IClientRepository clientRepository, IMapper mapper)
    {
        _clientRepository = clientRepository;
        _mapper = mapper;
    }

    public async Task<(List<ClientRespViewModel>, int)> FindAll(int pageNumber, int pageSize)
    {
        var (clients, totalItems) = await _clientRepository.FindAll(pageNumber, pageSize);
        return (_mapper.Map<List<ClientRespViewModel>>(clients), totalItems);
    }

    public ClientRespViewModel FindById(Guid id)
    {
        var client = _clientRepository.FindById(id);
        return _mapper.Map<ClientRespViewModel>(client);
    }

    public List<ClientRespViewModel> FindAllByName(string name)
    {
        var clients = _clientRepository.FindAllByName(name);
        return _mapper.Map<List<ClientRespViewModel>>(clients);
    }

    public async Task<List<(ClientRespViewModel, int)>> FindBestSeller()
    {
        var topThreeClients = await _clientRepository.FindBestSeller();

        return topThreeClients.Select(c => (_mapper.Map<ClientRespViewModel>(c.Item1), c.Item2)).ToList();
    }

    public ClientRespViewModel Created(ClientPostViewModel clientViewModel)
    {
        var clientExists = _clientRepository.FindByEmail(clientViewModel.Email);
        if (clientExists != null)
            throw new DomainException("Já existe cadastro de cliente com o email informado.");


        var client = _mapper.Map<Client>(clientViewModel);
        _clientRepository.Created(client);

        return _mapper.Map<ClientRespViewModel>(client); ;
    }

    public void Updated(ClientPutViewModel clientViewModel)
    {
        var clientExists = _clientRepository.Exists(clientViewModel.Id);

        if (!clientExists)
            throw new DomainException("Não encontramos o cliente informado.");

        var clientExistsByEmail = _clientRepository.FindByEmail(clientViewModel.Email);
        if (clientExistsByEmail != null && clientExistsByEmail.Id != clientViewModel.Id)
            throw new DomainException("Já existe cadastro de cliente com o email informado.");

        var client = _mapper.Map<Client>(clientViewModel);
        client.EditedOn = DateTime.Now;
        _clientRepository.Update(client);
    }

    public void Delete(Guid id)
    {
        _clientRepository.Delete(id);
    }


}
