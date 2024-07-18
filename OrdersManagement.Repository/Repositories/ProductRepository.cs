using Microsoft.EntityFrameworkCore;
using OrdersManagement.Core;
using OrdersManagement.Core.Entities;
using OrdersManagement.Core.Repositories.Interfaces;
using OrdersManagement.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OrdersManagement.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly OrdersDbContext _context;
		private readonly IUnitOfWork _unitOfWork;

		public ProductRepository(OrdersDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
		{
			_context = context;
			_unitOfWork = unitOfWork;
		}

		public async Task<Product> AddProductAsync(Product product)
        {
            _context.Products.Add(product);

            await _context.SaveChangesAsync();

            return product;

        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);

            await _context.SaveChangesAsync();

        }


        public List<Product> GetAllProducts()
        {
            var product = _context.Products.ToList();

            return product;

        }

        public Product GetProductById(int id)
        {
            var product = _context.Products.Where(P => P.Id == id).FirstOrDefault();

            return product;
        }

		

		

		

		
	}
}
