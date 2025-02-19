using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Unit
{
    public class BaseTest
    {
        internal readonly DefaultContext defaultContext;

        public BaseTest()
        {
            var dbContextOptions = new DbContextOptionsBuilder<DefaultContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;
            defaultContext = new DefaultContext(dbContextOptions);
        }
    }
}
