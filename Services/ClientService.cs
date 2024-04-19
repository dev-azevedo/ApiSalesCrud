using AutoMapper;
using CamposDealerCrud.Exceptions;
using CamposDealerCrud.Model;
using CamposDealerCrud.Repository;
using CamposDealerCrud.Repository.Interfaces;
using CamposDealerCrud.Services.Interfaces;
using CamposDealerCrud.ViewModel;

namespace CamposDealerCrud.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;

    public ClientService(IClientRepository clientRepository, IMapper mapper)
    {
        _clientRepository = clientRepository;
        _mapper = mapper;
    }

    public List<ClientRespViewModel> FindAll()
    {
        var clients = _clientRepository.FindAll();
        return _mapper.Map<List<ClientRespViewModel>>(clients);
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
    public ClientRespViewModel Created(ClientPostViewModel clientViewModel)
    {
        var clientDb = _clientRepository.FindByName(clientViewModel.Name);
        if (clientDb != null)
            throw new DomainException("Já existe cadastro de cliente com o nome informado.");


        var client = _mapper.Map<Client>(clientViewModel);
        _clientRepository.Created(client);

        return _mapper.Map<ClientRespViewModel>(client); ;
    }

    public void Updated(ClientPutViewModel clientViewModel)
    {
        var clientExists = _clientRepository.Exists(clientViewModel.Id);

        if (!clientExists)
            throw new DomainException("Não encontramos o cliente informado.");

        var client = _mapper.Map<Client>(clientViewModel);
        client.EditedOn = DateTime.Now;
        _clientRepository.Update(client);       
    }

    public void Delete(Guid id)
    {
        _clientRepository.Delete(id);
    }


}
