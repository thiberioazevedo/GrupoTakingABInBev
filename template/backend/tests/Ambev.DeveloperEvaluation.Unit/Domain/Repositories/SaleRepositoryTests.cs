using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Repositories
{
    public class SaleRepositoryTests: BaseTest
    {
        readonly ISaleRepository _saleRepository;

        public SaleRepositoryTests()
        {
            _saleRepository = new SaleRepository(defaultContext);
        }

        [Fact]
        public async Task ShouldIncrementSequentiallyWhenCreated()
        {
            for (var i = 1; i <= 2; i += 1) 
            {
                // Arrange
                var sale = new Sale { };
                await _saleRepository.CreateAsync(sale);

                // Act
                var result = _saleRepository.GetLastNumber();

                // Assert
                Assert.Equal(i, sale.Number);
                Assert.Equal(i, result);
            }
        }
    }
}