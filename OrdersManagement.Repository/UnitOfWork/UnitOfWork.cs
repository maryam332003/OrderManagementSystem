using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrdersManagement.Core;
using OrdersManagement.Core.Entities;
using OrdersManagement.Core.Repositories.Interfaces;
using OrdersManagement.Repository.Data;
using OrdersManagement.Repository.Repositories;

namespace OrdersManagement.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly OrdersDbContext _context;
		private Hashtable _repositories = new Hashtable();


		public UnitOfWork(OrdersDbContext context)
		{
			_context = context;
		}


		public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();


		public async ValueTask DisposeAsync() => await _context.DisposeAsync();


		public IGenericRepository<T> Repository<T>() where T : BaseEntity
		{
			var type = typeof(T).Name;

			if (!_repositories.ContainsKey(type))
			{

				var Repository = new GenericRepository<T>(_context,this);

				_repositories.Add(type, Repository);

			}

			return  ( IGenericRepository<T>)_repositories[type] ;
		}
	}
}
