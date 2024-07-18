using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrdersManagement.Core.Entities;
using OrdersManagement.Core.Specifications;

namespace OrdersManagement.Core.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity 
    {


		Task<IEnumerable<T>> GetAllAsync();
		Task<T?> GetByIdAsync(int id);

		// with specifications

		Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec);

		Task<T> GetEntityWithSpecAsync(ISpecifications<T> spec);

		



		Task Add(T item);
		Task Update(T item);
		//Task Delete(T item);

		int Save();


















	}
}
