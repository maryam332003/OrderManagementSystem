using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrdersManagement.Core.Entities;
using OrdersManagement.Core.Specifications;

namespace OrdersManagement.Repository.Specifications
{
	
	public class SpecifcationsEvaluator<T> where T : BaseEntity
	{
		public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecifications<T> spec)
		{
			var query = inputQuery;     // _context.Set<Product>()


			if (spec.Criteria is not null)
			{
				query = query.Where(spec.Criteria);
				// _context.Set<Porduct>().Where(P => P.Id == 3)
			}

			query = spec.Includes.Aggregate(query, (currentQuery, IncludeExpression) => currentQuery.Include(IncludeExpression));


			return query;


		}
	}






}
