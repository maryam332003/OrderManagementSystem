using OrdersManagement.Core.Dtos;
using OrdersManagement.Core.Repositories.Interfaces;
using OrdersManagement.Service.Product.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagement.Service.Product
{
    public class ValidateTheOrderServices : IValidateTheOrderServices
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IProductRepository productRepository;
		public ValidateTheOrderServices(IOrderRepository orderRepository, IProductRepository productRepository)
		{
			_orderRepository = orderRepository;
			this.productRepository = productRepository;
		}

		public bool Validate(int Cont, int ProductId)
		{

			var count = productRepository.GetProductById(ProductId).Stock;
			return count > Cont;


		}
		public decimal Discount(decimal TotalBeforeDiscount )
		{
			var result = 0;
			if( TotalBeforeDiscount >= 100 && TotalBeforeDiscount <= 200)
			{
				return TotalBeforeDiscount * (decimal)5.0 / (decimal)100.0;
			}
			else if (TotalBeforeDiscount >200)
				return TotalBeforeDiscount * (decimal)10.0 / (decimal)100.0;

			return result;
		}
	}
}
