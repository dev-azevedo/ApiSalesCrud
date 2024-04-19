using AutoMapper;
using CamposDealerCrud.Exceptions;
using CamposDealerCrud.Model;
using CamposDealerCrud.Repository;
using CamposDealerCrud.Repository.Interfaces;
using CamposDealerCrud.Services.Interfaces;
using CamposDealerCrud.ViewModel;

namespace CamposDealerCrud.Services;

public class SaleService : ISaleService
{ 
    private readonly ISaleRepository _saleRepository;
    private readonly IProductRepository _productRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;

    public SaleService(ISaleRepository saleRepository, IProductRepository productRepository, IClientRepository clientRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _productRepository = productRepository;
        _clientRepository = clientRepository;
        _mapper = mapper;
    }

    public List<SaleRespViewModel> FindAll()
    {
        var sales = _saleRepository.FindAll();
        return _mapper.Map<List<SaleRespViewModel>>(sales);
    }

    public SaleRespViewModel FindById(Guid id)
    {
        var sale = _saleRepository.FindById(id);
        return _mapper.Map<SaleRespViewModel>(sale);
    }

    public List<SaleRespViewModel> FindAllByNameOrDescription(string nameOrDescription)
    {
        var sale = _saleRepository.FindAllByNameOrDescription(nameOrDescription);
        return _mapper.Map<List<SaleRespViewModel>>(sale);
    }

    public SaleRespViewModel Created(SalePostViewModel saleViewModel)
    {
        var sale = _mapper.Map<Sale>(saleViewModel);
        var product = _productRepository.FindById(saleViewModel.ProductId);
        var client = _clientRepository.FindById(saleViewModel.ClientId);

        if (product == null)
            throw new DomainException("Não encontramos o produto informado."); 
        if (client == null)
            throw new DomainException("Não encontramos o cliente informado.");

        sale.ValueSale = product.UnitaryValue * sale.ProductQuantity;
        _saleRepository.Created(sale);

        return _mapper.Map<SaleRespViewModel>(sale);
    }
   
    public void Updated(SalePutViewModel saleViewModel)
    {
        var saleExists = _saleRepository.Exists(saleViewModel.Id);
        if (!saleExists)
            throw new DomainException("Não encontramos a venda informada.");

        var sale = _mapper.Map<Sale>(saleViewModel);

        var product = _productRepository.FindById(saleViewModel.ProductId);
        var client = _clientRepository.FindById(saleViewModel.ClientId);

        if (product == null)
            throw new DomainException("Não encontramos o produto informado.");
        if (client == null)
            throw new DomainException("Não encontramos o cliente informado.");

        sale.ValueSale = product.UnitaryValue * sale.ProductQuantity;
        sale.EditedOn = DateTime.Now;
        _saleRepository.Update(sale);
    }

    public void Delete(Guid id)
    {
        _saleRepository.Delete(id);
    }

}
