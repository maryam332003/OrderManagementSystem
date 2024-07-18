using AutoMapper;
using OrdersManagement.Core.Dtos;
using OrdersManagement.Core.Entities;

namespace OrdersManagement.APIs.Helpers
{
	public class MappingProfile : Profile
	{
        public MappingProfile()
        {


			CreateMap<Product, ProductDto>().ReverseMap();
			
			CreateMap<Order, OrderDto>().ReverseMap();

			CreateMap<OrderItem, OrderItemDto>().ReverseMap();

			CreateMap<Invoice, InvoiceDto>().ReverseMap();
			


			CreateMap<Customer, CustomerDto>()
				              .ReverseMap();

			

		}



    }
}
