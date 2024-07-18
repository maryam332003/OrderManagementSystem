using OrdersManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagement.Core.Repositories.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        List<Product> GetAllProducts();

        Product GetProductById(int id);

        Task<Product> AddProductAsync(Product product);

        Task UpdateProductAsync(Product product);

    }
}
