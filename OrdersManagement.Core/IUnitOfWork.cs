using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrdersManagement.Core.Entities;
using OrdersManagement.Core.Repositories;
using OrdersManagement.Core.Repositories.Interfaces;

namespace OrdersManagement.Core
{
	public interface IUnitOfWork : IAsyncDisposable
	{
		IGenericRepository< T > Repository< T >() where T : BaseEntity ;
		Task<int> CompleteAsync();
	}
}
