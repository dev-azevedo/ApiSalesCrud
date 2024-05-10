using AutoMapper;
using SalesCrud.Exceptions;
using SalesCrud.Model;
using SalesCrud.Repository;
using SalesCrud.Repository.Interfaces;
using SalesCrud.Services.Interfaces;
using SalesCrud.ViewModel;

namespace SalesCrud.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public List<ProductRespViewModel> FindAll()
    {
        var clients = _productRepository.FindAll();
        return _mapper.Map<List<ProductRespViewModel>>(clients);
    }

    public ProductRespViewModel FindById(Guid id)
    {
        var client = _productRepository.FindById(id);
        return _mapper.Map<ProductRespViewModel>(client);
    }

    public List<ProductRespViewModel> FindAllByDescription(string description)
    {
        var clients = _productRepository.FindAllByDescription(description);
        return _mapper.Map<List<ProductRespViewModel>>(clients);
    }
    
    public ProductRespViewModel Created(ProductPostViewModel productViewModel)
    {
        var productExists = _productRepository.FindByDescription(productViewModel.Description);
        if (productExists != null)
            throw new DomainException("Já existe cadastro de produto com a descrição informada.");


        var product = _mapper.Map<Product>(productViewModel);
        _productRepository.Created(product);

        return _mapper.Map<ProductRespViewModel>(product);
    }

    public void Updated(ProductPutViewModel productViewModel)
    {
        var productExists = _productRepository.Exists(productViewModel.Id);

        if (!productExists)
            throw new DomainException("Não encontramos o produto informado.");

        var productExistsByDescription = _productRepository.FindByDescription(productViewModel.Description);
        if (productExistsByDescription != null && productExistsByDescription.Id != productViewModel.Id)
            throw new DomainException("Já existe cadastro de produto com a descrição informada.");


        var product = _mapper.Map<Product>(productViewModel);
        product.EditedOn = DateTime.Now;
        _productRepository.Update(product);
    }

    public void Delete(Guid id)
    {
        _productRepository.Delete(id);
    }

   
}
