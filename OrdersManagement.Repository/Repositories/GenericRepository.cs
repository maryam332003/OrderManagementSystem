using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrdersManagement.Core.Entities;
using OrdersManagement.Core.Repositories.Interfaces;
using OrdersManagement.Core.Specifications;
using OrdersManagement.Repository.Data;
using OrdersManagement.Repository.Specifications;
using OrdersManagement.Core;

namespace OrdersManagement.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {

        private readonly OrdersDbContext _context;
		private readonly IUnitOfWork _unitOfWork;

		public GenericRepository(OrdersDbContext context,IUnitOfWork unitOfWork)
        {
            _context = context;
			_unitOfWork = unitOfWork;
		}
		public async Task<IEnumerable<T>> GetAllAsync()
		{

		
			return await _context.Set<T>().ToListAsync();

		}
		public async Task<T?> GetByIdAsync(int id)
		{
			

			return await _context.Set<T>().FindAsync(id);
		}

		public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
		{

			
			return await ApplySpecification(spec).ToListAsync();
		}
		public IQueryable<T> ApplySpecification(ISpecifications<T> spec)
		{
			return SpecifcationsEvaluator<T>.GetQuery(_context.Set<T>() , spec);
		}

		public async Task<T> GetEntityWithSpecAsync(ISpecifications<T> spec)
		{
			return await ApplySpecification(spec).FirstOrDefaultAsync();
		}

		public async Task Add(T item)
		{

			await _context.Set<T>().AddAsync(item);
			await _context.SaveChangesAsync();

		}

		public async Task  Update(T item) {

			 _context.Set<T>().Update(item);
			await _context.SaveChangesAsync();
			Save();

		}


		public void Delete(T item) => _context.Set<T>().Remove(item);

		public int Save()
		{
			try
			{
				return _context.SaveChanges();
			}
			catch (Exception ex) 
			{
				throw ex; 
			}

		}

	}
}
