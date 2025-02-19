using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class CreateSaleHandlerTests: BaseTest
{
    private readonly ISaleRepository _saleRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly CreateSaleHandler _handler;

    public CreateSaleHandlerTests()
    {
        _mapper = Substitute.For<IMapper>();
        _saleRepository = new SaleRepository(defaultContext);
        _productRepository = new ProductRepository(defaultContext);
        _handler = new CreateSaleHandler(_saleRepository, _productRepository, _mapper);
    }

    [Fact(DisplayName = "Given valid sale data When creating sale Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var product = new Product { Id = new Guid("474ba4e1-8594-4bad-a1f2-97475903447e"), Name = "Pen", UnitPrice = (decimal)10 };
        await _productRepository.CreateAsync(product);

        var command = new CreateSaleCommand(
            Guid.NewGuid(), 
            Guid.NewGuid(), 
            new List<DeveloperEvaluation.Application.Sales.SaleItem> { 
                new DeveloperEvaluation.Application.Sales.SaleItem { ProductId = product.Id, Quantity = 1 }});
        
        var sale = CommandToEntity(command);
        _mapper.Map<Sale>(command).Returns(sale);
        _mapper.Map<CreateSaleResult>(sale).Returns(EntityToResult(sale));

        // When
        var createSaleResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createSaleResult.Should().NotBeNull();
        createSaleResult.Id.Should().Be(createSaleResult.Id);
        createSaleResult.SaleItemCollection.Should().NotBeNull();
        (await _saleRepository.GetByIdAsync(sale.Id)).Should().NotBeNull();
    }

    [Fact(DisplayName = "Given invalid sale data When creating sale Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new CreateSaleCommand(Guid.NewGuid(), Guid.NewGuid(), new List<DeveloperEvaluation.Application.Sales.SaleItem> { });

        var sale = CommandToEntity(command);
        _mapper.Map<Sale>(command).Returns(sale);
        _mapper.Map<CreateSaleResult>(sale).Returns(EntityToResult(sale));

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    Sale CommandToEntity(CreateSaleCommand createSaleCommand) {
        return new Sale {
            SaleItemCollection = createSaleCommand.SaleItemCollection.Select(i => 
            new SaleItem { 
                UnitPrice = i.UnitPrice, 
                ProductId = i.ProductId,
                Quantity = i.Quantity
            }).ToList()
        };
    }

    CreateSaleResult EntityToResult(Sale sale) {
        return new CreateSaleResult {
            Id = sale.Id,
            SaleItemCollection = sale.SaleItemCollection.Select(i => 
            new DeveloperEvaluation.Application.Sales.SaleItem {
                UnitPrice = i.UnitPrice,
                ProductId = i.ProductId,
                Quantity = i.Quantity
            }).ToList()
        };
    }
}
